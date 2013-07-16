<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm081
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm081))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.BS = New System.Windows.Forms.BindingSource(Me.components)
        Me.dtgData = New System.Windows.Forms.DataGridView
        Me.stBar = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtCount = New System.Windows.Forms.ToolStripStatusLabel
        Me.BGW = New System.ComponentModel.BackgroundWorker
        Me.ToolStrip1.SuspendLayout()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.stBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(828, 39)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'dtgData
        '
        Me.dtgData.AllowUserToAddRows = False
        Me.dtgData.AllowUserToDeleteRows = False
        Me.dtgData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgData.Location = New System.Drawing.Point(12, 42)
        Me.dtgData.Name = "dtgData"
        Me.dtgData.Size = New System.Drawing.Size(804, 327)
        Me.dtgData.TabIndex = 1
        '
        'stBar
        '
        Me.stBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.txtCount})
        Me.stBar.Location = New System.Drawing.Point(0, 383)
        Me.stBar.Name = "stBar"
        Me.stBar.Size = New System.Drawing.Size(828, 22)
        Me.stBar.TabIndex = 2
        Me.stBar.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(52, 17)
        Me.lblStatus.Text = "Records:"
        '
        'txtCount
        '
        Me.txtCount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtCount.ForeColor = System.Drawing.Color.Blue
        Me.txtCount.Name = "txtCount"
        Me.txtCount.Size = New System.Drawing.Size(14, 17)
        Me.txtCount.Text = "0"
        '
        'BGW
        '
        Me.BGW.WorkerReportsProgress = True
        '
        'frm081
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(828, 405)
        Me.Controls.Add(Me.stBar)
        Me.Controls.Add(Me.dtgData)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frm081"
        Me.Text = "[081] LA Bloqued Invoices Report"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtgData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.stBar.ResumeLayout(False)
        Me.stBar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BS As System.Windows.Forms.BindingSource
    Friend WithEvents dtgData As System.Windows.Forms.DataGridView
    Friend WithEvents stBar As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BGW As System.ComponentModel.BackgroundWorker
End Class
