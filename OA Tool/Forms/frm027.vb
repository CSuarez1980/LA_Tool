Public Class frm027

    Private Sub frm027_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim cn As New OAConnection.Connection
        'Dim Tabla As New DataTable

        'Tabla = cn.RunSentence("Select * From Comentarios_Requis Where Requisicion = " & Me.txtRequisicion.Text & " And Item = " & Me.txtReqItem.Text & " And SAPBox = '" & Me.txtSAPBox.Text & "'").Tables(0)
        'If Tabla.Rows.Count > 0 Then
        '    Me.dtgComentarios.DataSource = Tabla
        'Else
        '    MsgBox("Esta requisición no tiene comentarios", MsgBoxStyle.Information)
        'End If
    End Sub
End Class