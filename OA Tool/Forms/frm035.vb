Imports System.Windows.Forms
Imports SAPCOM.SAPTextIDs

Public Class frm035
    Public lsRequisition As String = ""
    Dim cn As New OAConnection.Connection
    Dim Requi As New DataTable
    Private _Is4BR As Boolean = False ' Determina si hay requisiciones para Brazil, proyecto GR/IR 
    Private _IsGoods As Boolean = False

    ''' <summary>
    ''' Determina si hay requisiciones para Brazil, proyecto GR/IR 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Is4BR() As Boolean
        Get
            Return _Is4BR
        End Get
        Set(ByVal value As Boolean)
            _Is4BR = value
        End Set
    End Property


    Private Sub LoadInfo()
        Requi = cn.RunSentence("Select * From TmpCrearPO Where Usuario = '" & gsUsuarioPC & "'").Tables(0)
        Me.dtgRequisiciones.DataSource = Requi

        Me.dtgRequisiciones.Columns("SAPBox").ReadOnly = True
        Me.dtgRequisiciones.Columns("Requisition").ReadOnly = True
        Me.dtgRequisiciones.Columns("Item").ReadOnly = True
        Me.dtgRequisiciones.Columns("Usuario").ReadOnly = True
        Me.dtgRequisiciones.Columns("New_PO").ReadOnly = True
        Me.dtgRequisiciones.Columns("Created").ReadOnly = True

        Me.dtgRequisiciones.Columns("SAPBox").DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
        Me.dtgRequisiciones.Columns("Requisition").DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
        Me.dtgRequisiciones.Columns("Item").DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
        Me.dtgRequisiciones.Columns("Usuario").DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
        Me.dtgRequisiciones.Columns("New_PO").DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
        Me.dtgRequisiciones.Columns("Created").DefaultCellStyle.BackColor = Drawing.Color.Gainsboro

        Check_Almacenaje()
        Check_IROnly()
    End Sub

    Private Sub frm035_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cn.LoadCombo(cboHT_HN, "Select IdComentarios,Descripcion From HeaderText_Note Where TNumber = '" & gsUsuario & "'", "IdComentarios", "Descripcion")

        If Is4BR Then
            Dim F As New frm085
            F.ShowDialog()
            _IsGoods = F.POType
            If _IsGoods Then
                cn.ExecuteInServer("Update tmpCrearPO Set [GR_BsdIV] = 1 Where Usuario = '" & gsUsuarioPC & "'")
            End If
        End If

        LoadInfo()
    End Sub

    Private Sub dtgRequisiciones_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgRequisiciones.KeyDown

        Select Case e.KeyCode
            Case Keys.V
                PasteClipboardToDataGridView(Me.dtgRequisiciones)

            Case Keys.Delete
                If MsgBox("Remove requisition " & dtgRequisiciones.CurrentRow.Cells("Requisition").Value & " Item " & dtgRequisiciones.CurrentRow.Cells("Item").Value & " from creation process?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Remove Item") = MsgBoxResult.Yes Then
                    cn.ExecuteInServer("Delete From TmpCrearPO Where Usuario = '" & gsUsuarioPC & "' And Requisition = " & dtgRequisiciones.CurrentRow.Cells("Requisition").Value & " And Item = " & dtgRequisiciones.CurrentRow.Cells("Item").Value)
                    Dim r As DataRow

                    For Each r In Requi.Rows
                        If r.RowState <> DataRowState.Deleted Then
                            If (r("Requisition") = dtgRequisiciones.CurrentRow.Cells("Requisition").Value) And (r("Item") = dtgRequisiciones.CurrentRow.Cells("Item").Value) Then
                                r.Delete()
                                Exit Sub
                            End If
                        End If

                    Next
                End If

        End Select
    End Sub

    ''' <summary>
    ''' Crea Ordenes de compra por caja de SAP
    ''' </summary>
    ''' <param name="Box">Nombre de la caja de SAP</param>
    ''' <remarks></remarks>
    Private Sub Create_POs(ByVal Box$)
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim POC As New SAPCOM.POCreator(Box, gsUsuarioPC, AppId)
        Dim Vendor As New DataTable
        Dim RequisXVendor As New DataTable

        If Me.cmdAgrupar.Checked Then
            '********************************************************************************************
            '********************************************************************************************
            '            Codigo para agrupamiento de las requis
            '********************************************************************************************
            '********************************************************************************************
            Vendor = cn.RunSentence("Select Distinct Vendor,PG,Cur,POrg,SAPBox From TmpCrearPO Where Created = 0 And Usuario = '" & gsUsuario & "' And SAPBox = '" & Box & "' Group by Vendor,SAPBox,Cur,PG,POrg").Tables(0)

            If Vendor.Rows.Count > 0 Then
                For I = 0 To Vendor.Rows.Count - 1
                    RequisXVendor = cn.RunSentence("Select * From TmpCrearPO Where Created = 0 And Usuario = '" & gsUsuarioPC & "' And SAPBox = '" & Box & "' And Vendor = '" & Vendor.Rows(I).Item("Vendor") & "' And Cur = '" & Vendor.Rows(I).Item("Cur") & "' And PG = '" & Vendor.Rows(I).Item("PG") & "' Order by Requisition, Item").Tables(0)
                    If RequisXVendor.Rows.Count > 0 Then
                        POC.CreateNewPO("NB")
                        POC.ItemInterval = 10
                        POC.AppendHeaderText(HeaderText, RequisXVendor.Rows(0).Item("HText"))
                        POC.AppendHeaderText(HeaderNote, RequisXVendor.Rows(0).Item("HNote"))

                        For J = 0 To RequisXVendor.Rows.Count - 1
                            '*********************************************
                            'Creación de las líneas de la PO Item por Item
                            '*********************************************

                            POC.AdoptReqItem(CStr(Int(RequisXVendor.Rows(J).Item("Requisition"))), CStr(RequisXVendor.Rows(J).Item("Item")))

                            POC.GRBasedIV = (RequisXVendor.Rows(J).Item("GR_BsdIV"))
                            POC.InvReceiptInd = (RequisXVendor.Rows(J).Item("Inv_Recpt"))
                            POC.GoodsReceiptInd = (RequisXVendor.Rows(J).Item("GR"))
                            POC.AckRequired = (RequisXVendor.Rows(J).Item("Ack_Req"))
                            POC.AppendItemText(ItemText, RequisXVendor.Rows(J).Item("ItemText"))


                            'Implementacion del Storage location solicitado por Lilliam Nuñez:
                            'Solamente para las ordenes de Brazil en L7P
                            If _Is4BR AndAlso Box = "L7P" Then
                                POC.Storage_Loc = "0001"
                            End If

                            'Aregado para ingresar el Item Note de la requi en los documentos de referencia de la PO
                            POC.AppendItemText("F06", RequisXVendor.Rows(J).Item("Item Ref"))

                            If Not DBNull.Value.Equals(Vendor.Rows(I).Item("Cur")) AndAlso Vendor.Rows(I).Item("Cur") <> "" Then
                                POC.Currency = Vendor.Rows(I).Item("Cur")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Price")) AndAlso RequisXVendor.Rows(J).Item("Price") <> "" Then
                                POC.ItemNetPrice = (RequisXVendor.Rows(J).Item("Price")).ToString
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("TaxCode")) AndAlso RequisXVendor.Rows(J).Item("TaxCode") <> "" Then
                                POC.ChangeTaxCode(RequisXVendor.Rows(J).Item("TaxCode"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("NCMCode")) AndAlso RequisXVendor.Rows(J).Item("NCMCode") <> "" Then
                                POC.BrasNCMCode = RequisXVendor.Rows(J).Item("NCMCode")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("JurdCode")) AndAlso RequisXVendor.Rows(J).Item("JurdCode") <> "" Then
                                POC.ChangeJurisCode(RequisXVendor.Rows(J).Item("JurdCode"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("MatUsage")) AndAlso RequisXVendor.Rows(J).Item("MatUsage") <> "" Then
                                POC.MatlUsage = (RequisXVendor.Rows(J).Item("MatUsage"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("MatOrigin")) AndAlso RequisXVendor.Rows(J).Item("MatOrigin") <> "" Then
                                POC.MatlOrigin = (RequisXVendor.Rows(J).Item("MatOrigin"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("MatCategory")) AndAlso RequisXVendor.Rows(J).Item("MatCategory") <> "" Then
                                POC.MatlCategory = (RequisXVendor.Rows(J).Item("MatCategory")).ToString.PadLeft(2, "0")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Qty")) AndAlso RequisXVendor.Rows(J).Item("Qty") <> "" Then
                                POC.ItemQuantity = (RequisXVendor.Rows(J).Item("Qty"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Descripcion")) AndAlso RequisXVendor.Rows(J).Item("Descripcion") <> "" Then
                                POC.ItemShortText = RequisXVendor.Rows(J).Item("Descripcion")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Delivery")) Then
                                If IsDate(RequisXVendor.Rows(J).Item("Delivery")) Then
                                    POC.DeliveryDate = CStr(CDate(RequisXVendor.Rows(J).Item("Delivery")))
                                End If
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Item Category")) Then
                                POC.ItemCategory = RequisXVendor.Rows(J).Item("Item Category")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Mat Group")) Then
                                POC.MatGroup = RequisXVendor.Rows(J).Item("Mat Group")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Conf Crtl")) AndAlso RequisXVendor.Rows(J).Item("Conf Crtl") <> "" Then
                                POC.ConfControl = RequisXVendor.Rows(J).Item("Conf Crtl").ToString.ToUpper
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Storage Loc")) AndAlso RequisXVendor.Rows(J).Item("Storage Loc") <> "" Then
                                POC.Storage_Loc = RequisXVendor.Rows(J).Item("Storage Loc").ToString.ToUpper
                            End If
                        Next

                        If Not DBNull.Value.Equals(Vendor.Rows(I).Item("Vendor")) AndAlso Vendor.Rows(I).Item("Vendor") <> "" Then
                            POC.Vendor = Vendor.Rows(I).Item("Vendor")
                        End If

                        If Not DBNull.Value.Equals(Vendor.Rows(I).Item("PG")) AndAlso Vendor.Rows(I).Item("PG") <> "" Then
                            POC.PurchGroup = Vendor.Rows(I).Item("PG").ToString.ToUpper
                        End If

                        If Not DBNull.Value.Equals(Vendor.Rows(I).Item("POrg")) AndAlso Vendor.Rows(I).Item("POrg") <> "" Then
                            POC.PurchOrg = Vendor.Rows(I).Item("POrg").ToString.ToUpper
                        End If

                        

                        POC.CommitChanges()
                        If POC.PONumber Is Nothing Then
                            Dim Err As Integer = 0
                            Dim Errors As String = ""
                            Dim msg As System.String

                            For Each msg In POC.Results
                                Errors = Errors & msg & Chr(13)
                            Next

                            MsgBox("Process failed! PO not created due to faulty data" & Chr(13) & Chr(13) & "Detail: " & Chr(13) & Errors, MsgBoxStyle.Critical)
                        Else
                            cn.ExecuteInServer("Update tmpCrearPO Set Created = 1, New_PO = '" & POC.PONumber & "' Where Created = 0 And Usuario = '" & gsUsuarioPC & "' And SAPBox = '" & Box & "' And Vendor = '" & Vendor.Rows(I).Item("Vendor") & "' And Cur = '" & Vendor.Rows(I).Item("Cur") & "' And PG = '" & Vendor.Rows(I).Item("PG") & "'")
                            Dim cont As Integer = 0

                            For cont = 0 To RequisXVendor.Rows.Count - 1
                                cn.ExecuteInServer("Delete From tmpOpenRequis Where [Req Number] = '" & RequisXVendor.Rows(cont).Item("Requisition") & "' And [Item Number] = '" & RequisXVendor.Rows(cont).Item("Item") & "' And [User] = '" & gsUsuarioPC & "'")
                            Next
                        End If
                    End If
                Next
            Else
                MsgBox("No creation pending requisition were found for: " & Box, MsgBoxStyle.Information)
            End If
        Else
            '********************************************************************************************
            '********************************************************************************************
            '            Codigo para Creación de una PO por requisicion:
            '********************************************************************************************
            '********************************************************************************************
            Vendor = cn.RunSentence("Select Distinct Requisition,Vendor,PG,Cur,SAPBox,POrg From TmpCrearPO Where Created = 0 And Usuario = '" & gsUsuarioPC & "' And SAPBox = '" & Box & "'").Tables(0)

            If Vendor.Rows.Count > 0 Then
                For I = 0 To Vendor.Rows.Count - 1
                    RequisXVendor = cn.RunSentence("Select * From TmpCrearPO Where Created = 0 And Usuario = '" & gsUsuarioPC & "' And SAPBox = '" & Box & "' And Requisition = '" & Vendor.Rows(I).Item("Requisition") & "' And PG = '" & Vendor.Rows(I).Item("PG") & "'  Order by Requisition, Item").Tables(0)
                    If RequisXVendor.Rows.Count > 0 Then
                        POC.CreateNewPO("NB")
                        POC.ItemInterval = 10
                        POC.AppendHeaderText(HeaderText, RequisXVendor.Rows(0).Item("HText"))
                        POC.AppendHeaderText(HeaderNote, RequisXVendor.Rows(0).Item("HNote"))

                        For J = 0 To RequisXVendor.Rows.Count - 1

                            '*********************************************
                            'Creación de las líneas de la PO Item por Item
                            '*********************************************

                            POC.AdoptReqItem(CStr(Int(RequisXVendor.Rows(J).Item("Requisition"))), CStr(RequisXVendor.Rows(J).Item("Item")))
                            POC.GRBasedIV = (RequisXVendor.Rows(J).Item("GR_BsdIV"))
                            POC.InvReceiptInd = (RequisXVendor.Rows(J).Item("Inv_Recpt"))
                            POC.GoodsReceiptInd = (RequisXVendor.Rows(J).Item("GR"))
                            POC.AckRequired = (RequisXVendor.Rows(J).Item("Ack_Req"))
                            POC.AppendItemText(ItemText, RequisXVendor.Rows(J).Item("ItemText"))

                            'Aregado para ingresar el Item Note de la requi en los documentos de referencia de la PO
                            POC.AppendItemText("F05", RequisXVendor.Rows(J).Item("Item Ref"))

                            'Implementacion del Storage location solicitado por Lilliam Nuñez:
                            'Solamente para las ordenes de Brazil
                            If _Is4BR AndAlso Box = "L7P" Then
                                POC.Storage_Loc = "0001"
                            End If

                            If Not DBNull.Value.Equals(Vendor.Rows(I).Item("Cur")) AndAlso Vendor.Rows(I).Item("Cur") <> "" Then
                                POC.Currency = Vendor.Rows(I).Item("Cur")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Price")) AndAlso RequisXVendor.Rows(J).Item("Price") <> "" Then
                                POC.ItemNetPrice = (RequisXVendor.Rows(J).Item("Price")).ToString
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("TaxCode")) AndAlso RequisXVendor.Rows(J).Item("TaxCode") <> "" Then
                                POC.ChangeTaxCode(RequisXVendor.Rows(J).Item("TaxCode"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("NCMCode")) AndAlso RequisXVendor.Rows(J).Item("NCMCode") <> "" Then
                                POC.BrasNCMCode = Trim(RequisXVendor.Rows(J).Item("NCMCode"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("JurdCode")) AndAlso RequisXVendor.Rows(J).Item("JurdCode") <> "" Then
                                POC.ChangeJurisCode(RequisXVendor.Rows(J).Item("JurdCode"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("MatUsage")) AndAlso RequisXVendor.Rows(J).Item("MatUsage") <> "" Then
                                POC.MatlUsage = (RequisXVendor.Rows(J).Item("MatUsage"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("MatOrigin")) AndAlso RequisXVendor.Rows(J).Item("MatOrigin") <> "" Then
                                POC.MatlOrigin = (RequisXVendor.Rows(J).Item("MatOrigin"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("MatCategory")) AndAlso RequisXVendor.Rows(J).Item("MatCategory") <> "" Then
                                POC.MatlCategory = (RequisXVendor.Rows(J).Item("MatCategory")).ToString.PadLeft(2, "0")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Qty")) AndAlso RequisXVendor.Rows(J).Item("Qty") <> "" Then
                                POC.ItemQuantity = (RequisXVendor.Rows(J).Item("Qty"))
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Descripcion")) AndAlso RequisXVendor.Rows(J).Item("Descripcion") <> "" Then
                                POC.ItemShortText = RequisXVendor.Rows(J).Item("Descripcion")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Delivery")) Then
                                If IsDate(RequisXVendor.Rows(J).Item("Delivery")) Then
                                    POC.DeliveryDate = CStr(CDate(RequisXVendor.Rows(J).Item("Delivery")))
                                End If
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Item Category")) Then
                                POC.ItemCategory = RequisXVendor.Rows(J).Item("Item Category")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Mat Group")) Then
                                POC.MatGroup = RequisXVendor.Rows(J).Item("Mat Group")
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Conf Crtl")) AndAlso RequisXVendor.Rows(J).Item("Conf Crtl") <> "" Then
                                POC.ConfControl = RequisXVendor.Rows(J).Item("Conf Crtl").ToString.ToUpper
                            End If

                            If Not DBNull.Value.Equals(RequisXVendor.Rows(J).Item("Storage Loc")) AndAlso RequisXVendor.Rows(J).Item("Storage Loc") <> "" Then
                                POC.Storage_Loc = RequisXVendor.Rows(J).Item("Storage Loc").ToString.ToUpper
                            End If
                        Next

                        If Not DBNull.Value.Equals(Vendor.Rows(I).Item("Vendor")) AndAlso Vendor.Rows(I).Item("Vendor") <> "" Then
                            POC.Vendor = Vendor.Rows(I).Item("Vendor")
                        End If

                        If Not DBNull.Value.Equals(Vendor.Rows(I).Item("PG")) AndAlso Vendor.Rows(I).Item("PG") <> "" Then
                            POC.PurchGroup = Vendor.Rows(I).Item("PG").ToString.ToUpper
                        End If

                        If Not DBNull.Value.Equals(Vendor.Rows(I).Item("POrg")) AndAlso Vendor.Rows(I).Item("POrg") <> "" Then
                            POC.PurchOrg = Vendor.Rows(I).Item("POrg").ToString.ToUpper
                        End If

                        POC.CommitChanges()
                        If POC.PONumber Is Nothing Then
                            Dim Err As Integer = 0
                            Dim Errors As String = ""
                            Dim msg As System.String

                            For Each msg In POC.Results
                                Errors = Errors & msg & Chr(13)
                            Next

                            MsgBox("Process failed! PO not created due to faulty data" & Chr(13) & Chr(13) & "Requisition: " & CStr(Int(RequisXVendor.Rows(I).Item("Requisition"))) & Chr(13) & Chr(13) & "Detail: " & Chr(13) & Errors, MsgBoxStyle.Exclamation)

                        Else
                            cn.ExecuteInServer("Update tmpCrearPO Set Created = 1, New_PO = '" & POC.PONumber & "' Where Created = 0 And Usuario = '" & gsUsuarioPC & "' And SAPBox = '" & Box & "' And Requisition = '" & Vendor.Rows(I).Item("Requisition") & "' And Cur = '" & Vendor.Rows(I).Item("Cur") & "' And PG = '" & Vendor.Rows(I).Item("PG") & "'")
                            Dim cont As Integer = 0

                            For cont = 0 To RequisXVendor.Rows.Count - 1
                                cn.ExecuteInServer("Delete From tmpOpenRequis Where [Req Number] = '" & RequisXVendor.Rows(cont).Item("Requisition") & "' And [Item Number] = '" & RequisXVendor.Rows(cont).Item("Item") & "' And [User] = '" & gsUsuarioPC & "'")
                            Next
                            'MsgBox("Success! Req turned into PO:" & POC.PONumber & vbCr)
                        End If
                    End If
                Next
            Else
                MsgBox("No creation pending requisition were found for: " & Box, MsgBoxStyle.Information)
            End If
        End If

        'POC.EndSession()
        POC = Nothing
    End Sub
    Private Sub cmdDowload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDowload.Click
        Dim Cajas As New DataTable
        Dim drRow As DataGridViewRow
        Dim AllowCreate As Boolean = True ' Variable para controlar que los campos mandatorios sean completados(Proyecto GR/IR)

        'Variables para la evaluacion de texto: Armando Gutierrez -> 08/13/2010
        Dim AllowCreation As Boolean = True
        Dim TextToEvaluate As String = ""
        Me.dtgRequisiciones.EndEdit()

        '*****************************************************************************************************************
        '*****************************************************************************************************************
        'Verificación para colocación de Tax Code para los vendors de Brazil: 03/09/2011
        For Each drRow In dtgRequisiciones.Rows
            Select Case drRow.Cells("vendor").Value
                Case "30000116"
                    If drRow.Cells("TaxCode").Value <> "5B" Then
                        MsgBox("The vendor: " & drRow.Cells("Vendor").Value & ", must use only tax code 5B.  ", MsgBoxStyle.Exclamation, "Warning: Wrong Tax Code!!!")
                        Exit Sub
                    End If

                Case "15119586", "15187788", "15189383", "15190023", "15197168", "15209977", "15224489", "15237569", "15249550", "15255509", "15261467", "15277142", "20600990"
                    If Not PGUser Then
                        MsgBox("To create Infosys PO's, please contact your P&G T2", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
            End Select



            'Validación de los campos mandatorios:
            If _Is4BR AndAlso _IsGoods Then
                If drRow.Cells("NCMCode").Value = "" Or drRow.Cells("MatUsage").Value = "" Or drRow.Cells("MatOrigin").Value = "" Or drRow.Cells("MatCategory").Value = "" Then
                    'If drRow.Cells("NCMCode").Value = "" Or drRow.Cells("MatUsage").Value = "" Or drRow.Cells("MatOrigin").Value = "" Or drRow.Cells("JurdCode").Value = "" Or drRow.Cells("MatCategory").Value = "" Then
                    MsgBox("Some mandatory field is blank please fill in before start creation process againg." & Chr(13) & Chr(13) & "Purchase order not created.", MsgBoxStyle.Exclamation, "Mandatory field in blank.")
                    Exit Sub
                End If
            End If
        Next
        '*****************************************************************************************************************
        '*****************************************************************************************************************

        For Each drRow In dtgRequisiciones.Rows
            TextToEvaluate = ""
            TextToEvaluate = drRow.Cells("Descripcion").Value.ToString

            '************************************************************************************************************
            '************************************************************************************************************
            '
            '    Evaluacion del Texto para evitar la creación
            If (TextToEvaluate.ToUpper.IndexOf("OTHER") >= 0) Then
                AllowCreation = False
            End If

            If (TextToEvaluate.ToUpper.IndexOf("OTRO") >= 0) Then
                AllowCreation = False
            End If

            If (TextToEvaluate.ToUpper.IndexOf("OUTRO") >= 0) Then
                AllowCreation = False
            End If

            If (TextToEvaluate.ToUpper.IndexOf("VARIOS") >= 0) Then
                AllowCreation = False
            End If

            If Not AllowCreation Then
                MsgBox("Please change the item descrption where description contain:   " & Chr(13) & Chr(13) & "                -- Other, Otro, Varios or Outros --", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            '
            '************************************************************************************************************
            '************************************************************************************************************

            cn.ExecuteInServer("Update TmpCrearPO Set " & _
                                                        "Descripcion = '" & drRow.Cells("Descripcion").Value & "'," & _
                                                        "Delivery = '" & drRow.Cells("Delivery").Value & "'," & _
                                                        "Qty = '" & drRow.Cells("Qty").Value & "'," & _
                                                        "Price = '" & drRow.Cells("Price").Value & "'," & _
                                                        "Vendor = '" & drRow.Cells("Vendor").Value & "'," & _
                                                        "Cur = '" & drRow.Cells("Cur").Value.ToString.ToUpper & "'," & _
                                                        "POrg = '" & (drRow.Cells("POrg").Value).ToString.ToUpper & "'," & _
                                                        "PG = '" & (drRow.Cells("PG").Value).ToString.ToUpper & "'," & _
                                                        "NCMCode = '" & drRow.Cells("NCMCode").Value.ToString.ToUpper & "'," & _
                                                        "JurdCode = '" & drRow.Cells("JurdCode").Value.ToString.ToUpper & "'," & _
                                                        "MatUsage = '" & drRow.Cells("MatUsage").Value.ToString.ToUpper & "'," & _
                                                        "MatOrigin = '" & drRow.Cells("MatOrigin").Value.ToString.ToUpper & "'," & _
                                                        "MatCategory = '" & drRow.Cells("MatCategory").Value.ToString.ToUpper & "'," & _
                                                        "HText = '" & drRow.Cells("HText").Value & "'," & _
                                                        "HNote = '" & drRow.Cells("HNote").Value & "'," & _
                                                        "ItemText = '" & drRow.Cells("ItemText").Value & "'," & _
                                                        "TaxCode = '" & drRow.Cells("TaxCode").Value.ToString.ToUpper & "'," & _
                                                        "GR_BsdIV = " & IIf(drRow.Cells("GR_BsdIV").Value, 1, 0) & "," & _
                                                        "Inv_Recpt = " & IIf(drRow.Cells("Inv_Recpt").Value, 1, 0) & "," & _
                                                        "GR = " & IIf(drRow.Cells("GR").Value, 1, 0) & "," & _
                                                        "Ack_Req = " & IIf(drRow.Cells("Ack_Req").Value, 1, 0) & "," & _
                                                        "[Item Ref] = '" & drRow.Cells("Item Ref").Value & "'," & _
                                                        "[Item Category] = '" & drRow.Cells("Item Category").Value & "'," & _
                                                        "[Mat Group] = '" & drRow.Cells("Mat Group").Value & "'," & _
                                                        "[Conf Crtl] = '" & drRow.Cells("Conf Crtl").Value & "'," & _
                                                        "[Storage Loc] = '" & drRow.Cells("Storage Loc").Value & "'" & _
                                                        " Where Requisition = '" & drRow.Cells("Requisition").Value & "' And Item = '" & drRow.Cells("Item").Value & "'")
        Next

        Cajas = cn.RunSentence("Select Distinct SAPBox From tmpCrearPO Where Usuario = '" & gsUsuarioPC & "'").Tables(0)
        If Cajas.Rows.Count > 0 Then
            For Each r As DataRow In Cajas.Rows
                Create_POs(r("SAPBox"))

                If _Is4BR And _IsGoods Then
                    Dim CD As SAPCOM.ConnectionData
                    Dim SC As New SAPCOM.SAPConnector
                    CD = SC.GetConnectionData(r("SAPBox"), gsUsuarioPC, "LAT")
                    'CD = SC.GetConnectionData("L7P", gsUsuarioPC, "LAT")
                    Dim SCn = SC.GetSAPConnection(CD)

                    'Este proceso es porque el BAPI no llama a la funcion BRF+ de SAP
                    Dim FixTax As DataTable
                    FixTax = cn.RunSentence("Select Distinct New_PO From TmpCrearPO Where (Created = 1) And (Usuario = '" & gsUsuarioPC & "') And SAPBox = '" & r("SAPBox") & "'").Tables(0)

                    'FixTax = cn.RunSentence("Select Distinct New_PO From TmpCrearPO Where (Created = 1) And (Usuario = '" & gsUsuarioPC & "') And SAPBox = 'L7P'").Tables(0)
                    If FixTax.Rows.Count > 0 Then
                        Dim EKPO As New SAPCOM.EKPO_Report(SCn)
                        If EKPO.IsReady Then
                            For Each Row As DataRow In FixTax.Rows
                                EKPO.IncludeDocument(Row("New_PO"))
                            Next
                            EKPO.Execute()
                            If EKPO.Success Then
                                Dim SAP As New SAPConection.c_SAP(r("SAPBox"))
                                'Dim SAP As New SAPConection.c_SAP("L7P")
                                SAP.UserName = gsUsuarioPC
                                SAP.Password = CD.Password
                                SAP.OpenConnection(SAPConfig)
                                Dim BRF As New SAPConection.BRF_Fixing(SAP.GUI)
                                If SAP.Conected Then
                                    For Each RW As DataRow In EKPO.Data.Rows
                                        BRF.IncludePO(New SAPConection.BRF_PO(RW("Doc Number"), RW("Item Number")))
                                    Next

                                    BRF.Execute()
                                    SAP.CloseConnection()
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Else
            MsgBox("No data could be selected.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Cajas = cn.RunSentence("Select Distinct SAPBox From tmpCrearPO Where Usuario = '" & gsUsuarioPC & "'").Tables(0)

        MsgBox("Process finished.", MsgBoxStyle.Information)

        '****************************************************************
        '****************************************************************
        '   Guardo un histórico con la información de las Ordenes Creadas
        '****************************************************************
        '****************************************************************
        Dim Historico As DataTable
        Historico = cn.RunSentence("Select * From TmpCrearPO Where Usuario = '" & gsUsuarioPC & "' And Created = 1").Tables(0)

        If Historico.Rows.Count > 0 Then
            cn.AppendTableToSqlServer("HistoricoCreacion", Historico)
        End If
        LoadInfo()
    End Sub

    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub
    Private Sub cmdAgrupar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAgrupar.Click
        Me.cmdAgrupar.Checked = Not Me.cmdAgrupar.Checked
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim i As Integer = 0

        For i = 0 To Me.dtgRequisiciones.Rows.Count - 1
            Me.dtgRequisiciones.Rows(i).Cells("MatUsage").Value = "2"
            Me.dtgRequisiciones.Rows(i).Cells("MatOrigin").Value = "0"
            Me.dtgRequisiciones.Rows(i).Cells("MatCategory").Value = "0"
        Next
    End Sub
    Private Sub ToolStripButton4_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.ButtonClick
        cn.ExportDataTableToXL(Requi)
    End Sub
    Private Sub mnuXPDtaTable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuXPDtaTable.Click
        cn.ExportDataTableToXL(Requi)
    End Sub
    Private Sub mnuXPDtaGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuXPDtaGrid.Click
        cn.ExportDataGridToXL(Me.dtgRequisiciones)
    End Sub
    Private Sub cmdSetTaxCode_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetTaxCode.ButtonClick
        'Method to select the correct tax code
        Dim cn As New OAConnection.Connection
        Dim Table As DataTable = cn.RunSentence("Select IdVendor,Tax_Code From TaxCodeAssigment_Catalogs").Tables(0)
        Dim row As System.Windows.Forms.DataGridViewRow
        Dim TRow As DataRow

        For Each row In Me.dtgRequisiciones.Rows
            For Each TRow In Table.Rows
                If row.Cells("Vendor").Value <> "" Then
                    If TRow("IdVendor") = CType(row.Cells("Vendor").Value, Double) Then
                        row.Cells("TaxCode").Value = TRow("Tax_Code").ToString.Trim
                    End If

                    'Rio Negro Tax seletor:

                    If row.Cells("Descripcion").Value.ToString.ToUpper.Trim.IndexOf("ELECTROBRAS – RIO NEGRO") >= 0 Then
                        row.Cells("TaxCode").Value = "5A"
                    End If

                End If
            Next
        Next
    End Sub
    Private Sub cboHT_HN_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboHT_HN.SelectedIndexChanged
        If cboHT_HN.SelectedValue.ToString <> "System.Data.DataRowView" Then
            Dim HText As String = ""
            Dim HNote As String = ""
            Dim Textos As New DataTable

            Textos = cn.RunSentence("Select HeaderText, HeaderNote From HeaderText_Note Where IdComentarios = " & cboHT_HN.SelectedValue.ToString).Tables(0)
            If Textos.Rows.Count > 0 Then
                HText = Textos.Rows(0).Item("HeaderText")
                HNote = Textos.Rows(0).Item("HeaderNote")

                If MsgBox("Do you want to change the header text and header note", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    cn.ExecuteInServer("Update TmpCrearPO Set Htext = '" & HText & "', HNote = '" & HNote & "' Where Usuario = '" & gsUsuarioPC & "'")

                    Dim r As DataGridViewRow
                    For Each r In dtgRequisiciones.Rows
                        r.Cells("HText").Value = HText
                        r.Cells("HNote").Value = HNote
                    Next
                End If
            End If
        End If


    End Sub
    Private Sub cmdGetInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetInfo.Click
        'Dim c As DataGridViewColumn
        'For Each c In Me.dtgRequisiciones.Columns
        '    MsgBox(c.Name)
        'Next

        If MsgBox("Do you really whant to replace the current Item Text with PR Item information?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Replace Item Text?") = MsgBoxResult.Yes Then
            Dim row As DataGridViewRow

            For Each row In Me.dtgRequisiciones.Rows
                Dim r As New SAPCOM.PRInfo(row.Cells("SAPBox").Value, gsUsuarioPC, AppId)
                r.ReqNumber = row.Cells("Requisition").Value
                Dim s As String = r.ItemText(row.Cells("Item").Value, "B02")
                row.Cells("Item Ref").Value = Replace(s, "'", "")
            Next
        End If
    End Sub
    Private Sub dtgRequisiciones_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dtgRequisiciones.UserDeletingRow
        MsgBox(e.Row.Cells(0).Value)
    End Sub
    Private Function Check_Almacenaje() As Boolean
        Dim row As DataGridViewRow
        Dim SAPBox As String
        Dim RiseWarning As Boolean = False

        For Each row In dtgRequisiciones.Rows
            SAPBox = row.Cells("SAPBox").Value
            'If ((SAPBox = "L7P") AndAlso (row.Cells("Plant").Value = "2921")) Or ((SAPBox = "L6P") AndAlso (row.Cells("Plant").Value = "1867")) Then
            If (((SAPBox = "L7P") Or (SAPBox = "L6P")) AndAlso IsNumeric(row.Cells("Vendor").Value)) Then
                Select Case row.Cells("Vendor").Value
                    Case "15033348", "15219645", "15219715", "30001095", "30001687"
                        Dim TextToEvaluate As String = LCase(row.Cells("Short Text").Value)

                        If (TextToEvaluate.ToLower.IndexOf("armazenagem") >= 0) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.LightCoral
                            RiseWarning = True
                        End If
                        If (TextToEvaluate.ToLower.IndexOf("armazenamento") >= 0) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.LightCoral
                            RiseWarning = True
                        End If
                        If (TextToEvaluate.ToLower.IndexOf("locação") >= 0) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.LightCoral
                            RiseWarning = True
                        End If
                        If (TextToEvaluate.ToLower.IndexOf("aluguel") >= 0) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.LightCoral
                            RiseWarning = True
                        End If
                End Select
            End If
        Next

        If RiseWarning Then
            MsgBox("Highlighted rows should use tax code 5B", MsgBoxStyle.Exclamation, "Plase check Tax Code")
        End If

        Return RiseWarning

    End Function
    Private Function Check_IROnly() As Boolean
        Dim row As DataGridViewRow
        Dim IRVendors As DataTable
        Dim RiseWarning As Boolean = False
        IRVendors = cn.RunSentence("Select * From IR_Only_Vendor").Tables(0)

        For Each row In dtgRequisiciones.Rows
            If (IsNumeric(row.Cells("Vendor").Value)) Then
                Try
                    Dim IRV = (From C In IRVendors.AsEnumerable() _
                                              Where C.Item("Vendor") = row.Cells("Vendor").Value _
                                              Select C.Item("Check Text")).First

                    If IRV Then
                        'If Vendor can be IR-Only or GR/IR Check text
                        Dim IRText As Boolean = False

                        If row.Cells("Descripcion").Value.ToString.ToLower.IndexOf("blackberry") <> -1 Then
                            IRText = True
                        End If
                        If row.Cells("Descripcion").Value.ToString.ToLower.IndexOf("ipad") <> -1 Then
                            IRText = True
                        End If
                        If row.Cells("Descripcion").Value.ToString.ToLower.IndexOf("icard") <> -1 Then
                            IRText = True
                        End If
                        If row.Cells("Descripcion").Value.ToString.ToLower.IndexOf("iphone") <> -1 Then
                            IRText = True
                        End If
                        If row.Cells("Descripcion").Value.ToString.ToLower.IndexOf("mobile") <> -1 Then
                            IRText = True
                        End If
                       
                        If IRText Then
                            row.Cells("Inv_Recpt").Value = True
                            row.Cells("GR").Value = False
                            row.Cells("GR_BsdIV").Value = False
                            RiseWarning = True
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(233, 253, 236)
                        End If

                    Else
                        'If Vendor is always IR-Only
                        row.Cells("Inv_Recpt").Value = True
                        row.Cells("GR").Value = False
                        row.Cells("GR_BsdIV").Value = False
                        RiseWarning = True
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(233, 253, 236)
                    End If

                Catch ex As Exception
                    'Do nothing, if an error occurs is because the vendor is not a IR_Only Vendor
                End Try

            End If
        Next

        If RiseWarning Then
            MsgBox("IR-Only Vendors were found.", MsgBoxStyle.Exclamation, "IR-Only Vendors were found")
        End If

        Return RiseWarning
    End Function
    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckIRVendor.Click
        Check_IROnly()
    End Sub
End Class