Public Class frm089
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim cn As New OAConnection.Connection

        'cn.ExecuteInServer("Delete From BR_NCM_Changes_Pass")
        If txtL7P.Text.Length > 0 Then
            cn.RunSentence("Update BR_NCM_Changes_Pass Set TNumber = '" & gsUsuarioPC & "', Pass = '" & cn.Encrypt(txtL7P.Text) & "' Where SAPBox = 'L7P'")
        End If

        If txtL6P.Text.Length > 0 Then
            cn.RunSentence("Update BR_NCM_Changes_Pass Set TNumber = '" & gsUsuarioPC & "', Pass = '" & cn.Encrypt(txtL6P.Text) & "' Where SAPBox = 'L6P'")
        End If

        If txtG4P.Text.Length > 0 Then
            cn.RunSentence("Update BR_NCM_Changes_Pass Set TNumber = '" & gsUsuarioPC & "', Pass = '" & cn.Encrypt(txtG4P.Text) & "' Where SAPBox = 'G4P'")
        End If

        If txtGBP.Text.Length > 0 Then
            cn.RunSentence("Update BR_NCM_Changes_Pass Set TNumber = '" & gsUsuarioPC & "', Pass = '" & cn.Encrypt(txtGBP.Text) & "' Where SAPBox = 'GBP'")
        End If

        MsgBox("Passwords saved", MsgBoxStyle.Information)
    End Sub
End Class