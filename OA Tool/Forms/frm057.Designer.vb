<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm057
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm057))
        Me.grpVendor = New System.Windows.Forms.GroupBox
        Me.lblTax = New System.Windows.Forms.Label
        Me.txtTax = New System.Windows.Forms.TextBox
        Me.chkCatalog = New System.Windows.Forms.CheckBox
        Me.chkSup_Portal = New System.Windows.Forms.CheckBox
        Me.lblContry = New System.Windows.Forms.Label
        Me.txtCountry = New System.Windows.Forms.TextBox
        Me.lblName = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.lblCode = New System.Windows.Forms.Label
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.tlbHerramientas = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.btn = New System.Windows.Forms.Button
        Me.grpVendor.SuspendLayout()
        Me.tlbHerramientas.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpVendor
        '
        Me.grpVendor.Controls.Add(Me.btn)
        Me.grpVendor.Controls.Add(Me.lblTax)
        Me.grpVendor.Controls.Add(Me.txtTax)
        Me.grpVendor.Controls.Add(Me.chkCatalog)
        Me.grpVendor.Controls.Add(Me.chkSup_Portal)
        Me.grpVendor.Controls.Add(Me.lblContry)
        Me.grpVendor.Controls.Add(Me.txtCountry)
        Me.grpVendor.Controls.Add(Me.lblName)
        Me.grpVendor.Controls.Add(Me.txtName)
        Me.grpVendor.Controls.Add(Me.lblCode)
        Me.grpVendor.Controls.Add(Me.txtCode)
        Me.grpVendor.Location = New System.Drawing.Point(3, 39)
        Me.grpVendor.Name = "grpVendor"
        Me.grpVendor.Size = New System.Drawing.Size(351, 180)
        Me.grpVendor.TabIndex = 0
        Me.grpVendor.TabStop = False
        Me.grpVendor.Text = "Tax configuration"
        '
        'lblTax
        '
        Me.lblTax.AutoSize = True
        Me.lblTax.Location = New System.Drawing.Point(20, 104)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(53, 13)
        Me.lblTax.TabIndex = 9
        Me.lblTax.Text = "Tax Code"
        '
        'txtTax
        '
        Me.txtTax.Location = New System.Drawing.Point(86, 100)
        Me.txtTax.Name = "txtTax"
        Me.txtTax.Size = New System.Drawing.Size(37, 20)
        Me.txtTax.TabIndex = 8
        '
        'chkCatalog
        '
        Me.chkCatalog.AutoSize = True
        Me.chkCatalog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkCatalog.Location = New System.Drawing.Point(21, 154)
        Me.chkCatalog.Name = "chkCatalog"
        Me.chkCatalog.Size = New System.Drawing.Size(80, 17)
        Me.chkCatalog.TabIndex = 7
        Me.chkCatalog.Text = "Catalog      "
        Me.chkCatalog.UseVisualStyleBackColor = True
        '
        'chkSup_Portal
        '
        Me.chkSup_Portal.AutoSize = True
        Me.chkSup_Portal.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSup_Portal.Location = New System.Drawing.Point(20, 131)
        Me.chkSup_Portal.Name = "chkSup_Portal"
        Me.chkSup_Portal.Size = New System.Drawing.Size(81, 17)
        Me.chkSup_Portal.TabIndex = 6
        Me.chkSup_Portal.Text = "Sup. Portal "
        Me.chkSup_Portal.UseVisualStyleBackColor = True
        '
        'lblContry
        '
        Me.lblContry.AutoSize = True
        Me.lblContry.Location = New System.Drawing.Point(20, 78)
        Me.lblContry.Name = "lblContry"
        Me.lblContry.Size = New System.Drawing.Size(43, 13)
        Me.lblContry.TabIndex = 5
        Me.lblContry.Text = "Country"
        '
        'txtCountry
        '
        Me.txtCountry.Location = New System.Drawing.Point(86, 74)
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(37, 20)
        Me.txtCountry.TabIndex = 4
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(20, 52)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(35, 13)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = "Name"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(86, 48)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(251, 20)
        Me.txtName.TabIndex = 2
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Location = New System.Drawing.Point(20, 26)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 13)
        Me.lblCode.TabIndex = 1
        Me.lblCode.Text = "Code"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(86, 22)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(100, 20)
        Me.txtCode.TabIndex = 0
        '
        'tlbHerramientas
        '
        Me.tlbHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.tlbHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tlbHerramientas.Name = "tlbHerramientas"
        Me.tlbHerramientas.Size = New System.Drawing.Size(359, 39)
        Me.tlbHerramientas.TabIndex = 1
        Me.tlbHerramientas.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Save"
        '
        'btn
        '
        Me.btn.Image = CType(resources.GetObject("btn.Image"), System.Drawing.Image)
        Me.btn.Location = New System.Drawing.Point(193, 20)
        Me.btn.Name = "btn"
        Me.btn.Size = New System.Drawing.Size(31, 23)
        Me.btn.TabIndex = 10
        Me.btn.UseVisualStyleBackColor = True
        '
        'frm057
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(359, 228)
        Me.Controls.Add(Me.tlbHerramientas)
        Me.Controls.Add(Me.grpVendor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm057"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[057] Tax Configuration"
        Me.grpVendor.ResumeLayout(False)
        Me.grpVendor.PerformLayout()
        Me.tlbHerramientas.ResumeLayout(False)
        Me.tlbHerramientas.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpVendor As System.Windows.Forms.GroupBox
    Friend WithEvents tlbHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkCatalog As System.Windows.Forms.CheckBox
    Friend WithEvents chkSup_Portal As System.Windows.Forms.CheckBox
    Friend WithEvents lblContry As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents lblTax As System.Windows.Forms.Label
    Friend WithEvents txtTax As System.Windows.Forms.TextBox
    Friend WithEvents btn As System.Windows.Forms.Button
End Class
