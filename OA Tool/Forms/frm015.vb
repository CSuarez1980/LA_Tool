Public Class frm015

     Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.txtContrato.Text.Length > 0 Then
            Dim cn As New OAConnection.Connection
            cn.SetOAWithBSSTicket(Trim(Me.txtContrato.Text))
            MsgBox("Done.", MsgBoxStyle.Information)

        Else
            MsgBox("No se ha ingresado ningún código de contrato.", MsgBoxStyle.Information)
        End If
    End Sub

End Class