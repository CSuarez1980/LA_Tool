Imports System.Windows.Forms

Public Class frm076
    Public StartDate As String
    Public EndDate As String
    Public Data As DataTable
    Public MinPO As String
    Public MaxPO As String

    Public MinPON6 As String
    Public MaxPON6 As String

    'Private WithEvents _N6P As New MultiThreading.WorkerHandler
    Public WithEvents _N6P As New WH
    Public _N6PData As Object

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        Dim cn As New OAConnection.Connection

        ' dgReport.DataSource = Data

        'cn.RunSentence("Delete From SC_EKET Where TNumber = '" & gsUsuarioPC & "'")
        'cn.RunSentence("Delete From SC_EKBE Where TNumber = '" & gsUsuarioPC & "'")
        'cn.RunSentence("Delete from SC_OTD_EKKO Where TNumber = '" & gsUsuarioPC & "'")

        Dim P As New List(Of Data.SqlClient.SqlParameter)
        P.Add(New Data.SqlClient.SqlParameter With {.ParameterName = "@User", .Value = gsUsuarioPC})

        cn.ExecuteStoredProcedure("OTD_Clear_Tables", P)

        pgrBar.Style = Windows.Forms.ProgressBarStyle.Marquee
        tlbHerremientas.Enabled = False
        StartDate = Me.dtpStartDate.Value.ToString
        EndDate = Me.dtpEndDate.Value.ToString

        'BGOTD.RunWorkerAsync()
        'BGOTDN6P.RunWorkerAsync()

       
        BW.RunWorkerAsync()
       


    End Sub
    Private Sub Check()
        If Not BGEKET.IsBusy AndAlso Not BGEKKO.IsBusy AndAlso Not BGEKETN6P.IsBusy AndAlso Not BGEKKON6P.IsBusy Then
            'Dim cn As New OAConnection.Connection
            'Data = cn.RunSentence("Select * From vst_SC_OTD Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
            'dgReport.DataSource = Data
            pgrBar.Style = Windows.Forms.ProgressBarStyle.Blocks
            tlbHerremientas.Enabled = True


        End If
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim cn As New OAConnection.Connection
        cn.ExportDataTableToXL(Data)
    End Sub
    Private Sub frm076_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler _N6P.Report_Downloaded, AddressOf TheEnd
        CheckForIllegalCrossThreadCalls = False
        BS.DataSource = Data
        dgReport.DataSource = BS
    End Sub

    Private Sub Report(ByVal Message As String) Handles _N6P.ReportChange
        lblStatus.Text = Message
    End Sub


