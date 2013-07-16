Public Class frm033
    Dim cn As New OAConnection.Connection

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim cn As New OAConnection.Connection
        Dim Tabla As New DataTable

        If (txtEstacionario.Text.Trim.Length) > 0 Then
            Try
                cn.ExecuteInServer("Insert Into Estacionarios(Descripcion,IdUsuario,IdIdioma,Asunto,Mensage) Values('" & Me.txtEstacionario.Text.Trim & "','" & gsUsuarioPC & "','" & Me.cboIdioma.SelectedValue.ToString & "','" & Me.txtAsunto.Text.Trim & "','" & Me.txtMensage.Text.Trim & "')")
                MsgBox("Done!", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub frm033_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboIdioma, "Select IDIdioma,Descripcion From Idioma", "IDIdioma", "Descripcion")
    End Sub
End Class