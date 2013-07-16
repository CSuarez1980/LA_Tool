Public Class frm049
    Dim cn As New OAConnection.Connection
    Dim Tabla As New DataTable

    Private Sub frm049_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Tabla = cn.RunSentence("Select * From vst_Requi_Analisis Where [User] = '" & gsUsuarioPC & "'").Tables(0)
        dtgRequis.DataSource = Tabla
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        cn.ExportDataTableToXL(Tabla)
    End Sub

    Private Sub frm049_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgRequis.Width = Me.Width - 30
        Me.dtgRequis.Height = Me.Height - 100
    End Sub
End Class