Public Class frm072
    Public StartDate As DateTime = Nothing
    Public EndDate As DateTime = Nothing
    Public Data As DataTable

    Private Sub frm072_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim s As New SAPCOM.Material_In_TDB("L7P", gsUsuarioPC, "LAT")
    End Sub

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        Dim cn As New OAConnection.Connection

        Me.lblStatus.Text = "Status: Downloading reports from SAP."
        Me.lblWorking.Visible = True
        Me.pgbProgress.Style = Windows.Forms.ProgressBarStyle.Marquee

        StartDate = dtpInicio.Value
        EndDate = dtpEnd.Value

        cn.RunSentence("Delete From IC_IROnly Where TNumber = '" & gsUsuarioPC & "'")

        If chkL7P.Checked Then
            BGL7P.RunWorkerAsync()
        End If

        If chkG4P.Checked Then
            BGG4P.RunWorkerAsync()
        End If

        If chkGGBP.Checked Then
            BGGBP.RunWorkerAsync()
        End If

        If chkL6P.Checked Then
            BGL6P.RunWorkerAsync()
        End If

        If chkN6P.Checked Then
            BGN6P.RunWorkerAsync()
        End If
    End Sub

    Private Sub BGL7P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGL7P.DoWork
        Dim Box As String = "L7P"
        Dim SAP As New SAPConection.c_SAP(Box)
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Box, gsUsuarioPC, "LAT")

        SAP.UserName = gsUsuarioPC
        SAP.Password = u.password
        SAP.OpenConnection(SAPConfig)

        If SAP.Conected Then
            Dim dt As New DataTable
            Dim cn As New OAConnection.Connection
            Dim Var As New DataTable

            Var = cn.RunSentence("Select Variant, TNumber From IC_SAP_Variant Where ((Module = 'IROnly') And (Active = 1) And (SAPBox = '" & Box & "'))").Tables(0)

            For Each row As DataRow In Var.Rows
                Try
                    dt = SAP.Get_SAP_IROnly(row("Variant"), row("TNumber"), Box, StartDate, EndDate, PDFPath)

                    If Not dt Is Nothing Then
                        Dim TN As New DataColumn

                        'Columna del Usuatio que descarga el reporte
                        TN.ColumnName = "TNumber"
                        TN.Caption = "TNumber"
                        TN.DefaultValue = gsUsuarioPC

                        dt.Columns.Add(TN)

                        cn.AppendTableToSqlServer("IC_IROnly", dt)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next

            SAP.CloseConnection()
        End If

    End Sub

    Private Sub BGL7P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGL7P.RunWorkerCompleted
        CheckBG()
    End Sub

    Private Sub CheckBG()
        If (Not BGL7P.IsBusy) AndAlso (Not BGG4P.IsBusy) AndAlso (Not BGGBP.IsBusy) AndAlso (Not BGL6P.IsBusy) AndAlso (Not BGN6P.IsBusy) Then
            Me.lblStatus.Text = "Status: Retreaving SQL Server information."
            Dim cn As New OAConnection.Connection

            Data = cn.RunSentence("Select * From IC_IROnly Where TNumber = '" & gsUsuarioPC & "'").Tables(0)

            Me.dtgData.DataSource = Data

            Me.lblWorking.Visible = False
            Me.lblStatus.Text = "Status: Report downloaded. Records found: " & Data.Rows.Count
            Me.pgbProgress.Style = Windows.Forms.ProgressBarStyle.Continuous
        End If
    End Sub

    Private Sub BGG4P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGG4P.DoWork
        Dim Box As String = "G4P"
        Dim SAP As New SAPConection.c_SAP(Box)
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Box, gsUsuarioPC, "LAT")

        SAP.UserName = gsUsuarioPC
        SAP.Password = u.password
        SAP.OpenConnection(SAPConfig)

        If SAP.Conected Then
            Dim dt As New DataTable
            Dim cn As New OAConnection.Connection
            Dim Var As New DataTable

            Var = cn.RunSentence("Select Variant, TNumber From IC_SAP_Variant Where ((Module = 'IROnly') And (Active = 1) And (SAPBox = '" & Box & "'))").Tables(0)

            For Each row As DataRow In Var.Rows
                Try
                    dt = SAP.Get_SAP_IROnly(row("Variant"), row("TNumber"), Box, StartDate, EndDate, PDFPath)

                    If Not dt Is Nothing Then
                        Dim TN As New DataColumn

                        'Columna del Usuatio que descarga el reporte
                        TN.ColumnName = "TNumber"
                        TN.Caption = "TNumber"
                        TN.DefaultValue = gsUsuarioPC

                        dt.Columns.Add(TN)

                        cn.AppendTableToSqlServer("IC_IROnly", dt)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next
            SAP.CloseConnection()
        End If
    End Sub

    Private Sub BGG4P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGG4P.RunWorkerCompleted
        CheckBG()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim cn As New OAConnection.Connection
        cn.ExportDataTableToXL(Data)
    End Sub

    Private Sub BGGBP_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGGBP.DoWork
        Dim Box As String = "GBP"
        Dim SAP As New SAPConection.c_SAP(Box)
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Box, gsUsuarioPC, "LAT")

        SAP.UserName = gsUsuarioPC
        SAP.Password = u.password
        SAP.OpenConnection(SAPConfig)

        If SAP.Conected Then
            Dim dt As New DataTable
            Dim cn As New OAConnection.Connection
            Dim Var As New DataTable

            Var = cn.RunSentence("Select Variant, TNumber From IC_SAP_Variant Where ((Module = 'IROnly') And (Active = 1) And (SAPBox = '" & Box & "'))").Tables(0)

            For Each row As DataRow In Var.Rows
                Try
                    dt = SAP.Get_SAP_IROnly(row("Variant"), row("TNumber"), Box, StartDate, EndDate, PDFPath)

                    If Not dt Is Nothing Then
                        Dim TN As New DataColumn

                        'Columna del Usuatio que descarga el reporte
                        TN.ColumnName = "TNumber"
                        TN.Caption = "TNumber"
                        TN.DefaultValue = gsUsuarioPC

                        dt.Columns.Add(TN)

                        cn.AppendTableToSqlServer("IC_IROnly", dt)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next
            SAP.CloseConnection()
        End If
    End Sub

    Private Sub BGL6P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGL6P.DoWork
        Dim Box As String = "L6P"
        Dim SAP As New SAPConection.c_SAP(Box)
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Box, gsUsuarioPC, "LAT")

        SAP.UserName = gsUsuarioPC
        SAP.Password = u.password
        SAP.OpenConnection(SAPConfig)

        If SAP.Conected Then
            Dim dt As New DataTable
            Dim cn As New OAConnection.Connection
            Dim Var As New DataTable

            Var = cn.RunSentence("Select Variant, TNumber From IC_SAP_Variant Where ((Module = 'IROnly') And (Active = 1) And (SAPBox = '" & Box & "'))").Tables(0)

            For Each row As DataRow In Var.Rows
                Try
                    dt = SAP.Get_SAP_IROnly(row("Variant"), row("TNumber"), Box, StartDate, EndDate, PDFPath)

                    If Not dt Is Nothing Then
                        Dim TN As New DataColumn

                        'Columna del Usuatio que descarga el reporte
                        TN.ColumnName = "TNumber"
                        TN.Caption = "TNumber"
                        TN.DefaultValue = gsUsuarioPC

                        dt.Columns.Add(TN)

                        cn.AppendTableToSqlServer("IC_IROnly", dt)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next
            SAP.CloseConnection()
        End If
    End Sub

    Private Sub BGN6P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGN6P.DoWork
        Dim Box As String = "N6P"
        Dim SAP As New SAPConection.c_SAP(Box)
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData(Box, gsUsuarioPC, "LAT")

        SAP.UserName = gsUsuarioPC
        SAP.Password = u.password
        SAP.OpenConnection(SAPConfig)

        If SAP.Conected Then
            Dim dt As New DataTable
            Dim cn As New OAConnection.Connection
            Dim Var As New DataTable

            Var = cn.RunSentence("Select Variant, TNumber From IC_SAP_Variant Where ((Module = 'IROnly') And (Active = 1) And (SAPBox = '" & Box & "'))").Tables(0)

            For Each row As DataRow In Var.Rows
                Try
                    dt = SAP.Get_SAP_IROnly(row("Variant"), row("TNumber"), Box, StartDate, EndDate, PDFPath)

                    If Not dt Is Nothing Then
                        Dim TN As New DataColumn

                        'Columna del Usuatio que descarga el reporte
                        TN.ColumnName = "TNumber"
                        TN.Caption = "TNumber"
                        TN.DefaultValue = gsUsuarioPC

                        dt.Columns.Add(TN)

                        cn.AppendTableToSqlServer("IC_IROnly", dt)

                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Next
            SAP.CloseConnection()
        End If
    End Sub

    Private Sub BGN6P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGN6P.RunWorkerCompleted
        CheckBG()
    End Sub
    Private Sub BGGBP_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGGBP.RunWorkerCompleted
        CheckBG()
    End Sub
    Private Sub BGL6P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGL6P.RunWorkerCompleted
        CheckBG()
    End Sub
End Class