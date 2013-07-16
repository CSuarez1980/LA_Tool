Imports SAPCOM.SAPTextIDs

Public Class _Test
    Public Sub DownloadZFI2()
        Dim Var As DataTable
        Dim SAP As SAPConection.c_SAP
        Dim sc As New SAPCOM.SAPConnector
        Dim row As DataRow

        Dim cn As New OAConnection.Connection

        Var = cn.RunSentence("Select * From vst_ZFI2_Variantes").Tables(0)
        cn.RunSentence("Delete From BI_TMP_ZFI2")


        For Each row In Var.Rows
            SAP = New SAPConection.c_SAP(row.Item("BoxLongName").ToString)
            Dim ur As Object = sc.GetSAPConnection(Microsoft.VisualBasic.Left(row.Item("SAP").ToString, 3), "BM4691", "LAT")

            SAP.UserName = "BM4691"
            SAP.Password = ur.password

            SAP.OpenConnection(True)

            Dim DT As DataTable = SAP.DownloadZFI2(row.Item("Var_Name").trim, row.Item("Var_User"))
            SAP.CloseConnection()

            If Not DT Is Nothing Then

                If DT.Columns.IndexOf("Column1") <> -1 Then
                    DT.Columns.Remove("Column1")
                End If

                'SAP Box
                Dim C As New DataColumn
                C.ColumnName = "SAP"
                C.Caption = "SAP"
                C.DefaultValue = Microsoft.VisualBasic.Left(row.Item("SAP").ToString, 3)

                'Buyer Comment Column
                Dim BC As New DataColumn
                BC.ColumnName = "Buyer Comment"
                BC.Caption = "Buyer Comment"
                BC.DefaultValue = ""

                'Upload Date Column
                Dim UC As New DataColumn
                UC.ColumnName = "Upload Date"
                UC.Caption = "Upload Date"
                UC.DefaultValue = Now.Date

                'Status ID Column
                Dim SIC As New DataColumn
                SIC.ColumnName = "Status ID"
                SIC.Caption = "Status ID"
                SIC.DefaultValue = 0

                DT.Columns.Add(C)
                DT.Columns.Add(BC)
                DT.Columns.Add(UC)
                DT.Columns.Add(SIC)

                cn.AppendTableToSqlServer("BI_TMP_ZFI2", DT)

            End If
        Next

        'Agrego los records que no estan en el reporte:
        cn.ExecuteInServer("INSERT INTO BI_ZFI2_Report SELECT * FROM vst_ZFI2_NewRecords")

        'Agrego los registros que fueron liberados al histórico
        Dim RR As DataTable
        RR = cn.RunSentence("Select * From vst_ZFI2_Records_Released").Tables(0)

        'Dia en que sale del reporte
        Dim RD As New DataColumn
        RD.ColumnName = "Release Date"
        RD.Caption = "Release Date"
        RD.DefaultValue = Now.Date

        RR.Columns.Add(RD)

        cn.AppendTableToSqlServer("BI_ZFI2_History", RR)

        'Elimino las que fueron eliminadas
        Dim r As DataRow
        For Each r In RR.Rows
            cn.ExecuteInServer("Delete From BI_ZFI2_Report Where Record = '" & r("Record") & "' And SAP = '" & r("SAP") & "'")
        Next

    End Sub

    Private Sub _Test_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gsUsuarioPC = "BM4691"
        AppId = "LAT"

        Dim r As New SAPCOM.EKBE_Report("L7P", "BM4691", "LAT")

        If r.IsReady Then
            r.Include_Document("4500842525")
            r.Include_Movement("101")
            r.Execute()
        End If

        Dim R2 As New SAPCOM.EKET_Report("L7P", "BM4691", "LAT")

        If R2.IsReady Then
            R2.IncludeDocument("4500842525")
            R2.Execute()
        End If

       

        'Dim cn As New OAConnection.Connection
        Dim d As New DataTable

        'd = cn.Read_SAP_PO_AI_File("c:\PDFs\PO_AI_IC Test_L7P.xls", "L7P")

        GetCurr()

        Dim SAP As New SAPConection.c_SAP("L7A TS Acceptance")
        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData("L7A", gsUsuarioPC, "LAT")


        SAP.UserName = "BM4691"
        SAP.Password = u.password

        SAP.OpenConnection(True)

        Dim P As New SAPConection.POPrinting(SAP.GUI)

        P.IncludeDocument("4500820937")
        P.IncludeDocument("3061244584")
        P.IncludeDocument("3061413623")
        P.IncludeDocument("3061588321")

        P.AllowPrintAle = True
        P.SAPBox = "L7A"
        P.DirectoryPath = "C:\PDFs"
        P.Excecute()

        SAP.CloseConnection()

        MsgBox("Done")



        '####################################################################################################
        'ZFI2
        DownloadZFI2()
        '####################################################################################################

        'Dim Oa As New SAPCOM.OAChanges("L7P", "BM4691", AppId, "4600002000")
        'Oa.NewHeaderText("K00", "DB-Test")
        'Oa.CommitChanges()

        Dim fInicio As Date
        Dim fFinal As Date

        fInicio = #6/1/2011#
        fFinal = #6/30/2011#

        ' Download_Rec2PO(fInicio, fFinal)
        Download_OTD(fInicio, fFinal)
        Download_PO_Reports(fInicio, fFinal)
        Download_SP()

        'Download_SP()
        ' Unir()
        ' Download_PO_Reports()

        MsgBox("Done")

        'Dim lfa1 As New SAPCOM.LFA1_Report("L7P", "BM4691")

        'lfa1.IncludeVendor("15067131")
        'lfa1.Execute()

        'Dim A As New SAPCOM.OTD_Report("L7P", "BM4691")
        'Dim y As New SAPCOM.

        'A.MonthFrom = ("03/2011")
        'A.MaterialTo = ("03/2011")
        'A.IncludePlant("0045")

        'A.Execute()

        'Obtener Item text de requicision:
        'Dim r As New SAPCOM.PRInfo("L7P", "BM4691")
        'r.ReqNumber = "1173055466"
        'Dim s As String = r.ItemText("10", "B02")

        'Dim r As New SAPCOM.OTD_Report("L7P", "BM4691", AppId)

        'Dim x As New OAConnection.Connection
        'Dim T As New DataTable

        'T = x.RunSentence("Select * From [Users] Where TNumber = 'BE7969'").Tables(0)

        ' Dim j As New SAPCOM.EKBE_Report("L7P", "BM4691", AppId)

        ''Dim z As New SAPCOM.OAChanges("L7A", "BM4691")

        ''z.OANumber = "4600002000"
        ''z.AddMaterial("32634756", "10000", "32", "7", "9653", "0001", "")
        ''z.CommitChanges()
        ''If z.Success Then

        'MsgBox(P.Data.Rows.Count)

        ''End If
        ''Dim EKKO As New SAPCOM.EKKO_Report("L7P", "BM4691")

        'EKKO.CreatedFrom = #2/1/2011#
        'EKKO.CreatedTo = #2/24/2011#

        'EKKO.Execute()


        'MsgBox(EKKO.Data.Rows.Count)

        'Dim MARC As New SAPCOM.MARC_Report("L7P", "BM4691")

        'MARC.IncludePlant("9475")
        'MARC.IncludePlant("9476")
        'MARC.IncludePlant("4563")
        'MARC.IncludePlant("0278")
        'MARC.IncludePlant("0051")
        'MARC.IncludePlant("9653")
        'MARC.IncludePlant("4950")
        'MARC.IncludePlant("4004")
        'MARC.IncludePlant("4047")
        'MARC.IncludePlant("7761")
        'MARC.IncludePlant("0301")
        'MARC.IncludePlant("0045")
        'MARC.IncludePlant("4841")
        'MARC.IncludePlant("0300")
        'MARC.IncludePlant("2921")
        'MARC.IncludePlant("2930")
        'MARC.IncludePlant("9245")
        'MARC.IncludePlant("9266")
        'MARC.IncludePlant("3665")
        'MARC.IncludePlant("8727")
        'MARC.IncludePlant("9367")
        'MARC.IncludePlant("9265")


        'MARC.Execute()

        'Dim c As New OAConnection.Connection

        'c.ExecuteInServer("Delete From MasterData")

        'c.AppendTableToSqlServer("MasterData", MARC.Data)

        ''Dim EKPO As New SAPCOM.EKPO_Report("L7P", "BM4691")

        'Dim x As New SAPCOM.OpenReqs_Report("L7P", "BM4691")

        'EKPO.AddCustomField("BANFN")
        'EKPO.AddCustomField("BNFPO")

        'EKPO.Execute()
        'If EKPO.Success Then
        '    If EKPO.ErrMessage = Nothing Then
        '        Dim EBAN As New SAPCOM.EBAN_Report("L7P", "BM4691")
        '        Dim r As DataRow

        '        EBAN.AddCustomField("FRGDT")
        '        For Each r In EKPO.Data.Rows
        '            EBAN.IncludeDocument(r("BANFN"))
        '        Next

        '        EBAN.Execute()

        '        If EBAN.Success Then
        '            If EBAN.ErrMessage = Nothing Then


        '                For Each r In EBAN.Data.Rows
        '                    r("FRGDT") = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(r("FRGDT"), 6), 2) & "/" & Microsoft.VisualBasic.Right(r("FRGDT"), 2) & "/" & Microsoft.VisualBasic.Left(r("FRGDT"), 4)
        '                Next


        '                Dim cn As New OAConnection.Connection
        '                cn.ExecuteInServer("Delete From WEB_EKPO")
        '                cn.ExecuteInServer("Delete From WEB_EBAN")

        '                cn.AppendTableToSqlServer("WEB_EKPO", EKPO.Data)
        '                cn.AppendTableToSqlServer("WEB_EBAN", EBAN.Data)



        '            End If
        '        End If


        '    End If
        'End If


        'MsgBox("Done.")
    End Sub

    Public Function Download_Rec2PO(ByVal fInicio As Date, ByVal fFinal As Date)
        Dim PlantCodes As New DataTable
        Dim cn As New OAConnection.Connection
        Dim Row As DataRow
        Dim EKKO As New SAPCOM.EKKO_Report("L7P", "BM4691", AppId)

        PlantCodes = cn.RunSentence("Select Code From Plant Where UseInDownload = 1").Tables(0)

        cn.RunSentence("Delete From WEB_EKKO")
        cn.RunSentence("Delete From WEB_EKPO")
        cn.RunSentence("Delete From WEB_EBAN")

        EKKO.CreatedFrom = fInicio
        EKKO.CreatedTo = fFinal
        EKKO.DeletionIndicator = False
        EKKO.DocumentFrom = ("3000000000")
        EKKO.DocumentTo = ("4599999999")

        EKKO.Execute()

        If EKKO.Success Then
            If EKKO.ErrMessage = Nothing Then
                cn.AppendTableToSqlServer("WEB_EKKO", EKKO.Data)

                Dim vst_MinMax_PO As New DataTable

                vst_MinMax_PO = cn.RunSentence("Select MinCat, MaxCat, MinGica, MaxGica From vst_WEB_MinMax_PO").Tables(0)

                Dim EKPO As New SAPCOM.EKPO_Report("L7P", "BM4691", AppId)

                'Obtengo las PO de catalogos
                EKPO.DocumentFrom = vst_MinMax_PO.Rows(0)("MinCat")
                EKPO.DocumentTo = vst_MinMax_PO.Rows(0)("MaxCat")

                For Each Row In PlantCodes.Rows
                    EKPO.IncludePlant(Row("Code"))
                Next

                EKPO.AddCustomField("BANFN")
                EKPO.AddCustomField("BNFPO")
                EKPO.DeletionIndicator = False

                EKPO.Execute()

                If EKPO.Success Then
                    If EKPO.ErrMessage = Nothing Then
                        cn.AppendTableToSqlServer("WEB_EKPO", EKPO.Data)
                    End If
                End If

                'Obtengo las PO Gicadas
                EKPO = New SAPCOM.EKPO_Report("L7P", "BM4691", True)
                EKPO.DocumentFrom = vst_MinMax_PO.Rows(0)("MinGica")
                EKPO.DocumentTo = vst_MinMax_PO.Rows(0)("MaxGica")

                For Each Row In PlantCodes.Rows
                    EKPO.IncludePlant(Row("Code"))
                Next

                EKPO.AddCustomField("BANFN")
                EKPO.AddCustomField("BNFPO")
                EKPO.DeletionIndicator = False

                EKPO.Execute()

                If EKPO.Success Then
                    If EKPO.ErrMessage = Nothing Then
                        cn.AppendTableToSqlServer("WEB_EKPO", EKPO.Data)
                    End If
                End If

            End If

            Dim Requis As New DataTable
            Requis = cn.RunSentence("SELECT * FROM vst_WEB_Rec2PO_Requis").Tables(0)

            Dim EBAN As New SAPCOM.EBAN_Report("L7P", "BM4691", True)


            EBAN.DocumentFrom = Requis.Rows(0)("MMR_Low").ToString.PadLeft(10, "0")
            EBAN.DocumentTo = Requis.Rows(0)("MMR_Hi").ToString.PadLeft(10, "0")



            For Each Row In PlantCodes.Rows
                EBAN.IncludePlant(Row("Code"))
            Next


            EBAN.DeletionIndicator = False
            EBAN.AddCustomField("FRGDT")
            EBAN.Execute()

            If EBAN.Success Then
                If EBAN.ErrMessage = Nothing Then
                    For Each Row In EBAN.Data.Rows
                        Row("FRGDT") = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(Row("FRGDT"), 6), 2) & "/" & Microsoft.VisualBasic.Right(Row("FRGDT"), 2) & "/" & Microsoft.VisualBasic.Left(Row("FRGDT"), 4)
                    Next

                    cn.AppendTableToSqlServer("WEB_EBAN", EBAN.Data)
                End If
            End If


            '+---------------------------------------------------------------------------------------------+
            Dim EBAN2 As New SAPCOM.EBAN_Report("L7P", "BM4691", True)

            EBAN2.DocumentFrom = Requis.Rows(0)("Cat_Low").ToString.PadLeft(10, "0")
            EBAN2.DocumentTo = (Requis.Rows(0)("Cat_Hi")).ToString.PadLeft(10, "0")

            EBAN2.DeletionIndicator = False
            EBAN2.AddCustomField("FRGDT")
            EBAN2.Execute()

            If EBAN2.Success Then
                If EBAN2.ErrMessage = Nothing Then
                    For Each Row In EBAN2.Data.Rows
                        Row("FRGDT") = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(Row("FRGDT"), 6), 2) & "/" & Microsoft.VisualBasic.Right(Row("FRGDT"), 2) & "/" & Microsoft.VisualBasic.Left(Row("FRGDT"), 4)
                    Next

                    cn.AppendTableToSqlServer("WEB_EBAN", EBAN2.Data)
                End If
            End If

        End If

    End Function

    Public Sub Download_OTD(ByVal fInicio As Date, ByVal fFinal As Date)
        Dim PlantCodes As New DataTable
        Dim cn As New OAConnection.Connection
        Dim Row As DataRow
        Dim Ekbe As New SAPCOM.EKBE_Report("L7P", "BM4691", AppId)


        PlantCodes = cn.RunSentence("Select Code From Plant Where UseInDownload = 1").Tables(0)

        Ekbe.AddCustomField("ERNAM", "UserGR")
        Ekbe.MaterialFrom = "30000000"
        Ekbe.MaterialTo = "39999999"
        Ekbe.PostingDateFrom = fInicio
        Ekbe.PostingDateTo = fFinal
        Ekbe.Include_Movement("101")
        Ekbe.Include_Movement("105")


        For Each Row In PlantCodes.Rows
            Ekbe.IncludePlant(Row.Item("Code"))
        Next


        Ekbe.Execute()

        If Ekbe.Success Then
            Dim MaxMin As DataTable

            cn.ExecuteInServer("Delete From WEB_OTD_EKBE")
            cn.AppendTableToSqlServer("WEB_OTD_EKBE", Ekbe.Data)

            MaxMin = cn.RunSentence("SELECT MAX([Purch Doc]) AS Max, MIN([Purch Doc]) AS Min FROM dbo.WEB_OTD_EKBE").Tables(0)
            If MaxMin.Rows.Count > 0 Then
                Dim POs As New SAPCOM.POs_Report("L7P", "BM4691", AppId)

                POs.IncludeDocumentFromTo(MaxMin.Rows(0)("Min"), MaxMin.Rows(0)("Max"))
                POs.ExcludeMaterial("")
                POs.Execute()

                If POs.Success Then
                    Dim EKET As New SAPCOM.EKET_Report("L7P", "BM4691", AppId)
                    cn.ExecuteInServer("Delete From WEB_OTD_POReport")
                    cn.AppendTableToSqlServer("WEB_OTD_POReport", POs.Data)

                    EKET.DocumentFrom = MaxMin.Rows(0)("Min")
                    EKET.DocumentTo = MaxMin.Rows(0)("Max")

                    EKET.Execute()

                    If EKET.Success Then
                        cn.ExecuteInServer("Delete From WEB_OTD_EKET")
                        cn.AppendTableToSqlServer("WEB_OTD_EKET", EKET.Data)
                    End If

                End If


            End If
        End If
    End Sub

    Public Sub Download_PO_Reports(ByVal fInicio As Date, ByVal fFinal As Date)
        Dim PlantCodes As New DataTable
        Dim POrg As New DataTable
        Dim cn As New OAConnection.Connection
        Dim Row As DataRow
        Dim PO As New SAPCOM.POs_Report("L7P", "BM4691", AppId)
        Dim cx As New SAPCOM.ConnectionData

        PlantCodes = cn.RunSentence("Select Code From Plant Where UseInDownload = 1").Tables(0)
        POrg = cn.RunSentence("Select PGroup From WEB_Automation_PGroup").Tables(0)

        If PlantCodes.Rows.Count > 0 AndAlso POrg.Rows.Count > 0 Then
            For Each Row In PlantCodes.Rows
                PO.IncludePlant(Row("Code"))
            Next

            For Each Row In POrg.Rows
                PO.IncludePurchGroup(Row("PGroup"))
            Next

            PO.IncludeDocsDatedFromTo(fInicio, fFinal)
            PO.Execute()

            If PO.Success Then
                cn.RunSentence("Delete From WEB_PO_Report")
                cn.AppendTableToSqlServer("WEB_PO_Report", PO.Data)
                cn.RunSentence("Delete FROM WEB_PO_Report WHERE ([Del Ind] <> '')")
            End If
        End If
    End Sub

    Public Sub Unir()
        Dim Data As New DataTable
        Dim cx As New OAConnection.Connection

        Data = cx.RunSentence("SELECT PurchOrder, Item, SAPBox, '' AS Comment FROM dbo.Comentarios_PO GROUP BY PurchOrder, Item, SAPBox ORDER BY PurchOrder").Tables(0)

        If Data.Rows.Count > 0 Then
            cx.AppendTableToSqlServer("All_Comments", Data)

            Dim row As DataRow

            For Each row In Data.Rows
                Dim DC As DataTable

                DC = cx.RunSentence("Select Comentario From Comentarios_PO Where PurchOrder = " & row("PurchOrder") & " And Item = " & row("Item") & " And SAPBox = '" & row("SAPBox") & "'").Tables(0)
                If DC.Rows.Count > 0 Then
                    Dim Dr As DataRow
                    Dim co As String = ""

                    For Each Dr In DC.Rows
                        co = co & Dr("Comentario") & " // "
                    Next

                    cx.ExecuteInServer("Update All_Comments set Comment = '" & co & "' Where ([Purch Doc] = " & row("PurchOrder") & " And Item = " & row("Item") & " And SAPBox = '" & row("SAPBox") & "')")
                End If

            Next

        End If


        MsgBox("Done!")


    End Sub

    Public Sub Download_SP()

        'Dim SAP As New SAPCOM.SAPConnector
        'Dim Conn As Object = SAP.GetSAPConnection("L7P", "BM4691", "LA")

        Dim cn As New OAConnection.Connection
        Dim DT As DataTable

        DT = cn.RunSentence("Select Cat_Low, Cat_Hi, PO_Low, PO_Hi From vst_WEB_Hi_Low_PO").Tables(0)

        If DT.Rows.Count > 0 Then
            Dim Nast_1 As New SAPCOM.NAST_Report("L7P", "BM4691", "LAT")


            Nast_1.DocumentFrom = DT.Rows(0)("Cat_Low")
            Nast_1.DocumentTo = DT.Rows(0)("Cat_Hi")

            Nast_1.IncludeProcStatus(SAPCOM.ProcStatus.Successfully_Processed)
            Nast_1.IncludeTransMedium("1")
            Nast_1.IncludeTransMedium("2")
            Nast_1.IncludeTransMedium("A")
            Nast_1.IncludeTransMedium("6")

            Nast_1.Execute()


            If Nast_1.Success Then
                cn.AppendTableToSqlServer("WEB_NAST", Nast_1.Data)
            End If

            '*****************************************************************************************
            Dim Nast_2 As New SAPCOM.NAST_Report("L7P", "BM4691", "LAT")

            Nast_2.DocumentFrom = DT.Rows(0)("PO_Low")
            Nast_2.DocumentTo = DT.Rows(0)("PO_Hi")

            Nast_2.IncludeProcStatus(SAPCOM.ProcStatus.Successfully_Processed)
            Nast_2.IncludeTransMedium("1")
            Nast_2.IncludeTransMedium("2")
            Nast_2.IncludeTransMedium("A")
            Nast_2.IncludeTransMedium("6")

            Nast_2.Execute()

            If Nast_2.Success Then
                cn.AppendTableToSqlServer("WEB_NAST", Nast_2.Data)
            End If


        End If


    End Sub

    Private Sub GetCurr()

        Dim P As New SAPCOM.Plants_Report("L7P", "BM4691", "LAT")
        P.IncludePlant("0045")
        P.Execute()

        Dim PG As New SAPCOM.Buyers_Report("L7P", "BM4691", "LAT")
        PG.IncludePGrp("0QB")
        PG.Execute()

        Dim c As New SAPCOM.SAPExchgRate("G4P", "BM4691", "LAT")
        c.FromCurrency = "BRL"
        c.ToCurrency = "USD"
        c.Execute()


    End Sub



    Public Shared Sub Send_eMail(ByVal Recipient As String, ByVal CopyTo As String, ByVal Subject As String, ByVal Body As String, ByVal Attachments() As String, ByVal OnBehalfOf As String)

        Dim MyolApp

        Dim myNameSpace

        Dim objMail

        ' Dim SMI

        Dim A As String

        Try

            MyolApp = CreateObject("Outlook.Application")

            myNameSpace = MyolApp.GetNamespace("MAPI")

            objMail = MyolApp.CreateItem(0)

            With objMail

                .Subject = Subject

                .HTMLBody = Body

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

            'SMI.SAVE()

            'SMI.Send()

            'objMail.save()

            objMail.Send()

        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Critical, "Outlook Error")

        End Try

    End Sub

End Class