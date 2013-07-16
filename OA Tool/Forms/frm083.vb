Public Class frm083
    Dim Data As New DataTable
    Dim Changed As Integer = 0
    Dim Failed As Integer = 0
    Dim Status As String = ""

    Private Sub frm083_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cn As New OAConnection.Connection
        Data = cn.RunSentence("Select * From vst_LA_Check_Trigger_Dristibution").Tables(0)

        DG.DataSource = Data
        txtStatus.Text = "Status: " & Data.Rows.Count & " Materials were found without owner"
    End Sub
    Private Function GetOwner(ByVal pSAP As String, Optional ByVal pPlant As String = Nothing, Optional ByVal pPGrp As String = Nothing, Optional ByVal pPOrg As String = Nothing) As Owner
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
                    Dim T As New Owner

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

                    Dim T As New Owner
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

    Private Sub cmdGetOwner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetOwner.Click
        Me.pbStatus.Maximum = Data.Rows.Count
        BG.RunWorkerAsync()
    End Sub

    Private Sub BG_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BG.DoWork
        Dim cn As New OAConnection.Connection
        Dim cont As Integer = 0
        Changed = 0
        Failed = 0


        'For Each r As DataRow In Data.Rows
        '    Try
        '        Status = "Status: Looking owner for: [Material: " & r("Material") & ", Plant: " & r("Plant") & "]"
        '        BG.ReportProgress(cont)
        '        Dim lO As New Owner
        '        lO = GetOwner(r("Box"), r("Plant"), r("PGrp"), r("POrg"))
        '        If Not lO Is Nothing Then
        '            cn.ExecuteInServer("Update LA_TMP_Trigger_Distribution Set SPS = '" & lO.SPS & "', Owner = '" & lO.Owner & "' Where ((SAPBox = '" & r("Box") & "') And ([Material] = '" & r("Material") & "') And ([Plant] = '" & r("Plant") & "'))")
        '            Changed += 1
        '        Else
        '            cn.ExecuteInServer("Update LA_TMP_Trigger_Distribution Set SPS = 'BB0898', Owner = 'BB0898' Where ((SAPBox = '" & r("Box") & "') And ([Material] = '" & r("Material") & "') And ([Plant] = '" & r("Plant") & "'))")
        '            Failed += 1
        '        End If
        '        cont += 1
        '    Catch ex As Exception
        '        Failed += 1
        '    End Try
        'Next

        For Each r As DataRow In Data.Rows
            Try
                Status = "Status: Looking owner for: [Material: " & r("Material") & ", Plant: " & r("Plant") & "]"
                BG.ReportProgress(cont)

                'If r("Material") = "30348221" Then
                '    MsgBox("")
                'End If

                Dim RX As New OAConnection.DMS_User(r("Box"), r("MATGRP"), r("PGrp"), r("POrg"), r("Plant"))
                RX.Execute()

                If RX.Success Then
                    cn.ExecuteInServer("Update LA_TMP_Trigger_Distribution Set SPS = '" & RX.SPS & "', Owner = '" & RX.PTB & "' Where ((SAPBox = '" & r("Box") & "') And ([Material] = '" & r("Material") & "') And ([Plant] = '" & r("Plant") & "'))")
                    Changed += 1
                Else
                    cn.ExecuteInServer("Update LA_TMP_Trigger_Distribution Set SPS = '" & RX.SPS & "', Owner = '" & RX.PTB & "' Where ((SAPBox = '" & r("Box") & "') And ([Material] = '" & r("Material") & "') And ([Plant] = '" & r("Plant") & "'))")
                    Failed += 1
                End If
                cont += 1
            Catch ex As Exception
                Failed += 1
            End Try
        Next
    End Sub
    Private Sub BG_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BG.ProgressChanged
        pbStatus.Value = e.ProgressPercentage
        txtStatus.Text = Status
    End Sub
    Private Sub BG_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BG.RunWorkerCompleted
        pbStatus.Value = 0
        Status = ("Process completed. Total owners found: " & Changed & ". Total failed: " & Failed)
        txtStatus.Text = Status
        MsgBox("Process completed.                     " & Chr(13) & Chr(13) & "    Total changed: " & Changed & Chr(13) & "    Total failed: " & Failed, MsgBoxStyle.Information, "Process completed")
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        txtStatus.Text = "Status: Reading trigger file..."
        ofdFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        ofdFile.Filter = "Excel files (*.xls)|*.xls|Excel 2007-2010 files (*.xlsx)|*.xlsx"

        If ofdFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim cn As New OAConnection.Connection
            Data = cn.Read_Trigger_File(ofdFile.FileName, "L7P")
            DG.DataSource = Data
            If data.Rows.Count > 0 Then
                cn.RunSentence("Delete From Trigger_LA")
                cn.AppendTableToSqlServer("Trigger_LA", Data)

                cn.ExecuteInServer("DELETE FROM LA_TMP_Trigger_Distribution Where (NOT EXISTS (SELECT [Material] FROM Trigger_LA Where (dbo.LA_TMP_Trigger_Distribution.[Material] = [Material]) AND (dbo.LA_TMP_Trigger_Distribution.SAPBox = SAPBox) AND (dbo.LA_TMP_Trigger_Distribution.Plant = Plant)))")

                'Agregar las POs que son nuevas a la distribucion temporal
                cn.ExecuteInServer("Insert Into LA_TMP_Trigger_Distribution (SAPBox, MATERIAL, PLANT) SELECT DISTINCT Box, MATERIAL, PLANT  From Trigger_LA WHERE (NOT EXISTS (SELECT Material From dbo.LA_TMP_Trigger_Distribution Where (dbo.Trigger_LA.[Material] = [Material]) AND (dbo.Trigger_LA.Box = SAPBox) AND (Trigger_LA.Plant = Plant)))")

                Data = cn.RunSentence("Select * From vst_LA_Check_Trigger_Dristibution").Tables(0)
                pbStatus.Maximum = Data.Rows.Count
                BG.RunWorkerAsync()
            End If
        End If

    End Sub
End Class

Friend Class Owner
    Public SPS
    Public Owner
End Class