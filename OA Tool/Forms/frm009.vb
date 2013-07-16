Public Class frm009
    Dim cn As New OAConnection.Connection
    Dim Table As DataSet
    Dim Access As New Access.Application


    Private Sub Filtrar()
        If txtBuscar.Text <> "" And Me.cboFiltro.Text <> "" Then
            Select Case Table.Tables(0).Columns(cboFiltro.Text).DataType.ToString
                Case "System.Double", "System.Int16", "System.Int32", "System.Int64"
                    Table.Tables(0).DefaultView.RowFilter = (cboFiltro.Text & " = " & txtBuscar.Text)

                Case "System.String"
                    Table.Tables(0).DefaultView.RowFilter = (cboFiltro.Text & " like '%" & txtBuscar.Text & "%'")

            End Select
        Else
            Table.Tables(0).DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub frm009_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
    End Sub


    Private Sub frm009_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i%

        Table = cn.RunSentence("Select * From vstContratosVencidos")
        Me.dtgContratos.DataSource = Table.Tables(0)
        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

        Me.txtContratos.Text = "  " & Me.dtgContratos.Rows.Count & "  " '& ". Entre el " & Today() & " y el " & Today().AddDays(90) 

        If Table.Tables(0).Columns.Count > 0 Then
            i = 0
            Do While i < Table.Tables(0).Columns.Count
                cboFiltro.Items.Add(Table.Tables(0).Columns(i).ColumnName)
                i = i + 1
            Loop
        End If
    End Sub

    Private Sub frm009_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgContratos.Width = Me.Width - 20
        Me.dtgContratos.Height = Me.Height - 160
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        'Mismo código que en la pantalla [004],[005],[008]
        Dim i%
        Dim J%
        Dim x%
        Dim FA As New DataTable
        Dim OA As New DataTable
        Dim Found As Boolean

        FA.Columns.Add()
        With FolderBrowserDialog
            .Description = "Seleccione el directorio donde se guardarán los formatos:"
            .ShowDialog()

            If .SelectedPath.Length = 0 Then
                MsgBox("Debe Seleccionar un directorio para poder guardar los formatos azules.")
                Exit Sub
            End If
        End With

        For i = 0 To Me.dtgContratos.RowCount - 1
            Found = False
            If Me.dtgContratos.Rows(i).Selected Then
                If FA.Rows.Count > 0 Then
                    For J = 0 To FA.Rows.Count - 1
                        If FA.Rows(J).Item(0) = Me.dtgContratos.Rows(i).Cells("Vendor").Value Then
                            Found = True
                            Exit For
                        End If
                    Next
                End If

                If Not Found Then
                    FA.Rows.Add()
                    FA.Rows(J).Item(0) = Me.dtgContratos.Rows(i).Cells("Vendor").Value

                    If Len(Dir(FolderBrowserDialog.SelectedPath & "\" & CStr(Me.dtgContratos.Rows(i).Cells("Vendor").Value) & " - " & Me.dtgContratos.Rows(i).Cells("Name").Value & ".xls")) > 0 Then
                        If MsgBox("El archivo ya existe." & vbCr & vbCr & "Desea reemplazarlo?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Reemplazar Archivo?") = MsgBoxResult.Yes Then
                            Kill(FolderBrowserDialog.SelectedPath & "\" & CStr(Me.dtgContratos.Rows(i).Cells("Vendor").Value) & " - " & Me.dtgContratos.Rows(i).Cells("Name").Value & ".xls")
                        Else
                            MsgBox("Acción cancelada por el usuario.", MsgBoxStyle.Information)
                            Exit For
                        End If
                    End If

                    Access.Run("GetBlueForm", CStr(Me.dtgContratos.Rows(i).Cells("Vendor").Value), FolderBrowserDialog.SelectedPath & "\" & CStr(Me.dtgContratos.Rows(i).Cells("Vendor").Value) & " - " & Me.dtgContratos.Rows(i).Cells("Name").Value & ".xls")
                    OA = cn.RunSentence("Select oa from vstOwners Where OwnerMail = '" & cn.GetUserMail & "' and Vendor = " & CStr(Me.dtgContratos.Rows(i).Cells("Vendor").Value)).Tables(0)

                    If OA.Rows.Count > 0 Then
                        For x = 0 To OA.Rows.Count - 1
                            cn.AddStepToTrackingOA(OA.Rows(x).Item("oa"), 3, True)
                        Next
                    End If

                End If
            End If
        Next

        MsgBox("Done!")
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Filtrar()
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
End Class