Public Class frm054
    Dim cn As New OAConnection.Connection
    Dim Table As DataTable

    Private Sub frm054_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Table = cn.RunSentence("Select * From vst_Anual_Spend_Resumen").Tables(0)
        Me.BS.DataSource = Table
        Me.dtgSpend.DataSource = BS
        Me.txtCantidad.Text = Table.Rows.Count
        FormatGrid()

    End Sub

    Public Sub FormatGrid()
        With dtgSpend
            .Columns("Total Spend").DefaultCellStyle.Format = "c"
            .Columns("Total Spend").DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleRight
        End With
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        cn.ExportDataTableToXLFromClipBoard(Table)
        MsgBox("Done!")
    End Sub

    Private Sub frm054_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgSpend.Width = Me.Width - 20
        Me.dtgSpend.Height = Me.Height - 100
    End Sub

End Class