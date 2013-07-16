<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm079
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm079))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.lblVName = New System.Windows.Forms.Label
        Me.txtNCMCode = New System.Windows.Forms.TextBox
        Me.txtMOrigin = New System.Windows.Forms.TextBox
        Me.txtMUsage = New System.Windows.Forms.TextBox
        Me.txtTCode = New System.Windows.Forms.TextBox
        Me.lblNCMCode = New System.Windows.Forms.Label
        Me.lblMOrigin = New System.Windows.Forms.Label
        Me.lblMUsage = New System.Windows.Forms.Label
        Me.lblTCode = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtMatGrp = New System.Windows.Forms.TextBox
        Me.txtVCode = New System.Windows.Forms.TextBox
        Me.txtPlant = New System.Windows.Forms.TextBox
        Me.txtLegEnt = New System.Windows.Forms.TextBox
        Me.txtSAP = New System.Windows.Forms.TextBox
        Me.lblMatGrp = New System.Windows.Forms.Label
        Me.lblVCode = New System.Windows.Forms.Label
        Me.lblPlant = New System.Windows.Forms.Label
        Me.lblLegEnt = New System.Windows.Forms.Label
        Me.lblSAP = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdRules = New System.Windows.Forms.DataGridView
        Me.BS = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.txtMatCategory = New System.Windows.Forms.TextBox
        Me.lblMatCategory = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdRules, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtMatCategory)
        Me.GroupBox1.Controls.Add(Me.lblMatCategory)
        Me.GroupBox1.Controls.Add(Me.cmdAdd)
        Me.GroupBox1.Controls.Add(Me.lblVName)
        Me.GroupBox1.Controls.Add(Me.txtNCMCode)
        Me.GroupBox1.Controls.Add(Me.txtMOrigin)
        Me.GroupBox1.Controls.Add(Me.txtMUsage)
        Me.GroupBox1.Controls.Add(Me.txtTCode)
        Me.GroupBox1.Controls.Add(Me.lblNCMCode)
        Me.GroupBox1.Controls.Add(Me.lblMOrigin)
        Me.GroupBox1.Controls.Add(Me.lblMUsage)
        Me.GroupBox1.Controls.Add(Me.lblTCode)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.txtMatGrp)
        Me.GroupBox1.Controls.Add(Me.txtVCode)
        Me.GroupBox1.Controls.Add(Me.txtPlant)
        Me.GroupBox1.Controls.Add(Me.txtLegEnt)
        Me.GroupBox1.Controls.Add(Me.txtSAP)
        Me.GroupBox1.Controls.Add(Me.lblMatGrp)
        Me.GroupBox1.Controls.Add(Me.lblVCode)
        Me.GroupBox1.Controls.Add(Me.lblPlant)
        Me.GroupBox1.Controls.Add(Me.lblLegEnt)
        Me.GroupBox1.Controls.Add(Me.lblSAP)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(749, 506)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Rules"
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(612, 28)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(97, 82)
        Me.cmdAdd.TabIndex = 21
        Me.cmdAdd.Text = "Include rule"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'lblVName
        '
        Me.lblVName.Location = New System.Drawing.Point(215, 97)
        Me.lblVName.Name = "lblVName"
        Me.lblVName.Size = New System.Drawing.Size(134, 13)
        Me.lblVName.TabIndex = 20
        Me.lblVName.Text = "<Vendor Name>"
        '
        'txtNCMCode
        '
        Me.txtNCMCode.Location = New System.Drawing.Point(496, 117)
        Me.txtNCMCode.Name = "txtNCMCode"
        Me.txtNCMCode.Size = New System.Drawing.Size(100, 20)
        Me.txtNCMCode.TabIndex = 19
        '
        'txtMOrigin
        '
        Me.txtMOrigin.Location = New System.Drawing.Point(496, 70)
        Me.txtMOrigin.Name = "txtMOrigin"
        Me.txtMOrigin.Size = New System.Drawing.Size(46, 20)
        Me.txtMOrigin.TabIndex = 18
        Me.txtMOrigin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMUsage
        '
        Me.txtMUsage.Location = New System.Drawing.Point(496, 47)
        Me.txtMUsage.Name = "txtMUsage"
        Me.txtMUsage.Size = New System.Drawing.Size(46, 20)
        Me.txtMUsage.TabIndex = 17
        Me.txtMUsage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTCode
        '
        Me.txtTCode.Location = New System.Drawing.Point(496, 24)
        Me.txtTCode.Name = "txtTCode"
        Me.txtTCode.Size = New System.Drawing.Size(46, 20)
        Me.txtTCode.TabIndex = 16
        Me.txtTCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblNCMCode
        '
        Me.lblNCMCode.AutoSize = True
        Me.lblNCMCode.Location = New System.Drawing.Point(414, 121)
        Me.lblNCMCode.Name = "lblNCMCode"
        Me.lblNCMCode.Size = New System.Drawing.Size(59, 13)
        Me.lblNCMCode.TabIndex = 15
        Me.lblNCMCode.Text = "NCM Code"
        '
        'lblMOrigin
        '
        Me.lblMOrigin.AutoSize = True
        Me.lblMOrigin.Location = New System.Drawing.Point(414, 74)
        Me.lblMOrigin.Name = "lblMOrigin"
        Me.lblMOrigin.Size = New System.Drawing.Size(55, 13)
        Me.lblMOrigin.TabIndex = 14
        Me.lblMOrigin.Text = "Mat Origin"
        '
        'lblMUsage
        '
        Me.lblMUsage.AutoSize = True
        Me.lblMUsage.Location = New System.Drawing.Point(414, 51)
        Me.lblMUsage.Name = "lblMUsage"
        Me.lblMUsage.Size = New System.Drawing.Size(59, 13)
        Me.lblMUsage.TabIndex = 13
        Me.lblMUsage.Text = "Mat Usage"
        '
        'lblTCode
        '
        Me.lblTCode.AutoSize = True
        Me.lblTCode.Location = New System.Drawing.Point(414, 28)
        Me.lblTCode.Name = "lblTCode"
        Me.lblTCode.Size = New System.Drawing.Size(53, 13)
        Me.lblTCode.TabIndex = 12
        Me.lblTCode.Text = "Tax Code"
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(374, 13)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(4, 133)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "GroupBox3"
        '
        'txtMatGrp
        '
        Me.txtMatGrp.Location = New System.Drawing.Point(109, 116)
        Me.txtMatGrp.Name = "txtMatGrp"
        Me.txtMatGrp.Size = New System.Drawing.Size(100, 20)
        Me.txtMatGrp.TabIndex = 10
        '
        'txtVCode
        '
        Me.txtVCode.Location = New System.Drawing.Point(109, 93)
        Me.txtVCode.Name = "txtVCode"
        Me.txtVCode.Size = New System.Drawing.Size(100, 20)
        Me.txtVCode.TabIndex = 9
        '
        'txtPlant
        '
        Me.txtPlant.Location = New System.Drawing.Point(109, 70)
        Me.txtPlant.Name = "txtPlant"
        Me.txtPlant.Size = New System.Drawing.Size(46, 20)
        Me.txtPlant.TabIndex = 8
        Me.txtPlant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLegEnt
        '
        Me.txtLegEnt.Location = New System.Drawing.Point(109, 47)
        Me.txtLegEnt.Name = "txtLegEnt"
        Me.txtLegEnt.Size = New System.Drawing.Size(46, 20)
        Me.txtLegEnt.TabIndex = 7
        Me.txtLegEnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSAP
        '
        Me.txtSAP.Location = New System.Drawing.Point(109, 24)
        Me.txtSAP.Name = "txtSAP"
        Me.txtSAP.Size = New System.Drawing.Size(46, 20)
        Me.txtSAP.TabIndex = 6
        Me.txtSAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMatGrp
        '
        Me.lblMatGrp.AutoSize = True
        Me.lblMatGrp.Location = New System.Drawing.Point(27, 120)
        Me.lblMatGrp.Name = "lblMatGrp"
        Me.lblMatGrp.Size = New System.Drawing.Size(76, 13)
        Me.lblMatGrp.TabIndex = 5
        Me.lblMatGrp.Text = "Material Group"
        '
        'lblVCode
        '
        Me.lblVCode.AutoSize = True
        Me.lblVCode.Location = New System.Drawing.Point(27, 97)
        Me.lblVCode.Name = "lblVCode"
        Me.lblVCode.Size = New System.Drawing.Size(69, 13)
        Me.lblVCode.TabIndex = 4
        Me.lblVCode.Text = "Vendor Code"
        '
        'lblPlant
        '
        Me.lblPlant.AutoSize = True
        Me.lblPlant.Location = New System.Drawing.Point(27, 74)
        Me.lblPlant.Name = "lblPlant"
        Me.lblPlant.Size = New System.Drawing.Size(31, 13)
        Me.lblPlant.TabIndex = 3
        Me.lblPlant.Text = "Plant"
        '
        'lblLegEnt
        '
        Me.lblLegEnt.AutoSize = True
        Me.lblLegEnt.Location = New System.Drawing.Point(27, 51)
        Me.lblLegEnt.Name = "lblLegEnt"
        Me.lblLegEnt.Size = New System.Drawing.Size(55, 13)
        Me.lblLegEnt.TabIndex = 2
        Me.lblLegEnt.Text = "Legal Ent."
        '
        'lblSAP
        '
        Me.lblSAP.AutoSize = True
        Me.lblSAP.Location = New System.Drawing.Point(27, 28)
        Me.lblSAP.Name = "lblSAP"
        Me.lblSAP.Size = New System.Drawing.Size(49, 13)
        Me.lblSAP.TabIndex = 1
        Me.lblSAP.Text = "SAP Box"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.ToolStrip1)
        Me.GroupBox2.Controls.Add(Me.grdRules)
        Me.GroupBox2.Location = New System.Drawing.Point(1, 152)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(749, 348)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'grdRules
        '
        Me.grdRules.AllowUserToAddRows = False
        Me.grdRules.AllowUserToDeleteRows = False
        Me.grdRules.AllowUserToResizeColumns = False
        Me.grdRules.AllowUserToResizeRows = False
        Me.grdRules.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdRules.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdRules.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdRules.Location = New System.Drawing.Point(6, 44)
        Me.grdRules.Name = "grdRules"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdRules.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdRules.Size = New System.Drawing.Size(737, 298)
        Me.grdRules.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 16)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(743, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Delete rule"
        '
        'txtMatCategory
        '
        Me.txtMatCategory.Location = New System.Drawing.Point(496, 94)
        Me.txtMatCategory.Name = "txtMatCategory"
        Me.txtMatCategory.Size = New System.Drawing.Size(46, 20)
        Me.txtMatCategory.TabIndex = 23
        Me.txtMatCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMatCategory
        '
        Me.lblMatCategory.AutoSize = True
        Me.lblMatCategory.Location = New System.Drawing.Point(414, 98)
        Me.lblMatCategory.Name = "lblMatCategory"
        Me.lblMatCategory.Size = New System.Drawing.Size(70, 13)
        Me.lblMatCategory.TabIndex = 22
        Me.lblMatCategory.Text = "Mat Category"
        '
        'frm079
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 531)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm079"
        Me.Text = "frm079"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdRules, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdRules As System.Windows.Forms.DataGridView
    Friend WithEvents txtMatGrp As System.Windows.Forms.TextBox
    Friend WithEvents txtVCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPlant As System.Windows.Forms.TextBox
    Friend WithEvents txtLegEnt As System.Windows.Forms.TextBox
    Friend WithEvents txtSAP As System.Windows.Forms.TextBox
    Friend WithEvents lblMatGrp As System.Windows.Forms.Label
    Friend WithEvents lblVCode As System.Windows.Forms.Label
    Friend WithEvents lblPlant As System.Windows.Forms.Label
    Friend WithEvents lblLegEnt As System.Windows.Forms.Label
    Friend WithEvents lblSAP As System.Windows.Forms.Label
    Friend WithEvents txtNCMCode As System.Windows.Forms.TextBox
    Friend WithEvents txtMOrigin As System.Windows.Forms.TextBox
    Friend WithEvents txtMUsage As System.Windows.Forms.TextBox
    Friend WithEvents txtTCode As System.Windows.Forms.TextBox
    Friend WithEvents lblNCMCode As System.Windows.Forms.Label
    Friend WithEvents lblMOrigin As System.Windows.Forms.Label
    Friend WithEvents lblMUsage As System.Windows.Forms.Label
    Friend WithEvents lblTCode As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblVName As System.Windows.Forms.Label
    Friend WithEvents BS As System.Windows.Forms.BindingSource
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtMatCategory As System.Windows.Forms.TextBox
    Friend WithEvents lblMatCategory As System.Windows.Forms.Label
End Class
