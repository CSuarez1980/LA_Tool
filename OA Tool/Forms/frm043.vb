Imports SAPCOM.RepairsLevels
Imports System.Configuration

Public Class frm043
    Dim cn As New OAConnection.Connection
    Dim Comentarios As New DataTable
    Dim POs As New DataTable
    Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    'Dim POFilter As Integer = 0 ' Variable para realizar un filtro para PO importados / nacionales
    Dim POFilterString As String = "" 'Variable para realizar el filtro para PO importados / nacionales

    'Private Sub frm043_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '    GuardarConfiguracion()
    'End Sub

    Dim PO_Print As SAPConection.POPrinting
    Dim SAP As SAPConection.c_SAP


    Private Sub frm043_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
        cn.LoadCombo(Me.cboIdioma, "Select IDIdioma,Descripcion From Idioma", "IDIdioma", "Descripcion")
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub cboIdioma_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboIdioma.SelectedIndexChanged
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub cmdDowload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDowload.Click
        Me.ToolBar.Enabled = False

        'Dim c As New SAPCOM.SAPConnector
        Dim i As Integer
        'Dim Rep As New SAPCOM.OpenOrders_Report("G4P", "BM4691")
        Dim Rep As New SAPCOM.POs_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
        Dim Plantas As New DataTable
        Dim Vendors As New DataTable
        Dim PGrp As New DataTable
        Dim POrg As New DataTable
        Dim POs As New DataTable
        Dim MatGrp As New DataTable

        Me.dtgRequisiciones.Columns.Clear()
        '**********************************
        'Bloqueo de combos, esto es para que
        'al momento de guardar los comentarios
        'no coloquen otra caja y la requi no
        'pierda el comentario:
        Me.cboSAPBox.Enabled = False
        Me.cboVariantes.Enabled = False
        Me.btnFiltrar.Visible = True
        '**********************************

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

        If Vendors.Rows.Count > 0 Then
            For i = 0 To Vendors.Rows.Count - 1
                If DBNull.Value.Equals(Vendors.Rows(i).Item("Prefijo")) Then
                    Rep.IncludeVendor("")
                    Rep.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                Else
                    If Vendors.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludeVendor("")
                        Rep.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                    Else
                        Rep.ExcludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
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

        Rep.IncludeDocsDatedFromTo(Me.dtpStart.Text, Me.dtpEnd.Text)


        Rep.RepairsLevel = IncludeRepairs
        'Rep.IncludeDelivDates = True

        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                POs = Rep.Data

                '*****************************************************
                '  13 de Enero 2010:
                '
                '  Este código fue agregado para evitar que en G4P 
                '  se presentaran problemas con columna adicionales
                '  exclusibas de en esta caja
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

                '*****************************************************
                '*****************************************************

                'Me.dtgRequisiciones.DataSource = POs

                POs = Rep.Data
                POs.Columns.Add("Usuario")
                POs.Columns.Add("SAPBox")
                POs.Columns.Add("Status")

                For i = 0 To POs.Rows.Count - 1
                    POs.Rows(i).Item("Usuario") = gsUsuarioPC
                    POs.Rows(i).Item("SAPBox") = Me.cboSAPBox.SelectedValue.ToString
                    POs.Rows(i).Item("Status") = ""
                Next

                cn.ExecuteInServer("Delete From TMPPrintPO Where Usuario = '" & gsUsuarioPC & "'")
                cn.AppendTableToSqlServer("TMPPrintPO", POs)

                'Carga la información del servidor:
                LoadInfoFromServer()


                Me.txtEstado.Text = "Información actualizada."
            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
                Me.txtEstado.Text = Rep.ErrMessage
            End If
        Else
            MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            Me.txtEstado.Text = Rep.ErrMessage
        End If

        Me.ToolBar.Enabled = True

    End Sub
    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuario & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")
        End If
    End Sub
    'Private Sub dtgRequisiciones_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellContentClick
    '    'Dim curRow As Integer = 0
    '    'Dim curCol As Integer = 0

    '    'curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
    '    'curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

    '    'Select Case Me.dtgRequisiciones.Columns(e.ColumnIndex).HeaderText.ToUpper
    '    '    Case "AUTOPO"
    '    '        Dim Requi As New OARequis.Requisition
    '    '        Requi.TestRequisition(Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value, Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value)

    '    '    Case "COMMENTS"
    '    '        Dim form As New frm027

    '    '        Comentarios = cn.RunSentence("Select * From Comentarios_Requis Where Requisicion = " & Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value & " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & " And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'").Tables(0)

    '    '        If Comentarios.Rows.Count > 0 Then
    '    '            form.lblDocumento.Text = "Purch Doc"
    '    '            form.txtRequisicion.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value
    '    '            form.txtReqItem.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value
    '    '            form.txtGica.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Material").Value
    '    '            form.txtMaterial.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Short Text").Value
    '    '            form.txtSAPBox.Text = Me.cboSAPBox.SelectedValue.ToString
    '    '            form.ShowDialog()
    '    '        Else
    '    '            MsgBox("Esta requisición no tiene comentarios", MsgBoxStyle.Information)
    '    '        End If
    '    'End Select
    'End Sub

    'Private Sub cmdComentarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If dtgRequisiciones.RowCount > 0 Then '<- Verifico que el datagrid contenga información
    '        Dim cn As New OAConnection.Connection
    '        Dim Form As New frm026
    '        Dim curRow As Integer = 0
    '        Dim curCol As Integer = 0

    '        curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
    '        curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

    '        Form.txtRequisicion.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value
    '        Form.txtReqItem.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value
    '        Form.txtGica.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Material").Value
    '        Form.txtMaterial.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Short Text").Value
    '        Form.txtSAPBox.Text = Me.cboSAPBox.SelectedValue.ToString
    '        Form.txtPlanta.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Plant").Value
    '        Form.txtMatGrp.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Mat Group").Value
    '        Form.txtPGrp.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Purch Grp").Value
    '        Form.txtPOrg.Text = Me.dtgRequisiciones.Rows(curRow).Cells("Purch Org").Value
    '        Form.ShowDialog()

    '        'If Form.Estado Then
    '        '    'VerificarComentariosYRootCauses()
    '        'End If

    '        Form.Close()

    '    End If
    'End Sub
    Private Sub cmdDesbloquear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesbloquear.Click
        Me.cboSAPBox.Enabled = True
        Me.cboVariantes.Enabled = True
        Me.dtgRequisiciones.Columns.Clear()
    End Sub
    'Private Sub VerificarComentariosYRootCauses()
    '    Dim Tabla As New DataTable
    '    Dim i As Integer
    '    Dim Requi As New OARequis.Requisition
    '    Dim Res As Boolean = False


    '    For i = 0 To Me.dtgRequisiciones.RowCount - 1
    '        'Verifica los comentarios de la requisición
    '        Tabla = cn.RunSentence("Select Top 1 Status From Comentarios_Requis Where Requisicion = " & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value & " And Item = " & Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value & " And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "' Order by Fecha Desc").Tables(0)
    '        If Tabla.Rows.Count > 0 Then
    '            Me.dtgRequisiciones.Rows(i).Cells("Comments").Value = "≈"
    '            Me.dtgRequisiciones.Rows(i).Cells("Comments").ToolTipText = "Esta requisición tiene comentario"
    '            Me.dtgRequisiciones.Rows(i).Cells("Status").Value = Tabla.Rows(0).Item("Status")
    '        End If

    '        'Verficación de RootCauses:
    '        Tabla = cn.RunSentence("Select RootCauses.ID From RootCauses_Requis,RootCauses Where RootCauses.ID = RootCauses_Requis.RootCause And Requisicion = " & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value & " And Item = " & Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value & " And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "' Order by Fecha Desc").Tables(0)
    '        If Tabla.Rows.Count > 0 Then
    '            Me.dtgRequisiciones.Rows(i).Cells("RootCauses").Value = Tabla.Rows(0).Item("ID")
    '        End If

    '        'Verifico si es gicado y corre en Automatico:
    '        If Me.dtgRequisiciones.Rows(i).Cells("Material").Value.Trim.Length > 0 Then
    '            If Requi.ReturnStatus(Me.dtgRequisiciones.Rows(i).Cells("Req Number").Value, Me.dtgRequisiciones.Rows(i).Cells("Item Number").Value) Then
    '                Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.BackColor = Drawing.Color.Green
    '                Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Value = "Ok"
    '            Else
    '                Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.BackColor = Drawing.Color.Red
    '                Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '                Me.dtgRequisiciones.Rows(i).Cells("AutoPO").Value = "Fail"
    '            End If
    '        End If
    '    Next

    'End Sub
    ''' <summary>
    ''' Carga la configuración del DataGrid, columnas visibles y el orden
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub CargarConfiguracion()
    '    'Dim Config As New DataTable
    '    'Dim i As Integer = 0
    '    'Config = cn.RunSentence("Select * From Config_Forms Where Usuario = '" & cn.GetUserId & "' And NombreFormulario = '" & Me.Name & "'").Tables(0)

    '    'If Config.Rows.Count > 0 Then
    '    '    'Aplico la configuración del DataGrid
    '    '    For i = 0 To Config.Rows.Count - 1
    '    '        Me.dtgRequisiciones.Columns(Config.Rows(i).Item("Columna")).DisplayIndex = Config.Rows(i).Item("Orden")
    '    '        Me.dtgRequisiciones.Columns(Config.Rows(i).Item("Columna")).Visible = Config.Rows(i).Item("Mostrar")
    '    '    Next

    '    'Else
    '    '    'Si el formulario no tiene configuración le guardo una
    '    '    For i = 0 To Me.dtgRequisiciones.ColumnCount - 1
    '    '        cn.ExecuteInServer("Insert Into Config_Forms(Usuario,NombreFormulario,Columna,Mostrar,Orden) " & _
    '    '                           "Values('" & cn.GetUserId & "','" & Me.Name & "','" & Me.dtgRequisiciones.Columns(i).HeaderText & "'," & IIf(Me.dtgRequisiciones.Columns(i).Visible, 1, 0) & "," & Me.dtgRequisiciones.Columns(i).DisplayIndex & ")")
    '    '    Next
    '    'End If
    'End Sub
    '''' <summary>
    '''' Guarda la configuración del DataGrid
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub GuardarConfiguracion()
    '    'Dim i As Integer = 0
    '    'For i = 0 To Me.dtgRequisiciones.Columns.Count - 1
    '    '    cn.ExecuteInServer("Update Config_Forms Set Orden = " & Me.dtgRequisiciones.Columns(i).DisplayIndex & " Where Usuario = '" & cn.GetUserId & "' And NombreFormulario = '" & Me.Name & "' And Columna = '" & Me.dtgRequisiciones.Columns(i).HeaderText & "'")
    '    'Next
    'End Sub

    'Private Sub dtgRequisiciones_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellEndEdit
    '    'Me.dtgRequisiciones.EndEdit()
    '    'Select Case Me.dtgRequisiciones.Columns(e.ColumnIndex).HeaderText.ToUpper
    '    '    Case "ROOTCAUSES"
    '    '        Dim Requi As New DataTable
    '    '        Dim curRow As Integer = 0
    '    '        Dim curCol As Integer = 0

    '    '        curRow = Me.dtgRequisiciones.CurrentCell.RowIndex
    '    '        curCol = Me.dtgRequisiciones.CurrentCell.ColumnIndex

    '    '        'Verifico si la requi ya tiene Root Cauese:
    '    '        Requi = cn.RunSentence("Select RootCauses.Descripcion From RootCauses,RootCauses_Requis " & _
    '    '                               "Where RootCauses.ID = RootCauses_Requis.RootCause " & _
    '    '                               " And Requisicion = " & Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value & _
    '    '                               " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & _
    '    '                               " And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'").Tables(0)


    '    '        If Requi.Rows.Count = 0 Then
    '    '            'Si la Requi no tiene RootCause le agrego:
    '    '            cn.ExecuteInServer("Insert Into RootCauses_Requis Values(" & _
    '    '                                Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value & "," & _
    '    '                                Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & "," & _
    '    '                                "'" & Me.cboSAPBox.SelectedValue.ToString & "'," & _
    '    '                                "{fn now()}," & _
    '    '                                "'" & cn.GetUserId & "'," & _
    '    '                                "'" & Me.dtgRequisiciones.Rows(curRow).Cells("Plant").Value & "'," & _
    '    '                                "'" & Me.dtgRequisiciones.Rows(curRow).Cells("Mat Group").Value & "'," & _
    '    '                                "'" & Me.dtgRequisiciones.Rows(curRow).Cells("Purch Grp").Value & "'," & _
    '    '                                "'" & Me.dtgRequisiciones.Rows(curRow).Cells("Purch Org").Value & "'," & _
    '    '                                Me.dtgRequisiciones.Rows(curRow).Cells("RootCauses").Value & ")")

    '    '        Else
    '    '            'Si la requi ya Root Cause la actualizo:
    '    '            cn.ExecuteInServer("Update RootCauses_Requis set RootCause = " & Me.dtgRequisiciones.Rows(curRow).Cells("RootCauses").Value & _
    '    '                               " Where Requisicion = " & Me.dtgRequisiciones.Rows(curRow).Cells("Req Number").Value & _
    '    '                               " And Item = " & Me.dtgRequisiciones.Rows(curRow).Cells("Item Number").Value & _
    '    '                               " And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'")

    '    '        End If
    '    'End Select
    'End Sub

    'Private Sub dtgRequisiciones_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dtgRequisiciones.ColumnHeaderMouseClick
    '    'VerificarComentariosYRootCauses()
    'End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            'If form.Guardar Then
            '    For i = 0 To form.dtgConfiguracion.RowCount - 1
            '        cn.ExecuteInServer("Update Config_Forms set Mostrar = " & IIf(form.dtgConfiguracion.Rows(i).Cells("Mostrar").Value, 1, 0) & " Where ID = " & form.dtgConfiguracion.Rows(i).Cells("ID").Value)
            '    Next
            'End If
            'CargarConfiguracion()
        Else
            MsgBox("Debe decargar la información antes de configurar el DataGrid", MsgBoxStyle.Information)
        End If
    End Sub
    ''' <summary>
    ''' Carga las órdnes de compra que se almacenaron en el servidor
    ''' </summary>
    Private Sub LoadInfoFromServer()
        Dim lsSql As String = ""
        Dim lsWhere As String = ""

        lsSql = "Select [Print],"

        'Si el check de ennviadas esta activo muestro la casilla de enviado:
        If Me.chkEnviadas.Checked Then
            lsSql = lsSql & "Sent,"
        End If

        'Si el proveedor se encuentra en el supplier portal muestro la casilla:
        If Me.chkSupPortal.Checked Then
            lsSql = lsSql & "SP,"
        End If


        POFilterString = ""
        If giDistribution <> 0 Then
            Select Case giDistribution
                Case 1
                    POFilterString = " And (([Nac / Imp] = 'National')" & cn.getExceptions(giDistribution) & ")"

                Case 2
                    POFilterString = " And (([Nac / Imp] = 'Import')" & cn.getExceptions(giDistribution) & ")"
            End Select
        End If

        lsSql = lsSql & "[Doc Date],[Doc Number],Vendor,[Vendor Name],[Purch Grp],Plant,Requisitioner,Mail,CC,Status,[Nac / Imp],[Purch Org],IsSTR As Spend, OEMS_Case_ID  From vstPrintPO Where (Usuario = '" & gsUsuarioPC & "'" & POFilterString & " AND ([Created By] NOT IN (Select TNumber From Exclude_POs_Created_By)) And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "') "

        'Realizo el filtro de los checks seleccionados por los usuarios:
        If Me.chkNoEnviadas.Checked Then
            If lsWhere.Length = 0 Then
                'lsWhere = lsWhere & " Sent Is Null "
                lsWhere = " Sent = 0 "
            End If
        End If

        If Me.chkEnviadas.Checked Then
            If lsWhere.Length = 0 Then
                'lsWhere = lsWhere & " Not(Sent Is Null)"
                lsWhere = " (Sent = 1) "
            Else
                'lsWhere = lsWhere & " Or Not(Sent Is Null)"
                lsWhere = "(" & lsWhere & " Or  Sent = 1) "
            End If
        End If

        'Para los proveedores que se encuentran en el supplier portal:
        If Me.chkSupPortal.Checked Then
            If lsWhere.Length = 0 Then
                'lsWhere = lsWhere & " Not(SP Is Null)"
                lsWhere = " SP = 1"
            Else
                'lsWhere = lsWhere & " Or Not(SP Is Null)"
                lsWhere = "(" & lsWhere & ") And SP = 1"
            End If
        Else
            'Muestra solo las PO's que no estan en el supplier portal:
            If lsWhere.Length = 0 Then
                'lsWhere = lsWhere & " Not(SP Is Null)"
                lsWhere = lsWhere & " SP = 0"
            Else
                'lsWhere = lsWhere & " Or Not(SP Is Null)"
                lsWhere = "(" & lsWhere & ") And SP = 0"
            End If
        End If


        If Me.txtBuscar.Text.Length > 0 Then

            Dim xWhere As String = ""

            xWhere = "(" & _
                     "Requisitioner Like '%" & Me.txtBuscar.Text & "%'" & _
                     " Or [Doc Number] Like '%" & Me.txtBuscar.Text & "%'" & _
                     " Or [Vendor Name] Like '%" & Me.txtBuscar.Text & "%'" & _
                     " Or Vendor Like '%" & Me.txtBuscar.Text & "%'" & _
                     " Or [Purch Org] Like '%" & Me.txtBuscar.Text & "%'" & _
                     " Or [Purch Grp] Like '%" & Me.txtBuscar.Text & "%'" & _
                     ")"

            If lsWhere.Length = 0 Then
                lsWhere = lsWhere & " " & xWhere
            Else
                lsWhere = "(" & lsWhere & ") And " & xWhere
            End If
        End If

        If lsWhere.Length = 0 Then
            'Esto es si no selecciona ninguna opción:
            lsWhere = " Usuario = 'XXXXXX'"
        End If

        lsSql = lsSql & " And (" & lsWhere & ")"

        POs = cn.RunSentence(lsSql).Tables(0)
        Me.dtgRequisiciones.DataSource = POs
        Me.txtTotalPO.Text = POs.Rows.Count

        LockGrid()
    End Sub
    Private Sub LockGrid()
        Dim I As Integer = 0

        For I = 0 To Me.dtgRequisiciones.Columns.Count - 1
            Me.dtgRequisiciones.Columns(I).ReadOnly = True
            Me.dtgRequisiciones.Columns(I).DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
        Next

        Me.dtgRequisiciones.Columns("Print").ReadOnly = False
        Me.dtgRequisiciones.Columns("Print").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("Mail").ReadOnly = False
        Me.dtgRequisiciones.Columns("Mail").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("CC").ReadOnly = False
        Me.dtgRequisiciones.Columns("CC").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("Status").ReadOnly = False
        Me.dtgRequisiciones.Columns("Status").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("Doc Number").ReadOnly = False
        Me.dtgRequisiciones.Columns("Doc Number").DefaultCellStyle.BackColor = Drawing.Color.White

        Me.dtgRequisiciones.Columns("OEMS_Case_ID").ReadOnly = False
        Me.dtgRequisiciones.Columns("OEMS_Case_ID").DefaultCellStyle.BackColor = Drawing.Color.White

    End Sub
    Private Sub btnFiltrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFiltrar.Click
        Me.cboSAPBox.Enabled = False
        Me.cboVariantes.Enabled = False
        LoadInfoFromServer()
    End Sub
    Private Sub cmdImprimirPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImprimirPDF.Click
        PrintingProcess()
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
            If r.Cells("Print").Value Then
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
                If Not DBNull.Value.Equals(r.Cells("Print").Value) AndAlso r.Cells("Print").Value Then
                    r.Cells("Status").Value = PO_Print.GetFilePath(r.Cells("Doc Number").Value)
                    If Not PO_Print.DocumentPrinted(r.Cells("Doc Number").Value) Then
                        r.DefaultCellStyle.BackColor = Drawing.Color.LightSalmon
                        r.Cells("Print").Value = False
                    Else
                        r.DefaultCellStyle.BackColor = Drawing.Color.White
                    End If
                End If
            Catch ex As Exception
                'Do nothing
            End Try
        Next

    End Sub

    Private Sub OldPrintingProcess()
        Dim SAP As New SAPConection.c_SAP(Me.cboSAPBox.Text)
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Microsoft.VisualBasic.Left(Me.cboSAPBox.SelectedValue.ToString, 3), gsUsuarioPC, "LAT")


        SAP.UserName = gsUsuarioPC
        SAP.Password = u.password

        SAP.OpenConnection(SAPConfig)



        Dim i As Integer = 0
        Dim Spool As String
        Dim FilesInFolder As String()

        'Estas Variables las utilizo para evitar que se genere un nombre incorrecto en el archivo de impresion;
        'Imprimo todos los PDF y despues que termine la impresion de todos; los renombro.
        Dim ArchivosAntesDeImpresion As Integer = 0
        Dim ArchivosDespuesImpresion As Integer = 0
        Dim ArchivosImpresos As Integer = 0
        Dim Intentos As Integer = 0 ' Variable para evitar que se se encicle 

        'Determino cuantos archivos hay en la carpeta antes de imprimir
        FilesInFolder = System.IO.Directory.GetFiles(PDFPath)
        ArchivosAntesDeImpresion = FilesInFolder.Length

        Me.dtgRequisiciones.EndEdit()

        If ArchivosAntesDeImpresion > 0 Then
            If MsgBox("PDF files were found in folder: " & PDFPath & Chr(13) & "Files found: " & ArchivosAntesDeImpresion & "." & Chr(13) & Chr(13) & "Files will be moved to: " & Replace(PDFPath, PDFPath, PDFPath & "\BackUp") & Chr(13) & Chr(13) & "Yes, move files and start printing." & Chr(13) & "No, Don't move or print, I'll fix it by myself.", MsgBoxStyle.Question & MsgBoxStyle.YesNo, "PDF files found!") = MsgBoxResult.No Then
                Exit Sub
            Else
                Dim PDFList As String() = System.IO.Directory.GetFiles(PDFPath)
                For Each f As String In PDFList
                    My.Computer.FileSystem.MoveFile(f, Replace(f, PDFPath, PDFPath & "\BackUp"), True)
                    System.IO.File.Delete(f)
                Next
            End If
        End If

        ArchivosAntesDeImpresion = 0



        For i = 0 To Me.dtgRequisiciones.Rows.Count - 1
            If Me.dtgRequisiciones.Rows(i).Cells("Print").Value Then
                Spool = ""
                'Spool = SAP.Print_PO(Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value, "NEU", Me.cboIdioma.SelectedValue.ToString)

                ' Do Until Spool.IndexOf("changed") > 0
                Spool = SAP.Print_PO(Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value, "NEU", Me.cboIdioma.SelectedValue.ToString, Me.chkAle.Checked)
                'Loop

                Me.dtgRequisiciones.Rows(i).Cells("Status").Value = Spool
                If Spool.Length > 0 Then

                    If Spool.IndexOf("changed") > 0 Then
                        ArchivosImpresos += 1
                    End If

                End If
            End If
        Next


        FilesInFolder = System.IO.Directory.GetFiles(PDFPath)
        ArchivosDespuesImpresion = FilesInFolder.Length

        'Este ciclo es para hacer una pausa hasta que se terminen de imprimir todos los PDF's
        Do Until ((((ArchivosAntesDeImpresion + ArchivosImpresos) = ArchivosDespuesImpresion)) Or Intentos > 10)
            Sleep(1000)
            FilesInFolder = System.IO.Directory.GetFiles(PDFPath)
            ArchivosDespuesImpresion = FilesInFolder.Length
            Intentos += 1
        Loop

        For i = 0 To Me.dtgRequisiciones.Rows.Count - 1
            If (Me.dtgRequisiciones.Rows(i).Cells("Print").Value) And (dtgRequisiciones.Rows(i).Cells("Status").Value.ToString.IndexOf("changed") > 0) Then
                Spool = ""
                If Me.dtgRequisiciones.Rows(i).Cells("Status").Value.ToString.Length > 0 Then
                    Spool = SAP.Spool_PO(Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value, "NEU", Me.cboIdioma.SelectedValue.ToString)
                    Me.dtgRequisiciones.Rows(i).Cells("Status").Value = Spool
                End If
            End If
        Next

        For i = 0 To Me.dtgRequisiciones.Rows.Count - 1
            If Me.dtgRequisiciones.Rows(i).Cells("Print").Value And (dtgRequisiciones.Rows(i).Cells("Status").Value.ToString.Length > 0) And (dtgRequisiciones.Rows(i).Cells("Status").Value.ToString.IndexOf("ERROR") <= 0) Then
                Me.dtgRequisiciones.Rows(i).Cells("Status").Value = Me.Match_PO(Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value, Me.dtgRequisiciones.Rows(i).Cells("Status").Value)
                cn.ExecuteInServer("Update tmpPrintPO Set Status = '" & Me.dtgRequisiciones.Rows(i).Cells("Status").Value & "' Where Usuario = '" & gsUsuario & "' And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "' And [Doc Number] = " & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value)
            End If
        Next

        SAP.CloseConnection()

        '***************************************************************************************
        '  Verifico cuales PO's no se imprimieron correctamente
        '***************************************************************************************
        Dim row As System.Windows.Forms.DataGridViewRow
        For Each row In Me.dtgRequisiciones.Rows
            If (row.Cells("Status").Value.ToString.IndexOf("Error") <> -1) And row.Cells("Print").Value Then
                row.DefaultCellStyle.BackColor = Drawing.Color.LightSalmon
                row.Cells("Print").Value = False
            End If
        Next

        MsgBox("Done!")
    End Sub

    Private Sub frm043_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgRequisiciones.Width = Me.Width - 20
        Me.dtgRequisiciones.Height = Me.Height - 220
    End Sub

    Private Function Match_PO(ByVal PO As String, ByVal Spool As String) As String
        Dim MyPath As String
        Dim OldName As String
        Dim NewName As String

        Try
            MyPath = PDFPath & "\*" & Spool & "-1.pdf" '
            OldName = PDFPath & "\" & Dir(MyPath)
            NewName = PDFPath & "\" & Me.cboSAPBox.SelectedValue.ToString & "-" & PO & ".pdf" ' Define file names.

            If (OldName) = (PDFPath & "\") Then
                Throw New Exception("Error con el Spool, Spool ID no se encuentra en los archivos")
            End If

            System.IO.File.Delete(NewName)
            System.IO.File.Move(OldName, NewName)
            System.IO.File.Delete(OldName)

            Match_PO = NewName

        Catch ex As Exception
            'MsgBox(ex.Message)
            Match_PO = "Error al modificar el nombre del PDF. Spool ID:" & Spool
        End Try

    End Function

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If cn.ExportDataTableToXL(POs) Then
            MsgBox("Done!.")
        End If

    End Sub

    Private Sub cboIdioma_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboIdioma.SelectionChangeCommitted
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub cmdEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Dim cn As New OAConnection.Connection
        Dim Mail As New DataTable
        Dim Sites As New DataTable
        Dim i As Integer = 0
        Dim Orden As String = ""
        Dim Attach As String()


        'Get LA Sites:
        Sites = cn.RunSentence("Select Plant_Code, Plant_Name From SC_Plant").Tables(0)

        '*******************************************
        'Verifico que se seleccione un estacionario:
        '*******************************************
        If Me.cboEstacionarios.SelectedValue <> Nothing Then
            If Me.cboEstacionarios.SelectedValue.ToString = "System.Data.DataRowView" Then
                MsgBox("Debe seleccionar un estacionario para continuar...", MsgBoxStyle.Exclamation, "Selección de estacionarios")
                Exit Sub
            End If
        Else
            MsgBox("Debe seleccionar un estacionario para continuar...", MsgBoxStyle.Exclamation, "Selección de estacionarios")
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
                If Me.dtgRequisiciones.Rows(i).Cells("Print").Value Then
                    If IO.File.Exists(Me.dtgRequisiciones.Rows(i).Cells("Status").Value) Then
                        Attach(0) = Me.dtgRequisiciones.Rows(i).Cells("Status").Value

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
                            LQ = ""
                        End Try


                        If Not DBNull.Value.Equals(LQ) Then
                            MailSubject = "Case ID:" & dtgRequisiciones.Rows(i).Cells("OEMS_Case_ID").Value.ToString & " PO Number  " & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value & "-" & Mail.Rows(0).Item("Asunto") & " - [" & LQ & "] "
                        Else
                            MailSubject = "Case ID:" & dtgRequisiciones.Rows(i).Cells("OEMS_Case_ID").Value.ToString & " PO Number  " & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value & "-" & (Mail.Rows(0).Item("Asunto"))
                        End If

                        If cn.SendOutlookMail(MailSubject, Attach, Me.dtgRequisiciones.Rows(i).Cells("Mail").Value, Me.dtgRequisiciones.Rows(i).Cells("CC").Value, lsBody, "", False, "HTML") Then
                            cn.ExecuteInServer("Insert Into HistorySendPO(PO, SendDate, Usuario, SAPBox, Send, [Upload Date], [Region],[Site]) Values(" & Me.dtgRequisiciones.Rows(i).Cells("Doc Number").Value & ",{fn now()},'" & gsUsuarioPC & "','" & Me.cboSAPBox.SelectedValue.ToString & "',1,'" & Me.dtgRequisiciones.Rows(i).Cells("Doc Date").Value & "','" & IIf(chkNA.Checked, "NA", "LA") & "','" & Me.dtgRequisiciones.Rows(i).Cells("Plant").Value & "')")
                        End If
                    Else
                        Me.dtgRequisiciones.Rows(i).Cells("Status").Value = "Error: File not found."
                    End If
                End If
            Next
            MsgBox("Mails was created succesully.", MsgBoxStyle.Information, "Mail Creation")
        Else
            MsgBox("Stationery not found.")
        End If
    End Sub

    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim I As Integer = 0

        Me.dtgRequisiciones.EndEdit()

        If MsgBox("This process will set PO's as sent." & Chr(13) & Chr(13) & "Continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Marcar PO's como enviadas?") = MsgBoxResult.Yes Then
            For I = 0 To Me.dtgRequisiciones.RowCount - 1
                If Me.dtgRequisiciones.Rows(I).Cells("Print").Value Then
                    cn.ExecuteInServer("Insert Into HistorySendPO(PO, SendDate, Usuario, SAPBox, Send, [Upload Date], [Region],[Site]) Values(" & Me.dtgRequisiciones.Rows(I).Cells("Doc Number").Value & ",{fn now()},'" & gsUsuarioPC & "','" & Me.cboSAPBox.SelectedValue.ToString & "',0,'" & Me.dtgRequisiciones.Rows(I).Cells("Doc Date").Value & "','" & IIf(chkNA.Checked, "NA", "LA") & "','" & Me.dtgRequisiciones.Rows(I).Cells("Plant").Value & "')")
                End If
            Next
            MsgBox("PO's saved.", MsgBoxStyle.Information, "Completed.")
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim Row As System.Windows.Forms.DataGridViewRow
        Me.dtgRequisiciones.EndEdit()

        For Each Row In Me.dtgRequisiciones.Rows
            If Row.Cells("Print").Value Then
                Row.Cells("CC").Value = Microsoft.VisualBasic.Left(Row.Cells("Requisitioner").Value, 6) & "@pg.com"
            End If
        Next
    End Sub

    Private Sub cmdFiltrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltrar.Click
        Me.cboSAPBox.Enabled = False
        Me.cboVariantes.Enabled = False
        LoadInfoFromServer()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim drRow As System.Windows.Forms.DataGridViewRow

        For Each drRow In dtgRequisiciones.Rows
            drRow.Cells("Print").Value = True
        Next

        dtgRequisiciones.EndEdit()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Dim drRow As System.Windows.Forms.DataGridViewRow

        For Each drRow In dtgRequisiciones.Rows
            drRow.Cells("Print").Value = False
        Next

        dtgRequisiciones.EndEdit()
    End Sub

    Private Sub cboVariantes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVariantes.SelectedIndexChanged


        If Me.cboVariantes.SelectedText <> "" Then
        Else
            If Not Me.cboVariantes.SelectedValue.ToString <> "System.Data.DataRowView" Then
                Exit Sub
            End If
        End If
    End Sub

    Private Sub bgPrint_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgPrint.DoWork
        PO_Print.Excecute()
    End Sub

    Private Sub bgPrint_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgPrint.RunWorkerCompleted
        SAP.CloseConnection()

        For Each r As Windows.Forms.DataGridViewRow In Me.dtgRequisiciones.Rows
            If Not DBNull.Value.Equals(r.Cells("Print").Value) AndAlso r.Cells("Print").Value Then
                r.Cells("Status").Value = PO_Print.GetFilePath(r.Cells("Doc Number").Value)
                If Not PO_Print.DocumentPrinted(r.Cells("Doc Number").Value) Then
                    r.DefaultCellStyle.BackColor = Drawing.Color.LightSalmon
                    r.Cells("Print").Value = False
                Else
                    r.DefaultCellStyle.BackColor = Drawing.Color.White
                End If
            End If
        Next
    End Sub

    Private Sub dtgRequisiciones_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgRequisiciones.CellEndEdit
        Select Case Me.dtgRequisiciones.Columns(e.ColumnIndex).HeaderText.ToUpper
            Case "OEMS_CASE_ID"
                Dim Param As New List(Of SqlClient.SqlParameter)

                'Dim SAP As New Data.SqlClient.SqlParameter("@SAPBox", Me.dtgRequisiciones.Rows(curRow).Cells("SAPBox").Value)
                Dim SAP As New Data.SqlClient.SqlParameter("@SAPBox", Me.cboSAPBox.SelectedValue.ToString)
                Dim PO As New Data.SqlClient.SqlParameter("@PO", Me.dtgRequisiciones.CurrentRow.Cells("Doc Number").Value)
                Dim Item As New Data.SqlClient.SqlParameter("@Item", 0)
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
End Class