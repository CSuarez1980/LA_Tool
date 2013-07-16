<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm086
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
        Me.cmdDownload = New System.Windows.Forms.ToolStripButton
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton
        Me.stbStatus = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.pgbProgress = New System.Windows.Forms.ToolStripProgressBar
        Me.dtgData = New System.Windows.Forms.DataGridView
        Me.bgwOAs = New System.ComponentModel.BackgroundWorker
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblTo = New System.Windows.Forms.Label
        Me.lblFrom = New System.Windows.Forms.Label
        Me.dtpTo = New System.Windows.Forms.DateTimePicker
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker
        Me.grpRegion = New System.Windows.Forms.GroupBox
        Me.rbtNA = New System.Windows.Forms.RadioButton
        Me.rbtLA = New System.Windows.Forms.RadioButton
        Me.tlbTools.SuspendLayout()
        Me.stbStatus.SuspendLayout()
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.grpRegion.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlbTools
        '
        Me.tlbTools.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDownload, Me.cmdExcel})
        Me.tlbTools.Location = New System.Drawing.Point(0, 0)
        Me.tlbTools.Name = "tlbTools"
        Me.tlbTools.Size = New System.Drawing.Size(708, 39)
        Me.tlbTools.TabIndex = 0
        Me.tlbTools.Text = "ToolStrip1"
        '
        'cmdDownload
        '
        Me.cmdDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDownload.Image = Global.OA_Tool.My.Resources.Resources._133
        Me.cmdDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDownload.Name = "cmdDownload"
        Me.cmdDownload.Size = New System.Drawing.Size(36, 36)
        Me.cmdDownload.Text = "ToolStripButton1"
        Me.cmdDownload.ToolTipText = "Download report"
        '
        'cmdExcel
        '
        Me.cmdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExcel.Image = Global.OA_Tool.My.Resources.Resources.doc_excel_original
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(36, 36)
        Me.cmdExcel.Text = "ToolStripButton2"
        Me.cmdExcel.ToolTipText = "Export to Excel"
        '
        'stbStatus
        '
        Me.stbStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.pgbProgress})
        Me.stbStatus.Location = New System.Drawing.Point(0, 390)
        Me.stbStatus.Name = "stbStatus"
        Me.stbStatus.Size = New System.Drawing.Size(708, 22)
        Me.stbStatus.TabIndex = 1
        Me.stbStatus.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(586, 17)
        Me.lblStatus.Spring = True
        Me.lblStatus.Text = "<Status>"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pgbProgress
        '
        Me.pgbProgress.Name = "pgbProgress"
        Me.pgbProgress.Size = New System.Drawing.Size(100, 16)
        '
        'dtgData
        '
        Me.dtgData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgData.Location = New System.Drawing.Point(7, 124)
        Me.dtgData.Name = "dtgData"
        Me.dtgData.Size = New System.Drawing.Size(694, 259)
        Me.dtgData.TabIndex = 2
        '
        'bgwOAs
        '
        Me.bgwOAs.WorkerReportsProgress = True
        Me.bgwOAs.WorkerSupportsCancellation = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTo)
        Me.GroupBox1.Controls.Add(Me.lblFrom)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(217, 77)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Time range"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(26, 49)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(20, 13)
        Me.lblTo.TabIndex = 3
        Me.lblTo.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(26, 23)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblFrom.TabIndex = 2
        Me.lblFrom.Text = "From"
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(76, 45)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(124, 20)
        Me.dtpTo.TabIndex = 1
        '
        'dtpFrom
        '
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(76, 19)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(124, 20)
        Me.dtpFrom.TabIndex = 0
        '
        'grpRegion
        '
        Me.grpRegion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpRegion.Controls.Add(Me.rbtNA)
        Me.grpRegion.Controls.Add(Me.rbtLA)
        Me.grpRegion.Location = New System.Drawing.Point(226, 43)
        Me.grpRegion.Name = "grpRegion"
        Me.grpRegion.Size = New System.Drawing.Size(474, 77)
        Me.grpRegion.TabIndex = 4
        Me.grpRegion.TabStop = False
        Me.grpRegion.Text = "Region"
        '
        'rbtNA
        '
        Me.rbtNA.AutoSize = True
        Me.rbtNA.Location = New System.Drawing.Point(23, 41)
        Me.rbtNA.Name = "rbtNA"
        Me.rbtNA.Size = New System.Drawing.Size(92, 17)
        Me.rbtNA.TabIndex = 1
        Me.rbtNA.Text = "North America"
        Me.rbtNA.UseVisualStyleBackColor = True
        '
        'rbtLA
        '
        Me.rbtLA.AutoSize = True
        Me.rbtLA.Checked = True
        Me.rbtLA.Location = New System.Drawing.Point(23, 18)
        Me.rbtLA.Name = "rbtLA"
        Me.rbtLA.Size = New System.Drawing.Size(89, 17)
        Me.rbtLA.TabIndex = 0
        Me.rbtLA.TabStop = True
        Me.rbtLA.Text = "Latin America"
        Me.rbtLA.UseVisualStyleBackColor = True
        '
        'frm086
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(708, 412)
        Me.Controls.Add(Me.grpRegion)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dtgData)
        Me.Controls.Add(Me.stbStatus)
        Me.Controls.Add(Me.tlbTools)
        Me.Name = "frm086"
        Me.Text = "[086] Background spend report"
        Me.tlbTools.ResumeLayout(False)
        Me.tlbTools.PerformLayout()
        Me.stbStatus.ResumeLayout(False)
        Me.stbStatus.PerformLayout()
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpRegion.ResumeLayout(False)
        Me.grpRegion.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlbTools As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents stbStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents dtgData As System.Windows.Forms.DataGridView
    Friend WithEvents bgwOAs As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pgbProgress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents grpRegion As System.Windows.Forms.GroupBox
    Friend WithEvents rbtNA As System.Windows.Forms.RadioButton
    Friend WithEvents rbtLA As System.Windows.Forms.RadioButton
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
End Class
