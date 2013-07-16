Public Class frm017
    Dim cn As New OAConnection.Connection
    Dim Tabla As New DataTable

    Private Sub frm017_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%
        Tabla = cn.RunSentence("Select * From vstContrato_MasterData Where PDT_OA <> PDT_MD").Tables(0)
        Me.dtgContratos.DataSource = Tabla

        If Tabla.Columns.Count > 0 Then
            i = 0
            Do While i < Tabla.Columns.Count
                cboFiltro.Items.Add(Tabla.Columns(i).ColumnName)
                i = i + 1
            Loop
        End If

        Me.txtMateriales.Text = Tabla.Rows.Count
    End Sub

    Private Sub Filtrar()
        If txtBuscar.Text <> "" And Me.cboFiltro.Text <> "" Then
            Select Case Tabla.Columns(cboFiltro.Text).DataType.ToString
                Case "System.Double", "System.Int16", "System.Int32", "System.Int64"
                    Tabla.DefaultView.RowFilter = (cboFiltro.Text & " = " & txtBuscar.Text)

                Case "System.String"
                    Tabla.DefaultView.RowFilter = (cboFiltro.Text & " like '%" & txtBuscar.Text & "%'")

            End Select
        Else
            Tabla.DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Filtrar()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.CellSelect
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub txtBuscar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuscar.KeyPress
        If e.KeyChar = Chr(13) Then
            Filtrar()
        End If
    End Sub

    Private Sub txtBuscar_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscar.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class