Imports Shared_Functions
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.ComponentModel
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Text


Public Class Status161_Analysis

    Private CS As String = DB_CS
    Private MBS As New BindingSource
    Private SF As New MyFunctions_Class
    Private BI As New BI_Functions
    Private Exc As New ExcelTranslator

#Region "DataGridView Subs"

    Public Function Initialize(ByVal User As String) As Boolean
        Initialize = False

        If SF.SQL_Execute_SC(CS, "SELECT Count(Record) FROM View_Status161_Analysis WHERE Owner = '" & Login & "' or Owner='" & Login_Backup & "'") = 0 Then
            MsgBox("You don't have any workflow under this category!", MsgBoxStyle.Information)
        Else
            Fill_DGV()
            Initialize = True
        End If
    End Function

    Private Sub Fill_DGV()
        Try
            Dim C As DataGridViewColumn
            dgv_Main.DataSource = Nothing
            dgv_Main.Columns.Clear()
            Dim flag As Boolean = False
            Dim query As String = ""

            query = "Select * From View_Status161_Analysis WHERE Owner = '" & Login & "' or Owner='" & Login_Backup & "'"
            MBS.DataSource = SF.GetDataTable(CS, query)

            dgv_Main.DataSource = MBS
            BindingNavigator1.BindingSource = MBS

            'Definimos los campos solo lectura
            For Each C In dgv_Main.Columns
                If C.Name <> "Flag" And C.Name <> "Issue" And C.Name <> "Priority" And C.Name <> "Reason" Then
                    C.ReadOnly = True
                End If
            Next

            dgv_Main.Columns("Comment").HeaderText = "Comment_PDM"

            'Definimos los campos Combobox
            Dim I As Integer
            Dim CC As DataGridViewComboBoxColumn
            I = dgv_Main.Columns("Issue").DisplayIndex
            dgv_Main.Columns.Remove("Issue")
            CC = New DataGridViewComboBoxColumn
            CC.Name = "Issue"
            CC.DataPropertyName = "Issue"
            CC.HeaderText = "Issue"
            CC.DataSource = SF.GetDataTable(CS, "SELECT Issue, Description FROM Status161_Issues where Enabled = 1 Order By Description")
            CC.DisplayMember = "Description"
            CC.ValueMember = "Issue"
            CC.DisplayIndex = I
            dgv_Main.Columns.Add(CC)


            I = dgv_Main.Columns("Priority").DisplayIndex
            dgv_Main.Columns.Remove("Priority")
            CC = New DataGridViewComboBoxColumn
            CC.Name = "Priority"
            CC.DataPropertyName = "Priority"
            CC.HeaderText = "Priority"
            CC.DataSource = SF.GetDataTable(CS, "SELECT Priority, Description FROM Status161_Priority Order By Description")
            CC.DisplayMember = "Description"
            CC.ValueMember = "Priority"
            CC.DisplayIndex = I
            dgv_Main.Columns.Add(CC)


            I = dgv_Main.Columns("Reason").DisplayIndex
            dgv_Main.Columns.Remove("Reason")
            CC = New DataGridViewComboBoxColumn
            CC.Name = "Reason"
            CC.DataPropertyName = "Reason"
            CC.HeaderText = "Reason"
            CC.DataSource = SF.GetDataTable(CS, "SELECT Reason, Description FROM Status161_Reason where Enabled = 1 Order By Description")
            CC.DisplayMember = "Description"
            CC.ValueMember = "Reason"
            CC.DisplayIndex = I
            dgv_Main.Columns.Add(CC)

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub dgv_Main_RowLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_Main.RowLeave
        Try
            If dgv_Main.IsCurrentRowDirty Then

                'Actualizamos los cambios en la base de datos
                Dim R As DataRow
                Dim Record As String = dgv_Main.CurrentRow.Cells("Record").Value

                Dim DT As DataTable = SF.GetSQLTable(CS, "Status161_Data", "Record=" & Record)
                If DT.Rows.Count = 1 Then
                    R = DT.Rows(0)
                    dgv_Main.EndEdit()
                    R("Flag") = dgv_Main.CurrentRow.Cells("Flag").Value
                    R("Issue") = dgv_Main.CurrentRow.Cells("Issue").Value
                    R("Comment") = dgv_Main.CurrentRow.Cells("Comment").Value
                    R("Priority") = dgv_Main.CurrentRow.Cells("Priority").Value
                    R("Reason") = dgv_Main.CurrentRow.Cells("Reason").Value

                    'Guardamos todos los cambios en la factura
                    Dim update_query As String = "Update Status161_Data Set Flag=" & BI.BooleanConvertion(R("Flag")) & ", Issue=" & R("Issue") & ",Comment='" & R("Comment") & "', Priority=" & R("Priority") & ",Reason=" & R("Reason") & "  Where Record=" & Record
                    Dim response = SF.SQL_Execute_NQ(CS, update_query)

                End If
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Update Error")
        End Try
    End Sub

    Private Sub dgv_Main_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgv_Main.KeyDown

        'Ctr + F 
        If e.KeyCode = Keys.F AndAlso e.Control Then
            Dim D As New Shared_Functions.BS_Find
            D.Initialize(dgv_Main)
            D.Show()
        End If

        'Delete
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

    Private Sub dgv_Main_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_Main.CellMouseDoubleClick
        Try

            If Not dgv_Main.CurrentRow.IsNewRow Then
                Dim FRM_Small As New ViewCommentsStatus161
                FRM_Small.Comment = ""
                InvoiceRow_Global = dgv_Main.CurrentRow
                If (FRM_Small.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                    dgv_Main.CurrentRow.Cells("Comment").Value = FRM_Small.Comment
                    dgv_Main.CurrentRow.Cells("Comment_AP").Value = FRM_Small.Comment_AP
                End If
            End If

        Catch
        End Try
    End Sub

#End Region

#Region "Tool Bar/Filters"

    Private Sub btn_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
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

    Private Sub AllToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllToolStripMenuItem1.Click
        FlagCheck("Flag=", 1)
    End Sub

    Private Sub NoneToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoneToolStripMenuItem1.Click
        FlagCheck("Flag=", 0)
    End Sub

    Private Sub FlagCheck(ByVal Column As String, ByVal flag As Integer)
        Try
            Dim R As DataRow
            Dim Record As String

            Dim Selec As String
            Dim Filter As String = MBS.Filter

            If Filter <> "Not (Flag = 'True')" And Filter <> Nothing Then
                Selec = "Select * from View_Status161_Analysis where (Box <> 'MBG') and " & Filter
            Else
                Selec = "Select * from View_Status161_Analysis"
            End If
            Dim DT As DataTable = SF.GetDataTable(CS, Selec)
            For i = 0 To DT.Rows.Count - 1

                R = DT.Rows(i)
                Record = R("Record").ToString()
                Dim Exc As String = SF.SQL_Execute_NQ(CS, "Update Status161_Data set " & Column & flag & " where  Record= " & Record)

            Next
            Fill_DGV()
        Catch ex As Exception
            MsgBox("Error to Flag/AutoComm = True/False", MsgBoxStyle.Critical, "Error!")
        End Try
    End Sub


#End Region

#Region "Button Actions"


    Private Sub Btn_ReleaseRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ReleaseRecords.Click
        If SF.IsConfirmed() Then
            ReleaseRecords()
        End If
    End Sub

#End Region

    Private Sub ReleaseRecords()
        Try
            Dim count As Integer = 0
            Dim DT As DataTable

            Dim Selec As String
            Dim Filter As String = MBS.Filter

            If Filter <> "Not (Flag = 'True')" And Filter <> Nothing Then
                Selec = "Select Record From View_Status161_Analysis where (Flag = 1) and (Issue <> 99) and (Reason <> 99) and (Owner = '" & Login & "' or owner='" & Login_Backup & "')" & Filter
            Else
                Selec = "Select Record From View_Status161_Analysis Where (Flag = 1) and (Issue <> 99) and (Reason <> 99) and (Owner = '" & Login & "' or owner='" & Login_Backup & "')"
            End If

            DT = SF.GetDataTable(CS, Selec)

            For Each DR As DataRow In DT.Rows

                Dim Update_Query As String = "Update Status161_Data Set Release_Date= GetDate(), Release_By='" & Login & "', ProcessLevel=2 Where Record=" & DR("Record")
                Dim Exc As String = SF.SQL_Execute_NQ(CS, Update_Query)

                If Exc <> Nothing Then
                    MsgBox("Error To Release Record: " & DR("Record"), MsgBoxStyle.Exclamation)
                Else
                    count = count + 1
                End If

            Next
            Fill_DGV()
            MsgBox("Records Released: " & count, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Error To Releases Records", MsgBoxStyle.Critical)
        End Try

    End Sub

End Class
