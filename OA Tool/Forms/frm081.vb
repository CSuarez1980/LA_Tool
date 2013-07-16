Public Class frm081
    Private Data As New DataTable

    Private Sub frm081_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BGW.RunWorkerAsync()
    End Sub
    Private Sub BGW_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGW.DoWork
        Dim cn As New OAConnection.Connection
        Data = cn.RunSentence("Select * From vst_BI_ZMR0_Report").Tables(0)
    End Sub
    Private Sub BGW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGW.RunWorkerCompleted
        dtgData.DataSource = Data
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim cn As New OAConnection.Connection
        cn.ExportDataTableToXL(Data)
    End Sub

End Class