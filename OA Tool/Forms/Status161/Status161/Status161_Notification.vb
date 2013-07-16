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
Imports System.Threading

Public Class Status161_Notification

    Private CS As String = DB_CS
    Private MBS As New BindingSource
    Private SF As New MyFunctions_Class
    Private BI As New BI_Functions
    Private Exc As New ExcelTranslator

#Region "DataGridView Subs"

    Public Function Initialize(ByVal User As String) As Boolean
        Initialize = False
        If SF.SQL_Execute_SC(CS, "SELECT * From View_Count_Status161_Notification") = 0 Then
            MsgBox("You don't have any workflow under this category!", MsgBoxStyle.Information)
        Else
            Fill_DGV()
            FlagCheck("Flag=", 1)
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

            query = "Select * From View_Status161_Notification"
            MBS.DataSource = SF.GetDataTable(CS, query)

            dgv_Main.DataSource = MBS
            BindingNavigator1.BindingSource = MBS

            'Definimos los campos solo lectura
            For Each C In dgv_Main.Columns
                If C.Name <> "Flag" And C.Name <> "Status" Then
                    C.ReadOnly = True
                End If
            Next

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
                    R("Status") = dgv_Main.CurrentRow.Cells("Status").Value

                    'Guardamos todos los cambios en la factura
                    Dim update_query As String = "Update Status161_Data Set Flag=" & BI.BooleanConvertion(R("Flag")) & ", Status='" & R("Status") & "' Where Record=" & Record
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

    Private Sub dgv_Main_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_Main.CellMouseDoubleClick
        Try

            If Not dgv_Main.CurrentRow.IsNewRow Then
                Dim FRM_Small As New ViewCommentsStatus161
                FRM_Small.Comment = ""
                InvoiceRow_Global = dgv_Main.CurrentRow
                If (FRM_Small.ShowDialog() = Windows.Forms.DialogResult.OK) Then
                    dgv_Main.CurrentRow.Cells("Comment").Value = FRM_Small.Comment
                End If
            End If

        Catch
        End Try
    End Sub

    Private Sub dgv_Main_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_Main.CellClick
        dgv_Main.BeginEdit(True)
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
                Selec = "Select * from View_Status161_Notification where (Box <> 'MBG') and " & Filter
            Else
                Selec = "Select * from View_Status161_Notification"
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


    Private Sub Btn_Send_NA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Send_NA.Click
        If SF.IsConfirmed() Then
            If Send_Status161_Notification("N6P") Then
                Fill_DGV()
                MsgBox("Done", MsgBoxStyle.Information, "Email Message")
            End If
        End If
    End Sub

    Private Sub Btn_Send_LA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Send_LA.Click
        If SF.IsConfirmed() Then
            If Send_Status161_Notification("L6P") Then
                Fill_DGV()
                MsgBox("Done", MsgBoxStyle.Information, "Email Message")
            End If
        End If
    End Sub

    Private Sub Btn_ReturnToVPL(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Return.Click
        If SF.IsConfirmed() Then
            Try
                Dim DT As DataTable
                DT = SF.GetDataTable(CS, "Select * From View_Status161_Notification Where Flag = 1")

                For Each DR As DataRow In DT.Rows
                    Dim Update As String = "Update Status161_Data Set ProcessLevel = 1 Where Record=" & DR("Record")
                    Dim Exc As String = SF.SQL_Execute_NQ(CS, Update)
                Next
                Fill_DGV()
                MsgBox("Done", MsgBoxStyle.Information, "Return To VPL")
            Catch ex As Exception
                MsgBox("Error/ Return To VPL", MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

#End Region


    Private Function Send_Status161_Notification(ByVal Box As String) As Boolean

        Dim aAA As String = My.Application.Culture.ToString
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", False)
        Dim BAA As String = My.Application.Culture.ToString

        Dim HTMLBody As String = Nothing
        Dim Body As String = Nothing
        Dim Subject As String = Nothing

        Dim CP As String = Nothing
        Dim CC As String = Nothing

        Dim attachment() As String = {"bullio"}
        Dim FilePath As String = Nothing
        Dim FileName As String = Nothing

        Dim update_query As String = ""
        Try
            '******************************************
            ' Enviamos la Notificacion para AP
            '******************************************
            Dim DT As DataTable
            DT = SF.GetDataTable(CS, "Select Box From View_Status161_Notification_AP Where Box = '" & Box & "'")

            If DT.Rows.Count > 0 Then

                HTMLBody = GetHTMLcode("C:\P&G\PSSD_BI\html\Status161 Notification.htm")
                HTMLBody = HTMLBody.Replace("X1", "Please your help to process the records in the attached file as each item has already been clarified by PSS.")

                Subject = Box & " Status 161 AP Action Required (" & Date.Now.Month & "-" & Date.Now.Day & "-" & Date.Now.Year & ")"
                FileName = "Status161 AP Report(" & Date.Now.Month & "-" & Date.Now.Day & "-" & Date.Now.Year & ").xls"
                FilePath = "C:\P&G\PSSD_BI\Excel\" & FileName
                CP = ReturnContact(Box)
                CC = Nothing

                'Eliminamos el archivo anterior de Excel si existe con el mismo nombre
                Try
                    Kill(FilePath)
                Catch
                End Try

                CreateExcelFileAP(Box, FilePath)
                attachment(0) = FilePath

                'Enviamos el correo electronico
                BI.Send_eMail(CP, CC, Subject, HTMLBody, attachment, Nothing)

            End If

            '******************************************
            ' Enviamos la Notificacion para PDM 
            '******************************************
            DT = SF.GetDataTable(CS, "Select Box From View_Status161_Notification_PDM Where Box = '" & Box & "'")

            If DT.Rows.Count > 0 Then

                HTMLBody = GetHTMLcode("C:\P&G\PSSD_BI\html\Status161 Notification.htm")
                HTMLBody = HTMLBody.Replace("X1", "Please your help processing attached file as per comments indicated by the VPL for each one.")

                Subject = Box & " Status 161 PDM Action Required (" & Date.Now.Month & "-" & Date.Now.Day & "-" & Date.Now.Year & ")"
                FileName = "Status161 PDM Report(" & Date.Now.Month & "-" & Date.Now.Day & "-" & Date.Now.Year & ").xls"
                FilePath = "C:\P&G\PSSD_BI\Excel\" & FileName
                CP = ReturnContact("PDM")
                CC = Nothing

                'Eliminamos el archivo anterior de Excel si existe con el mismo nombre
                Try
                    Kill(FilePath)
                Catch
                End Try

                CreateExcelFilePDM(Box, FilePath)
                Dim attachment3() As String = {"bullio"}
                attachment3(0) = FilePath

                'Enviamos el correo electronico
                BI.Send_eMail(CP, CC, Subject, HTMLBody, attachment3, Nothing)

            End If


            '******************************************
            ' Enviamos la Notificacion Rejected Records
            '******************************************
            DT = SF.GetDataTable(CS, "Select Box From View_Status161_Notification_Rejected")

            If DT.Rows.Count > 0 Then

                HTMLBody = GetHTMLcode("C:\P&G\PSSD_BI\html\Status161 Notification.htm")
                HTMLBody = HTMLBody.Replace("X1", "Please your help reviewing the status 161 items from the attached file to assign the correct owner for each one.")

                Subject = "Status 161 Rejected Action Required (" & Date.Now.Month & "-" & Date.Now.Day & "-" & Date.Now.Year & ")"
                FileName = "Status161 Rejected Report(" & Date.Now.Month & "-" & Date.Now.Day & "-" & Date.Now.Year & ").xls"
                FilePath = "C:\P&G\PSSD_BI\Excel\" & FileName
                CP = ReturnContact("REJ")
                CC = Nothing

                'Eliminamos el archivo anterior de Excel si existe con el mismo nombre
                Try
                    Kill(FilePath)
                Catch
                End Try

                CreateExcelFileREJ(FilePath)
                Dim attachment2() As String = {"bullio"}
                attachment2(0) = FilePath

                'Enviamos el correo electronico
                BI.Send_eMail(CP, CC, Subject, HTMLBody, attachment2, Nothing)
            End If

            'Registramos el envio de las notificaciones
            DT = SF.GetDataTable(CS, "Select Record From View_Status161_Notification Where Box='" & Box & "'")
            For Each DR As DataRow In DT.Rows
                update_query = "Update Status161_Data Set Email_Date= GetDate(), Email_To='AP/PDM',Email_By='" & Login & "', ProcessLevel=3 Where Record=" & DR("Record")
                Dim Exc As String = SF.SQL_Execute_NQ(CS, update_query)
            Next

            Send_Status161_Notification = True
        Catch ex As Exception
            MsgBox("Error to Send Email Notification: " & ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Send_Status161_Notification = False
        End Try

    End Function

    Private Function ReturnContact(ByVal Box As String) As String
        Try
            Dim Contact As String = Nothing
            Dim DT As DataTable
            DT = SF.GetDataTable(CS, "Select ContactPoint From Status161_Contacts Where Box='" & Box & "'")

            For Each DR As DataRow In DT.Rows
                Contact = DR(0)
            Next
            Return Contact
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub CreateExcelFileAP(ByVal Box As String, ByVal FilePath As String)
        Try
            Dim ExcTranslator As New ExcelTranslator
            Dim DT As DataTable = SF.GetDataTable(DB_CS, "Select * From View_Status161_Notification_AP Where Box= '" & Box & "'")
            ExcTranslator.DataTableToExcel(DT, FilePath, "Status 161")
        Catch ex As Exception
            MsgBox("Error To Create Report, Please Contact The System Administrator", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CreateExcelFileREJ(ByVal FilePath As String)
        Try
            Dim ExcTranslator As New ExcelTranslator
            Dim DT As DataTable = SF.GetDataTable(DB_CS, "Select * From View_Status161_Notification_Rejected")
            ExcTranslator.DataTableToExcel(DT, FilePath, "Status 161")
        Catch ex As Exception
            MsgBox("Error To Create Report, Please Contact The System Administrator", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub CreateExcelFilePDM(ByVal Box As String, ByVal FilePath As String)
        Try
            Dim ExcTranslator As New ExcelTranslator
            Dim DT As DataTable = SF.GetDataTable(DB_CS, "Select * From View_Status161_Notification_PDM Where Box= '" & Box & "'")
            ExcTranslator.DataTableToExcel(DT, FilePath, "Status 161")
        Catch ex As Exception
            MsgBox("Error To Create Report, Please Contact The System Administrator", MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Function GetHTMLcode(ByVal path As String) As String
        Dim HTMLcode As String = ""
        If System.IO.File.Exists(path) = True Then
            Dim objReader As New System.IO.StreamReader(path)
            HTMLcode = objReader.ReadToEnd
            objReader.Close()
        Else
            MsgBox("File: " & path & " Does Not Exist")
        End If
        Return HTMLcode
    End Function


End Class
