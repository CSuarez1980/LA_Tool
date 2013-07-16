<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm062
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
        Me.dtgRequisitions = New System.Windows.Forms.DataGridView
        CType(Me.dtgRequisitions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtgRequisitions
        '
        Me.dtgRequisitions.AllowUserToAddRows = False
        Me.dtgRequisitions.AllowUserToDeleteRows = False
        Me.dtgRequisitions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgRequisitions.Location = New System.Drawing.Point(13, 12)
        Me.dtgRequisitions.Name = "dtgRequisitions"
        Me.dtgRequisitions.RowHeadersWidth = 10
        Me.dtgRequisitions.Size = New System.Drawing.Size(743, 242)
        Me.dtgRequisitions.TabIndex = 0
        '
        'frm062
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(768, 266)
        Me.Controls.Add(Me.dtgRequisitions)
        Me.Name = "frm062"
        Me.Text = "[062] Requisitions without confirmation"
        CType(Me.dtgRequisitions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtgRequisitions As System.Windows.Forms.DataGridView
End Class
