<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm076
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm076))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grpDateFromTo = New System.Windows.Forms.GroupBox
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker
        Me.lblTo = New System.Windows.Forms.Label
        Me.lblFrom = New System.Windows.Forms.Label
        Me.tlbHerremientas = New System.Windows.Forms.ToolStrip
        Me.cmdDownload = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.pgrBar = New System.Windows.Forms.ToolStripProgressBar
        Me.BGOTD = New System.ComponentModel.BackgroundWorker
        Me.dgReport = New System.Windows.Forms.DataGridView
        Me.BGEKET = New System.ComponentModel.BackgroundWorker
        Me.BGEKKO = New System.ComponentModel.BackgroundWorker
        Me.grpDateFromTo.SuspendLayout()
        Me.tlbHerremientas.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpDateFromTo
        '
        Me.grpDateFromTo.Controls.Add(Me.dtpEndDate)
        Me.grpDateFromTo.Controls.Add(Me.dtpStartDate)
        Me.grpDateFromTo.Controls.Add(Me.lblTo)
        Me.grpDateFromTo.Controls.Add(Me.lblFrom)
        Me.grpDateFromTo.Location = New System.Drawing.Point(9, 42)
        Me.grpDateFromTo.Name = "grpDateFromTo"
        Me.grpDateFromTo.Size = New System.Drawing.Size(348, 57)
        Me.grpDateFromTo.TabIndex = 4
        Me.grpDateFromTo.TabStop = False
        Me.grpDateFromTo.Text = "Date range"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDate.Location = New System.Drawing.Point(222, 22)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(116, 20)
        Me.dtpEndDate.TabIndex = 6
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Location = New System.Drawing.Point(48, 22)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(116, 20)
        Me.dtpStartDate.TabIndex = 5
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(196, 26)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(20, 13)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(12, 26)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblFrom.TabIndex = 3
        Me.lblFrom.Text = "From"
        '
        'tlbHerremientas
        '
        Me.tlbHerremientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbHerremientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDownload, Me.ToolStripSeparator1, Me.ToolStripButton1})
        Me.tlbHerremientas.Location = New System.Drawing.Point(0, 0)
        Me.tlbHerremientas.Name = "tlbHerremientas"
        Me.tlbHerremientas.Size = New System.Drawing.Size(886, 39)
        Me.tlbHerremientas.TabIndex = 6
        Me.tlbHerremientas.Text = "ToolStrip1"
        '
        'cmdDownload
        '
        Me.cmdDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDownload.Image = CType(resources.GetObject("cmdDownload.Image"), System.Drawing.Image)
        Me.cmdDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDownload.Name = "cmdDownload"
        Me.cmdDownload.Size = New System.Drawing.Size(36, 36)
        Me.cmdDownload.Text = "ToolStripButton1"
        Me.cmdDownload.ToolTipText = "Download report"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.pgrBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 443)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(886, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(769, 17)
        Me.lblStatus.Spring = True
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pgrBar
        '
        Me.pgrBar.Name = "pgrBar"
        Me.pgrBar.Size = New System.Drawing.Size(100, 16)
        '
        'BGOTD
        '
        Me.BGOTD.WorkerReportsProgress = True
        '
        'dgReport
        '
        Me.dgReport.AllowUserToAddRows = False
        Me.dgReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgReport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgReport.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgReport.Location = New System.Drawing.Point(12, 105)
        Me.dgReport.Name = "dgReport"
        Me.dgReport.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgReport.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgReport.Size = New System.Drawing.Size(862, 328)
        Me.dgReport.TabIndex = 7
        '
        'BGEKET
        '
        Me.BGEKET.WorkerReportsProgress = True
        '
        'BGEKKO
        '
        Me.BGEKKO.WorkerReportsProgress = True
        '
        'frm076
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(886, 465)
        Me.Controls.Add(Me.dgReport)
        Me.Controls.Add(Me.tlbHerremientas)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.grpDateFromTo)
        Me.Name = "frm076"
        Me.Text = "[076] OTD Report"
        Me.grpDateFromTo.ResumeLayout(False)
        Me.grpDateFromTo.PerformLayout()
        Me.tlbHerremientas.ResumeLayout(False)
        Me.tlbHerremientas.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpDateFromTo As System.Windows.Forms.GroupBox
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents tlbHerremientas As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pgrBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents BGOTD As System.ComponentModel.BackgroundWorker
    Friend WithEvents dgReport As System.Windows.Forms.DataGridView
    Friend WithEvents BGEKET As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGEKKO As System.ComponentModel.BackgroundWorker
End Class
