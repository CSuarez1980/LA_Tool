Public Class frm041
    Dim cn As New OAConnection.Connection
    Dim BackUpTo As String = ""
    Dim Tabla As New DataTable

    Private Sub frm041_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Tabla = cn.RunSentence("Select * From [Users] Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
        If Tabla.Rows.Count > 0 Then

            BackUpTo = Tabla.Rows(0).Item("MakingBackUpTo")

            If Tabla.Rows(0).Item("MakingBackUp") Then
                Me.txtStatus.Text = "Haciendo BackUp"
            Else
                Me.txtStatus.Text = "Normal"
            End If

            cn.LoadCombo(Me.cboUsers, "Select TNumber, Nombre From [Users] Where TNumber <> 'BM4691' Order by Nombre", "TNumber", "Nombre")
        Else
            MsgBox("No se ha encontrado su TNumber en la base de datos", MsgBoxStyle.Information, "Usuario no encontrado")
        End If
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        cn.ExecuteInServer("Update [Users] set MakingBackUp = 1, MakingBackUpTo = '" & Me.cboUsers.SelectedValue.ToString & "' Where TNumber = '" & gsUsuarioPC & "'")
        cn.ExecuteInServer("Update [Users] set BackUped = 1, BackUpedBy = '" & gsUsuarioPC & "' Where TNumber = '" & Me.cboUsers.SelectedValue.ToString & "'")
        MsgBox("Configuración de BackUp Aceptada", MsgBoxStyle.Information)
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        cn.ExecuteInServer("Update [Users] set MakingBackUp = 0 Where TNumber = '" & gsUsuarioPC & "'")
        cn.ExecuteInServer("Update [Users] set BackUped = 0 Where BackUpedBy = '" & gsUsuarioPC & "'")
        MsgBox("Perfil de BackUp Removido", MsgBoxStyle.Information)

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        cn.ExecuteInServer("Update [Users] set MakingBackUp = 0 Where MakingBackUpTo = '" & gsUsuarioPC & "'")
        cn.ExecuteInServer("Update [Users] set BackUped = 0 Where TNumber = '" & gsUsuarioPC & "'")
        cn.ExecuteInServer("Update [Users] set BackUped = 0 Where TNumber = '" & BackUpTo & "'")
        MsgBox("Perfiles de BackUp Removidos", MsgBoxStyle.Information)
    End Sub
End Class