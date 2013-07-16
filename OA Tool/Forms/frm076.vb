Public Class frm076
    Public StartDate As String
    Public EndDate As String
    Public Data As DataTable
    Public MinPO As String
    Public MaxPO As String


    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        Dim cn As New OAConnection.Connection
        cn.RunSentence("Delete From SC_EKET Where TNumber = '" & gsUsuarioPC & "'")
        cn.RunSentence("Delete From SC_EKBE Where TNumber = '" & gsUsuarioPC & "'")
        cn.RunSentence("Delete from SC_OTD_EKKO Where TNumber = '" & gsUsuarioPC & "'")

        pgrBar.Style = Windows.Forms.ProgressBarStyle.Marquee
        tlbHerremientas.Enabled = False
        StartDate = Me.dtpStartDate.Value.ToString
        EndDate = Me.dtpEndDate.Value.ToString

        BGOTD.RunWorkerAsync()

    End Sub

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

                TEKBE.Columns.Add(UEKBE)
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

                '    For Each row As DataRow In TEKET.Rows
                '        Dim d As DateTime

                '        If DateTime.TryParse(row("Stat Del Date"), d) Then
                '            If d > #1/1/2050# Then
                '                row("Stat Del Date") = row("Delivery Date")
                '            End If
                '        Else
                '            row.Delete()
                '        End If

                '    Next
                '    TEKET.AcceptChanges()
                '

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
    Private Sub Check()
        If Not BGEKET.IsBusy AndAlso Not BGEKKO.IsBusy Then
            Dim cn As New OAConnection.Connection

            Data = cn.RunSentence("Select * From vst_SC_OTD Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
            dgReport.DataSource = Data
            pgrBar.Style = Windows.Forms.ProgressBarStyle.Blocks
            tlbHerremientas.Enabled = True
        End If
    End Sub
    Private Sub frm076_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim cn As New OAConnection.Connection
        cn.ExportDataTableToXL(Data)
    End Sub
End Class