#Region "Latin America"
    Private Sub BGOTD_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGOTD.DoWork
        Dim r As New SAPCOM.EKBE_Report("L7P", gsUsuarioPC, AppId)
        Dim TEKBE As New DataTable
        Dim cn As New OAConnection.Connection

        If r.IsReady Then
            r.PostingDateFrom = Date.Parse(StartDate)
            r.PostingDateTo = EndDate
            r.MaterialFrom = "30000000"
            r.MaterialTo = "39999999"
            r.Include_Movement("101")
            r.Include_Movement("105")
            r.Execute()

            If r.Success Then
                TEKBE = r.Data

                MinPO = (From C In r.Data.AsEnumerable() _
                             Where C.Item("Purch Doc") > "450000000" _
                             Select C.Item("Purch Doc")).Min

                MaxPO = (From C In r.Data.AsEnumerable() _
                             Where C.Item("Purch Doc") > "450000000" _
                             Select C.Item("Purch Doc")).Max

                Dim UEKBE As New DataColumn
                UEKBE.ColumnName = "TNumber"
                UEKBE.Caption = "TNumber"
                UEKBE.DefaultValue = gsUsuarioPC

                Dim Reg As New DataColumn
                Reg.ColumnName = "Region"
                Reg.Caption = "Region"
                Reg.DefaultValue = "LA"

                TEKBE.Columns.Add(UEKBE)
                TEKBE.Columns.Add(Reg)

                cn.AppendTableToSqlServer("SC_EKBE", TEKBE)
            End If

        End If
    End Sub
    Private Sub BGOTD_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGOTD.RunWorkerCompleted
        BGEKET.RunWorkerAsync()
        BGEKKO.RunWorkerAsync()
    End Sub
    Private Sub BGEKET_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGEKET.DoWork
        Dim R2 As New SAPCOM.EKET_Report("L7P", gsUsuarioPC, AppId)
        Dim TEKET As New DataTable
        Dim cn As New OAConnection.Connection

        If R2.IsReady Then

            R2.DocumentFrom = MinPO
            R2.DocumentTo = MaxPO
            R2.Execute()

            If R2.Success Then
                TEKET = R2.Data
                Dim UEKET As New DataColumn

                'Columna del Usuario que descarga el reporte
                UEKET.ColumnName = "TNumber"
                UEKET.Caption = "TNumber"
                UEKET.DefaultValue = gsUsuarioPC
                TEKET.Columns.Add(UEKET)

                cn.AppendTableToSqlServer("SC_EKET", TEKET)
                cn.RunSentence("Delete From SC_EKET Where ([Delivery date] = '00000000') or ([Stat Del Date] = '00000000')")

            End If
        End If
    End Sub
    Private Sub BGEKET_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGEKET.RunWorkerCompleted
        Check()
    End Sub
    Private Sub BGEKKO_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGEKKO.DoWork
        Dim EKKO As New SAPCOM.EKKO_Report("L7P", gsUsuarioPC, AppId)

        If EKKO.IsReady Then
            EKKO.DocumentFrom = MinPO
            EKKO.DocumentTo = MaxPO

            EKKO.Execute()
            If EKKO.Success Then
                Dim UEKET As New DataColumn
                UEKET.ColumnName = "TNumber"
                UEKET.Caption = "TNumber"
                UEKET.DefaultValue = gsUsuarioPC
                EKKO.Data.Columns.Add(UEKET)
                Dim cn As New OAConnection.Connection
                cn.AppendTableToSqlServer("SC_OTD_EKKO", EKKO.Data)
            End If
        Else
            MsgBox("Fail to connect with RFC Class")
        End If
    End Sub
    Private Sub BGEKKO_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGEKKO.RunWorkerCompleted
        Check()
    End Sub

#End Region

