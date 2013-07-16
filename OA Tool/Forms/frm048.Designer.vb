<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm048
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm048))
        Me.tblHerramientas = New System.Windows.Forms.ToolStrip
        Me.cmdUblock = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripSplitButton
        Me.SaveConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dtgBI = New System.Windows.Forms.DataGridView
        Me.grpSAPBox = New System.Windows.Forms.GroupBox
        Me.cboSAPBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblVariant = New System.Windows.Forms.Label
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblTotal = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdFiltrar = New System.Windows.Forms.Button
        Me.txtBuscar = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.tblHerramientas.SuspendLayout()
        CType(Me.dtgBI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSAPBox.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblHerramientas
        '
        Me.tblHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdUblock, Me.ToolStripSeparator1, Me.cmdExcel, Me.ToolStripButton1})
        Me.tblHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tblHerramientas.Name = "tblHerramientas"
        Me.tblHerramientas.Size = New System.Drawing.Size(844, 39)
        Me.tblHerramientas.TabIndex = 2
        Me.tblHerramientas.Text = "ToolStrip1"
        '
        'cmdUblock
        '
        Me.cmdUblock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdUblock.Image = CType(resources.GetObject("cmdUblock.Image"), System.Drawing.Image)
        Me.cmdUblock.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdUblock.Name = "cmdUblock"
        Me.cmdUblock.Size = New System.Drawing.Size(36, 36)
        Me.cmdUblock.Text = "ToolStripButton1"
        Me.cmdUblock.ToolTipText = "Unblock"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdExcel
        '
        Me.cmdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExcel.Image = CType(resources.GetObject("cmdExcel.Image"), System.Drawing.Image)
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(36, 36)
        Me.cmdExcel.ToolTipText = "Export to Excel"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveConfigurationToolStripMenuItem})
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(48, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'SaveConfigurationToolStripMenuItem
        '
        Me.SaveConfigurationToolStripMenuItem.Image = CType(resources.GetObject("SaveConfigurationToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveConfigurationToolStripMenuItem.Name = "SaveConfigurationToolStripMenuItem"
        Me.SaveConfigurationToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.SaveConfigurationToolStripMenuItem.Text = "Save columns position"
        '
        'dtgBI
        '
        Me.dtgBI.AllowUserToAddRows = False
        Me.dtgBI.AllowUserToOrderColumns = True
        Me.dtgBI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgBI.Location = New System.Drawing.Point(12, 121)
        Me.dtgBI.Name = "dtgBI"
        Me.dtgBI.Size = New System.Drawing.Size(820, 385)
        Me.dtgBI.TabIndex = 3
        '
        'grpSAPBox
        '
        Me.grpSAPBox.Controls.Add(Me.cboSAPBox)
        Me.grpSAPBox.Controls.Add(Me.Label1)
        Me.grpSAPBox.Location = New System.Drawing.Point(12, 42)
        Me.grpSAPBox.Name = "grpSAPBox"
        Me.grpSAPBox.Size = New System.Drawing.Size(276, 51)
        Me.grpSAPBox.TabIndex = 31
        Me.grpSAPBox.TabStop = False
        Me.grpSAPBox.Text = "SAP Box"
        '
        'cboSAPBox
        '
        Me.cboSAPBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSAPBox.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboSAPBox.FormattingEnabled = True
        Me.cboSAPBox.Location = New System.Drawing.Point(64, 19)
        Me.cboSAPBox.Name = "cboSAPBox"
        Me.cboSAPBox.Size = New System.Drawing.Size(191, 21)
        Me.cboSAPBox.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "SAP Box"
        '
        'lblVariant
        '
        Me.lblVariant.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVariant.ForeColor = System.Drawing.Color.Navy
        Me.lblVariant.Location = New System.Drawing.Point(15, 96)
        Me.lblVariant.Name = "lblVariant"
        Me.lblVariant.Size = New System.Drawing.Size(555, 22)
        Me.lblVariant.TabIndex = 32
        Me.lblVariant.Text = "Variant Name"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTotal})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 521)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(844, 22)
        Me.StatusStrip1.TabIndex = 33
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblTotal
        '
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(137, 17)
        Me.lblTotal.Text = "Total Blocked Invoices: 0"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdFiltrar)
        Me.GroupBox2.Controls.Add(Me.txtBuscar)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(294, 42)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(276, 51)
        Me.GroupBox2.TabIndex = 39
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Filter"
        '
        'cmdFiltrar
        '
        Me.cmdFiltrar.Image = CType(resources.GetObject("cmdFiltrar.Image"), System.Drawing.Image)
        Me.cmdFiltrar.Location = New System.Drawing.Point(232, 15)
        Me.cmdFiltrar.Name = "cmdFiltrar"
        Me.cmdFiltrar.Size = New System.Drawing.Size(31, 25)
        Me.cmdFiltrar.TabIndex = 2
        Me.cmdFiltrar.UseVisualStyleBackColor = True
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(56, 18)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(174, 20)
        Me.txtBuscar.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Search:"
        '
        'frm048
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(844, 543)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.lblVariant)
        Me.Controls.Add(Me.grpSAPBox)
        Me.Controls.Add(Me.dtgBI)
        Me.Controls.Add(Me.tblHerramientas)
        Me.Name = "frm048"
        Me.Text = "[048] Blocked Invoice Report from ZMR0"
        Me.tblHerramientas.ResumeLayout(False)
        Me.tblHerramientas.PerformLayout()
        CType(Me.dtgBI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSAPBox.ResumeLayout(False)
        Me.grpSAPBox.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tblHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents dtgBI As System.Windows.Forms.DataGridView
    Friend WithEvents grpSAPBox As System.Windows.Forms.GroupBox
    Friend WithEvents cboSAPBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblVariant As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblTotal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmdUblock As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdFiltrar As System.Windows.Forms.Button
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SaveConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
