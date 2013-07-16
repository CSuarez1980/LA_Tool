Public Class frm055
    Public cn As New OAConnection.Connection
    Public Table As DataTable

    Private Sub frm055_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BS.DataSource = cn.RunSentence("Select * From vst_BI_Unblocked_Report Where SAP = 'XXX'").Tables(0)
        dtgBI.DataSource = BS
    End Sub

    Private Sub frm055_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgBI.Width = Me.Width - 20
        Me.dtgBI.Height = Me.Height - 180
    End Sub

    Private Sub cmdQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuery.Click
        Dim sql As String = ""
        Dim Filter As String = ""

        sql = "Select * From vst_BI_Unblocked_Report Where (Unblocked between '" & dtpInicio.Value & "' And '" & dtpFin.Value & "')"

        If Me.txtFilter.Text.Length > 0 Then

            Filter = "([Company Code] = " & Me.txtFilter.Text & _
                 " Or [Invoice Number] = " & Me.txtFilter.Text & _
                 " Or [lineItem0] like '%" & Me.txtFilter.Text & "%'" & _
                 " Or [Purchase Doc] = " & Me.txtFilter.Text & _
                 " Or [Vendor] like '%" & Me.txtFilter.Text & "%'" & _
                 " Or [Line Item1] = " & Me.txtFilter.Text & ")"

            'Filtro para multiple.

            sql = sql & " And " & Filter
        End If
        sql = sql & " order by UnBlocked"
        Table = cn.RunSentence(sql).Tables(0)
        'BS.DataSource = cn.RunSentence("Select * From vst_BI_Unblocked_Report Where Unblocked between '" & dtpInicio.Value & "' And '" & dtpFin.Value & "' order by UnBlocked").Tables(0)
        BS.DataSource = Table
    End Sub

    Private Sub GetCount() Handles cmdQuery.Click
        Me.txtTotal.Text = dtgBI.Rows.Count
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        'cn.ExportDataGridToXL(dtgBI)

        cn.ExportDataTableToXL(Table)
        

        'Table.WriteXml(My.Computer.FileSystem.SpecialDirectories.Desktop & "\MyFile.xml")

    End Sub
End Class