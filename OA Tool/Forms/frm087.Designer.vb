<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm087
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tlbTools = New System.Windows.Forms.ToolStrip
        Me.cmdSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdTemplate = New System.Windows.Forms.ToolStripButton
        Me.cmdSearch = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton
        Me.bgwNCM = New System.ComponentModel.BackgroundWorker
        Me.ofdFile = New System.Windows.Forms.OpenFileDialog
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.txtStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblResult = New System.Windows.Forms.ToolStripStatusLabel
        Me.pbProgress = New System.Windows.Forms.ToolStripProgressBar
        Me.dtgData = New System.Windows.Forms.DataGridView
        Me.tlbTools.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlbTools
        '
        Me.tlbTools.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSave, Me.ToolStripSeparator2, Me.cmdTemplate, Me.cmdSearch, Me.ToolStripSeparator1, Me.cmdExcel})
        Me.tlbTools.Location = New System.Drawing.Point(0, 0)
        Me.tlbTools.Name = "tlbTools"
        Me.tlbTools.Size = New System.Drawing.Size(784, 39)
        Me.tlbTools.TabIndex = 1
        Me.tlbTools.Text = "ToolStrip1"
        '
        'cmdSave
        '
        Me.cmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSave.Image = Global.OA_Tool.My.Resources.Resources.Save
        Me.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(36, 36)
        Me.cmdSave.Text = "ToolStripButton1"
        Me.cmdSave.ToolTipText = "Download report"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'cmdTemplate
        '
        Me.cmdTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdTemplate.Image = Global.OA_Tool.My.Resources.Resources.icons
        Me.cmdTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdTemplate.Name = "cmdTemplate"
        Me.cmdTemplate.Size = New System.Drawing.Size(36, 36)
        Me.cmdTemplate.Text = "ToolStripButton1"
        Me.cmdTemplate.ToolTipText = "Get template"
        Me.cmdTemplate.Visible = False
        '
        'cmdSearch
        '
        Me.cmdSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSearch.Image = Global.OA_Tool.My.Resources.Resources.findf
        Me.cmdSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(36, 36)
        Me.cmdSearch.Text = "ToolStripButton1"
        Me.cmdSearch.ToolTipText = "Search for NCM file"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdExcel
        '
        Me.cmdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExcel.Image = Global.OA_Tool.My.Resources.Resources.doc_excel_original
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(36, 36)
        Me.cmdExcel.Text = "ToolStripButton2"
        Me.cmdExcel.ToolTipText = "Export to MS Excel current version"
        '
        'bgwNCM
        '
        '
        'ofdFile
        '
        Me.ofdFile.Title = "Search trigger file"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtStatus, Me.lblResult, Me.pbProgress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 440)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(784, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'txtStatus
        '
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(42, 17)
        Me.txtStatus.Text = "Status:"
        '
        'lblResult
        '
        Me.lblResult.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(625, 17)
        Me.lblResult.Spring = True
        Me.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbProgress
        '
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(100, 16)
        '
        'dtgData
        '
        Me.dtgData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgData.Location = New System.Drawing.Point(13, 43)
        Me.dtgData.Name = "dtgData"
        Me.dtgData.Size = New System.Drawing.Size(759, 394)
        Me.dtgData.TabIndex = 3
        '
        'frm087
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 462)
        Me.Controls.Add(Me.dtgData)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.tlbTools)
        Me.Name = "frm087"
        Me.Text = "[087] NCM massive upload"
        Me.tlbTools.ResumeLayout(False)
        Me.tlbTools.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlbTools As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdTemplate As System.Windows.Forms.ToolStripButton
    Friend WithEvents bgwNCM As System.ComponentModel.BackgroundWorker
    Friend WithEvents cmdSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents ofdFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents txtStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblResult As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbProgress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents dtgData As System.Windows.Forms.DataGridView
End Class
