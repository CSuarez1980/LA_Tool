Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports Microsoft.Office.Interop
Imports Shared_Functions


Public Class BI_Functions

    Private SF As New MyFunctions_Class

#Region "Functions"

    Public Function returnMonth(ByVal m As String) As String
        Dim return_month As String = ""
        Try
            Select Case m
                Case "1"
                    return_month = "Jan"
                Case "2"
                    return_month = "Feb"
                Case "3"
                    return_month = "Mar"
                Case "4"
                    return_month = "Apr"
                Case "5"
                    return_month = "May"
                Case "6"
                    return_month = "Jun"
                Case "7"
                    return_month = "Jul"
                Case "8"
                    return_month = "Aug"
                Case "9"
                    return_month = "Sep"
                Case "10"
                    return_month = "Oct"
                Case "11"
                    return_month = "Nov"
                Case "12"
                    return_month = "Dec"
                Case Else
                    return_month = ""
            End Select
        Catch
            return_month = ""
        End Try
        Return return_month
    End Function

    Public Function ReturnMonth00(ByVal m As String) As String
        Dim return_month As String = ""
        Try
            Select Case m
                Case "01"
                    return_month = "Jan"
                Case "02"
                    return_month = "Feb"
                Case "03"
                    return_month = "Mar"
                Case "04"
                    return_month = "Apr"
                Case "05"
                    return_month = "May"
                Case "06"
                    return_month = "Jun"
                Case "07"
                    return_month = "Jul"
                Case "08"
                    return_month = "Aug"
                Case "09"
                    return_month = "Sep"
                Case "10"
                    return_month = "Oct"
                Case "11"
                    return_month = "Nov"
                Case "12"
                    return_month = "Dec"
                Case Else
                    return_month = ""
            End Select
        Catch
            return_month = ""
        End Try
        Return return_month
    End Function

    Public Function ReturnMonthName00(ByVal m As String) As String
        Dim return_month As String = ""
        Try
            Select Case m
                Case "01"
                    return_month = "January"
                Case "02"
                    return_month = "February"
                Case "03"
                    return_month = "March"
                Case "04"
                    return_month = "April"
                Case "05"
                    return_month = "May"
                Case "06"
                    return_month = "June"
                Case "07"
                    return_month = "July"
                Case "08"
                    return_month = "August"
                Case "09"
                    return_month = "September"
                Case "10"
                    return_month = "October"
                Case "11"
                    return_month = "November"
                Case "12"
                    return_month = "December"
                Case Else
                    return_month = ""
            End Select
        Catch
            return_month = ""
        End Try
        Return return_month
    End Function

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

    Public Sub Send_eMail(ByVal Recipient As String, ByVal CopyTo As String, ByVal Subject As String, ByVal Body As String, ByVal Attachments() As String, ByVal OnBehalfOf As String)
        Dim MyolApp
        Dim myNameSpace
        Dim objMail
        Dim A As String
        'Dim SecurityManager As New AddinExpress.Outlook.SecurityManager
        Try
            MyolApp = CreateObject("Outlook.Application")
            myNameSpace = MyolApp.GetNamespace("MAPI")
            objMail = MyolApp.CreateItem(0)

            With objMail
                MyolApp = .GetInspector()
                .Subject = Subject
                .HTMLBody = Body & .HTMLBody
                .To = Recipient
                .CC = CopyTo
                .SentOnBehalfOfName = OnBehalfOf
                If Not Attachments Is Nothing Then
                    For Each A In Attachments
                        If A <> "" Then
                            .Attachments.Add(CStr(A))
                        End If
                    Next
                End If
            End With

            'SMI = CreateObject("Redemption.SafeMailItem")
            'SMI.Item = objMail
            'SMI.Send()

            objMail.Send()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Outlook Error")
        End Try
    End Sub

    Public Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Function ChangeDateFormat(ByVal ddmmyyyy As String) As String 'dd/mm/yyyy to mm/dd/yyyy
        Dim Parts() As String = Nothing
        Dim DateParts() As String = Nothing

        Dim Date_part As String = ""
        Dim Time_part As String = ""
        Try
            Parts = Split(ddmmyyyy, " ", -1)
            If (Parts.Length > 0) Then
                Date_part = Parts(0)
                Time_part = Parts(1)
                DateParts = Split(Date_part, "/", -1)
                ChangeDateFormat = DateParts(1) & "/" & DateParts(0) & "/" & DateParts(2) & " " & Time_part

            Else
                ChangeDateFormat = ""
            End If
        Catch ex As Exception
            ChangeDateFormat = ""
        End Try
    End Function

    Public Function ChangeDateFormat(ByVal ddmmyyyy As String, ByVal HaveHour As Boolean) As String 'dd/mm/yyyy to mm/dd/yyyy (With Hour)
        Dim Parts() As String = Nothing
        Dim DateParts() As String = Nothing

        Dim Date_part As String = ""
        Dim Time_part As String = ""
        Try
            Parts = Split(ddmmyyyy, " ", -1)
            If (Parts.Length > 0) Then
                Date_part = Parts(0)
                DateParts = Split(Date_part, "/", -1)
                If HaveHour Then
                    Time_part = Parts(1)
                    ChangeDateFormat = DateParts(1) & "/" & DateParts(0) & "/" & DateParts(2) & " " & Time_part
                Else
                    ChangeDateFormat = DateParts(1) & "/" & DateParts(0) & "/" & DateParts(2)
                End If
            Else
                ChangeDateFormat = ""
            End If
        Catch ex As Exception
            ChangeDateFormat = ""
        End Try
    End Function

    Public Function SplitDate(ByVal ddmmyyyy As String, ByVal date_part As String) As String
        Dim Return_part As String = ""
        Dim Parts() As String
        Dim year() As String
        Try
            Parts = Split(ddmmyyyy, "/", -1)
            year = Split(Parts(2), " ", -1)
            If (Parts.Length > 0) Then
                Select Case date_part
                    Case "dd"
                        Return_part = Parts(0)
                    Case "mm"
                        Return_part = Parts(1)
                    Case "yyyy"
                        Return_part = year(0)
                    Case Else
                        Return_part = ""
                End Select
            End If
        Catch ex As Exception
            Return_part = ""
        End Try
        Return Return_part
    End Function

    Public Function ReturnDescription(ByVal CS As String, ByVal table As String, ByVal column As String, ByVal Filter As String) As String
        Dim Description As String = ""
        Dim SF As New Shared_Functions.MyFunctions_Class
        Try
            Dim DT As DataTable = SF.GetSQLTable(CS, table, Filter & " and " & column & " is not null")
            Dim R As DataRow

            If DT.Rows.Count = 1 Then
                R = DT.Rows(0)
                Description = R(column).ToString()
            End If
        Catch
            Description = ""
        End Try
        Return Description
    End Function

    Public Function BooleanConvertion(ByVal val As String) As Integer
        BooleanConvertion = 1
        If (val = "False") Then
            BooleanConvertion = 0
        End If
    End Function

    Public Sub Load_ComboBox(ByVal ComboBox As ComboBox, ByVal CS As String, ByVal sql_select As String)
        Dim cn As New SqlConnection(CS)
        Try
            cn.Open()
            Dim cmd As New SqlCommand(sql_select, cn)
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            ComboBox.DataSource = ds.Tables(0)
            ComboBox.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
            ComboBox.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    Public Function Return_TotalRows(ByVal table As String) As String

        Dim DT As DataTable
        Dim Count As String = "?"
        Try
            DT = SF.GetDataTable(DB_CS, "Select Total From " & table)

            For Each DR As DataRow In DT.Rows
                Count = DR(0)
            Next
            Return_TotalRows = Count
        Catch ex As Exception
            Return_TotalRows = "?"
        End Try

    End Function
