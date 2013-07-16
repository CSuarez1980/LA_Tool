Imports SAPCOM.RepairsLevels
Imports System.Windows.Forms
Imports System.Drawing

Public Class frm024
    Dim cn As New OAConnection.Connection
    Dim Comentarios As New DataTable
    Dim Requisiciones As New DataTable
    Dim Req_Request As New List(Of Requisition)
    Dim IDVariante As Double = 0
    Public CKSap As String = ""

    Private Sub frm024_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        GuardarConfiguracion()
    End Sub
    Private Sub frm024_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
        cn.LoadCombo(Me.cboIdioma, "Select IDIdioma,Descripcion From Idioma", "IDIdioma", "Descripcion")
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")

        Requisiciones = cn.RunSentence("SELECT [Req Number], [Item Number], [Mat Group], Material, [Short Text], [Purch Grp], Plant, [Tracking Field], [Doc Type], [Item Category], [PO Quantity]," & _
                                       "[Fixed vendor], [Fix Vendor Name], [Desired Vendor], [Des Vendor Name], [Purch Org], [Outline Agreement], [Agreement Item], Rquisitioner, [Req Date], [PO Date], " & _
                                       "[MRP Controller], Storage, [Del Indicator], [PO Number], [PO Item], [Created by], [Delivery Date], UOM, Quantity, [Release Date], Repair, SAPBox " & _
                                       "FROM tmpOpenRequis " & _
                                       "Where [User] = 'x'").Tables(0)
    End Sub
    Private Sub ChekRequest()
        Dim r As New DataTable
        Dim f As Boolean = False
        Dim row As DataRow
        Dim RD As Date
        Dim c As Integer = 0
        r = cn.RunSentence("Select Req, Item, SAP, TNumber, Request From vst_Cotizacion_Envio Where Request is not Null And TNumber = '" & gsUsuarioPC & "'").Tables(0)

        Req_Request.Clear()

        If r.Rows.Count > 0 Then
            For Each row In r.Rows
                RD = row("Request")
                RD = RD.AddDays(QDays)

                If RD.Date <= Today Then
                    cmdRequis.Visible = True
                    f = True
                    c += 1

                    Req_Request.Add(New Requisition(row("Req"), row("Item"), row("SAP")))
                End If
            Next
        End If

        If c > 0 Then
            MsgBox("You have: " & c & " requisitions without confirmation.", MsgBoxStyle.Information)
        Else
            cmdRequis.Visible = False
        End If
    End Sub
    Private Sub cmdDowload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDowload.Click
        Me.tlbHerramientas.Enabled = False
        'Dim c As New SAPCOM.SAPConnector
        Dim i As Integer

        'Dim Rep As New SAPCOM.OpenReqs_Report("L7A", "BM4691")
        Dim Rep As New SAPCOM.OpenReqs_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)

        CKSap = Me.cboSAPBox.Text

        bgCheckSAP.RunWorkerAsync()

        Dim Plantas As New DataTable
        Dim Vendors As New DataTable
        Dim PGrp As New DataTable
        Dim POrg As New DataTable
        Dim MatGrp As New DataTable

        Me.dtgRequisiciones.Columns.Clear()

        '**********************************
        'Bloqueo de combos, esto es para que
        'al momento de guardar los comentarios
        'no coloquen otra caja y la requi no
        'pierda el comentario:
        Me.cboSAPBox.Enabled = False
        Me.cboVariantes.Enabled = False
        '**********************************

        IDVariante = Me.cboVariantes.SelectedValue.ToString

        Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        MatGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)

        If Plantas.Rows.Count > 0 Then
            For i = 0 To Plantas.Rows.Count - 1
                If DBNull.Value.Equals(Plantas.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePlant("")
                    Rep.IncludePlant(Plantas.Rows(i).Item("Valor"))
                Else
                    If Plantas.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePlant("")
                        Rep.IncludePlant(Plantas.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludePlant(Plantas.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        If PGrp.Rows.Count > 0 Then
            For i = 0 To PGrp.Rows.Count - 1
                If DBNull.Value.Equals(PGrp.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePurchGroup("")
                    Rep.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
                Else
                    If PGrp.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePurchGroup("")
                        Rep.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludePurchGroup(PGrp.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        If POrg.Rows.Count > 0 Then
            For i = 0 To POrg.Rows.Count - 1
                If DBNull.Value.Equals(POrg.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePurchOrg("")
                    Rep.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
                Else
                    If POrg.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePurchOrg("")
                        Rep.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludePurchOrg(POrg.Rows(i).Item("Valor"))
                    End If

                End If
            Next
        End If

        If MatGrp.Rows.Count > 0 Then
            For i = 0 To MatGrp.Rows.Count - 1
                If DBNull.Value.Equals(MatGrp.Rows(i).Item("Prefijo")) Then
                    Rep.IncludeMatGroup("")
                    Rep.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                Else
                    If MatGrp.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludeMatGroup("")
                        Rep.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        Rep.RepairsLevel = IncludeRepairs
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then

                Requisiciones = Rep.Data

                'Requisiciones.Columns.Add("User")
                'Requisiciones.Columns.Add("SAPBox")

                If Not cmdReeplazar.Checked Then
                    cn.ExecuteInServer("Delete From tmpOpenRequis Where [User] = '" & gsUsuarioPC & "'")
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

                Requisiciones.Columns.Add(TN)
                Requisiciones.Columns.Add(SB)

                'Dim j As Integer
                'For j = 0 To Requisiciones.Rows.Count - 1
                '    Requisiciones.Rows(j).Item("User") = gsUsuarioPC
                '    Requisiciones.Rows(j).Item("SAPBox") = Me.cboSAPBox.SelectedValue.ToString
                'Next

                cn.AppendTableToSqlServer("tmpOpenRequis", Requisiciones)
                CargarInfo()
            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Exclamation)
            End If

        Else
            MsgBox(Rep.ErrMessage, MsgBoxStyle.Exclamation)
        End If
        'c = Nothing

        Me.tlbHerramientas.Enabled = True
    End Sub
    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuario & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")

            If Not Me.cboVariantes.SelectedValue.ToString = "System.Data.DataRowView" Then
                IDVariante = Me.cboVariantes.SelectedValue.ToString
            End If
        End If
    End Sub
    Private Sub dtgRequisiciones_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellContentClick
        Dim curRow As Integer = 0
        Dim curCol As Integer = 0

        curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
        curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

        Select Case Me.dtgRequisiciones.Columns(e.ColumnIndex).HeaderText.ToUpper
            Case "AUTOPO"
                'Verifico si la requi corre en automatico o no:
                Dim Requi As New OARequis.Requisition
                Requi.TestRequisition(Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value, Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value)

            Case "COMMENTS"
                'Creo la ventana standard para mostrar los comentarios de las requis:
                Dim form As New frm027

                Comentarios = cn.RunSentence("Select * From Comentarios_Requis Where Requisicion = " & Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value & " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & " And SAPBox = '" & Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value & "'").Tables(0)
                If Comentarios.Rows.Count > 0 Then
                    form.dtgComentarios.DataSource = Comentarios
                    form.lblDocumento.Text = "Purch Doc"
                    form.txtRequisicion.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value
                    form.txtReqItem.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value
                    form.txtGica.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Material").Value
                    form.txtMaterial.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Short Text").Value
                    form.txtSAPBox.Text = Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value
                    form.ShowDialog()
                Else
                    MsgBox("Esta requisición no tiene comentarios", MsgBoxStyle.Information)
                End If

            Case "QUOTATION"
                Dim Form As New frm060

                Form.txtRequisition.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value
                Form.txtItem.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value
                Form.txtGica.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Material").Value
                Form.txtDescription.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Short Text").Value
                Form.txtManufacter.Text = IIf(Not DBNull.Value.Equals(Me.dtgRequisiciones.Rows(curRow).Cells("Manufacter").Value), Me.dtgRequisiciones.Rows(curRow).Cells("Manufacter").Value, "")
                Form.txtQuantity.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Quantity").Value
                Form.txtPartNumber.Text = IIf(Not DBNull.Value.Equals(Me.dtgRequisiciones.Rows(curRow).Cells("PartNumber").Value), Me.dtgRequisiciones.Rows(curRow).Cells("PartNumber").Value, "")
                Form.txtSAPBox.Text = Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value
                Form.OEMS_Case_ID = Me.dtgRequisiciones.Rows(curRow).Cells("OEMS_Case_ID").Value
                Form.ShowDialog()
        End Select
    End Sub
    Private Sub cmdComentarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComentarios.Click
        If dtgRequisiciones.RowCount > 0 Then '<- Verifico que el datagrid contenga información
            '' ''Dim cn As New OAConnection.Connection
            '' ''Dim Form As New frm026
            '' ''Dim curRow As Integer = 0
            '' ''Dim curCol As Integer = 0

            '' ''curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
            '' ''curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

            '' ''Form.txtRequisicion.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value
            '' ''Form.txtReqItem.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value
            '' ''Form.txtGica.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Material").Value
            '' ''Form.txtMaterial.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Short Text").Value
            '' ''Form.txtSAPBox.Text = Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value
            '' ''Form.txtPlanta.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Plant").Value
            '' ''Form.txtMatGrp.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Mat Group").Value
            '' ''Form.txtPGrp.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Purch Grp").Value
            '' ''Form.txtPOrg.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Purch Org").Value
            '' ''Form.ShowDialog()

            '' ''If Form.Estado Then
            '' ''    VerificarComentariosYRootCauses()

            '' ''    If Form.cmdCopyComment.Checked Then
            '' ''        Dim drRequisition As System.Windows.Forms.DataGridViewRow
            '' ''        Dim lsComentario As String = ""
            '' ''        lsComentario = Replace(Replace(Form.txtComentarios.Text, "'", ""), ",", "")

            '' ''        For Each drRequisition In Me.dtgRequisiciones.Rows
            '' ''            If drRequisition.Cells("Req Number").Value = Form.txtRequisicion.Text Then
            '' ''                cn.ExecuteInServer("Insert Into Comentarios_Requis(Requisicion,Item,Comentario,Fecha,Usuario,SAPBox,Status,Planta,MatGrp,PGrp,POrg) Values(" & Form.txtRequisicion.Text & "," & drRequisition.Cells("Item Number").Value & ",'" & lsComentario & "', {fn now()}, '" & cn.GetUserId & "','" & Form.txtSAPBox.Text & "','" & Form.txtStatus.Text & "','" & Form.txtPlanta.Text & "','" & Form.txtMatGrp.Text & "','" & Form.txtPGrp.Text & "','" & Form.txtPOrg.Text & "')")
            '' ''            End If
            '' ''        Next
            '' ''    End If
            '' ''End If

            '' ''Form.Close()


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
                            cn.ExecuteInServer("Insert Into Comentarios_Requis(Requisicion,Item,Comentario,Fecha,Usuario,SAPBox,Status,Planta,MatGrp,PGrp,POrg) Values(" & .Cells("Req Number").Value & "," & .Cells("Item Number").Value & ",'" & lsComentario & "', {fn now()}, '" & gsUsuarioPC & "','" & .Cells("SAPBox").Value & "','" & Form.txtStatus.Text & "','" & .Cells("Plant").Value & "','" & .Cells("Mat Group").Value & "','" & .Cells("Purch Grp").Value & "','" & .Cells("Purch Org").Value & "')")
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
        '**********************************************************************************************
        '**********************************************************************************************
        '********************* TEST PARA LA VELOCIDAD DE DESCARGA DE LOS REPORTES *********************
        '**********************************************************************************************
        '**********************************************************************************************
        Dim Tabla As New DataTable
        Dim i, j As Integer
        Dim Res As Boolean = False

        Dim Requi As New OARequis.Requisition
        Dim ResReq As String = ""
        Dim dtRCCM As New DataTable ' -> datatable con la información de los RootCauses y Comments

        dtRCCM = cn.RunSentence("Select * From [vstRootCauseAndCommentsReq] Where [User] = '" & gsUsuarioPC & "'").Tables(0)

        For i = 0 To Me.dtgRequisiciones.RowCount - 1

            If Me.dtgRequisiciones.Rows(i).Cells("SPS TNumber").Value <> "" Then
                Me.dtgRequisiciones.Rows(i).Cells("SPS").Value = Me.dtgRequisiciones.Rows(i).Cells("SPS TNumber").Value
            End If

            If Me.dtgRequisiciones.Rows(i).Cells("Responsible ID").Value <> 0 Then
                Me.dtgRequisiciones.Rows(i).Cells("Responsible").Value = Me.dtgRequisiciones.Rows(i).Cells("Responsible ID").Value
            Else
                Me.dtgRequisiciones.Rows(i).Cells("Responsible").Value = Decimal.Parse("1")
            End If

            If dtRCCM.Rows.Count > 0 Then
                Dim vstdn As String '-> Req. Number from View
                Dim grddn As String '-> Req. Number from DataGrid
                Dim vstdi As String '-> Req. Item from View
                Dim grddi As String '-> Req. Item from DataGrid
                Dim vstbx As String '-> SAPBox From View
                Dim grdbx As String '-> SAPBox from DataGrid

                For j = 0 To dtRCCM.Rows.Count - 1
                    vstdn = ""
                    grddn = ""
                    vstdi = ""
                    grddi = ""
                    vstbx = ""
                    grdbx = ""

                    vstdn = dtRCCM.Rows(j).Item("Req Number")
                    grddn = Me.dtgRequisiciones.Rows(i).Cells("Req Number").Value
                    vstdi = dtRCCM.Rows(j).Item("Item Number")
                    grddi = Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value
                    vstbx = dtRCCM.Rows(j).Item("SAPBox")
                    grdbx = Me.dtgRequisiciones.Rows(i).Cells("SAPBox").Value

                    If (vstdn = grddn) And (vstdi = grddi) And (vstbx = grdbx) Then
                        If Not DBNull.Value.Equals(dtRCCM.Rows(j).Item("Comentario")) Then
                            Me.dtgRequisiciones.Rows(i).Cells("Comments").Value = "≈≈≈"
                            Me.dtgRequisiciones.Rows(i).Cells("Comments").ToolTipText = dtRCCM.Rows(j).Item("Comentario")
                            Me.dtgRequisiciones.Rows(i).Cells("Status").Value = dtRCCM.Rows(j).Item("Status")
                        End If

                        If Not DBNull.Value.Equals(dtRCCM.Rows(j).Item("RootCause")) Then
                            Me.dtgRequisiciones.Rows(i).Cells("RootCauses").Value = dtRCCM.Rows(j).Item("RootCause")
                        End If
                    End If
                Next
            End If

            'Verifico si es gicado y corre en Automatico:
            If Me.dtgRequisiciones.Rows(i).Cells("Material").Value.Trim.Length > 0 Then

                ResReq = ""
                ResReq = Requi.ReturnStatus(Me.dtgRequisiciones.Rows(i).Cells("Req Number").Value, Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value, True)

                If ResReq = "-1" Then
                    Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.BackColor = Drawing.Color.Green
                    Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
                    Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Value = "Ok"
                Else
                    If ResReq.IndexOf("1") <> -1 Then
                        Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.BackColor = Drawing.Color.Red
                        Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
                        Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Value = "No OA"
                    Else
                        Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.BackColor = Drawing.Color.Red
                        Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
                        Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Value = "Revisar"
                    End If
                End If
            End If
            'Validación del aging de la requi:
            Me.dtgRequisiciones.Rows(i).Cells("Aging").Value = (Now.Date - Date.Parse(Me.dtgRequisiciones.Rows(i).Cells("Release Date").Value)).Days

            If Not DBNull.Value.Equals(dtgRequisiciones.Rows(i).Cells("Q_Qty").Value) Then
                Me.dtgRequisiciones.Rows(i).Cells("Quotation").Value = "Quoting(" & Me.dtgRequisiciones.Rows(i).Cells("Q_Qty").Value & ")"
                Me.dtgRequisiciones.Rows(i).Cells("Quotation").Style.BackColor = Drawing.Color.SpringGreen
            Else
                Me.dtgRequisiciones.Rows(i).Cells("Quotation").Value = "Quote"
                Me.dtgRequisiciones.Rows(i).Cells("Quotation").Style.BackColor = Drawing.Color.AliceBlue
            End If

            If Me.dtgRequisiciones.Rows(i).Cells("IR Only").Value = True Then
                Me.dtgRequisiciones.Rows(i).DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(157, 223, 165)
            End If
        Next

        ChekRequest()

        'Dim IR As Boolean = True
        'Dim IROnlyRequi = (From C In Requisiciones.AsEnumerable() _
        '                      Where C.Item("IR Only") = IR _
        '                      Select C.Item("Desired Vendor")).Count

     
        Dim IROnlyRequi = (From C In Requisiciones Where C("IR Only") = True).Count

        If IROnlyRequi > 0 Then
            Dim f As New frmMessage
            f.Message = "Requisitions(" & IROnlyRequi & ") with IR Only vendor were found. Please be sure to create purchase order with IR Only checked."
            f.ShowDialog()
        End If
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
            cn.ExecuteInServer("Update Config_Forms Set Orden = " & Me.dtgRequisiciones.Columns(i).DisplayIndex & " Where Usuario = '" & cn.GetUserId & "' And NombreFormulario = 'frm024' And Columna = '" & Me.dtgRequisiciones.Columns(i).HeaderText & "'")
        Next
    End Sub
    Private Sub dtgRequisiciones_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellEndEdit
        Me.dtgRequisiciones.EndEdit()
        Dim curRow As Integer = 0
        Dim curCol As Integer = 0

        curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
        curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

        Select Case Me.dtgRequisiciones.Columns(e.ColumnIndex).HeaderText.ToUpper
            Case "ROOTCAUSES"
                Dim Requi As New DataTable

                'Verifico si la requi ya tiene Root Cause:
                Requi = cn.RunSentence("Select RootCauses.Descripcion From RootCauses,RootCauses_Requis " & _
                                       "Where RootCauses.ID = RootCauses_Requis.RootCause " & _
                                       " And Requisicion = " & Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value & _
                                       " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & _
                                       " And SAPBox = '" & Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value & "'").Tables(0)

                If Requi.Rows.Count = 0 Then
                    'Si la Requi no tiene RootCause le agrego:
                    If Not Me.dtgRequisiciones.Rows(curRow).Cells("RootCauses").Value = Nothing Then
                        cn.ExecuteInServer("Insert Into RootCauses_Requis Values(" & _
                                                               Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value & "," & _
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
                    'Si la requi ya Root Cause la actualizo:
                    cn.ExecuteInServer("Update RootCauses_Requis set RootCause = " & Me.dtgRequisiciones.Rows(curRow).Cells("RootCauses").Value & _
                                       " Where Requisicion = " & Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value & _
                                       " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & _
                                       " And SAPBox = '" & Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value & "'")

                End If

            Case "SPS", "RESPONSIBLE", "OEMS_CASE_ID"
                Dim SAP As New Data.SqlClient.SqlParameter("@SAPBox", Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value)
                Dim PR As New Data.SqlClient.SqlParameter("@PR", Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value)
                Dim Item As New Data.SqlClient.SqlParameter("@Item", Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value)
                Dim SPS As New Data.SqlClient.SqlParameter("@SPS", IIf(Me.dtgRequisiciones.Rows(curRow).Cells("SPS").Value = Nothing, "", Me.dtgRequisiciones.Rows(curRow).Cells("SPS").Value))
                Dim Res As New Data.SqlClient.SqlParameter("@Responsible", IIf(Me.dtgRequisiciones.Rows(curRow).Cells("Responsible").Value = Nothing, 0, Me.dtgRequisiciones.Rows(curRow).Cells("Responsible").Value))
                Dim CaseID As New Data.SqlClient.SqlParameter("@CaseID", IIf(Me.dtgRequisiciones.Rows(curRow).Cells("OEMS_Case_ID").Value = Nothing, 0, Me.dtgRequisiciones.Rows(curRow).Cells("OEMS_Case_ID").Value))

                Dim PG As New List(Of Data.SqlClient.SqlParameter)
                PG.Add(SAP)
                PG.Add(PR)
                PG.Add(Item)
                PG.Add(SPS)
                PG.Add(Res)
                PG.Add(CaseID)
                cn.ExecuteStoredProcedure("stp_Update_PR_SPS", PG)
        End Select
    End Sub
    Private Sub dtgRequisiciones_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellEnter
        Me.txtRow.Text = Me.dtgRequisiciones.CurrentRow.Index + 1
    End Sub
    Private Sub dtgRequisiciones_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dtgRequisiciones.ColumnHeaderMouseClick
        VerificarComentariosYRootCauses()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
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
    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        'If cn.ExportDataGridToXL(Me.dtgRequisiciones) Then
        '    MsgBox("Done!")
        'End If
    End Sub
    Private Sub cmdReeplazar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReeplazar.Click
        Me.cmdReeplazar.Checked = Not Me.cmdReeplazar.Checked
    End Sub
    Private Sub CargarInfo()
        '' '' '' Comentado el 11 de Mayo para hacer pruebas con la columna de catálogo
        '' '' ''************************************************************************************************
        '' '' ''************************************************************************************************
        '' '' ''vstTmpOpenRequis: Codigo tomado de la vista en el server
        '' '' ''Esto es para tener en el servidor las requis que se bajaron tanto para agregarle otras cajas
        '' '' ''como para la creación de ordenes de compra.
        ' '' ''Dim SQLRequis As String = "SELECT [Req Number], [Item Number], [Mat Group], Material, [Short Text], [Purch Grp], Plant, [Tracking Field], [Doc Type], [Item Category], [PO Quantity]," & _
        ' '' ''                                 "[Fixed vendor], [Fix Vendor Name], [Desired Vendor], [Des Vendor Name], [Purch Org], [Outline Agreement], [Agreement Item], Rquisitioner, [Req Date], [PO Date], " & _
        ' '' ''                                 "[MRP Controller], Storage, [Del Indicator], [PO Number], [PO Item], [Created by], [Delivery Date], UOM, Quantity, [Release Date], Repair, SAPBox " & _
        ' '' ''                          "FROM tmpOpenRequis " & _
        ' '' ''                          "Where [User] = '" & gsUsuarioPC & "'"

        Dim SQLRequis As String = "SELECT * FROM vstTmpOpenRequis Where [User] = '" & gsUsuarioPC & "'"

        '' '' ''************************************************************************************************
        '' '' ''************************************************************************************************
        Me.dtgRequisiciones.Columns.Clear()
        Requisiciones = cn.RunSentence(SQLRequis).Tables(0)
        Requisiciones.Columns.Add("Aging", System.Type.GetType("System.Decimal"))

        Me.dtgRequisiciones.DataSource = Requisiciones

        Me.dtgRequisiciones.Columns.Insert(0, cn.AddComboRootCauses("RootCauses", "RootCauses", "Select ID, Descripcion From RootCauses Where Tipo = 1 Order by Descripcion", "ID", "Descripcion"))

        'Solitud de Maria Perez: Mail PR to PO / Trigger Standardization Reports
        Me.dtgRequisiciones.Columns.Insert(0, cn.AddComboRootCauses("SPS", "SPS", "Select TNumber, Nombre From [Users] Where Show_In_List = 1", "TNumber", "Nombre"))
        Me.dtgRequisiciones.Columns.Insert(0, cn.AddComboRootCauses("Responsible", "Responsible", "Select ID, Description From [GCT_PR_Responsible_List]", "ID", "Description"))

        Me.dtgRequisiciones.Columns.Insert(0, cn.AddButtonColumn("AutoPO", "AutoPO", 25))
        Me.dtgRequisiciones.Columns.Insert(0, cn.AddButtonColumn("COMMENTS", "COMMENTS", 25))
        Me.dtgRequisiciones.Columns.Insert(0, cn.AddCheckBoxColumn("Ck", "Ck"))
        Me.dtgRequisiciones.Columns.Insert(0, cn.AddButtonColumn("Quotation", "Quotation", 25))
        Me.dtgRequisiciones.Columns.Add("Status", "Status")

        Me.txtTotalReq.Text = Requisiciones.Rows.Count

        If Requisiciones.Rows.Count > 0 Then
            VerificarComentariosYRootCauses()
            CargarConfiguracion()
        End If
    End Sub
    ''' <summary>
    ''' Carga la info con un parámetro de búsqueda
    ''' </summary>
    ''' <param name="Filtro">Filtro para la carga de la información</param>
    ''' <remarks></remarks>
    Private Sub CargarInfo(ByVal Filtro As String)
        If Filtro.Length > 0 Then
            '' '' ''************************************************************************************************
            '' '' ''************************************************************************************************
            '' '' ''vstTmpOpenRequis: Codigo tomado de la vista en el server 
            '' '' ''Esto es para tener en el servidor las requis que se bajaron tanto para agregarle otras cajas
            '' '' ''como para la creación de ordenes de compra.
            ' '' ''Dim SQLRequis As String = "SELECT [Req Number], [Item Number], [Mat Group], Material, [Short Text], [Purch Grp], Plant, [Tracking Field], [Doc Type], [Item Category], [PO Quantity]," & _
            ' '' ''                                 "[Fixed vendor], [Fix Vendor Name], [Desired Vendor], [Des Vendor Name], [Purch Org], [Outline Agreement], [Agreement Item], Rquisitioner, [Req Date], [PO Date], " & _
            ' '' ''                                 "[MRP Controller], Storage, [Del Indicator], [PO Number], [PO Item], [Created by], [Delivery Date], UOM, Quantity, [Release Date], Repair, SAPBox " & _
            ' '' ''                          "FROM tmpOpenRequis " & _
            ' '' ''                          "Where [User] = '" & gsUsuarioPC & "' And " & Filtro


            Dim SQLRequis As String = "SELECT * FROM vstTmpOpenRequis Where [User] = '" & gsUsuarioPC & "' And (" & Filtro & ")"

            '' '' ''************************************************************************************************
            '' '' ''************************************************************************************************
            Me.dtgRequisiciones.Columns.Clear()
            Requisiciones = cn.RunSentence(SQLRequis).Tables(0)
            Requisiciones.Columns.Add("Aging", System.Type.GetType("System.Decimal"))

            Me.dtgRequisiciones.DataSource = Requisiciones

            Me.dtgRequisiciones.Columns.Insert(0, cn.AddComboRootCauses("RootCauses", "RootCauses", "Select ID, Descripcion From RootCauses Where Tipo = 1 Order by Descripcion", "ID", "Descripcion"))

            'Solitud de Maria Perez: Mail PR to PO / Trigger Standardization Reports
            Me.dtgRequisiciones.Columns.Insert(0, cn.AddComboRootCauses("SPS", "SPS", "Select TNumber, Nombre From [Users] Where Show_In_List = 1", "TNumber", "Nombre"))
            Me.dtgRequisiciones.Columns.Insert(0, cn.AddComboRootCauses("Responsible", "Responsible", "Select ID, Description From [GCT_PR_Responsible_List]", "ID", "Description"))


            Me.dtgRequisiciones.Columns.Insert(0, cn.AddButtonColumn("AutoPO", "AutoPO", 25))
            Me.dtgRequisiciones.Columns.Insert(0, cn.AddButtonColumn("COMMENTS", "COMMENTS", 25))
            Me.dtgRequisiciones.Columns.Insert(0, cn.AddCheckBoxColumn("Ck", "Ck"))
            Me.dtgRequisiciones.Columns.Insert(0, cn.AddButtonColumn("Quotation", "Quotation", 25))

            Me.dtgRequisiciones.Columns.Add("Status", "Status")
            Me.txtTotalReq.Text = Requisiciones.Rows.Count

            If Requisiciones.Rows.Count > 0 Then
                VerificarComentariosYRootCauses()
                CargarConfiguracion()
            End If
        End If
    End Sub
    Private Sub cmdRefrescar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefrescar.Click
        CargarInfo()
    End Sub
    Private Sub LockGrid()
        Dim I As Integer = 0
        Dim drRow As System.Windows.Forms.DataGridViewRow

        For I = 0 To Me.dtgRequisiciones.Columns.Count - 1
            Me.dtgRequisiciones.Columns(I).ReadOnly = True
            Me.dtgRequisiciones.Columns(I).DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
        Next

        For Each drRow In Me.dtgRequisiciones.Rows
            If drRow.Cells("Req Date").Value > drRow.Cells("Release Date").Value Then
                drRow.Cells("Req Date").Style.BackColor = Drawing.Color.Crimson
                drRow.Cells("Release Date").Style.BackColor = Drawing.Color.Crimson
            End If
        Next

        Me.dtgRequisiciones.Columns("Comments").ReadOnly = False
        Me.dtgRequisiciones.Columns("Comments").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("CK").ReadOnly = False
        Me.dtgRequisiciones.Columns("CK").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("RootCauses").ReadOnly = False
        Me.dtgRequisiciones.Columns("RootCauses").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("SPS").ReadOnly = False
        Me.dtgRequisiciones.Columns("SPS").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("Responsible").ReadOnly = False
        Me.dtgRequisiciones.Columns("Responsible").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("Responsible ID").Visible = False

        Me.dtgRequisiciones.Columns("Q_Qty").Visible = False


        Me.dtgRequisiciones.Columns("OEMS_Case_ID").ReadOnly = False
        Me.dtgRequisiciones.Columns("OEMS_Case_ID").DefaultCellStyle.BackColor = Drawing.Color.White

    End Sub
    Private Sub cmdCreaPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreaPO.Click
        Dim Form As New frm035
        Dim i As Integer
        Dim Textos As New DataTable
        Dim HText As String = ""
        Dim HNote As String = ""

        Me.dtgRequisiciones.EndEdit()

        '*********************************************************************************************************
        ' Code to avoid to create a PO with the G/L Account 51070001
        Dim EBKN As New SAPCOM.EBKN_Report(Me.dtgRequisiciones.Rows(i).Cells("SAPBox").Value, gsUsuarioPC, AppId)
        Dim dgRow As System.Windows.Forms.DataGridViewRow
        Dim lReq As New List(Of String)
        Dim AllowCreation As Boolean = True 'Allow to continue with the creation

        For Each dgRow In Me.dtgRequisiciones.Rows
            If dgRow.Cells("Ck").Value Then
                EBKN.IncludeReq(dgRow.Cells("Req Number").Value)
            End If
        Next

        EBKN.Execute()
        If EBKN.Success Then
            If EBKN.ErrMessage = Nothing Then
                If EBKN.Data.Rows.Count > 0 Then
                    Dim row As DataRow
                    For Each row In EBKN.Data.Rows
                        If CInt(row("GL Account")) = 51070001 Then
                            AllowCreation = False
                            lReq.Add(row("Req Number") & " - " & row("Req Item"))
                        End If
                    Next
                End If
            Else
                MsgBox(EBKN.ErrMessage)
            End If
        End If

        If Not AllowCreation Then
            Dim Requis As String = ""
            Dim rq As String

            For Each rq In lReq
                Requis = Requis & Chr(13) & rq
            Next

            MsgBox("Please verify the G/L Account for the next PRs: " & Chr(13) & Requis, MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '*********************************************************************************************************

        cn.ExecuteInServer("Delete From TmpCrearPO Where Usuario = '" & gsUsuarioPC & "'")

        For Each R As DataGridViewRow In dtgRequisiciones.Rows
            If R.Cells("Ck").Value Then
                cn.ExecuteInServer("Insert Into TmpCrearPO(SAPBox, Requisition, Item, Usuario,Descripcion,Delivery,Qty,Vendor,Name,PG,POrg,HText,HNote,NCMCode,JurdCode,TaxCode,GR_BsdIV,Inv_Recpt,GR,New_PO,Created,ItemText,Ack_Req,MatUsage,MatOrigin,MatCategory,[Item Ref], [Item Category], [Mat Group]) Values('" & _
                                   R.Cells("SAPBox").Value & "','" & _
                                   R.Cells("Req Number").Value & "','" & _
                                   R.Cells("Item Number").Value & "','" & _
                                   gsUsuarioPC & "','" & _
                                   Replace(R.Cells("Short Text").Value, "'", "") & "','" & _
                                   R.Cells("Delivery Date").Value & "','" & _
                                   R.Cells("Quantity").Value & "','" & _
                                   R.Cells("Desired Vendor").Value & "','" & _
                                   R.Cells("Des Vendor Name").Value & "','" & _
                                   R.Cells("Purch Grp").Value & "','" & _
                                   R.Cells("Purch Org").Value & "'," & _
                                   "'','','','','',0,1,1,'',0,'',1,'','','','','','" & R.Cells("Mat Group").Value & "')") ' El iif del UOM fue eliminado para esperar la validación regional. [María Perez]
                '"'','','','','',0,1,1,'',0,'',1,'','','','','" & IIf(dtgRequisiciones.Rows(i).Cells("UOM").Value = "ACT", "D", "") & "')")

                If R.Cells("Country").Value = "BR" Then
                    Form.Is4BR = True
                End If
            End If
        Next


        'For i = 0 To Me.dtgRequisiciones.RowCount - 1
        '    If Me.dtgRequisiciones.Rows(i).Cells("Ck").Value Then
        '        cn.ExecuteInServer("Insert Into TmpCrearPO(SAPBox, Requisition, Item, Usuario,Descripcion,Delivery,Qty,Vendor,Name,PG,POrg,HText,HNote,NCMCode,JurdCode,TaxCode,GR_BsdIV,Inv_Recpt,GR,New_PO,Created,ItemText,Ack_Req,MatUsage,MatOrigin,MatCategory,[Item Ref], [Item Category], [Mat Group]) Values('" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("SAPBox").Value & "','" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("Req Number").Value & "','" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value & "','" & _
        '                           gsUsuarioPC & "','" & _
        '                           Replace(Me.dtgRequisiciones.Rows(i).Cells("Short Text").Value, "'", "") & "','" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("Delivery Date").Value & "','" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("Quantity").Value & "','" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("Desired Vendor").Value & "','" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("Des Vendor Name").Value & "','" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("Purch Grp").Value & "','" & _
        '                           Me.dtgRequisiciones.Rows(i).Cells("Purch Org").Value & "'," & _
        '                           "'','','','','',0,1,1,'',0,'',1,'','','','','','" & Me.dtgRequisiciones.Rows(i).Cells("Mat Group").Value & "')") ' El iif del UOM fue eliminado para esperar la validación regional. [María Perez]
        '        '"'','','','','',0,1,1,'',0,'',1,'','','','','" & IIf(dtgRequisiciones.Rows(i).Cells("UOM").Value = "ACT", "D", "") & "')")
        '    End If
        'Next

        '****************************************************
        '****************************************************
        '  Selección de los textos para la PO
        '****************************************************
        '****************************************************
        Textos = cn.RunSentence("Select HeaderText, HeaderNote From HeaderText_Note Where TNumber = '" & gsUsuario & "' Order by IdComentarios desc").Tables(0)
        If Textos.Rows.Count > 0 Then
            HText = Textos.Rows(0).Item("HeaderText")
            HNote = Textos.Rows(0).Item("HeaderNote")
            cn.ExecuteInServer("Update TmpCrearPO Set Htext = '" & HText & "', HNote = '" & HNote & "' Where Usuario = '" & gsUsuarioPC & "'")
        End If

        Form.ShowDialog()
        Me.CargarInfo()
    End Sub
    Private Sub cmdFiltrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltrar.Click
        Dim MyFilter As String = String.Empty

        For Each Col As Data.DataColumn In Requisiciones.Columns
            If Col.ColumnName <> "Aging" Then
                Select Case Col.DataType.ToString.ToUpper
                    Case "SYSTEM.STRING"
                        If MyFilter.Length > 0 Then
                            MyFilter = MyFilter & " or ([" & Col.ColumnName & "] = '" & txtBuscar.Text & "')"
                        Else
                            MyFilter = "([" & Col.ColumnName & "] = '" & txtBuscar.Text & "')"
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
                                MyFilter = MyFilter & " or ([" & Col.ColumnName & "] = " & lDouble & ")"
                            Else
                                MyFilter = "([" & Col.ColumnName & "] = " & lDouble & ")"
                            End If
                        End If


                End Select
            End If
        Next
        'Comentado las variables y el filtro para realizar pruebas del filtro dinamico por nombre de columna
        'Dim Filtro As String = ""
        'Dim i As Integer = 0
        'Dim DataType As String = ""

        If Me.txtBuscar.Text.Length > 0 Then

            'Filtro = "([Req Number] = " & Me.txtBuscar.Text & _
            '     " Or [Mat Group] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or Material like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Short Text] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Purch Grp] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or Plant like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Fixed vendor] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Fix Vendor Name] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Desired Vendor] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Des Vendor Name] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Purch Org] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Outline Agreement] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Agreement Item] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or Rquisitioner like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [PO Number] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [PO Item] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Created by] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or [Release Date] like '%" & Me.txtBuscar.Text & "%'" & _
            '     " Or SAPBox like '%" & Me.txtBuscar.Text & "%')"

            'Me.CargarInfo(Filtro)
            Me.CargarInfo(MyFilter)
        Else

            Me.CargarInfo()
        End If
    End Sub
    Private Sub cmdExcel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.ButtonClick
        cn.ExportDataTableToXL(Requisiciones)

        'If cn.ExportDataGridToXL(Me.dtgRequisiciones) Then
        '    MsgBox("Done!")
        'End If
    End Sub
    Private Sub ExportarColumnasOcultasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportarColumnasOcultasToolStripMenuItem.Click
        If cn.ExportDataTableToXL(Requisiciones) Then
            MsgBox("Done!")
        End If
    End Sub
    Private Sub cmdReqAnalisys_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReqAnalisys.Click
        Dim Form As New frm049
        Form.ShowDialog()
    End Sub
    Private Sub cmdRequis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRequis.Click
        Dim r As New Requisition
        Dim s As String = ""
        Dim d As DataTable

        For Each r In Req_Request
            s = s & " Or (Req = " & r.Requisition & " and Item = " & r.Item & " And SAP = '" & r.SAP & "')"
        Next

        d = cn.RunSentence("Select * From vst_Quotation_WO_Confirmation Where [User] = '" & gsUsuarioPC & "' And Confirmation = 0 " & s).Tables(0)

        Dim f As New frm062

        f.Data = d
        f.ShowDialog()

    End Sub
    Private Sub dtgRequisiciones_CellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs) Handles dtgRequisiciones.CellFormatting
        Try
            If Me.dtgRequisiciones.Columns(e.ColumnIndex).Name = "IR Only" AndAlso e.Value = True Then
                dtgRequisiciones.Rows(e.RowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(157, 223, 165)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub bgCheckSAP_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgCheckSAP.DoWork
        Dim SAP As New SAPConection.c_SAP(CKSap)
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Microsoft.VisualBasic.Left(CKSap, 3), gsUsuarioPC, "LAT")
        SAP.UserName = gsUsuarioPC

        SAP.Password = u.password
        SAP.OpenConnection(SAPConfig)
        SAP.Check_SAP_Config()
        SAP.CloseConnection()
    End Sub

    Private Sub cboIdioma_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboIdioma.SelectionChangeCommitted
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtAttach.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        dtgRequisiciones.EndEdit()
        Dim cn As New OAConnection.Connection
        Dim Attach() As String
        ReDim Attach(1)
        Dim _IsSTR As Boolean = False


        If txtAttach.Text.Length > 0 Then
            Attach(0) = txtAttach.Text
        End If

        If Me.cboEstacionarios.SelectedValue <> Nothing Then
            If Not Me.cboEstacionarios.SelectedValue.ToString <> "System.Data.DataRowView" Then
                MsgBox("Please select a stationary.", MsgBoxStyle.Information)
                Exit Sub
            End If
        Else
            MsgBox("Please select a stationary.", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim Stationary As DataTable
        Dim MailBody As String
        Dim MailSubject As String
        Stationary = cn.RunSentence("Select * From Estacionarios Where IDEstacionario = " & Me.cboEstacionarios.SelectedValue.ToString).Tables(0)
        If Stationary.Rows.Count <= 0 Then
            MsgBox("Stationary couldn't be loaded", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            MailBody = Stationary.Rows(0).Item("Mensage")
            MailSubject = Stationary.Rows(0).Item("Asunto")
        End If

        Dim ReqTable As String = ""
        ReqTable = "<Table border=1  BORDERCOLOR=" & Chr(34) & "888888" & Chr(34) & "><tr>"
        ReqTable = ReqTable & "<th  bgcolor=" & Chr(34) & "000099" & Chr(34) & ">Req Number</th>"
        ReqTable = ReqTable & "<th  bgcolor=" & Chr(34) & "000099" & Chr(34) & ">Item Number</th>"
        ReqTable = ReqTable & "<th  bgcolor=" & Chr(34) & "000099" & Chr(34) & ">Material</th>"
        ReqTable = ReqTable & "<th  bgcolor=" & Chr(34) & "000099" & Chr(34) & ">Short Text</th>"
        ReqTable = ReqTable & "<th  bgcolor=" & Chr(34) & "000099" & Chr(34) & ">Purch Grp</th>"
        ReqTable = ReqTable & "<th  bgcolor=" & Chr(34) & "000099" & Chr(34) & ">Purch Org</th>"
        ReqTable = ReqTable & "<th  bgcolor=" & Chr(34) & "000099" & Chr(34) & ">SAP</th>"

        Dim _PR As String = ""
        Dim _Case As String = ""
        For Each R As DataGridViewRow In dtgRequisiciones.Rows
            If R.Cells("CK").Value Then
                ReqTable = ReqTable & "<TR>"
                ReqTable = ReqTable & "<TD>" & R.Cells("Req Number").Value & "</TD>"
                ReqTable = ReqTable & "<TD>" & R.Cells("Item Number").Value & "</TD>"
                ReqTable = ReqTable & "<TD>" & R.Cells("Material").Value & "</TD>"
                If Microsoft.VisualBasic.Left(R.Cells("Material").Value.ToString, 1) = "3" Then
                    _IsSTR = True
                End If

                _PR = R.Cells("Req Number").Value
                _Case = R.Cells("OEMS_Case_ID").Value

                ReqTable = ReqTable & "<TD>" & R.Cells("Short Text").Value & "</TD>"
                ReqTable = ReqTable & "<TD>" & R.Cells("Purch Grp").Value & "</TD>"
                ReqTable = ReqTable & "<TD>" & R.Cells("Purch Org").Value & "</TD>"
                ReqTable = ReqTable & "<TD>" & R.Cells("SAPBox").Value & "</TD>"
                ReqTable = ReqTable & "</TR>"
                ReqTable = ReqTable & Chr(13) & Chr(10)
            End If
        Next
        ReqTable = ReqTable & "</Table>"

        MailBody = Replace(MailBody, "<@SOURCING>", ReqTable)
        MailSubject = "Case ID:" & _Case & " PR Number  " & _PR & "-" & MailSubject

        If (cn.SendOutlookMail(MailSubject, Attach, txtSourcingMail.Text, "", MailBody, "", False, "HTML", "", Not chkDraft.Checked, _IsSTR)) Then
            For Each R As DataGridViewRow In dtgRequisiciones.Rows
                If R.Cells("CK").Value Then
                    cn.RunSentence("Insert into HistoricoEnvioSourcing(Fecha,Requi,Item,SAPBox,Sourcing,TNumber) Values({fn now()},'" & R.Cells("Req Number").Value & "','" & R.Cells("Item Number").Value & "','" & R.Cells("SAPBox").Value & "','" & txtSourcingMail.Text & "', '" & gsUsuarioPC & "')")
                End If
            Next
        End If

    End Sub

    Private Sub bgwWorkflow_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwWorkflow.DoWork
        Dim TMP As New DataTable

        TMP.Columns.Add("SAPBox")
        TMP.Columns.Add("Req Number")
        TMP.Columns.Add("Item Number")
        TMP.Columns.Add("VariantID")
        TMP.Columns.Add("Upload Date")
        TMP.Columns.Add("Release Date")
        TMP.Columns.Add("Close Date")

        For Each R As DataRow In Requisiciones.Rows
            Dim NR As DataRow
            NR = TMP.NewRow()
            NR("SAPBox") = R("SAPBox")
            NR("Req Number") = R("Req Number")
            NR("Item Number") = R("Item Number")
            NR("VariantID") = IDVariante
            NR("Upload Date") = Now.Date
            NR("Release Date") = R("Release Date")
            TMP.Rows.Add(NR)
            Try
                cn.ExecuteInServer("Insert Into tmpWorkFlow(SAPBox,[Req Number],[Item Number],VariantID,[Upload Date],[Release Date]) Values('" & R("SAPBox") & "'," & R("Req Number") & ", " & R("Item Number") & "," & IDVariante & ",{fn now()}, '" & R("Release Date") & "')")
            Catch ex As Exception

            End Try
        Next
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        bgwWorkflow.RunWorkerAsync()
    End Sub

    Private Sub cboVariantes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVariantes.SelectedIndexChanged
        If Not Me.cboVariantes.SelectedValue.ToString = "System.Data.DataRowView" Then
            IDVariante = Me.cboVariantes.SelectedValue.ToString
        End If
    End Sub
End Class

Public Class Requisition
#Region "Variables"
    Private _Requisition As Double
    Private _Item As Integer
    Private _SAP As String
#End Region
#Region "Properties"
    Public Property Requisition() As Double
        Get
            Return _Requisition
        End Get
        Set(ByVal value As Double)
            _Requisition = value
        End Set
    End Property

    Public Property Item() As Integer
        Get
            Return _Item
        End Get
        Set(ByVal value As Integer)
            _Item = value
        End Set
    End Property

    Public Property SAP() As String
        Get
            Return _SAP
        End Get
        Set(ByVal value As String)
            _SAP = value
        End Set
    End Property
#End Region
#Region "Methods"
    Public Sub New(ByVal pRequisition As Double, ByVal pItem As Integer, ByVal pSAP As String)
        _Requisition = pRequisition
        _Item = pItem
        _SAP = pSAP
    End Sub

    Public Sub New()

    End Sub
#End Region
End Class