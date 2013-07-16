Public Class frm010
    Dim cn As New OAConnection.Connection
    Dim Table As New DataTable
    Dim Access As New Access.Application

    Private Sub Filtrar()
        If txtBuscar.Text <> "" And Me.cboFiltro.Text <> "" Then
            Select Case Table.Columns(cboFiltro.Text).DataType.ToString
                Case "System.Double", "System.Int16", "System.Int32", "System.Int64"
                    Table.DefaultView.RowFilter = (cboFiltro.Text & " = " & txtBuscar.Text)

                Case "System.String"
                    Table.DefaultView.RowFilter = (cboFiltro.Text & " like '%" & txtBuscar.Text & "%'")

            End Select
        Else
            Table.DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Filtrar()
    End Sub

    Private Sub txtBuscar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuscar.KeyPress
        If e.KeyChar = Chr(13) Then
            Filtrar()
        End If
    End Sub

    Private Sub frm010_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
    End Sub

    Private Sub frm010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%

        Table = cn.RunSentence("Select * From vstOwners where OwnerMail = '" & cn.GetUserMail & "' order by OA").Tables(0)
        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

        Me.dtgContratos.DataSource = Table

        If Table.Columns.Count > 0 Then
            i = 0
            Do While i < Table.Columns.Count
                cboFiltro.Items.Add(Table.Columns(i).ColumnName)
                i = i + 1
            Loop
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim SaveFileDialog As New Windows.Forms.SaveFileDialog
        Dim FileName$

        FileName = "Owners"
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = SaveFileDialog.FileName
            Access.Run("ExportOwnersToXL", FileName)
        End If

        MsgBox("Done!.", MsgBoxStyle.Information)
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim OpenFileDialog As New Windows.Forms.OpenFileDialog
        Dim FileName$

        FileName = ""
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
        End If

        If FileName <> "" Then
            Table = cn.GetXLTable(FileName, "dbo_vstOwners", "Select * From [dbo_vstOwners$]").Tables(0)
            Me.dtgContratos.DataSource = Table
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.CellSelect
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        Dim i%
        Dim cn As New OAConnection.Connection
        Dim Tabla As DataTable
        Dim Found As Boolean
        Dim count%

        For i = 0 To Me.dtgContratos.RowCount - 1
            Tabla = cn.RunSentence("Select OA From Distribucion").Tables(0)

            Found = False
            count = 0
            Do While count < Tabla.Rows.Count Or Found
                If Tabla.Rows(count).Item("OA") = Me.dtgContratos.Rows(i).Cells("OA").Value Then
                    Found = True
                    Exit Do
                End If
                count = count + 1
            Loop

            If Found Then
                cn.ExecuteInServer("Update Distribucion set Owner = '" & Me.dtgContratos.Rows(i).Cells("Owner").Value & "', OwnerMail = '" & Me.dtgContratos.Rows(i).Cells("OwnerMail").Value & "' Where OA = " & Me.dtgContratos.Rows(i).Cells("OA").Value)
            Else
                cn.ExecuteInServer("Insert Into Distribucion Values(" & Me.dtgContratos.Rows(i).Cells("OA").Value & ",'" & Me.dtgContratos.Rows(i).Cells("Owner").Value & "','" & Me.dtgContratos.Rows(i).Cells("OwnerMail").Value & "')")
            End If
        Next

        MsgBox("Changes saved.", MsgBoxStyle.Information, "Changes saved")
    End Sub
End Class