<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm032
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm032))
        Me.dtgRequisiciones = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboSAPBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboVariantes = New System.Windows.Forms.ComboBox
        Me.lblVariantes = New System.Windows.Forms.Label
        Me.ToolBar = New System.Windows.Forms.ToolStrip
        Me.cmdDowload = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdDesbloquear = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdImprimirPDF = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnExcel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.cmdEnviar = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.chkAle = New System.Windows.Forms.CheckBox
        Me.btnFiltrar = New System.Windows.Forms.Button
        Me.chkSupPortal = New System.Windows.Forms.CheckBox
        Me.chkEnviadas = New System.Windows.Forms.CheckBox
        Me.chkNoEnviadas = New System.Windows.Forms.CheckBox
        Me.Carpeta = New System.Windows.Forms.FolderBrowserDialog
        Me.StbEstado = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtTotalPO = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtEstado = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.cboEstacionarios = New System.Windows.Forms.ComboBox
        Me.cboIdioma = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblNombre = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmdFiltrar = New System.Windows.Forms.Button
        Me.txtBuscar = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdSelectAll = New System.Windows.Forms.ToolStripButton
        Me.cmdUnselect = New System.Windows.Forms.ToolStripButton
        CType(Me.dtgRequisiciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.ToolBar.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.StbEstado.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgRequisiciones
        '
        Me.dtgRequisiciones.AllowUserToAddRows = False
        Me.dtgRequisiciones.AllowUserToDeleteRows = False
        Me.dtgRequisiciones.AllowUserToOrderColumns = True
        Me.dtgRequisiciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgRequisiciones.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dtgRequisiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dtgRequisiciones.DefaultCellStyle = DataGridViewCellStyle8
        Me.dtgRequisiciones.Location = New System.Drawing.Point(7, 114)
        Me.dtgRequisiciones.MultiSelect = False
        Me.dtgRequisiciones.Name = "dtgRequisiciones"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgRequisiciones.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dtgRequisiciones.Size = New System.Drawing.Size(931, 304)
        Me.dtgRequisiciones.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboSAPBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboVariantes)
        Me.GroupBox1.Controls.Add(Me.lblVariantes)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(253, 71)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameters"
        '
        'cboSAPBox
        '
        Me.cboSAPBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSAPBox.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboSAPBox.FormattingEnabled = True
        Me.cboSAPBox.Location = New System.Drawing.Point(57, 19)
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
        Me.cboVariantes.Location = New System.Drawing.Point(57, 43)
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
        'ToolBar
        '
        Me.ToolBar.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDowload, Me.ToolStripSeparator1, Me.cmdDesbloquear, Me.ToolStripSeparator2, Me.cmdImprimirPDF, Me.ToolStripSeparator3, Me.btnExcel, Me.ToolStripSeparator5, Me.ToolStripButton2, Me.cmdEnviar, Me.ToolStripSeparator4, Me.ToolStripButton1, Me.ToolStripSeparator6, Me.cmdSelectAll, Me.cmdUnselect})
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.Size = New System.Drawing.Size(944, 39)
        Me.ToolBar.TabIndex = 3
        Me.ToolBar.Text = "ToolStrip1"
        '
        'cmdDowload
        '
        Me.cmdDowload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDowload.Image = CType(resources.GetObject("cmdDowload.Image"), System.Drawing.Image)
        Me.cmdDowload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDowload.Name = "cmdDowload"
        Me.cmdDowload.Size = New System.Drawing.Size(36, 36)
        Me.cmdDowload.Text = "ToolStripButton1"
        Me.cmdDowload.ToolTipText = "Download Open Orders Report"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdDesbloquear
        '
        Me.cmdDesbloquear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDesbloquear.Image = CType(resources.GetObject("cmdDesbloquear.Image"), System.Drawing.Image)
        Me.cmdDesbloquear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDesbloquear.Name = "cmdDesbloquear"
        Me.cmdDesbloquear.Size = New System.Drawing.Size(36, 36)
        Me.cmdDesbloquear.Text = "ToolStripButton1"
        Me.cmdDesbloquear.ToolTipText = "New Search"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'cmdImprimirPDF
        '
        Me.cmdImprimirPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdImprimirPDF.Image = CType(resources.GetObject("cmdImprimirPDF.Image"), System.Drawing.Image)
        Me.cmdImprimirPDF.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdImprimirPDF.Name = "cmdImprimirPDF"
        Me.cmdImprimirPDF.Size = New System.Drawing.Size(36, 36)
        Me.cmdImprimirPDF.Text = "ToolStripButton1"
        Me.cmdImprimirPDF.ToolTipText = "Print PDF"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'btnExcel
        '
        Me.btnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(36, 36)
        Me.btnExcel.Text = "ToolStripButton1"
        Me.btnExcel.ToolTipText = "Export to MS Excel"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        Me.ToolStripButton2.ToolTipText = "Copy mail to Requisitioner"
        '
        'cmdEnviar
        '
        Me.cmdEnviar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdEnviar.Image = CType(resources.GetObject("cmdEnviar.Image"), System.Drawing.Image)
        Me.cmdEnviar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEnviar.Name = "cmdEnviar"
        Me.cmdEnviar.Size = New System.Drawing.Size(36, 36)
        Me.cmdEnviar.Text = "ToolStripButton2"
        Me.cmdEnviar.ToolTipText = "Send mails to MS Outlook"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "Set as sent"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkAle)
        Me.GroupBox2.Controls.Add(Me.btnFiltrar)
        Me.GroupBox2.Controls.Add(Me.chkSupPortal)
        Me.GroupBox2.Controls.Add(Me.chkEnviadas)
        Me.GroupBox2.Controls.Add(Me.chkNoEnviadas)
        Me.GroupBox2.Location = New System.Drawing.Point(257, 41)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(167, 71)
        Me.GroupBox2.TabIndex = 34
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Show"
        '
        'chkAle
        '
        Me.chkAle.AutoSize = True
        Me.chkAle.Location = New System.Drawing.Point(91, 49)
        Me.chkAle.Name = "chkAle"
        Me.chkAle.Size = New System.Drawing.Size(70, 17)
        Me.chkAle.TabIndex = 38
        Me.chkAle.Text = "Print ALE"
        Me.chkAle.UseVisualStyleBackColor = True
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Location = New System.Drawing.Point(99, 8)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Size = New System.Drawing.Size(51, 23)
        Me.btnFiltrar.TabIndex = 36
        Me.btnFiltrar.Text = "Load"
        Me.btnFiltrar.UseVisualStyleBackColor = True
        '
        'chkSupPortal
        '
        Me.chkSupPortal.AutoSize = True
        Me.chkSupPortal.Location = New System.Drawing.Point(11, 50)
        Me.chkSupPortal.Name = "chkSupPortal"
        Me.chkSupPortal.Size = New System.Drawing.Size(78, 17)
        Me.chkSupPortal.TabIndex = 35
        Me.chkSupPortal.Text = "Sup. Portal"
        Me.chkSupPortal.UseVisualStyleBackColor = True
        '
        'chkEnviadas
        '
        Me.chkEnviadas.AutoSize = True
        Me.chkEnviadas.Location = New System.Drawing.Point(11, 34)
        Me.chkEnviadas.Name = "chkEnviadas"
        Me.chkEnviadas.Size = New System.Drawing.Size(48, 17)
        Me.chkEnviadas.TabIndex = 34
        Me.chkEnviadas.Text = "Sent"
        Me.chkEnviadas.UseVisualStyleBackColor = True
        '
        'chkNoEnviadas
        '
        Me.chkNoEnviadas.AutoSize = True
        Me.chkNoEnviadas.Checked = True
        Me.chkNoEnviadas.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNoEnviadas.Location = New System.Drawing.Point(11, 18)
        Me.chkNoEnviadas.Name = "chkNoEnviadas"
        Me.chkNoEnviadas.Size = New System.Drawing.Size(60, 17)
        Me.chkNoEnviadas.TabIndex = 33
        Me.chkNoEnviadas.Text = "Unsent"
        Me.chkNoEnviadas.UseVisualStyleBackColor = True
        '
        'StbEstado
        '
        Me.StbEstado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.txtTotalPO, Me.txtEstado})
        Me.StbEstado.Location = New System.Drawing.Point(0, 421)
        Me.StbEstado.Name = "StbEstado"
        Me.StbEstado.Size = New System.Drawing.Size(944, 22)
        Me.StbEstado.TabIndex = 35
        Me.StbEstado.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(74, 17)
        Me.ToolStripStatusLabel1.Text = "Total de PO's:"
        '
        'txtTotalPO
        '
        Me.txtTotalPO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTotalPO.ForeColor = System.Drawing.Color.Maroon
        Me.txtTotalPO.Name = "txtTotalPO"
        Me.txtTotalPO.Size = New System.Drawing.Size(98, 17)
        Me.txtTotalPO.Text = "<Total de PO's>"
        '
        'txtEstado
        '
        Me.txtEstado.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.Size = New System.Drawing.Size(757, 17)
        Me.txtEstado.Spring = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cboEstacionarios)
        Me.GroupBox4.Controls.Add(Me.cboIdioma)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.lblNombre)
        Me.GroupBox4.Location = New System.Drawing.Point(423, 41)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(291, 71)
        Me.GroupBox4.TabIndex = 37
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "language and Stationary"
        '
        'cboEstacionarios
        '
        Me.cboEstacionarios.FormattingEnabled = True
        Me.cboEstacionarios.Location = New System.Drawing.Point(63, 39)
        Me.cboEstacionarios.Name = "cboEstacionarios"
        Me.cboEstacionarios.Size = New System.Drawing.Size(222, 21)
        Me.cboEstacionarios.TabIndex = 8
        '
        'cboIdioma
        '
        Me.cboIdioma.FormattingEnabled = True
        Me.cboIdioma.Location = New System.Drawing.Point(63, 14)
        Me.cboIdioma.Name = "cboIdioma"
        Me.cboIdioma.Size = New System.Drawing.Size(162, 21)
        Me.cboIdioma.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Language"
        '
        'lblNombre
        '
        Me.lblNombre.AutoSize = True
        Me.lblNombre.Location = New System.Drawing.Point(8, 43)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(54, 13)
        Me.lblNombre.TabIndex = 4
        Me.lblNombre.Text = "Stationary"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmdFiltrar)
        Me.GroupBox3.Controls.Add(Me.txtBuscar)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(713, 41)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(226, 71)
        Me.GroupBox3.TabIndex = 40
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Filter"
        '
        'cmdFiltrar
        '
        Me.cmdFiltrar.Image = CType(resources.GetObject("cmdFiltrar.Image"), System.Drawing.Image)
        Me.cmdFiltrar.Location = New System.Drawing.Point(185, 40)
        Me.cmdFiltrar.Name = "cmdFiltrar"
        Me.cmdFiltrar.Size = New System.Drawing.Size(31, 25)
        Me.cmdFiltrar.TabIndex = 2
        Me.cmdFiltrar.UseVisualStyleBackColor = True
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(9, 43)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(174, 20)
        Me.txtBuscar.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Search:"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 39)
        '
        'cmdSelectAll
        '
        Me.cmdSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSelectAll.Image = CType(resources.GetObject("cmdSelectAll.Image"), System.Drawing.Image)
        Me.cmdSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSelectAll.Name = "cmdSelectAll"
        Me.cmdSelectAll.Size = New System.Drawing.Size(36, 36)
        Me.cmdSelectAll.Text = "ToolStripButton3"
        Me.cmdSelectAll.ToolTipText = "Select all"
        '
        'cmdUnselect
        '
        Me.cmdUnselect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdUnselect.Image = CType(resources.GetObject("cmdUnselect.Image"), System.Drawing.Image)
        Me.cmdUnselect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdUnselect.Name = "cmdUnselect"
        Me.cmdUnselect.Size = New System.Drawing.Size(36, 36)
        Me.cmdUnselect.Text = "ToolStripButton4"
        Me.cmdUnselect.ToolTipText = "Clear selection"
        '
        'frm032
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(944, 443)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.StbEstado)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.dtgRequisiciones)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolBar)
        Me.Name = "frm032"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[032] Print Purchase Order by Open Order Report"
        CType(Me.dtgRequisiciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolBar.ResumeLayout(False)
        Me.ToolBar.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.StbEstado.ResumeLayout(False)
        Me.StbEstado.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgRequisiciones As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboSAPBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboVariantes As System.Windows.Forms.ComboBox
    Friend WithEvents lblVariantes As System.Windows.Forms.Label
    Friend WithEvents ToolBar As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDowload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdDesbloquear As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnFiltrar As System.Windows.Forms.Button
    Friend WithEvents chkSupPortal As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnviadas As System.Windows.Forms.CheckBox
    Friend WithEvents chkNoEnviadas As System.Windows.Forms.CheckBox
    Friend WithEvents cmdImprimirPDF As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdEnviar As System.Windows.Forms.ToolStripButton
    Friend WithEvents Carpeta As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents StbEstado As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtTotalPO As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cboEstacionarios As System.Windows.Forms.ComboBox
    Friend WithEvents cboIdioma As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents txtEstado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdFiltrar As System.Windows.Forms.Button
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkAle As System.Windows.Forms.CheckBox
    Friend WithEvents cmdSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdUnselect As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
End Class
