<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm064
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm064))
        Me.dtgTicket = New System.Windows.Forms.DataGridView
        Me.tlbHerramientas = New System.Windows.Forms.ToolStrip
        Me.cmdSave = New System.Windows.Forms.ToolStripButton
        CType(Me.dtgTicket, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlbHerramientas.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgTicket
        '
        Me.dtgTicket.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtgTicket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgTicket.Location = New System.Drawing.Point(12, 42)
        Me.dtgTicket.Name = "dtgTicket"
        Me.dtgTicket.Size = New System.Drawing.Size(666, 295)
        Me.dtgTicket.TabIndex = 0
        '
        'tlbHerramientas
        '
        Me.tlbHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSave})
        Me.tlbHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tlbHerramientas.Name = "tlbHerramientas"
        Me.tlbHerramientas.Size = New System.Drawing.Size(690, 39)
        Me.tlbHerramientas.TabIndex = 1
        Me.tlbHerramientas.Text = "ToolStrip1"
        '
        'cmdSave
        '
        Me.cmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(36, 36)
        Me.cmdSave.Text = "ToolStripButton1"
        Me.cmdSave.ToolTipText = "Upload report"
        '
        'frm064
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 363)
        Me.Controls.Add(Me.tlbHerramientas)
        Me.Controls.Add(Me.dtgTicket)
        Me.Name = "frm064"
        Me.Text = "[064] Upload Ticket Report"
        CType(Me.dtgTicket, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlbHerramientas.ResumeLayout(False)
        Me.tlbHerramientas.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgTicket As System.Windows.Forms.DataGridView
    Friend WithEvents tlbHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSave As System.Windows.Forms.ToolStripButton
End Class
