Public Class frmDestino_Origen
    Dim cn As New OAConnection.Connection

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Me.dtgOrigen.EndEdit()
        cn.ExecuteInServer("Delete From Origen_Destino Where IDDestino = " & Me.txtCodDestino.Text)
        Dim i As Integer

        For i = 0 To Me.dtgOrigen.Rows.Count - 1
            If Me.dtgOrigen.Rows(i).Cells("Link").Value = True Then
                cn.ExecuteInServer("Insert into Origen_Destino( IDDestino,IDOrigen) Values(" & Me.txtCodDestino.Text & "," & Me.dtgOrigen.Rows(i).Cells("IDOrigen").Value & ")")
            End If
        Next
    End Sub

    Private Sub frmDestino_Origen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Tabla As New DataTable
        Dim Origenes As New DataTable

        Tabla = cn.RunSentence("Select * From Origen").Tables(0)

        If Tabla.Rows.Count > 0 Then
            Me.dtgOrigen.DataSource = Tabla
            Me.dtgOrigen.Columns.Insert(0, cn.AddCheckBoxColumn("Link", "Link"))
        End If

        Origenes = cn.RunSentence("Select * From Origen_Destino Where IDDestino = " & Me.txtCodDestino.Text).Tables(0)
        If Origenes.Rows.Count > 0 Then
            Dim i As Integer
            Dim j As Integer

            For i = 0 To Origenes.Rows.Count - 1
                For j = 0 To Me.dtgOrigen.Rows.Count - 1
                    If Not Me.dtgOrigen.Rows(j).Cells("IDOrigen").Value <> Origenes.Rows(i).Item("IDOrigen") Then
                        Me.dtgOrigen.Rows(j).Cells("Link").Value = True
                    End If
                Next
            Next
        End If
    End Sub
End Class