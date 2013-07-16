Public Class frm026
    Public Estado As Boolean = False

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Estado = False
        Me.Visible = False
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        If Not Me.cmdCopyComment.Checked Then
            Dim lsComentario As String = ""
            Dim cn As New OAConnection.Connection

            lsComentario = Replace(Replace(Me.txtComentarios.Text, "'", ""), ",", "")
            cn.ExecuteInServer("Insert Into Comentarios_Requis(Requisicion,Item,Comentario,Fecha,Usuario,SAPBox,Status,Planta,MatGrp,PGrp,POrg) Values(" & Me.txtRequisicion.Text & "," & Me.txtReqItem.Text & ",'" & lsComentario & "', {fn now()}, '" & cn.GetUserId & "','" & Me.txtSAPBox.Text & "','" & Me.txtStatus.Text & "','" & Me.txtPlanta.Text & "','" & Me.txtMatGrp.Text & "','" & Me.txtPGrp.Text & "','" & Me.txtPOrg.Text & "')")
            MsgBox("Comentario Guardado.", MsgBoxStyle.Information)
        End If

        Estado = True
        Me.Visible = False
    End Sub


    Private Sub cmdCopyComment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyComment.Click
        Me.cmdCopyComment.Checked = Not Me.cmdCopyComment.Checked
    End Sub
End Class