Public Class frm084
    Public Data As New DataTable
    Public Status As String = ""

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        txtStatus.Text = "Status: Reading trigger file..."
        ofdFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        ofdFile.Filter = "Distribution files (*.txt)|*.txt"
        If ofdFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Data = Read_Distribution_File(ofdFile.FileName)

            If Not Data Is Nothing Then
                dtgData.DataSource = Data
            End If
        End If
        txtStatus.Text = "Status: Done."
    End Sub
    Public Function Read_Distribution_File(ByVal Path As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0
        'Dim ID As Integer = 0

        S = FileReader.ReadLine
        'D.Columns.Add(New DataColumn("ID", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("SAPBox", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Plant", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("P Org", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("P Grp", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Mat Grp", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("SPS", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("PTB", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("TYPE", System.Type.GetType("System.String")))


        'Para mostrar a Carolina Matamoros como owner de los que no se les asigna
        DR = D.NewRow
        'DR("id") = 0
        DR("SAPBox") = "L7A"
        DR("Plant") = ""
        DR("P Org") = ""
        DR("P Grp") = ""
        DR("Mat Grp") = ""
        DR("SPS") = "BI5226"
        D.Rows.Add(DR)


        Do While Not S Is Nothing
            S = FileReader.ReadLine
            If S Is Nothing Or S = "" Then
                ExitLoop = True
            Else
                W = Split(S, vbTab)

                DR = D.NewRow
                I = 1
                'ID += 1

                'DR("ID") = ID
                DR("SAPBox") = W(0)
                DR("Plant") = W(1)
                DR("P Org") = W(2)
                DR("P Grp") = W(3)
                DR("Mat Grp") = W(4)
                DR("SPS") = Microsoft.VisualBasic.Left(W(5), 6)
                DR("PTB") = Microsoft.VisualBasic.Left(W(6), 6)
                DR("Type") = W(7)


                'For Each S In W
                '    If DR(I).columnname = "" Then

                '    End If
                '    DR(I) = S
                '    I += 1
                'Next
                'DR("ID") = ID
                D.Rows.Add(DR)
            End If
        Loop

        D.AcceptChanges()
        FileReader.Close()
        Read_Distribution_File = D
    End Function
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Data.Rows.Count > 0 Then
            bgwSave.RunWorkerAsync()
        Else
            MsgBox("No data found.", MsgBoxStyle.Information)
        End If

    End Sub
    Private Sub cmdTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTemplate.Click
        MsgBox("This template MOST be saved as a tab delimited file.", MsgBoxStyle.Information)

        Dim Temp As New DataTable
        Dim cn As New OAConnection.Connection
        Temp = cn.RunSentence("Select * From LA_Indirect_Distribution Where SPS = 'XXX'").Tables(0)
        cn.ExportDataTableToXL(Temp)
    End Sub
    Private Sub bgwSave_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwSave.DoWork
        Dim cn As New OAConnection.Connection
        bgwSave.ReportProgress(0, "Cleaning Table")
        cn.ExecuteInServer("Delete From LA_Indirect_Distribution")
        bgwSave.ReportProgress(50, "Updating distribution")
        cn.AppendTableToSqlServer("LA_Indirect_Distribution", Data)
        MsgBox("Done.")
    End Sub
    Private Sub cmdApplyChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApplyChanges.Click
        If MsgBox("Do you really want to apply this new distribution to current items?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Apply Changes") = MsgBoxResult.Yes Then
            bgwApplyChanges.RunWorkerAsync()
        End If
    End Sub

    Private Sub bgwApplyChanges_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwApplyChanges.DoWork
        Dim cn As New OAConnection.Connection
        Dim P As Double = 0

        '*****************************************************************************
        ' Trigger:
        '*****************************************************************************
        cn.RunSentence("Delete From LA_TMP_Trigger_Distribution")
        Data = cn.RunSentence("Select  Box, material, POrg, PGrp, Plant, MATGRP from Trigger_LA").Tables(0)
        For Each r As DataRow In Data.Rows
            P += 1
            Try
                bgwApplyChanges.ReportProgress(((P * 100) / Data.Rows.Count), "Looking owner for: [Material: " & r("Material") & ", Plant: " & r("Plant") & "]")
                'BG.ReportProgress(cont)
                Dim lO As New oOwner

                Dim o As New OAConnection.DMS_User()
                o.Material_Group = r("MATGRP")
                o.Plant = r("Plant")
                o.Purch_Grp = r("PGrp")
                o.Purch_Org = r("POrg")
                o.SAPBox = r("Box")
                o.Execute()


                lO = GetOwner(r("Box"), r("Plant"), r("PGrp"), r("POrg"))
                If Not lO Is Nothing Then
                    cn.ExecuteInServer("Update LA_TMP_Trigger_Distribution Set SPS = '" & lO.SPS & "', Owner = '" & lO.Owner & "' Where ((SAPBox = '" & r("Box") & "') And ([Material] = '" & r("Material") & "') And ([Plant] = '" & r("Plant") & "'))")
                Else
                    cn.ExecuteInServer("Update LA_TMP_Trigger_Distribution Set SPS = 'BB0898', Owner = 'BB0898' Where ((SAPBox = '" & r("Box") & "') And ([Material] = '" & r("Material") & "') And ([Plant] = '" & r("Plant") & "'))")
                End If
            Catch ex As Exception
                'Do nothing
            End Try
        Next

        '*****************************************************************************
        ' Requis:
        '*****************************************************************************
        P = 0
        cn.RunSentence("Delete From LA_TMP_Open_Req_Distribution")
        Data = cn.RunSentence("Select * From vst_LA_Check_Req_Distribution").Tables(0)
        If Data.Rows.Count > 0 Then
            For Each r As DataRow In Data.Rows
                P += 1
                bgwApplyChanges.ReportProgress(((P * 100) / Data.Rows.Count), "Looking owner for: [PR: " & r("Req Number") & ", SAPBox: " & r("SAPBox") & "]")
                Try
                    Dim lO As New oOwner
                    lO = GetOwner(r("SAPBox"), r("Plant"), r("Purch Grp"), r("Purch Org"))
                    If Not lO Is Nothing Then
                        Dim PRStatus As Double
                        PRStatus = cn.RunSentence("SELECT dbo.GetWorkingDatesFn([Req Date], { fn NOW() }) AS Aging FROM dbo.SC_OpenRequis WHERE ([Req Number] = " & r("Req Number") & ") AND (SAPBox = '" & r("SAPBox") & "')").Tables(0).Rows(0).Item(0)
                        cn.ExecuteInServer("Update LA_TMP_Open_Req_Distribution Set SPS = '" & lO.SPS & "', Owner = '" & lO.Owner & "', [Total USD] = dbo._fn_Get_LA_PR_Value('" & r("SAPBox") & "'," & r("Req Number") & "), Aging = " & PRStatus & " Where ((SAPBox = '" & r("SAPBox") & "') And ([Req Number] = '" & r("Req Number") & "'))")
                    Else
                        Dim PRStatus As Double
                        PRStatus = cn.RunSentence("SELECT dbo.GetWorkingDatesFn([Req Date], { fn NOW() }) AS Aging FROM dbo.SC_OpenRequis WHERE ([Req Number] = " & r("Req Number") & ") AND (SAPBox = '" & r("SAPBox") & "')").Tables(0).Rows(0).Item(0)
                        cn.ExecuteInServer("Update LA_TMP_Open_Req_Distribution Set SPS = 'BB0898', Owner = 'BB0898', [Total USD] = dbo._fn_Get_LA_PR_Value('" & r("SAPBox") & "'," & r("Req Number") & "), Aging = " & PRStatus & " Where ((SAPBox = '" & r("SAPBox") & "') And ([Req Number] = '" & r("Req Number") & "'))")
                    End If
                Catch ex As Exception
                    'Do nothing
                End Try
            Next
        End If

        '*****************************************************************************
        ' POs:
        '*****************************************************************************
        cn.ExecuteInServer("Delete From LA_TMP_Open_Orders_Distribution")
        Data = cn.RunSentence("Select * From vst_LA_Check_Distribution").Tables(0)
        If Data.Rows.Count > 0 Then
            For Each r As DataRow In Data.Rows
                Try
                    bgwApplyChanges.ReportProgress(((P * 100) / Data.Rows.Count), "Looking owner for: [PO: " & r("Doc Number") & ", SAPBox: " & r("SAPBox") & "]")
                    Dim lO As New oOwner
                    lO = GetOwner_OpenOrder(r("SAPBox"), r("Spend"), r("Plant"), r("Purch Grp"), r("Purch Org"))
                    If Not lO Is Nothing Then
                        cn.ExecuteInServer("Update LA_TMP_Open_Orders_Distribution Set SPS = '" & lO.SPS & "', Owner = '" & lO.Owner & "' Where ((SAPBox = '" & r("SAPBox") & "') And ([Doc Number] = '" & r("Doc Number") & "'))")
                    Else
                        'Si no tiene Owner se le asigna a Alejandra Baltodano
                        cn.ExecuteInServer("Update LA_TMP_Open_Orders_Distribution Set SPS = 'BB0898', Owner = 'BB0898' Where ((SAPBox = '" & r("SAPBox") & "') And ([Doc Number] = '" & r("Doc Number") & "'))")
                    End If
                Catch ex As Exception

                End Try
            Next
        End If

    End Sub


    Private Function GetOwner(ByVal pSAP As String, Optional ByVal pPlant As String = Nothing, Optional ByVal pPGrp As String = Nothing, Optional ByVal pPOrg As String = Nothing) As oOwner
        Dim cn As New OAConnection.Connection
        Dim Data As DataTable
        Dim Where As String = ""

        Try
            If Not pPlant Is Nothing Then
                If Where <> "" Then
                    Where = Where & " And "
                End If

                ' Where = Where & "((Plant = '') or (Plant = '" & pPlant & "'))"
                Where = Where & "((Plant = '" & pPlant & "'))"
            End If

            If Not pPGrp Is Nothing Then
                If Where <> "" Then
                    Where = Where & " And "
                End If

                'Where = Where & "(([P Grp] = '') or ([P Grp] = '" & pPGrp & "'))"
                Where = Where & "(([P Grp] = '" & pPGrp & "'))"
            End If

            If Not pPOrg Is Nothing Then
                If Where <> "" Then
                    Where = Where & " And "
                End If

                'Where = Where & "(([P Org] = '') or ([P Org] = '" & pPOrg & "'))"
                Where = Where & "(([P Org] = '" & pPOrg & "'))"
            End If

            Data = cn.RunSentence("Select *,0 as Value From LA_Indirect_Distribution Where (SAPBox = '" & pSAP & "')" & IIf(Where <> "", " And (" & Where & ")", "")).Tables(0)
            If Data.Rows.Count > 0 Then
                If Data.Rows.Count = 1 Then
                    Dim T As New oOwner

                    T.SPS = Data.Rows(0).Item("SPS")
                    T.Owner = Data.Rows(0).Item("Owner")
                    Return T
                Else

                    For Each r As DataRow In Data.Rows
                        Dim val As Integer = 0

                        If (r("SAPBox") = pSAP) Then
                            val += 2
                        Else
                            If r("SAPBox") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("Plant") = pPlant) Then
                            val += 2
                        Else
                            If r("Plant") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("P Org") = pPOrg) Then
                            val += 2
                        Else
                            If r("P Org") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("P Grp") = pPGrp) Then
                            val += 2
                        Else
                            If r("P Grp") = "" Then
                                val += 1
                            End If
                        End If

                        r("Value") = val
                    Next

                    Dim T As New oOwner
                    Dim SPS = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("SPS")).ToList()
                    Dim DOwner = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("Owner")).ToList()

                    T.SPS = SPS(0)
                    T.Owner = DOwner(0)

                    Return T
                End If
            Else
                ' MsgBox("Rules can't be found")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Function GetOwner_OpenOrder(ByVal pSAP As String, Optional ByVal pSpend As String = Nothing, Optional ByVal pPlant As String = Nothing, Optional ByVal pPGrp As String = Nothing, Optional ByVal pPOrg As String = Nothing) As oOwner
        Dim cn As New OAConnection.Connection
        Dim Data As DataTable
        Dim Where As String = ""

        Try
            If Not pSpend Is Nothing Then
                Where = "(([Spend] = 0) or ([Spend] = " & pSpend & "))"
            End If

            If Not pPlant Is Nothing Then
                If Where <> "" Then
                    Where = Where & " And "
                End If

                'Where = Where & "((Plant = '') or (Plant = '" & pPlant & "'))"
                Where = Where & "((Plant = '" & pPlant & "'))"
            End If

            If Not pPGrp Is Nothing Then
                If Where <> "" Then
                    Where = Where & " And "
                End If

                ' Where = Where & "(([P Grp] = '" & pPGrp & "'))"
                Where = Where & "(([P Grp] = '') or ([P Grp] = '" & pPGrp & "'))"
            End If

            If Not pPOrg Is Nothing Then
                If Where <> "" Then
                    Where = Where & " And "
                End If

                'Where = Where & "(([P Org] = '') or ([P Org] = '" & pPOrg & "'))"
                Where = Where & "(([P Org] = '" & pPOrg & "'))"
            End If


            Data = cn.RunSentence("Select *,0 as Value From LA_Indirect_Distribution Where (SAPBox = '" & pSAP & "')" & IIf(Where <> "", " And (" & Where & ")", "")).Tables(0)
            If Data.Rows.Count > 0 Then
                If Data.Rows.Count = 1 Then
                    Dim T As New oOwner

                    T.SPS = Data.Rows(0).Item("SPS")
                    T.Owner = Data.Rows(0).Item("Owner")
                    Return T
                Else

                    For Each r As DataRow In Data.Rows
                        Dim val As Integer = 0

                        If (r("SAPBox") = pSAP) Then
                            val += 2
                        Else
                            If r("SAPBox") = "" Then
                                val += 1
                            End If
                        End If


                        If (r("Plant") = pPlant) Then
                            val += 2
                        Else
                            If r("Plant") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("P Org") = pPOrg) Then
                            val += 2
                        Else
                            If r("P Org") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("P Grp") = pPGrp) Then
                            val += 2
                        Else
                            If r("P Grp") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("Spend") = pSpend) Then
                            val += 2
                        Else
                            If r("Spend") = 0 Then
                                val += 1
                            End If
                        End If

                        r("Value") = val
                    Next

                    Dim T As New oOwner
                    Dim SPS = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("SPS")).ToList()
                    Dim DOwner = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("Owner")).ToList()

                    T.SPS = SPS(0)
                    T.Owner = DOwner(0)

                    'MsgBox("Multiple choises for:" & Chr(13) & Chr(13) & "SAPBox: " & pSAP & Chr(13) & "LE: " & pLE & Chr(13) & "Plant:" & pPlant & Chr(13) & "Vendor: " & pVendor & Chr(13) & "Mat. Grp: " & pMatGrp)
                    Return T
                End If
            Else
                ' MsgBox("Rules can't be found")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub bgwSave_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwSave.ProgressChanged
        lblResult.Text = e.UserState
        pbProgress.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwApplyChanges_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwApplyChanges.ProgressChanged
        lblResult = e.UserState
        pbProgress.Value = e.ProgressPercentage
    End Sub
End Class

Friend Class oOwner
    Public SPS
    Public Owner
End Class

