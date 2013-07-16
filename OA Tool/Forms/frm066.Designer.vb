<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm066
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm066))
        Me.DG = New System.Windows.Forms.DataGridView
        Me.TS = New System.Windows.Forms.ToolStrip
        Me.cmdRelease = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdSelect = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboSAPBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboVariantes = New System.Windows.Forms.ComboBox
        Me.lblVariantes = New System.Windows.Forms.Label
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        CType(Me.DG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TS.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DG
        '
        Me.DG.AllowUserToAddRows = False
        Me.DG.AllowUserToResizeRows = False
        Me.DG.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG.Location = New System.Drawing.Point(12, 120)
        Me.DG.Name = "DG"
        Me.DG.Size = New System.Drawing.Size(804, 225)
        Me.DG.TabIndex = 0
        '
        'TS
        '
        Me.TS.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.TS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripSeparator2, Me.cmdRelease, Me.ToolStripSeparator1, Me.cmdSelect})
        Me.TS.Location = New System.Drawing.Point(0, 0)
        Me.TS.Name = "TS"
        Me.TS.Size = New System.Drawing.Size(828, 39)
        Me.TS.TabIndex = 1
        Me.TS.Text = "ToolStrip1"
        '
        'cmdRelease
        '
        Me.cmdRelease.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRelease.Image = CType(resources.GetObject("cmdRelease.Image"), System.Drawing.Image)
        Me.cmdRelease.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRelease.Name = "cmdRelease"
        Me.cmdRelease.Size = New System.Drawing.Size(36, 36)
        Me.cmdRelease.Text = "ToolStripButton1"
        Me.cmdRelease.ToolTipText = "Release records"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdSelect
        '
        Me.cmdSelect.CheckOnClick = True
        Me.cmdSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSelect.Image = CType(resources.GetObject("cmdSelect.Image"), System.Drawing.Image)
        Me.cmdSelect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(36, 36)
        Me.cmdSelect.Text = "ToolStripButton1"
        Me.cmdSelect.ToolTipText = "Select / Unselect All"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboSAPBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboVariantes)
        Me.GroupBox1.Controls.Add(Me.lblVariantes)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(261, 72)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameters"
        '
        'cboSAPBox
        '
        Me.cboSAPBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSAPBox.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboSAPBox.FormattingEnabled = True
        Me.cboSAPBox.Location = New System.Drawing.Point(60, 19)
        Me.cboSAPBox.Name = "cboSAPBox"
        Me.cboSAPBox.Size = New System.Drawing.Size(191, 21)
        Me.cboSAPBox.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "SAP Box"
        '
        'cboVariantes
        '
        Me.cboVariantes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVariantes.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboVariantes.FormattingEnabled = True
        Me.cboVariantes.Location = New System.Drawing.Point(60, 43)
        Me.cboVariantes.Name = "cboVariantes"
        Me.cboVariantes.Size = New System.Drawing.Size(191, 21)
        Me.cboVariantes.TabIndex = 26
        '
        'lblVariantes
        '
        Me.lblVariantes.AutoSize = True
        Me.lblVariantes.Location = New System.Drawing.Point(6, 46)
        Me.lblVariantes.Name = "lblVariantes"
        Me.lblVariantes.Size = New System.Drawing.Size(45, 13)
        Me.lblVariantes.TabIndex = 25
        Me.lblVariantes.Text = "Variants"
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
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'frm066
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(828, 357)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TS)
        Me.Controls.Add(Me.DG)
        Me.Name = "frm066"
        Me.Text = "[066] Release Records"
        CType(Me.DG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TS.ResumeLayout(False)
        Me.TS.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DG As System.Windows.Forms.DataGridView
    Friend WithEvents TS As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdRelease As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdSelect As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboSAPBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboVariantes As System.Windows.Forms.ComboBox
    Friend WithEvents lblVariantes As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
End Class
