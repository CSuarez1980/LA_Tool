<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm049
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm049))
        Me.dtgRequis = New System.Windows.Forms.DataGridView
        Me.tbrHerramientas = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        CType(Me.dtgRequis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbrHerramientas.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgRequis
        '
        Me.dtgRequis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgRequis.Location = New System.Drawing.Point(13, 45)
        Me.dtgRequis.Name = "dtgRequis"
        Me.dtgRequis.Size = New System.Drawing.Size(645, 326)
        Me.dtgRequis.TabIndex = 0
        '
        'tbrHerramientas
        '
        Me.tbrHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tbrHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.tbrHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tbrHerramientas.Name = "tbrHerramientas"
        Me.tbrHerramientas.Size = New System.Drawing.Size(670, 39)
        Me.tbrHerramientas.TabIndex = 1
        Me.tbrHerramientas.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Export to MS Excell"
        '
        'frm049
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(670, 383)
        Me.Controls.Add(Me.tbrHerramientas)
        Me.Controls.Add(Me.dtgRequis)
        Me.Name = "frm049"
        Me.Text = "[049] Requisition Analysis"
        CType(Me.dtgRequis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbrHerramientas.ResumeLayout(False)
        Me.tbrHerramientas.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgRequis As System.Windows.Forms.DataGridView
    Friend WithEvents tbrHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
End Class
