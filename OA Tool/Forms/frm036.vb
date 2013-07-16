Public Class frm036
    Dim cn As New OAConnection.Connection

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            cn.ExecuteInServer("Insert Into HeaderText_Note(Descripcion,TNumber,HeaderText,HeaderNote) values('" & Me.txtNombre.Text & "','" & gsUsuarioPC & "','" & Me.txtHeaderText.Text & "','" & Me.txtHeaderNote.Text & "')")
            MsgBox("Done!", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

End Class