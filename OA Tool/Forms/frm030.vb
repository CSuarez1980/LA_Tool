Imports SAPCOM.RepairsLevels
Imports System.Xml.Linq
Imports SAPCOM.SAPTextIDs
Imports System.Windows.Forms


Public Class frm030
    Dim cn As New OAConnection.Connection
    Dim Comentarios As New DataTable
    Dim Waitting As Boolean = False
    'Comentada para utilizar la variable global
    'Dim POFilter As Integer = 0 ' Variable para realizar un filtro para PO importados / nacionales
    Dim POFilterString As String = ""
    Dim POs As New DataTable
    Dim ShowPReq As Boolean = False

    Private Sub frm030_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        GuardarConfiguracion()
    End Sub

    Private Sub frm030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
        cn.LoadCombo(Me.cboIdioma, "Select IDIdioma,Descripcion From Idioma", "IDIdioma", "Descripcion")
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    'Private Sub cmdDowload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDowload.Click
    Private Sub Download()
        Me.tlbHerramientas.Enabled = False
        Me.pbProgress.Value = 5
       
        Dim i As Integer
        Dim Rep As New SAPCOM.OpenOrders_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
        Dim Rep2 As New SAPCOM.OpenGR105_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId) 'Este es para el reporte con GR103 de Imports

        Dim Plantas As New DataTable
        Dim Vendors As New DataTable
        Dim PGrp As New DataTable
        Dim POrg As New DataTable
        'Dim POs As New DataTable
        Dim MatGrp As New DataTable

        Me.pbProgress.Value = 10
       
        Me.dtgRequisiciones.Columns.Clear()

        '**********************************
        'Bloqueo de combos, esto es para que
        'al momento de guardar los comentarios
        'no coloquen otra caja y la PO no
        'pierda el comentario:
        Me.cboSAPBox.Enabled = False
        Me.cboVariantes.Enabled = False
        '**********************************

        If Not cmdAgregar.Checked Then
            cn.ExecuteInServer("Delete From tmpOpenOrders Where Usuario = '" & gsUsuarioPC & "'")
            cn.ExecuteInServer("Delete From tmpSupPortalConfimation Where TNumber = '" & gsUsuarioPC & "'")
        End If


        Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Me.pbProgress.Value = 15

        Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Me.pbProgress.Value = 20

        PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Me.pbProgress.Value = 25

        POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Me.pbProgress.Value = 30

        MatGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Me.pbProgress.Value = 35

        If Plantas.Rows.Count > 0 Then

            For i = 0 To Plantas.Rows.Count - 1
                If DBNull.Value.Equals(Plantas.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePlant("")
                    Rep.IncludePlant(Plantas.Rows(i).Item("Valor"))

                    'GR105
                    Rep2.IncludePlant("")
                    Rep2.IncludePlant(Plantas.Rows(i).Item("Valor"))
                Else
                    If Plantas.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePlant("")
                        Rep.IncludePlant(Plantas.Rows(i).Item("Valor"))

                        'GR105
                        Rep2.IncludePlant("")
                        Rep2.IncludePlant(Plantas.Rows(i).Item("Valor"))
                    Else

                        Rep.ExcludePlant(Plantas.Rows(i).Item("Valor"))

                        'GR105
                        Rep2.ExcludePlant(Plantas.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        Me.pbProgress.Value = 40

        If PGrp.Rows.Count > 0 Then
            For i = 0 To PGrp.Rows.Count - 1
                If DBNull.Value.Equals(PGrp.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePurchGroup("")
                    Rep.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))

                    'GR105
                    Rep2.IncludePurchGroup("")
                    Rep2.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
                Else
                    If PGrp.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePurchGroup("")
                        Rep.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))

                        'GR105
                        Rep2.IncludePurchGroup("")
                        Rep2.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludePurchGroup(PGrp.Rows(i).Item("Valor"))

                        'GR105
                        Rep2.ExcludePurchGroup(PGrp.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        Me.pbProgress.Value = 45

        If POrg.Rows.Count > 0 Then
            For i = 0 To POrg.Rows.Count - 1
                If DBNull.Value.Equals(POrg.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePurchOrg("")
                    Rep.IncludePurchOrg(POrg.Rows(i).Item("Valor"))

                    'GR105
                    Rep2.IncludePurchOrg("")
                    Rep2.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
                Else
                    If POrg.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePurchOrg("")
                        Rep.IncludePurchOrg(POrg.Rows(i).Item("Valor"))

                        'GR105
                        Rep2.IncludePurchOrg("")
                        Rep2.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludePurchOrg(POrg.Rows(i).Item("Valor"))

                        'GR105
                        Rep2.ExcludePurchOrg(POrg.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        Me.pbProgress.Value = 50

        If Vendors.Rows.Count > 0 Then
            For i = 0 To Vendors.Rows.Count - 1
                If DBNull.Value.Equals(Vendors.Rows(i).Item("Prefijo")) Then
                    Rep.IncludeVendor("")
                    Rep.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))

                    'GR105
                    Rep2.IncludeVendor("")
                    Rep2.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                Else
                    If Vendors.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludeVendor("")
                        Rep.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))

                        'GR105
                        Rep2.IncludeVendor("")
                        Rep2.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                    Else
                        Rep.ExcludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))

                        'GR105
                        Rep2.ExcludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                    End If
                End If
            Next
        End If

        Me.pbProgress.Value = 55

        If MatGrp.Rows.Count > 0 Then
            For i = 0 To MatGrp.Rows.Count - 1
                If DBNull.Value.Equals(MatGrp.Rows(i).Item("Prefijo")) Then
                    Rep.IncludeMatGroup("")
                    Rep.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))

                    'GR105
                    Rep2.IncludeMatGroup("")
                    Rep2.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                Else
                    If MatGrp.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludeMatGroup("")
                        Rep.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))

                        'GR105
                        Rep2.IncludeMatGroup("")
                        Rep2.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludeMatGroup(MatGrp.Rows(i).Item("Valor"))

                        'GR105
                        Rep2.ExcludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        Me.pbProgress.Value = 60

        Rep.RepairsLevel = IncludeRepairs ' Para que incluya las reparaciones
        Rep2.RepairsLevel = IncludeRepairs ' Para que incluya las reparaciones con GR105


        Rep.Include_GR_IR = True
        Rep2.Include_GR_IR = True


        Rep.IncludeDelivDates = True 'Para que incluya los delivery dates
        Rep2.IncludeDelivDates = True

        Rep.Include_YO_Ref = True
        Rep2.Include_YO_Ref = True

        Rep.Execute()

        If cmdShowGR103.Checked Then
            Rep2.Execute()
        End If

        Me.pbProgress.Value = 90

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                POs = Rep.Data
                '*****************************************************
                '  13 de Enero 2010:
                '
                '  Este código fue agregado para evitar que en G4P 
                '  se presentaran problemas con columna adicionales
                '  exclusivas de en esta caja
                '*****************************************************

                If POs.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0219")
                End If

                If POs.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
                    POs.Columns.Remove("EKPO-ZWERT")
                End If

                If POs.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0218")
                End If

                If POs.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0220")
                End If

                If POs.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
                    POs.Columns.Remove("EKKO-MEMORYTYPE")
                End If


                'If POs.Columns.IndexOf("YReference") <> -1 Then
                '    POs.Columns.Remove("YReference")
                'End If

                'If POs.Columns.IndexOf("OReference") <> -1 Then
                '    POs.Columns.Remove("OReference")
                'End If



                '*****************************************************
                '*****************************************************

                '*****************************************************
                '*****************************************************
                '     15 Marzo 2010
                '   Este código es para eliminar las PO's que ya tienen
                '   GR y IR
                'Dim x As Integer = 0
                'For x = 0 To POs.Rows.Count - 1
                '    If (POs.Rows(x).Item("GR Qty") > 0 And (POs.Rows(x).Item("GR Qty") = POs.Rows(x).Item("IR Qty"))) Then
                '        POs.Rows(x).Delete()
                '    End If
                'Next

                'Elimino las columnas para evitar un error al momento de cargar la info
                'If POs.Columns.IndexOf("GR Qty") <> -1 Then
                '    POs.Columns.Remove("GR Qty")
                'End If

                'If POs.Columns.IndexOf("IR Qty") <> -1 Then
                '    POs.Columns.Remove("IR Qty")
                'End If
                '' '' '' '' ''*****************************************************
                '' '' '' '' ''*****************************************************

                Dim TN As New DataColumn
                Dim SB As New DataColumn

                'Columna del Usuatio que descarga el reporte
                TN.ColumnName = "Usuario"
                TN.Caption = "Usuario"
                TN.DefaultValue = gsUsuarioPC

                'Columna de la caja
                SB.DefaultValue = Me.cboSAPBox.SelectedValue.ToString
                SB.ColumnName = "SAPBox"
                SB.Caption = "SAPBox"

                POs.Columns.Add(TN)
                POs.Columns.Add(SB)

                cn.AppendTableToSqlServer("tmpOpenOrders", POs)
                'cn.ExecuteInServer("DELETE FROM TMPOpenOrders WHERE (Usuario = '" & gsUsuarioPC & "') And EXISTS( " & _
                '                                                     "SELECT  tmpOpenOrders.[Doc Number], tmpOpenOrders.[Item Number], tmpOpenOrders.Usuario, (CASE WHEN plant.Country = vendorsG11.country THEN 'National' ELSE 'Import' END) AS [Nac / Imp] " & _
                '                                                     "FROM  tmpOpenOrders LEFT OUTER JOIN Plant ON tmpOpenOrders.Plant = Plant.Code LEFT OUTER JOIN VendorsG11 ON tmpOpenOrders. Vendor = VendorsG11. Vendor " & _
                '                                                     "WHERE (tmpOpenOrders.Usuario = '" & gsUsuarioPC & "') AND ((CASE WHEN plant.Country = vendorsG11.country THEN 'National' ELSE 'Import' END) = 'National'))")
                If cmdShowGR103.Checked Then
                    Dim POs2 As New DataTable

                    If Rep2.Success Then
                        If Rep2.ErrMessage = Nothing Then
                            POs2 = Rep2.Data

                            If POs2.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
                                POs2.Columns.Remove("EKKO-WAERS-0219")
                            End If

                            If POs2.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
                                POs2.Columns.Remove("EKPO-ZWERT")
                            End If

                            If POs2.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
                                POs2.Columns.Remove("EKKO-WAERS-0218")
                            End If

                            If POs2.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
                                POs2.Columns.Remove("EKKO-WAERS-0220")
                            End If

                            If POs2.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
                                POs2.Columns.Remove("EKKO-MEMORYTYPE")
                            End If

                            If POs.Columns.IndexOf("YReference") <> -1 Then
                                POs.Columns.Remove("YReference")
                            End If

                            If POs.Columns.IndexOf("OReference") <> -1 Then
                                POs.Columns.Remove("OReference")
                            End If

                            Dim TN2 As New DataColumn
                            Dim SB2 As New DataColumn

                            'Columna del Usuario que descarga el reporte
                            TN2.ColumnName = "Usuario"
                            TN2.Caption = "Usuario"
                            TN2.DefaultValue = gsUsuarioPC

                            'Columna de la caja
                            SB2.DefaultValue = Me.cboSAPBox.SelectedValue.ToString
                            SB2.ColumnName = "SAPBox"
                            SB2.Caption = "SAPBox"

                            POs2.Columns.Add(TN2)
                            POs2.Columns.Add(SB2)

                            Try
                                cn.AppendTableToSqlServer("tmpOpenOrders", POs2)
                            Catch ex As Exception
                                InsertRowByRow(POs2)
                            End Try
                            'cn.ExecuteInServer("DELETE FROM TMPOpenOrders WHERE (Usuario = '" & gsUsuarioPC & "') And EXISTS( " & _
                            '                                   "SELECT  tmpOpenOrders.[Doc Number], tmpOpenOrders.[Item Number], tmpOpenOrders.Usuario, (CASE WHEN plant.Country = vendorsG11.country THEN 'National' ELSE 'Import' END) AS [Nac / Imp] " & _
                            '                                   "FROM  tmpOpenOrders LEFT OUTER JOIN Plant ON tmpOpenOrders.Plant = Plant.Code LEFT OUTER JOIN VendorsG11 ON tmpOpenOrders. Vendor = VendorsG11. Vendor " & _
                            '                                   "WHERE (tmpOpenOrders.Usuario = '" & gsUsuarioPC & "') AND ((CASE WHEN plant.Country = vendorsG11.country THEN 'National' ELSE 'Import' END) = 'National'))")
                        End If
                    End If
                End If


                '**************************************************************************************************
                '**************************************************************************************************
                ' **->  Este procedimiento se agregó en lugar del código para poder ser reutilizado
                ' **->  Esto es para poder agregar varias cajas de SAP en el reporte

                Me.pbProgress.Value = 95

                '**************************************************************************************************
                ' Proceso agregado para la verificación del supplier portal 2010-05-13

                Dim EKPO As New SAPCOM.EKPO_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
                Dim Row As DataRow

                For Each Row In Rep.Documents.Rows
                    EKPO.IncludeDocument(Row.Item("Doc Number"))
                Next

                If cmdShowGR103.Checked Then
                    If Rep2.Success Then
                        If Rep2.ErrMessage = Nothing Then
                            For Each Row In Rep2.Documents.Rows
                                EKPO.IncludeDocument(Row.Item("Doc Number"))
                            Next
                        End If
                    End If
                End If

                EKPO.AddCustomField("LABNR")
                EKPO.Execute()

                If EKPO.Success Then
                    If EKPO.ErrMessage = Nothing Then

                        If EKPO.Data.Columns.IndexOf("Material") <> -1 Then
                            EKPO.Data.Columns.Remove("Material")
                        End If

                        If EKPO.Data.Columns.IndexOf("Plant") <> -1 Then
                            EKPO.Data.Columns.Remove("Plant")
                        End If

                        If EKPO.Data.Columns.IndexOf("Inforecord") <> -1 Then
                            EKPO.Data.Columns.Remove("Inforecord")
                        End If
                        If EKPO.Data.Columns.IndexOf("Quantity") <> -1 Then
                            EKPO.Data.Columns.Remove("Quantity")
                        End If
                        If EKPO.Data.Columns.IndexOf("UOM") <> -1 Then
                            EKPO.Data.Columns.Remove("UOM")
                        End If
                        If EKPO.Data.Columns.IndexOf("Price") <> -1 Then
                            EKPO.Data.Columns.Remove("Price")
                        End If
                        If EKPO.Data.Columns.IndexOf("Tax Code") <> -1 Then
                            EKPO.Data.Columns.Remove("Tax Code")
                        End If
                        If EKPO.Data.Columns.IndexOf("PDT") <> -1 Then
                            EKPO.Data.Columns.Remove("PDT")
                        End If
                        If EKPO.Data.Columns.IndexOf("Mat Group") <> -1 Then
                            EKPO.Data.Columns.Remove("Mat Group")
                        End If
                        If EKPO.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                            EKPO.Data.Columns.Remove("Tracking Fld")
                        End If

                        If EKPO.Data.Columns.IndexOf("Price Unit") <> -1 Then
                            EKPO.Data.Columns.Remove("Price Unit")
                        End If

                        Dim ETN As New DataColumn
                        Dim ESB As New DataColumn

                        'Columna del Usuario que descarga el reporte
                        ETN.ColumnName = "Usuario"
                        ETN.Caption = "Usuario"
                        ETN.DefaultValue = gsUsuarioPC

                        'Columna de la caja
                        ESB.DefaultValue = Me.cboSAPBox.SelectedValue.ToString
                        ESB.ColumnName = "SAPBox"
                        ESB.Caption = "SAPBox"

                        EKPO.Data.Columns.Add(ETN)
                        EKPO.Data.Columns.Add(ESB)

                        cn.AppendTableToSqlServer("tmpSupPortalConfimation", EKPO.Data)
                    Else
                        MsgBox(EKPO.ErrMessage, MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox(EKPO.ErrMessage, MsgBoxStyle.Information)
                End If
                LoadInfo("")
                Me.pbProgress.Value = 90
            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            End If
        Else
            'MsgBox("Error el conectar con SAP.", MsgBoxStyle.Exclamation)
            MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
        End If
        Me.tlbHerramientas.Enabled = True
        Me.pbProgress.Value = 0
    End Sub

    Private Sub InsertRowByRow(ByVal T As DataTable)
        Dim T2 As New DataTable

        T2 = T.Clone

        Try
            For Each X As DataRow In T.Rows
                T2.Clear()
                T2.ImportRow(X)
                cn.AppendTableToSqlServer("tmpOpenOrders", T2)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuario & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")
        End If
    End Sub

    Private Sub dtgRequisiciones_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellContentClick
        Dim curRow As Integer = 0
        Dim curCol As Integer = 0

        curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
        curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

        Select Case Me.dtgRequisiciones.Columns(e.ColumnIndex).HeaderText.ToUpper
            Case "COMMENTS"
                Dim form As New frm027

                Comentarios = cn.RunSentence("Select * From Comentarios_PO Where PurchOrder = " & Me.dtgRequisiciones.Rows(curRow).Cells("Doc Number").Value & " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & " And SAPBox = '" & Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value & "'").Tables(0)
                If Comentarios.Rows.Count > 0 Then
                    form.dtgComentarios.DataSource = Comentarios
                    form.lblDocumento.Text = "Purch Doc"
                    form.txtRequisicion.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Doc Number").Value
                    form.txtReqItem.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value
                    form.txtGica.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Material").Value
                    form.txtMaterial.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Short Text").Value
                    form.txtSAPBox.Text = Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value
                    form.ShowDialog()
                Else
                    MsgBox("Esta órden de compra no tiene comentarios", MsgBoxStyle.Information)
                End If
        End Select
    End Sub

    Private Sub cmdComentarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComentarios.Click
        If dtgRequisiciones.RowCount > 0 Then '<- Verifico que el datagrid contenga información
            ' '' '' ''Dim cn As New OAConnection.Connection
            ' '' '' ''Dim Form As New frm031
            ' '' '' ''Dim curRow As Integer = 0
            ' '' '' ''Dim curCol As Integer = 0

            ' '' '' ''curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
            ' '' '' ''curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

            ' '' '' ''Form.txtRequisicion.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Doc Number").Value
            ' '' '' ''Form.txtReqItem.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value
            ' '' '' ''Form.txtGica.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Material").Value
            ' '' '' ''Form.txtMaterial.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Short Text").Value
            ' '' '' ''Form.txtSAPBox.Text = Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value
            ' '' '' ''Form.txtPlanta.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Plant").Value
            ' '' '' ''Form.txtMatGrp.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Mat Group").Value
            ' '' '' ''Form.txtPGrp.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Purch Grp").Value
            ' '' '' ''Form.txtPOrg.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Purch Org").Value
            ' '' '' ''Form.ShowDialog()

            ' '' '' ''If Form.Estado Then
            ' '' '' ''    VerificarComentariosYRootCauses()
            ' '' '' ''End If

            ' '' '' ''Form.Close()


            '*************************************************************************************************
            '*************************************************************************************************
            ' Test for multi-comments:


            Me.dtgRequisiciones.EndEdit()
            Dim Form As New frm044
            Form.ShowDialog()

            If Form.Estado Then
                Dim cn As New OAConnection.Connection
                Dim drRequisition As System.Windows.Forms.DataGridViewRow
                Dim lsComentario As String = ""
                lsComentario = Replace(Replace(Form.txtComentarios.Text, "'", ""), ",", "")

                For Each drRequisition In Me.dtgRequisiciones.Rows
                    If drRequisition.Cells("CK").Value Then
                        With drRequisition
                            cn.ExecuteInServer("Insert Into Comentarios_PO(PurchOrder,Item,Comentario,Fecha,Usuario,SAPBox,Status,Planta,MatGrp,PGrp,POrg) Values(" & .Cells("Doc Number").Value & "," & .Cells("Item Number").Value & ",'" & lsComentario & "', {fn now()}, '" & gsUsuarioPC & "','" & .Cells("SAPBox").Value & "','" & Form.txtStatus.Text & "','" & .Cells("Plant").Value & "','" & .Cells("Mat Group").Value & "','" & .Cells("Purch Grp").Value & "','" & .Cells("Purch Org").Value & "')")
                        End With
                    End If
                Next

                VerificarComentariosYRootCauses()
            End If

            Form.Close()
            '*************************************************************************************************
            '*************************************************************************************************
        End If
    End Sub

    Private Sub cmdDesbloquear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesbloquear.Click
        Me.cboSAPBox.Enabled = True
        Me.cboVariantes.Enabled = True
        Me.dtgRequisiciones.Columns.Clear()
    End Sub

    Private Sub VerificarComentariosYRootCauses()
        '' ''Dim i, j As Integer
        '' ''Dim Res As Boolean = False

        '' ''Dim dtRCCM As New DataTable ' -> datatable con la informacion de los rootcauses y comments

        '' ''dtRCCM = cn.RunSentence("Select * From [xvstRootCauseAndComments] Where Usuario = '" & gsUsuarioPC & "'").Tables(0)

        '' ''For i = 0 To Me.dtgRequisiciones.RowCount - 1
        '' ''    If dtRCCM.Rows.Count > 0 Then
        '' ''        Dim vstdn As String
        '' ''        Dim grddn As String
        '' ''        Dim vstdi As String
        '' ''        Dim grddi As String
        '' ''        Dim vstbx As String
        '' ''        Dim grdbx As String

        '' ''        For j = 0 To dtRCCM.Rows.Count - 1

        '' ''            vstdn = ""
        '' ''            grddn = ""
        '' ''            vstdi = ""
        '' ''            grddi = ""
        '' ''            vstbx = ""
        '' ''            grdbx = ""

        '' ''            vstdn = dtRCCM.Rows(j).Item("Doc Number")
        '' ''            grddn = Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value
        '' ''            vstdi = dtRCCM.Rows(j).Item("Item Number")
        '' ''            grddi = Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value
        '' ''            vstbx = dtRCCM.Rows(j).Item("SAPBox")
        '' ''            grdbx = Me.dtgRequisiciones.Rows(i).Cells("SAPBox").Value

        '' ''            If (vstdn = grddn) And (vstdi = grddi) And (vstbx = grdbx) Then
        '' ''                If Not DBNull.Value.Equals(dtRCCM.Rows(j).Item("Comentario")) Then
        '' ''                    Me.dtgRequisiciones.Rows(i).Cells("Comments").Value = "≈≈≈"
        '' ''                    Me.dtgRequisiciones.Rows(i).Cells("Comments").ToolTipText = dtRCCM.Rows(j).Item("Comentario")
        '' ''                    Me.dtgRequisiciones.Rows(i).Cells("Status").Value = dtRCCM.Rows(j).Item("Status")
        '' ''                End If

        '' ''                If Not DBNull.Value.Equals(dtRCCM.Rows(j).Item("RootCause")) Then
        '' ''                    Me.dtgRequisiciones.Rows(i).Cells("RootCauses").Value = dtRCCM.Rows(j).Item("RootCause")
        '' ''                End If

        '' ''                '**********************************************************************************************************
        '' ''                'Comentada para utilizar la nueva vista: 2011-06-28
        '' ''                ' Me.dtgRequisiciones.Rows(i).Cells("Aging").Value = (Now.Date - Date.Parse(Me.dtgRequisiciones.Rows(i).Cells("Delivery Date").Value)).Days

        '' ''                '******************************************************************************************
        '' ''                '******************************************************************************************
        '' ''                'Configuración para los colores de la celda para el proyecto de expeditación de Ersa y Hibe

        '' ''                Select Case Me.dtgRequisiciones.Rows(i).Cells("Vendor OTD Average").Value
        '' ''                    Case 90 To 100
        '' ''                        Me.dtgRequisiciones.Rows(i).Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.MediumAquamarine

        '' ''                    Case 80 To 89
        '' ''                        Me.dtgRequisiciones.Rows(i).Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.SandyBrown

        '' ''                    Case Is < 79
        '' ''                        Me.dtgRequisiciones.Rows(i).Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.Tomato
        '' ''                End Select

        '' ''                '******************************************************************************************
        '' ''                '******************************************************************************************

        '' ''                Exit For
        '' ''            End If
        '' ''        Next
        '' ''    End If
        '' ''Next

        Dim row As DataGridViewRow

        For Each row In Me.dtgRequisiciones.Rows
            If Not DBNull.Value.Equals(row.Cells("Comentarios").Value) Then
                row.Cells("Comments").Value = "≈≈≈"
                row.Cells("Comments").ToolTipText = row.Cells("Comentarios").Value
            End If

            If Not DBNull.Value.Equals(row.Cells("RC")) Then
                row.Cells("RootCauses").Value = row.Cells("RC").Value
            End If

            Select Case row.Cells("Vendor OTD Average").Value
                Case 90 To 100
                    row.Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.MediumAquamarine

                Case 80 To 89
                    row.Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.SandyBrown

                Case Is < 79
                    row.Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.Tomato
            End Select
        Next
    End Sub

    ''' <summary>
    ''' Carga la configuración del DataGrid, columnas visibles y el orden
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarConfiguracion()
        Dim Config As New DataTable
        Dim i As Integer = 0
        Config = cn.RunSentence("Select * From Config_Forms Where Usuario = '" & cn.GetUserId & "' And NombreFormulario = '" & Me.Name & "' Order by Orden").Tables(0)

        If Config.Rows.Count > 0 Then
            'Aplico la configuración del DataGrid
            For i = 0 To Config.Rows.Count - 1
                Me.dtgRequisiciones.Columns(Config.Rows(i).Item("Columna")).DisplayIndex = Config.Rows(i).Item("Orden")
                Me.dtgRequisiciones.Columns(Config.Rows(i).Item("Columna")).Visible = Config.Rows(i).Item("Mostrar")
            Next

        Else
            'Si el formulario no tiene configuración le guardo una
            For i = 0 To Me.dtgRequisiciones.ColumnCount - 1
                cn.ExecuteInServer("Insert Into Config_Forms(Usuario,NombreFormulario,Columna,Mostrar,Orden) " & _
                                   "Values('" & cn.GetUserId & "','" & Me.Name & "','" & Me.dtgRequisiciones.Columns(i).HeaderText & "'," & IIf(Me.dtgRequisiciones.Columns(i).Visible, 1, 0) & "," & Me.dtgRequisiciones.Columns(i).DisplayIndex & ")")
            Next
        End If

        LockGrid()
    End Sub

    ''' <summary>
    ''' Guarda la configuración del DataGrid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GuardarConfiguracion()
        Dim i As Integer = 0
        For i = 0 To Me.dtgRequisiciones.Columns.Count - 1
            cn.ExecuteInServer("Update Config_Forms Set Orden = " & Me.dtgRequisiciones.Columns(i).DisplayIndex & " Where Usuario = '" & cn.GetUserId & "' And NombreFormulario = '" & Me.Name & "' And Columna = '" & Me.dtgRequisiciones.Columns(i).HeaderText & "'")
        Next
    End Sub

    Private Sub dtgRequisiciones_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellEndEdit
        Me.dtgRequisiciones.EndEdit()
        Select Case Me.dtgRequisiciones.Columns(e.ColumnIndex).HeaderText.ToUpper
            Case "ROOTCAUSES"
                Dim Requi As New DataTable
                Dim curRow As Integer = 0
                Dim curCol As Integer = 0

                curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
                curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

                'Verifico si la PO ya tiene Root Cauese:
                Requi = cn.RunSentence("Select RootCauses.Descripcion From RootCauses,RootCauses_PO " & _
                                       "Where RootCauses.ID = RootCauses_PO.RootCause " & _
                                       " And PurchOrder = " & Me.dtgRequisiciones.Rows(curRow).Cells("Doc Number").Value & _
                                       " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & _
                                       " And SAPBox = '" & Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value & "'").Tables(0)


                If Requi.Rows.Count = 0 Then
                    If Not Me.dtgRequisiciones.Rows(curRow).Cells("RootCauses").Value = Nothing Then
                        'Si la PO no tiene RootCause le agrego:
                        cn.ExecuteInServer("Insert Into RootCauses_PO Values(" & _
                                            Me.dtgRequisiciones.Rows(curRow).Cells("Doc Number").Value & "," & _
                                            Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & "," & _
                                            "'" & Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value & "'," & _
                                            "{fn now()}," & _
                                            "'" & cn.GetUserId & "'," & _
                                            "'" & Me.dtgRequisiciones.Rows(curRow).Cells("Plant").Value & "'," & _
                                            "'" & Me.dtgRequisiciones.Rows(curRow).Cells("Mat Group").Value & "'," & _
                                            "'" & Me.dtgRequisiciones.Rows(curRow).Cells("Purch Grp").Value & "'," & _
                                            "'" & Me.dtgRequisiciones.Rows(curRow).Cells("Purch Org").Value & "'," & _
                                            Me.dtgRequisiciones.Rows(curRow).Cells("RootCauses").Value & ")")
                    End If
                Else
                    'Si la PO ya Root Cause la actualizo:
                    cn.ExecuteInServer("Update RootCauses_PO set RootCause = " & Me.dtgRequisiciones.Rows(curRow).Cells("RootCauses").Value & _
                                       " Where PurchOrder = " & Me.dtgRequisiciones.Rows(curRow).Cells("Doc Number").Value & _
                                       " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & _
                                       " And SAPBox = '" & Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value & "'")

                End If
        End Select
    End Sub

    Private Sub dtgRequisiciones_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellEnter
        Me.txtRow.Text = Me.dtgRequisiciones.CurrentRow.Index + 1
    End Sub

    Private Sub dtgRequisiciones_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dtgRequisiciones.ColumnHeaderMouseClick
        VerificarComentariosYRootCauses()
    End Sub

    Private Sub btnCampos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCampos.Click
        Dim form As New frm028
        Dim Tabla As New DataTable
        Dim i As Integer = 0

        'Cargo la configuración actual del formulario:
        Tabla = cn.RunSentence("Select ID,Mostrar,Columna From Config_Forms Where NombreFormulario = '" & Me.Name & "' And Usuario = '" & cn.GetUserId & "'").Tables(0)
        If Tabla.Rows.Count > 0 Then
            form.dtgConfiguracion.DataSource = Tabla
            With form.dtgConfiguracion.Columns("ID")
                .ReadOnly = True
                .Width = 30
            End With

            With form.dtgConfiguracion.Columns("Columna")
                .ReadOnly = True
                .Width = 200
            End With

            With form.dtgConfiguracion.Columns("Mostrar")
                .Width = 50
            End With

            form.ShowDialog()

            'Guardo la configuración:
            If form.Guardar Then
                For i = 0 To form.dtgConfiguracion.RowCount - 1
                    cn.ExecuteInServer("Update Config_Forms set Mostrar = " & IIf(form.dtgConfiguracion.Rows(i).Cells("Mostrar").Value, 1, 0) & " Where ID = " & form.dtgConfiguracion.Rows(i).Cells("ID").Value)
                Next
            End If
            CargarConfiguracion()
        Else
            MsgBox("Debe decargar la información antes de configurar el DataGrid", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'If cn.ExportDataGridToXL(Me.dtgRequisiciones) Then
        '    MsgBox("Done!")
        'End If


        cn.ExportDataTableToXL(POs)


        ' '' ''Dim xl As Object 'New Microsoft.Office.Interop.Excel.Application
        ' '' ''Dim i, j As Integer
        ' '' ''Dim SaveFile As New SaveFileDialog
        ' '' ''Dim FileName As String = ""
        ' '' ''Dim ActiveColumns As Integer = 0

        ' '' ''Try
        ' '' ''    SaveFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        ' '' ''    SaveFile.Filter = "Archivos de Excel (*.xls)|*.xls"

        ' '' ''    If (SaveFile.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
        ' '' ''        FileName = SaveFile.FileName
        ' '' ''    Else
        ' '' ''        MsgBox("Acción cancelada por el ususario.")
        ' '' ''        Exit Sub
        ' '' ''    End If

        ' '' ''    xl = CreateObject("Excel.Application")

        ' '' ''    xl.Workbooks.Add()

        ' '' ''    If Len(Dir(FileName)) > 0 Then
        ' '' ''        Kill(FileName)
        ' '' ''    End If

        ' '' ''    Dim Column As DataGridViewColumn
        ' '' ''    i = 0
        ' '' ''    For Each Column In Me.dtgRequisiciones.Columns
        ' '' ''        If Column.Visible Then
        ' '' ''            xl.Cells(1, i + 1).value = Column.Name
        ' '' ''            xl.Cells(1, i + 1).interior.color = RGB(192, 192, 192)
        ' '' ''            i += 1
        ' '' ''        End If
        ' '' ''    Next

        ' '' ''    Dim Row As DataGridViewRow
        ' '' ''    Dim Cell As DataGridViewCell

        ' '' ''    i = 0

        ' '' ''    For Each Row In Me.dtgRequisiciones.Rows
        ' '' ''        j = 0
        ' '' ''        For Each Cell In Row.Cells
        ' '' ''            If Cell.Visible Then
        ' '' ''                If Cell.GetType.ToString.ToUpper = "SYSTEM.DATETIME" Then
        ' '' ''                    xl.Cells(i + 2, j + 1).value = Microsoft.VisualBasic.Format(Cell.Value, "dd/MM/yyyy")
        ' '' ''                Else
        ' '' ''                    If Cell.ToolTipText.Length > 0 Then
        ' '' ''                        xl.Cells(i + 2, j + 1).value = Cell.ToolTipText
        ' '' ''                    Else
        ' '' ''                        xl.Cells(i + 2, j + 1).value = Cell.Value
        ' '' ''                    End If
        ' '' ''                    xl.Cells(i + 2, j + 1).value = Cell.Value
        ' '' ''                End If
        ' '' ''                j += 1
        ' '' ''            End If
        ' '' ''        Next
        ' '' ''        i += 1
        ' '' ''    Next

        ' '' ''    xl.ActiveWorkbook.SaveAs(FileName)
        ' '' ''    xl.ActiveWorkbook.Close()
        ' '' ''    xl.Quit()

        ' '' ''Catch ex As Exception
        ' '' ''    MsgBox("Fail to export datatable to MS Excel" & Chr(13) & Chr(13) & ex.Message, MsgBoxStyle.Critical)


        ' '' ''Finally
        ' '' ''    xl = Nothing
        ' '' ''End Try


    End Sub

    Private Sub cmdReeplazar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Me.cmdAgregar.Checked = Not Me.cmdAgregar.Checked
    End Sub

    'Private Function getExceptions(ByVal pDistribution As Integer) As String
    '    Dim lsFilter As String = ""

    '    If pDistribution <> 0 Then
    '        Dim dtException As DataTable
    '        dtException = cn.RunSentence("Select VendorId From VendorException Where ExceptionFor = " & pDistribution).Tables(0)

    '        If dtException.Rows.Count > 0 Then
    '            For Each r As DataRow In dtException.Rows
    '                If lsFilter.Length > 0 Then
    '                    lsFilter = lsFilter & " Or "
    '                End If

    '                lsFilter = lsFilter & "(Vendor = " & r("VendorId") & ")"
    '            Next

    '            lsFilter = " Or (" & lsFilter & ")"
    '        End If


    '    End If


    '    Return lsFilter
    'End Function

    ''' <summary>
    ''' Procedimiento para Cargar la información del las órdenes de compra del servidor
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

#Region "2012/02/05"
    'Proceso original, daba error de time-out

    'Private Sub LoadInfo(ByVal pFiltro As String)
    '    Dim POFilterString As String = ""

    '    If giDistribution <> 0 Then
    '        'Agrego al filtro los vendors según su exception:
    '        Select Case giDistribution
    '            Case 1
    '                POFilterString = " And (( (CASE WHEN dbo.plant.Country = dbo.vendorsG11.country THEN 'National' ELSE 'Import' END) = 'National')" & cn.getExceptions(giDistribution, "dbo.tmpOpenOrders.") & ")"

    '            Case 2
    '                POFilterString = " And (( (CASE WHEN dbo.plant.Country = dbo.vendorsG11.country THEN 'National' ELSE 'Import' END) = 'Import')" & cn.getExceptions(giDistribution, "dbo.tmpOpenOrders.") & ")"
    '        End Select
    '    End If

    '    If chkFinalInvoice.Checked Then
    '        POFilterString = POFilterString + " And ([Final Invoice] <> 'X')"
    '    End If

    '    Me.dtgRequisiciones.DataSource = ""
    '    Me.dtgRequisiciones.Columns.Clear()

    '    'Columnas eliminadas al linkearse la nueva vista
    '    'POs = cn.RunSentence("Select * From vsttmpOpenOrders Where " & " (Usuario = '" & gsUsuarioPC & "'" & POFilterString & IIf(Filtro.Length > 0, ") And (" & Filtro, "") & ") Order by [Doc Number], [Item Number]").Tables(0)        
    '    'POs.Columns.Add("Status")
    '    'POs.Columns.Add("Aging", System.Type.GetType("System.Decimal"))

    '    ''Nueva Vista:
    '    'POs = cn.RunSentence("Select  [Doc Number], [Item Number], [Mat Group], Material, [Short Text], Vendor, [Vendor Name], [Company Code], [Purch Org], [Purch Grp], Plant, [Doc Date], [Doc Type], " & _
    '    '                     "[Created By], [Tracking Field], Quantity, UOM, Currency, [Del Indicator], [Delivery Comp], [Final Invoice], Requisitioner, Price, [Delivery Date], Repair, M_Type, Usuario, " & _
    '    '                     "SAPBox, Correo, [Vendor OTD Average], [SP Confirmation], [Nac / Imp], [GR Qty], [IR Qty], Aging, RC, Comentarios, Status From vstDB_Open_Orders Where " & " ((Usuario = '" & gsUsuarioPC & "')" & POFilterString & IIf(pFiltro.Length > 0, ") And (" & pFiltro, "") & ") Order by [Doc Number], [Item Number]").Tables(0)




    '    '****************************************************************************************************************************************************
    '    '****************************************************************************************************************************************************
    '    'Este codigo es para evitar el time out del server por la consulta
    '    Dim vst As String = ""
    '    vst = "SELECT TOP 100 PERCENT dbo.tmpOpenOrders.[Doc Number], dbo.tmpOpenOrders.[Item Number], dbo.tmpOpenOrders.[Mat Group], dbo.tmpOpenOrders.Material, " & _
    '                  "dbo.tmpOpenOrders.[Short Text], dbo.tmpOpenOrders.Vendor, dbo.tmpOpenOrders.[Vendor Name], dbo.tmpOpenOrders.[Company Code], " & _
    '                  "dbo.tmpOpenOrders.[Purch Org], dbo.tmpOpenOrders.[Purch Grp], dbo.tmpOpenOrders.Plant, dbo.tmpOpenOrders.[Doc Date], dbo.tmpOpenOrders.[Doc Type], " & _
    '                  "dbo.tmpOpenOrders.[Created By], dbo.tmpOpenOrders.[Tracking Field], dbo.tmpOpenOrders.Quantity, dbo.tmpOpenOrders.UOM, dbo.tmpOpenOrders.Currency, " & _
    '                  "dbo.tmpOpenOrders.[Del Indicator], dbo.tmpOpenOrders.[Delivery Comp], dbo.tmpOpenOrders.[Final Invoice], dbo.tmpOpenOrders.Requisitioner, " & _
    '                  "dbo.tmpOpenOrders.Price, dbo.tmpOpenOrders.[Delivery Date], dbo.tmpOpenOrders.Repair, ISNULL(dbo.ERSA_HIBE.M_Type, N'') AS M_Type, " & _
    '                  "dbo.tmpOpenOrders.Usuario, dbo.tmpOpenOrders.SAPBox, ISNULL(dbo.VendorsG11.VndMail1, N'') AS Correo, ISNULL(dbo.HERSA_HIBE_Expedating.Average, 0) " & _
    '                  "AS [Vendor OTD Average], dbo.tmpSupPortalConfimation.Confirmation AS [SP Confirmation], " & _
    '                  " (CASE WHEN dbo.plant.Country = dbo.vendorsG11.country THEN 'National' ELSE 'Import' END) AS [Nac / Imp], dbo.tmpOpenOrders.[GR Qty], " & _
    '                  "dbo.tmpOpenOrders.[IR Qty], DATEDIFF(day, dbo.tmpOpenOrders.[Delivery Date], { fn NOW() }) AS Aging, dbo.RootCauses_PO.RootCause AS RC," & _
    '                  "(SELECT TOP 1 CAST(Comentario AS nvarchar(3000)) AS Expr1" & _
    '                  " FROM dbo.Comentarios_PO AS B" & _
    '                  " WHERE (PurchOrder = dbo.tmpOpenOrders.[Doc Number]) AND (Item = dbo.tmpOpenOrders.[Item Number]) AND (SAPBox = dbo.tmpOpenOrders.SAPBox)" & _
    '                  " ORDER BY ID DESC) AS Comentarios," & _
    '                  "(SELECT TOP 1 CAST(Status AS nvarchar(250)) AS Expr1" & _
    '                  " FROM dbo.Comentarios_PO AS B" & _
    '                  " WHERE (PurchOrder = dbo.tmpOpenOrders.[Doc Number]) AND (Item = dbo.tmpOpenOrders.[Item Number]) AND (SAPBox = dbo.tmpOpenOrders.SAPBox)" & _
    '                  " ORDER BY ID DESC) AS Status" & _
    '                  " FROM dbo.tmpOpenOrders LEFT OUTER JOIN " & _
    '                  "dbo.RootCauses_PO ON dbo.tmpOpenOrders.[Item Number] = dbo.RootCauses_PO.Item AND dbo.tmpOpenOrders.[Doc Number] = dbo.RootCauses_PO.PurchOrder AND " & _
    '                  "dbo.tmpOpenOrders.SAPBox = dbo.RootCauses_PO.SAPBox LEFT OUTER JOIN " & _
    '                  "dbo.HERSA_HIBE_Expedating ON dbo.tmpOpenOrders.Vendor = dbo.HERSA_HIBE_Expedating.IDVendor LEFT OUTER JOIN " & _
    '                  "dbo.Plant ON dbo.tmpOpenOrders.Plant = dbo.Plant.Code LEFT OUTER JOIN " & _
    '                  "dbo.tmpSupPortalConfimation ON dbo.tmpOpenOrders.Usuario = dbo.tmpSupPortalConfimation.TNumber AND " & _
    '                  "dbo.tmpOpenOrders.[Item Number] = dbo.tmpSupPortalConfimation.Item AND dbo.tmpOpenOrders.[Doc Number] = dbo.tmpSupPortalConfimation.PO AND " & _
    '                  "dbo.tmpOpenOrders.SAPBox = dbo.tmpSupPortalConfimation.SAPBox LEFT OUTER JOIN " & _
    '                  "dbo.VendorsG11 ON dbo.tmpOpenOrders.Vendor = dbo.VendorsG11.Vendor LEFT OUTER JOIN " & _
    '                  "dbo.ERSA_HIBE ON dbo.tmpOpenOrders.Material = dbo.ERSA_HIBE.Material " & _
    '                  " WHERE (dbo.tmpOpenOrders.[GR Qty] < dbo.tmpOpenOrders.Quantity) And " & _
    '                  " ((dbo.tmpOpenOrders.Usuario = '" & gsUsuarioPC & "')" & POFilterString & IIf(pFiltro.Length > 0, ") And (" & pFiltro, "") & ") Order by [Doc Number], [Item Number]"


    '    POs = cn.RunSentence(vst).Tables(0)

    '    '****************************************************************************************************************************************************
    '    '****************************************************************************************************************************************************


    '    Me.dtgRequisiciones.DataSource = POs
    '    Me.dtgRequisiciones.Columns.Insert(0, cn.AddComboRootCauses("RootCauses", "RootCauses", "Select ID, Descripcion From RootCauses Where Tipo = 2 Order by Descripcion", "ID", "Descripcion"))
    '    Me.dtgRequisiciones.Columns.Insert(0, cn.AddButtonColumn("COMMENTS", "COMMENTS", 25))
    '    Me.dtgRequisiciones.Columns.Insert(0, cn.AddCheckBoxColumn("Ck", "Ck"))
    '    Me.txtTotalPO.Text = POs.Rows.Count

    '    If ShowPReq Then
    '        Dim EBAN As New SAPCOM.EBAN_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
    '        Dim drPO As DataRow

    '        For Each drPO In POs.Rows
    '            EBAN.IncludePurchOrder(drPO("Doc Number"))
    '        Next

    '        EBAN.Execute()

    '        If EBAN.Success Then
    '            If EBAN.ErrMessage = Nothing Then
    '                Dim EBANRow As DataRow
    '                'Me.dtgRequisiciones.Columns.Add("Req Number", "Req Number")
    '                'Me.dtgRequisiciones.Columns.Add("Req Item", "Req Item")

    '                POs.Columns.Add("Req Number")
    '                POs.Columns.Add("Req Item")

    '                For Each drPO In POs.Rows
    '                    For Each EBANRow In EBAN.Data.Rows
    '                        If drPO("Doc Number") = EBANRow("Purch Doc") AndAlso drPO("Item Number") = CDbl(EBANRow("PO Item")) Then
    '                            drPO("Req Number") = EBANRow("Req Number")
    '                            drPO("Req Item") = EBANRow("Req Item")
    '                            Exit For
    '                        End If
    '                    Next
    '                Next
    '            Else
    '                MsgBox(EBAN.ErrMessage)
    '            End If
    '            MsgBox(EBAN.ErrMessage)
    '        End If
    '    End If

    '    If POs.Rows.Count > 0 Then
    '        VerificarComentariosYRootCauses()
    '        CargarConfiguracion()
    '    End If
    'End Sub
#End Region
   
    Private Sub LoadInfo(ByVal pFiltro As String)
        'Try
        Dim POFilterString As String = ""

        If giDistribution <> 0 Then
            'Agrego al filtro los vendors según su exception:
            Select Case giDistribution
                Case 1
                    POFilterString = " And (( [Nac / Imp] = 'National')" & cn.getExceptions(giDistribution) & ")"

                Case 2
                    POFilterString = " And (( [Nac / Imp] = 'Import')" & cn.getExceptions(giDistribution) & ")"
            End Select
        End If

        If chkFinalInvoice.Checked Then
            POFilterString = POFilterString + " And ([Final Invoice] <> 'X')"
        End If

        Me.dtgRequisiciones.DataSource = ""
        Me.dtgRequisiciones.Columns.Clear()

        Dim vst As String = ""
        vst = "SELECT * ,DATEDIFF(day, A.[Delivery Date], { fn NOW() }) as Aging From fn_get_Open_Items_Report('" & gsUsuarioPC & "') as A WHERE (([GR Qty] < Quantity) AND ([Created By] NOT IN (Select TNumber From Exclude_POs_Created_By))" & POFilterString & IIf(pFiltro.Length > 0, ") And (" & pFiltro, "") & ")"

        POs = cn.RunSentence(vst).Tables(0)

        '****************************************************************************************************************************************************
        '****************************************************************************************************************************************************


        Me.dtgRequisiciones.DataSource = POs
        Me.dtgRequisiciones.Columns.Insert(0, cn.AddComboRootCauses("RootCauses", "RootCauses", "Select ID, Descripcion From RootCauses Where Tipo = 2 Order by Descripcion", "ID", "Descripcion"))
        Me.dtgRequisiciones.Columns.Insert(0, cn.AddButtonColumn("COMMENTS", "COMMENTS", 25))
        Me.dtgRequisiciones.Columns.Insert(0, cn.AddCheckBoxColumn("Ck", "Ck"))
        Me.txtTotalPO.Text = POs.Rows.Count

        If ShowPReq Then
            Dim EBAN As New SAPCOM.EBAN_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
            Dim drPO As DataRow

            For Each drPO In POs.Rows
                EBAN.IncludePurchOrder(drPO("Doc Number"))
            Next

            EBAN.Execute()

            If EBAN.Success Then
                If EBAN.ErrMessage = Nothing Then
                    Dim EBANRow As DataRow
                    'Me.dtgRequisiciones.Columns.Add("Req Number", "Req Number")
                    'Me.dtgRequisiciones.Columns.Add("Req Item", "Req Item")

                    POs.Columns.Add("Req Number")
                    POs.Columns.Add("Req Item")

                    For Each drPO In POs.Rows
                        For Each EBANRow In EBAN.Data.Rows
                            If drPO("Doc Number") = EBANRow("Purch Doc") AndAlso drPO("Item Number") = CDbl(EBANRow("PO Item")) Then
                                drPO("Req Number") = EBANRow("Req Number")
                                drPO("Req Item") = EBANRow("Req Item")
                                Exit For
                            End If
                        Next
                    Next
                Else
                    MsgBox(EBAN.ErrMessage)
                End If
                MsgBox(EBAN.ErrMessage)
            End If
        End If

        If POs.Rows.Count > 0 Then
            VerificarComentariosYRootCauses()
            CargarConfiguracion()
        End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub cboIdioma_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboIdioma.SelectionChangeCommitted
        Me.cboEstacionarios.Text = ""
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub cmdRefrescar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefrescar.Click
        LoadInfo("")
    End Sub

    ''' <summary>
    ''' Bloque el DataGrid para edición y habilita solo celdas elegidas por programación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockGrid()
        Dim I As Integer = 0

        For I = 0 To Me.dtgRequisiciones.Columns.Count - 1
            Me.dtgRequisiciones.Columns(I).ReadOnly = True
            Me.dtgRequisiciones.Columns(I).DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
        Next

        Me.dtgRequisiciones.Columns("Comments").ReadOnly = False
        Me.dtgRequisiciones.Columns("Comments").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("CK").ReadOnly = False
        Me.dtgRequisiciones.Columns("CK").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("RootCauses").ReadOnly = False
        Me.dtgRequisiciones.Columns("RootCauses").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("Correo").ReadOnly = False
        Me.dtgRequisiciones.Columns("Correo").DefaultCellStyle.BackColor = Drawing.Color.White

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim I As Integer = 0

        For I = 0 To Me.dtgRequisiciones.RowCount - 1
            Me.dtgRequisiciones.Rows(I).Cells("CK").Value = True
        Next
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim I As Integer = 0

        For I = 0 To Me.dtgRequisiciones.RowCount - 1
            Me.dtgRequisiciones.Rows(I).Cells("CK").Value = False
        Next
    End Sub

    Private Sub cmdFiltrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltrar.Click
        Dim MyFilter As String = ""
        If Me.txtBuscar.Text.Length > 0 Then

            For Each Col As Data.DataColumn In POs.Columns
                If Col.ColumnName <> "Aging" Then
                    Select Case Col.DataType.ToString.ToUpper
                        Case "SYSTEM.STRING"
                            If MyFilter.Length > 0 Then

                                Select Case Col.ColumnName
                                    'Case "M_Type"
                                    '    MyFilter = MyFilter & " or (dbo.ERSA_HIBE.[" & Col.ColumnName & "] = '" & txtBuscar.Text & "')"

                                    'Case "Correo"
                                    '    MyFilter = MyFilter & " or (dbo.VendorsG11.[VndMail1] = '" & txtBuscar.Text & "')"

                                    'Case "SP Confirmation"
                                    '    MyFilter = MyFilter & " or (dbo.tmpSupPortalConfimation.[Confirmation] = '" & txtBuscar.Text & "')"

                                    Case "Nac / Imp"

                                    Case "Comentarios"

                                    Case "Status"

                                    Case "Req Number"

                                    Case "Req Item"



                                    Case Else
                                        If ((Col.ColumnName <> "Vendor OTD Average") And (Col.ColumnName <> "RC")) Then
                                            MyFilter = MyFilter & " or ([" & Col.ColumnName & "] = '" & txtBuscar.Text & "')"
                                        End If

                                End Select

                            Else
                                If Col.ColumnName = "M_Type" Then

                                Else
                                    MyFilter = "([" & Col.ColumnName & "] = '" & txtBuscar.Text & "')"
                                End If


                            End If

                        Case "SYSTEM.DATETIME"
                            Dim lDate As Date
                            If (DateTime.TryParse(txtBuscar.Text, lDate)) Then

                                If MyFilter.Length > 0 Then
                                    MyFilter = MyFilter & " or ([" & Col.ColumnName & "] = '" & lDate & "')"
                                Else
                                    MyFilter = "([" & Col.ColumnName & "] = '" & lDate & "')"
                                End If

                            End If

                        Case "SYSTEM.BOOLEAN"
                            Dim lBoolean As Boolean

                            If Boolean.TryParse(txtBuscar.Text, lBoolean) Then
                                If MyFilter.Length > 0 Then
                                    MyFilter = MyFilter & " or ([" & Col.ColumnName & "] = '" & lBoolean & "')"
                                Else
                                    MyFilter = "([" & Col.ColumnName & "] = '" & lBoolean & "')"
                                End If
                            End If

                        Case Else
                            Dim lDouble As Double

                            If Double.TryParse(txtBuscar.Text, lDouble) Then
                                If MyFilter.Length > 0 Then
                                    If ((Col.ColumnName <> "Vendor OTD Average") And (Col.ColumnName <> "RC")) Then
                                        MyFilter = MyFilter & " or ([" & Col.ColumnName & "] = " & lDouble & ")"
                                    End If
                                Else
                                    If ((Col.ColumnName <> "Vendor OTD Average") And (Col.ColumnName <> "RC")) Then
                                        MyFilter = "([" & Col.ColumnName & "] = " & lDouble & ")"
                                    End If
                                End If
                            End If


                    End Select
                End If
            Next
            LoadInfo(MyFilter)
        Else
            LoadInfo("")
        End If

    End Sub

    Private Sub cmdCopiarARequisitante_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopiarARequisitante.Click
        Me.cmdCopiarARequisitante.Checked = Not Me.cmdCopiarARequisitante.Checked
    End Sub

    Private Sub cmdOutlook_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOutlook.ButtonClick
        Dim i As Integer = 0
        Dim Aging As Integer = 0
        Dim PO As String = ""
        Dim Item As String = ""
        Dim Material As String = ""
        Dim Cant As String = ""
        Dim Desc As String = ""
        Dim DelDate As String = ""
        Dim Vendor As String = ""
        Dim VendorName As String = ""
        Dim SAP As String = ""
        Dim Mail As String = ""
        Dim MailCC As String = ""
        Dim Precio As String = ""
        Dim VendorEnvio As New DataTable 'Variable para saber cuales vendors son los que seleccionó el usuario
        Dim lsBody As String = ""
        Dim Detalle As New DataTable 'Datatable donde se colocará el detalle de las órdenes de compra a ser enviadas al proveedor
        Dim SQLDetalle As String = ""
        Dim Index As Integer = 0
        Dim CC As String = ""

        Dim Attach() As String 'variable para anexar archivos al correo
        ReDim Attach(1)

        Dim Estacionarios As DataTable

        'Vrificación de la seleccion de estacionario:
        If Me.cboEstacionarios.SelectedValue <> Nothing Then
            If Not Me.cboEstacionarios.SelectedValue.ToString <> "System.Data.DataRowView" Then
                MsgBox("Por Favor seleccione un estacionario", MsgBoxStyle.Information)
                Exit Sub
            End If
        Else
            MsgBox("Por Favor seleccione un estacionario", MsgBoxStyle.Information)
            Exit Sub
        End If

        'Cargo el estacionario
        Estacionarios = cn.RunSentence("Select * From Estacionarios Where IDEstacionario = " & Me.cboEstacionarios.SelectedValue.ToString).Tables(0)
        If Estacionarios.Rows.Count <= 0 Then
            MsgBox("Error al cargar el estacionario", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        '************************************************************************************************************
        '************************************************************************************************************
        '   Guardo los ítems que voy a enviar al vendor por correo
        Me.dtgRequisiciones.EndEdit()

        cn.ExecuteInServer("Delete From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "'")

        For Each R As DataGridViewRow In dtgRequisiciones.Rows
            If R.Cells("CK").Value Then
                Aging = R.Cells("Aging").Value
                PO = R.Cells("Doc Number").Value
                Item = R.Cells("Item Number").Value
                Material = R.Cells("Material").Value
                Cant = R.Cells("Quantity").Value
                Desc = Replace(R.Cells("Short Text").Value, "'", "")
                DelDate = R.Cells("Delivery Date").Value
                Vendor = R.Cells("Vendor").Value
                SAP = R.Cells("SAPBox").Value
                Mail = R.Cells("Correo").Value
                Precio = R.Cells("Price").Value
                VendorName = R.Cells("Vendor Name").Value
                MailCC = R.Cells("Requisitioner").Value
                cn.ExecuteInServer("Insert Into tmpEnvioSeguimiento Values('" & gsUsuarioPC & "','" & Aging & "'," & PO & "," & Item & ",'" & Material & "','" & Desc & "','" & DelDate & "','" & Vendor & "','" & SAP & "','" & Mail & "','" & Cant & "','" & Precio & "','" & VendorName & "','" & MailCC & "')")
            End If
        Next


        'Selección de los vendors a los que se le van a enviar correos:
        If Me.cmdCopiarARequisitante.Checked Then
            VendorEnvio = cn.RunSentence("Select distinct Vendor, Mail,MailCC From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "' Group by Vendor,Mail,MailCC").Tables(0)
        Else
            VendorEnvio = cn.RunSentence("Select distinct Vendor, Mail From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "' Group by Vendor,Mail").Tables(0)
        End If

        If VendorEnvio.Rows.Count > 0 Then
            For i = 0 To VendorEnvio.Rows.Count - 1
                '************************************************************
                '************************************************************
                'Verifico que el estacionario tenga el tag para el detalle:
                'Para que el estacionario pueda tener dos tipos de Detalle: 8/mar/2010
                Index = Estacionarios.Rows(0).Item("Mensage").IndexOf("<@DETALLE>")
                If Index <= 0 Then 'Si no encuentra el <@DETALLE> busco el <@DETALLEUSUARIO>
                    Index = Estacionarios.Rows(0).Item("Mensage").IndexOf("<@DETALLEUSUARIO>")
                    If Index <= 0 Then
                        MsgBox("No se encontraron los Tag de detalle en el body del estacionario.", MsgBoxStyle.Information, "Tag no encontrados")
                        Exit Sub
                    Else
                        'Cargo el SQL de <@DETALLEUSUARIO>
                        SQLDetalle = "Select Aging as [Días], SAP,[Purch. Order] as [Orden Compra],[Item],Vendor,VendorName,Cantidad,Material,Description as Descripcion,Precio,[Del. Date] as [F. Entrega] From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "' And Vendor = '" & VendorEnvio.Rows(i).Item("Vendor") & "'"
                    End If
                Else
                    'Cargo el SQL de <@DETALLE>
                    SQLDetalle = "Select Aging as [Días], SAP,[Purch. Order] as [Orden Compra],[Item],Cantidad,Material,Description as Descripcion,[Del. Date] as [F. Entrega] From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "' And Vendor = '" & VendorEnvio.Rows(i).Item("Vendor") & "'"
                End If
                '************************************************************
                '************************************************************

                Detalle = cn.RunSentence(SQLDetalle).Tables(0)
                If cn.ExportDataTableToXL(Detalle, PDFPath & "\OpenOrder.xls") Then
                    Attach(0) = PDFPath & "\OpenOrder.xls"
                    cn.Put_HTML_Table_In_ClipBoard(Detalle)
                    lsBody = Replace(Estacionarios.Rows(0).Item("Mensage"), "<@DETALLE>", My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text))
                    lsBody = Replace(lsBody, "<@DETALLEUSUARIO>", My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text))

                    If Me.cmdCopiarARequisitante.Checked Then
                        MailCC = Microsoft.VisualBasic.Left(VendorEnvio.Rows(i).Item("MailCC"), 6) & "@PG.COM"
                    Else
                        MailCC = ""
                    End If
                    cn.SendOutlookMail(Estacionarios.Rows(0).Item("Asunto"), Attach, VendorEnvio.Rows(i).Item("Mail"), MailCC, lsBody, "", False, "HTML")
                End If
            Next
        Else
            MsgBox("No se han encontrado registros exportados a la base de datos", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        MsgBox("Done!")
    End Sub

    Private Sub SendMailToRequisitionerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendMailToRequisitionerToolStripMenuItem.Click
        Dim i As Integer = 0
        Dim Aging As Integer = 0
        Dim PO As String = ""
        Dim Item As String = ""
        Dim Material As String = ""
        Dim Cant As String = ""
        Dim Desc As String = ""
        Dim DelDate As String = ""
        Dim Vendor As String = ""
        Dim VendorName As String = ""
        Dim SAP As String = ""
        Dim Mail As String = ""
        Dim MailCC As String = ""
        Dim Precio As String = ""
        Dim VendorEnvio As New DataTable 'Variable para saber cuales vendors son los que seleccionó el usuario
        Dim lsBody As String = ""
        Dim Detalle As New DataTable 'Datatable donde se colocará el detalle de las órdenes de compra a ser enviadas al proveedor
        Dim SQLDetalle As String = ""
        Dim Index As Integer = 0
        Dim CC As String = ""
        Dim requis As String = ""

        Dim Attach() As String 'variable para anexar archivos al correo
        ReDim Attach(1)

        Dim Estacionarios As DataTable
        'Verificación de la seleccion de estacionario:
        If Me.cboEstacionarios.SelectedValue <> Nothing Then
            If Not Me.cboEstacionarios.SelectedValue.ToString <> "System.Data.DataRowView" Then
                MsgBox("Por Favor seleccione un estacionario", MsgBoxStyle.Information)
                Exit Sub
            End If
        Else
            MsgBox("Por Favor seleccione un estacionario", MsgBoxStyle.Information)
            Exit Sub
        End If

        'Cargo el estacionario
        Estacionarios = cn.RunSentence("Select * From Estacionarios Where IDEstacionario = " & Me.cboEstacionarios.SelectedValue.ToString).Tables(0)
        If Estacionarios.Rows.Count <= 0 Then
            MsgBox("Error al cargar el estacionario", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        '************************************************************************************************************
        '************************************************************************************************************
        '   Guardo los ítems que voy a enviar al vendor por correo
        Me.dtgRequisiciones.EndEdit()

        cn.ExecuteInServer("Delete From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "'")



        For Each R As DataGridViewRow In dtgRequisiciones.Rows
            If R.Cells("CK").Value Then
                Aging = R.Cells("Aging").Value
                PO = R.Cells("Doc Number").Value
                Item = R.Cells("Item Number").Value
                Material = R.Cells("Material").Value
                Cant = R.Cells("Quantity").Value
                Desc = Replace(R.Cells("Short Text").Value, "'", "")
                DelDate = R.Cells("Delivery Date").Value
                Vendor = R.Cells("Vendor").Value
                SAP = R.Cells("SAPBox").Value
                Mail = R.Cells("Correo").Value
                Precio = R.Cells("Price").Value
                VendorName = R.Cells("Vendor Name").Value
                MailCC = R.Cells("Requisitioner").Value
                cn.ExecuteInServer("Insert Into tmpEnvioSeguimiento Values('" & gsUsuarioPC & "','" & Aging & "'," & PO & "," & Item & ",'" & Material & "','" & Desc & "','" & DelDate & "','" & Vendor & "','" & SAP & "','" & Mail & "','" & Cant & "','" & Precio & "','" & VendorName & "','" & MailCC & "')")
            End If
        Next


        'For i = 0 To Me.dtgRequisiciones.RowCount - 1
        '    If Me.dtgRequisiciones.Rows(i).Cells("CK").Value Then
        '        Aging = Me.dtgRequisiciones.Rows(i).Cells("Aging").Value
        '        PO = Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value
        '        Item = Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value
        '        Material = Me.dtgRequisiciones.Rows(i).Cells("Material").Value
        '        Cant = Me.dtgRequisiciones.Rows(i).Cells("Quantity").Value
        '        Desc = Replace(Me.dtgRequisiciones.Rows(i).Cells("Short Text").Value, "'", "")
        '        DelDate = Me.dtgRequisiciones.Rows(i).Cells("Delivery Date").Value
        '        Vendor = Me.dtgRequisiciones.Rows(i).Cells("Vendor").Value
        '        SAP = Me.dtgRequisiciones.Rows(i).Cells("SAPBox").Value
        '        Mail = Me.dtgRequisiciones.Rows(i).Cells("Correo").Value
        '        Precio = Me.dtgRequisiciones.Rows(i).Cells("Price").Value
        '        VendorName = Me.dtgRequisiciones.Rows(i).Cells("Vendor Name").Value
        '        MailCC = Me.dtgRequisiciones.Rows(i).Cells("Requisitioner").Value
        '        cn.ExecuteInServer("Insert Into tmpEnvioSeguimiento Values('" & gsUsuarioPC & "','" & Aging & "'," & PO & "," & Item & ",'" & Material & "','" & Desc & "','" & DelDate & "','" & Vendor & "','" & SAP & "','" & Mail & "','" & Cant & "','" & Precio & "','" & VendorName & "','" & MailCC & "')")
        '    End If
        'Next


        'Selección de los vendors a los que se le van a enviar correos:
        VendorEnvio = cn.RunSentence("Select distinct MailCC From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "' Group by MailCC").Tables(0)

        If VendorEnvio.Rows.Count > 0 Then
            For i = 0 To VendorEnvio.Rows.Count - 1
                '************************************************************
                '************************************************************
                'Verifico que el estacionario tenga el tag para el detalle:
                'Para que el estacionario pueda tener dos tipos de Detalle: 8/mar/2010
                Index = Estacionarios.Rows(0).Item("Mensage").IndexOf("<@DETALLE>")
                If Index <= 0 Then 'Si no encuentra el <@DETALLE> busco el <@DETALLEUSUARIO>
                    Index = Estacionarios.Rows(0).Item("Mensage").IndexOf("<@DETALLEUSUARIO>")
                    If Index <= 0 Then
                        MsgBox("No se encontraron los Tag de detalle en el body del estacionario.", MsgBoxStyle.Information, "Tag no encontrados")
                        Exit Sub
                    Else
                        'Cargo el SQL de <@DETALLEUSUARIO>
                        SQLDetalle = "Select Aging as [Días], SAP,[Purch. Order] as [Orden Compra],[Item],Vendor,VendorName,Cantidad,Material,Description as Descripcion,Precio,[Del. Date] as [F. Entrega] From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "' And MailCC = '" & VendorEnvio.Rows(i).Item("MailCC") & "'"
                    End If
                Else
                    'Cargo el SQL de <@DETALLE>
                    SQLDetalle = "Select Aging as [Días], SAP,[Purch. Order] as [Orden Compra],[Item],Cantidad,Material,Description as Descripcion,[Del. Date] as [F. Entrega] From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "' And MailCC = '" & VendorEnvio.Rows(i).Item("MailCC") & "'"
                End If
                '************************************************************
                '************************************************************

                Detalle = cn.RunSentence(SQLDetalle).Tables(0)
                If cn.ExportDataTableToXL(Detalle, PDFPath & "\OpenOrder.xls") Then
                    Attach(0) = PDFPath & "\OpenOrder.xls"
                    cn.Put_HTML_Table_In_ClipBoard(Detalle)
                    lsBody = Replace(Estacionarios.Rows(0).Item("Mensage"), "<@DETALLE>", My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text))
                    lsBody = Replace(lsBody, "<@DETALLEUSUARIO>", My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text))

                    cn.SendOutlookMail(Estacionarios.Rows(0).Item("Asunto"), Attach, Microsoft.VisualBasic.Left(VendorEnvio.Rows(i).Item("MailCC"), 6) & "@PG.COM", "", lsBody, "", False, "HTML")
                End If
            Next
        Else
            MsgBox("No se han encontrado registros exportados a la base de datos", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        MsgBox("Done!")
    End Sub

    Private Sub cboVariantes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVariantes.SelectedIndexChanged

        If Me.cboVariantes.SelectedText <> "" Then

        Else
            If Not Me.cboVariantes.SelectedValue.ToString <> "System.Data.DataRowView" Then
                'MsgBox("Por Favor seleccione una variante", MsgBoxStyle.Information)
                Exit Sub
            End If

            '*********************************************************************
            '*********************************************************************
            'Comentado para utilizar la variable global

            ''Dim dtPOFilter As DataTable
            ''dtPOFilter = cn.RunSentence("Select POFilter From HeaderVariante Where IDVariante = " & Me.cboVariantes.SelectedValue.ToString).Tables(0)

            ''If Not DBNull.Value.Equals(dtPOFilter.Rows(0).Item("POFilter")) Then
            ''    POFilter = dtPOFilter.Rows(0).Item("POFilter")
            ''Else
            ''    POFilter = 0
            ''End If
        End If
    End Sub

    Private Sub cboIdioma_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboIdioma.SelectedIndexChanged
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub cmdPOAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPOAnalysis.Click
        Dim tmp As DataTable

        tmp = POs.Copy

        cn.ExecuteInServer("Delete From tmpOpenOrders_Analisys Where Usuario = '" & gsUsuarioPC & "'")

        tmp.Columns.Remove("Doc Number")
        tmp.Columns.Remove("Item Number")
        tmp.Columns.Remove("Mat Group")
        tmp.Columns.Remove("Material")
        tmp.Columns.Remove("Short Text")
        tmp.Columns.Remove("Vendor")
        tmp.Columns.Remove("Vendor Name")
        tmp.Columns.Remove("Company Code")
        tmp.Columns.Remove("Purch Org")
        tmp.Columns.Remove("Purch Grp")
        tmp.Columns.Remove("Doc Date")
        tmp.Columns.Remove("Doc Type")
        tmp.Columns.Remove("Created By")
        tmp.Columns.Remove("Tracking Field")
        tmp.Columns.Remove("Quantity")
        tmp.Columns.Remove("UOM")
        tmp.Columns.Remove("Currency")
        tmp.Columns.Remove("Del Indicator")
        tmp.Columns.Remove("Delivery Comp")
        tmp.Columns.Remove("Final Invoice")
        tmp.Columns.Remove("Requisitioner")
        tmp.Columns.Remove("Price")
        'tmp.Columns.Remove("Delivery Date")
        tmp.Columns.Remove("Repair")
        tmp.Columns.Remove("M_Type")
        tmp.Columns.Remove("Correo")
        tmp.Columns.Remove("Vendor OTD Average")
        tmp.Columns.Remove("SP Confirmation")
        tmp.Columns.Remove("Nac / Imp")
        tmp.Columns.Remove("Status")
        tmp.Columns.Remove("Aging")
        tmp.Columns.Remove("GR Qty")
        tmp.Columns.Remove("IR Qty")

        cn.AppendTableToSqlServer("tmpOpenOrders_Analisys", tmp)
        Dim Form As New frm050
        Form.ShowDialog()
    End Sub

    Private Sub cmdClosePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClosePO.Click
        Dim drRow As New System.Windows.Forms.DataGridViewRow
        Me.dtgRequisiciones.EndEdit()
        Dim frmComments As New frm056
        Dim Flag As Boolean = True

        If MsgBox("Are you sure to cancel these Purchase Orders?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            For Each drRow In dtgRequisiciones.Rows
                If drRow.Cells("Ck").Value Then

                    If Flag Then
                        frmComments.Document = drRow.Cells("Doc Number").Value
                        frmComments.Item = drRow.Cells("Item Number").Value
                        frmComments.ShowDialog()
                        Flag = frmComments.This_Item
                    End If

                    Dim POChange As New SAPCOM.POChanges(drRow.Cells("SAPBox").Value, gsUsuarioPC, AppId, drRow.Cells("Doc Number").Value)

                    POChange.InsertHeaderText(HeaderNote, "[DB:\> Item " & drRow.Cells("Item Number").Value.ToString.Trim & "]: " & frmComments.Comment)
                    POChange.CloseItem(drRow.Cells("Item Number").Value)

                    POChange.CommitChanges()
                    If Not POChange.Success Then
                        MsgBox("Error while closing PO: " & drRow.Cells("SAPBox").Value & ":" & drRow.Cells("Doc Number").Value & " Item " & drRow.Cells("Item Number").Value & Chr(13) & Chr(13))
                    End If
                End If
            Next
        End If

        MsgBox("Process finished.")
    End Sub

    Private Sub cmdDowload_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDowload.ButtonClick
        ShowPReq = False
        Download()
    End Sub

    Private Sub DownloadWithPurchReqInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadWithPurchReqInfoToolStripMenuItem.Click
        ShowPReq = True
        Download()
    End Sub

    Private Sub cmdConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirm.Click
        Me.dtgRequisiciones.EndEdit()
        Dim C As New Confirmation

        For Each drRow As System.Windows.Forms.DataGridViewRow In dtgRequisiciones.Rows
            If drRow.Cells("Ck").Value Then
                If drRow.Cells("O Reference").Value.ToString.ToUpper.IndexOf("Y") = -1 Then
                    Dim P As New PO2Confirm(drRow.Cells("SAPBox").Value, drRow.Cells("Doc Number").Value, drRow.Cells("O Reference").Value)
                    C.Add(P)
                End If
            End If
        Next
        If Not C.Confirm() Then
            Dim NCNF As String = ""
            For Each P As PO2Confirm In C.POs
                If Not P.Confirmed Then
                    NCNF = NCNF & Chr(13) & P.DocNumber
                End If
            Next

            MsgBox("Process finished." & Chr(13) & Chr(13) & "Following items couldn't be confirmed: " & NCNF, MsgBoxStyle.Information)
        Else
            MsgBox("Process finished." & MsgBoxStyle.Information)
        End If
    End Sub
End Class


Public Class PO2Confirm
#Region "Variables"
    Private _SAPBox As String = ""
    Private _DocNumber As String = ""
    Private _Confirmed As Boolean = False
    Private _OReference As String = ""
#End Region
#Region "Properties"
    Public Property SAPBox() As String
        Get
            Return _SAPBox
        End Get
        Set(ByVal value As String)
            _SAPBox = value
        End Set
    End Property
    Public Property DocNumber() As String
        Get
            Return _DocNumber
        End Get
        Set(ByVal value As String)
            _DocNumber = value
        End Set
    End Property
    Public Property Confirmed() As Boolean
        Get
            Return _Confirmed
        End Get
        Set(ByVal value As Boolean)
            _Confirmed = value
        End Set
    End Property
    Public Property OReference() As String
        Get
            Return _OReference
        End Get
        Set(ByVal value As String)
            _OReference = value
        End Set
    End Property
#End Region
#Region "Methods"
    Public Sub New(ByVal SAPBox As String, ByVal DocNumber As String, ByVal OReference As String)
        _SAPBox = SAPBox
        _DocNumber = DocNumber
        _OReference = OReference
    End Sub
    Public Sub New()

    End Sub
#End Region
End Class

Public Class Confirmation
#Region "Variables"
    Private _POs As New List(Of PO2Confirm)
    Private _TNumber As String
    Private _Success As Boolean
    Private _ErrorList As New List(Of String)
#End Region
#Region "Properties"
    Public Property TNumber() As String
        Get
            Return _TNumber
        End Get
        Set(ByVal value As String)
            _TNumber = value
        End Set
    End Property
    Public ReadOnly Property POs() As List(Of PO2Confirm)
        Get
            Return _POs
        End Get
    End Property
#End Region
#Region "Methods"
    Public Function Confirm() As Boolean
        _Success = True

        For Each P As PO2Confirm In _POs
            Try
                Dim POChange As New SAPCOM.POChanges(P.SAPBox, gsUsuarioPC, "LAT", P.DocNumber)
                If POChange.IsReady Then
                    POChange.OurReference = "Y" & Left(P.OReference, 11)
                    POChange.CommitChanges()

                    If Not POChange.Success Then
                        For Each er As String In POChange.Results
                            _ErrorList.Add("Doc: " & P.DocNumber & " - " & er)
                        Next
                        P.Confirmed = False
                    Else
                        P.Confirmed = True
                    End If
                Else
                    P.Confirmed = False

                End If

            Catch ex As Exception
                P.Confirmed = False
            End Try
        Next

        'Check confirmations:
        For Each P As PO2Confirm In _POs
            If Not P.Confirmed Then
                _Success = False
            End If
        Next


    End Function
    Public Sub Add(ByVal PO As PO2Confirm)
        Dim Found As Boolean = False

        For Each P As PO2Confirm In _POs
            If P.DocNumber = PO.DocNumber Then
                Found = True
                Exit For
            End If
        Next

        If Not Found Then
            _POs.Add(PO)
        End If
    End Sub
#End Region
End Class