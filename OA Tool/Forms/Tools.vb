Public Class Tools
    Public Function AddCheckBoxColumn(ByVal ColumnTitle$) As Windows.Forms.DataGridViewCheckBoxColumn
        Dim CheckColumn As New Windows.Forms.DataGridViewCheckBoxColumn

        CheckColumn.HeaderText = ColumnTitle
        CheckColumn.FlatStyle = Windows.Forms.FlatStyle.Popup
        CheckColumn.Width = 45
        Return CheckColumn
    End Function
End Class
