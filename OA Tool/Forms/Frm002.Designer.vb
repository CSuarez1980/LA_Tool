<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm002
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm002))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkSourceList = New System.Windows.Forms.CheckBox
        Me.chkSincServer = New System.Windows.Forms.CheckBox
        Me.chkActualizarDetalleContrato = New System.Windows.Forms.CheckBox
        Me.chkActualizarHeaderContratos = New System.Windows.Forms.CheckBox
        Me.chkDetalleContrato = New System.Windows.Forms.CheckBox
        Me.chkHeaderContrato = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkHeaderCatalogos = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFinCompras = New System.Windows.Forms.TextBox
        Me.txtInicioCompras = New System.Windows.Forms.TextBox
        Me.chkDetalleCompras = New System.Windows.Forms.CheckBox
        Me.chkHeaderCompras = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkActualizarMasterData = New System.Windows.Forms.CheckBox
        Me.chkMasterData = New System.Windows.Forms.CheckBox
        Me.chkActualizarActualOTD = New System.Windows.Forms.CheckBox
        Me.chkActualizarManufacter = New System.Windows.Forms.CheckBox
        Me.chkActualizarProveedores = New System.Windows.Forms.CheckBox
        Me.chkActualOTD = New System.Windows.Forms.CheckBox
        Me.chkManufacter = New System.Windows.Forms.CheckBox
        Me.chkProveedores = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.txtPwr = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.chkKathia = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txt2 = New System.Windows.Forms.TextBox
        Me.txt1 = New System.Windows.Forms.TextBox
        Me.btnSapProcess = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSapProcess, Me.ToolStripButton2, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(494, 39)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkSourceList)
        Me.GroupBox1.Controls.Add(Me.chkSincServer)
        Me.GroupBox1.Controls.Add(Me.chkActualizarDetalleContrato)
        Me.GroupBox1.Controls.Add(Me.chkActualizarHeaderContratos)
        Me.GroupBox1.Controls.Add(Me.chkDetalleContrato)
        Me.GroupBox1.Controls.Add(Me.chkHeaderContrato)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 79)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(181, 149)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Contratos"
        '
        'chkSourceList
        '
        Me.chkSourceList.AutoSize = True
        Me.chkSourceList.Location = New System.Drawing.Point(6, 129)
        Me.chkSourceList.Name = "chkSourceList"
        Me.chkSourceList.Size = New System.Drawing.Size(106, 17)
        Me.chkSourceList.TabIndex = 14
        Me.chkSourceList.Text = "Bajar Source List"
        Me.chkSourceList.UseVisualStyleBackColor = True
        '
        'chkSincServer
        '
        Me.chkSincServer.AutoSize = True
        Me.chkSincServer.Location = New System.Drawing.Point(6, 107)
        Me.chkSincServer.Name = "chkSincServer"
        Me.chkSincServer.Size = New System.Drawing.Size(108, 17)
        Me.chkSincServer.TabIndex = 7
        Me.chkSincServer.Text = "Sinc. SQL Server"
        Me.chkSincServer.UseVisualStyleBackColor = True
        '
        'chkActualizarDetalleContrato
        '
        Me.chkActualizarDetalleContrato.AutoSize = True
        Me.chkActualizarDetalleContrato.Location = New System.Drawing.Point(39, 83)
        Me.chkActualizarDetalleContrato.Name = "chkActualizarDetalleContrato"
        Me.chkActualizarDetalleContrato.Size = New System.Drawing.Size(72, 17)
        Me.chkActualizarDetalleContrato.TabIndex = 6
        Me.chkActualizarDetalleContrato.Text = "Actualizar"
        Me.chkActualizarDetalleContrato.UseVisualStyleBackColor = True
        '
        'chkActualizarHeaderContratos
        '
        Me.chkActualizarHeaderContratos.AutoSize = True
        Me.chkActualizarHeaderContratos.Location = New System.Drawing.Point(39, 42)
        Me.chkActualizarHeaderContratos.Name = "chkActualizarHeaderContratos"
        Me.chkActualizarHeaderContratos.Size = New System.Drawing.Size(72, 17)
        Me.chkActualizarHeaderContratos.TabIndex = 5
        Me.chkActualizarHeaderContratos.Text = "Actualizar"
        Me.chkActualizarHeaderContratos.UseVisualStyleBackColor = True
        '
        'chkDetalleContrato
        '
        Me.chkDetalleContrato.AutoSize = True
        Me.chkDetalleContrato.Location = New System.Drawing.Point(25, 67)
        Me.chkDetalleContrato.Name = "chkDetalleContrato"
        Me.chkDetalleContrato.Size = New System.Drawing.Size(122, 17)
        Me.chkDetalleContrato.TabIndex = 4
        Me.chkDetalleContrato.Text = "Detalle de Contratos"
        Me.chkDetalleContrato.UseVisualStyleBackColor = True
        '
        'chkHeaderContrato
        '
        Me.chkHeaderContrato.AutoSize = True
        Me.chkHeaderContrato.Location = New System.Drawing.Point(25, 24)
        Me.chkHeaderContrato.Name = "chkHeaderContrato"
        Me.chkHeaderContrato.Size = New System.Drawing.Size(124, 17)
        Me.chkHeaderContrato.TabIndex = 3
        Me.chkHeaderContrato.Text = "Header de Contratos"
        Me.chkHeaderContrato.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkHeaderCatalogos)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtFinCompras)
        Me.GroupBox2.Controls.Add(Me.txtInicioCompras)
        Me.GroupBox2.Controls.Add(Me.chkDetalleCompras)
        Me.GroupBox2.Controls.Add(Me.chkHeaderCompras)
        Me.GroupBox2.Location = New System.Drawing.Point(193, 79)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(286, 149)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Compras"
        '
        'chkHeaderCatalogos
        '
        Me.chkHeaderCatalogos.AutoSize = True
        Me.chkHeaderCatalogos.Location = New System.Drawing.Point(30, 90)
        Me.chkHeaderCatalogos.Name = "chkHeaderCatalogos"
        Me.chkHeaderCatalogos.Size = New System.Drawing.Size(111, 17)
        Me.chkHeaderCatalogos.TabIndex = 13
        Me.chkHeaderCatalogos.Text = "Header Catalogos"
        Me.chkHeaderCatalogos.UseVisualStyleBackColor = True
        Me.chkHeaderCatalogos.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(153, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "A"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Creada"
        '
        'txtFinCompras
        '
        Me.txtFinCompras.Location = New System.Drawing.Point(173, 20)
        Me.txtFinCompras.Name = "txtFinCompras"
        Me.txtFinCompras.Size = New System.Drawing.Size(100, 20)
        Me.txtFinCompras.TabIndex = 8
        '
        'txtInicioCompras
        '
        Me.txtInicioCompras.Location = New System.Drawing.Point(47, 20)
        Me.txtInicioCompras.Name = "txtInicioCompras"
        Me.txtInicioCompras.Size = New System.Drawing.Size(100, 20)
        Me.txtInicioCompras.TabIndex = 7
        '
        'chkDetalleCompras
        '
        Me.chkDetalleCompras.AutoSize = True
        Me.chkDetalleCompras.Location = New System.Drawing.Point(30, 67)
        Me.chkDetalleCompras.Name = "chkDetalleCompras"
        Me.chkDetalleCompras.Size = New System.Drawing.Size(118, 17)
        Me.chkDetalleCompras.TabIndex = 6
        Me.chkDetalleCompras.Text = "Detalle de Compras"
        Me.chkDetalleCompras.UseVisualStyleBackColor = True
        '
        'chkHeaderCompras
        '
        Me.chkHeaderCompras.AutoSize = True
        Me.chkHeaderCompras.Location = New System.Drawing.Point(30, 44)
        Me.chkHeaderCompras.Name = "chkHeaderCompras"
        Me.chkHeaderCompras.Size = New System.Drawing.Size(120, 17)
        Me.chkHeaderCompras.TabIndex = 5
        Me.chkHeaderCompras.Text = "Header de Compras"
        Me.chkHeaderCompras.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkActualizarMasterData)
        Me.GroupBox3.Controls.Add(Me.chkMasterData)
        Me.GroupBox3.Controls.Add(Me.chkActualizarActualOTD)
        Me.GroupBox3.Controls.Add(Me.chkActualizarManufacter)
        Me.GroupBox3.Controls.Add(Me.chkActualizarProveedores)
        Me.GroupBox3.Controls.Add(Me.chkActualOTD)
        Me.GroupBox3.Controls.Add(Me.chkManufacter)
        Me.GroupBox3.Controls.Add(Me.chkProveedores)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 242)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(467, 70)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Varios"
        '
        'chkActualizarMasterData
        '
        Me.chkActualizarMasterData.AutoSize = True
        Me.chkActualizarMasterData.Location = New System.Drawing.Point(364, 42)
        Me.chkActualizarMasterData.Name = "chkActualizarMasterData"
        Me.chkActualizarMasterData.Size = New System.Drawing.Size(72, 17)
        Me.chkActualizarMasterData.TabIndex = 12
        Me.chkActualizarMasterData.Text = "Actualizar"
        Me.chkActualizarMasterData.UseVisualStyleBackColor = True
        '
        'chkMasterData
        '
        Me.chkMasterData.AutoSize = True
        Me.chkMasterData.Location = New System.Drawing.Point(354, 19)
        Me.chkMasterData.Name = "chkMasterData"
        Me.chkMasterData.Size = New System.Drawing.Size(84, 17)
        Me.chkMasterData.TabIndex = 11
        Me.chkMasterData.Text = "Master Data"
        Me.chkMasterData.UseVisualStyleBackColor = True
        '
        'chkActualizarActualOTD
        '
        Me.chkActualizarActualOTD.AutoSize = True
        Me.chkActualizarActualOTD.Location = New System.Drawing.Point(245, 42)
        Me.chkActualizarActualOTD.Name = "chkActualizarActualOTD"
        Me.chkActualizarActualOTD.Size = New System.Drawing.Size(72, 17)
        Me.chkActualizarActualOTD.TabIndex = 10
        Me.chkActualizarActualOTD.Text = "Actualizar"
        Me.chkActualizarActualOTD.UseVisualStyleBackColor = True
        '
        'chkActualizarManufacter
        '
        Me.chkActualizarManufacter.AutoSize = True
        Me.chkActualizarManufacter.Location = New System.Drawing.Point(134, 42)
        Me.chkActualizarManufacter.Name = "chkActualizarManufacter"
        Me.chkActualizarManufacter.Size = New System.Drawing.Size(72, 17)
        Me.chkActualizarManufacter.TabIndex = 9
        Me.chkActualizarManufacter.Text = "Actualizar"
        Me.chkActualizarManufacter.UseVisualStyleBackColor = True
        '
        'chkActualizarProveedores
        '
        Me.chkActualizarProveedores.AutoSize = True
        Me.chkActualizarProveedores.Location = New System.Drawing.Point(24, 42)
        Me.chkActualizarProveedores.Name = "chkActualizarProveedores"
        Me.chkActualizarProveedores.Size = New System.Drawing.Size(72, 17)
        Me.chkActualizarProveedores.TabIndex = 8
        Me.chkActualizarProveedores.Text = "Actualizar"
        Me.chkActualizarProveedores.UseVisualStyleBackColor = True
        '
        'chkActualOTD
        '
        Me.chkActualOTD.AutoSize = True
        Me.chkActualOTD.Location = New System.Drawing.Point(228, 19)
        Me.chkActualOTD.Name = "chkActualOTD"
        Me.chkActualOTD.Size = New System.Drawing.Size(82, 17)
        Me.chkActualOTD.TabIndex = 7
        Me.chkActualOTD.Text = "Actual OTD"
        Me.chkActualOTD.UseVisualStyleBackColor = True
        '
        'chkManufacter
        '
        Me.chkManufacter.AutoSize = True
        Me.chkManufacter.Location = New System.Drawing.Point(118, 19)
        Me.chkManufacter.Name = "chkManufacter"
        Me.chkManufacter.Size = New System.Drawing.Size(85, 17)
        Me.chkManufacter.TabIndex = 6
        Me.chkManufacter.Text = "Manufacters"
        Me.chkManufacter.UseVisualStyleBackColor = True
        '
        'chkProveedores
        '
        Me.chkProveedores.AutoSize = True
        Me.chkProveedores.Location = New System.Drawing.Point(10, 19)
        Me.chkProveedores.Name = "chkProveedores"
        Me.chkProveedores.Size = New System.Drawing.Size(86, 17)
        Me.chkProveedores.TabIndex = 5
        Me.chkProveedores.Text = "Proveedores"
        Me.chkProveedores.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(61, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "T#"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(204, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Password"
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(88, 53)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(100, 20)
        Me.txtUser.TabIndex = 11
        '
        'txtPwr
        '
        Me.txtPwr.Location = New System.Drawing.Point(257, 53)
        Me.txtPwr.Name = "txtPwr"
        Me.txtPwr.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwr.Size = New System.Drawing.Size(145, 20)
        Me.txtPwr.TabIndex = 12
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.chkKathia)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txt2)
        Me.GroupBox4.Controls.Add(Me.txt1)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 324)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(467, 90)
        Me.GroupBox4.TabIndex = 13
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Reporte OTD"
        '
        'chkKathia
        '
        Me.chkKathia.AutoSize = True
        Me.chkKathia.Location = New System.Drawing.Point(34, 26)
        Me.chkKathia.Name = "chkKathia"
        Me.chkKathia.Size = New System.Drawing.Size(71, 17)
        Me.chkKathia.TabIndex = 15
        Me.chkKathia.Text = "Bajar Info"
        Me.chkKathia.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(203, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "A"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(56, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Creada"
        '
        'txt2
        '
        Me.txt2.Location = New System.Drawing.Point(223, 49)
        Me.txt2.Name = "txt2"
        Me.txt2.Size = New System.Drawing.Size(100, 20)
        Me.txt2.TabIndex = 12
        '
        'txt1
        '
        Me.txt1.Location = New System.Drawing.Point(97, 49)
        Me.txt1.Name = "txt1"
        Me.txt1.Size = New System.Drawing.Size(100, 20)
        Me.txt1.TabIndex = 11
        '
        'btnSapProcess
        '
        Me.btnSapProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSapProcess.Image = CType(resources.GetObject("btnSapProcess.Image"), System.Drawing.Image)
        Me.btnSapProcess.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSapProcess.Name = "btnSapProcess"
        Me.btnSapProcess.Size = New System.Drawing.Size(36, 36)
        Me.btnSapProcess.Text = "ToolStripButton1"
        Me.btnSapProcess.ToolTipText = "Descargar Información de SAP"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.ToolTipText = "Exportar reporte de OTD"
        '
        'frm002
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(494, 426)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.txtPwr)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frm002"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[002] Descarga de SAP"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSapProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkDetalleContrato As System.Windows.Forms.CheckBox
    Friend WithEvents chkHeaderContrato As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFinCompras As System.Windows.Forms.TextBox
    Friend WithEvents txtInicioCompras As System.Windows.Forms.TextBox
    Friend WithEvents chkDetalleCompras As System.Windows.Forms.CheckBox
    Friend WithEvents chkHeaderCompras As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkActualOTD As System.Windows.Forms.CheckBox
    Friend WithEvents chkManufacter As System.Windows.Forms.CheckBox
    Friend WithEvents chkProveedores As System.Windows.Forms.CheckBox
    Friend WithEvents chkActualizarHeaderContratos As System.Windows.Forms.CheckBox
    Friend WithEvents chkActualizarDetalleContrato As System.Windows.Forms.CheckBox
    Friend WithEvents chkActualizarMasterData As System.Windows.Forms.CheckBox
    Friend WithEvents chkMasterData As System.Windows.Forms.CheckBox
    Friend WithEvents chkActualizarActualOTD As System.Windows.Forms.CheckBox
    Friend WithEvents chkActualizarManufacter As System.Windows.Forms.CheckBox
    Friend WithEvents chkActualizarProveedores As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtPwr As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt2 As System.Windows.Forms.TextBox
    Friend WithEvents txt1 As System.Windows.Forms.TextBox
    Friend WithEvents chkKathia As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkSincServer As System.Windows.Forms.CheckBox
    Friend WithEvents chkHeaderCatalogos As System.Windows.Forms.CheckBox
    Friend WithEvents chkSourceList As System.Windows.Forms.CheckBox
End Class
