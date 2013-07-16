Public Class frm077
    Public SD As Date
    Public ED As Date
    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        SD = dtpStartDate.Value
        ED = dtpEndDate.Value

        BGEKKO.RunWorkerAsync()
    End Sub

    Private Sub BGEKKO_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BGEKKO.DoWork
        Dim SAP As String = "L7P"

        Dim Nast As New SAPCOM.NAST_Report(SAP, gsUsuarioPC, AppId)

        If Not Nast.IsReady Then
            MsgBox("Error getting SAP RFC connection.")
        Else

        End If
    End Sub

   
End Class