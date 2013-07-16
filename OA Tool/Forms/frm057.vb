Imports System.Data.SqlClient

Public Class frm057
    '************************************************************
    'Este mantenimiento se realiza por medio de Stored Procedures
    ' Insert: InsertVendor_TaxCode
    ' Update: UpdateVendor_TaxCode
    '************************************************************
    Dim Found As Boolean = False

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim cn As New OAConnection.Connection
        Dim conn As New SqlConnection(cn.GetConnectionString())
        Dim Exe As String = ""

        If Me.txtCode.Text.Length = 0 Then
            MsgBox("Please type the vendor code.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Me.txtName.Text.Length = 0 Then
            MsgBox("Please type the vendor name.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Me.txtCountry.Text.Length = 0 Then
            MsgBox("Please type the vendor country", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            conn.Open()

            If Not Found Then
                Exe = "InsertVendor_TaxCode"
            Else
                Exe = "UpdateVendor_TaxCode"
            End If

            Dim comm As New SqlCommand(Exe, conn)
            comm.CommandType = Data.CommandType.StoredProcedure

            Dim cVendor As Double
            If Double.TryParse(txtCode.Text, cVendor) Then
                comm.Parameters.Add(New SqlParameter("IdVendor", cVendor))
                comm.Parameters.Add(New SqlParameter("Name", Me.txtName.Text))
                comm.Parameters.Add(New SqlParameter("SP", Me.chkSup_Portal.Checked))
                comm.Parameters.Add(New SqlParameter("CT", Me.chkCatalog.Checked))
                comm.Parameters.Add(New SqlParameter("Country", Me.txtCountry.Text))
                comm.Parameters.Add(New SqlParameter("TaxCode", Me.txtTax.Text))

                comm.ExecuteNonQuery()

            Else
                MsgBox("Please verify Vendor code.")
            End If



        Catch ex As Exception
            MsgBox("Error trying insert new vendor.", MsgBoxStyle.Exclamation)
        Finally
            conn.Close()
            MsgBox("Done.")
        End Try
    End Sub

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn.Click
        Dim cn As New OAConnection.Connection
        Dim dt As New DataTable

        dt = cn.RunSentence("Select Vendor,Name, VndSP, CT, Country,Tax_Code From vst_Tax_Config Where Vendor = '" & Me.txtCode.Text & "'").Tables(0)

        If dt.Rows.Count > 0 Then
            Found = True
            Me.txtCode.Text = dt.Rows(0).Item("Vendor")
            Me.txtName.Text = dt.Rows(0).Item("Name")
            Me.txtCountry.Text = dt.Rows(0).Item("Country")
            Me.txtTax.Text = dt.Rows(0).Item("Tax_Code")
            Me.chkCatalog.Checked = dt.Rows(0).Item("CT")
            Me.chkSup_Portal.Checked = dt.Rows(0).Item("VndSP")
        Else
            Found = False
            MsgBox("Vendor not found. Please complete the information and click save.", MsgBoxStyle.Information)
            Me.txtName.Text = ""
            Me.txtCountry.Text = ""
            Me.txtTax.Text = ""
            Me.chkCatalog.Checked = False
            Me.chkSup_Portal.Checked = False
        End If

    End Sub
End Class