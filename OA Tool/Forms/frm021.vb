Public Class frm021
    Dim SAPCn As New SAPConection.SAPTools
    Dim SAP As Object
    Dim cn As New OAConnection.Connection
    Dim Access As New Access.Application
    Dim Plantas As DataSet
    Dim Tabla As DataTable

    Private Sub frm021_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
        cn.KillProcess("MSAccess")
        SAP = Nothing
        SAPCn = Nothing
    End Sub


    Private Sub frm021_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

        Dim i%
        If Me.Text = "[021] Materiales sin Source list de contrato [Por Usuario]" Then
            Tabla = cn.RunSentence("Select * From vstMaterialesSinSourceList where OwnerMail = '" & cn.GetUserMail() & "'").Tables(0)
        Else
            Tabla = cn.RunSentence("Select * From vstMaterialesSinSourceList").Tables(0)
        End If

        If Tabla.Columns.Count > 0 Then
            i = 0
            Do While i < Tabla.Columns.Count
                cboFiltro.Items.Add(Tabla.Columns(i).ColumnName)
                i = i + 1
            Loop
        End If

        Me.dtgContratos.DataSource = Tabla
        Me.txtContratos.Text = Tabla.Rows.Count
    End Sub

    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Filtrar()
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

    Private Sub txtBuscar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuscar.KeyPress
        If e.KeyChar = Chr(13) Then
            Filtrar()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.CellSelect
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim login As New SapLogin
        Dim i%

        login.ShowDialog()

        If gUser.Length > 0 And gPwr.Length > 0 Then
            SAP = SAPCn.GetConnectionToSAP("L7P LA TS Prod", Trim(gUser), Trim(gPwr), SAPConfig)

            For i = 0 To Me.dtgContratos.RowCount - 1
                SAPCn.FixSourceListOA(SAP, Me.dtgContratos.Rows(i).Cells("Material").Value, Me.dtgContratos.Rows(i).Cells("Planta").Value, Me.dtgContratos.Rows(i).Cells("Inicio").Value, Me.dtgContratos.Rows(i).Cells("Fin").Value, Me.dtgContratos.Rows(i).Cells("Vendor").Value, Me.dtgContratos.Rows(i).Cells("PO").Value, Me.dtgContratos.Rows(i).Cells("OA").Value, Me.dtgContratos.Rows(i).Cells("Item").Value)
            Next

            SAPCn.CloseSession(SAP)
        End If

    End Sub
End Class