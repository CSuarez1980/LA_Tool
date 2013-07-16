Public Class frm071
    Public Data As New DataTable

    Private Sub frm071_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cn As New OAConnection.Connection

        Data = cn.RunSentence("Select * From vst_OA_Detalle_Contrato_Master_Data").Tables(0)

        dtgData.DataSource = Data
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim cn As New OAConnection.Connection

        cn.ExportDataTableToXL(Data)
    End Sub
End Class