Public Class frm053
    Private Sub frm053_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WebBrowser1.Navigate(My.Application.Info.DirectoryPath & "\OAHTML\Convertidor.htm")
        Me.WebBrowser1.ScriptErrorsSuppressed = True
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Me.WebBrowser1.Navigate(My.Application.Info.DirectoryPath & "\OAHTML\Convertidor.htm")
        Me.WebBrowser1.ScriptErrorsSuppressed = True
    End Sub

    Private Sub WebBrowser1_ProgressChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles WebBrowser1.ProgressChanged
        Me.ProgressBar.Maximum = e.MaximumProgress
        ProgressBar.Value = e.CurrentProgress
    End Sub
End Class