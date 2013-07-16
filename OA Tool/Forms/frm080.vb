Public Class frm080
    Public cn As New OAConnection.Connection
    Public Table As DataTable

    Private Sub frm080_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BS.DataSource = cn.RunSentence("Select * From vst_Status_161_Release Where SAP = 'XXX'").Tables(0)
        dtgBI.DataSource = BS
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim sql As String = ""
        Dim Filter As String = ""

        sql = "Select * From vst_Status_161_Release Where ([Release Date] between '" & dtpInicio.Value & "' And '" & dtpFin.Value & "')"

        If Me.txtFilter.Text.Length > 0 Then
            Filter = "([Comp Code] = " & Me.txtFilter.Text & _
                 " Or [Record] = " & Me.txtFilter.Text & _
                 " Or [Plant] = " & Me.txtFilter.Text & ")"
            sql = sql & " And " & Filter
        End If

        sql = sql & " order by [Release Date]"
        Table = cn.RunSentence(sql).Tables(0)
        BS.DataSource = Table
    End Sub

    Private Sub GetCount() Handles cmdQuery.Click
        Me.txtTotal.Text = dtgBI.Rows.Count
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        cn.ExportDataTableToXL(Table)
    End Sub
End Class