'Imports Common_Functions.DMT_Shared
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Data

Public Class frmAmericasDMS


    Private WSID As String
    Private CS As String
    Private CSU As String
    Private DT1 As DataTable
    Private DT2 As DataTable
    Private DownloadView As String
    Private DownloadView2 As String
    Private MBS As New BindingSource
    Private SQL1 As String = Nothing
    Private SQL2 As String = Nothing
    Private PBClosing As Boolean = False
    Private WinVersion As String = Nothing
    Private FN1 As String = Nothing
    Private FN2 As String = Nothing
    Private ExportAll As Boolean = False ' Global Variable to identify what the current user needs to download 
    Private WithEvents bw As New BackgroundWorker
    Event ReportProgress(ByVal Msg As String, ByVal Percent As Integer)


    Private Sub frmAmericasDMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DMS_DS.Trigger_Step2' table. You can move, or remove it, as needed.
        'Me.Trigger_Step2TableAdapter.Fill(Me.DMS_DS.Trigger_Step2)
        'TODO: This line of code loads data into the 'DMS_DS.PastDueItems_MMR' table. You can move, or remove it, as needed.
        ' Me.PastDueItems_MMRTableAdapter.Fill(Me.DMS_DS.PastDueItems_MMR)
        'TODO: This line of code loads data into the 'DMS_DS.POConfirmation_MMR_NCNF' table. You can move, or remove it, as needed.
        ' Me.POConfirmation_MMR_NCNFTableAdapter.Fill(Me.DMS_DS.POConfirmation_MMR_NCNF)
        'TODO: This line of code loads data into the 'DMS_DS.REQtoPO_100M' table. You can move, or remove it, as needed.
        '' Me.REQtoPO_100MTableAdapter.Fill(Me.DMS_DS.REQtoPO_100M)
        'TODO: This line of code loads data into the 'DMS_DS.REQtoPO_10M100M' table. You can move, or remove it, as needed.
        'Me.REQtoPO_10M100MTableAdapter.Fill(Me.DMS_DS.REQtoPO_10M100M)
        'TODO: This line of code loads data into the 'DMS_DS.REQtoPO_10M100M' table. You can move, or remove it, as needed.
        'TODO: This line of code loads data into the 'DmS_DS1.REQtoPO_10M' table. You can move, or remove it, as needed.
        'Me.REQtoPO_10MTableAdapter.Fill(Me.DmS_DS1.REQtoPO_10M)

        WSID = "User"
        Dim cn As New OAConnection.Connection
        cn.GetConnectionString()

        'CS = ConStr(WSID, "NA_DMS") ' Connection String to the DMS database
        'CSU = ConStr(WSID, "DMTool_Users") 'Connection String to the Users database

        CS = cn.GetConnectionString
        'CSU = ConStr(WSID, "DMTool_Users") 'Connection String to the Users database

        PictureBox1.Visible = False

        'If Return_UserSystem(CSU) = 1 Then
        '    WinVersion = "C:\Documents and Settings\" & WSID & "\Desktop\"
        'ElseIf Return_UserSystem(CSU) = 2 Then
        '    WinVersion = "C:\Users\" & Environ("USERNAME") & "\Desktop\"
        'End If

        Check_DMS_Exporting()

        Fill_DGV()

        DownloadView = "All_Req2Po"
        DownloadView2 = "NA_Total_Values"

    End Sub

    Private Sub Fill_DGV()
        Dim cn As New OAConnection.Connection
        Dim CS As String = cn.GetConnectionString

        If Not cmdFilter.Checked Then
            'REQtoPO_10MTableAdapter.Connection.ConnectionString = "Data Source=131.190.68.149\SQLEXPRESS;Initial Catalog= NA_DMS;Persist Security Info=True;User ID=developer;Password=hmetal"
            REQtoPO_10MTableAdapter.Connection.ConnectionString = CS
            Me.REQtoPO_10MTableAdapter.FillBy(DMS_DS.REQtoPO_10M())
            Me.RV1.RefreshReport()

            REQtoPO_10M100MTableAdapter.Connection.ConnectionString = CS
            Me.REQtoPO_10M100MTableAdapter.FillBy(DMS_DS.REQtoPO_10M100M())
            Me.RV2.RefreshReport()

            REQtoPO_100MTableAdapter.Connection.ConnectionString = CS
            Me.REQtoPO_100MTableAdapter.FillBy(DMS_DS.REQtoPO_100M())
            Me.RV3.RefreshReport()

            Me.POConfirmation_MMR_NCNFTableAdapter.Connection.ConnectionString = CS
            Me.POConfirmation_MMR_NCNFTableAdapter.FillBy(DMS_DS.POConfirmation_MMR_NCNF())
            Me.RV4.RefreshReport()

            Me.PastDueItems_MMRTableAdapter.Connection.ConnectionString = CS
            Me.PastDueItems_MMRTableAdapter.FillBy(DMS_DS.PastDueItems_MMR())
            Me.RV5.RefreshReport()

            Me.PastDueItems_FFTTableAdapter.Connection.ConnectionString = CS
            Me.PastDueItems_FFTTableAdapter.FillBy(DMS_DS.PastDueItems_FFT())
            Me.RV6.RefreshReport()

            Me.Trigger_Step2TableAdapter.Connection.ConnectionString = CS
            Me.Trigger_Step2TableAdapter.FillBy(DMS_DS.Trigger_Step2())
            Me.RV7.RefreshReport()
        Else
            REQtoPO_10MTableAdapter.Connection.ConnectionString = CS
            Me.REQtoPO_10MTableAdapter.Fill(DMS_DS.REQtoPO_10M(), gsUsuarioPC)
            Me.RV1.RefreshReport()

            REQtoPO_10M100MTableAdapter.Connection.ConnectionString = CS
            Me.REQtoPO_10M100MTableAdapter.Fill(DMS_DS.REQtoPO_10M100M(), gsUsuarioPC)
            Me.RV2.RefreshReport()

            REQtoPO_100MTableAdapter.Connection.ConnectionString = CS
            Me.REQtoPO_100MTableAdapter.Fill(DMS_DS.REQtoPO_100M(), gsUsuarioPC)
            Me.RV3.RefreshReport()

            Me.POConfirmation_MMR_NCNFTableAdapter.Connection.ConnectionString = CS
            Me.POConfirmation_MMR_NCNFTableAdapter.Fill(DMS_DS.POConfirmation_MMR_NCNF(), gsUsuarioPC)
            Me.RV4.RefreshReport()

            Me.PastDueItems_MMRTableAdapter.Connection.ConnectionString = CS
            Me.PastDueItems_MMRTableAdapter.Fill(DMS_DS.PastDueItems_MMR(), gsUsuarioPC)
            Me.RV5.RefreshReport()

            Me.PastDueItems_FFTTableAdapter.Connection.ConnectionString = CS
            Me.PastDueItems_FFTTableAdapter.Fill(DMS_DS.PastDueItems_FFT(), gsUsuarioPC)
            Me.RV6.RefreshReport()

            Me.Trigger_Step2TableAdapter.Connection.ConnectionString = CS
            Me.Trigger_Step2TableAdapter.Fill(DMS_DS.Trigger_Step2(), gsUsuarioPC)
            Me.RV7.RefreshReport()

        End If
    End Sub

    'Private Function Return_UserSystem(ByVal CS As String) As Integer
    '    Dim R As DataRow = GetDataRow(CSU, "SELECT Sys FROM Users WHERE TNumber  = '" & WSID & "'")
    '    If Not R Is Nothing Then
    '        Return_UserSystem = R("Sys")
    '    Else
    '        Return_UserSystem = 1
    '    End If
    'End Function

    Private Sub Check_DMS_Exporting()
        'Try
        '    Dim Vl As String = Nothing

        '    Vl = SQL_Execute_SC(CSU, "SELECT DMS_Export_All FROM Users WHERE TNumber = '" & WSID & "'")

        '    If Not DBNull.Value.Equals(Vl) Then
        '        If Vl = "True" Then
        '            ExportAll = True
        '        Else
        '            ExportAll = False
        '        End If
        '    End If

        'Catch ex As Exception
        '    ExportAll = False
        'End Try
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Fill_DGV()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If PBClosing = True Then
            Timer1.Enabled = False
            PictureBox1.Visible = False
        End If
    End Sub

    Private Sub TP1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TP1.Enter
        DownloadView = "All_Req2Po"
        FN1 = "All_Req2Po"
        DownloadView2 = "NA_Total_Values"
        FN2 = "All_Req2Po_GB"
    End Sub

    Private Sub TP2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TP2.Enter
        DownloadView = "All_Req2Po"
        FN1 = "All_Req2Po"
        DownloadView2 = "NA_Total_Values"
        FN2 = "All_Req2Po_GB"
    End Sub

    Private Sub TP3_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TP3.Enter
        DownloadView = "All_Req2Po"
        FN1 = "All_Req2Po"
        DownloadView2 = "NA_Total_Values"
        FN2 = "All_Req2Po_GB"
    End Sub

    Private Sub TP4_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TP4.Enter
        DownloadView = "NA_Confirmation_Values" 'Confirmation = 'No'
        FN1 = "NA_Confirmation_No"
        DownloadView2 = "NA_Confirmation_Values_Conf" 'Confirmation = 'Yes'
        FN1 = "NA_Confirmation_Yes"
    End Sub

    Private Sub TP5_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TP5.Enter
        DownloadView = "MMR_PastDues"
        FN1 = "MMR_PastDues"
        DownloadView2 = ""
        FN2 = ""
    End Sub

    Private Sub TP6_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TP6.Enter
        DownloadView = "FFT_PastDues"
        FN1 = "FFT_PastDues"
        DownloadView2 = ""
        FN2 = ""
    End Sub

    Private Sub TP7_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TP7.Enter
        DownloadView = "All_Trigger"
        FN1 = "All_Trigger"
        DownloadView2 = ""
        FN1 = ""
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        bw.WorkerReportsProgress = True
        bw.WorkerSupportsCancellation = True

        Dim I As Integer = e.Argument
        Select Case I
            Case 0
                Export()
        End Select
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        'Do Nothing
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        lblStatusl.Text = My.Computer.Clock.LocalTime.ToShortTimeString & " Process Reports Completed...!"
        PBClosing = True
    End Sub

    Private Function Export() As Boolean
        Try
            Export = False

            Load_DTs()

            If DT1.Rows.Count > 0 Then
                If DataTableToExcel(DT1, WinVersion & FN1 & ".XLS") Then
                End If
            End If

            If DT2.Rows.Count > 0 Then
                If DataTableToExcel(DT2, WinVersion & FN2 & ".XLS") Then
                End If
            End If

            Export = True
        Catch ex As Exception
            Export = False
        End Try
    End Function

    Private Sub Load_DTs()
        Dim cn As New OAConnection.Connection

        If DownloadView <> "" Then

            If ExportAll Then
                SQL1 = "SELECT * FROM " & DownloadView
                DT1 = cn.RunSentence(SQL1).Tables(0)

                'DT1 = GetDataTable(CS, SQL1)
            Else
                SQL1 = "SELECT * FROM " & DownloadView & " WHERE SPS = '" & WSID & "'"
                DT1 = cn.RunSentence(SQL1).Tables(0)
                ' DT1 = GetDataTable(CS, SQL1)
            End If
        End If

        If DownloadView2 <> "" Then
            If ExportAll Then
                SQL2 = "SELECT * FROM " & DownloadView2
                DT2 = cn.RunSentence(SQL2).Tables(0)
                'DT2 = GetDataTable(CS, SQL2)
            Else
                SQL2 = "SELECT * FROM " & DownloadView2 & " WHERE SPS = '" & WSID & "'"
                DT2 = cn.RunSentence(SQL2).Tables(0)
                'DT2 = GetDataTable(CS, SQL2)
            End If
        End If
    End Sub
    Public Function DataTableToExcel(ByVal DT As DataTable, ByVal Path As String) As Boolean

        'DataTableToExcel = True
        'If My.Computer.FileSystem.FileExists(Path) Then
        '    Try
        '        My.Computer.FileSystem.DeleteFile(Path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin, FileIO.UICancelOption.ThrowException)
        '    Catch ex As Exception
        '        DataTableToExcel = False
        '        Exit Function
        '    End Try
        'End If

        'Dim AP As New Microsoft.Office.Interop.Excel.Application
        'Dim WB As Microsoft.Office.Interop.Excel.Workbook = AP.Workbooks.Add
        'Dim WS As Microsoft.Office.Interop.Excel.Worksheet = WB.Sheets(1)
        'Dim dc As DataColumn
        'Dim iCols As Int32 = 0
        'For Each dc In DT.Columns
        '    WS.Range("A1").Offset(0, iCols).Value = dc.ColumnName
        '    iCols += 1
        'Next
        'With WS.Range(WS.Cells(1, 1), WS.Cells(1, iCols - 1)).Interior
        '    .Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid
        '    .PatternColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
        '    .ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorLight2
        '    .TintAndShade = 0.399975585192419
        '    .PatternTintAndShade = 0
        'End With
        'With WS.Range(WS.Cells(1, 1), WS.Cells(1, iCols - 1)).Font
        '    .ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorDark1
        '    .TintAndShade = 0
        'End With
        'Dim iRows As Int32
        'For iRows = 0 To DT.Rows.Count - 1
        '    WS.Range("A2").Offset(iRows).Resize(1, iCols).Value = DT.Rows(iRows).ItemArray()
        'Next

        'WS.Columns.AutoFit()
        'WS.Range("A1").Select()
        'Try
        '    WB.SaveAs(Path)
        'Catch ex As Exception
        '    DataTableToExcel = False
        'End Try
        'WB.Close(False)
        'AP.Quit()

    End Function
    Private Sub ExcelButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelButton1.Click
        Dim cn As New OAConnection.Connection
        Dim dt As New DataTable
        Dim qr As String = ""

        Select Case TCMain.SelectedIndex
            Case 0
                qr = "Select * From vst_LA_Open_Req Where ([Total USD] < 2500)"
            Case 1
                qr = "Select * From vst_LA_Open_Req Where ([Total USD] between 2500 and 19999.99)"
            Case 2
                qr = "Select * From vst_LA_Open_Req Where ([Total USD] >= 20000)"
            Case 3
                'Filtro para tres vendors aplicado en la vista
                qr = "Select * From vst_DMS_PO_Confirmed"
            Case 4
                qr = "Select * From vst_LA_Open_Items Where (Material <> '')"
            Case 5
                qr = "Select * From vst_LA_Open_Items Where (Material = '')"
            Case 6
                qr = "Select * From vst_DMS_LA_Trigeer_All"
        End Select

        If cmdFilter.Checked Then
            If TCMain.SelectedIndex = 3 Or TCMain.SelectedIndex = 6 Then
                qr = qr & " Where (sps = '" & gsUsuarioPC & "')"
            Else
                qr = qr & " And (sps = '" & gsUsuarioPC & "')"
            End If
        End If

        dt = cn.RunSentence(qr).Tables(0)

        If dt.Rows.Count > 0 Then
            cn.ExportDataTableToXL(dt)
        Else
            MsgBox("No records could be selected.")
        End If




        'If bw.IsBusy Then
        '    MsgBox("Process is already running!", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'Else
        '    ' If IsConfirmed() Then
        '    PBClosing = False
        '    PictureBox1.Visible = True
        '    Timer1.Enabled = True
        '    Me.BackgroundWorker1.RunWorkerAsync(0)
        '    'End If
        'End If
    End Sub

    Private Sub cmdFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilter.Click
        Fill_DGV()
    End Sub

    
End Class