Public Class frm044
    Public Estado As Boolean = False

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Estado = False
        Me.Visible = False
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Estado = True
        Me.Visible = False
    End Sub

End Class