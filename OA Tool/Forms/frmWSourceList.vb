Public Class frmWSourceList
    Private Sub frmWSourceList_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        Me.Opacity = 1
    End Sub

    Private Sub frmWSourceList_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        Me.Opacity = 0.5
    End Sub

    Private Sub frmWSourceList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cn As New OAConnection.Connection
        Dim Tabla As New DataTable

        Tabla = cn.RunSentence("SELECT Owner, OwnerMail, COUNT(OA) AS [OA's'] FROM dbo.vstContratosVencidos GROUP BY Owner, OwnerMail ORDER BY COUNT(OA) DESC, Owner").Tables(0)

        Me.dtgVencidos.DataSource = tabla
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label1.MouseEnter
        Me.Opacity = 1
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub PictureBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.MouseEnter
        Me.Opacity = 1
    End Sub

    Private Sub dtgVencidos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgVencidos.CellContentClick

    End Sub

    Private Sub dtgVencidos_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtgVencidos.MouseEnter
        Me.Opacity = 1
    End Sub
End Class