#Region "North America"
    Private Sub BGEKETN6P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGEKETN6P.RunWorkerCompleted
        Check()
    End Sub
    Private Sub BGEKKON6P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGEKKON6P.RunWorkerCompleted
        Check()
    End Sub
    Private Sub BGOTDN6P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGOTDN6P.RunWorkerCompleted
        Dim i As Integer = 0
        Dim EKKO As New EKKO_W
        Dim EKET As New EKET_W
        _N6P.Clear_Workers()

        For Each PO As String In _N6PData
            i += 1
            EKKO.Include_Document(PO)
            EKET.Include_Document(PO)

            If i = 100 Then
                EKKO.SAPBox = "N6P"
                _N6P.Include_Worker(EKKO)
                EKKO = New EKKO_W
                i = 0

                EKET.SAPBox = "N6P"
                _N6P.Include_Worker(EKET)
                EKET = New EKET_W
            End If
        Next

        If i > 0 Then
            EKKO.SAPBox = "N6P"
            _N6P.Include_Worker(EKKO)

            EKET.SAPBox = "N6P"
            _N6P.Include_Worker(EKET)
        End If

        _N6P.Run_Process()

        'BGEKETN6P.RunWorkerAsync()
        'BGEKKON6P.RunWorkerAsync()
    End Sub
    Private Sub BGEKKON6P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGEKKON6P.DoWork
        Dim EKKO As New SAPCOM.EKKO_Report("N6P", gsUsuarioPC, AppId)

        If EKKO.IsReady Then
            EKKO.DocumentFrom = MinPON6
            EKKO.DocumentTo = MaxPON6

            EKKO.Execute()
            If EKKO.Success Then
                Dim UEKET As New DataColumn
                UEKET.ColumnName = "TNumber"
                UEKET.Caption = "TNumber"
                UEKET.DefaultValue = gsUsuarioPC
                EKKO.Data.Columns.Add(UEKET)
                Dim cn As New OAConnection.Connection
                cn.AppendTableToSqlServer("SC_OTD_EKKO", EKKO.Data)
            End If
        Else
            MsgBox("Fail to connect with RFC Class: BGEKKON6P")
        End If
    End Sub
    Private Sub BGOTDN6P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGOTDN6P.DoWork
        Dim r As New SAPCOM.EKBE_Report("N6P", gsUsuarioPC, AppId)
        Dim TEKBE As New DataTable
        Dim cn As New OAConnection.Connection

        If r.IsReady Then
            r.PostingDateFrom = Date.Parse(StartDate)
            r.PostingDateTo = EndDate
            r.MaterialFrom = "30000000"
            r.MaterialTo = "39999999"
            r.Include_Movement("101")
            r.Include_Movement("105")
            r.Execute()

            If r.Success Then
                TEKBE = r.Data

                _N6PData = (From C In TEKBE.AsEnumerable _
                           Select C.Item("Purch Doc")).Distinct


                MinPON6 = (From C In r.Data.AsEnumerable() _
                             Where C.Item("Purch Doc") > "450000000" _
                             Select C.Item("Purch Doc")).Min

                MaxPON6 = (From C In r.Data.AsEnumerable() _
                             Where C.Item("Purch Doc") > "450000000" _
                             Select C.Item("Purch Doc")).Max

                Dim UEKBE As New DataColumn
                UEKBE.ColumnName = "TNumber"
                UEKBE.Caption = "TNumber"
                UEKBE.DefaultValue = gsUsuarioPC

                Dim Reg As New DataColumn
                Reg.ColumnName = "Region"
                Reg.Caption = "Region"
                Reg.DefaultValue = "NA"

                TEKBE.Columns.Add(UEKBE)
                TEKBE.Columns.Add(Reg)

                cn.AppendTableToSqlServer("SC_EKBE", TEKBE)
            End If
        End If
    End Sub
    Private Sub BGEKETN6P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGEKETN6P.DoWork
        Dim R2 As New SAPCOM.EKET_Report("N6P", gsUsuarioPC, AppId)
        Dim TEKET As New DataTable
        Dim cn As New OAConnection.Connection

        If R2.IsReady Then
            R2.DocumentFrom = MinPON6
            R2.DocumentTo = MaxPON6
            R2.Execute()

            If R2.Success Then
                TEKET = R2.Data
                Dim UEKET As New DataColumn

                'Columna del Usuario que descarga el reporte
                UEKET.ColumnName = "TNumber"
                UEKET.Caption = "TNumber"
                UEKET.DefaultValue = gsUsuarioPC
                TEKET.Columns.Add(UEKET)

                cn.AppendTableToSqlServer("SC_EKET", TEKET)
                cn.RunSentence("Delete From SC_EKET Where ([Delivery date] = '00000000') or ([Stat Del Date] = '00000000')")
            End If
        End If
    End Sub
#End Region

    Private Sub TheEnd()
        Check()
        BS.ResetBindings(False)
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        BS.ResetBindings(False)
    End Sub

    Private Sub BW_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BW.DoWork
        Dim F As New frm091
        F.StartDate = Me.dtpStartDate.Value.ToString
        F.EndDate = Me.dtpEndDate.Value.ToString

        F.ShowDialog()
    End Sub

    Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BW.RunWorkerCompleted
        Dim cn As New OAConnection.Connection
        Data = cn.RunSentence("Select * From vst_SC_OTD Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
        dgReport.DataSource = Data
        pgrBar.Style = Windows.Forms.ProgressBarStyle.Blocks
        tlbHerremientas.Enabled = True
    End Sub
End Class