#End Region

#Region "Project Functions"

    Public Function ReturnBackup(ByVal CS As String, ByVal User As String) As String
        ReturnBackup = Me.ReturnDescription(CS, "BI_Users", "BackupT", "Tnumber='" & User & "'")
    End Function

    Public Function IsAdministrator(ByVal Tnumber As String) As Boolean
        Dim response As String = ReturnDescription(DB_CS, "BI_Users", "Administrator", "Tnumber='" & Tnumber & "'")
        If response = "False" Then
            IsAdministrator = False
        Else
            IsAdministrator = True
        End If
    End Function

    Public Function Status161_Permission(ByVal Tnumber As String) As Boolean
        Dim response As String = ReturnDescription(DB_CS, "BI_Users", "Status161_Notification", "Tnumber='" & Tnumber & "'")
        If response = "False" Then
            Status161_Permission = False
        Else
            Status161_Permission = True
        End If
    End Function

    Public Function Update_NONCPO() As Boolean
        Try
            If SF.SQL_Execute_NQ(DB_CS, "Exec Update_NON_CPO") = Nothing Then
                Update_NONCPO = True
            Else
                Update_NONCPO = False
            End If
        Catch ex As Exception
            Update_NONCPO = False
        End Try
    End Function

    Public Function Update_ToRelease() As Boolean
        Try
            If SF.SQL_Execute_NQ(DB_CS, "Exec Update_ToRelease") = Nothing Then
                Update_ToRelease = True
            Else
                Update_ToRelease = False
            End If
        Catch ex As Exception
            Update_ToRelease = False
        End Try
    End Function

#End Region

End Class

'Public Sub Send_eMail(ByVal Recipient As String, ByVal CopyTo As String, ByVal Subject As String, ByVal Body As String, ByVal Attachments() As String, ByVal OnBehalfOf As String)
'    Dim MyolApp
'    Dim myNameSpace
'    Dim objMail
'    Dim A As String
'    Try
'        MyolApp = CreateObject("Outlook.Application")
'        myNameSpace = MyolApp.GetNamespace("MAPI")
'        objMail = MyolApp.CreateItem(0)

'        With objMail
'            MyolApp = .GetInspector()
'            .Subject = Subject
'            .HTMLBody = Body & .HTMLBody
'            .To = Recipient
'            .CC = CopyTo
'            .SentOnBehalfOfName = OnBehalfOf
'            If Not Attachments Is Nothing Then
'                For Each A In Attachments
'                    If A <> "" Then
'                        .Attachments.Add(CStr(A))
'                    End If
'                Next
'            End If
'        End With

'        'SMI = CreateObject("Redemption.SafeMailItem")
'        'SMI.Item = objMail
'        'SMI.Send()

'        objMail.Send()
'    Catch ex As Exception
'        MsgBox(ex.Message, MsgBoxStyle.Critical, "Outlook Error")
'    End Try
'End Sub