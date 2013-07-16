Public Class frm068
    Dim cn As New OAConnection.BaseConexion


    Private Sub frm068_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'LA_ToolDataSet.vst_SC_Report' table. You can move, or remove it, as needed.
        Dim DB As New OAConnection.Connection
        Dim Data As New DataTable
        Dim row As DataRow

        Data = DB.RunSentence("Select Distinct Plant, Plant + ' - ' + [Plant Name] as Name From vst_SC_Report Where usuario = '" & gsUsuarioPC & "' Order by Name").Tables(0)
        row = Data.NewRow()
        row("Plant") = "000"
        row("Name") = "--- Show All ---"

        Data.Rows.Add(row)
        cboPlant.DataSource = Data
        cboPlant.DisplayMember = Data.Columns("Name").Caption.ToString
        cboPlant.ValueMember() = Data.Columns("Plant").Caption.ToString
        cboPlant.SelectedValue = "000"


        Data = DB.RunSentence("Select Distinct SAPBOX, SAPBOX as Name From vst_SC_Report Where usuario = '" & gsUsuarioPC & "' Order by SAPBOX").Tables(0)
        row = Data.NewRow()
        row("SAPBOX") = "000"
        row("Name") = "--- Show All ---"

        Data.Rows.Add(row)
        cboSAP.DataSource = Data
        cboSAP.DisplayMember = Data.Columns("Name").Caption.ToString
        cboSAP.ValueMember() = Data.Columns("SAPBox").Caption.ToString
        cboSAP.SelectedValue = "000"



        Data = DB.RunSentence("Select Distinct Spend, Spend as Name From vst_SC_Report Where usuario = '" & gsUsuarioPC & "' Order by Spend").Tables(0)
        row = Data.NewRow()
        row("Spend") = "000"
        row("Name") = "--- Show All ---"

        Data.Rows.Add(row)
        cboSpend.DataSource = Data
        cboSpend.DisplayMember = Data.Columns("Name").Caption.ToString
        cboSpend.ValueMember() = Data.Columns("Spend").Caption.ToString
        cboSpend.SelectedValue = "000"


        Data = DB.RunSentence("Select Distinct [PO Type], [PO Type] as Name From vst_SC_Report Where usuario = '" & gsUsuarioPC & "' Order by [PO Type]").Tables(0)
        row = Data.NewRow()
        row("PO Type") = "000"
        row("Name") = "--- Show All ---"

        Data.Rows.Add(row)
        cboPOType.DataSource = Data
        cboPOType.DisplayMember = Data.Columns("Name").Caption.ToString
        cboPOType.ValueMember() = Data.Columns("PO Type").Caption.ToString
        cboPOType.SelectedValue = "000"



        cn.OpenConection()

        vst_SC_ReportTableAdapter.Connection = cn._ConnectionDB

        Me.vst_SC_ReportTableAdapter.Fill(Me.LA_ToolDataSet.vst_SC_Report, gsUsuarioPC)
        Me.ReportViewer1.RefreshReport()


    End Sub

    Private Sub FillBy1ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Me.vst_SC_ReportTableAdapter.FillBy1(Me.LA_ToolDataSet.vst_SC_Report, )
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub FillByToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Me.vst_SC_ReportTableAdapter.FillBy(Me.LA_ToolDataSet.vst_SC_Report)
        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim lsWhere As String = String.Empty

            If cboPlant.SelectedValue.ToString <> "000" Then
                lsWhere = lsWhere & "(Plant = '" & cboPlant.SelectedValue.ToString & "')"
            End If


            If cboPOType.SelectedValue.ToString <> "000" Then
                If lsWhere = String.Empty Then
                    lsWhere = lsWhere & "([PO Type] = '" & cboPOType.SelectedValue.ToString & "')"
                Else
                    lsWhere = lsWhere & " And ([PO Type] = '" & cboPOType.SelectedValue.ToString & "')"
                End If
            End If


            If cboSAP.SelectedValue.ToString <> "000" Then
                If lsWhere = String.Empty Then
                    lsWhere = lsWhere & "([SAPBox] = '" & cboSAP.SelectedValue.ToString & "')"
                Else
                    lsWhere = lsWhere & " And ([SAPBox] = '" & cboSAP.SelectedValue.ToString & "')"
                End If
            End If

            If cboSpend.SelectedValue.ToString <> "000" Then
                If lsWhere = String.Empty Then
                    lsWhere = lsWhere & "([Spend] = '" & cboSpend.SelectedValue.ToString & "')"
                Else
                    lsWhere = lsWhere & " And ([Spend] = '" & cboSpend.SelectedValue.ToString & "')"
                End If
            End If

            If lsWhere <> String.Empty Then
                lsWhere = " Where (" & lsWhere & " And (Usuario = '" & gsUsuario & "'))"
            Else
                lsWhere = " Where (Usuario = '" & gsUsuario & "')"
            End If


            Vst_SC_ReportTableAdapter.Adapter.SelectCommand.CommandText = "Select * From vst_SC_Report" & lsWhere
            Me.Vst_SC_ReportTableAdapter.Fill(Me.LA_ToolDataSet.vst_SC_Report, gsUsuarioPC)
            'Me.Vst_SC_ReportTableAdapter.FillBy(Me.LA_ToolDataSet.vst_SC_Report, UsuarioToolStripTextBox.Text)
            Me.ReportViewer1.RefreshReport()

        Catch ex As System.Exception
            System.Windows.Forms.MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class