Imports oaconnection


Public Class frm086
    Private Data As DataTable

    Private Sub bgwOAs_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwOAs.DoWork
        Dim cn As New OAConnection.Connection
        Dim P As BGWParam = e.Argument
        Dim i As Integer
        Dim SAP As String = ""

        SAP = IIf(P.Region = 1, "L7P", "N6P")

        bgwOAs.ReportProgress(0, "Cleaning report...")
        cn.ExecuteInServer("Delete From OA_Quar_Report Where TNumber = '" & gsUsuarioPC & "'")

        bgwOAs.ReportProgress(0, "PrgChange,Marquee")
        bgwOAs.ReportProgress(0, "Getting SAP Connection")

        Dim Plants As DataTable
        bgwOAs.ReportProgress(0, "Getting report sites")

        If P.Region = 1 Then
            Plants = cn.RunSentence("Select Code From Plant").Tables(0)
        Else
            Plants = cn.RunSentence("Select Code From NA_Plants").Tables(0)
        End If

        Dim POR As New SAPCOM.POs_Report(SAP, gsUsuarioPC, "LAT")
        ' bgwOAs.ReportProgress(0, "PrgChange,Blocks," & Plants.Rows.Count + 1)
        ' i = 0
        For Each R As DataRow In Plants.Rows
            '  bgwOAs.ReportProgress(((i / Plants.Rows.Count) * 100), "Including plant:" & R("Code"))
            POR.IncludePlant(R("Code"))
            ' i += 1
        Next
        ' i = 0

        'POR.IncludeDocument("4500902170")
        POR.IncludeDocsDatedFromTo(P.DateFrom, P.DateTo)
        POR.ExcludeMaterial("")
        POR.Include_OAs = True

        ' bgwOAs.ReportProgress(0, "PrgChange,Marquee")
        bgwOAs.ReportProgress(0, "Getting POs")

        POR.Execute()
        If POR.Success Then
            Dim ER As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
            If ER.IsReady Then
                Dim LOW_MMR = (From C In POR.Data.AsEnumerable() Select C.Item("Doc Number")).Min
                Dim HIGH_MMR = (From C In POR.Data.AsEnumerable() Select C.Item("Doc Number")).Max

                ER.DocumentFrom = LOW_MMR
                ER.DocumentTo = HIGH_MMR
                ER.ExcludeMaterial("")
                ER.AddCustomField("NETWR", "Net Value")
                bgwOAs.ReportProgress(0, "Getting POs Net Price")
                ER.Execute()

                If ER.Success Then
                    ER.ColumnToDoubleStr("Net Value")
                    POR.Data.Columns.Add("Net Value", System.Type.GetType("System.Double"))
                    POR.Data.Columns.Add("Net Value USD", System.Type.GetType("System.Double"), 0)

                    Dim TN As New DataColumn
                    TN.DefaultValue = gsUsuarioPC
                    TN.ColumnName = "TNumber"
                    POR.Data.Columns.Add(TN)

                    bgwOAs.ReportProgress(0, "Updating POs Net Price")
                    ' bgwOAs.ReportProgress(0, "PrgChange,Continous," & POR.Data.Rows.Count)
                    i = 0
                    For Each R As DataRow In POR.Data.Rows
                        bgwOAs.ReportProgress(0, "Updating Net Price: PO " & R("Doc Number"))
                        Dim LV = (From C In ER.Data.AsEnumerable() _
                                  Where (C.Item("Doc Number") = R("Doc Number") And C.Item("Item Number") = R("Item Number")) _
                                  Select C.Item("Net Value")).First

                        R("Net Value") = LV

                        i += 1
                    Next

                    If POR.Data.Columns.IndexOf("Mat Group") <> -1 Then
                        POR.Data.Columns.Remove("Mat Group")
                    End If
                    If POR.Data.Columns.IndexOf("Material") <> -1 Then
                        POR.Data.Columns.Remove("Material")
                    End If
                    If POR.Data.Columns.IndexOf("Short Text") <> -1 Then
                        POR.Data.Columns.Remove("Short Text")
                    End If
                    If POR.Data.Columns.IndexOf("Company Code") <> -1 Then
                        POR.Data.Columns.Remove("Company Code")
                    End If
                    If POR.Data.Columns.IndexOf("Purch Org") <> -1 Then
                        POR.Data.Columns.Remove("Purch Org")
                    End If
                    If POR.Data.Columns.IndexOf("Purch Grp") <> -1 Then
                        POR.Data.Columns.Remove("Purch Grp")
                    End If
                    'If POR.Data.Columns.IndexOf("Plant") <> -1 Then
                    '    POR.Data.Columns.Remove("Plant")
                    'End If
                    If POR.Data.Columns.IndexOf("Doc Date") <> -1 Then
                        POR.Data.Columns.Remove("Doc Date")
                    End If
                    If POR.Data.Columns.IndexOf("Doc Type") <> -1 Then
                        POR.Data.Columns.Remove("Doc Type")
                    End If
                    If POR.Data.Columns.IndexOf("Tracking Field") <> -1 Then
                        POR.Data.Columns.Remove("Tracking Field")
                    End If
                    If POR.Data.Columns.IndexOf("UOM") <> -1 Then
                        POR.Data.Columns.Remove("UOM")
                    End If
                    If POR.Data.Columns.IndexOf("Delivery Comp") <> -1 Then
                        POR.Data.Columns.Remove("Delivery Comp")
                    End If
                    If POR.Data.Columns.IndexOf("Final Invoice") <> -1 Then
                        POR.Data.Columns.Remove("Final Invoice")
                    End If
                    If POR.Data.Columns.IndexOf("Requisitioner") <> -1 Then
                        POR.Data.Columns.Remove("Requisitioner")
                    End If
                    If POR.Data.Columns.IndexOf("Quantity") <> -1 Then
                        POR.Data.Columns.Remove("Quantity")
                    End If
                    If POR.Data.Columns.IndexOf("Price") <> -1 Then
                        POR.Data.Columns.Remove("Price")
                    End If

                    bgwOAs.ReportProgress(0, "Uploading to Server")
                    cn.AppendTableToSqlServer("OA_Quar_Report", POR.Data)


                    bgwOAs.ReportProgress(0, "Uploading currency exchange")
                    cn.ExecuteInServer("Insert into SC_CurExchange(CurID) Select OA_Quar_Report.Currency From SC_CurExchange RIGHT OUTER JOIN OA_Quar_Report ON SC_CurExchange.CurId = OA_Quar_Report.Currency WHERE (SC_CurExchange.CurId IS NULL) GROUP BY OA_Quar_Report.Currency")


                    Dim CE As New DataTable 'Currency Exchange

                    CE = cn.RunSentence("Select Distinct Currency From OA_Quar_Report Where TNumber = '" & gsUsuarioPC & "'").Tables(0)

                    If CE.Rows.Count > 0 Then
                        For Each row As DataRow In CE.Rows
                            Dim Ex As New SAPCOM.SAPExchgRate(SAP, gsUsuarioPC, AppId)

                            Ex.FromCurrency = row("Currency")
                            Ex.ToCurrency = "USD"
                            Ex.Execute()

                            If Ex.Success Then
                                cn.ExecuteInServer("Update SC_CurExchange Set Value = " & IIf(Ex.Rate = 0, 1, Ex.Rate) & " Where CurID = '" & row("Currency") & "'")
                            End If
                        Next
                    End If

                    'cn.ExecuteInServer("Delete From OA_Quar_Report Where ((OA = '') or ([Del Indicator] <> '') or ([Created by] <> 'BACKGROUNDOP'))")
                    cn.ExecuteInServer("Delete From OA_Quar_Report Where ((OA = '') or ([Del Indicator] <> ''))")

                    CE = cn.RunSentence("Select * From SC_CurExchange").Tables(0)

                    bgwOAs.ReportProgress(0, "Updating USD Value")
                    For Each R As DataRow In CE.Rows
                        cn.ExecuteInServer("Update OA_Quar_Report Set [Net Value USD] =([Net Value] / " & (R("Value")) & ") Where Currency = '" & R("CurID") & "'")
                    Next
                Else
                    MsgBox("Error while getting net price: " & ER.ErrMessage, MsgBoxStyle.Exclamation)
                End If
            End If


            bgwOAs.ReportProgress(0, "Finished.")
        Else
            MsgBox("Error getting purchase orders: Message:" & Chr(13) & Chr(13) & POR.ErrMessage, MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        If Not bgwOAs.IsBusy Then
            pgbProgress.Style = Windows.Forms.ProgressBarStyle.Marquee
            bgwOAs.RunWorkerAsync(New BGWParam(dtpFrom.Value, dtpTo.Value, IIf(rbtLA.Checked, 1, 0)))
        Else
            MsgBox("Report is been downloaded, please wait.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub bgwOAs_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwOAs.ProgressChanged
        lblStatus.Text = e.UserState
        pgbProgress.Value = e.ProgressPercentage
    End Sub
    Private Sub bgwOAs_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwOAs.RunWorkerCompleted
        pgbProgress.Style = Windows.Forms.ProgressBarStyle.Blocks
        Dim cn As New OAConnection.Connection

        Data = cn.RunSentence("Select * From vst_OA_Quar_Report Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
        dtgData.DataSource = Data
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        Dim cn As New OAConnection.Connection
        cn.ExportDataTableToXL(Data)
    End Sub
End Class

Friend Class BGWParam
    Private _DateFrom As Date
    Private _DateTo As Date
    Private _Region As Integer = 1

    Public Property DateFrom() As Date
        Get
            Return _DateFrom
        End Get
        Set(ByVal value As Date)
            _DateFrom = value
        End Set
    End Property
    Public Property DateTo() As Date
        Get
            Return _DateTo
        End Get
        Set(ByVal value As Date)
            _DateTo = value
        End Set
    End Property
    Public Property Region() As Integer
        Get
            Return _Region
        End Get
        Set(ByVal value As Integer)
            _Region = value
        End Set
    End Property
    Public Sub New(ByVal pDateFrom As Date, ByVal pDateTo As Date, ByVal pRegion As Integer)
        _DateFrom = pDateFrom
        _DateTo = pDateTo
        _Region = pRegion
    End Sub
End Class