<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm088
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
        Me.dtData = New System.Windows.Forms.DataGridView
        Me.tspTools = New System.Windows.Forms.ToolStrip
        Me.cmdRun = New System.Windows.Forms.ToolStripButton
        Me.stsStatus = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.PG = New System.Windows.Forms.ToolStripProgressBar
        Me.BGL7P = New System.ComponentModel.BackgroundWorker
        Me.BGL6P = New System.ComponentModel.BackgroundWorker
        Me.BGG4P = New System.ComponentModel.BackgroundWorker
        Me.BGGBP = New System.ComponentModel.BackgroundWorker
        Me.lstTrackChanges = New System.Windows.Forms.ListBox
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.dtData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tspTools.SuspendLayout()
        Me.stsStatus.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtData
        '
        Me.dtData.AllowUserToAddRows = False
        Me.dtData.AllowUserToDeleteRows = False
        Me.dtData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dtData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtData.Location = New System.Drawing.Point(12, 42)
        Me.dtData.Name = "dtData"
        Me.dtData.Size = New System.Drawing.Size(871, 456)
        Me.dtData.TabIndex = 0
        '
        'tspTools
        '
        Me.tspTools.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tspTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdRun})
        Me.tspTools.Location = New System.Drawing.Point(0, 0)
        Me.tspTools.Name = "tspTools"
        Me.tspTools.Size = New System.Drawing.Size(895, 39)
        Me.tspTools.TabIndex = 1
        Me.tspTools.Text = "ToolStrip1"
        '
        'cmdRun
        '
        Me.cmdRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRun.Image = Global.OA_Tool.My.Resources.Resources._133
        Me.cmdRun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRun.Name = "cmdRun"
        Me.cmdRun.Size = New System.Drawing.Size(36, 36)
        Me.cmdRun.Text = "ToolStripButton1"
        '
        'stsStatus
        '
        Me.stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.PG})
        Me.stsStatus.Location = New System.Drawing.Point(0, 609)
        Me.stsStatus.Name = "stsStatus"
        Me.stsStatus.Size = New System.Drawing.Size(895, 22)
        Me.stsStatus.TabIndex = 2
        Me.stsStatus.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(778, 17)
        Me.lblStatus.Spring = True
        Me.lblStatus.Text = "Purchase order P. Group changes"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PG
        '
        Me.PG.Name = "PG"
        Me.PG.Size = New System.Drawing.Size(100, 16)
        '
        'BGL7P
        '
        Me.BGL7P.WorkerReportsProgress = True
        Me.BGL7P.WorkerSupportsCancellation = True
        '
        'BGL6P
        '
        Me.BGL6P.WorkerReportsProgress = True
        Me.BGL6P.WorkerSupportsCancellation = True
        '
        'BGG4P
        '
        Me.BGG4P.WorkerReportsProgress = True
        Me.BGG4P.WorkerSupportsCancellation = True
        '
        'BGGBP
        '
        Me.BGGBP.WorkerReportsProgress = True
        Me.BGGBP.WorkerSupportsCancellation = True
        '
        'lstTrackChanges
        '
        Me.lstTrackChanges.Dock = System.Windows.Forms.DockStyle.Right
        Me.lstTrackChanges.FormattingEnabled = True
        Me.lstTrackChanges.Location = New System.Drawing.Point(661, 0)
        Me.lstTrackChanges.Name = "lstTrackChanges"
        Me.lstTrackChanges.Size = New System.Drawing.Size(234, 95)
        Me.lstTrackChanges.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lstTrackChanges)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 504)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(895, 105)
        Me.Panel1.TabIndex = 4
        '
        'frm088
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(895, 631)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.stsStatus)
        Me.Controls.Add(Me.tspTools)
        Me.Controls.Add(Me.dtData)
        Me.Name = "frm088"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[088] Purchase group changes"
        CType(Me.dtData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tspTools.ResumeLayout(False)
        Me.tspTools.PerformLayout()
        Me.stsStatus.ResumeLayout(False)
        Me.stsStatus.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtData As System.Windows.Forms.DataGridView
    Friend WithEvents tspTools As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdRun As System.Windows.Forms.ToolStripButton
    Friend WithEvents stsStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents BGL7P As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGL6P As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGG4P As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGGBP As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PG As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lstTrackChanges As System.Windows.Forms.ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
