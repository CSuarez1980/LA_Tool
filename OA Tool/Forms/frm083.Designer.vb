<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm083
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm083))
        Me.BG = New System.ComponentModel.BackgroundWorker
        Me.DG = New System.Windows.Forms.DataGridView
        Me.ts_Estado = New System.Windows.Forms.StatusStrip
        Me.txtStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.pbStatus = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.cmdGetOwner = New System.Windows.Forms.ToolStripButton
        Me.ofdFile = New System.Windows.Forms.OpenFileDialog
        CType(Me.DG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ts_Estado.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BG
        '
        Me.BG.WorkerReportsProgress = True
        '
        'DG
        '
        Me.DG.AllowUserToAddRows = False
        Me.DG.AllowUserToDeleteRows = False
        Me.DG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG.Location = New System.Drawing.Point(6, 42)
        Me.DG.Name = "DG"
        Me.DG.Size = New System.Drawing.Size(699, 265)
        Me.DG.TabIndex = 0
        '
        'ts_Estado
        '
        Me.ts_Estado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtStatus, Me.pbStatus})
        Me.ts_Estado.Location = New System.Drawing.Point(0, 310)
        Me.ts_Estado.Name = "ts_Estado"
        Me.ts_Estado.Size = New System.Drawing.Size(711, 22)
        Me.ts_Estado.TabIndex = 1
        Me.ts_Estado.Text = "StatusStrip1"
        '
        'txtStatus
        '
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(544, 17)
        Me.txtStatus.Spring = True
        Me.txtStatus.Text = "Status"
        Me.txtStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbStatus
        '
        Me.pbStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(150, 16)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.cmdGetOwner})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(711, 39)
        Me.ToolStrip1.TabIndex = 2
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
        Me.ToolStripButton1.ToolTipText = "Upload trigger file"
        '
        'cmdGetOwner
        '
        Me.cmdGetOwner.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdGetOwner.Image = CType(resources.GetObject("cmdGetOwner.Image"), System.Drawing.Image)
        Me.cmdGetOwner.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGetOwner.Name = "cmdGetOwner"
        Me.cmdGetOwner.Size = New System.Drawing.Size(36, 36)
        Me.cmdGetOwner.Text = "ToolStripButton1"
        Me.cmdGetOwner.ToolTipText = "Find owner"
        '
        'ofdFile
        '
        Me.ofdFile.Title = "Search trigger file"
        '
        'frm083
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(711, 332)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.ts_Estado)
        Me.Controls.Add(Me.DG)
        Me.Name = "frm083"
        Me.Text = "[083] Trigger upload"
        CType(Me.DG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ts_Estado.ResumeLayout(False)
        Me.ts_Estado.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BG As System.ComponentModel.BackgroundWorker
    Friend WithEvents DG As System.Windows.Forms.DataGridView
    Friend WithEvents ts_Estado As System.Windows.Forms.StatusStrip
    Friend WithEvents txtStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbStatus As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdGetOwner As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ofdFile As System.Windows.Forms.OpenFileDialog
End Class
