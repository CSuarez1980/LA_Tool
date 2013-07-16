Public Class frm088
    Private Data As DataTable

    Private Sub frm088_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cn As New OAConnection.Connection

        Data = cn.RunSentence("Select * From vst_PGrp_Change").Tables(0)

        If Data.Rows.Count > 0 Then
            dtData.DataSource = Data
            dtData.AlternatingRowsDefaultCellStyle.BackColor = Drawing.Color.Azure
            With dtData.Columns("Total USD Value")
                .DefaultCellStyle.Format = "c"
                .DefaultCellStyle.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleRight
            End With
        Else
            MsgBox("No data could be selected.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub cmdRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRun.Click
        dtData.EndEdit()
        Data.AcceptChanges()

        If Not BGG4P.IsBusy AndAlso Not BGGBP.IsBusy AndAlso Not BGL6P.IsBusy AndAlso Not BGL7P.IsBusy Then
            PG.Style = Windows.Forms.ProgressBarStyle.Marquee
            BGG4P.RunWorkerAsync(Data)
            BGGBP.RunWorkerAsync(Data)
            BGL6P.RunWorkerAsync(Data)
            BGL7P.RunWorkerAsync(Data)
        Else
            MsgBox("Some process are runing please wait.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub The_End()
        If Not BGG4P.IsBusy AndAlso Not BGGBP.IsBusy AndAlso Not BGL6P.IsBusy AndAlso Not BGL7P.IsBusy Then
            lblStatus.Text = "P.Groups changed"
            PG.Style = Windows.Forms.ProgressBarStyle.Blocks
            MsgBox("Process finished")
        End If
    End Sub
    
    Private Sub BGL7P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGL7P.DoWork
        Dim D As DataTable = e.Argument
        Dim SAP As String = "L7P"

        Dim SC As New SAPCOM.SAPConnector
        Dim C As New Object
        C = SC.GetSAPConnection(SAP, gsUsuarioPC, "LAT")

        If D.Rows.Count > 0 Then
            For Each r As DataRow In D.Rows
                If r("SAPBox") = SAP AndAlso r("Change") Then
                    Dim POC As New SAPCOM.POChanges(C, r("Doc Number"))
                    'Dim POC As New SAPCOM.POChanges(C, "4500903209") ' -> L7A test
                    POC.PurchGroup = r("New PGrp")
                    POC.CommitChanges()

                    If POC.Success Then
                        BGL7P.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " changed.")
                    Else
                        BGL7P.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " failed." & POC.ResultString(False))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BGL7P_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BGL7P.ProgressChanged
        lstTrackChanges.Items.Insert(0, e.UserState)
    End Sub

    Private Sub BGL7P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGL7P.RunWorkerCompleted
        The_End()
    End Sub

    Private Sub BGL6P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGL6P.DoWork
        Dim D As DataTable = e.Argument
        Dim SAP As String = "L6P"

        Dim SC As New SAPCOM.SAPConnector
        Dim C As New Object
        C = SC.GetSAPConnection(SAP, gsUsuarioPC, "LAT")

        If D.Rows.Count > 0 Then
            For Each r As DataRow In D.Rows
                If r("SAPBox") = SAP AndAlso r("Change") Then
                    If Not DBNull.Value.Equals(r("New PGrp")) Then
                        Dim POC As New SAPCOM.POChanges(C, r("Doc Number"))
                        POC.PurchGroup = r("New PGrp")
                        POC.CommitChanges()

                        If POC.Success Then
                            BGL6P.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " changed.")
                        Else
                            BGL6P.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " failed." & POC.ResultString(False))
                        End If
                    Else
                        BGL6P.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " failed. No new P Grp selected.")
                    End If

                End If
            Next
        End If
    End Sub

    Private Sub BGG4P_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGG4P.DoWork
        Dim D As DataTable = e.Argument
        Dim SAP As String = "G4P"

        Dim SC As New SAPCOM.SAPConnector
        Dim C As New Object
        C = SC.GetSAPConnection(SAP, gsUsuarioPC, "LAT")

        If D.Rows.Count > 0 Then
            For Each r As DataRow In D.Rows
                If r("SAPBox") = SAP AndAlso r("Change") Then
                    Dim POC As New SAPCOM.POChanges(C, r("Doc Number"))
                    POC.PurchGroup = r("New PGrp")
                    POC.CommitChanges()

                    If POC.Success Then
                        BGG4P.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " changed.")
                    Else
                        BGG4P.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " failed." & POC.ResultString(False))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BGGBP_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGGBP.DoWork
        Dim D As DataTable = e.Argument
        Dim SAP As String = "GBP"

        Dim SC As New SAPCOM.SAPConnector
        Dim C As New Object
        C = SC.GetSAPConnection(SAP, gsUsuarioPC, "LAT")

        If D.Rows.Count > 0 Then
            For Each r As DataRow In D.Rows
                If r("SAPBox") = SAP AndAlso r("Change") Then
                    Dim POC As New SAPCOM.POChanges(C, r("Doc Number"))
                    POC.PurchGroup = r("New PGrp")
                    POC.CommitChanges()

                    If POC.Success Then
                        BGGBP.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " changed.")
                    Else
                        BGGBP.ReportProgress(0, Now.ToShortTimeString & " - PO: " & r("SAPBox") & r("Doc Number") & " failed." & POC.ResultString(False))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub BGL6P_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BGL6P.ProgressChanged
        lstTrackChanges.Items.Insert(0, e.UserState)
    End Sub

    Private Sub BGL6P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGL6P.RunWorkerCompleted
        The_End()
    End Sub

    Private Sub BGG4P_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BGG4P.ProgressChanged
        lstTrackChanges.Items.Insert(0, e.UserState)
    End Sub

    Private Sub BGG4P_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGG4P.RunWorkerCompleted
        The_End()
    End Sub

    Private Sub BGGBP_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BGGBP.ProgressChanged
        lstTrackChanges.Items.Insert(0, e.UserState)
    End Sub

    Private Sub BGGBP_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BGGBP.RunWorkerCompleted
        The_End()
    End Sub
End Class