Public Class frm028
    Public Guardar As Boolean = False

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.Close()
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Guardar = True
        Me.dtgConfiguracion.EndEdit()
        Me.Hide()
    End Sub
End Class