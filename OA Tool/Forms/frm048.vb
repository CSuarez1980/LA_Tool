Imports System.Drawing

Public Class frm048
    Dim cn As New OAConnection.Connection
    Dim Grid As New OAConnection.ConfigurationGrid.DataGrid(gsUsuarioPC, "frm048")
    Dim dtInfo As DataTable

    Private Sub cmdImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim Table As DataTable
        Dim Rep As New SAPCOM.EKPO_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
        'Table = cn.RunSentence("Select Top 1 * From vstBI_APTrayTemplate Where Box = 'x'").Tables(0)
        'cn.ExportDataTableToXL(Table)

        Table = cn.Read_ZMR0_File("c:\BI.xls", Me.cboSAPBox.SelectedValue.ToString)

        Table.Columns.Add("UploadDate")
        Dim row As DataRow

        For Each row In Table.Rows
            row.Item("UploadDate") = Now
        Next

        cn.AppendTableToSqlServer("BI_ZMR0_Report", Table)
        dtgBI.DataSource = Table
    End Sub
    Private Sub cmdTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Table As DataTable

        'Table = cn.RunSentence("Select Top 1 * From vstBI_APTrayTemplate Where Box = 'x'").Tables(0)
        'cn.ExportDataTableToXL(Table)

        Table = cn.Read_ZMR0_File("c:\BI.xls", "L7P")

        Table.Columns.Add("UploadDate")
        Dim row As DataRow

        For Each row In Table.Rows
            row.Item("UploadDate") = Now
        Next

        cn.AppendTableToSqlServer("BI_ZMR0_Report", Table)

        dtgBI.DataSource = Table
    End Sub
    Private Sub frm048_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")

        Dim Table As DataTable
        Table = cn.RunSentence("Select * From vst_BI_ZMR0_Report Where SAP = 'XXX'").Tables(0)
        Me.dtgBI.DataSource = Table
        Me.dtgBI.Columns.Insert(0, cn.AddComboRootCauses("RootCauses", "RootCauses", "Select ID, Descripcion From RootCauses Where Tipo = 3 Order by Descripcion", "ID", "Descripcion"))
        Me.dtgBI.Columns.Insert(0, cn.AddComboRootCauses("Status", "Status", "Select IdStatus, Description From BI_Status Order by Description", "IdStatus", "Description"))
        Me.dtgBI.Columns.Insert(0, cn.AddComboRootCauses("R_Code", "R_Code", "Select RC_Code, (Cast(RC_Code as NVarchar) + ' - ' + RC_Description) as Description From BI_Reason_Code Order by RC_Code", "RC_Code", "Description"))

        Grid._MyDataGrid = Me.dtgBI
        Grid.GetConfiguration()
        Grid.SetConfiguration()
    End Sub
    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        GetInfo()
    End Sub

    Private Sub GetInfo(Optional ByVal FiltroAvanzado As String = "")
        Dim TipoFiltro As Integer = 0

        Me.dtgBI.DataSource = ""
        Me.dtgBI.Columns.Clear()
        Me.lblTotal.Text = "Total Blocked Invoice: 0"

        If (cboSAPBox.Text <> "") And (cboSAPBox.Text <> "System.Data.DataRowView") Then
            If cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
                Dim dt As DataTable
                lblVariant.Text = ""
                'MsgBox("Select * From HeaderVariante Where SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "' And BIDefault = 1 And TNumber = '" & gsUsuario & "'")
                dt = cn.RunSentence("Select * From HeaderVariante Where SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "' And BIDefault = 1 And TNumber = '" & gsUsuario & "'").Tables(0)

                If dt.Rows.Count > 0 Then
                    TipoFiltro = giDistribution
                    'TipoFiltro = dt.Rows(0).Item("POFilter")

                    lblVariant.Text = dt.Rows(0).Item("Nombre")
                    Dim Plantas As New DataTable
                    Dim Vendors As New DataTable
                    Dim PGrp As New DataTable
                    Dim POrg As New DataTable
                    Dim MatGrp As New DataTable

                    Dim FP As String = "" 'Filtro para las plantas
                    Dim FV As String = "" 'Filtro para los vendors
                    Dim FPG As String = "" ' Filtro para purch grp
                    Dim FPO As String = "" ' Filtro para POrg
                    Dim FM As String = "" ' Filtro para material group
                    Dim row As DataRow

                    Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' And HeaderVariante.IDVariante = " & dt.Rows(0).Item("IDVariante") & ")").Tables(0)
                    Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' And HeaderVariante.IDVariante = " & dt.Rows(0).Item("IDVariante") & ")").Tables(0)
                    PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' And HeaderVariante.IDVariante = " & dt.Rows(0).Item("IDVariante") & ")").Tables(0)
                    POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' And HeaderVariante.IDVariante = " & dt.Rows(0).Item("IDVariante") & ")").Tables(0)
                    MatGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' And HeaderVariante.IDVariante = " & dt.Rows(0).Item("IDVariante") & ")").Tables(0)

                    '***********************************************************************
                    ' Configuro el filtro para los Plantas
                    '***********************************************************************
                    If Plantas.Rows.Count > 0 Then
                        ' ''FP = "("

                        ' ''For Each row In Plantas.Rows
                        ' ''    If FP.Length > 2 Then
                        ' ''        FP = FP & " or "
                        ' ''    End If

                        ' ''    If DBNull.Value.Equals(row.Item("Prefijo")) Then
                        ' ''        FP = FP & "Plant = '" & row.Item("Valor") & "'"
                        ' ''    Else
                        ' ''        If row.Item("Prefijo") = "" Then
                        ' ''            FP = FP & "Plant = '" & row.Item("Valor") & "'"
                        ' ''        Else
                        ' ''            FP = FP & "Plant <> '" & row.Item("Valor") & "'"
                        ' ''        End If
                        ' ''    End If
                        ' ''Next

                        ' ''FP = FP & ")"

                        For Each row In Plantas.Rows
                            Dim Eval As String = ""

                            If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                                Eval = "(Plant = '" & row.Item("Valor") & "')"
                            Else
                                Eval = "(Plant <> '" & row.Item("Valor") & "')"
                            End If


                            If FP.Length > 0 Then
                                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                                    FP = FP & " or " & Eval
                                Else
                                    FP = FP & " And " & Eval
                                End If
                            Else
                                FP = Eval
                            End If
                        Next

                        FP = "(" & FP & ")"


                    End If

                    '***********************************************************************
                    '  Configuro el filtro para los vendors
                    '***********************************************************************
                    If Vendors.Rows.Count > 0 Then
                        ''FV = "("
                        ''For Each row In Vendors.Rows
                        ''    If FV.Length > 2 Then
                        ''        FV = FV & ") or ("
                        ''    Else
                        ''        FV = FV & "("
                        ''    End If

                        ''    If DBNull.Value.Equals(row.Item("Prefijo")) Then
                        ''        FV = FV & "Vendor = '" & row.Item("Valor") & "'"
                        ''    Else
                        ''        If row.Item("Prefijo") = "" Then
                        ''            FV = FV & "Vendor = '" & row.Item("Valor") & "'"
                        ''        Else
                        ''            FV = FV & "Vendor <> '" & row.Item("Valor") & "'"
                        ''        End If
                        ''    End If

                        ''Next
                        ''FV = FV & ")"

                        ''If FV.Length <= 2 Then
                        ''    FV = ""
                        ''Else
                        ''    FV = FV & ")"
                        ''End If

                        '*********************************************************************

                        For Each row In Vendors.Rows
                            Dim Eval As String = ""

                            If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                                Eval = "(Vendor = '" & row.Item("Valor") & "')"
                            Else
                                Eval = "(Vendor <> '" & row.Item("Valor") & "')"
                            End If

                            If FV.Length > 0 Then
                                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                                    FV = FV & " or " & Eval
                                Else
                                    FV = FV & " And " & Eval
                                End If
                            Else
                                FV = Eval
                            End If
                        Next

                        FV = "(" & FV & ")"

                    End If

                    '***********************************************************************
                    ' Configuro el filtro para los P. Groups
                    '***********************************************************************
                    If PGrp.Rows.Count > 0 Then
                        ''FPG = "("
                        ''For Each row In PGrp.Rows
                        ''    If FPG.Length > 2 Then
                        ''        FPG = FPG & " or "
                        ''    End If

                        ''    If DBNull.Value.Equals(row.Item("Prefijo")) Then
                        ''        FPG = FPG & "[Purch Group] = '" & row.Item("Valor") & "'"
                        ''    Else
                        ''        If row.Item("Prefijo") = "" Then
                        ''            FPG = FPG & "[Purch Group] = '" & row.Item("Valor") & "'"
                        ''        Else
                        ''            FPG = FPG & "[Purch Group] <> '" & row.Item("Valor") & "'"
                        ''        End If
                        ''    End If
                        ''Next
                        ''FPG = FPG & ")"

                        For Each row In PGrp.Rows
                            Dim Eval As String = ""

                            If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                                Eval = "([Purch Group] = '" & row.Item("Valor") & "')"
                            Else
                                Eval = "([Purch Group] <> '" & row.Item("Valor") & "')"
                            End If

                            If FPG.Length > 0 Then
                                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                                    FPG = FPG & " or " & Eval
                                Else
                                    FPG = FPG & " And " & Eval
                                End If
                            Else
                                FPG = Eval
                            End If
                        Next

                        FPG = "(" & FPG & ")"
                    End If

                    '***********************************************************************
                    ' Configuro el filtro para los P. Orgs
                    '***********************************************************************
                    If POrg.Rows.Count > 0 Then
                        ' ''FPO = "("
                        ' ''For Each row In POrg.Rows
                        ' ''    If FPO.Length > 2 Then
                        ' ''        FPO = FPO & " or "
                        ' ''    End If

                        ' ''    If DBNull.Value.Equals(row.Item("Prefijo")) Then
                        ' ''        FPO = FPO & "[Purch Org] = '" & row.Item("Valor") & "'"
                        ' ''    Else
                        ' ''        If row.Item("Prefijo") = "" Then
                        ' ''            FPO = FPO & "[Purch Org] = '" & row.Item("Valor") & "'"
                        ' ''        Else
                        ' ''            FPO = FPO & "[Purch Org] <> '" & row.Item("Valor") & "'"
                        ' ''        End If
                        ' ''    End If
                        ' ''Next
                        ' ''FPO = FPO & ")"

                        For Each row In POrg.Rows
                            Dim Eval As String = ""

                            If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                                Eval = "([Purch Org] = '" & row.Item("Valor") & "')"
                            Else
                                Eval = "([Purch Org] <> '" & row.Item("Valor") & "')"
                            End If

                            If FPO.Length > 0 Then
                                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                                    FPO = FPO & " or " & Eval
                                Else
                                    FPO = FPO & " And " & Eval
                                End If
                            Else
                                FPO = Eval
                            End If
                        Next

                        FPO = "(" & FPO & ")"
                    End If

                    Dim SQL As String
                    Dim lsFiltro As String = ""

                    Select Case TipoFiltro
                        Case 1
                            lsFiltro = "([Import/Nac] = 'National')" & cn.getExceptions(giDistribution)
                        Case 2
                            lsFiltro = "([Import/Nac] = 'Import')" & cn.getExceptions(giDistribution)
                    End Select

                    SQL = "Select * From vst_BI_ZMR0_Report Where (SAP = '" & Me.cboSAPBox.SelectedValue.ToString & "')" & IIf(lsFiltro.Length > 0, "And (" & lsFiltro & ")", "")

                    If FiltroAvanzado.Length > 0 Then
                        SQL = SQL & " And (" & FiltroAvanzado & ")"
                    End If

                    If FP.Length > 0 Then
                        SQL = SQL & " And " & FP
                    End If

                    If FV.Length > 0 Then
                        SQL = SQL & " And " & FV
                    End If

                    If FPG.Length > 0 Then
                        SQL = SQL & " And " & FPG
                    End If

                    If FPO.Length > 0 Then
                        SQL = SQL & " And " & FPO
                    End If

                    'MsgBox(SQL)

                    dtInfo = cn.RunSentence(SQL).Tables(0)

                    Me.dtgBI.DataSource = dtInfo

                    Me.dtgBI.Columns.Insert(0, cn.AddComboRootCauses("RootCauses", "RootCauses", "Select ID, Descripcion From RootCauses Where Tipo = 3 Order by Descripcion", "ID", "Descripcion"))
                    Me.dtgBI.Columns.Insert(0, cn.AddComboRootCauses("Status", "Status", "Select IdStatus, Description From BI_Status Order by Description", "IdStatus", "Description"))
                    Me.dtgBI.Columns.Insert(0, cn.AddComboRootCauses("R_Code", "R_Code", "Select RC_Code, (Cast(RC_Code as NVarchar) + ' - ' + RC_Description) as Description From BI_Reason_Code Order by RC_Code", "RC_Code", "Description"))

                    If dtgBI.Rows.Count > 0 Then
                        Me.dtgBI.Columns("IdStatus").Visible = False
                        Me.dtgBI.Columns("IdRootCause").Visible = False

                        'Estan ocultas las columnas hasta que se definan cuales van a ser los root causes
                        Me.dtgBI.Columns("RootCauses").Visible = False
                        Me.dtgBI.Columns("IdRootCause").Visible = False

                        Select Case gsUsuarioPC
                            Case "BM4691", "BA8955"
                                Me.dtgBI.Columns("Comment Date").Visible = True
                            Case Else
                                Me.dtgBI.Columns("Comment Date").Visible = False
                        End Select

                    End If

                    GetColors()
                    Grid.SetConfiguration()

                    Me.lblTotal.Text = "Total Blocked Invoice: " & dtInfo.Rows.Count
                    'MsgBox("Done!")
                Else
                    MsgBox("You haven't set any variant as default for this SAPBox.", MsgBoxStyle.Information, "No default variant found")
                End If
            End If
        End If
    End Sub

    Public Sub GetColors()

        Dim drrow As New System.Windows.Forms.DataGridViewRow

        For Each drrow In Me.dtgBI.Rows
            If Not DBNull.Value.Equals(drrow.Cells("IdRootCause").Value) Then
                drrow.Cells("RootCauses").Value = drrow.Cells("IdRootCause").Value
            End If

            If Not DBNull.Value.Equals(drrow.Cells("IdStatus").Value) Then
                drrow.Cells("Status").Value = drrow.Cells("IdStatus").Value
            End If

            'If Not DBNull.Value.Equals(drrow.Cells("Payment Status").Value) AndAlso drrow.Cells("Payment Status").Value > 0 Then
            'drrow.DefaultCellStyle.ForeColor = Drawing.Color.Red
            'Dim BG As Integer
            'BG = RGB(255, 114, 86)

            'drrow.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 114, 86)
            'End If


            Select Case drrow.Cells("Payment Status Detail").Value
                Case "On Time"
                    drrow.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(146, 208, 80)

                Case "Rush"
                    drrow.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 192, 0)

                Case "Over Due"
                    drrow.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(252, 170, 170)

            End Select


            If Not DBNull.Value.Equals(drrow.Cells("Status Code").Value) AndAlso CType(drrow.Cells("Status Code").Value, String) = "0001" Then
                Dim f As Font = Me.dtgBI.DefaultCellStyle.Font

                drrow.DefaultCellStyle.BackColor = System.Drawing.Color.Khaki
                drrow.DefaultCellStyle.ForeColor = Drawing.Color.Black
                drrow.DefaultCellStyle.Font = New Font(f, FontStyle.Strikeout Or FontStyle.Italic)

            End If

        Next
    End Sub
    Private Sub cmdUblock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUblock.Click
        Dim UnBlockedList As New List(Of String)

        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Microsoft.VisualBasic.Left(Me.cboSAPBox.SelectedValue.ToString, 3), gsUsuarioPC, "LAT")

        Me.dtgBI.EndEdit()


        ' DT = cn.RunSentence("Select * From Users Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
        'If DT.Rows.Count > 0 Then

        '    If DBNull.Value.Equals(DT.Rows(0).Item(Me.cboSAPBox.SelectedValue.ToString)) Then
        '        MsgBox("No password found, please type it and try again.", MsgBoxStyle.Information)
        '        Exit Sub
        '    End If

        '    PWR = cn.Encrypt(DT.Rows(0).Item(Me.cboSAPBox.SelectedValue.ToString))

        ' If PWR.Length > 0 Then


        Dim sap As New SAPConection.BI_Release(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, u.password, True, SAPConfig)
        'Dim sap As New SAPConection.BI_Release(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, PWR, True, SAPConfig)
        Dim dr As System.Windows.Forms.DataGridViewRow

        For Each dr In Me.dtgBI.Rows
            If dr.Cells("CK").Value Then
                sap.Include(dr.Cells("Invoice Number").Value, dr.Cells("Vendor").Value, IIf(DBNull.Value.Equals(dr.Cells("Manual").Value) = False, dr.Cells("Manual").Value, ""), dr.Cells("R_Code").Value, dr.Cells("RC Text").Value)
                UnBlockedList.Add("Insert Into BI_Unblocked_History(SAP,[Company Code],[Invoice Number],[Purch Doc],[Line Item],[Reason Code],[Reason Text],Fecha,TNumber) Values(" & _
                                  "'" & dr.Cells("SAP").Value & "'," & _
                                  dr.Cells("Company Code").Value & "," & _
                                  dr.Cells("Invoice Number").Value & "," & _
                                  dr.Cells("Purchase Doc").Value & "," & _
                                  dr.Cells("Line Item1").Value & "," & _
                                  dr.Cells("R_Code").Value & "," & _
                                  "'" & dr.Cells("RC Text").Value & "', {fn Now()},'" & gsUsuarioPC & "')")

                UnBlockedList.Add("Update BI_ZMR0_Report Set [Status Code] = '0001', [Status Code Description] = 'Unblocked' Where " & _
                                 "SAP = '" & dr.Cells("SAP").Value & "' And " & _
                                 "[Company Code] = " & dr.Cells("Company Code").Value & " And " & _
                                 "[Invoice Number] = " & dr.Cells("Invoice Number").Value & " And " & _
                                 "[Purchase Doc] = " & dr.Cells("Purchase Doc").Value & " And " & _
                                 "[Line Item1] = " & dr.Cells("Line Item1").Value)

            End If
        Next
        sap.Execute()
        Dim sql As String
        For Each sql In UnBlockedList
            cn.ExecuteInServer(sql)
        Next
        'End If
        'End If
    End Sub
    Private Sub frm048_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgBI.Width = Me.Width - 30
        Me.dtgBI.Height = Me.Height - 180
    End Sub
    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        cn.ExportDataGridToXL(Me.dtgBI)


        'dtInfo.WriteXml(My.Computer.FileSystem.SpecialDirectories.Desktop & "\Text.xml")
        'Dim DS As New DataSet
        'Dim DT As DataTable = DS.Tables(0)

        'MsgBox(dtgBI.DataSource)

    End Sub
    Private Sub cmdFiltrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltrar.Click
        If Me.txtBuscar.Text.Length > 0 Then
            Dim FiltroAvanzado As String = ""

            FiltroAvanzado = "([Company Code] = " & Me.txtBuscar.Text & _
                 " Or [Invoice Number] = " & Me.txtBuscar.Text & _
                 " Or [lineItem0] like '%" & Me.txtBuscar.Text & "%'" & _
                 " Or [Purchase Doc] = " & Me.txtBuscar.Text & _
                 " Or [Vendor] like '%" & Me.txtBuscar.Text & "%'" & _
                 " Or [Line Item1] = " & Me.txtBuscar.Text & ")"

            GetInfo(FiltroAvanzado)
        Else
            GetInfo()
        End If
    End Sub
    '' ''Private Sub dtgBI_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dtgBI.CellFormatting
    '' ''    Dim f As Font = dtgBI.DefaultCellStyle.Font

    '' ''    If dtgBI.Columns(e.ColumnIndex).Name.Equals("Status Code", StringComparison.InvariantCultureIgnoreCase) Then
    '' ''        If Not DBNull.Value.Equals(dtgBI.Rows(e.RowIndex).Cells("Status Code").Value) AndAlso CType(dtgBI.Rows(e.RowIndex).Cells("Status Code").Value, String) = "0001" Then
    '' ''            'Dim f As Font = Me.dtgBI.DefaultCellStyle.Font

    '' ''            'dtgBI.Rows(e.RowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 32, 96)
    '' ''            ' dtgBI.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Drawing.Color.White
    '' ''            'dtgBI.Rows(e.RowIndex).DefaultCellStyle.Font.Style = New Font(f, FontStyle.Strikeout Or FontStyle.Italic)
    '' ''            'dtgBI.Rows(e.RowIndex).DefaultCellStyle.Font = New Font(f, FontStyle.Strikeout Or FontStyle.Italic)

    '' ''        End If



    '' ''        If e.Value = True AndAlso e.Value IsNot DBNull.Value Then
    '' ''            dtgBI.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Gray
    '' ''            dtgBI.Rows(e.RowIndex).DefaultCellStyle.Font = New Font(f, FontStyle.Strikeout Or FontStyle.Italic)
    '' ''        End If
    '' ''    End If





    '' ''End Sub
    Private Sub dtgBI_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dtgBI.ColumnHeaderMouseClick
        GetColors()
    End Sub
    Private Sub SaveConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveConfigurationToolStripMenuItem.Click
        Grid.SaveConfiguration()
    End Sub
    Private Sub ToolStripButton1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.ButtonClick
        Dim form As New frm028
        Dim Tabla As New DataTable
        Dim i As Integer = 0

        'Cargo la configuración actual del formulario:
        Tabla = cn.RunSentence("Select ID,Mostrar,Columna From Config_Forms Where NombreFormulario = '" & Me.Name & "' And Usuario = '" & gsUsuarioPC & "'").Tables(0)
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

            Grid.GetConfiguration()
            Grid.SetConfiguration()

        Else
            MsgBox("Debe decargar la información antes de configurar el DataGrid", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub dtgBI_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgBI.CellEndEdit
        Me.dtgBI.EndEdit()
        Select Case Me.dtgBI.Columns(e.ColumnIndex).HeaderText.ToUpper
            Case "ROOTCAUSES", "STATUS", "COMMENTS"
                Dim Requi As New DataTable
                Dim curRow As Integer = 0
                Dim curCol As Integer = 0

                curRow = Me.dtgBI.CurrentCell.RowIndex
                curCol = Me.dtgBI.CurrentCell.ColumnIndex

                'Verifico si tiene Root Cause:
                Requi = cn.RunSentence("Select SAP From vst_BI_RootCause_Comments " & _
                                       "Where SAP = '" & Me.cboSAPBox.SelectedValue.ToString & "' And " & _
                                       "[Company Code] = " & Me.dtgBI.Rows(curRow).Cells("Company Code").Value & _
                                       " And [Invoice Number] = " & Me.dtgBI.Rows(curRow).Cells("Invoice Number").Value & _
                                       " And lineItem0 = '" & Me.dtgBI.Rows(curRow).Cells("lineItem0").Value & "'" & _
                                       " And [Purchase Doc] = " & Me.dtgBI.Rows(curRow).Cells("Purchase Doc").Value & _
                                       " And [Line Item 1] = " & Me.dtgBI.Rows(curRow).Cells("Line Item1").Value).Tables(0)

                If Requi.Rows.Count = 0 Then
                    'Si la BI no tiene RootCause le agrego:
                    If (Not Me.dtgBI.Rows(curRow).Cells("RootCauses").Value = Nothing) Or (Not Me.dtgBI.Rows(curRow).Cells("Status").Value = Nothing) Or ((Me.dtgBI.Rows(curRow).Cells("Comments").Value.ToString <> "")) Then
                        cn.ExecuteInServer("Insert Into RootCausesBI Values(" & _
                                                               "'" & Me.cboSAPBox.SelectedValue.ToString & "'," & _
                                                               Me.dtgBI.Rows(curRow).Cells("Company Code").Value & "," & _
                                                               Me.dtgBI.Rows(curRow).Cells("Invoice Number").Value & "," & _
                                                               "'" & Me.dtgBI.Rows(curRow).Cells("lineItem0").Value & "'," & _
                                                               Me.dtgBI.Rows(curRow).Cells("Purchase Doc").Value & "," & _
                                                               Me.dtgBI.Rows(curRow).Cells("Line Item1").Value & "," & _
                                                               IIf(Me.dtgBI.Rows(curRow).Cells("RootCauses").Value = Nothing, 40, Me.dtgBI.Rows(curRow).Cells("RootCauses").Value) & "," & _
                                                               IIf(Me.dtgBI.Rows(curRow).Cells("Status").Value = Nothing, 7, Me.dtgBI.Rows(curRow).Cells("Status").Value) & ", " & _
                                                               "'" & Me.dtgBI.Rows(curRow).Cells("Comments").Value & "', {fn now()})")
                    End If
                Else
                    'Si la BI ya tiene Root Cause la actualizo:
                    cn.ExecuteInServer("Update RootCausesBI set " & _
                                       "IdRootCause = " & IIf(Me.dtgBI.Rows(curRow).Cells("RootCauses").Value = Nothing, 40, Me.dtgBI.Rows(curRow).Cells("RootCauses").Value) & ", " & _
                                       "IdStatus = " & IIf(Me.dtgBI.Rows(curRow).Cells("Status").Value = Nothing, 7, Me.dtgBI.Rows(curRow).Cells("Status").Value) & ", " & _
                                       "Comments = '" & Me.dtgBI.Rows(curRow).Cells("Comments").Value & "'" & _
                                       " Where SAP = '" & Me.cboSAPBox.SelectedValue.ToString & "' And " & _
                                       "[Company Code] = " & Me.dtgBI.Rows(curRow).Cells("Company Code").Value & _
                                       " And [Invoice Number] = " & Me.dtgBI.Rows(curRow).Cells("Invoice Number").Value & _
                                       " And lineItem0 = '" & Me.dtgBI.Rows(curRow).Cells("lineItem0").Value & "'" & _
                                       " And [Purchase Doc] = " & Me.dtgBI.Rows(curRow).Cells("Purchase Doc").Value & _
                                       " And [Line Item 1] = " & Me.dtgBI.Rows(curRow).Cells("Line Item1").Value)

                End If
        End Select
    End Sub

   
End Class