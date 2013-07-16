Public Class frm051
    Dim cn As New OAConnection.Connection
    Dim Table As New DataTable

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim FileName$
        Dim OpenFileDialog As New Windows.Forms.OpenFileDialog

        FileName = ""
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        OpenFileDialog.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
        End If

        If FileName <> "" Then
            Table = cn.GetXLTable(FileName, "Sheet1", "Select * From [Sheet1$]").Tables(0)
            Me.dtgAPTrade.DataSource = Table
        End If
    End Sub

    Private Sub cmdUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        cn.ExecuteInServer("Delete From BI_APTrade")

        If Table.Rows.Count > 0 Then
            If cn.AppendTableToSqlServer("BI_APTrade", Table) Then
                'Cargar los materiales del ATrade al ZMR0 Report:
                cn.ExecuteInServer("Insert Into BI_ZMR0_Report Select * From vst_BI_POsFromAPTrade")

                MsgBox("Upload sucessfully uploaded.", MsgBoxStyle.Information)
            Else
                MsgBox("Error trying to upload file", MsgBoxStyle.Critical)
            End If
        Else
            MsgBox("No rows could be found.")
        End If
    End Sub

    Private Sub cmdExportTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExportTemplate.Click
        Dim Template As New DataTable

        Template = cn.RunSentence("Select * From BI_APTrade Where Box = 'XXX'").Tables(0)
        cn.ExportDataTableToXL(Template)
    End Sub

   

End Class