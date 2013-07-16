Public Class Working
    Public Active As Boolean = False

    Private Sub Working_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.BringToFront()
    End Sub

    Private Sub Working_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        Active = True
    End Sub

    Private Sub Working_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

    End Sub
End Class