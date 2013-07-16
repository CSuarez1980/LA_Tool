Public Class frm031
    Public Estado As Boolean = False

    Private Sub cmdCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Estado = False
        Me.Visible = False
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim lsComentario As String = ""
        Dim cn As New OAConnection.Connection

        lsComentario = Replace(Replace(Me.txtComentarios.Text, "'", ""), ",", "")
        cn.ExecuteInServer("Insert Into Comentarios_PO(PurchOrder,Item,Comentario,Fecha,Usuario,SAPBox,Status,Planta,MatGrp,PGrp,POrg) Values(" & Me.txtRequisicion.Text & "," & Me.txtReqItem.Text & ",'" & lsComentario & "', {fn now()}, '" & cn.GetUserId & "','" & Me.txtSAPBox.Text & "','" & Me.txtStatus.Text & "','" & Me.txtPlanta.Text & "','" & Me.txtMatGrp.Text & "','" & Me.txtPGrp.Text & "','" & Me.txtPOrg.Text & "')")
        MsgBox("Comentario Guardado.", MsgBoxStyle.Information)
        Estado = True
        Me.Visible = False
    End Sub
End Class