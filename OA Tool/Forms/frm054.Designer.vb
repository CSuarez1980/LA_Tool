<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm054
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm054))
        Me.dtgSpend = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton
        Me.tlbEstado = New System.Windows.Forms.StatusStrip
        Me.lblTotal = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtCantidad = New System.Windows.Forms.ToolStripStatusLabel
        Me.BS = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.dtgSpend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.tlbEstado.SuspendLayout()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtgSpend
        '
        Me.dtgSpend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgSpend.Location = New System.Drawing.Point(13, 42)
        Me.dtgSpend.Name = "dtgSpend"
        Me.dtgSpend.Size = New System.Drawing.Size(787, 444)
        Me.dtgSpend.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdExcel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(812, 39)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdExcel
        '
        Me.cmdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExcel.Image = CType(resources.GetObject("cmdExcel.Image"), System.Drawing.Image)
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(36, 36)
        Me.cmdExcel.Text = "Export to Excel"
        '
        'tlbEstado
        '
        Me.tlbEstado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTotal, Me.txtCantidad})
        Me.tlbEstado.Location = New System.Drawing.Point(0, 489)
        Me.tlbEstado.Name = "tlbEstado"
        Me.tlbEstado.Size = New System.Drawing.Size(812, 22)
        Me.tlbEstado.TabIndex = 2
        Me.tlbEstado.Text = "StatusStrip1"
        '
        'lblTotal
        '
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(35, 17)
        Me.lblTotal.Text = "Total:"
        '
        'txtCantidad
        '
        Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCantidad.ForeColor = System.Drawing.Color.Navy
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(14, 17)
        Me.txtCantidad.Text = "0"
        '
        'frm054
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 511)
        Me.Controls.Add(Me.tlbEstado)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.dtgSpend)
        Me.Name = "frm054"
        Me.Text = "[054] Annual Spend Resumen"
        CType(Me.dtgSpend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tlbEstado.ResumeLayout(False)
        Me.tlbEstado.PerformLayout()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgSpend As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbEstado As System.Windows.Forms.StatusStrip
    Friend WithEvents lblTotal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtCantidad As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BS As System.Windows.Forms.BindingSource
End Class
