Imports SAPCOM.RepairsLevels
Imports System.Xml.Linq
Imports SAPCOM.SAPTextIDs
Imports System.Windows.Forms

Public Class frm082
    Public Report_Rows As Integer
    Public Report_Count As Integer
    Public Report_Message As String
    Dim PO_Print As SAPConection.POPrinting

    Dim cn As New OAConnection.Connection
    Dim Comentarios As New DataTable
    Dim Waitting As Boolean = False
    Dim POFilterString As String = ""
    Dim POs As New DataTable
    Dim ShowPReq As Boolean = False
    Dim SAPBox As String = ""
    Dim IdVar As Integer = 0

    Private Sub frm030_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        GuardarConfiguracion()
    End Sub
    Private Sub frm030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
        cn.LoadCombo(Me.cboIdioma, "Select IDIdioma,Descripcion From Idioma", "IDIdioma", "Descripcion")
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
        'MsgBox("This is a beta test module." & Chr(13) & Chr(13) & "Consider that some function would not work properly until release version.", MsgBoxStyle.Information, "Beta test module")
    End Sub
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
        Dim MatGrp As New DataTable

        Me.pbProgress.Value = 10

        Me.dtgRequisiciones.Columns.Clear()

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


                If POs.Columns.IndexOf("YReference") <> -1 Then
                    POs.Columns.Remove("YReference")
                End If

                If POs.Columns.IndexOf("OReference") <> -1 Then
                    POs.Columns.Remove("OReference")
                End If

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
                        End If
                    End If
                End If
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

        Dim row As DataGridViewRow

        For Each row In Me.dtgRequisiciones.Rows
            If Not DBNull.Value.Equals(row.Cells("Last Comments").Value) Then
                row.Cells("Comments").Value = "≈≈≈"
                row.Cells("Comments").ToolTipText = row.Cells("Last Comments").Value
            End If

            If Not DBNull.Value.Equals(row.Cells("RC").Value) AndAlso row.Cells("RC").Value <> 0 Then
                row.Cells("RootCauses").Value = row.Cells("RC").Value
            End If

            If Not DBNull.Value.Equals(row.Cells("Vendor OTD Average").Value) Then
                Select Case row.Cells("Vendor OTD Average").Value
                    Case 90 To 100
                        row.Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.MediumAquamarine

                    Case 80 To 89
                        row.Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.SandyBrown

                    Case Is < 79
                        row.Cells("Vendor OTD Average").Style.BackColor = Drawing.Color.Tomato
                End Select
            End If


            If row.Cells("Multi Deliv").Value Then
                row.DefaultCellStyle.BackColor = Drawing.Color.Plum
            End If
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

            Case "OEMS_CASE_ID"
                Dim Param As New List(Of SqlClient.SqlParameter)

                'Dim SAP As New Data.SqlClient.SqlParameter("@SAPBox", Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value)
                Dim SAP As New Data.SqlClient.SqlParameter("@SAPBox", Me.dtgRequisiciones.CurrentRow.Cells("SAPBox").Value)
                Dim PO As New Data.SqlClient.SqlParameter("@PO", Me.dtgRequisiciones.CurrentRow.Cells("Doc Number").Value)
                Dim Item As New Data.SqlClient.SqlParameter("@Item", Me.dtgRequisiciones.CurrentRow.Cells("Item Number").Value)
                Dim SavedBy As New Data.SqlClient.SqlParameter("@SavedBy", gsUsuarioPC)
                Dim CaseID As New Data.SqlClient.SqlParameter("@OEMS_Case_ID", Me.dtgRequisiciones.CurrentRow.Cells("OEMS_Case_ID").Value)

                Param.Add(SAP)
                Param.Add(PO)
                Param.Add(Item)
                Param.Add(SavedBy)
                Param.Add(CaseID)

                cn.ExecuteStoredProcedure("stp_Save_OEMS_Case_ID_PO", Param)

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
                    POFilterString = " And (( [Spend] = 'National')" & cn.getExceptions(giDistribution) & ")"

                Case 2
                    POFilterString = " And (( [Spend] = 'Import')" & cn.getExceptions(giDistribution) & ")"
            End Select
        End If

        If chkFinalInvoice.Checked Then
            POFilterString = POFilterString + " And ([Final Invoice] <> 'X')"
        End If

        Me.dtgRequisiciones.DataSource = ""
        Me.dtgRequisiciones.Columns.Clear()

        Dim vst As String = ""
        vst = "SELECT * ,DATEDIFF(day, [Delivery Date], {fn NOW()}) as Aging From LA_User_Open_Order WHERE ((Usuario = '" & gsUsuarioPC & "') AND ([Created By] NOT IN (Select TNumber From Exclude_POs_Created_By)) And ([GR Qty] < Quantity)" & POFilterString & IIf(pFiltro.Length > 0, ") And (" & pFiltro, "") & ")"

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

        Me.dtgRequisiciones.Columns("Vendor Mail").ReadOnly = False
        Me.dtgRequisiciones.Columns("Vendor Mail").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("OEMS_Case_ID").ReadOnly = False
        Me.dtgRequisiciones.Columns("OEMS_Case_ID").DefaultCellStyle.BackColor = Drawing.Color.White

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
                MsgBox("Please select a stationary", MsgBoxStyle.Information)
                Exit Sub
            End If
        Else
            MsgBox("Please select a stationary", MsgBoxStyle.Information)
            Exit Sub
        End If

        'Cargo el estacionario
        Estacionarios = cn.RunSentence("Select * From Estacionarios Where IDEstacionario = " & Me.cboEstacionarios.SelectedValue.ToString).Tables(0)
        If Estacionarios.Rows.Count <= 0 Then
            MsgBox("Couldn't load stationary", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        '************************************************************************************************************
        '************************************************************************************************************
        '   Guardo los ítems que voy a enviar al vendor por correo
        Me.dtgRequisiciones.EndEdit()

        cn.ExecuteInServer("Delete From tmpEnvioSeguimiento Where TNumber = '" & gsUsuarioPC & "'")
        For i = 0 To Me.dtgRequisiciones.RowCount - 1
            If Me.dtgRequisiciones.Rows(i).Cells("CK").Value Then
                Aging = Me.dtgRequisiciones.Rows(i).Cells("Aging").Value
                PO = Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value
                Item = Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value
                Material = Me.dtgRequisiciones.Rows(i).Cells("Material").Value
                Cant = Me.dtgRequisiciones.Rows(i).Cells("Quantity").Value
                Desc = Replace(Me.dtgRequisiciones.Rows(i).Cells("Short Text").Value, "'", "")
                DelDate = Me.dtgRequisiciones.Rows(i).Cells("Delivery Date").Value
                Vendor = Me.dtgRequisiciones.Rows(i).Cells("Vendor").Value
                SAP = Me.dtgRequisiciones.Rows(i).Cells("SAPBox").Value
                Mail = Me.dtgRequisiciones.Rows(i).Cells("Vendor Mail").Value
                Precio = Me.dtgRequisiciones.Rows(i).Cells("Price").Value
                VendorName = Me.dtgRequisiciones.Rows(i).Cells("Vendor Name").Value
                MailCC = Me.dtgRequisiciones.Rows(i).Cells("Requisitioner").Value
                cn.ExecuteInServer("Insert Into tmpEnvioSeguimiento Values('" & gsUsuarioPC & "','" & Aging & "','" & PO & "','" & Item & "','" & Material & "','" & Desc & "','" & DelDate & "','" & Vendor & "','" & SAP & "','" & Mail & "','" & Cant & "','" & Precio & "','" & VendorName & "','" & MailCC & "')")
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
                        MsgBox("Tag: <@DETALLE> or <@DETALLEUSUARIO> not found.", MsgBoxStyle.Information, "Tag not found")
                        Exit Sub
                    Else
                        'Cargo el SQL de <@DETALLEUSUARIO>
                        SQLDetalle = "Select Aging AS Días, SAP, [Purch. Order] AS [Orden Compra], Item, Vendor, VendorName, Cantidad, Material, Description AS Descripcion, Precio, [Del. Date] AS [F. Entrega], OEMS_Case_ID FROM tmpEnvioSeguimiento LEFT OUTER JOIN OEMS_Case_ID_PO ON Item = [Item Number] AND [Purch. Order] = [PO Number] AND SAP = SAPBox Where TNumber = '" & gsUsuarioPC & "' And Vendor = '" & VendorEnvio.Rows(i).Item("Vendor") & "'"
                    End If
                Else
                    'Cargo el SQL de <@DETALLE>
                    SQLDetalle = "Select Aging as [Días], SAP,[Purch. Order] as [Orden Compra],[Item],Cantidad,Material,Description as Descripcion,[Del. Date] as [F. Entrega], OEMS_Case_ID From tmpEnvioSeguimiento LEFT OUTER JOIN OEMS_Case_ID_PO ON Item = [Item Number] AND [Purch. Order] = [PO Number] AND SAP = SAPBox Where TNumber = '" & gsUsuarioPC & "' And Vendor = '" & VendorEnvio.Rows(i).Item("Vendor") & "'"
                End If
                '************************************************************
                '************************************************************

                Detalle = cn.RunSentence(SQLDetalle).Tables(0)
                If cn.ExportDataTableToXL(Detalle, PDFPath & "\OpenOrder.xls") Then
                    Dim _IsSTR As Boolean = False
                    Dim CaseID As String = ""
                    Dim CaseIDPO As String = ""
                    For Each r As DataRow In Detalle.Rows
                        _IsSTR = IIf(Microsoft.VisualBasic.Left(r("Material"), 1) = "3", True, False)
                        CaseID = r("OEMS_Case_ID").ToString
                        CaseIDPO = r("Orden Compra").ToString
                    Next

                    Attach(0) = PDFPath & "\OpenOrder.xls"
                    cn.Put_HTML_Table_In_ClipBoard(Detalle)
                    lsBody = Replace(Estacionarios.Rows(0).Item("Mensage"), "<@DETALLE>", My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text))
                    lsBody = Replace(lsBody, "<@DETALLEUSUARIO>", My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text))

                    If Me.cmdCopiarARequisitante.Checked Then
                        MailCC = Microsoft.VisualBasic.Left(VendorEnvio.Rows(i).Item("MailCC"), 6) & "@PG.COM"
                    Else
                        MailCC = ""
                    End If

                    Dim Subject As String = ""
                    Subject = "Case ID:" & CaseID & " PO Number  " & CaseIDPO & "-" & Estacionarios.Rows(0).Item("Asunto")

                    cn.SendOutlookMail(Subject, Attach, VendorEnvio.Rows(i).Item("Mail"), MailCC, lsBody, "", False, "HTML", "", False, _IsSTR)
                End If
            Next
        Else
            MsgBox("Data not exported to data base.", MsgBoxStyle.Exclamation)
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
        Dim _IsSTR As Boolean = False
        Dim CaseID As String = ""
        Dim CaseIDPO As String = ""

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
        For i = 0 To Me.dtgRequisiciones.RowCount - 1
            If Me.dtgRequisiciones.Rows(i).Cells("CK").Value Then
                Aging = Me.dtgRequisiciones.Rows(i).Cells("Aging").Value
                PO = Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value
                Item = Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value
                Material = Me.dtgRequisiciones.Rows(i).Cells("Material").Value

                _IsSTR = IIf(Microsoft.VisualBasic.Left(Material, 1) = "3", True, False)
               
                CaseID = Me.dtgRequisiciones.Rows(i).Cells("OEMS_Case_ID").Value.ToString
                CaseIDPO = PO

                Cant = Me.dtgRequisiciones.Rows(i).Cells("Quantity").Value
                Desc = Replace(Me.dtgRequisiciones.Rows(i).Cells("Short Text").Value, "'", "")
                DelDate = Me.dtgRequisiciones.Rows(i).Cells("Delivery Date").Value
                Vendor = Me.dtgRequisiciones.Rows(i).Cells("Vendor").Value
                SAP = Me.dtgRequisiciones.Rows(i).Cells("SAPBox").Value
                Mail = Me.dtgRequisiciones.Rows(i).Cells("Vendor Mail").Value
                Precio = Me.dtgRequisiciones.Rows(i).Cells("Price").Value
                VendorName = Me.dtgRequisiciones.Rows(i).Cells("Vendor Name").Value
                MailCC = Me.dtgRequisiciones.Rows(i).Cells("Requisitioner").Value
                cn.ExecuteInServer("Insert Into tmpEnvioSeguimiento Values('" & gsUsuarioPC & "','" & Aging & "','" & PO & "','" & Item & "','" & Material & "','" & Desc & "','" & DelDate & "','" & Vendor & "','" & SAP & "','" & Mail & "','" & Cant & "','" & Precio & "','" & VendorName & "','" & MailCC & "')")
            End If
        Next

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
                        SQLDetalle = "Select Aging as [Días], SAP,[Purch. Order] as [Orden Compra],[Item],Vendor,VendorName,Cantidad,Material,Description as Descripcion,Precio,[Del. Date] as [F. Entrega] From tmpEnvioSeguimiento LEFT OUTER JOIN OEMS_Case_ID_PO ON Item = [Item Number] AND [Purch. Order] = [PO Number] AND SAP = SAPBox Where TNumber = '" & gsUsuarioPC & "' And MailCC = '" & VendorEnvio.Rows(i).Item("MailCC") & "'"
                    End If
                Else
                    'Cargo el SQL de <@DETALLE>
                    SQLDetalle = "Select Aging as [Días], SAP,[Purch. Order] as [Orden Compra],[Item],Cantidad,Material,Description as Descripcion,[Del. Date] as [F. Entrega] From tmpEnvioSeguimiento LEFT OUTER JOIN OEMS_Case_ID_PO ON Item = [Item Number] AND [Purch. Order] = [PO Number] AND SAP = SAPBox Where TNumber = '" & gsUsuarioPC & "' And MailCC = '" & VendorEnvio.Rows(i).Item("MailCC") & "'"
                End If
                '************************************************************
                '************************************************************

                Detalle = cn.RunSentence(SQLDetalle).Tables(0)
                CaseIDPO = Detalle.Rows(0)("Orden Compra")
                For Each r1 As DataGridViewRow In dtgRequisiciones.Rows
                    If CaseIDPO = r1.Cells("Doc Number").Value Then
                        CaseID = r1.Cells("OEMS_Case_ID").Value.ToString
                        _IsSTR = IIf(Microsoft.VisualBasic.Left(r1.Cells("Material").Value, 1) = "3", True, False)
                        Exit For
                    End If
                Next

                If cn.ExportDataTableToXL(Detalle, PDFPath & "\OpenOrder.xls") Then
                    Attach(0) = PDFPath & "\OpenOrder.xls"
                    cn.Put_HTML_Table_In_ClipBoard(Detalle)
                    lsBody = Replace(Estacionarios.Rows(0).Item("Mensage"), "<@DETALLE>", My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text))
                    lsBody = Replace(lsBody, "<@DETALLEUSUARIO>", My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text))

                    Dim Subject As String = ""
                    Subject = "Case ID:" & CaseID & " PO Number  " & CaseIDPO & "-" & Estacionarios.Rows(0).Item("Asunto")

                    cn.SendOutlookMail(Subject, Attach, Microsoft.VisualBasic.Left(VendorEnvio.Rows(i).Item("MailCC"), 6) & "@PG.COM", "", lsBody, "", False, "HTML", "", False, _IsSTR)
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
        IdVar = Me.cboVariantes.SelectedValue
        SAPBox = Me.cboSAPBox.SelectedValue.ToString
        If Not BGPO.IsBusy Then
            pgbBGW.Style = ProgressBarStyle.Marquee
            BGPO.RunWorkerAsync()
            If cmdShowGR103.Checked Then

            End If
        Else
            MsgBox("Download process is currently working. Please wait until it finish.", MsgBoxStyle.Information)
        End If

        'Download()
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
            If NCNF.Length > 0 Then
                MsgBox("Process finished." & Chr(13) & Chr(13) & "Following items couldn't be confirmed: " & NCNF, MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Process finished." & MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub BGPO_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGPO.DoWork
        Dim CN As New OAConnection.Connection

        Report_Message = "Cleaning temporary files..."
        BGPO.ReportProgress(0)
        CN.ExecuteInServer("Delete From LA_User_Open_Order  Where Usuario = '" & gsUsuarioPC & "'")

        Report_Message = "Getting SAP Connection..."
        BGPO.ReportProgress(0)
        Dim Rep As New SAPCOM.OpenOrders_Report(SAPBox, gsUsuarioPC, AppId)

        Dim Plantas As New DataTable
        Dim Vendors As New DataTable
        Dim PGrp As New DataTable
        Dim POrg As New DataTable
        Dim MatGrp As New DataTable

        Report_Message = "Getting variant values..."
        BGPO.ReportProgress(0)

        Plantas = CN.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & IdVar & ")").Tables(0)
        Vendors = CN.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & IdVar & ")").Tables(0)
        PGrp = CN.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & IdVar & ")").Tables(0)
        POrg = CN.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & IdVar & ")").Tables(0)
        MatGrp = CN.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & IdVar & ")").Tables(0)


        Report_Message = "Including plants..."
        BGPO.ReportProgress(0)

        For Each R As DataRow In Plantas.Rows
            If DBNull.Value.Equals(R("Prefijo")) Then
                Rep.IncludePlant("")
                Rep.IncludePlant(R("Valor"))
            Else
                If R("Prefijo") = "" Then
                    Rep.IncludePlant("")
                    Rep.IncludePlant(R("Valor"))
                Else
                    Rep.ExcludePlant(R("Valor"))
                End If
            End If
        Next

        Report_Message = "Including purch. groups..."
        BGPO.ReportProgress(0)

        For Each R As DataRow In PGrp.Rows
            If DBNull.Value.Equals(R("Prefijo")) Then
                Rep.IncludePurchGroup("")
                Rep.IncludePurchGroup(R("Valor"))
            Else
                If R("Prefijo") = "" Then
                    Rep.IncludePurchGroup("")
                    Rep.IncludePurchGroup(R("Valor"))
                Else
                    Rep.ExcludePurchGroup(R("Valor"))
                End If
            End If
        Next

        Report_Message = "Including purch. organizations..."
        BGPO.ReportProgress(0)

        For Each R As DataRow In POrg.Rows
            If DBNull.Value.Equals(R("Prefijo")) Then
                Rep.IncludePurchOrg("")
                Rep.IncludePurchOrg(R("Valor"))
            Else
                If R("Prefijo") = "" Then
                    Rep.IncludePurchOrg("")
                    Rep.IncludePurchOrg(R("Valor"))
                Else
                    Rep.ExcludePurchOrg(R("Valor"))
                End If
            End If
        Next

        Report_Message = "Including vendors..."
        BGPO.ReportProgress(0)

        For Each R As DataRow In Vendors.Rows
            If DBNull.Value.Equals(R("Prefijo")) Then
                Rep.IncludeVendor("")
                Rep.IncludeVendor(R("Valor"))
            Else
                If R("Prefijo") = "" Then
                    Rep.IncludeVendor("")
                    Rep.IncludeVendor(R("Valor"))
                Else
                    Rep.ExcludeVendor(R("Valor"))
                End If
            End If
        Next

        Report_Message = "Including material groups..."
        BGPO.ReportProgress(0)

        For Each R As DataRow In MatGrp.Rows
            If DBNull.Value.Equals(R("Prefijo")) Then
                Rep.IncludeMatGroup("")
                Rep.IncludeMatGroup(R("Valor"))
            Else
                If R("Prefijo") = "" Then
                    Rep.IncludeMatGroup("")
                    Rep.IncludeMatGroup(R("Valor"))
                Else
                    Rep.ExcludeMatGroup(R("Valor"))
                End If
            End If
        Next

        Rep.RepairsLevel = IncludeRepairs ' Para que incluya las reparaciones
        Rep.Include_GR_IR = True
        Rep.IncludeDelivDates = True 'Para que incluya los delivery dates
        Rep.Include_YO_Ref = True
        Report_Message = "Getting report from SAP, Please wait..."
        BGPO.ReportProgress(0)

        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                POs = Rep.Data

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

                Dim TN As New DataColumn
                Dim SB As New DataColumn

                'Columna del Usuatio que descarga el reporte
                TN.ColumnName = "Usuario"
                TN.Caption = "Usuario"
                TN.DefaultValue = gsUsuarioPC

                'Columna de la caja
                SB.DefaultValue = SAPBox
                SB.ColumnName = "SAPBox"
                SB.Caption = "SAPBox"

                Dim MTP As New DataColumn
                Dim VOA As New DataColumn
                Dim VML As New DataColumn
                Dim SPD As New DataColumn
                Dim RCD As New DataColumn
                Dim SPC As New DataColumn
                Dim LTC As New DataColumn
                Dim LTS As New DataColumn
                Dim RQN As New DataColumn
                Dim RQI As New DataColumn
                Dim POC As New DataColumn
                Dim PDF As New DataColumn
                Dim RCT As New DataColumn 'Root cause description
                Dim CNK As New DataColumn

                POs.Columns.Add(TN)
                POs.Columns.Add(SB)

                POs.Columns.Add(MTP)
                POs.Columns.Add(VOA)
                POs.Columns.Add(VML)
                POs.Columns.Add(SPD)
                POs.Columns.Add(RCD)
                POs.Columns.Add(SPC)
                POs.Columns.Add(LTC)
                POs.Columns.Add(LTS)
                POs.Columns.Add(RQN)
                POs.Columns.Add(RQI)
                POs.Columns.Add(POC)
                'POs.Columns.Add(PDF)
                POs.Columns.Add(RCT)
                POs.Columns.Add(CNK)

                Report_Message = "Saving report..."
                BGPO.ReportProgress(0)

                CN.AppendTableToSqlServer("LA_User_Open_Order", POs)

                Report_Message = "Getting confirmation connection..."
                BGPO.ReportProgress(0)

                Dim EKPO As New SAPCOM.EKPO_Report(SAPBox, gsUsuarioPC, AppId)
                Dim Row As DataRow

                For Each Row In Rep.Documents.Rows
                    EKPO.IncludeDocument(Row.Item("Doc Number"))
                    CN.ExecuteInServer("Update LA_User_Open_Order Set [CNF Key] = (SELECT TOP 1 [Confirmation] FROM [DMS_Confirmation] Where ((PO = " & Row.Item("Doc Number") & ") and (SAPBox) = '" & SAPBox & "')) Where (usuario = '" & gsUsuarioPC & "') and ([Doc Number] = " & Row.Item("Doc Number") & ")")
                    CN.ExecuteInServer("Update LA_User_Open_Order Set [Trans Status] = (SELECT Cast(count([Doc Number]) as Bit) FROM [LA_Transmition_NAST] Where (([Doc Number] = " & Row.Item("Doc Number") & ") And ([Proc Status] = 1) and (SAP = '" & SAPBox & "'))) Where (([Doc Number] = " & Row.Item("Doc Number") & ") and (SAPBox = '" & SAPBox & "') And (usuario = '" & gsUsuarioPC & "'))")
                Next

                EKPO.AddCustomField("LABNR")

                Report_Message = "Geeting SP Confirmation..."
                BGPO.ReportProgress(0)

                EKPO.Execute()

                If EKPO.Success Then
                    If EKPO.ErrMessage = Nothing Then
                        Report_Count = 0
                        Report_Rows = EKPO.Data.Rows.Count
                        For Each R As DataRow In EKPO.Data.Rows
                            If R("LABNR") <> "" Then
                                CN.ExecuteInServer("Update LA_User_Open_Order Set [SP Confirmation] = '" & R("LABNR") & "' Where [Doc Number] = " & R("Doc Number") & " And [Item Number] = " & R("Item Number"))
                            End If
                            Report_Message = "Updating ERSA/HIBE materials..."
                            Report_Count += 1
                            BGPO.ReportProgress(0)
                        Next
                        'CN.AppendTableToSqlServer("tmpSupPortalConfimation", EKPO.Data)
                    Else
                        MsgBox(EKPO.ErrMessage, MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox(EKPO.ErrMessage, MsgBoxStyle.Information)
                End If

                Dim EH As DataTable 'Get Hersa/HIBE materials
                EH = CN.RunSentence("SELECT DISTINCT LA_User_Open_Order.Material, ERSA_HIBE.M_Type " & _
                                    "FROM LA_User_Open_Order LEFT OUTER JOIN ERSA_HIBE ON LA_User_Open_Order.Material = ERSA_HIBE.Material " & _
                                    "WHERE (LA_User_Open_Order.Material <> '') AND (LA_User_Open_Order.Usuario = '" & gsUsuarioPC & "')").Tables(0)

                If EH.Rows.Count > 0 Then
                    Report_Count = 0
                    Report_Rows = EH.Rows.Count
                    For Each dr As DataRow In EH.Rows
                        CN.ExecuteInServer("Update LA_User_Open_Order Set [M_Type] = '" & dr("M_Type") & "' Where [Material] = '" & dr("Material") & "'")
                        Report_Count += 1
                        Report_Message = "Updating material type..."
                        BGPO.ReportProgress(0)
                    Next
                End If

                'Dim cnx As New OAConnection.Connection
                Dim EXP As DataTable 'Vendor OTD
                EXP = CN.RunSentence("SELECT LA_User_Open_Order.Vendor, CASE WHEN Average IS NULL THEN 0 ELSE Average END AS Average, vst_Vendors.VndMail1 AS Mail, " & _
                                     "(CASE WHEN plant.Country = vst_Vendors.country THEN 'National' ELSE 'Import' END) AS Spend " & _
                                     "FROM LA_User_Open_Order LEFT OUTER JOIN " & _
                                     "Plant ON LA_User_Open_Order.Plant = Plant.Code LEFT OUTER JOIN " & _
                                     "vst_Vendors ON LA_User_Open_Order.Vendor = vst_Vendors.Vendor LEFT OUTER JOIN " & _
                                     "HERSA_HIBE_Expedating ON LA_User_Open_Order.Vendor = HERSA_HIBE_Expedating.IDVendor " & _
                                     "Where LA_User_Open_Order.Usuario = '" & gsUsuarioPC & "' " & _
                                     "GROUP BY LA_User_Open_Order.Vendor, CASE WHEN Average IS NULL THEN 0 ELSE Average END, vst_Vendors.VndMail1, " & _
                                     "(CASE WHEN plant.Country = vst_Vendors.country THEN 'National' ELSE 'Import' END)").Tables(0)

                Report_Count = 0
                Report_Rows = EXP.Rows.Count
                For Each r As DataRow In EXP.Rows
                    CN.ExecuteInServer("Update LA_User_Open_Order Set [Vendor OTD Average] = '" & r("Average") & "', [Vendor Mail] = '" & r("Mail") & "', Spend = '" & r("Spend") & "' Where [Vendor] = " & r("Vendor"))
                    Report_Count += 1
                    Report_Message = "Updating comments & mails..."
                    BGPO.ReportProgress(0)
                Next


                Dim CRC As DataTable 'Comment & root causes
                CRC = CN.RunSentence("SELECT LA_User_Open_Order.[Doc Number], LA_User_Open_Order.[Item Number], LA_User_Open_Order.SAPBox, RootCauses_PO.RootCause, " & _
                                     "(SELECT TOP 1 CAST(Comentario AS varchar(3000)) AS Expr1 " & _
                                     "FROM dbo.Comentarios_PO AS B " & _
                                     "WHERE (PurchOrder = LA_User_Open_Order.[Doc Number]) AND (Item = LA_User_Open_Order.[Item Number]) AND " & _
                                     "(SAPBox = LA_User_Open_Order.SAPBox) " & _
                                     "ORDER BY ID DESC) AS Comment, " & _
                                     "(SELECT TOP 1 CAST(Status AS varchar(250)) AS Expr1 " & _
                                     "FROM dbo.Comentarios_PO AS B " & _
                                     "WHERE (PurchOrder = dbo.LA_User_Open_Order.[Doc Number]) AND (Item = dbo.LA_User_Open_Order.[Item Number]) AND  " & _
                                     "(SAPBox = dbo.LA_User_Open_Order.SAPBox) " & _
                                     "ORDER BY ID DESC) AS Status, " & _
                                     "(SELECT TOP 1 Fecha AS Expr1 FROM dbo.Comentarios_PO AS B WHERE (PurchOrder = dbo.LA_User_Open_Order.[Doc Number]) AND (Item = dbo.LA_User_Open_Order.[Item Number]) AND (SAPBox = dbo.LA_User_Open_Order.SAPBox) ORDER BY ID DESC) as Comment_Date " & _
                                     "FROM dbo.LA_User_Open_Order LEFT OUTER JOIN " & _
                                     "Comentarios_PO ON dbo.LA_User_Open_Order.[Item Number] = dbo.Comentarios_PO.Item AND  " & _
                                     "LA_User_Open_Order.[Doc Number] = dbo.Comentarios_PO.PurchOrder AND dbo.LA_User_Open_Order.SAPBox = dbo.Comentarios_PO.SAPBox LEFT OUTER JOIN " & _
                                     "RootCauses_PO ON dbo.LA_User_Open_Order.[Doc Number] = dbo.RootCauses_PO.PurchOrder AND  " & _
                                     "LA_User_Open_Order.[Item Number] = dbo.RootCauses_PO.Item AND dbo.LA_User_Open_Order.SAPBox = dbo.RootCauses_PO.SAPBox " & _
                                     "WHERE dbo.LA_User_Open_Order.Usuario = '" & gsUsuarioPC & "' " & _
                                     "GROUP BY dbo.LA_User_Open_Order.[Doc Number], dbo.LA_User_Open_Order.[Item Number], dbo.LA_User_Open_Order.SAPBox, dbo.RootCauses_PO.RootCause").Tables(0)
                Report_Count = 0
                Report_Rows = CRC.Rows.Count
                For Each r As DataRow In CRC.Rows
                    If Not DBNull.Value.Equals(r("Rootcause")) Or Not DBNull.Value.Equals(r("Comment")) Or Not DBNull.Value.Equals(r("Status")) Then
                        CN.ExecuteInServer("Update LA_User_Open_Order Set [RC] = " & IIf(DBNull.Value.Equals(r("RootCause")), 18, r("RootCause")) & ", [Last Comments] = '[" & r("Comment_Date") & "]" & r("Comment") & "', [Last Status] = '" & r("Status") & "' Where [Doc Number] = " & r("Doc Number") & " And [Item Number] = " & r("Item Number"))
                    End If
                    Report_Count += 1
                    Report_Message = "Updating root causes..."
                    BGPO.ReportProgress(0)
                Next

                'Update Root cause description:
                CN.ExecuteInServer("Update LA_User_Open_Order Set [Root Cause Description] = RootCauses.Descripcion From RootCauses Where LA_User_Open_Order.RC = RootCauses.[ID]")
                CN.ExecuteStoredProcedure("stp_Update_OEMS_Case_ID")

            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            End If
        Else
            MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub BGPO_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BGPO.ProgressChanged
        Me.lblReport.Text = Report_Message
        pbProgress.Maximum = Report_Rows
        pbProgress.Value = Report_Count
    End Sub
    Private Sub BGPO_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGPO.RunWorkerCompleted
        pgbBGW.Style = ProgressBarStyle.Blocks
        pbProgress.Value = 0
        lblReport.Text = ""
        LoadInfo("")
    End Sub
    Private Sub PrintingProcess()
        ''+++++++++++++++++++++++++++++++++++++++++++++++++++
        ''Codigo Original: 2012-03-01
        Me.dtgRequisiciones.EndEdit()

        If System.IO.Directory.GetFiles(PDFPath, "*.pdf").Count > 0 Then
            If MsgBox("PDF files were found in folder: " & PDFPath & Chr(13) & "Files found: " & System.IO.Directory.GetFiles(PDFPath, "*.pdf").Count & "." & Chr(13) & Chr(13) & "Do you want to move those files to: " & Replace(PDFPath, PDFPath, PDFPath & "\BackUp ?") & Chr(13) & Chr(13) & "Yes, move files and start printing." & Chr(13) & "No, Don't move or print, I'll fix it by myself.", MsgBoxStyle.Question & MsgBoxStyle.YesNo, "PDF files found!") = MsgBoxResult.No Then
                Exit Sub
            Else
                Dim PDFList As String() = System.IO.Directory.GetFiles(PDFPath)
                For Each f As String In PDFList
                    My.Computer.FileSystem.MoveFile(f, Replace(f, PDFPath, PDFPath & "\BackUp"), True)
                    System.IO.File.Delete(f)
                Next
            End If
        End If

        Dim SAP As New SAPConection.c_SAP(Me.cboSAPBox.Text)
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Microsoft.VisualBasic.Left(Me.cboSAPBox.SelectedValue.ToString, 3), gsUsuarioPC, "LAT")
        SAP.UserName = gsUsuarioPC
        SAP.Password = u.password
        SAP.OpenConnection(SAPConfig)

        PO_Print = New SAPConection.POPrinting(SAP.GUI)

        PO_Print.AllowPrintAle = Me.chkAle.Checked
        PO_Print.SAPBox = Microsoft.VisualBasic.Left(Me.cboSAPBox.SelectedValue.ToString, 3)
        PO_Print.DirectoryPath = PDFPath

        PO_Print.UseFreePDF4 = FreePDF4
        PO_Print.TimeOut = PDFTimeOut

        For Each r As Windows.Forms.DataGridViewRow In Me.dtgRequisiciones.Rows
            If r.Cells("CK").Value Then
                If chkNA.Checked Then
                    If r.Cells("Purch Org").Value = "1257" Then
                        PO_Print.IncludeDocument(r.Cells("Doc Number").Value, SAPConection.Printingtype.NEXX, PO_Print.GetLanguage(Me.cboIdioma.SelectedValue.ToString))
                    Else
                        PO_Print.IncludeDocument(r.Cells("Doc Number").Value, SAPConection.Printingtype.NNXX, PO_Print.GetLanguage(Me.cboIdioma.SelectedValue.ToString))
                    End If
                Else
                    PO_Print.IncludeDocument(r.Cells("Doc Number").Value, SAPConection.Printingtype.NEU, PO_Print.GetLanguage(Me.cboIdioma.SelectedValue.ToString))
                End If

                'Changed for new Global PO Layout
                'PO_Print.IncludeDocument(r.Cells("Doc Number").Value, SAPConection.Printingtype.ZGBL, PO_Print.GetLanguage(Me.cboIdioma.SelectedValue.ToString))
            End If
        Next

        PO_Print.Excecute()
        SAP.CloseConnection()

        For Each r As Windows.Forms.DataGridViewRow In Me.dtgRequisiciones.Rows
            Try
                If Not DBNull.Value.Equals(r.Cells("Ck").Value) AndAlso r.Cells("CK").Value Then
                    r.Cells("PDF File").Value = PO_Print.GetFilePath(r.Cells("Doc Number").Value)
                    If Not PO_Print.DocumentPrinted(r.Cells("Doc Number").Value) Then
                        'r.DefaultCellStyle.BackColor = Drawing.Color.LightSalmon
                        r.Cells("CK").Style.BackColor = Drawing.Color.LightSalmon
                        r.Cells("CK").Value = False
                        'Else
                        '    r.DefaultCellStyle.BackColor = Drawing.Color.White
                    End If
                End If
            Catch ex As Exception
                'Do nothing
            End Try
        Next

    End Sub
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        PrintingProcess()
    End Sub
    Private Sub SendPDFFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendPDFFileToolStripMenuItem.Click
        Dim cn As New OAConnection.Connection
        Dim Mail As New DataTable
        Dim Sites As New DataTable
        Dim i As Integer = 0
        Dim Orden As String = ""
        Dim Attach As String()
        Dim _IsSTR As Boolean = False

        'Get LA Sites:
        Sites = cn.RunSentence("Select Plant_Code, Plant_Name From SC_Plant").Tables(0)

        '*******************************************
        'Verifico que se seleccione un estacionario:
        '*******************************************
        If Me.cboEstacionarios.SelectedValue <> Nothing Then
            If Me.cboEstacionarios.SelectedValue.ToString = "System.Data.DataRowView" Then
                MsgBox("Please select a stationary...", MsgBoxStyle.Exclamation, "Stationary not selected")
                Exit Sub
            End If
        Else
            MsgBox("Please select a stationary...", MsgBoxStyle.Exclamation, "Stationary not selected")
            Exit Sub
        End If

        Me.dtgRequisiciones.EndEdit()
        ReDim Attach(1)

        '***************************************
        'Obtengo la estructura del estacionario:
        '***************************************
        Mail = cn.RunSentence("Select * From Estacionarios Where IdEstacionario = " & Me.cboEstacionarios.SelectedValue.ToString).Tables(0)

        If Mail.Rows.Count > 0 Then
            For i = 0 To Me.dtgRequisiciones.RowCount - 1
                If Me.dtgRequisiciones.Rows(i).Cells("CK").Value Then
                    If IO.File.Exists(Me.dtgRequisiciones.Rows(i).Cells("PDF File").Value) Then
                        Attach(0) = Me.dtgRequisiciones.Rows(i).Cells("PDF File").Value
                        
                        _IsSTR = IIf(Microsoft.VisualBasic.Left(Me.dtgRequisiciones.Rows(i).Cells("Material").Value, 1) = "3", True, False)
                        Dim lsBody As String = Mail.Rows(0).Item("Mensage")
                        If lsBody.IndexOf("<@NATag>") > -1 Then
                            lsBody = Replace(lsBody, "<@NATag>", "PO Number: " & Me.cboSAPBox.SelectedValue.ToString & "-" & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value)
                            lsBody = Replace(lsBody, "<@NATagVendor>", Me.dtgRequisiciones.Rows(i).Cells("Vendor Name").Value)
                        End If

                        Dim LQ
                        Dim MailSubject As String = ""

                        Try
                            LQ = (From C In Sites _
                                  Where C("Plant_Code") = Me.dtgRequisiciones.Rows(i).Cells("Plant").Value _
                                  Select (C("Plant_Name"))).Single()
                        Catch ex As Exception

                        End Try

                        If Not DBNull.Value.Equals(LQ) AndAlso Not LQ Is Nothing Then
                            MailSubject = "Case ID:" & Me.dtgRequisiciones.Rows(i).Cells("OEMS_Case_ID").Value & " PO Number  " & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value & "-" & Mail.Rows(0).Item("Asunto") & " - [" & LQ & "] "
                        Else

                            MailSubject = "Case ID:" & Me.dtgRequisiciones.Rows(i).Cells("OEMS_Case_ID").Value & " PO Number  " & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value & "-" & Mail.Rows(0).Item("Asunto")
                        End If

                        If cn.SendOutlookMail(MailSubject, Attach, Me.dtgRequisiciones.Rows(i).Cells("Vendor Mail").Value, "", lsBody, "", False, "HTML", "", False, _IsSTR) Then
                            cn.ExecuteInServer("Insert Into HistorySendPO(PO, SendDate, Usuario, SAPBox, Send,[Upload Date],[Region], [Site]) Values(" & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value & ",{fn now()},'" & gsUsuarioPC & "','" & Me.cboSAPBox.SelectedValue.ToString & "',1,'" & Me.dtgRequisiciones.Rows(i).Cells("Doc Date").Value & "','" & IIf(chkNA.Checked, "NA", "LA") & "','" & Me.dtgRequisiciones.Rows(i).Cells("Plant").Value & "')")
                        End If
                    Else
                        Me.dtgRequisiciones.Rows(i).Cells("PDF File").Value = "Error: File not found."
                    End If
                End If
            Next
            MsgBox("Mails was succesully created .", MsgBoxStyle.Information, "Mail Creation")
        Else
            MsgBox("Stationary not found.")
        End If
    End Sub
End Class




