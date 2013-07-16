Public Class frm059
    Private dtVendors As DataTable
    Private cn As New OAConnection.Connection

    Private Sub frm059_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtVendors = cn.RunSentence("Select IdVendor as [Vendor Code], '' as [Vendor Name], '' as Mail From ZMA1_Vendor Where IdVendor = 'xxxxx'").Tables(0)
        BS.DataSource = dtVendors

        dtgVendors.DataSource = BS

        cn.LoadCombo(cboManufacter, "Select Distinct Manufacter as Id, Manufacter  From vstManufacters order by Manufacter asc", "ID", "Manufacter")

    End Sub

    Private Sub dtgVendors_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgVendors.CellEndEdit

        If Not DBNull.Value.Equals(dtgVendors.CurrentCell.Value) AndAlso dtgVendors.CurrentRow.Cells("Vendor Code").Value <> "" Then
            Dim dtVn As New DataTable

            dtVn = cn.RunSentence("Select Name, VndMail1 as Mail From VendorsG11 Where Vendor = '" & dtgVendors.CurrentCell.Value & "'").Tables(0)

            If dtVn.Rows.Count > 0 Then
                dtgVendors.CurrentRow.Cells("Vendor Name").Value = dtVn.Rows(0).Item("Name")
                dtgVendors.CurrentRow.Cells("Mail").Value = dtVn.Rows(0).Item("Mail")
            End If
        End If


    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim row As DataRow
        Dim em As String = ""


        cn.ExecuteInServer("Delete From ZMA1_Vendor Where Manufacter = '" & Me.cboManufacter.SelectedValue.ToString & "'")

        For Each row In dtVendors.Rows
            Try
                If Not DBNull.Value.Equals(row.Item("Vendor Code")) AndAlso row.Item("Vendor Code") <> "" Then
                    cn.ExecuteInServer("Insert Into ZMA1_Vendor(IdVendor, Manufacter) Values('" & row.Item("Vendor Code") & "', '" & Me.cboManufacter.SelectedValue.ToString & "')")
                End If
            Catch ex As Exception
                em = em & Chr(13) & "- " & ex.Message
            End Try
        Next

        If em.Length > 0 Then
            MsgBox(em)
        End If

        MsgBox("Done.!   ", MsgBoxStyle.Information)
    End Sub

    Private Sub cboManufacter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboManufacter.SelectedIndexChanged

        If cboManufacter.SelectedValue.ToString <> "System.Data.DataRowView" Then
            dtVendors = cn.RunSentence("Select Vendor as [Vendor Code],Name as [Vendor Name], Mail From vstVendor_Manufacter Where Manufacter = '" & cboManufacter.SelectedValue.ToString & "'").Tables(0)
            BS.DataSource = dtVendors

            dtgVendors.DataSource = BS
        End If
    End Sub


End Class