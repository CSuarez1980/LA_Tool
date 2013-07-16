Public Class frm014
    Dim cn As New OAConnection.Connection
    Dim table As New DataTable

    Private Sub frm014_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%

        table = cn.RunSentence("Select * From vstOATracking").Tables(0)
        'table = cn.RunSentence("Select * From vstOATracking Where OwnerMail = 'mejia.m.14@pg.com' order by OA").Tables(0)
        Me.dtgContratos.DataSource = table

        If table.Columns.Count > 0 Then
            i = 0
            Do While i < table.Columns.Count
                cboFiltro.Items.Add(table.Columns(i).ColumnName)
                i = i + 1
            Loop
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.CellSelect
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Filtrar()
    End Sub

    Private Sub txtBuscar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuscar.KeyPress
        If e.KeyChar = Chr(13) Then
            Filtrar()
        End If
    End Sub

    Private Sub Filtrar()
        If txtBuscar.Text <> "" And Me.cboFiltro.Text <> "" Then
            Select Case Table.Columns(cboFiltro.Text).DataType.ToString
                Case "System.Double", "System.Int16", "System.Int32", "System.Int64", "System.Decimal"
                    table.DefaultView.RowFilter = (cboFiltro.Text & " = " & txtBuscar.Text)

                Case "System.String"
                    table.DefaultView.RowFilter = (cboFiltro.Text & " like '%" & txtBuscar.Text & "%'")

                Case "System.Boolean"
                    table.DefaultView.RowFilter = (cboFiltro.Text & " = " & txtBuscar.Text)

            End Select
        Else
            Table.DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim SaveFileDialog As New Windows.Forms.SaveFileDialog
        Dim FileName$

        FileName = "Owners"
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = SaveFileDialog.FileName
            cn.ExportDataTableToXL(table, FileName)
        End If

        MsgBox("Done!.", MsgBoxStyle.Information)
    End Sub
End Class