Imports Shared_Functions
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.ComponentModel
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Text
Imports SAPScripts

Public Class Status161_Upload

    Private CS As String = DB_CS
    Private MBS As New BindingSource
    Private SF As New MyFunctions_Class
    Private BI As New BI_Functions
    Private Exc As New ExcelTranslator

#Region "DataGridView Subs"

    Public Function Initialize(ByVal User As String) As Boolean
        Initialize = False
        Fill_DGV()
        Initialize = True
    End Function

    Private Sub Fill_DGV()
        Try
            Dim C As DataGridViewColumn
            dgv_Main.DataSource = Nothing
            dgv_Main.Columns.Clear()
            Dim flag As Boolean = False
            Dim query As String = ""

            query = "Select * From View_Status161_Upload"
            MBS.DataSource = SF.GetDataTable(CS, query)

            dgv_Main.DataSource = MBS
            BindingNavigator1.BindingSource = MBS

            'Definimos los campos solo lectura
            For Each C In dgv_Main.Columns
                If C.Name <> "Flag" Then
                    C.ReadOnly = True
                End If
            Next

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
                    R("Owner") = dgv_Main.CurrentRow.Cells("Owner").Value

                    'Guardamos todos los cambios en la factura
                    Dim update_query As String = "Update Status161_Data Set Flag=" & BI.BooleanConvertion(R("Flag")) & ", Owner='" & R("Owner") & "' Where Record=" & Record
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
        'If e.KeyCode = Keys.Delete Then
        '    If Not dgv_Main.CurrentCell Is Nothing Then
        '        dgv_Main.CurrentCell.Value = DBNull.Value
        '    End If
        'End If

        If dgv_Main.AreAllCellsSelected(True) Or ((Not dgv_Main.CurrentRow Is Nothing) AndAlso dgv_Main.CurrentRow.Selected) Then
            dgv_Main.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Else
            dgv_Main.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        End If

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
                Selec = "Select * from View_Status161_Upload where (Box <> 'MBG') and " & Filter
            Else
                Selec = "Select * from View_Status161_Upload"
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

    Private Sub Btn_UploadFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_UploadFile.Click
        If SF.IsConfirmed() Then
            UploadFile()
        End If
    End Sub

    Private Sub Btn_SendRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SendRecords.Click
        If SF.IsConfirmed() Then
            SendRecords()
        End If
    End Sub

    Private Sub Btn_DeleteRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_DeleteRecords.Click
        If SF.IsConfirmed() Then
            DeleteRecords()
        End If
    End Sub

    Private Sub Btn_Reopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Reopen.Click
        If SF.IsConfirmed() Then
            Try
                Dim Update As String
                Update = "Update Status161_Data Set Flag=0,Owner='TBD'," & _
                "ProcessLevel=0,Upload_Date=NULL,Upload_By=NULL,Release_Date=NULL," & _
                "Release_By=NULL,Email_Date=NULL,Email_To=NULL, Email_By=NULL Where  Record=" & txt_record.Text

                Dim Exc As String = SF.SQL_Execute_NQ(CS, Update)
                If Exc <> Nothing Then
                    MsgBox("Error To Reopen Record:" & txt_record.Text, MsgBoxStyle.Critical)
                Else
                    Fill_DGV()
                    MsgBox("Done", MsgBoxStyle.Information)
                End If
            Catch ex As Exception
                MsgBox("Error To Reopen Record:" & txt_record.Text, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

#End Region

    Private Sub UploadFile()
        Try
            'Cantidad de regsitros agregados
            Dim Count As Integer = 0

            'Seleccionamos el File de excel
            Dim ExcelFilePath As String = ""
            ExcelFilePath = ReturnExcelFilePath()

            If ExcelFilePath <> "" Then
                Dim DT As DataTable
                DT = Exc.ExcelToDataTableOLEDB(ExcelFilePath, "Status 161")

                'Si tenemos records para intentar subir a la base
                If DT.Rows.Count > 0 Then

                    'Vaciamos la tabla Status161_Upload_Today
                    Delete_Upload_Today()

                    For Each DR As DataRow In DT.Rows

                        'Validamos el usuario
                        DR(1) = ValidateUser(DR(1))

                        'Tratamos de Insertar el Record 
                        Dim Insert_Query As String = "Insert into Status161_Data(Flag,Issue,Comment,Owner,Box,Vendor,VendorName," & _
                        "Reference,Record,PurchaseDoc,PGrp,BuyerName,Due_Date) Values(" & _
                        "0,99,''" & _
                        ",'" & DR(1) & "'" & _
                        ",'" & DR(2) & "'" & _
                        ",'" & DR(4) & "'" & _
                        ",'" & DR(5) & "'" & _
                        ",'" & DR(6) & "'" & _
                        "," & DR(7) & _
                        ",'" & DR(8) & "'" & _
                        ",'" & DR(9) & "'" & _
                        ",'" & DR(10) & "'" & _
                        ",'" & DR(11) & "')"

                        Dim Exc As String = SF.SQL_Execute_NQ(CS, Insert_Query)
                        If Exc <> Nothing Then
                            If Exc.Contains("Violation of PRIMARY") Then 'Si el record vuelve a bloquearse
                                'Debe re-abrir el record (ProcessLevel = 1 Analysis)

                                Dim ReOpen As String = "Update Status161_Data Set ProcessLevel=1,Upload_By='" & Login & "',Owner='" & DR(1) & "' where Record = " & DR(7) & " and Processlevel <> 0"
                                Dim Exc2 As String = SF.SQL_Execute_NQ(CS, ReOpen)
                                Dim a As String = "aa"

                            Else ' Si hubo un error al agregar el record
                                MsgBox("Error To Upload Record " & DR(7) & Exc, MsgBoxStyle.Exclamation)
                            End If
                        Else 'Si el record subio correctamente y es un record nuevo
                            Count = Count + 1
                        End If

                        'Registramos el record en la tabla Status161_upload_today
                        Save_Upload_Today(DR(7), DR(1))

                    Next


                    'Cerramos los Records que No subieron hoy
                    Close_Unblocked_Records()
                    Fill_DGV()

                    MsgBox("Records Added: " & Count, MsgBoxStyle.Information)
                End If
            Else
                MsgBox("Error To Upload xls File", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MsgBox("Error To Upload xls File/Insert Query", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Delete_Upload_Today()
        Dim Delete As String = "Delete From Status161_Upload_Today"
        Dim Exc As String = SF.SQL_Execute_NQ(CS, Delete)
    End Sub

    Private Sub Save_Upload_Today(ByVal Record As String, ByVal Owner As String)
        Dim Insert As String = "Insert into Status161_Upload_Today values(" & Record & ",'" & Owner & "',GetDate())"
        Dim Exc As String = SF.SQL_Execute_NQ(CS, Insert)
    End Sub

    Private Sub Close_Unblocked_Records()

        Dim T As DataTable = SF.GetDataTable(CS, "Select Record From View_Status161_NotBlocked")

        For Each R As DataRow In T.Rows
            Dim Close As String = "Update Status161_Data set ProcessLevel = 3, Release_Date=GetDate(),Release_By='DB JOB' Where Record =" & R(0)

            Dim Exc As String = SF.SQL_Execute_NQ(CS, Close)

        Next

    End Sub

    Private Sub SendRecords()
        Try
            Dim count As Integer = 0
            Dim DT As DataTable

            Dim Selec As String
            Dim Filter As String = MBS.Filter

            If Filter <> "Not (Flag = 'True')" And Filter <> Nothing Then
                Selec = "Select Record From View_Status161_Upload where (Flag = 1) and " & Filter
            Else
                Selec = "Select Record From View_Status161_Upload Where Flag = 1"
            End If

            DT = SF.GetDataTable(CS, Selec)

            For Each DR As DataRow In DT.Rows

                Dim Update_Query As String = "Update Status161_Data Set Upload_Date= GetDate(), Upload_By='" & Login & "',Flag=0, ProcessLevel = 1 Where Record=" & DR("Record")
                Dim Exc As String = SF.SQL_Execute_NQ(CS, Update_Query)

                If Exc <> Nothing Then
                    MsgBox("Error To Send Record: " & DR("Record"), MsgBoxStyle.Exclamation)
                Else
                    count = count + 1
                End If

            Next
            Fill_DGV()
            MsgBox("Records Sended: " & count, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Error To Send Records", MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub DeleteRecords()
        Try
            Dim count As Integer = 0
            Dim DT As DataTable

            Dim Selec As String
            Dim Filter As String = MBS.Filter

            If Filter <> "Not (Flag = 'True')" And Filter <> Nothing Then
                Selec = "Select Record From View_Status161_Upload where (Flag = 1) and " & Filter
            Else
                Selec = "Select Record From View_Status161_Upload Where Flag = 1"
            End If

            DT = SF.GetDataTable(CS, Selec)

            For Each DR As DataRow In DT.Rows

                Dim Delete_Query As String = "Delete Status161_Data Where Record=" & DR("Record")
                Dim Exc As String = SF.SQL_Execute_NQ(CS, Delete_Query)

                If Exc <> Nothing Then
                    MsgBox("Error To Delete Record: " & DR("Record"), MsgBoxStyle.Exclamation)
                Else
                    count = count + 1
                End If

            Next
            Fill_DGV()
            MsgBox("Records Deleted: " & count, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Error To Delete Records", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Function ReturnExcelFilePath() As String
        Dim FileName As String = ""
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        OpenFileDialog.FileName = ""
        OpenFileDialog.ShowDialog()
        FileName = OpenFileDialog.FileName
        If Not FileName.Contains(".xls") Then
            FileName = ""
            ReturnExcelFilePath = FileName
        Else
            If OpenFileDialog.ValidateNames Then
                ReturnExcelFilePath = FileName
            Else
                ReturnExcelFilePath = ""
            End If
        End If


    End Function

    Private Function ValidateUser(ByVal User As String) As String
        ValidateUser = False

        Dim P As String = SF.SQL_Execute_SC(CS, "Select TNumber From BI_Users Where TNumber='" & User.Replace(" ", "").Trim & "' and Active=1")
        If P Is Nothing Then
            ValidateUser = "TBD"
        Else
            ValidateUser = User

        End If
    End Function




End Class
