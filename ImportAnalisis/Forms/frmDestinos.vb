Public Class frmDestinos

    Dim T_Origen As DataSet
    Dim cn As New OAConnection.Connection

    Private Sub frmDestinos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        T_Origen = cn.RunSentence("Select * From Destino")
        Me.dtgOrigen.DataSource = T_Origen.Tables(0)
        Me.dtgOrigen.Columns("IDDestino").ReadOnly = True
    End Sub

    Private Sub btnEditar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditar.Click
        Me.dtgOrigen.AllowUserToAddRows = True
        Me.dtgOrigen.AllowUserToDeleteRows = True
        Me.btnEliminar.Enabled = True
        Me.btnGuardar.Enabled = True
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim i As Integer
        Me.dtgOrigen.EndEdit()
        Me.dtgOrigen.AllowUserToAddRows = False
        Me.dtgOrigen.AllowUserToDeleteRows = False

        For i = 0 To Me.dtgOrigen.RowCount - 1
            If Me.dtgOrigen.Rows(i).Cells("IDDestino").Value.ToString = "" Then
                If Me.dtgOrigen.Rows(i).Cells("Descripcion").Value.ToString.Trim <> "" Then
                    cn.ExecuteInServer("Insert Into Destino(Descripcion,ValorHonorario) Values('" & Me.dtgOrigen.Rows(i).Cells("Descripcion").Value.ToString.ToUpper & "'," & Me.dtgOrigen.Rows(i).Cells("ValorHonorario").Value.ToString & ")")
                End If
            Else
                cn.ExecuteInServer("Update Destino set Descripcion = '" & Me.dtgOrigen.Rows(i).Cells("Descripcion").Value.ToString.ToUpper & "', ValorHonorario = " & Me.dtgOrigen.Rows(i).Cells("ValorHonorario").Value.ToString & " Where IDDestino = " & Me.dtgOrigen.Rows(i).Cells("IDDestino").Value.ToString)
            End If
        Next

        Me.btnEliminar.Enabled = False
        Me.btnGuardar.Enabled = False
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If MsgBox("Seguro que desea eliminar el Destino: " & Me.dtgOrigen.SelectedRows.Item(0).Cells("Descripcion").Value & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Eliminar el Destino de Importación") = MsgBoxResult.Yes Then
            If Me.dtgOrigen.SelectedRows.Item(0).Cells("IDOrigen").Value.ToString <> "" Then
                cn.ExecuteInServer("Delete From Destino Where IDOrigen = " & Me.dtgOrigen.SelectedRows.Item(0).Cells("IDOrigen").Value)
            End If
        End If
    End Sub

    Private Sub cmdOrigen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOrigen.Click
        Dim Form As New frmDestino_Origen

        Form.txtCodDestino.Text = Me.dtgOrigen.SelectedRows.Item(0).Cells("IDDestino").Value
        Form.txtDestino.Text = Me.dtgOrigen.SelectedRows.Item(0).Cells("Descripcion").Value

        Form.Show()

    End Sub
End Class