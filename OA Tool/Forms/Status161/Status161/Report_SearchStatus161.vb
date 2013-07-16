Imports Shared_Functions
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Public Class Report_SearchStatus161

    Private CS As String = DB_CS
    Private MBS As New BindingSource
    Private SF As New MyFunctions_Class
    Private SQL As New BI_Functions

    Public Function Initialize(ByVal User As String) As Boolean
        Initialize = False
        SQL.Load_ComboBox(Combo_VPL, CS, "Select TNumber,UserName From BI_Users Order By UserName")
        AddToCheckList()
        Initialize = True
    End Function

    Private Sub AddToCheckList()
        CheckedList.Items.Clear()
        CheckedList.Items.Add("Analysis")
        CheckedList.Items.Add("FollowUp")
        CheckedList.Items.Add("Sent to AP")
    End Sub

    Private Sub Fill_DGV()
        Try
            Dim C As DataGridViewColumn
            dgv_Main.DataSource = Nothing
            dgv_Main.Columns.Clear()
            Dim flag As Boolean = False
            Dim query As String = ""
            Dim filter As String = "Where Box <> '' "

            If txt_invnum.Text <> "" Then
                filter = filter & " and Record=" & txt_invnum.Text
            End If


            If Combo_VPL.SelectedIndex > 0 Then
                filter = filter & " and Owner='" & Combo_VPL.SelectedValue & "'"
            End If

            For i = 0 To CheckedList.Items.Count - 1
                If (CheckedList.GetItemChecked(i)) Then
                    If Not flag Then
                        filter = filter & " and (Status='" & CheckedList.Items(i).ToString & "'"
                        flag = True
                    Else
                        filter = filter & " or Status='" & CheckedList.Items(i).ToString & "'"
                    End If
                End If
            Next
            If flag Then
                filter = filter & ")"
            End If

            If CheckBox_upload.Checked Then
                Dim fromD As String = DT_UPF.Value.Month & "/" & DT_UPF.Value.Day & "/" & DT_UPF.Value.Year & " 00:00:01"
                Dim TOD As String = DT_UPTO.Value.Month & "/" & DT_UPTO.Value.Day & "/" & DT_UPTO.Value.Year & " 23:59:59"
                filter = filter & "and (Upload_Date BETWEEN '" & fromD & "' AND '" & TOD & "')"
            End If


            If CheckBox_Release.Checked Then
                Dim fromDR As String = DT_RF.Value.Month & "/" & DT_RF.Value.Day & "/" & DT_RF.Value.Year & " 00:00:01"
                Dim TOR As String = DT_RTO.Value.Month & "/" & DT_RTO.Value.Day & "/" & DT_RTO.Value.Year & " 23:59:59"
                filter = filter & "and (Release_Date BETWEEN '" & fromDR & "' AND '" & TOR & "')"
            End If


            query = "Select * From View_Status161_Report " & filter
            MBS.DataSource = SF.GetDataTable(CS, query)

            dgv_Main.DataSource = MBS
            BindingNavigator1.BindingSource = MBS

            'Definimos los campos solo lectura
            For Each C In dgv_Main.Columns
                C.ReadOnly = True
            Next

            Dim Login As String = Environ("USERID")
            If SQL.IsAdministrator(Login) Then

                'Permitimos cambiar el L2_Ticket
                For Each C In dgv_Main.Columns
                    If C.Name = "L2_Ticket" Then
                        C.ReadOnly = False
                    End If

                Next

                dgv_Main.Columns("Owner_Name").Visible = False

                Dim I As Integer
                Dim CC As DataGridViewComboBoxColumn

                'Definimos los campos Combobox
                I = dgv_Main.Columns("Owner").DisplayIndex
                dgv_Main.Columns.Remove("Owner")
                CC = New DataGridViewComboBoxColumn
                CC.Name = "Owner"
                CC.DataPropertyName = "Owner"
                CC.HeaderText = "Owner"
                CC.DataSource = SF.GetDataTable(CS, "SELECT TNumber, UserName FROM BI_Users Order By UserName")
                CC.DisplayMember = "UserName"
                CC.ValueMember = "TNumber"
                CC.DisplayIndex = I
                dgv_Main.Columns.Add(CC)

            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub dgv_Main_RowLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_Main.RowLeave
        Dim Login As String = Environ("USERID")
        If SQL.IsAdministrator(Login) Then
            Try
                If dgv_Main.IsCurrentRowDirty Then


                    'Actualizamos los cambios en la Base de Datos
                    Dim R As DataRow
                    Dim Record As String = dgv_Main.CurrentRow.Cells("Record").Value


                    Dim DT As DataTable = SF.GetSQLTable(CS, "Status161_Data", " Record=" & Record)
                    If DT.Rows.Count = 1 Then
                        R = DT.Rows(0)
                        dgv_Main.EndEdit()
                  
                        R("Owner") = dgv_Main.CurrentRow.Cells("Owner").Value
                        R("L2_Ticket") = dgv_Main.CurrentRow.Cells("L2_Ticket").Value


                        'Guardamos todos los cambios en la Factura
                        Dim update_query As String = "Update Status161_Data Set L2_Ticket='" & R("L2_Ticket") & "',Owner='" & R("Owner") & "' Where  Record=" & Record
                        Dim exe As String = SF.SQL_Execute_NQ(CS, update_query)

                        If exe = Nothing Then
                            lb_Message.Text = "Record Successfully Updated " & Record

                        Else
                            MsgBox("Error To Update Record", MsgBoxStyle.Critical)
                        End If

                    End If
                End If

            Catch ex As Exception
                MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Update Error")
            End Try
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Fill_DGV()
    End Sub

    Private Sub btn_Filter_BySelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Filter_BySelection.Click
        Dim FV As String
        If Not DBNull.Value.Equals(dgv_Main.CurrentCell.Value) Then
            If dgv_Main.CurrentCell.ValueType.Equals(GetType(Date)) Then
                FV = " >= '" & String.Format("{0:yyyy-MM-dd}", dgv_Main.CurrentCell.Value) & " 00:00:00.000' AND " & dgv_Main.CurrentCell.OwningColumn.Name & " <= '" & String.Format("{0:yyyy-MM-dd}", dgv_Main.CurrentCell.Value) & " 23:59:00.000'"
            Else
                FV = " = '" & dgv_Main.CurrentCell.Value & "'"
            End If
        Else
            FV = " Is Null"
        End If
        Dim FE As String = dgv_Main.CurrentCell.OwningColumn.Name & FV
        If MBS.Filter <> Nothing Then
            MBS.Filter = MBS.Filter & " AND " & FE
        Else
            MBS.Filter = FE
        End If
        btn_Filter_Clear.CheckOnClick = True
        btn_Filter_Clear.Checked = True
    End Sub

    Private Sub btn_Filter_ExcSelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Filter_ExcSelection.Click
        Try
            Dim FV As String
            If Not DBNull.Value.Equals(dgv_Main.CurrentCell.Value) Then
                If dgv_Main.CurrentCell.ValueType.Equals(GetType(Date)) Then
                    FV = " >= '" & String.Format("{0:yyyy-MM-dd}", dgv_Main.CurrentCell.Value) & " 00:00:00.000' AND " & dgv_Main.CurrentCell.OwningColumn.Name & " <= '" & String.Format("{0:yyyy-MM-dd}", dgv_Main.CurrentCell.Value) & " 23:59:00.000'"
                Else
                    FV = " = '" & dgv_Main.CurrentCell.Value & "'"
                End If
            Else
                FV = " Is Null"
            End If
            Dim FE As String = "Not (" & dgv_Main.CurrentCell.OwningColumn.Name & FV & ")"
            If MBS.Filter <> Nothing Then
                MBS.Filter = MBS.Filter & " AND " & FE
            Else
                MBS.Filter = FE
            End If
            btn_Filter_Clear.CheckOnClick = True
            btn_Filter_Clear.Checked = True
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btn_Filter_Clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Filter_Clear.Click
        btn_Filter_Clear.CheckOnClick = False
        MBS.Filter = Nothing
    End Sub

    Private Sub dgv_Main_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgv_Main.KeyDown

        If e.KeyCode = Keys.F AndAlso e.Control Then
            Dim D As New Shared_Functions.BS_Find
            D.Initialize(dgv_Main)
            D.Show()
        End If

        If e.KeyCode = Keys.Delete Then
            If Not dgv_Main.CurrentCell Is Nothing Then
                dgv_Main.CurrentCell.Value = DBNull.Value
            End If
        End If

        If dgv_Main.AreAllCellsSelected(True) Or ((Not dgv_Main.CurrentRow Is Nothing) AndAlso dgv_Main.CurrentRow.Selected) Then
            dgv_Main.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Else
            dgv_Main.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        End If

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Fill_DGV()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        ClearFilter()
        lb_Message.Text = ""
        Fill_DGV()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Fill_DGV()
        lb_Message.Text = ""
        MsgBox("Done", MsgBoxStyle.Information, "Search Invoce")
    End Sub

    Private Sub PaintRows() Handles dgv_Main.RowPrePaint
        ''Pintamos las lineas con descuento
        'For Each row As DataGridViewRow In dgv_Main.Rows
        '    If Not row.IsNewRow Then
        '        If Not row.Cells("DiscountAmount").Value Is DBNull.Value Then
        '            If row.Cells("DiscountAmount").Value > 0 Then
        '                row.DefaultCellStyle.BackColor = Color.Yellow
        '            End If
        '        End If
        '    End If
        'Next
    End Sub

    Private Sub ClearFilter()
        lb_Message.Text = ""
        CheckBox_Release.Checked = False
        CheckBox_upload.Checked = False
      
        For i = 0 To CheckedList.Items.Count - 1
            If (CheckedList.GetItemChecked(i)) Then
                CheckedList.SetItemChecked(i, False)
            End If
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ClearFilter()

    End Sub

    Private Sub dgv_Main_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_Main.CellMouseDoubleClick
        'If Not dgv_Main.CurrentRow.IsNewRow Then
        '    'InvoiceNumber_Global = dgv_Main.CurrentRow.Cells("InvoiceNumber").Value
        '    InvoiceRow_Global = dgv_Main.CurrentRow
        '    InvoiceDetails.Show()
        'End If
    End Sub

    Private Sub dgv_Main_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_Main.CellClick
        dgv_Main.BeginEdit(True)
    End Sub

End Class
