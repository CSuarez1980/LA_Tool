<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm001
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm001))
        Me.dtgCatalogos = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.lblSAPBox = New System.Windows.Forms.ToolStripLabel
        Me.cboSAPBox = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.cboTipoDescarga = New System.Windows.Forms.ToolStripComboBox
        Me.txtPwr = New System.Windows.Forms.TextBox
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtInicioCompras = New System.Windows.Forms.DateTimePicker
        Me.txtFinCompras = New System.Windows.Forms.DateTimePicker
        Me.btnSapProcess = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        CType(Me.dtgCatalogos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgCatalogos
        '
        Me.dtgCatalogos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgCatalogos.Location = New System.Drawing.Point(8, 63)
        Me.dtgCatalogos.Name = "dtgCatalogos"
        Me.dtgCatalogos.Size = New System.Drawing.Size(905, 369)
        Me.dtgCatalogos.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSapProcess, Me.ToolStripButton2, Me.ToolStripButton1, Me.ToolStripButton3, Me.lblSAPBox, Me.cboSAPBox, Me.ToolStripLabel1, Me.cboTipoDescarga})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(923, 31)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'lblSAPBox
        '
        Me.lblSAPBox.Name = "lblSAPBox"
        Me.lblSAPBox.Size = New System.Drawing.Size(51, 28)
        Me.lblSAPBox.Text = "SAP Box:"
        '
        'cboSAPBox
        '
        Me.cboSAPBox.AutoSize = False
        Me.cboSAPBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSAPBox.Name = "cboSAPBox"
        Me.cboSAPBox.Size = New System.Drawing.Size(150, 21)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(58, 28)
        Me.ToolStripLabel1.Text = "Download:"
        '
        'cboTipoDescarga
        '
        Me.cboTipoDescarga.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoDescarga.Name = "cboTipoDescarga"
        Me.cboTipoDescarga.Size = New System.Drawing.Size(121, 31)
        '
        'txtPwr
        '
        Me.txtPwr.Location = New System.Drawing.Point(201, 34)
        Me.txtPwr.Name = "txtPwr"
        Me.txtPwr.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwr.Size = New System.Drawing.Size(145, 20)
        Me.txtPwr.TabIndex = 16
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(32, 34)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(100, 20)
        Me.txtUser.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(148, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "T#"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(571, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(16, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "to"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(395, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Created from"
        '
        'txtInicioCompras
        '
        Me.txtInicioCompras.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtInicioCompras.Location = New System.Drawing.Point(465, 36)
        Me.txtInicioCompras.Name = "txtInicioCompras"
        Me.txtInicioCompras.Size = New System.Drawing.Size(100, 20)
        Me.txtInicioCompras.TabIndex = 21
        '
        'txtFinCompras
        '
        Me.txtFinCompras.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFinCompras.Location = New System.Drawing.Point(591, 35)
        Me.txtFinCompras.Name = "txtFinCompras"
        Me.txtFinCompras.Size = New System.Drawing.Size(100, 20)
        Me.txtFinCompras.TabIndex = 22
        '
        'btnSapProcess
        '
        Me.btnSapProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSapProcess.Image = CType(resources.GetObject("btnSapProcess.Image"), System.Drawing.Image)
        Me.btnSapProcess.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSapProcess.Name = "btnSapProcess"
        Me.btnSapProcess.Size = New System.Drawing.Size(28, 28)
        Me.btnSapProcess.Text = "ToolStripButton1"
        Me.btnSapProcess.ToolTipText = "Descargar Información de SAP"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        Me.ToolStripButton2.ToolTipText = "Exportar catalogos"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Buscar archivo para actualizar"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButton3.Text = "ToolStripButton3"
        Me.ToolStripButton3.ToolTipText = "Correr script para actualizar"
        '
        'Frm001
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(923, 447)
        Me.Controls.Add(Me.txtFinCompras)
        Me.Controls.Add(Me.txtInicioCompras)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPwr)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.dtgCatalogos)
        Me.Name = "Frm001"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[001] Update Catalogs"
        CType(Me.dtgCatalogos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgCatalogos As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSapProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtPwr As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblSAPBox As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cboSAPBox As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents txtInicioCompras As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtFinCompras As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cboTipoDescarga As System.Windows.Forms.ToolStripComboBox
End Class
