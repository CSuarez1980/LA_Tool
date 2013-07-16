Public Class frm029
    Public cn As New OAConnection.Connection

    Private Sub frm029_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        'Dim c As New SAPCOM.SAPConnector
        'c.SetPwd(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, Me.txtContraseña.Text.ToLower)
        'c.TestConnection(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
        'If c.Status = "" Then
        '    cn.ExecuteInServer("Update Users Set " & Me.cboSAPBox.SelectedValue.ToString & " = '" & cn.Encrypt(Me.txtContraseña.Text.ToLower) & "' Where TNumber ='" & gsUsuarioPC & "'")
        '    MsgBox("Acceso a SAP verificado.", MsgBoxStyle.Information)
        'Else
        '    MsgBox(c.Status, MsgBoxStyle.Information, "Error registrando el acceso")
        'End If

    End Sub
End Class