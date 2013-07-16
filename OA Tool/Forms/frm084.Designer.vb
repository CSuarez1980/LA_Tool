<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm084
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
        Me.dtgData = New System.Windows.Forms.DataGridView
        Me.bgwReadFile = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.txtStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblResult = New System.Windows.Forms.ToolStripStatusLabel
        Me.pbProgress = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdSearch = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdTemplate = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdApplyChanges = New System.Windows.Forms.ToolStripButton
        Me.ofdFile = New System.Windows.Forms.OpenFileDialog
        Me.bgwSave = New System.ComponentModel.BackgroundWorker
        Me.bgwApplyChanges = New System.ComponentModel.BackgroundWorker
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgData
        '
        Me.dtgData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgData.Location = New System.Drawing.Point(7, 42)
        Me.dtgData.Name = "dtgData"
        Me.dtgData.Size = New System.Drawing.Size(658, 224)
        Me.dtgData.TabIndex = 0
        '
        'bgwReadFile
        '
        Me.bgwReadFile.WorkerReportsProgress = True
        Me.bgwReadFile.WorkerSupportsCancellation = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtStatus, Me.lblResult, Me.pbProgress})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 271)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(671, 22)
        Me.StatusStrip1.TabIndex = 1
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
        Me.lblResult.Size = New System.Drawing.Size(481, 17)
        Me.lblResult.Spring = True
        Me.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbProgress
        '
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(100, 16)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSearch, Me.ToolStripSeparator1, Me.cmdSave, Me.ToolStripSeparator2, Me.cmdTemplate, Me.ToolStripSeparator3, Me.cmdApplyChanges})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(671, 39)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdSearch
        '
        Me.cmdSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSearch.Image = Global.OA_Tool.My.Resources.Resources.findf
        Me.cmdSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(36, 36)
        Me.cmdSearch.Text = "ToolStripButton1"
        Me.cmdSearch.ToolTipText = "Search for file"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdSave
        '
        Me.cmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSave.Image = Global.OA_Tool.My.Resources.Resources.Save
        Me.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(36, 36)
        Me.cmdSave.Text = "ToolStripButton1"
        Me.cmdSave.ToolTipText = "Save"
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
        Me.cmdTemplate.Text = "ToolStripButton2"
        Me.cmdTemplate.ToolTipText = "Get distibution template"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'cmdApplyChanges
        '
        Me.cmdApplyChanges.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdApplyChanges.Image = Global.OA_Tool.My.Resources.Resources.quick_restart
        Me.cmdApplyChanges.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdApplyChanges.Name = "cmdApplyChanges"
        Me.cmdApplyChanges.Size = New System.Drawing.Size(36, 36)
        Me.cmdApplyChanges.Text = "ToolStripButton1"
        Me.cmdApplyChanges.ToolTipText = "Apply changes"
        '
        'ofdFile
        '
        Me.ofdFile.Title = "Search trigger file"
        '
        'bgwSave
        '
        Me.bgwSave.WorkerReportsProgress = True
        Me.bgwSave.WorkerSupportsCancellation = True
        '
        'bgwApplyChanges
        '
        Me.bgwApplyChanges.WorkerReportsProgress = True
        Me.bgwApplyChanges.WorkerSupportsCancellation = True
        '
        'frm084
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 293)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.dtgData)
        Me.Name = "frm084"
        Me.Text = "[084] LA Indirect DMS Distribution"
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgData As System.Windows.Forms.DataGridView
    Friend WithEvents bgwReadFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ofdFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdTemplate As System.Windows.Forms.ToolStripButton
    Friend WithEvents bgwSave As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdApplyChanges As System.Windows.Forms.ToolStripButton
    Friend WithEvents bgwApplyChanges As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblResult As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbProgress As System.Windows.Forms.ToolStripProgressBar
End Class
