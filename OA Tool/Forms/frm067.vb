Imports OAConnection
Imports SAPCOM.RepairsLevels


'Para eliminar las PO's fuera del scope de PSS
'DELETE FROM dbo.SC_POReport
'WHERE     (Usuario = 'BM4691') AND ([Doc Type] <> 'EC') AND (SAPBox <> 'L7P') AND (NOT EXISTS
'                          (SELECT     Name, Tnumber, Status
'                            FROM          dbo.[PSS People]
'                            WHERE      (dbo.SC_POReport.[Created By] = Tnumber)))






Public Class frm067
    'DELETE FROM dbo.SC_EBAN
    'WHERE     (TNumber = 'BM4691') AND (NOT EXISTS
    '(SELECT   * FROM          dbo.SC_POReport AS B WHERE      (dbo.SC_EBAN.[Purch Doc] = [Doc Number]) AND (dbo.SC_EBAN.[PO Item] = [Item Number])))
    Public StartDate As String
    Public EndDate As String
    Public Rep As New DataTable
    Public MinOTDPO As String
    Public MaxOTDPO As String

    Private Param_NAST_L7P As New P_Nast
    Private Param_NAST_L6P As New P_Nast
    Private Param_NAST_G4P As New P_Nast
    Private Param_NAST_GBP As New P_Nast

    Public fPO As Boolean = True
    Public fTra As Boolean = True
    Public fOI As Boolean = True
    Public fOTD As Boolean = True


    Public HasError As Boolean = False
    Public ErrorList As New List(Of String)


    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        tlbHerremientas.Enabled = False
        'CheckPOWorkers()
        Dim cn As New OAConnection.Connection
        pgrBar.Style = Windows.Forms.ProgressBarStyle.Marquee
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\working.gif")

        Panel1.Visible = True

        StartDate = dtpStartDate.Text
        EndDate = dtpEndDate.Text

        fPO = chkPO.Checked
        fTra = chkTrn.Checked
        fOTD = chkOTD.Checked

        If fTra Then
            cn.ExecuteInServer("Delete From SC_NAST Where [TNumber] = '" & gsUsuarioPC & "'")
        End If

        If fPO Then
            cn.ExecuteInServer("Delete From SC_POReport Where [Usuario] = '" & gsUsuarioPC & "'")
            cn.ExecuteInServer("Delete From SC_POReport_Requis Where [User] = '" & gsUsuarioPC & "'")
            cn.ExecuteInServer("Delete From SC_EBAN Where [TNumber] = '" & gsUsuarioPC & "'")
        End If

        If fPO Then
            If (chkL7P.Checked) Then
                imgL7P.Image = I
                BGL7P.RunWorkerAsync()
            End If

            If (chkL6P.Checked) Then
                imgL6P.Image = I
                BGL6P.RunWorkerAsync()
            End If

            If chkG4P.Checked Then
                imgG4P.Image = I
                BGG4P.RunWorkerAsync()
            End If

            If chkGBP.Checked Then
                imgGBP.Image = I
                BGGBP.RunWorkerAsync()
            End If
        End If

        bgExchange.RunWorkerAsync()

        If fOTD Then
            cn.RunSentence("Delete from SC_OTD_EKKO Where TNumber = '" & gsUsuarioPC & "'")
            cn.RunSentence("Delete From SC_EKET Where TNumber = '" & gsUsuarioPC & "'")
            cn.RunSentence("Delete From SC_EKBE Where TNumber = '" & gsUsuarioPC & "'")

            BGOTD.RunWorkerAsync()
            imgOTD.Image = I
        End If


    End Sub
    Private Sub BGL7P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGL7P.DoWork
        Dim SAP As String = "L7P"
        Dim dtPlants As New OAConnection.SQLInstruction(eSQLInstruction.Select)
        Dim Rep As New SAPCOM.POs_Report(SAP, gsUsuarioPC, AppId)
        Dim POs As New DataTable
        Dim cn As New OAConnection.Connection
        dtPlants.Tabla = "SC_Plant"
        dtPlants.AgregarParametro(New SQLInstrucParam("Plant_Code", "", False))
        dtPlants.Execute()

        For Each row As DataRow In dtPlants.Data.Rows
            Rep.IncludePlant(row("Plant_Code"))
        Next
        Rep.IncludeDocsDatedFromTo(StartDate, EndDate)
        Rep.RepairsLevel = IncludeRepairs
        Rep.Execute()
        If Not Rep.Success Then
            HasError = True
            ErrorList.Add(SAP & ": Getting PO Report")
        Else
            If Rep.ErrMessage = Nothing Then
                POs = Rep.Data
                If POs.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0219")
                End If
                If POs.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
                    POs.Columns.Remove("EKPO-ZWERT")
                End If
                If POs.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0218")
                End If
                If POs.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0220")
                End If
                If POs.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
                    POs.Columns.Remove("EKKO-MEMORYTYPE")
                End If
                POs = Rep.Data
                POs.Columns.Add("Status")
                POs.Columns.Add("Confirm")
                Dim ETN As New DataColumn
                Dim ESB As New DataColumn
                ETN.ColumnName = "Usuario"
                ETN.Caption = "Usuario"
                ETN.DefaultValue = gsUsuarioPC
                ESB.DefaultValue = SAP
                ESB.ColumnName = "SAPBox"
                ESB.Caption = "SAPBox"
                POs.Columns.Add(ETN)
                POs.Columns.Add(ESB)
                cn.AppendTableToSqlServer("SC_POReport", POs)
                Dim MinPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Min

                Dim MaxPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Max

                Dim R2 As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
                R2.DocumentFrom = MinPO
                R2.DocumentTo = MaxPO
                R2.AddCustomField("BANFN", "Requisition")
                R2.AddCustomField("BNFPO", "Req Item")
                R2.AddCustomField("IDNLF", "IDNLF")
                R2.AddCustomField("BRTWR", "GS VALUE")
                R2.Execute()
                If Not R2.Success Then
                    HasError = True
                    ErrorList.Add(SAP & ": Getting CAT-EKPO Report")
                Else
                    R2.ColumnToDoubleStr("Requisition")
                    R2.ColumnToDoubleStr("Req Item")

                    If R2.Data.Columns.IndexOf("Short Text") <> -1 Then
                        R2.Data.Columns.Remove("Short Text")
                    End If
                    If R2.Data.Columns.IndexOf("Material") <> -1 Then
                        R2.Data.Columns.Remove("Material")
                    End If
                    If R2.Data.Columns.IndexOf("Plant") <> -1 Then
                        R2.Data.Columns.Remove("Plant")
                    End If
                    If R2.Data.Columns.IndexOf("Inforecord") <> -1 Then
                        R2.Data.Columns.Remove("Inforecord")
                    End If
                    If R2.Data.Columns.IndexOf("Quantity") <> -1 Then
                        R2.Data.Columns.Remove("Quantity")
                    End If
                    If R2.Data.Columns.IndexOf("UOM") <> -1 Then
                        R2.Data.Columns.Remove("UOM")
                    End If
                    If R2.Data.Columns.IndexOf("Price") <> -1 Then
                        R2.Data.Columns.Remove("Price")
                    End If
                    If R2.Data.Columns.IndexOf("Tax Code") <> -1 Then
                        R2.Data.Columns.Remove("Tax Code")
                    End If
                    If R2.Data.Columns.IndexOf("PDT") <> -1 Then
                        R2.Data.Columns.Remove("PDT")
                    End If
                    If R2.Data.Columns.IndexOf("Mat Group") <> -1 Then
                        R2.Data.Columns.Remove("Mat Group")
                    End If
                    If R2.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                        R2.Data.Columns.Remove("Tracking Fld")
                    End If
                    If R2.Data.Columns.IndexOf("Price Unit") <> -1 Then
                        R2.Data.Columns.Remove("Price Unit")
                    End If
                    Dim ETN2 As New DataColumn
                    Dim ESB2 As New DataColumn
                    ETN2.ColumnName = "Usuario"
                    ETN2.Caption = "Usuario"
                    ETN2.DefaultValue = gsUsuarioPC
                    ESB2.DefaultValue = SAP
                    ESB2.ColumnName = "SAPBox"
                    ESB2.Caption = "SAPBox"
                    R2.Data.Columns.Add(ETN2)
                    R2.Data.Columns.Add(ESB2)
                    cn.AppendTableToSqlServer("SC_POReport_Requis", R2.Data)
                    Dim MinCT = (From C In POs.AsEnumerable() _
                            Where C.Item("Doc Number") < "450000000" _
                            Select C.Item("Doc Number")).Min

                    Dim MaxCT = (From C In POs.AsEnumerable() _
                                 Where C.Item("Doc Number") < "450000000" _
                                 Select C.Item("Doc Number")).Max

                    Param_NAST_L7P.Max_PO = MaxPO
                    Param_NAST_L7P.Min_PO = MinPO
                    Param_NAST_L7P.Max_Cat = MaxCT
                    Param_NAST_L7P.Min_Cat = MinCT

                    Dim R3 As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
                    R3.DocumentFrom = MinCT
                    R3.DocumentTo = MaxCT
                    R3.AddCustomField("BANFN", "Requisition")
                    R3.AddCustomField("BNFPO", "Req Item")
                    R3.AddCustomField("IDNLF", "IDNLF")
                    R3.AddCustomField("BRTWR", "GS VALUE")
                    R3.Execute()

                    If Not R3.Success Then
                        HasError = True
                        ErrorList.Add(SAP & ": Getting EKPO-MMR Report")
                    Else
                        R3.ColumnToDoubleStr("Requisition")
                        R3.ColumnToDoubleStr("Req Item")
                        If R3.Data.Columns.IndexOf("Short Text") <> -1 Then
                            R3.Data.Columns.Remove("Short Text")
                        End If
                        If R3.Data.Columns.IndexOf("Material") <> -1 Then
                            R3.Data.Columns.Remove("Material")
                        End If
                        If R3.Data.Columns.IndexOf("Plant") <> -1 Then
                            R3.Data.Columns.Remove("Plant")
                        End If
                        If R3.Data.Columns.IndexOf("Inforecord") <> -1 Then
                            R3.Data.Columns.Remove("Inforecord")
                        End If
                        If R3.Data.Columns.IndexOf("Quantity") <> -1 Then
                            R3.Data.Columns.Remove("Quantity")
                        End If
                        If R3.Data.Columns.IndexOf("UOM") <> -1 Then
                            R3.Data.Columns.Remove("UOM")
                        End If
                        If R3.Data.Columns.IndexOf("Price") <> -1 Then
                            R3.Data.Columns.Remove("Price")
                        End If
                        If R3.Data.Columns.IndexOf("Tax Code") <> -1 Then
                            R3.Data.Columns.Remove("Tax Code")
                        End If
                        If R3.Data.Columns.IndexOf("PDT") <> -1 Then
                            R3.Data.Columns.Remove("PDT")
                        End If
                        If R3.Data.Columns.IndexOf("Mat Group") <> -1 Then
                            R3.Data.Columns.Remove("Mat Group")
                        End If
                        If R3.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                            R3.Data.Columns.Remove("Tracking Fld")
                        End If
                        If R3.Data.Columns.IndexOf("Price Unit") <> -1 Then
                            R3.Data.Columns.Remove("Price Unit")
                        End If
                        Dim ETN3 As New DataColumn
                        Dim ESB3 As New DataColumn
                        ETN3.ColumnName = "Usuario"
                        ETN3.Caption = "Usuario"
                        ETN3.DefaultValue = gsUsuarioPC
                        ESB3.DefaultValue = SAP
                        ESB3.ColumnName = "SAPBox"
                        ESB3.Caption = "SAPBox"
                        R3.Data.Columns.Add(ETN3)
                        R3.Data.Columns.Add(ESB3)
                        cn.AppendTableToSqlServer("SC_POReport_Requis", R3.Data)

                        Dim FixEKPO As DataTable
                        FixEKPO = cn.RunSentence("Select [Doc Number] From SC_POReport_Requis Where ([GS Value] Like '%*%') And (SAP = '" & SAP & "') And ([User] = '" & gsUsuarioPC & "')").Tables(0)
                        If FixEKPO.Rows.Count > 0 Then
                            Dim iSAP As New SAPConection.c_SAP(SAP)
                            Dim c As New SAPCOM.SAPConnector
                            Dim u As Object = c.GetConnectionData(SAP, gsUsuarioPC, "LAT")

                            iSAP.UserName = gsUsuarioPC
                            iSAP.Password = u.password
                            iSAP.OpenConnection(SAPConfig)
                            If iSAP.Conected Then
                                Dim Data = iSAP.Get_GS_Value_From_EKPO(SAP, FixEKPO, My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
                                iSAP.CloseConnection()

                                If Not Data Is Nothing Then
                                    For Each r As DataRow In Data.Rows
                                        Try
                                            cn.ExecuteInServer("Update SC_POReport_Requis Set [GS Value] = '" & r("Value") & "' Where (([Doc Number] = '" & r("PO") & "') And ([Item Number] = '" & r("Item") & "') And (SAP = '" & SAP & "'))")
                                        Catch ex As Exception
                                            ' do nothing
                                        End Try
                                    Next
                                End If

                            End If
                        End If

                        Dim EBAN_LOW_MMR As String
                        Dim EBAN_HIGH_MMR As String
                        Dim EBAN_LOW_CAT As String
                        Dim EBAN_HIGH_CAT As String

                        EBAN_LOW_CAT = (From C In R2.Data.AsEnumerable() _
                               Where C.Item("Requisition") > "1100000000" _
                               Select C.Item("Requisition")).Min

                        EBAN_HIGH_CAT = (From C In R2.Data.AsEnumerable() _
                                     Where C.Item("Requisition") > "1100000000" _
                                     Select C.Item("Requisition")).Max

                        EBAN_LOW_MMR = (From C In R2.Data.AsEnumerable() _
                               Where (C.Item("Requisition") > "0" And C.Item("Requisition") < "1100000000") _
                               Select C.Item("Requisition")).Min

                        EBAN_HIGH_MMR = (From C In R2.Data.AsEnumerable() _
                                     Where C.Item("Requisition") < "1100000000" _
                                     Select C.Item("Requisition")).Max

                        Dim EBAN_CAT As New SAPCOM.EBAN_Report(SAP, gsUsuarioPC, "LAT")
                        Dim EBAN_MMR As New SAPCOM.EBAN_Report(SAP, gsUsuarioPC, "LAT")

                        'cn.ExportDataTableToXL(R2.Data)

                        If EBAN_CAT.IsReady AndAlso EBAN_MMR.IsReady Then
                            If (Not EBAN_LOW_CAT Is Nothing) AndAlso (Not EBAN_HIGH_CAT Is Nothing) Then
                                EBAN_CAT.DocumentFrom = EBAN_LOW_CAT
                                EBAN_CAT.DocumentTo = EBAN_HIGH_CAT
                                EBAN_CAT.AddCustomField("FRGDT", "Release Date")
                                EBAN_CAT.Execute()
                                If Not EBAN_CAT.Success Then
                                    HasError = True
                                    ErrorList.Add(SAP & ": Getting EBAN-CAT Report")
                                Else
                                    Dim CATS As New DataColumn
                                    Dim CATU As New DataColumn
                                    CATU.ColumnName = "Usuario"
                                    CATU.Caption = "Usuario"
                                    CATU.DefaultValue = gsUsuarioPC
                                    CATS.DefaultValue = SAP
                                    CATS.ColumnName = "SAPBox"
                                    CATS.Caption = "SAPBox"
                                    EBAN_CAT.Data.Columns.Add(CATU)
                                    EBAN_CAT.Data.Columns.Add(CATS)
                                    EBAN_CAT.ColumnToDateStr("Release Date")
                                    EBAN_CAT.ColumnToDoubleStr("PO Item")
                                    cn.AppendTableToSqlServer("SC_EBAN", EBAN_CAT.Data)
                                End If
                            End If

                            If (Not EBAN_LOW_MMR Is Nothing) AndAlso (Not EBAN_HIGH_MMR Is Nothing) Then
                                EBAN_MMR.DocumentFrom = EBAN_LOW_MMR.PadLeft(10, "0")
                                EBAN_MMR.DocumentTo = EBAN_HIGH_MMR.PadLeft(10, "0")
                                EBAN_MMR.AddCustomField("FRGDT", "Release Date")
                                EBAN_MMR.Execute()
                                If Not EBAN_MMR.Success Then
                                    HasError = True
                                    ErrorList.Add(SAP & ": Getting EBAN-MMR Report")
                                Else
                                    Dim MMRS As New DataColumn
                                    Dim MMRU As New DataColumn
                                    MMRU.ColumnName = "Usuario"
                                    MMRU.Caption = "Usuario"
                                    MMRU.DefaultValue = gsUsuarioPC
                                    MMRS.DefaultValue = SAP
                                    MMRS.ColumnName = "SAPBox"
                                    MMRS.Caption = "SAPBox"
                                    EBAN_MMR.Data.Columns.Add(MMRU)
                                    EBAN_MMR.Data.Columns.Add(MMRS)
                                    EBAN_MMR.ColumnToDateStr("Release Date")
                                    EBAN_MMR.ColumnToDoubleStr("PO Item")

                                    Dim foundRows() As Data.DataRow
                                    foundRows = (EBAN_MMR.Data.Select("[Req Date] = ''"))
                                    'cn.ExportDataTableToXL(EBAN_MMR.Data)

                                    If foundRows.Count > 0 Then
                                        For Each r As DataRow In EBAN_MMR.Data.Rows
                                            If r("Req Date") = "" Then
                                                r("Req Date") = r("Release Date")
                                            End If
                                        Next
                                    End If

                                    cn.AppendTableToSqlServer("SC_EBAN", EBAN_MMR.Data)
                                End If
                            End If
                        Else
                            HasError = True
                            ErrorList.Add(SAP & ": Getting EBAN-CAT or EBAN=MMR report ready")
                        End If
                    End If
                End If
            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            End If
        End If

    End Sub
    Private Sub BGL6P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGL6P.DoWork
        Dim SAP As String = "L6P"
        Dim dtPlants As New OAConnection.SQLInstruction(eSQLInstruction.Select)
        Dim Rep As New SAPCOM.POs_Report(SAP, gsUsuarioPC, AppId)
        Dim POs As New DataTable
        Dim cn As New OAConnection.Connection
        dtPlants.Tabla = "SC_Plant"
        dtPlants.AgregarParametro(New SQLInstrucParam("Plant_Code", "", False))
        dtPlants.Execute()
        For Each row As DataRow In dtPlants.Data.Rows
            Rep.IncludePlant(row("Plant_Code"))
        Next
        Rep.IncludeDocsDatedFromTo(StartDate, EndDate)
        Rep.RepairsLevel = IncludeRepairs
        Rep.Execute()
        If Not Rep.Success Then
            HasError = True
            ErrorList.Add(SAP & ": Getting PO Report")
        Else
            If Rep.ErrMessage = Nothing Then
                POs = Rep.Data
                If POs.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0219")
                End If
                If POs.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
                    POs.Columns.Remove("EKPO-ZWERT")
                End If
                If POs.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0218")
                End If
                If POs.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0220")
                End If
                If POs.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
                    POs.Columns.Remove("EKKO-MEMORYTYPE")
                End If
                POs = Rep.Data
                POs.Columns.Add("Status")
                POs.Columns.Add("Confirm")
                Dim ETN As New DataColumn
                Dim ESB As New DataColumn
                ETN.ColumnName = "Usuario"
                ETN.Caption = "Usuario"
                ETN.DefaultValue = gsUsuarioPC
                ESB.DefaultValue = SAP
                ESB.ColumnName = "SAPBox"
                ESB.Caption = "SAPBox"
                POs.Columns.Add(ETN)
                POs.Columns.Add(ESB)
                cn.AppendTableToSqlServer("SC_POReport", POs)
                Dim MinPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Min

                Dim MaxPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Max

                Dim R2 As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
                R2.DocumentFrom = MinPO
                R2.DocumentTo = MaxPO
                R2.AddCustomField("BANFN", "Requisition")
                R2.AddCustomField("BNFPO", "Req Item")
                R2.AddCustomField("IDNLF", "IDNLF")
                R2.AddCustomField("BRTWR", "GS VALUE")
                R2.Execute()
                If Not R2.Success Then
                    HasError = True
                    ErrorList.Add(SAP & ": Getting CAT-EKPO Report")
                Else
                    R2.ColumnToDoubleStr("Requisition")
                    R2.ColumnToDoubleStr("Req Item")

                    If R2.Data.Columns.IndexOf("Short Text") <> -1 Then
                        R2.Data.Columns.Remove("Short Text")
                    End If
                    If R2.Data.Columns.IndexOf("Material") <> -1 Then
                        R2.Data.Columns.Remove("Material")
                    End If
                    If R2.Data.Columns.IndexOf("Plant") <> -1 Then
                        R2.Data.Columns.Remove("Plant")
                    End If
                    If R2.Data.Columns.IndexOf("Inforecord") <> -1 Then
                        R2.Data.Columns.Remove("Inforecord")
                    End If
                    If R2.Data.Columns.IndexOf("Quantity") <> -1 Then
                        R2.Data.Columns.Remove("Quantity")
                    End If
                    If R2.Data.Columns.IndexOf("UOM") <> -1 Then
                        R2.Data.Columns.Remove("UOM")
                    End If
                    If R2.Data.Columns.IndexOf("Price") <> -1 Then
                        R2.Data.Columns.Remove("Price")
                    End If
                    If R2.Data.Columns.IndexOf("Tax Code") <> -1 Then
                        R2.Data.Columns.Remove("Tax Code")
                    End If
                    If R2.Data.Columns.IndexOf("PDT") <> -1 Then
                        R2.Data.Columns.Remove("PDT")
                    End If
                    If R2.Data.Columns.IndexOf("Mat Group") <> -1 Then
                        R2.Data.Columns.Remove("Mat Group")
                    End If
                    If R2.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                        R2.Data.Columns.Remove("Tracking Fld")
                    End If
                    If R2.Data.Columns.IndexOf("Price Unit") <> -1 Then
                        R2.Data.Columns.Remove("Price Unit")
                    End If
                    Dim ETN2 As New DataColumn
                    Dim ESB2 As New DataColumn
                    ETN2.ColumnName = "Usuario"
                    ETN2.Caption = "Usuario"
                    ETN2.DefaultValue = gsUsuarioPC
                    ESB2.DefaultValue = SAP
                    ESB2.ColumnName = "SAPBox"
                    ESB2.Caption = "SAPBox"
                    R2.Data.Columns.Add(ETN2)
                    R2.Data.Columns.Add(ESB2)
                    cn.AppendTableToSqlServer("SC_POReport_Requis", R2.Data)
                    Dim MinCT = (From C In POs.AsEnumerable() _
                            Where C.Item("Doc Number") < "450000000" _
                            Select C.Item("Doc Number")).Min

                    Dim MaxCT = (From C In POs.AsEnumerable() _
                                 Where C.Item("Doc Number") < "450000000" _
                                 Select C.Item("Doc Number")).Max

                    Param_NAST_L6P.Max_PO = MaxPO
                    Param_NAST_L6P.Min_PO = MinPO
                    Param_NAST_L6P.Max_Cat = MaxCT
                    Param_NAST_L6P.Min_Cat = MinCT

                    Dim R3 As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
                    R3.DocumentFrom = MinCT
                    R3.DocumentTo = MaxCT
                    R3.AddCustomField("BANFN", "Requisition")
                    R3.AddCustomField("BNFPO", "Req Item")
                    R3.AddCustomField("IDNLF", "IDNLF")
                    R3.AddCustomField("BRTWR", "GS VALUE")
                    R3.Execute()

                    If Not R3.Success Then
                        HasError = True
                        ErrorList.Add(SAP & ": Getting EKPO-MMR Report")
                    Else
                        R3.ColumnToDoubleStr("Requisition")
                        R3.ColumnToDoubleStr("Req Item")
                        If R3.Data.Columns.IndexOf("Short Text") <> -1 Then
                            R3.Data.Columns.Remove("Short Text")
                        End If
                        If R3.Data.Columns.IndexOf("Material") <> -1 Then
                            R3.Data.Columns.Remove("Material")
                        End If
                        If R3.Data.Columns.IndexOf("Plant") <> -1 Then
                            R3.Data.Columns.Remove("Plant")
                        End If
                        If R3.Data.Columns.IndexOf("Inforecord") <> -1 Then
                            R3.Data.Columns.Remove("Inforecord")
                        End If
                        If R3.Data.Columns.IndexOf("Quantity") <> -1 Then
                            R3.Data.Columns.Remove("Quantity")
                        End If
                        If R3.Data.Columns.IndexOf("UOM") <> -1 Then
                            R3.Data.Columns.Remove("UOM")
                        End If
                        If R3.Data.Columns.IndexOf("Price") <> -1 Then
                            R3.Data.Columns.Remove("Price")
                        End If
                        If R3.Data.Columns.IndexOf("Tax Code") <> -1 Then
                            R3.Data.Columns.Remove("Tax Code")
                        End If
                        If R3.Data.Columns.IndexOf("PDT") <> -1 Then
                            R3.Data.Columns.Remove("PDT")
                        End If
                        If R3.Data.Columns.IndexOf("Mat Group") <> -1 Then
                            R3.Data.Columns.Remove("Mat Group")
                        End If
                        If R3.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                            R3.Data.Columns.Remove("Tracking Fld")
                        End If
                        If R3.Data.Columns.IndexOf("Price Unit") <> -1 Then
                            R3.Data.Columns.Remove("Price Unit")
                        End If
                        Dim ETN3 As New DataColumn
                        Dim ESB3 As New DataColumn
                        ETN3.ColumnName = "Usuario"
                        ETN3.Caption = "Usuario"
                        ETN3.DefaultValue = gsUsuarioPC
                        ESB3.DefaultValue = SAP
                        ESB3.ColumnName = "SAPBox"
                        ESB3.Caption = "SAPBox"
                        R3.Data.Columns.Add(ETN3)
                        R3.Data.Columns.Add(ESB3)
                        cn.AppendTableToSqlServer("SC_POReport_Requis", R3.Data)

                        Dim FixEKPO As DataTable
                        FixEKPO = cn.RunSentence("Select [Doc Number] From SC_POReport_Requis Where ([GS Value] Like '%*%') And (SAP = '" & SAP & "') And ([User] = '" & gsUsuarioPC & "')").Tables(0)
                        If FixEKPO.Rows.Count > 0 Then
                            Dim iSAP As New SAPConection.c_SAP(SAP)
                            Dim c As New SAPCOM.SAPConnector
                            Dim u As Object = c.GetConnectionData(SAP, gsUsuarioPC, "LAT")

                            iSAP.UserName = gsUsuarioPC
                            iSAP.Password = u.password
                            iSAP.OpenConnection(SAPConfig)
                            If iSAP.Conected Then
                                cn.Put_DataTable_In_ClipBoard(FixEKPO)
                                Dim Data = iSAP.Get_GS_Value_From_EKPO(SAP, FixEKPO, My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
                                iSAP.CloseConnection()

                                If Not Data Is Nothing Then
                                    For Each r As DataRow In Data.Rows
                                        Try
                                            cn.ExecuteInServer("Update SC_POReport_Requis Set [GS Value] = '" & r("Value") & "' Where (([Doc Number] = '" & r("PO") & "') And ([Item Number] = '" & r("Item") & "') And (SAP = '" & SAP & "'))")
                                        Catch ex As Exception
                                            ' do nothing
                                        End Try
                                    Next
                                End If

                            End If
                        End If


                        Dim EBAN_LOW_MMR As String
                        Dim EBAN_HIGH_MMR As String
                        Dim EBAN_LOW_CAT As String
                        Dim EBAN_HIGH_CAT As String

                        EBAN_LOW_CAT = (From C In R2.Data.AsEnumerable() _
                               Where C.Item("Requisition") > "1100000000" _
                               Select C.Item("Requisition")).Min

                        EBAN_HIGH_CAT = (From C In R2.Data.AsEnumerable() _
                                     Where C.Item("Requisition") > "1100000000" _
                                     Select C.Item("Requisition")).Max

                        EBAN_LOW_MMR = (From C In R2.Data.AsEnumerable() _
                               Where (C.Item("Requisition") > "0" And C.Item("Requisition") < "1100000000") _
                               Select C.Item("Requisition")).Min

                        EBAN_HIGH_MMR = (From C In R2.Data.AsEnumerable() _
                                     Where C.Item("Requisition") < "1100000000" _
                                     Select C.Item("Requisition")).Max

                        Dim EBAN_CAT As New SAPCOM.EBAN_Report(SAP, gsUsuarioPC, "LAT")
                        Dim EBAN_MMR As New SAPCOM.EBAN_Report(SAP, gsUsuarioPC, "LAT")

                        If EBAN_CAT.IsReady AndAlso EBAN_MMR.IsReady Then
                            If (Not EBAN_LOW_CAT Is Nothing) AndAlso (Not EBAN_HIGH_CAT Is Nothing) Then
                                EBAN_CAT.DocumentFrom = EBAN_LOW_CAT
                                EBAN_CAT.DocumentTo = EBAN_HIGH_CAT
                                EBAN_CAT.AddCustomField("FRGDT", "Release Date")
                                EBAN_CAT.Execute()
                                If Not EBAN_CAT.Success Then
                                    HasError = True
                                    ErrorList.Add(SAP & ": Getting EBAN-CAT Report")
                                Else
                                    Dim CATS As New DataColumn
                                    Dim CATU As New DataColumn
                                    CATU.ColumnName = "Usuario"
                                    CATU.Caption = "Usuario"
                                    CATU.DefaultValue = gsUsuarioPC
                                    CATS.DefaultValue = SAP
                                    CATS.ColumnName = "SAPBox"
                                    CATS.Caption = "SAPBox"
                                    EBAN_CAT.Data.Columns.Add(CATU)
                                    EBAN_CAT.Data.Columns.Add(CATS)
                                    EBAN_CAT.ColumnToDateStr("Release Date")
                                    EBAN_CAT.ColumnToDoubleStr("PO Item")
                                    cn.AppendTableToSqlServer("SC_EBAN", EBAN_CAT.Data)
                                End If
                            End If

                            If (Not EBAN_LOW_MMR Is Nothing) AndAlso (Not EBAN_HIGH_MMR Is Nothing) Then
                                EBAN_MMR.DocumentFrom = EBAN_LOW_MMR.PadLeft(10, "0")
                                EBAN_MMR.DocumentTo = EBAN_HIGH_MMR.PadLeft(10, "0")
                                EBAN_MMR.AddCustomField("FRGDT", "Release Date")
                                EBAN_MMR.Execute()
                                If Not EBAN_MMR.Success Then
                                    HasError = True
                                    ErrorList.Add(SAP & ": Getting EBAN-MMR Report")
                                Else
                                    Dim MMRS As New DataColumn
                                    Dim MMRU As New DataColumn
                                    MMRU.ColumnName = "Usuario"
                                    MMRU.Caption = "Usuario"
                                    MMRU.DefaultValue = gsUsuarioPC
                                    MMRS.DefaultValue = SAP
                                    MMRS.ColumnName = "SAPBox"
                                    MMRS.Caption = "SAPBox"
                                    EBAN_MMR.Data.Columns.Add(MMRU)
                                    EBAN_MMR.Data.Columns.Add(MMRS)
                                    EBAN_MMR.ColumnToDateStr("Release Date")
                                    EBAN_MMR.ColumnToDoubleStr("PO Item")
                                    cn.AppendTableToSqlServer("SC_EBAN", EBAN_MMR.Data)
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            End If
        End If

    End Sub
    Private Sub BGG4P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGG4P.DoWork
        Dim SAP As String = "G4P"
        Dim dtPlants As New OAConnection.SQLInstruction(eSQLInstruction.Select)
        Dim Rep As New SAPCOM.POs_Report(SAP, gsUsuarioPC, AppId)
        Dim POs As New DataTable
        Dim cn As New OAConnection.Connection
        dtPlants.Tabla = "SC_Plant"
        dtPlants.AgregarParametro(New SQLInstrucParam("Plant_Code", "", False))
        dtPlants.Execute()
        For Each row As DataRow In dtPlants.Data.Rows
            Rep.IncludePlant(row("Plant_Code"))
        Next
        Rep.IncludeDocsDatedFromTo(StartDate, EndDate)
        Rep.RepairsLevel = IncludeRepairs
        Rep.Execute()
        If Not Rep.Success Then
            HasError = True
            ErrorList.Add(SAP & ": Getting PO Report")
        Else
            If Rep.ErrMessage = Nothing Then
                POs = Rep.Data
                If POs.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0219")
                End If
                If POs.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
                    POs.Columns.Remove("EKPO-ZWERT")
                End If
                If POs.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0218")
                End If
                If POs.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0220")
                End If
                If POs.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
                    POs.Columns.Remove("EKKO-MEMORYTYPE")
                End If
                POs = Rep.Data
                POs.Columns.Add("Status")
                POs.Columns.Add("Confirm")
                Dim ETN As New DataColumn
                Dim ESB As New DataColumn
                ETN.ColumnName = "Usuario"
                ETN.Caption = "Usuario"
                ETN.DefaultValue = gsUsuarioPC
                ESB.DefaultValue = SAP
                ESB.ColumnName = "SAPBox"
                ESB.Caption = "SAPBox"
                POs.Columns.Add(ETN)
                POs.Columns.Add(ESB)
                cn.AppendTableToSqlServer("SC_POReport", POs)
                Dim MinPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Min

                Dim MaxPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Max

                Dim R2 As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
                R2.DocumentFrom = MinPO
                R2.DocumentTo = MaxPO
                R2.AddCustomField("BANFN", "Requisition")
                R2.AddCustomField("BNFPO", "Req Item")
                R2.AddCustomField("IDNLF", "IDNLF")
                R2.AddCustomField("BRTWR", "GS VALUE")
                R2.Execute()
                If Not R2.Success Then
                    HasError = True
                    ErrorList.Add(SAP & ": Getting CAT-EKPO Report")
                Else
                    R2.ColumnToDoubleStr("Requisition")
                    R2.ColumnToDoubleStr("Req Item")

                    If R2.Data.Columns.IndexOf("Short Text") <> -1 Then
                        R2.Data.Columns.Remove("Short Text")
                    End If
                    If R2.Data.Columns.IndexOf("Material") <> -1 Then
                        R2.Data.Columns.Remove("Material")
                    End If
                    If R2.Data.Columns.IndexOf("Plant") <> -1 Then
                        R2.Data.Columns.Remove("Plant")
                    End If
                    If R2.Data.Columns.IndexOf("Inforecord") <> -1 Then
                        R2.Data.Columns.Remove("Inforecord")
                    End If
                    If R2.Data.Columns.IndexOf("Quantity") <> -1 Then
                        R2.Data.Columns.Remove("Quantity")
                    End If
                    If R2.Data.Columns.IndexOf("UOM") <> -1 Then
                        R2.Data.Columns.Remove("UOM")
                    End If
                    If R2.Data.Columns.IndexOf("Price") <> -1 Then
                        R2.Data.Columns.Remove("Price")
                    End If
                    If R2.Data.Columns.IndexOf("Tax Code") <> -1 Then
                        R2.Data.Columns.Remove("Tax Code")
                    End If
                    If R2.Data.Columns.IndexOf("PDT") <> -1 Then
                        R2.Data.Columns.Remove("PDT")
                    End If
                    If R2.Data.Columns.IndexOf("Mat Group") <> -1 Then
                        R2.Data.Columns.Remove("Mat Group")
                    End If
                    If R2.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                        R2.Data.Columns.Remove("Tracking Fld")
                    End If
                    If R2.Data.Columns.IndexOf("Price Unit") <> -1 Then
                        R2.Data.Columns.Remove("Price Unit")
                    End If
                    Dim ETN2 As New DataColumn
                    Dim ESB2 As New DataColumn
                    ETN2.ColumnName = "Usuario"
                    ETN2.Caption = "Usuario"
                    ETN2.DefaultValue = gsUsuarioPC
                    ESB2.DefaultValue = SAP
                    ESB2.ColumnName = "SAPBox"
                    ESB2.Caption = "SAPBox"
                    R2.Data.Columns.Add(ETN2)
                    R2.Data.Columns.Add(ESB2)
                    cn.AppendTableToSqlServer("SC_POReport_Requis", R2.Data)
                    Dim MinCT = (From C In POs.AsEnumerable() _
                            Where C.Item("Doc Number") < "450000000" _
                            Select C.Item("Doc Number")).Min

                    Dim MaxCT = (From C In POs.AsEnumerable() _
                                 Where C.Item("Doc Number") < "450000000" _
                                 Select C.Item("Doc Number")).Max

                    Param_NAST_G4P.Max_PO = MaxPO
                    Param_NAST_G4P.Min_PO = MinPO
                    Param_NAST_G4P.Max_Cat = MaxCT
                    Param_NAST_G4P.Min_Cat = MinCT

                    Dim R3 As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
                    R3.DocumentFrom = MinCT
                    R3.DocumentTo = MaxCT
                    R3.AddCustomField("BANFN", "Requisition")
                    R3.AddCustomField("BNFPO", "Req Item")
                    R3.AddCustomField("IDNLF", "IDNLF")
                    R3.AddCustomField("BRTWR", "GS VALUE")
                    R3.Execute()

                    If Not R3.Success Then
                        HasError = True
                        ErrorList.Add(SAP & ": Getting EKPO-MMR Report")
                    Else
                        R3.ColumnToDoubleStr("Requisition")
                        R3.ColumnToDoubleStr("Req Item")
                        If R3.Data.Columns.IndexOf("Short Text") <> -1 Then
                            R3.Data.Columns.Remove("Short Text")
                        End If
                        If R3.Data.Columns.IndexOf("Material") <> -1 Then
                            R3.Data.Columns.Remove("Material")
                        End If
                        If R3.Data.Columns.IndexOf("Plant") <> -1 Then
                            R3.Data.Columns.Remove("Plant")
                        End If
                        If R3.Data.Columns.IndexOf("Inforecord") <> -1 Then
                            R3.Data.Columns.Remove("Inforecord")
                        End If
                        If R3.Data.Columns.IndexOf("Quantity") <> -1 Then
                            R3.Data.Columns.Remove("Quantity")
                        End If
                        If R3.Data.Columns.IndexOf("UOM") <> -1 Then
                            R3.Data.Columns.Remove("UOM")
                        End If
                        If R3.Data.Columns.IndexOf("Price") <> -1 Then
                            R3.Data.Columns.Remove("Price")
                        End If
                        If R3.Data.Columns.IndexOf("Tax Code") <> -1 Then
                            R3.Data.Columns.Remove("Tax Code")
                        End If
                        If R3.Data.Columns.IndexOf("PDT") <> -1 Then
                            R3.Data.Columns.Remove("PDT")
                        End If
                        If R3.Data.Columns.IndexOf("Mat Group") <> -1 Then
                            R3.Data.Columns.Remove("Mat Group")
                        End If
                        If R3.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                            R3.Data.Columns.Remove("Tracking Fld")
                        End If
                        If R3.Data.Columns.IndexOf("Price Unit") <> -1 Then
                            R3.Data.Columns.Remove("Price Unit")
                        End If
                        Dim ETN3 As New DataColumn
                        Dim ESB3 As New DataColumn
                        ETN3.ColumnName = "Usuario"
                        ETN3.Caption = "Usuario"
                        ETN3.DefaultValue = gsUsuarioPC
                        ESB3.DefaultValue = SAP
                        ESB3.ColumnName = "SAPBox"
                        ESB3.Caption = "SAPBox"
                        R3.Data.Columns.Add(ETN3)
                        R3.Data.Columns.Add(ESB3)
                        cn.AppendTableToSqlServer("SC_POReport_Requis", R3.Data)

                        Dim FixEKPO As DataTable
                        FixEKPO = cn.RunSentence("Select [Doc Number] From SC_POReport_Requis Where ([GS Value] Like '%*%') And (SAP = '" & SAP & "') And ([User] = '" & gsUsuarioPC & "')").Tables(0)
                        If FixEKPO.Rows.Count > 0 Then
                            Dim iSAP As New SAPConection.c_SAP(SAP)
                            Dim c As New SAPCOM.SAPConnector
                            Dim u As Object = c.GetConnectionData(SAP, gsUsuarioPC, "LAT")

                            iSAP.UserName = gsUsuarioPC
                            iSAP.Password = u.password
                            iSAP.OpenConnection(SAPConfig)
                            If iSAP.Conected Then
                                cn.Put_DataTable_In_ClipBoard(FixEKPO)
                                Dim Data = iSAP.Get_GS_Value_From_EKPO(SAP, FixEKPO, My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
                                iSAP.CloseConnection()

                                If Not Data Is Nothing Then
                                    For Each r As DataRow In Data.Rows
                                        Try
                                            cn.ExecuteInServer("Update SC_POReport_Requis Set [GS Value] = '" & r("Value") & "' Where (([Doc Number] = '" & r("PO") & "') And ([Item Number] = '" & r("Item") & "') And (SAP = '" & SAP & "'))")
                                        Catch ex As Exception
                                            ' do nothing
                                        End Try
                                    Next
                                End If

                            End If
                        End If

                        Dim EBAN_LOW_MMR As String
                        Dim EBAN_HIGH_MMR As String
                        Dim EBAN_LOW_CAT As String
                        Dim EBAN_HIGH_CAT As String

                        EBAN_LOW_CAT = (From C In R2.Data.AsEnumerable() _
                               Where C.Item("Requisition") > "1100000000" _
                               Select C.Item("Requisition")).Min

                        EBAN_HIGH_CAT = (From C In R2.Data.AsEnumerable() _
                                     Where C.Item("Requisition") > "1100000000" _
                                     Select C.Item("Requisition")).Max

                        EBAN_LOW_MMR = (From C In R2.Data.AsEnumerable() _
                               Where (C.Item("Requisition") > "0" And C.Item("Requisition") < "1100000000") _
                               Select C.Item("Requisition")).Min

                        EBAN_HIGH_MMR = (From C In R2.Data.AsEnumerable() _
                                     Where C.Item("Requisition") < "1100000000" _
                                     Select C.Item("Requisition")).Max

                        Dim EBAN_CAT As New SAPCOM.EBAN_Report(SAP, gsUsuarioPC, "LAT")
                        Dim EBAN_MMR As New SAPCOM.EBAN_Report(SAP, gsUsuarioPC, "LAT")

                        If EBAN_CAT.IsReady AndAlso EBAN_MMR.IsReady Then
                            If (Not EBAN_LOW_CAT Is Nothing) AndAlso (Not EBAN_HIGH_CAT Is Nothing) Then
                                EBAN_CAT.DocumentFrom = EBAN_LOW_CAT
                                EBAN_CAT.DocumentTo = EBAN_HIGH_CAT
                                EBAN_CAT.AddCustomField("FRGDT", "Release Date")
                                EBAN_CAT.Execute()
                                If Not EBAN_CAT.Success Then
                                    HasError = True
                                    ErrorList.Add(SAP & ": Getting EBAN-CAT Report")
                                Else
                                    Dim CATS As New DataColumn
                                    Dim CATU As New DataColumn
                                    CATU.ColumnName = "Usuario"
                                    CATU.Caption = "Usuario"
                                    CATU.DefaultValue = gsUsuarioPC
                                    CATS.DefaultValue = SAP
                                    CATS.ColumnName = "SAPBox"
                                    CATS.Caption = "SAPBox"
                                    EBAN_CAT.Data.Columns.Add(CATU)
                                    EBAN_CAT.Data.Columns.Add(CATS)
                                    EBAN_CAT.ColumnToDateStr("Release Date")
                                    EBAN_CAT.ColumnToDoubleStr("PO Item")
                                    cn.AppendTableToSqlServer("SC_EBAN", EBAN_CAT.Data)
                                End If
                            End If

                            If (Not EBAN_LOW_MMR Is Nothing) AndAlso (Not EBAN_HIGH_MMR Is Nothing) Then
                                EBAN_MMR.DocumentFrom = EBAN_LOW_MMR.PadLeft(10, "0")
                                EBAN_MMR.DocumentTo = EBAN_HIGH_MMR.PadLeft(10, "0")
                                EBAN_MMR.AddCustomField("FRGDT", "Release Date")
                                EBAN_MMR.Execute()
                                If Not EBAN_MMR.Success Then
                                    HasError = True
                                    ErrorList.Add(SAP & ": Getting EBAN-MMR Report")
                                Else
                                    Dim MMRS As New DataColumn
                                    Dim MMRU As New DataColumn
                                    MMRU.ColumnName = "Usuario"
                                    MMRU.Caption = "Usuario"
                                    MMRU.DefaultValue = gsUsuarioPC
                                    MMRS.DefaultValue = SAP
                                    MMRS.ColumnName = "SAPBox"
                                    MMRS.Caption = "SAPBox"
                                    EBAN_MMR.Data.Columns.Add(MMRU)
                                    EBAN_MMR.Data.Columns.Add(MMRS)
                                    EBAN_MMR.ColumnToDateStr("Release Date")
                                    EBAN_MMR.ColumnToDoubleStr("PO Item")
                                    cn.AppendTableToSqlServer("SC_EBAN", EBAN_MMR.Data)
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            End If
        End If

    End Sub
    Private Sub BGGBP_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGGBP.DoWork
        Dim SAP As String = "GBP"
        Dim dtPlants As New OAConnection.SQLInstruction(eSQLInstruction.Select)
        Dim Rep As New SAPCOM.POs_Report(SAP, gsUsuarioPC, AppId)
        Dim POs As New DataTable
        Dim cn As New OAConnection.Connection
        dtPlants.Tabla = "SC_Plant"
        dtPlants.AgregarParametro(New SQLInstrucParam("Plant_Code", "", False))
        dtPlants.Execute()
        For Each row As DataRow In dtPlants.Data.Rows
            Rep.IncludePlant(row("Plant_Code"))
        Next
        Rep.IncludeDocsDatedFromTo(StartDate, EndDate)
        Rep.RepairsLevel = IncludeRepairs
        Rep.Execute()
        If Not Rep.Success Then
            HasError = True
            ErrorList.Add(SAP & ": Getting PO Report")
        Else
            If Rep.ErrMessage = Nothing Then
                POs = Rep.Data
                If POs.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0219")
                End If
                If POs.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
                    POs.Columns.Remove("EKPO-ZWERT")
                End If
                If POs.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0218")
                End If
                If POs.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0220")
                End If
                If POs.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
                    POs.Columns.Remove("EKKO-MEMORYTYPE")
                End If
                POs = Rep.Data
                POs.Columns.Add("Status")
                POs.Columns.Add("Confirm")
                Dim ETN As New DataColumn
                Dim ESB As New DataColumn
                ETN.ColumnName = "Usuario"
                ETN.Caption = "Usuario"
                ETN.DefaultValue = gsUsuarioPC
                ESB.DefaultValue = SAP
                ESB.ColumnName = "SAPBox"
                ESB.Caption = "SAPBox"
                POs.Columns.Add(ETN)
                POs.Columns.Add(ESB)
                cn.AppendTableToSqlServer("SC_POReport", POs)
                Dim MinPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Min

                Dim MaxPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Max

                Dim R2 As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
                R2.DocumentFrom = MinPO
                R2.DocumentTo = MaxPO
                R2.AddCustomField("BANFN", "Requisition")
                R2.AddCustomField("BNFPO", "Req Item")
                R2.AddCustomField("IDNLF", "IDNLF")
                R2.AddCustomField("BRTWR", "GS VALUE")
                R2.Execute()
                If Not R2.Success Then
                    HasError = True
                    ErrorList.Add(SAP & ": Getting CAT-EKPO Report")
                Else
                    R2.ColumnToDoubleStr("Requisition")
                    R2.ColumnToDoubleStr("Req Item")

                    If R2.Data.Columns.IndexOf("Short Text") <> -1 Then
                        R2.Data.Columns.Remove("Short Text")
                    End If
                    If R2.Data.Columns.IndexOf("Material") <> -1 Then
                        R2.Data.Columns.Remove("Material")
                    End If
                    If R2.Data.Columns.IndexOf("Plant") <> -1 Then
                        R2.Data.Columns.Remove("Plant")
                    End If
                    If R2.Data.Columns.IndexOf("Inforecord") <> -1 Then
                        R2.Data.Columns.Remove("Inforecord")
                    End If
                    If R2.Data.Columns.IndexOf("Quantity") <> -1 Then
                        R2.Data.Columns.Remove("Quantity")
                    End If
                    If R2.Data.Columns.IndexOf("UOM") <> -1 Then
                        R2.Data.Columns.Remove("UOM")
                    End If
                    If R2.Data.Columns.IndexOf("Price") <> -1 Then
                        R2.Data.Columns.Remove("Price")
                    End If
                    If R2.Data.Columns.IndexOf("Tax Code") <> -1 Then
                        R2.Data.Columns.Remove("Tax Code")
                    End If
                    If R2.Data.Columns.IndexOf("PDT") <> -1 Then
                        R2.Data.Columns.Remove("PDT")
                    End If
                    If R2.Data.Columns.IndexOf("Mat Group") <> -1 Then
                        R2.Data.Columns.Remove("Mat Group")
                    End If
                    If R2.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                        R2.Data.Columns.Remove("Tracking Fld")
                    End If
                    If R2.Data.Columns.IndexOf("Price Unit") <> -1 Then
                        R2.Data.Columns.Remove("Price Unit")
                    End If
                    Dim ETN2 As New DataColumn
                    Dim ESB2 As New DataColumn
                    ETN2.ColumnName = "Usuario"
                    ETN2.Caption = "Usuario"
                    ETN2.DefaultValue = gsUsuarioPC
                    ESB2.DefaultValue = SAP
                    ESB2.ColumnName = "SAPBox"
                    ESB2.Caption = "SAPBox"
                    R2.Data.Columns.Add(ETN2)
                    R2.Data.Columns.Add(ESB2)
                    cn.AppendTableToSqlServer("SC_POReport_Requis", R2.Data)
                    Dim MinCT = (From C In POs.AsEnumerable() _
                            Where C.Item("Doc Number") < "450000000" _
                            Select C.Item("Doc Number")).Min

                    Dim MaxCT = (From C In POs.AsEnumerable() _
                                 Where C.Item("Doc Number") < "450000000" _
                                 Select C.Item("Doc Number")).Max

                    Param_NAST_GBP.Max_PO = MaxPO
                    Param_NAST_GBP.Min_PO = MinPO
                    Param_NAST_GBP.Max_Cat = MaxCT
                    Param_NAST_GBP.Min_Cat = MinCT

                    Dim R3 As New SAPCOM.EKPO_Report(SAP, gsUsuarioPC, "LAT")
                    R3.DocumentFrom = MinCT
                    R3.DocumentTo = MaxCT
                    R3.AddCustomField("BANFN", "Requisition")
                    R3.AddCustomField("BNFPO", "Req Item")
                    R3.AddCustomField("IDNLF", "IDNLF")
                    R3.AddCustomField("BRTWR", "GS VALUE")
                    R3.Execute()

                    If Not R3.Success Then
                        HasError = True
                        ErrorList.Add(SAP & ": Getting EKPO-MMR Report")
                    Else
                        R3.ColumnToDoubleStr("Requisition")
                        R3.ColumnToDoubleStr("Req Item")
                        If R3.Data.Columns.IndexOf("Short Text") <> -1 Then
                            R3.Data.Columns.Remove("Short Text")
                        End If
                        If R3.Data.Columns.IndexOf("Material") <> -1 Then
                            R3.Data.Columns.Remove("Material")
                        End If
                        If R3.Data.Columns.IndexOf("Plant") <> -1 Then
                            R3.Data.Columns.Remove("Plant")
                        End If
                        If R3.Data.Columns.IndexOf("Inforecord") <> -1 Then
                            R3.Data.Columns.Remove("Inforecord")
                        End If
                        If R3.Data.Columns.IndexOf("Quantity") <> -1 Then
                            R3.Data.Columns.Remove("Quantity")
                        End If
                        If R3.Data.Columns.IndexOf("UOM") <> -1 Then
                            R3.Data.Columns.Remove("UOM")
                        End If
                        If R3.Data.Columns.IndexOf("Price") <> -1 Then
                            R3.Data.Columns.Remove("Price")
                        End If
                        If R3.Data.Columns.IndexOf("Tax Code") <> -1 Then
                            R3.Data.Columns.Remove("Tax Code")
                        End If
                        If R3.Data.Columns.IndexOf("PDT") <> -1 Then
                            R3.Data.Columns.Remove("PDT")
                        End If
                        If R3.Data.Columns.IndexOf("Mat Group") <> -1 Then
                            R3.Data.Columns.Remove("Mat Group")
                        End If
                        If R3.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                            R3.Data.Columns.Remove("Tracking Fld")
                        End If
                        If R3.Data.Columns.IndexOf("Price Unit") <> -1 Then
                            R3.Data.Columns.Remove("Price Unit")
                        End If
                        Dim ETN3 As New DataColumn
                        Dim ESB3 As New DataColumn
                        ETN3.ColumnName = "Usuario"
                        ETN3.Caption = "Usuario"
                        ETN3.DefaultValue = gsUsuarioPC
                        ESB3.DefaultValue = SAP
                        ESB3.ColumnName = "SAPBox"
                        ESB3.Caption = "SAPBox"
                        R3.Data.Columns.Add(ETN3)
                        R3.Data.Columns.Add(ESB3)
                        cn.AppendTableToSqlServer("SC_POReport_Requis", R3.Data)


                        Dim FixEKPO As DataTable
                        FixEKPO = cn.RunSentence("Select [Doc Number] From SC_POReport_Requis Where ([GS Value] Like '%*%') And (SAP = '" & SAP & "') And ([User] = '" & gsUsuarioPC & "')").Tables(0)
                        If FixEKPO.Rows.Count > 0 Then
                            Dim iSAP As New SAPConection.c_SAP(SAP)
                            Dim c As New SAPCOM.SAPConnector
                            Dim u As Object = c.GetConnectionData(SAP, gsUsuarioPC, "LAT")

                            iSAP.UserName = gsUsuarioPC
                            iSAP.Password = u.password
                            iSAP.OpenConnection(SAPConfig)
                            If iSAP.Conected Then
                                cn.Put_DataTable_In_ClipBoard(FixEKPO)
                                Dim Data = iSAP.Get_GS_Value_From_EKPO(SAP, FixEKPO, My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
                                iSAP.CloseConnection()

                                If Not Data Is Nothing Then
                                    For Each r As DataRow In Data.Rows
                                        Try
                                            cn.ExecuteInServer("Update SC_POReport_Requis Set [GS Value] = '" & r("Value") & "' Where (([Doc Number] = '" & r("PO") & "') And ([Item Number] = '" & r("Item") & "') And (SAP = '" & SAP & "'))")
                                        Catch ex As Exception
                                            ' do nothing
                                        End Try
                                    Next
                                End If

                            End If
                        End If
                        Dim EBAN_LOW_MMR As String
                        Dim EBAN_HIGH_MMR As String
                        Dim EBAN_LOW_CAT As String
                        Dim EBAN_HIGH_CAT As String

                        EBAN_LOW_CAT = (From C In R2.Data.AsEnumerable() _
                               Where C.Item("Requisition") > "1100000000" _
                               Select C.Item("Requisition")).Min

                        EBAN_HIGH_CAT = (From C In R2.Data.AsEnumerable() _
                                     Where C.Item("Requisition") > "1100000000" _
                                     Select C.Item("Requisition")).Max

                        EBAN_LOW_MMR = (From C In R2.Data.AsEnumerable() _
                                 Where (C.Item("Requisition") > "0" And C.Item("Requisition") < "1100000000") _
                                 Select C.Item("Requisition")).Min

                        EBAN_HIGH_MMR = (From C In R2.Data.AsEnumerable() _
                                     Where C.Item("Requisition") < "1100000000" _
                                     Select C.Item("Requisition")).Max

                        Dim EBAN_CAT As New SAPCOM.EBAN_Report(SAP, gsUsuarioPC, "LAT")
                        Dim EBAN_MMR As New SAPCOM.EBAN_Report(SAP, gsUsuarioPC, "LAT")

                        If EBAN_CAT.IsReady AndAlso EBAN_MMR.IsReady Then
                            If (Not EBAN_LOW_CAT Is Nothing) AndAlso (Not EBAN_HIGH_CAT Is Nothing) Then
                                EBAN_CAT.DocumentFrom = EBAN_LOW_CAT
                                EBAN_CAT.DocumentTo = EBAN_HIGH_CAT
                                EBAN_CAT.AddCustomField("FRGDT", "Release Date")
                                EBAN_CAT.Execute()
                                If Not EBAN_CAT.Success Then
                                    HasError = True
                                    ErrorList.Add(SAP & ": Getting EBAN-CAT Report")
                                Else
                                    Dim CATS As New DataColumn
                                    Dim CATU As New DataColumn
                                    CATU.ColumnName = "Usuario"
                                    CATU.Caption = "Usuario"
                                    CATU.DefaultValue = gsUsuarioPC
                                    CATS.DefaultValue = SAP
                                    CATS.ColumnName = "SAPBox"
                                    CATS.Caption = "SAPBox"
                                    EBAN_CAT.Data.Columns.Add(CATU)
                                    EBAN_CAT.Data.Columns.Add(CATS)
                                    EBAN_CAT.ColumnToDateStr("Release Date")
                                    EBAN_CAT.ColumnToDoubleStr("PO Item")
                                    cn.AppendTableToSqlServer("SC_EBAN", EBAN_CAT.Data)
                                End If
                            End If

                            If (Not EBAN_LOW_MMR Is Nothing) AndAlso (Not EBAN_HIGH_MMR Is Nothing) Then
                                EBAN_MMR.DocumentFrom = EBAN_LOW_MMR.PadLeft(10, "0")
                                EBAN_MMR.DocumentTo = EBAN_HIGH_MMR.PadLeft(10, "0")
                                EBAN_MMR.AddCustomField("FRGDT", "Release Date")
                                EBAN_MMR.Execute()
                                If Not EBAN_MMR.Success Then
                                    HasError = True
                                    ErrorList.Add(SAP & ": Getting EBAN-MMR Report")
                                Else
                                    Dim MMRS As New DataColumn
                                    Dim MMRU As New DataColumn
                                    MMRU.ColumnName = "Usuario"
                                    MMRU.Caption = "Usuario"
                                    MMRU.DefaultValue = gsUsuarioPC
                                    MMRS.DefaultValue = SAP
                                    MMRS.ColumnName = "SAPBox"
                                    MMRS.Caption = "SAPBox"
                                    EBAN_MMR.Data.Columns.Add(MMRU)
                                    EBAN_MMR.Data.Columns.Add(MMRS)
                                    EBAN_MMR.ColumnToDateStr("Release Date")
                                    EBAN_MMR.ColumnToDoubleStr("PO Item")
                                    cn.AppendTableToSqlServer("SC_EBAN", EBAN_MMR.Data)
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            End If
        End If

    End Sub

    Private Sub BGL7P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGL7P.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgL7P.Image = I

        If fTra Then
            I = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\working.gif")
            imgL7P_NAST.Image = I
            BGL7P_NAST.RunWorkerAsync()
        End If
        CheckPOWorkers()
    End Sub

    Private Sub BGL6P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGL6P.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgL6P.Image = I

        If fTra Then
            I = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\working.gif")
            imgL6P_Nast.Image = I
            BGL6P_NAST.RunWorkerAsync()
        End If

        CheckPOWorkers()
    End Sub
    Private Sub BGG4P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGG4P.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgG4P.Image = I

        If fTra Then
            I = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\working.gif")
            imgG4P_NAST.Image = I
            BGG4P_NAST.RunWorkerAsync()
        End If

        CheckPOWorkers()
    End Sub
    Private Sub BGGBP_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGGBP.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgGBP.Image = I

        If fTra Then
            I = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\working.gif")
            imgGBP_NAST.Image = I
            BGGBP_NAST.RunWorkerAsync()
        End If

        CheckPOWorkers()
    End Sub

    Private Sub CheckPOWorkers()
        Dim cn As New OAConnection.Connection
        cn.ExecuteInServer("DELETE FROM dbo.SC_POReport WHERE (Usuario = '" & gsUsuarioPC & "') AND ([Doc Type] <> 'EC') AND (SAPBox <> 'L7P') AND (NOT EXISTS (SELECT Name, Tnumber, Status FROM dbo.[PSS People] WHERE (dbo.SC_POReport.[Created By] = Tnumber)))")
        cn.ExecuteInServer("DELETE FROM dbo.SC_POReport WHERE (Usuario = '" & gsUsuarioPC & "') AND ([Doc Type] = 'NB') AND (SAPBox = 'L7P') AND (NOT EXISTS (SELECT Name, Tnumber, Status FROM dbo.[PSS People] WHERE (dbo.SC_POReport.[Created By] = Tnumber)))")
        'cn.ExecuteInServer("DELETE FROM SC_EBAN WHERE (TNumber = '" & gsUsuarioPC & "') AND (NOT EXISTS (SELECT * FROM SC_POReport_Requis AS B WHERE (SC_EBAN.[Purch Doc] = [Doc Number]) AND (dbo.SC_EBAN.[PO Item] = [Item Number])))")
        If Not BGL7P.IsBusy AndAlso Not BGL6P.IsBusy AndAlso Not BGG4P.IsBusy AndAlso Not BGGBP.IsBusy AndAlso Not bgExchange.IsBusy AndAlso Not BGL7P_NAST.IsBusy AndAlso Not BGL6P_NAST.IsBusy AndAlso Not BGG4P_NAST.IsBusy AndAlso Not BGGBP_NAST.IsBusy AndAlso Not BGEKET.IsBusy AndAlso Not BGEKKO.IsBusy AndAlso Not BGOTD.IsBusy Then
            Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\working.gif")
            imgVendors.Image = I
            bgVendors.RunWorkerAsync()
        End If
    End Sub

    Private Sub bgExchange_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgExchange.DoWork
        Dim T As New DataTable
        Dim cn As New OAConnection.Connection
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\working.gif")

        imgCurrency.Image = I
        T = cn.RunSentence("Select Curid From SC_CurExchange").Tables(0)

        If T.Rows.Count > 0 Then

            For Each row As DataRow In T.Rows
                Dim Ex As New SAPCOM.SAPExchgRate("G4P", gsUsuarioPC, AppId)

                Ex.FromCurrency = row("curid")
                Ex.ToCurrency = "USD"
                Ex.Execute()

                If Ex.Success Then
                    cn.ExecuteInServer("Update SC_CurExchange Set Value = " & IIf(Ex.Rate = 0, 1, Ex.Rate) & " Where CurID = '" & row("CurID") & "'")
                End If
            Next
        End If
    End Sub
    Private Sub bgExchange_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgExchange.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgCurrency.Image = I
        CheckPOWorkers()
    End Sub
    Private Sub bgVendors_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgVendors.DoWork
        'Verifico si hay vendors que no están en la tabla de los vendor del scorecard:
        Dim T As DataTable
        Dim cn As New OAConnection.Connection

        T = cn.RunSentence("SELECT DISTINCT SC_POReport.Vendor FROM SC_Vendors RIGHT OUTER JOIN SC_POReport ON SC_Vendors.Vendor = SC_POReport.Vendor WHERE (SC_Vendors.Vendor IS NULL)").Tables(0)

        If (T.Rows.Count > 0) Then
            Dim Vendor As New SAPCOM.LFA1_Report("G4P", gsUsuarioPC, AppId)

            For Each row As DataRow In T.Rows
                Vendor.IncludeVendor(row("Vendor"))
            Next

            Vendor.Execute()
            If Vendor.Success Then
                cn.AppendTableToSqlServer("SC_Vendors", Vendor.Data)
            End If

        End If

    End Sub

    Public Sub CheckOtherWorkers()
        Dim cn As New OAConnection.Connection

        If Not bgVendors.IsBusy Then
            Panel1.Visible = False

            'Elimino las PO's fuera del scope de PSS
            cn.ExecuteInServer("DELETE FROM SC_POReport WHERE (Usuario = '" & gsUsuarioPC & "') AND ([Doc Type] <> 'EC') AND (SAPBox <> 'L7P') AND (NOT EXISTS (SELECT Name, Tnumber, Status FROM dbo.[PSS People] WHERE (dbo.SC_POReport.[Created By] = Tnumber)))")
            cn.ExecuteInServer("DELETE FROM SC_POReport WHERE (Usuario = '" & gsUsuarioPC & "') AND ([Doc Type] = 'NB') AND (SAPBox = 'L7P') AND (NOT EXISTS (SELECT Name, Tnumber, Status FROM dbo.[PSS People] WHERE (dbo.SC_POReport.[Created By] = Tnumber)))")
            cn.ExecuteInServer("DELETE FROM SC_EBAN WHERE (TNumber = '" & gsUsuarioPC & "') AND (NOT EXISTS (SELECT * FROM SC_POReport_Requis AS B WHERE (SC_EBAN.[Req Number] = [Requisition]) AND (dbo.SC_EBAN.[Req Item] = [Req Item])))")
            cn.ExecuteInServer("DELETE From SC_POReport Where (Vendor = '15145463')")
          
            Rep = cn.RunSentence("Select * From vst_SC_Report Where Usuario = '" & gsUsuarioPC & "'").Tables(0)

            dgReport.DataSource = Rep
            pgrBar.Style = Windows.Forms.ProgressBarStyle.Blocks

            Me.lblStatus.Text = "Total PO Items found: " & Rep.Rows.Count
            tlbHerremientas.Enabled = True
        End If
    End Sub

    Private Sub bgVendors_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgVendors.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgVendors.Image = I

        CheckOtherWorkers()
    End Sub
    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim cn As New OAConnection.Connection
        cn.ExportDataTableToXL(Rep)
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim form As New frm068
        form.Show()

    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim cn As New OAConnection.Connection

        If Not bgExchange.IsBusy AndAlso Not bgVendors.IsBusy Then
            Panel1.Visible = False

            'Rep = cn.RunSentence("Select * From Get_SC_Report('" & gsUsuarioPC & "')").Tables(0)
            Rep = cn.RunSentence("Select * From vst_SC_Report Where Usuario = '" & gsUsuarioPC & "'").Tables(0)

            dgReport.DataSource = Rep
            pgrBar.Style = Windows.Forms.ProgressBarStyle.Blocks

            Me.lblStatus.Text = "Total PO Items found: " & Rep.Rows.Count
            tlbHerremientas.Enabled = True

        End If
    End Sub

    Private Sub BGL7P_NAST_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGL7P_NAST.DoWork
        Dim SAP As String = "L7P"
        Dim NAST As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If NAST.IsReady Then
            NAST.DocumentFrom = Param_NAST_L7P.Min_PO
            NAST.DocumentTo = Param_NAST_L7P.Max_PO
            NAST.Execute()

            If NAST.Success Then
                Dim cn As New OAConnection.Connection
                Dim MMRS As New DataColumn
                Dim MMRU As New DataColumn

                'Columna del Usuario que descarga el reporte
                MMRU.ColumnName = "Usuario"
                MMRU.Caption = "Usuario"
                MMRU.DefaultValue = gsUsuarioPC

                'Columna de la caja
                MMRS.DefaultValue = SAP
                MMRS.ColumnName = "SAPBox"
                MMRS.Caption = "SAPBox"

                NAST.Data.Columns.Add(MMRU)
                NAST.Data.Columns.Add(MMRS)

                cn.AppendTableToSqlServer("SC_NAST", NAST.Data)

            End If
        End If

        Dim NAST_CAT As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If NAST_CAT.IsReady Then
            NAST_CAT.DocumentFrom = Param_NAST_L7P.Min_Cat
            NAST_CAT.DocumentTo = Param_NAST_L7P.Min_Cat
            NAST_CAT.Execute()

            If NAST_CAT.Success Then
                Dim cn As New OAConnection.Connection
                Dim MMRS As New DataColumn
                Dim MMRU As New DataColumn

                'Columna del Usuario que descarga el reporte
                MMRU.ColumnName = "Usuario"
                MMRU.Caption = "Usuario"
                MMRU.DefaultValue = gsUsuarioPC

                'Columna de la caja
                MMRS.DefaultValue = SAP
                MMRS.ColumnName = "SAPBox"
                MMRS.Caption = "SAPBox"

                NAST_CAT.Data.Columns.Add(MMRU)
                NAST_CAT.Data.Columns.Add(MMRS)

                cn.AppendTableToSqlServer("SC_NAST", NAST_CAT.Data)

            End If
        End If




    End Sub
    Private Sub BGL6P_NAST_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGL6P_NAST.DoWork
        Dim SAP As String = "L6P"
        Dim NAST As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If NAST.IsReady Then
            NAST.DocumentFrom = Param_NAST_L6P.Min_PO
            NAST.DocumentTo = Param_NAST_L6P.Max_PO
            NAST.Execute()

            If NAST.Success Then
                Dim cn As New OAConnection.Connection
                Dim MMRS As New DataColumn
                Dim MMRU As New DataColumn

                'Columna del Usuario que descarga el reporte
                MMRU.ColumnName = "Usuario"
                MMRU.Caption = "Usuario"
                MMRU.DefaultValue = gsUsuarioPC

                'Columna de la caja
                MMRS.DefaultValue = SAP
                MMRS.ColumnName = "SAPBox"
                MMRS.Caption = "SAPBox"

                NAST.Data.Columns.Add(MMRU)
                NAST.Data.Columns.Add(MMRS)

                cn.AppendTableToSqlServer("SC_NAST", NAST.Data)

            End If
        End If


        Dim NAST_CAT As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If NAST_CAT.IsReady Then
            NAST_CAT.DocumentFrom = Param_NAST_L6P.Min_Cat
            NAST_CAT.DocumentTo = Param_NAST_L6P.Max_Cat
            NAST_CAT.Execute()

            If NAST_CAT.Success Then
                Dim cn As New OAConnection.Connection
                Dim MMRS As New DataColumn
                Dim MMRU As New DataColumn

                'Columna del Usuario que descarga el reporte
                MMRU.ColumnName = "Usuario"
                MMRU.Caption = "Usuario"
                MMRU.DefaultValue = gsUsuarioPC

                'Columna de la caja
                MMRS.DefaultValue = SAP
                MMRS.ColumnName = "SAPBox"
                MMRS.Caption = "SAPBox"

                NAST_CAT.Data.Columns.Add(MMRU)
                NAST_CAT.Data.Columns.Add(MMRS)

                cn.AppendTableToSqlServer("SC_NAST", NAST_CAT.Data)

            End If
        End If
    End Sub
    Private Sub BGG4P_NAST_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGG4P_NAST.DoWork
        Dim SAP As String = "G4P"
        Dim NAST As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If NAST.IsReady Then
            NAST.DocumentFrom = Param_NAST_G4P.Min_PO
            NAST.DocumentTo = Param_NAST_G4P.Max_PO
            NAST.Execute()

            If NAST.Success Then
                Dim cn As New OAConnection.Connection
                Dim MMRS As New DataColumn
                Dim MMRU As New DataColumn

                'Columna del Usuario que descarga el reporte
                MMRU.ColumnName = "Usuario"
                MMRU.Caption = "Usuario"
                MMRU.DefaultValue = gsUsuarioPC

                'Columna de la caja
                MMRS.DefaultValue = SAP
                MMRS.ColumnName = "SAPBox"
                MMRS.Caption = "SAPBox"

                NAST.Data.Columns.Add(MMRU)
                NAST.Data.Columns.Add(MMRS)

                cn.AppendTableToSqlServer("SC_NAST", NAST.Data)

            End If
        End If


        Dim NAST_CAT As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If NAST_CAT.IsReady Then
            NAST_CAT.DocumentFrom = Param_NAST_G4P.Min_Cat
            NAST_CAT.DocumentTo = Param_NAST_G4P.Max_Cat
            NAST_CAT.Execute()

            If NAST_CAT.Success Then
                Dim cn As New OAConnection.Connection
                Dim MMRS As New DataColumn
                Dim MMRU As New DataColumn

                'Columna del Usuario que descarga el reporte
                MMRU.ColumnName = "Usuario"
                MMRU.Caption = "Usuario"
                MMRU.DefaultValue = gsUsuarioPC

                'Columna de la caja
                MMRS.DefaultValue = SAP
                MMRS.ColumnName = "SAPBox"
                MMRS.Caption = "SAPBox"

                NAST_CAT.Data.Columns.Add(MMRU)
                NAST_CAT.Data.Columns.Add(MMRS)

                cn.AppendTableToSqlServer("SC_NAST", NAST_CAT.Data)

            End If
        End If
    End Sub
    Private Sub BGGBP_NAST_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGGBP_NAST.DoWork
        Dim SAP As String = "GBP"
        Dim NAST As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If NAST.IsReady Then
            NAST.DocumentFrom = Param_NAST_GBP.Min_PO
            NAST.DocumentTo = Param_NAST_GBP.Max_PO
            NAST.Execute()

            If NAST.Success Then
                Dim cn As New OAConnection.Connection
                Dim MMRS As New DataColumn
                Dim MMRU As New DataColumn

                'Columna del Usuario que descarga el reporte
                MMRU.ColumnName = "Usuario"
                MMRU.Caption = "Usuario"
                MMRU.DefaultValue = gsUsuarioPC

                'Columna de la caja
                MMRS.DefaultValue = SAP
                MMRS.ColumnName = "SAPBox"
                MMRS.Caption = "SAPBox"

                NAST.Data.Columns.Add(MMRU)
                NAST.Data.Columns.Add(MMRS)

                cn.AppendTableToSqlServer("SC_NAST", NAST.Data)
            End If
        End If

        Dim NAST_CAT As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If NAST_CAT.IsReady Then
            NAST_CAT.DocumentFrom = Param_NAST_GBP.Min_Cat
            NAST_CAT.DocumentTo = Param_NAST_GBP.Max_Cat
            NAST_CAT.Execute()

            If NAST_CAT.Success Then
                Dim cn As New OAConnection.Connection
                Dim MMRS As New DataColumn
                Dim MMRU As New DataColumn

                'Columna del Usuario que descarga el reporte
                MMRU.ColumnName = "Usuario"
                MMRU.Caption = "Usuario"
                MMRU.DefaultValue = gsUsuarioPC

                'Columna de la caja
                MMRS.DefaultValue = SAP
                MMRS.ColumnName = "SAPBox"
                MMRS.Caption = "SAPBox"

                NAST_CAT.Data.Columns.Add(MMRU)
                NAST_CAT.Data.Columns.Add(MMRS)

                cn.AppendTableToSqlServer("SC_NAST", NAST_CAT.Data)
            End If
        End If
    End Sub

    Private Sub BGL7P_NAST_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGL7P_NAST.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgL7P_NAST.Image = I

        CheckPOWorkers()
    End Sub
    Private Sub BGL6P_NAST_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGL6P_NAST.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgL6P_Nast.Image = I
        CheckPOWorkers()
    End Sub
    Private Sub BGG4P_NAST_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGG4P_NAST.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgG4P_NAST.Image = I
        CheckPOWorkers()
    End Sub
    Private Sub BGGBP_NAST_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGGBP_NAST.RunWorkerCompleted
        Dim I As Drawing.Bitmap = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
        imgGBP_NAST.Image = I
        CheckPOWorkers()
    End Sub

    Private Class P_Nast
        Public Max_PO
        Public Min_PO
        Public Max_Cat
        Public Min_Cat
    End Class
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        'Dim Rep As New frm078
        'Rep.Show()
        Dim V
        Dim cn As New OAConnection.Connection
        Dim Data_Local As New DataTable
        Dim Data_Import As New DataTable

        Dim POs As New DataTable
        Dim Row As DataRow
        Dim R2PO As DataTable
        Dim Print As DataTable
        Dim OTD As DataTable
        Dim AutoMMR As DataTable
        Dim AutoPO As DataTable
        Dim Automation As DataTable
        Dim BI As DataTable
        Dim OIMMR As DataTable
        Dim OIFFT As DataTable

        Data_Local = cn.RunSentence("Select * From vst_SC_Sumary").Tables(0)
        POs = cn.RunSentence("SELECT Plant, [Total POs], [Total Items] FROM Get_SC_Sumary_Qty_POs('" & gsUsuarioPC & "') Where Spend = 'Local'").Tables(0)
        R2PO = cn.RunSentence("Select * From get_SC_Sumary_Rec_To_PO('" & gsUsuarioPC & "') Where Spend = 'Local'").Tables(0)
        Print = cn.RunSentence("Select * From get_SC_Printed('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        OTD = cn.RunSentence("Select * From fn_Get_SC_OTD_Summary('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        AutoMMR = cn.RunSentence("Select * From fn_Get_SC_AUTO_MMR_Sumary('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        AutoPO = cn.RunSentence("Select * From fn_Get_SC_Sumary_AUTO_PO('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        Automation = cn.RunSentence("Select * From fn_Get_SC_Total_Automation('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        BI = cn.RunSentence("Select * From fn_Get_SC_BI_Unblock_Sumary('" & dtpStartDate.Value & "','" & dtpEndDate.Value & "') Where Spend = 'National'").Tables(0)
        OIMMR = cn.RunSentence("Select * From fn_Get_SC_OpenItem_MMR() Where Spend = 'Local'").Tables(0)
        OIFFT = cn.RunSentence("Select * From fn_Get_SC_OpenItem_FFT() Where Spend = 'Local'").Tables(0)

        For Each Row In Data_Local.Rows
            V = ((From C In POs.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total Items")).SingleOrDefault)
            Row("Total PO Items") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In POs.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total POs")).SingleOrDefault)
            Row("Total PO headers") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In R2PO.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("% Req To PO")).SingleOrDefault)
            Row("% Req To PO on time") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In Print.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("% Transmission") = IIf(Not V Is Nothing, ((V / Row("Total PO Headers"))), DBNull.Value)

            V = ((From C In OTD.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("% OTD")).SingleOrDefault)
            Row("% OTD") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In AutoMMR.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("AutoMMR")).SingleOrDefault)
            Row("% of STR items through MMR") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In AutoPO.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("% Auto")).SingleOrDefault)
            Row("% of Self Service items through catalogs/auto PO") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In Automation.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("Overall automation %") = IIf(Not V Is Nothing, ((V / Row("Total PO Items"))), DBNull.Value)

            V = ((From C In BI.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("# of blocked invoices (at report creation date)") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In BI.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("On Time")).SingleOrDefault)
            Row("% of blocked invoices resolved before due date") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In OIMMR.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("MMR orders >1 day overdue") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In OIFFT.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("FFT orders >60 days overdue") = IIf(Not V Is Nothing, V, DBNull.Value)

        Next


        Data_Import = cn.RunSentence("Select * From vst_SC_Sumary").Tables(0)
        POs = cn.RunSentence("SELECT Plant, [Total POs], [Total Items] FROM Get_SC_Sumary_Qty_POs('" & gsUsuarioPC & "') Where Spend <> 'Local'").Tables(0)
        R2PO = cn.RunSentence("Select * From get_SC_Sumary_Rec_To_PO('" & gsUsuarioPC & "') Where Spend <> 'Local'").Tables(0)
        Print = cn.RunSentence("Select * From get_SC_Printed('" & gsUsuario & "') Where Spend <> 'Local'").Tables(0)
        OTD = cn.RunSentence("Select * From fn_Get_SC_OTD_Summary('" & gsUsuario & "') Where Spend <> 'Local'").Tables(0)
        AutoMMR = cn.RunSentence("Select * From fn_Get_SC_AUTO_MMR_Sumary('" & gsUsuario & "') Where Spend <> 'Local'").Tables(0)
        AutoPO = cn.RunSentence("Select * From fn_Get_SC_Sumary_AUTO_PO('" & gsUsuario & "') Where Spend <> 'Local'").Tables(0)
        Automation = cn.RunSentence("Select * From fn_Get_SC_Total_Automation('" & gsUsuario & "') Where Spend <> 'Local'").Tables(0)
        BI = cn.RunSentence("Select * From fn_Get_SC_BI_Unblock_Sumary('" & dtpStartDate.Value & "','" & dtpEndDate.Value & "') Where Spend <> 'National'").Tables(0)
        OIMMR = cn.RunSentence("Select * From fn_Get_SC_OpenItem_MMR() Where Spend <> 'Local'").Tables(0)
        OIFFT = cn.RunSentence("Select * From fn_Get_SC_OpenItem_FFT() Where Spend <> 'Local'").Tables(0)

        For Each Row In Data_Import.Rows
            V = ((From C In POs.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total Items")).SingleOrDefault)
            Row("Total PO Items") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In POs.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total POs")).SingleOrDefault)
            Row("Total PO headers") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In R2PO.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("% Req To PO")).SingleOrDefault)
            Row("% Req To PO on time") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In Print.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("% Transmission") = IIf(Not V Is Nothing, ((V / Row("Total PO Headers"))), DBNull.Value)

            V = ((From C In OTD.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("% OTD")).SingleOrDefault)
            Row("% OTD") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In AutoMMR.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("AutoMMR")).SingleOrDefault)
            Row("% of STR items through MMR") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In AutoPO.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("% Auto")).SingleOrDefault)
            Row("% of Self Service items through catalogs/auto PO") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In Automation.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("Overall automation %") = IIf(Not V Is Nothing, ((V / Row("Total PO Items"))), DBNull.Value)

            V = ((From C In BI.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("# of blocked invoices (at report creation date)") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In BI.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("On Time")).SingleOrDefault)
            Row("% of blocked invoices resolved before due date") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In OIMMR.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("MMR orders >1 day overdue") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In OIFFT.AsEnumerable() Where C.Item("Plant") = Row("Site") Select C.Item("Total")).SingleOrDefault)
            Row("FFT orders >60 days overdue") = IIf(Not V Is Nothing, V, DBNull.Value)

        Next

        Dim SL As New DataColumn
        Dim SI As New DataColumn

        SL.ColumnName = "Spend"
        SL.Caption = "Spend"
        SL.DefaultValue = "Local"


        SI.ColumnName = "Spend"
        SI.Caption = "Spend"
        SI.DefaultValue = "Import"

        Data_Local.Rows.Add()

        Data_Local.Columns.Add(SL)
        Data_Import.Columns.Add(SI)

        Data_Local.Merge(Data_Import)

        cn.GetScorecardSumary(Data_Local)
    End Sub
    Private Sub CheckOTD()

        If Not BGEKET.IsBusy AndAlso Not BGEKKO.IsBusy Then
            Dim I = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\apply.png")
            imgOTD.Image = I
        End If

        CheckPOWorkers()
    End Sub
    Private Sub BGEKET_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGEKET.RunWorkerCompleted
        CheckOTD()
    End Sub
    Private Sub BGEKKO_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGEKKO.DoWork
        Dim EKKO As New SAPCOM.EKKO_Report("L7P", gsUsuarioPC, AppId)
        'Dim I = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\working.gif")

        If EKKO.IsReady Then
            EKKO.DocumentFrom = MinOTDPO
            EKKO.DocumentTo = MaxOTDPO

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
        CheckOTD()
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

                MinOTDPO = (From C In r.Data.AsEnumerable() _
                             Where C.Item("Purch Doc") > "450000000" _
                             Select C.Item("Purch Doc")).Min

                MaxOTDPO = (From C In r.Data.AsEnumerable() _
                             Where C.Item("Purch Doc") > "450000000" _
                             Select C.Item("Purch Doc")).Max

                Dim UEKBE As New DataColumn
                UEKBE.ColumnName = "TNumber"
                UEKBE.Caption = "TNumber"
                UEKBE.DefaultValue = gsUsuarioPC

                TEKBE.Columns.Add(UEKBE)
                cn.AppendTableToSqlServer("SC_EKBE", TEKBE)
                ' cn.RunSentence("Delete From SC_EKBE Where ([Delivery date] = '00000000') or ([Stat Del Date] = '00000000')")
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

            R2.DocumentFrom = MinOTDPO
            R2.DocumentTo = MaxOTDPO
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
                cn.RunSentence("DELETE FROM SC_EKET WHERE ([Delivery date] = '00000000') OR ([Stat Del Date] = '00000000')")

            End If
        End If
    End Sub
    Private Sub BGOI_L7P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim SAP As String = "L7P"
        Dim dtPlants As New OAConnection.SQLInstruction(eSQLInstruction.Select)

        Dim POs As New DataTable
        Dim cn As New OAConnection.Connection
        dtPlants.Tabla = "SC_Plant"
        dtPlants.AgregarParametro(New SQLInstrucParam("Plant_Code", "", False))
        dtPlants.Execute()

        For Each row As DataRow In dtPlants.Data.Rows
            Dim Rep As New SAPCOM.OpenOrders_Report(SAP, gsUsuarioPC, AppId)

            Rep.RepairsLevel = IncludeRepairs
            Rep.Include_GR_IR = True
            Rep.IncludeDelivDates = True
            Rep.IncludePlant(row("Plant_Code"))

            Rep.Execute()
            If Rep.Success Then
                If Rep.ErrMessage = Nothing Then
                    POs = Rep.Data
                    If POs.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
                        POs.Columns.Remove("EKKO-WAERS-0219")
                    End If
                    If POs.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
                        POs.Columns.Remove("EKPO-ZWERT")
                    End If
                    If POs.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
                        POs.Columns.Remove("EKKO-WAERS-0218")
                    End If
                    If POs.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
                        POs.Columns.Remove("EKKO-WAERS-0220")
                    End If
                    If POs.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
                        POs.Columns.Remove("EKKO-MEMORYTYPE")
                    End If

                    Dim TN As New DataColumn
                    Dim SB As New DataColumn

                    TN.ColumnName = "Usuario"
                    TN.Caption = "Usuario"
                    TN.DefaultValue = gsUsuarioPC

                    SB.DefaultValue = SAP
                    SB.ColumnName = "SAPBox"
                    SB.Caption = "SAPBox"

                    POs.Columns.Add(TN)
                    POs.Columns.Add(SB)

                    cn.AppendTableToSqlServer("SC_OpenOrders", POs)
                End If
            End If
        Next

    End Sub
    Private Sub BGOI_L7P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        CheckPOWorkers()
    End Sub
    Private Sub cmdOpenItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenItems.Click
        Dim Data As DataTable
        Dim cn As New OAConnection.Connection

        Data = cn.RunSentence("Select * From vst_SC_OpenItems").Tables(0)
        cn.ExportDataTableToXL(Data)
    End Sub
    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        Dim Data As DataTable
        Dim cn As New OAConnection.Connection

        Data = cn.RunSentence("Select * From SC_Nast Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
        cn.ExportDataTableToXL(Data)
    End Sub
    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        Dim Data As DataTable
        Dim cn As New OAConnection.Connection

        Data = cn.RunSentence("Select * From get_SC_Rec_To_PO('" & gsUsuarioPC & "')").Tables(0)
        cn.ExportDataTableToXL(Data)
    End Sub
End Class



