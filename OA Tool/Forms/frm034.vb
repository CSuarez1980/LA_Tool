Public Class frm034
    Dim cn As New OAConnection.Connection

    Private Sub EnableEdit()
        Me.cmdGuardar.Enabled = True
        Me.cmdModificar.Enabled = False
        Me.txtAsunto.Enabled = True
        Me.txtMensage.Enabled = True
        Me.cboEstacionarios.Enabled = False
        Me.cboIdioma.Enabled = False
    End Sub

    Private Sub DisableEdit()
        Me.cmdGuardar.Enabled = False
        Me.cmdModificar.Enabled = True
        Me.txtAsunto.Enabled = False
        Me.txtMensage.Enabled = False
        Me.cboEstacionarios.Enabled = True
        Me.cboIdioma.Enabled = True
    End Sub

    Private Sub cmdModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        EnableEdit()
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        DisableEdit()
        Try
            cn.ExecuteInServer("Update Estacionarios set Asunto = '" & Me.txtAsunto.Text.Trim & "', Mensage = '" & Me.txtMensage.Text.Trim & "' Where IDEstacionario = " & Me.cboEstacionarios.SelectedValue.ToString)
            MsgBox("Done!")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frm034_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboIdioma, "Select IDIdioma,Descripcion From Idioma", "IDIdioma", "Descripcion")
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuarioPC & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub CargarEstacionarios()
        Dim Tabla As New DataTable

        If (Me.cboIdioma.SelectedValue.ToString <> "System.Data.DataRowView") Then

            Tabla = cn.RunSentence("Select * From Estacionarios Where IdEstacionario = " & Me.cboEstacionarios.SelectedValue.ToString & " And IdUsuario = '" & gsUsuarioPC & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'").Tables(0)

            If Tabla.Rows.Count > 0 Then
                Me.txtAsunto.Text = Tabla.Rows(0).Item("Asunto")
                Me.txtMensage.Text = Tabla.Rows(0).Item("Mensage")
            End If
        End If
    End Sub

    Private Sub cboIdioma_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboIdioma.SelectionChangeCommitted
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuarioPC & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub cboEstacionarios_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstacionarios.SelectionChangeCommitted
        CargarEstacionarios()
    End Sub
End Class