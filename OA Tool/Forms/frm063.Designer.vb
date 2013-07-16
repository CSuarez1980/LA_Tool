<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm063
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm063))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grpOutlineAgreement = New System.Windows.Forms.GroupBox
        Me.txtVendorName = New System.Windows.Forms.TextBox
        Me.chkForceInclude = New System.Windows.Forms.CheckBox
        Me.txtCurrency = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtValEnd = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtValStart = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPGrp = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPOrg = New System.Windows.Forms.TextBox
        Me.lblPOrg = New System.Windows.Forms.Label
        Me.txtPlant = New System.Windows.Forms.TextBox
        Me.txtVendor = New System.Windows.Forms.TextBox
        Me.txtOA = New System.Windows.Forms.TextBox
        Me.lblPlant = New System.Windows.Forms.Label
        Me.lblVendor = New System.Windows.Forms.Label
        Me.lblOA = New System.Windows.Forms.Label
        Me.BS = New System.Windows.Forms.BindingSource(Me.components)
        Me.tlbHerramientas = New System.Windows.Forms.ToolStrip
        Me.cmdRun = New System.Windows.Forms.ToolStripButton
        Me.dtgMateriales = New System.Windows.Forms.DataGridView
        Me.grpOutlineAgreement.SuspendLayout()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlbHerramientas.SuspendLayout()
        CType(Me.dtgMateriales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpOutlineAgreement
        '
        Me.grpOutlineAgreement.Controls.Add(Me.txtVendorName)
        Me.grpOutlineAgreement.Controls.Add(Me.chkForceInclude)
        Me.grpOutlineAgreement.Controls.Add(Me.txtCurrency)
        Me.grpOutlineAgreement.Controls.Add(Me.Label4)
        Me.grpOutlineAgreement.Controls.Add(Me.txtValEnd)
        Me.grpOutlineAgreement.Controls.Add(Me.Label2)
        Me.grpOutlineAgreement.Controls.Add(Me.txtValStart)
        Me.grpOutlineAgreement.Controls.Add(Me.Label3)
        Me.grpOutlineAgreement.Controls.Add(Me.txtPGrp)
        Me.grpOutlineAgreement.Controls.Add(Me.Label1)
        Me.grpOutlineAgreement.Controls.Add(Me.txtPOrg)
        Me.grpOutlineAgreement.Controls.Add(Me.lblPOrg)
        Me.grpOutlineAgreement.Controls.Add(Me.txtPlant)
        Me.grpOutlineAgreement.Controls.Add(Me.txtVendor)
        Me.grpOutlineAgreement.Controls.Add(Me.txtOA)
        Me.grpOutlineAgreement.Controls.Add(Me.lblPlant)
        Me.grpOutlineAgreement.Controls.Add(Me.lblVendor)
        Me.grpOutlineAgreement.Controls.Add(Me.lblOA)
        Me.grpOutlineAgreement.Location = New System.Drawing.Point(8, 42)
        Me.grpOutlineAgreement.Name = "grpOutlineAgreement"
        Me.grpOutlineAgreement.Size = New System.Drawing.Size(737, 103)
        Me.grpOutlineAgreement.TabIndex = 0
        Me.grpOutlineAgreement.TabStop = False
        Me.grpOutlineAgreement.Text = "Outline Agreement information"
        '
        'txtVendorName
        '
        Me.txtVendorName.Location = New System.Drawing.Point(143, 53)
        Me.txtVendorName.Name = "txtVendorName"
        Me.txtVendorName.ReadOnly = True
        Me.txtVendorName.Size = New System.Drawing.Size(237, 20)
        Me.txtVendorName.TabIndex = 17
        '
        'chkForceInclude
        '
        Me.chkForceInclude.AutoSize = True
        Me.chkForceInclude.Location = New System.Drawing.Point(558, 80)
        Me.chkForceInclude.Name = "chkForceInclude"
        Me.chkForceInclude.Size = New System.Drawing.Size(95, 17)
        Me.chkForceInclude.TabIndex = 16
        Me.chkForceInclude.Text = "Force inclution"
        Me.chkForceInclude.UseVisualStyleBackColor = True
        '
        'txtCurrency
        '
        Me.txtCurrency.Location = New System.Drawing.Point(453, 77)
        Me.txtCurrency.Name = "txtCurrency"
        Me.txtCurrency.ReadOnly = True
        Me.txtCurrency.Size = New System.Drawing.Size(51, 20)
        Me.txtCurrency.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(406, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Currency"
        '
        'txtValEnd
        '
        Me.txtValEnd.Location = New System.Drawing.Point(614, 53)
        Me.txtValEnd.Name = "txtValEnd"
        Me.txtValEnd.ReadOnly = True
        Me.txtValEnd.Size = New System.Drawing.Size(106, 20)
        Me.txtValEnd.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(555, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Val. End"
        '
        'txtValStart
        '
        Me.txtValStart.Location = New System.Drawing.Point(614, 28)
        Me.txtValStart.Name = "txtValStart"
        Me.txtValStart.ReadOnly = True
        Me.txtValStart.Size = New System.Drawing.Size(106, 20)
        Me.txtValStart.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(555, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Val. Start"
        '
        'txtPGrp
        '
        Me.txtPGrp.Location = New System.Drawing.Point(453, 53)
        Me.txtPGrp.Name = "txtPGrp"
        Me.txtPGrp.ReadOnly = True
        Me.txtPGrp.Size = New System.Drawing.Size(51, 20)
        Me.txtPGrp.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(406, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "P. Grp."
        '
        'txtPOrg
        '
        Me.txtPOrg.Location = New System.Drawing.Point(453, 28)
        Me.txtPOrg.Name = "txtPOrg"
        Me.txtPOrg.ReadOnly = True
        Me.txtPOrg.Size = New System.Drawing.Size(51, 20)
        Me.txtPOrg.TabIndex = 7
        '
        'lblPOrg
        '
        Me.lblPOrg.AutoSize = True
        Me.lblPOrg.Location = New System.Drawing.Point(406, 32)
        Me.lblPOrg.Name = "lblPOrg"
        Me.lblPOrg.Size = New System.Drawing.Size(40, 13)
        Me.lblPOrg.TabIndex = 6
        Me.lblPOrg.Text = "P. Org."
        '
        'txtPlant
        '
        Me.txtPlant.Location = New System.Drawing.Point(63, 77)
        Me.txtPlant.Name = "txtPlant"
        Me.txtPlant.ReadOnly = True
        Me.txtPlant.Size = New System.Drawing.Size(54, 20)
        Me.txtPlant.TabIndex = 5
        '
        'txtVendor
        '
        Me.txtVendor.Location = New System.Drawing.Point(63, 53)
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.ReadOnly = True
        Me.txtVendor.Size = New System.Drawing.Size(79, 20)
        Me.txtVendor.TabIndex = 4
        '
        'txtOA
        '
        Me.txtOA.Location = New System.Drawing.Point(63, 28)
        Me.txtOA.Name = "txtOA"
        Me.txtOA.Size = New System.Drawing.Size(79, 20)
        Me.txtOA.TabIndex = 3
        '
        'lblPlant
        '
        Me.lblPlant.AutoSize = True
        Me.lblPlant.Location = New System.Drawing.Point(16, 81)
        Me.lblPlant.Name = "lblPlant"
        Me.lblPlant.Size = New System.Drawing.Size(31, 13)
        Me.lblPlant.TabIndex = 2
        Me.lblPlant.Text = "Plant"
        '
        'lblVendor
        '
        Me.lblVendor.AutoSize = True
        Me.lblVendor.Location = New System.Drawing.Point(16, 57)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(41, 13)
        Me.lblVendor.TabIndex = 1
        Me.lblVendor.Text = "Vendor"
        '
        'lblOA
        '
        Me.lblOA.AutoSize = True
        Me.lblOA.Location = New System.Drawing.Point(16, 32)
        Me.lblOA.Name = "lblOA"
        Me.lblOA.Size = New System.Drawing.Size(44, 13)
        Me.lblOA.TabIndex = 0
        Me.lblOA.Text = "Number"
        '
        'tlbHerramientas
        '
        Me.tlbHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdRun})
        Me.tlbHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tlbHerramientas.Name = "tlbHerramientas"
        Me.tlbHerramientas.Size = New System.Drawing.Size(757, 39)
        Me.tlbHerramientas.TabIndex = 1
        Me.tlbHerramientas.Text = "ToolStrip1"
        '
        'cmdRun
        '
        Me.cmdRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRun.Image = CType(resources.GetObject("cmdRun.Image"), System.Drawing.Image)
        Me.cmdRun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRun.Name = "cmdRun"
        Me.cmdRun.Size = New System.Drawing.Size(36, 36)
        Me.cmdRun.Text = "ToolStripButton1"
        Me.cmdRun.ToolTipText = "Incluide materials"
        '
        'dtgMateriales
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgMateriales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dtgMateriales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dtgMateriales.DefaultCellStyle = DataGridViewCellStyle2
        Me.dtgMateriales.Enabled = False
        Me.dtgMateriales.Location = New System.Drawing.Point(10, 152)
        Me.dtgMateriales.Name = "dtgMateriales"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgMateriales.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dtgMateriales.Size = New System.Drawing.Size(735, 283)
        Me.dtgMateriales.TabIndex = 2
        '
        'frm063
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(757, 447)
        Me.Controls.Add(Me.dtgMateriales)
        Me.Controls.Add(Me.tlbHerramientas)
        Me.Controls.Add(Me.grpOutlineAgreement)
        Me.Name = "frm063"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[063] Include new materials to outline agreement"
        Me.grpOutlineAgreement.ResumeLayout(False)
        Me.grpOutlineAgreement.PerformLayout()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlbHerramientas.ResumeLayout(False)
        Me.tlbHerramientas.PerformLayout()
        CType(Me.dtgMateriales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpOutlineAgreement As System.Windows.Forms.GroupBox
    Friend WithEvents BS As System.Windows.Forms.BindingSource
    Friend WithEvents tlbHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdRun As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtPOrg As System.Windows.Forms.TextBox
    Friend WithEvents lblPOrg As System.Windows.Forms.Label
    Friend WithEvents txtPlant As System.Windows.Forms.TextBox
    Friend WithEvents txtVendor As System.Windows.Forms.TextBox
    Friend WithEvents txtOA As System.Windows.Forms.TextBox
    Friend WithEvents lblPlant As System.Windows.Forms.Label
    Friend WithEvents lblVendor As System.Windows.Forms.Label
    Friend WithEvents lblOA As System.Windows.Forms.Label
    Friend WithEvents dtgMateriales As System.Windows.Forms.DataGridView
    Friend WithEvents txtPGrp As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtValEnd As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtValStart As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCurrency As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkForceInclude As System.Windows.Forms.CheckBox
    Friend WithEvents txtVendorName As System.Windows.Forms.TextBox
End Class
