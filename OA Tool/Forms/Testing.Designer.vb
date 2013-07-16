<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Testing
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
        Me.Button1 = New System.Windows.Forms.Button
        Me.dgvZMR0 = New System.Windows.Forms.DataGridView
        Me.BS = New System.Windows.Forms.BindingSource(Me.components)
        Me.tlbEstatus = New System.Windows.Forms.StatusStrip
        Me.lblContador = New System.Windows.Forms.ToolStripStatusLabel
        CType(Me.dgvZMR0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlbEstatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(2, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dgvZMR0
        '
        Me.dgvZMR0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvZMR0.Location = New System.Drawing.Point(2, 57)
        Me.dgvZMR0.Name = "dgvZMR0"
        Me.dgvZMR0.Size = New System.Drawing.Size(733, 430)
        Me.dgvZMR0.TabIndex = 1
        '
        'tlbEstatus
        '
        Me.tlbEstatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblContador})
        Me.tlbEstatus.Location = New System.Drawing.Point(0, 514)
        Me.tlbEstatus.Name = "tlbEstatus"
        Me.tlbEstatus.Size = New System.Drawing.Size(739, 22)
        Me.tlbEstatus.TabIndex = 2
        Me.tlbEstatus.Text = "StatusStrip1"
        '
        'lblContador
        '
        Me.lblContador.Name = "lblContador"
        Me.lblContador.Size = New System.Drawing.Size(37, 17)
        Me.lblContador.Text = "Rows:"
        '
        'Testing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 536)
        Me.Controls.Add(Me.tlbEstatus)
        Me.Controls.Add(Me.dgvZMR0)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Testing"
        Me.Text = "Testing"
        CType(Me.dgvZMR0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlbEstatus.ResumeLayout(False)
        Me.tlbEstatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dgvZMR0 As System.Windows.Forms.DataGridView
    Friend WithEvents BS As System.Windows.Forms.BindingSource
    Friend WithEvents tlbEstatus As System.Windows.Forms.StatusStrip
    Friend WithEvents lblContador As System.Windows.Forms.ToolStripStatusLabel
End Class
