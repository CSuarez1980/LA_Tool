<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm072
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm072))
        Me.BGN6P = New System.ComponentModel.BackgroundWorker
        Me.BGL7P = New System.ComponentModel.BackgroundWorker
        Me.BGGBP = New System.ComponentModel.BackgroundWorker
        Me.BGL6P = New System.ComponentModel.BackgroundWorker
        Me.tlbMenu = New System.Windows.Forms.ToolStrip
        Me.cmdDownload = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.stbStatus = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblSpace = New System.Windows.Forms.ToolStripStatusLabel
        Me.pgbProgress = New System.Windows.Forms.ToolStripProgressBar
        Me.lblWorking = New System.Windows.Forms.ToolStripStatusLabel
        Me.BGG4P = New System.ComponentModel.BackgroundWorker
        Me.grpParameters = New System.Windows.Forms.GroupBox
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.lblEnd = New System.Windows.Forms.Label
        Me.dtpInicio = New System.Windows.Forms.DateTimePicker
        Me.lblDateFrom = New System.Windows.Forms.Label
        Me.dtgData = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkN6P = New System.Windows.Forms.CheckBox
        Me.chkL6P = New System.Windows.Forms.CheckBox
        Me.chkGGBP = New System.Windows.Forms.CheckBox
        Me.chkG4P = New System.Windows.Forms.CheckBox
        Me.chkL7P = New System.Windows.Forms.CheckBox
        Me.tlbMenu.SuspendLayout()
        Me.stbStatus.SuspendLayout()
        Me.grpParameters.SuspendLayout()
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BGN6P
        '
        Me.BGN6P.WorkerReportsProgress = True
        '
        'BGL7P
        '
        Me.BGL7P.WorkerReportsProgress = True
        '
        'BGGBP
        '
        Me.BGGBP.WorkerReportsProgress = True
        '
        'BGL6P
        '
        Me.BGL6P.WorkerReportsProgress = True
        '
        'tlbMenu
        '
        Me.tlbMenu.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDownload, Me.ToolStripSeparator1, Me.ToolStripButton1})
        Me.tlbMenu.Location = New System.Drawing.Point(0, 0)
        Me.tlbMenu.Name = "tlbMenu"
        Me.tlbMenu.Size = New System.Drawing.Size(937, 39)
        Me.tlbMenu.TabIndex = 7
        Me.tlbMenu.Text = "ToolStrip1"
        '
        'cmdDownload
        '
        Me.cmdDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDownload.Image = CType(resources.GetObject("cmdDownload.Image"), System.Drawing.Image)
        Me.cmdDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDownload.Name = "cmdDownload"
        Me.cmdDownload.Size = New System.Drawing.Size(36, 36)
        Me.cmdDownload.Text = "ToolStripButton1"
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
        'stbStatus
        '
        Me.stbStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.lblSpace, Me.pgbProgress, Me.lblWorking})
        Me.stbStatus.Location = New System.Drawing.Point(0, 571)
        Me.stbStatus.Name = "stbStatus"
        Me.stbStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.stbStatus.Size = New System.Drawing.Size(937, 22)
        Me.stbStatus.TabIndex = 8
        Me.stbStatus.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(145, 17)
        Me.lblStatus.Text = "Status: Waiting for actions"
        '
        'lblSpace
        '
        Me.lblSpace.Name = "lblSpace"
        Me.lblSpace.Size = New System.Drawing.Size(625, 17)
        Me.lblSpace.Spring = True
        '
        'pgbProgress
        '
        Me.pgbProgress.Name = "pgbProgress"
        Me.pgbProgress.Size = New System.Drawing.Size(150, 16)
        '
        'lblWorking
        '
        Me.lblWorking.Image = CType(resources.GetObject("lblWorking.Image"), System.Drawing.Image)
        Me.lblWorking.Name = "lblWorking"
        Me.lblWorking.Size = New System.Drawing.Size(136, 17)
        Me.lblWorking.Text = "working... please wait"
        Me.lblWorking.Visible = False
        '
        'BGG4P
        '
        Me.BGG4P.WorkerReportsProgress = True
        '
        'grpParameters
        '
        Me.grpParameters.Controls.Add(Me.dtpEnd)
        Me.grpParameters.Controls.Add(Me.lblEnd)
        Me.grpParameters.Controls.Add(Me.dtpInicio)
        Me.grpParameters.Controls.Add(Me.lblDateFrom)
        Me.grpParameters.Location = New System.Drawing.Point(12, 42)
        Me.grpParameters.Name = "grpParameters"
        Me.grpParameters.Size = New System.Drawing.Size(228, 90)
        Me.grpParameters.TabIndex = 10
        Me.grpParameters.TabStop = False
        Me.grpParameters.Text = "Parameters"
        '
        'dtpEnd
        '
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEnd.Location = New System.Drawing.Point(84, 51)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(124, 20)
        Me.dtpEnd.TabIndex = 3
        '
        'lblEnd
        '
        Me.lblEnd.AutoSize = True
        Me.lblEnd.Location = New System.Drawing.Point(21, 55)
        Me.lblEnd.Name = "lblEnd"
        Me.lblEnd.Size = New System.Drawing.Size(42, 13)
        Me.lblEnd.TabIndex = 2
        Me.lblEnd.Text = "Date to"
        '
        'dtpInicio
        '
        Me.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInicio.Location = New System.Drawing.Point(84, 25)
        Me.dtpInicio.Name = "dtpInicio"
        Me.dtpInicio.Size = New System.Drawing.Size(124, 20)
        Me.dtpInicio.TabIndex = 1
        '
        'lblDateFrom
        '
        Me.lblDateFrom.AutoSize = True
        Me.lblDateFrom.Location = New System.Drawing.Point(21, 29)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(53, 13)
        Me.lblDateFrom.TabIndex = 0
        Me.lblDateFrom.Text = "Date from"
        '
        'dtgData
        '
        Me.dtgData.AllowUserToAddRows = False
        Me.dtgData.AllowUserToDeleteRows = False
        Me.dtgData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgData.Location = New System.Drawing.Point(12, 138)
        Me.dtgData.Name = "dtgData"
        Me.dtgData.Size = New System.Drawing.Size(913, 430)
        Me.dtgData.TabIndex = 9
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkN6P)
        Me.GroupBox1.Controls.Add(Me.chkL6P)
        Me.GroupBox1.Controls.Add(Me.chkGGBP)
        Me.GroupBox1.Controls.Add(Me.chkG4P)
        Me.GroupBox1.Controls.Add(Me.chkL7P)
        Me.GroupBox1.Location = New System.Drawing.Point(238, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(687, 90)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SAP box"
        '
        'chkN6P
        '
        Me.chkN6P.AutoSize = True
        Me.chkN6P.Checked = True
        Me.chkN6P.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkN6P.Location = New System.Drawing.Point(138, 24)
        Me.chkN6P.Name = "chkN6P"
        Me.chkN6P.Size = New System.Drawing.Size(47, 17)
        Me.chkN6P.TabIndex = 17
        Me.chkN6P.Text = "N6P"
        Me.chkN6P.UseVisualStyleBackColor = True
        '
        'chkL6P
        '
        Me.chkL6P.AutoSize = True
        Me.chkL6P.Checked = True
        Me.chkL6P.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkL6P.Location = New System.Drawing.Point(78, 50)
        Me.chkL6P.Name = "chkL6P"
        Me.chkL6P.Size = New System.Drawing.Size(45, 17)
        Me.chkL6P.TabIndex = 16
        Me.chkL6P.Text = "L6P"
        Me.chkL6P.UseVisualStyleBackColor = True
        '
        'chkGGBP
        '
        Me.chkGGBP.AutoSize = True
        Me.chkGGBP.Checked = True
        Me.chkGGBP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGGBP.Location = New System.Drawing.Point(78, 24)
        Me.chkGGBP.Name = "chkGGBP"
        Me.chkGGBP.Size = New System.Drawing.Size(48, 17)
        Me.chkGGBP.TabIndex = 15
        Me.chkGGBP.Text = "GBP"
        Me.chkGGBP.UseVisualStyleBackColor = True
        '
        'chkG4P
        '
        Me.chkG4P.AutoSize = True
        Me.chkG4P.Checked = True
        Me.chkG4P.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkG4P.Location = New System.Drawing.Point(16, 50)
        Me.chkG4P.Name = "chkG4P"
        Me.chkG4P.Size = New System.Drawing.Size(47, 17)
        Me.chkG4P.TabIndex = 14
        Me.chkG4P.Text = "G4P"
        Me.chkG4P.UseVisualStyleBackColor = True
        '
        'chkL7P
        '
        Me.chkL7P.AutoSize = True
        Me.chkL7P.Checked = True
        Me.chkL7P.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkL7P.Location = New System.Drawing.Point(16, 24)
        Me.chkL7P.Name = "chkL7P"
        Me.chkL7P.Size = New System.Drawing.Size(45, 17)
        Me.chkL7P.TabIndex = 13
        Me.chkL7P.Text = "L7P"
        Me.chkL7P.UseVisualStyleBackColor = True
        '
        'frm072
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(937, 593)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpParameters)
        Me.Controls.Add(Me.dtgData)
        Me.Controls.Add(Me.tlbMenu)
        Me.Controls.Add(Me.stbStatus)
        Me.Name = "frm072"
        Me.Text = "[072] IR-Only Purchase Order Report "
        Me.tlbMenu.ResumeLayout(False)
        Me.tlbMenu.PerformLayout()
        Me.stbStatus.ResumeLayout(False)
        Me.stbStatus.PerformLayout()
        Me.grpParameters.ResumeLayout(False)
        Me.grpParameters.PerformLayout()
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BGN6P As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGL7P As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGGBP As System.ComponentModel.BackgroundWorker
    Friend WithEvents BGL6P As System.ComponentModel.BackgroundWorker
    Friend WithEvents tlbMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents stbStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblSpace As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pgbProgress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblWorking As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BGG4P As System.ComponentModel.BackgroundWorker
    Friend WithEvents grpParameters As System.Windows.Forms.GroupBox
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEnd As System.Windows.Forms.Label
    Friend WithEvents dtpInicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDateFrom As System.Windows.Forms.Label
    Friend WithEvents dtgData As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkN6P As System.Windows.Forms.CheckBox
    Friend WithEvents chkL6P As System.Windows.Forms.CheckBox
    Friend WithEvents chkGGBP As System.Windows.Forms.CheckBox
    Friend WithEvents chkG4P As System.Windows.Forms.CheckBox
    Friend WithEvents chkL7P As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
End Class
