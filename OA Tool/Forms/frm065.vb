Imports System.Windows.Forms
Imports System.Drawing

Public Class frm065
    Public cn As New OAConnection.Connection
    Public POFilterString As String = ""
    Public MyConfig As New DataTable
    Public dtInfo As DataTable


    Private Sub WriteConfig()

        MyConfig = New DataTable

        If MyConfig.Columns.IndexOf("ColName") = -1 Then
            MyConfig.Columns.Add("ColName")
        End If

        If MyConfig.Columns.IndexOf("Position") = -1 Then
            MyConfig.Columns.Add("Position")
        End If

        If MyConfig.Columns.IndexOf("Visible") = -1 Then
            MyConfig.Columns.Add("Visible", System.Type.GetType("System.Boolean"))
        End If

        If MyConfig.Columns.IndexOf("Width") = -1 Then
            MyConfig.Columns.Add("Width")
        End If

        For Each c As DataGridViewColumn In DG.Columns
            MyConfig.Rows.Add(c.Name, c.DisplayIndex, c.Visible, c.Width)
        Next

        MyConfig.TableName = "MyConfig"

        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\056.xml") Then
            My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\056.xml")
        End If

        MyConfig.WriteXml(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\056.xml", System.Data.XmlWriteMode.WriteSchema)

    End Sub

    Private Sub ReadCongif()
        Dim DS As New DataSet

        If Not My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\056.xml") Then
            WriteConfig()
            ReadCongif()
        Else
            DS.ReadXml(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\056.xml")
            MyConfig = DS.Tables(0)
        End If

    End Sub

    Private Sub SetConfiguration(Optional ByVal LoadFromFile As Boolean = True)
        Try
            If LoadFromFile Then
                ReadCongif()
            End If

            Dim i As Integer = 0

            For i = 0 To DG.ColumnCount
                For Each r As DataRow In MyConfig.Rows
                    If r.Item("Position") = i Then
                        DG.Columns(r.Item("ColName")).DisplayIndex = i
                        DG.Columns(r.Item("ColName")).Visible = r.Item("Visible")
                        DG.Columns(r.Item("ColName")).Width = r.Item("Width")


                        Select Case DG.Columns(r.Item("ColName")).name.ToString.ToUpper

                            Case "CK", "COMMENT UPLOADED", "COMMENT", "REQUEST IMPORTANCE", "REQUEST NEW STATUS", "STATUS", "IMPORTANCE REASON"
                                'DO NOTHING

                            Case Else
                                DG.Columns(r.Item("ColName")).Readonly = True
                                DG.Columns(r.Item("ColName")).DefaultCellStyle.BackColor = Drawing.Color.Gainsboro

                        End Select


                        Exit For
                    End If
                Next



            Next


           

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub frm065_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
    End Sub

    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuario & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")
        End If
    End Sub

    Private Sub cboVariantes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVariantes.SelectedIndexChanged


        If Me.cboVariantes.SelectedText <> "" Then
        Else
            If Not Me.cboVariantes.SelectedValue.ToString <> "System.Data.DataRowView" Then
                Exit Sub
            End If

            ' ''Dim dtPOFilter As DataTable
            ' ''dtPOFilter = cn.RunSentence("Select POFilter From HeaderVariante Where IDVariante = " & Me.cboVariantes.SelectedValue.ToString).Tables(0)

            ' ''If Not DBNull.Value.Equals(dtPOFilter.Rows(0).Item("POFilter")) Then
            ' ''    POFilter = dtPOFilter.Rows(0).Item("POFilter")
            ' ''Else
            ' ''    POFilter = 0
            ' ''End If
        End If
    End Sub

    Private Sub GetInfo(Optional ByVal FiltroAvanzado As String = "")
        DG.DataSource = ""
        DG.Columns.Clear()

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

        Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        MatGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)


        '***********************************************************************
        ' Configuro el filtro para los Plantas
        '***********************************************************************
        If Plantas.Rows.Count > 0 Then
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
            For Each row In Vendors.Rows
                Dim Eval As String = ""

                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                    Eval = "([Vendor] = '" & row.Item("Valor") & "')"
                Else
                    Eval = "([Vendor] <> '" & row.Item("Valor") & "')"
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

        Select Case giDistribution
            Case 1
                lsFiltro = "(([Import/Nac] = 'National')" & cn.getExceptions(giDistribution, ) & ") And "
            Case 2
                lsFiltro = "(([Import/Nac] = 'Import')" & cn.getExceptions(giDistribution) & ") And "
        End Select

        SQL = "Select * From vst_BI_ZFI2_Report Where (" & lsFiltro & " SAP = '" & Me.cboSAPBox.SelectedValue.ToString & "')"

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

        dtInfo = cn.RunSentence(SQL).Tables(0)

        DG.DataSource = dtInfo
        DG.Columns.Insert(0, cn.AddComboRootCauses("Status", "Status", "Select ID, Description From BI_ZFI2_Status_List Order by Description", "ID", "Description"))
        DG.Columns.Insert(0, cn.AddComboRootCauses("Request Importance", "Request Importance", "Select ID, Description From BI_ZFI2_Importance", "ID", "Description"))
        DG.Columns.Insert(0, cn.AddComboRootCauses("Importance Reason", "Importance Reason", "Select Id, Description From BI_ZFI2_Importance_Reason", "ID", "Description"))
        DG.Columns.Insert(0, cn.AddComboRootCauses("Request New Status", "Request New Status", "Select Status From BI_ZFI2_Release_Status_New_Code", "Status", "Status"))
        txtTotal.Text = "Total records found: " & dtInfo.Rows.Count


        Dim DGR As DataGridViewRow

        For Each DGR In DG.Rows
            If Not DBNull.Value.Equals(DGR.Cells("Status ID")) Then
                DGR.Cells("Status").Value = DGR.Cells("Status ID").Value
            End If
        Next


        DG.Columns.Insert(0, cn.AddPictureColumn("Comment", "Comment", 25))
        CkeckComments()

    End Sub

    Private Sub CkeckComments()
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\contents.png")
        Dim C As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\clear.png")

        For Each R As Windows.Forms.DataGridViewRow In DG.Rows

            If Not DBNull.Value.Equals(R.Cells("Last Buyer Comment").Value) Then
                ' If Not DBNull.Value.Equals(R.Cells("Last Buyer Comment").Value) AndAlso (R.Cells("Last Buyer Comment").Value <> "") Then
                R.Cells("Comment").Value = I
                R.Cells("Comment").ToolTipText = "Show comments"
            Else
                R.Cells("Comment").Value = C
            End If


            If Not DBNull.Value.Equals(R.Cells("Last User Release").Value) Then
                Dim f As Font = DG.DefaultCellStyle.Font
                R.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue
                R.DefaultCellStyle.ForeColor = Drawing.Color.Black
                R.DefaultCellStyle.Font = New Font(f, FontStyle.Strikeout Or FontStyle.Italic)
            End If
        Next


    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        GetInfo()
        SetConfiguration()
    End Sub

    Private Sub DG_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG.CellContentClick

        DG.EndEdit()
        Dim curRow As Integer = 0
        Dim curCol As Integer = 0

        curRow = DG.CurrentCell.RowIndex
        curCol = DG.CurrentCell.ColumnIndex

        If DG.Columns(curCol).Name = "Comment" Then
            Dim form As New frm027
            Dim DTC As New DataTable

            DTC = cn.RunSentence("Select * From BI_ZFI2_CommentS161 Where (SAP = '" & cboSAPBox.SelectedValue & "') And (Record = '" & DG.Rows(curRow).Cells("Record").Value & "')").Tables(0)
            If DTC.Rows.Count > 0 Then
                form.dtgComentarios.DataSource = DTC
                form.lblDocumento.Text = "Record"
                form.txtRequisicion.Text = DG.Rows(curRow).Cells("Record").Value
                form.Label2.Text = "Vendor"
                form.txtReqItem.Visible = False
                form.txtGica.Text = DG.Rows(curRow).Cells("Vendor").Value
                form.txtMaterial.Text = DG.Rows(curRow).Cells("Vendor Name").Value
                form.txtSAPBox.Text = cboSAPBox.SelectedValue
                form.ShowDialog()
            End If

        End If

    End Sub

    Private Sub DG_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG.CellValueChanged

        DG.EndEdit()
        Dim curRow As Integer = 0
        Dim curCol As Integer = 0

        curRow = DG.CurrentCell.RowIndex
        curCol = DG.CurrentCell.ColumnIndex

        Select Case DG.Columns(e.ColumnIndex).HeaderText.ToUpper
            Case "STATUS"
                cn.ExecuteInServer("Update BI_ZFI2_Report Set [Status ID] = " & DG.Rows(curRow).Cells("Status").Value & " Where Record = '" & DG.Rows(curRow).Cells("Record").Value & "' And SAP ='" & DG.Rows(curRow).Cells("SAP").Value & "'")

            Case "BUYER COMMENT"
                SaveComment(DG.Rows(curRow).Cells("Record").Value, DG.Rows(curRow).Cells("Buyer Comment").Value)

        End Select

    End Sub

    Private Sub SaveComment(ByVal pRecord As String, ByVal pComment As String)
        Dim _Con As New SqlClient.SqlConnection

        Try
            _Con = New SqlClient.SqlConnection(cn.GetConnectionString)
            _Con.Open()

            Dim cmd As New SqlClient.SqlCommand("Insert Into BI_ZFI2_CommentS161(SAP, Record, Comment, TNumber) Values(@SAP, @Record, @Comment, @UserId) ", _Con)

            cmd.Parameters.AddWithValue("@SAP", Me.cboSAPBox.SelectedValue)
            cmd.Parameters.AddWithValue("@Record", pRecord)
            cmd.Parameters.AddWithValue("@Comment", pComment)
            cmd.Parameters.AddWithValue("@UserId", gsUsuarioPC)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)

        Finally
            If _Con.State = ConnectionState.Open Then
                _Con.Close()
            End If

        End Try

    End Sub

    Private Sub ToolStripButton1_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.ButtonClick
        Dim form As New frm028
        form.dtgConfiguracion.DataSource = MyConfig

        With form.dtgConfiguracion.Columns("ColName")
            .ReadOnly = True
            .Width = 200
        End With

        With form.dtgConfiguracion.Columns("Position")
            .ReadOnly = True
            .Width = 50
            .DisplayIndex = 0
        End With

        With form.dtgConfiguracion.Columns("Visible")
            .Width = 50
        End With

        form.ShowDialog()

        If form.Guardar Then
            MyConfig.AcceptChanges()
            SetConfiguration(False)
            WriteConfig()
        End If
    End Sub

    Private Sub SaveColumnsPositionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveColumnsPositionToolStripMenuItem.Click
        WriteConfig()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComment.Click
        Dim Form As New frm044
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\contents.png")

        DG.EndEdit()

        If DG.RowCount > 0 Then
            Form.ShowDialog()

            If Form.Estado Then
                For Each R As System.Windows.Forms.DataGridViewRow In DG.Rows
                    If R.Cells("Ck").Value Then
                        'MsgBox("Saving")
                        SaveComment(R.Cells("Record").Value, Form.txtComentarios.Text)
                        R.Cells("Comment").Value = I
                        R.Cells("Comment").ToolTipText = "Show comments"
                    End If

                Next
            End If
        End If


    End Sub

    Private Sub DG_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DG.ColumnHeaderMouseClick
        CkeckComments()
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        cn.ExportDataTableToXL(dtInfo)
    End Sub

    Private Sub cmdRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRelease.Click
        DG.EndEdit()

        For Each Row As Windows.Forms.DataGridViewRow In Me.DG.Rows
            If Row.Cells("CK").Value Then
                If Row.Cells("Request New Status").Value = Nothing Then
                    MsgBox("New record status should be selected.", MsgBoxStyle.Exclamation)
                    Row.DefaultCellStyle.BackColor = Drawing.Color.LightSalmon
                Else
                    If Row.Cells("Comment Uploaded").Value Then

                        cn.ExecuteInServer("Insert Into BI_ZFI2_TMPRelease(SAP, Record, [User], Date, LE, Status, Importance, [Imp Reason]) Values ('" & Row.Cells("SAP").Value & "','" & Row.Cells("Record").Value & "','" & gsUsuarioPC & "',{fn now()}, '" & Row.Cells("Code").Value & "','" & Row.Cells("Request New Status").Value & "'," & IIf(Row.Cells("Request Importance").Value <> Nothing, Row.Cells("Request Importance").Value, 0) & "," & IIf(Row.Cells("Importance Reason").Value <> Nothing, Row.Cells("Importance Reason").Value, 0) & ")")
                        Row.Cells("Ck").Value = False

                        Dim f As Font = DG.DefaultCellStyle.Font
                        Row.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue
                        Row.DefaultCellStyle.ForeColor = Drawing.Color.Black
                        Row.DefaultCellStyle.Font = New Font(f, FontStyle.Strikeout Or FontStyle.Italic)
                    Else
                        MsgBox("Record: " & Row.Cells("Record").Value & " without comment?" & Chr(13) & Chr(13) & "If comments were uploaded in SAP, please check upload comment column; then try again.", MsgBoxStyle.Question)
                        Row.DefaultCellStyle.BackColor = Drawing.Color.LightSalmon
                    End If
                End If


            End If
        Next
    End Sub
End Class