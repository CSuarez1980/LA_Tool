<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm089
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtL7P = New System.Windows.Forms.TextBox
        Me.txtL6P = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtG4P = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtGBP = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmdSave = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "L7P"
        '
        'txtL7P
        '
        Me.txtL7P.Location = New System.Drawing.Point(54, 18)
        Me.txtL7P.Name = "txtL7P"
        Me.txtL7P.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtL7P.Size = New System.Drawing.Size(165, 20)
        Me.txtL7P.TabIndex = 2
        '
        'txtL6P
        '
        Me.txtL6P.Location = New System.Drawing.Point(54, 44)
        Me.txtL6P.Name = "txtL6P"
        Me.txtL6P.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtL6P.Size = New System.Drawing.Size(165, 20)
        Me.txtL6P.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "L6P"
        '
        'txtG4P
        '
        Me.txtG4P.Location = New System.Drawing.Point(54, 70)
        Me.txtG4P.Name = "txtG4P"
        Me.txtG4P.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtG4P.Size = New System.Drawing.Size(165, 20)
        Me.txtG4P.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "G4P"
        '
        'txtGBP
        '
        Me.txtGBP.Location = New System.Drawing.Point(54, 96)
        Me.txtGBP.Name = "txtGBP"
        Me.txtGBP.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtGBP.Size = New System.Drawing.Size(165, 20)
        Me.txtGBP.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "GBP"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(246, 18)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 9
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'frm089
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(331, 145)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.txtGBP)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtG4P)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtL6P)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtL7P)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frm089"
        Me.Text = "[089] Auto NCM Changes pass register"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtL7P As System.Windows.Forms.TextBox
    Friend WithEvents txtL6P As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtG4P As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtGBP As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
End Class
