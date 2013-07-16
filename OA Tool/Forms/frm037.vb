Public Class frm037
    Dim cn As New OAConnection.Connection

    Private Sub EnableEdit()
        Me.cmdGuardar.Enabled = True
        Me.cmdModificar.Enabled = False

        Me.txtHeaderText.Enabled = True
        Me.txtHeaderNote.Enabled = True
        
    End Sub

    Private Sub DisableEdit()
        Me.cmdGuardar.Enabled = False
        Me.cmdModificar.Enabled = True

        Me.txtHeaderText.Enabled = False
        Me.txtHeaderNote.Enabled = False
    End Sub

    Private Sub cmdModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        EnableEdit()
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        DisableEdit()
        Try
            cn.ExecuteInServer("Update HeaderText_Note Set HeaderText = '" & Me.txtHeaderText.Text & "', HeaderNote = '" & Me.txtHeaderNote.Text & "' Where IDComentarios = " & Me.cboTextos.SelectedValue.ToString & " And TNumber = '" & gsUsuarioPC & "'")
            MsgBox("Done!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frm037_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboTextos, "SELECT IDComentarios, Descripcion FROM HeaderText_Note Where TNumber = '" & gsUsuarioPC & "'", "IDComentarios", "Descripcion")
    End Sub

    Private Sub cboTextos_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTextos.SelectionChangeCommitted
        Dim Textos As New DataTable

        Textos = cn.RunSentence("Select * From HeaderText_Note Where IDComentarios = " & Me.cboTextos.SelectedValue.ToString).Tables(0)
        If Textos.Rows.Count > 0 Then
            Me.txtHeaderText.Text = Textos.Rows(0).Item("HeaderText")
            Me.txtHeaderNote.Text = Textos.Rows(0).Item("HeaderNote")
        Else
            MsgBox("ID de comentario no encontrado.", MsgBoxStyle.Critical)
        End If
    End Sub
End Class