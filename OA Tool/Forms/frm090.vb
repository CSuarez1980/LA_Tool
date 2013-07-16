Public Class frm090
    Dim Data As New DataTable

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim cn As New OAConnection.Connection
        Data = cn.RunSentence("Select * From vst_Transmission Where ([Sent date] between cast('" & dtpFrom.Value.ToShortDateString & "' + ' 12:00 AM' as datetime) And cast('" & dtpTo.Value.ToShortDateString & "' + ' 11:59 PM' as datetime))").Tables(0)
        BS.DataSource = Data
        dtgData.DataSource = BS
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        If Data.Rows.Count > 0 Then
            Dim cn As New OAConnection.Connection
            cn.ExportDataTableToXL(Data)
        Else
            MsgBox("There is no data to be exported.", MsgBoxStyle.Exclamation, "No data found")
        End If
    End Sub
End Class