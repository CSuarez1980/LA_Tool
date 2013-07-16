<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm056
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm056))
        Me.grpApplyTo = New System.Windows.Forms.GroupBox
        Me.txtItem = New System.Windows.Forms.TextBox
        Me.txtDocument = New System.Windows.Forms.TextBox
        Me.lblItem = New System.Windows.Forms.Label
        Me.lblDocument = New System.Windows.Forms.Label
        Me.optAll = New System.Windows.Forms.RadioButton
        Me.optOne = New System.Windows.Forms.RadioButton
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.grpApplyTo.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpApplyTo
        '
        Me.grpApplyTo.Controls.Add(Me.txtItem)
        Me.grpApplyTo.Controls.Add(Me.txtDocument)
        Me.grpApplyTo.Controls.Add(Me.lblItem)
        Me.grpApplyTo.Controls.Add(Me.lblDocument)
        Me.grpApplyTo.Controls.Add(Me.optAll)
        Me.grpApplyTo.Controls.Add(Me.optOne)
        Me.grpApplyTo.Location = New System.Drawing.Point(13, 13)
        Me.grpApplyTo.Name = "grpApplyTo"
        Me.grpApplyTo.Size = New System.Drawing.Size(461, 75)
        Me.grpApplyTo.TabIndex = 0
        Me.grpApplyTo.TabStop = False
        Me.grpApplyTo.Text = "Apply to:"
        '
        'txtItem
        '
        Me.txtItem.Location = New System.Drawing.Point(297, 42)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.ReadOnly = True
        Me.txtItem.Size = New System.Drawing.Size(64, 20)
        Me.txtItem.TabIndex = 5
        Me.txtItem.Visible = False
        '
        'txtDocument
        '
        Me.txtDocument.Location = New System.Drawing.Point(297, 20)
        Me.txtDocument.Name = "txtDocument"
        Me.txtDocument.ReadOnly = True
        Me.txtDocument.Size = New System.Drawing.Size(154, 20)
        Me.txtDocument.TabIndex = 4
        Me.txtDocument.Visible = False
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Location = New System.Drawing.Point(223, 46)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(67, 13)
        Me.lblItem.TabIndex = 3
        Me.lblItem.Text = "Item Number"
        Me.lblItem.Visible = False
        '
        'lblDocument
        '
        Me.lblDocument.AutoSize = True
        Me.lblDocument.Location = New System.Drawing.Point(223, 24)
        Me.lblDocument.Name = "lblDocument"
        Me.lblDocument.Size = New System.Drawing.Size(70, 13)
        Me.lblDocument.TabIndex = 2
        Me.lblDocument.Text = "Doc. Number"
        Me.lblDocument.Visible = False
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Checked = True
        Me.optAll.Location = New System.Drawing.Point(36, 44)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(106, 17)
        Me.optAll.TabIndex = 1
        Me.optAll.TabStop = True
        Me.optAll.Text = "All selected items"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'optOne
        '
        Me.optOne.AutoSize = True
        Me.optOne.Location = New System.Drawing.Point(36, 22)
        Me.optOne.Name = "optOne"
        Me.optOne.Size = New System.Drawing.Size(87, 17)
        Me.optOne.TabIndex = 0
        Me.optOne.Text = "Only this item"
        Me.optOne.UseVisualStyleBackColor = True
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(13, 95)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(461, 224)
        Me.txtComment.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(402, 325)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 30)
        Me.Button1.TabIndex = 2
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frm056
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(488, 363)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.grpApplyTo)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm056"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[056] Add header text comment"
        Me.grpApplyTo.ResumeLayout(False)
        Me.grpApplyTo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpApplyTo As System.Windows.Forms.GroupBox
    Friend WithEvents txtDocument As System.Windows.Forms.TextBox
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents lblDocument As System.Windows.Forms.Label
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents optOne As System.Windows.Forms.RadioButton
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
