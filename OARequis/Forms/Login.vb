Public Class Login

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If Me.txtUser.Text.Length = 0 Then
            MsgBox("Por favor ingrse su numero de usuario.", MsgBoxStyle.Exclamation, "Usuario inválido")
            Exit Sub
        End If

        If Me.txtPwr.Text.Length = 0 Then
            MsgBox("Por favor ingrse su password.", MsgBoxStyle.Exclamation, "Password inválido")
            Exit Sub
        End If

        gUser = Me.txtUser.Text
        gPwr = Me.txtPwr.Text

        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        gUser = ""
        gPwr = ""
        Me.Close()
    End Sub
End Class