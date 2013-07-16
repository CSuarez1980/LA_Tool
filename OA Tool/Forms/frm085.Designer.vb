<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm085
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdServices = New System.Windows.Forms.Button
        Me.cmdGoods = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdServices)
        Me.GroupBox1.Controls.Add(Me.cmdGoods)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(335, 147)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'cmdServices
        '
        Me.cmdServices.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdServices.Image = Global.OA_Tool.My.Resources.Resources.advancedsettings
        Me.cmdServices.Location = New System.Drawing.Point(187, 42)
        Me.cmdServices.Name = "cmdServices"
        Me.cmdServices.Size = New System.Drawing.Size(101, 82)
        Me.cmdServices.TabIndex = 5
        Me.cmdServices.Text = "Services"
        Me.cmdServices.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdServices.UseVisualStyleBackColor = True
        '
        'cmdGoods
        '
        Me.cmdGoods.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdGoods.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGoods.Image = Global.OA_Tool.My.Resources.Resources.kdf
        Me.cmdGoods.Location = New System.Drawing.Point(50, 42)
        Me.cmdGoods.Name = "cmdGoods"
        Me.cmdGoods.Size = New System.Drawing.Size(101, 82)
        Me.cmdGoods.TabIndex = 4
        Me.cmdGoods.Text = "Goods"
        Me.cmdGoods.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdGoods.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(18, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(312, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Please select purchase order type:"
        '
        'frm085
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(348, 164)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm085"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[085] Please select purchase order type:"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdServices As System.Windows.Forms.Button
    Friend WithEvents cmdGoods As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
