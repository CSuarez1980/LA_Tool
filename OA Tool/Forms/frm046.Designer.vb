<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm046
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm046))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboSAPBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboVariantes = New System.Windows.Forms.ComboBox
        Me.lblVariantes = New System.Windows.Forms.Label
        Me.ToolBar = New System.Windows.Forms.ToolStrip
        Me.cmdDowload = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnExcel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtgPO = New System.Windows.Forms.DataGridView
        Me.SFD = New System.Windows.Forms.SaveFileDialog
        Me.BG = New System.ComponentModel.BackgroundWorker
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.PB = New System.Windows.Forms.ToolStripProgressBar
        Me.imgWorking = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox1.SuspendLayout()
        Me.ToolBar.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.dtgPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboSAPBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboVariantes)
        Me.GroupBox1.Controls.Add(Me.lblVariantes)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(253, 71)
        Me.GroupBox1.TabIndex = 49
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameters"
        '
        'cboSAPBox
        '
        Me.cboSAPBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSAPBox.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboSAPBox.FormattingEnabled = True
        Me.cboSAPBox.Location = New System.Drawing.Point(57, 19)
        Me.cboSAPBox.Name = "cboSAPBox"
        Me.cboSAPBox.Size = New System.Drawing.Size(191, 21)
        Me.cboSAPBox.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "SAP Box"
        '
        'cboVariantes
        '
        Me.cboVariantes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVariantes.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboVariantes.FormattingEnabled = True
        Me.cboVariantes.Location = New System.Drawing.Point(57, 43)
        Me.cboVariantes.Name = "cboVariantes"
        Me.cboVariantes.Size = New System.Drawing.Size(191, 21)
        Me.cboVariantes.TabIndex = 26
        '
        'lblVariantes
        '
        Me.lblVariantes.AutoSize = True
        Me.lblVariantes.Location = New System.Drawing.Point(7, 46)
        Me.lblVariantes.Name = "lblVariantes"
        Me.lblVariantes.Size = New System.Drawing.Size(45, 13)
        Me.lblVariantes.TabIndex = 25
        Me.lblVariantes.Text = "Variants"
        '
        'ToolBar
        '
        Me.ToolBar.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDowload, Me.ToolStripSeparator1, Me.btnExcel, Me.ToolStripSeparator5, Me.ToolStripButton1})
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.Size = New System.Drawing.Size(925, 39)
        Me.ToolBar.TabIndex = 54
        Me.ToolBar.Text = "ToolStrip1"
        '
        'cmdDowload
        '
        Me.cmdDowload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDowload.Image = CType(resources.GetObject("cmdDowload.Image"), System.Drawing.Image)
        Me.cmdDowload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDowload.Name = "cmdDowload"
        Me.cmdDowload.Size = New System.Drawing.Size(36, 36)
        Me.cmdDowload.Text = "ToolStripButton1"
        Me.cmdDowload.ToolTipText = "Download órdenes de compra"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'btnExcel
        '
        Me.btnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(36, 36)
        Me.btnExcel.Text = "ToolStripButton1"
        Me.btnExcel.ToolTipText = "Exporta a MS Excel"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Export XML"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.dtpEnd)
        Me.GroupBox5.Controls.Add(Me.dtpStart)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Location = New System.Drawing.Point(259, 40)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(204, 71)
        Me.GroupBox5.TabIndex = 55
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "PO's Created Range"
        '
        'dtpEnd
        '
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEnd.Location = New System.Drawing.Point(56, 39)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(120, 20)
        Me.dtpEnd.TabIndex = 3
        '
        'dtpStart
        '
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStart.Location = New System.Drawing.Point(57, 16)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(120, 20)
        Me.dtpStart.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "To:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "From"
        '
        'dtgPO
        '
        Me.dtgPO.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgPO.Location = New System.Drawing.Point(7, 117)
        Me.dtgPO.Name = "dtgPO"
        Me.dtgPO.Size = New System.Drawing.Size(909, 415)
        Me.dtgPO.TabIndex = 56
        '
        'BG
        '
        Me.BG.WorkerReportsProgress = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.PB, Me.imgWorking})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 531)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(925, 26)
        Me.StatusStrip1.TabIndex = 57
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(758, 21)
        Me.lblStatus.Spring = True
        Me.lblStatus.Text = "Purchase Order Report"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PB
        '
        Me.PB.Enabled = False
        Me.PB.Name = "PB"
        Me.PB.Size = New System.Drawing.Size(150, 20)
        Me.PB.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'imgWorking
        '
        Me.imgWorking.Image = CType(resources.GetObject("imgWorking.Image"), System.Drawing.Image)
        Me.imgWorking.Name = "imgWorking"
        Me.imgWorking.Size = New System.Drawing.Size(16, 21)
        Me.imgWorking.Visible = False
        '
        'frm046
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(925, 557)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.dtgPO)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.ToolBar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm046"
        Me.Text = "[046] Purchase Orders Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolBar.ResumeLayout(False)
        Me.ToolBar.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.dtgPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboSAPBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboVariantes As System.Windows.Forms.ComboBox
    Friend WithEvents lblVariantes As System.Windows.Forms.Label
    Friend WithEvents ToolBar As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDowload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtgPO As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SFD As System.Windows.Forms.SaveFileDialog
    Friend WithEvents BG As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents imgWorking As System.Windows.Forms.ToolStripStatusLabel
End Class
