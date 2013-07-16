<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm030
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm030))
        Me.dtgRequisiciones = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkFinalInvoice = New System.Windows.Forms.CheckBox
        Me.cboSAPBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboVariantes = New System.Windows.Forms.ComboBox
        Me.lblVariantes = New System.Windows.Forms.Label
        Me.tlbHerramientas = New System.Windows.Forms.ToolStrip
        Me.cmdDowload = New System.Windows.Forms.ToolStripSplitButton
        Me.DownloadWithPurchReqInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdAgregar = New System.Windows.Forms.ToolStripButton
        Me.cmdRefrescar = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdShowGR103 = New System.Windows.Forms.ToolStripButton
        Me.cmdDesbloquear = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdComentarios = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCampos = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnExcel = New System.Windows.Forms.ToolStripButton
        Me.cmdCopiarARequisitante = New System.Windows.Forms.ToolStripButton
        Me.cmdOutlook = New System.Windows.Forms.ToolStripSplitButton
        Me.SendMailToRequisitionerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdPOAnalysis = New System.Windows.Forms.ToolStripButton
        Me.cmdClosePO = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdConfirm = New System.Windows.Forms.ToolStripButton
        Me.StbEstado = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtTotalPO = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtRow = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblWorking = New System.Windows.Forms.ToolStripStatusLabel
        Me.pbProgress = New System.Windows.Forms.ToolStripProgressBar
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.cboEstacionarios = New System.Windows.Forms.ComboBox
        Me.cboIdioma = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblNombre = New System.Windows.Forms.Label
        Me.WaitWindow = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdFiltrar = New System.Windows.Forms.Button
        Me.txtBuscar = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.BG = New System.ComponentModel.BackgroundWorker
        CType(Me.dtgRequisiciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.tlbHerramientas.SuspendLayout()
        Me.StbEstado.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgRequisiciones
        '
        Me.dtgRequisiciones.AllowUserToAddRows = False
        Me.dtgRequisiciones.AllowUserToDeleteRows = False
        Me.dtgRequisiciones.AllowUserToOrderColumns = True
        Me.dtgRequisiciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgRequisiciones.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dtgRequisiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dtgRequisiciones.DefaultCellStyle = DataGridViewCellStyle2
        Me.dtgRequisiciones.Location = New System.Drawing.Point(9, 134)
        Me.dtgRequisiciones.MultiSelect = False
        Me.dtgRequisiciones.Name = "dtgRequisiciones"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgRequisiciones.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dtgRequisiciones.Size = New System.Drawing.Size(848, 294)
        Me.dtgRequisiciones.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkFinalInvoice)
        Me.GroupBox1.Controls.Add(Me.cboSAPBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboVariantes)
        Me.GroupBox1.Controls.Add(Me.lblVariantes)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(261, 86)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameters"
        '
        'chkFinalInvoice
        '
        Me.chkFinalInvoice.AutoSize = True
        Me.chkFinalInvoice.Location = New System.Drawing.Point(60, 67)
        Me.chkFinalInvoice.Name = "chkFinalInvoice"
        Me.chkFinalInvoice.Size = New System.Drawing.Size(191, 17)
        Me.chkFinalInvoice.TabIndex = 29
        Me.chkFinalInvoice.Text = "Hide PO's with Final Invoice check"
        Me.chkFinalInvoice.UseVisualStyleBackColor = True
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
        'tlbHerramientas
        '
        Me.tlbHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlbHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDowload, Me.cmdAgregar, Me.cmdRefrescar, Me.ToolStripSeparator1, Me.cmdShowGR103, Me.cmdDesbloquear, Me.ToolStripSeparator2, Me.cmdComentarios, Me.ToolStripSeparator3, Me.btnCampos, Me.ToolStripSeparator4, Me.btnExcel, Me.cmdCopiarARequisitante, Me.cmdOutlook, Me.ToolStripSeparator5, Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator6, Me.cmdPOAnalysis, Me.cmdClosePO, Me.ToolStripSeparator7, Me.cmdConfirm})
        Me.tlbHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tlbHerramientas.Name = "tlbHerramientas"
        Me.tlbHerramientas.Size = New System.Drawing.Size(864, 39)
        Me.tlbHerramientas.TabIndex = 3
        Me.tlbHerramientas.Text = "ToolStrip1"
        '
        'cmdDowload
        '
        Me.cmdDowload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDowload.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DownloadWithPurchReqInfoToolStripMenuItem})
        Me.cmdDowload.Image = CType(resources.GetObject("cmdDowload.Image"), System.Drawing.Image)
        Me.cmdDowload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDowload.Name = "cmdDowload"
        Me.cmdDowload.Size = New System.Drawing.Size(48, 36)
        Me.cmdDowload.Text = "ToolStripButton1"
        Me.cmdDowload.ToolTipText = "Download Purchases Orders"
        '
        'DownloadWithPurchReqInfoToolStripMenuItem
        '
        Me.DownloadWithPurchReqInfoToolStripMenuItem.Name = "DownloadWithPurchReqInfoToolStripMenuItem"
        Me.DownloadWithPurchReqInfoToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.DownloadWithPurchReqInfoToolStripMenuItem.Text = "Download with Purch Req. info"
        '
        'cmdAgregar
        '
        Me.cmdAgregar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAgregar.Image = CType(resources.GetObject("cmdAgregar.Image"), System.Drawing.Image)
        Me.cmdAgregar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(36, 36)
        Me.cmdAgregar.Text = "Agregar información"
        Me.cmdAgregar.ToolTipText = "Add Information to Report"
        '
        'cmdRefrescar
        '
        Me.cmdRefrescar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRefrescar.Image = CType(resources.GetObject("cmdRefrescar.Image"), System.Drawing.Image)
        Me.cmdRefrescar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRefrescar.Name = "cmdRefrescar"
        Me.cmdRefrescar.Size = New System.Drawing.Size(36, 36)
        Me.cmdRefrescar.Text = "ToolStripButton2"
        Me.cmdRefrescar.ToolTipText = "Download information from SQL Server"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdShowGR103
        '
        Me.cmdShowGR103.CheckOnClick = True
        Me.cmdShowGR103.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdShowGR103.Image = CType(resources.GetObject("cmdShowGR103.Image"), System.Drawing.Image)
        Me.cmdShowGR103.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdShowGR103.Name = "cmdShowGR103"
        Me.cmdShowGR103.Size = New System.Drawing.Size(36, 36)
        Me.cmdShowGR103.Text = "Show all Open Order Report"
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
        'cmdComentarios
        '
        Me.cmdComentarios.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdComentarios.Image = CType(resources.GetObject("cmdComentarios.Image"), System.Drawing.Image)
        Me.cmdComentarios.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdComentarios.Name = "cmdComentarios"
        Me.cmdComentarios.Size = New System.Drawing.Size(36, 36)
        Me.cmdComentarios.Text = "ToolStripButton1"
        Me.cmdComentarios.ToolTipText = "Set Comment"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'btnCampos
        '
        Me.btnCampos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnCampos.Image = CType(resources.GetObject("btnCampos.Image"), System.Drawing.Image)
        Me.btnCampos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCampos.Name = "btnCampos"
        Me.btnCampos.Size = New System.Drawing.Size(36, 36)
        Me.btnCampos.Text = "ToolStripButton1"
        Me.btnCampos.ToolTipText = "Choose Fields"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 39)
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
        'cmdCopiarARequisitante
        '
        Me.cmdCopiarARequisitante.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCopiarARequisitante.Image = CType(resources.GetObject("cmdCopiarARequisitante.Image"), System.Drawing.Image)
        Me.cmdCopiarARequisitante.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCopiarARequisitante.Name = "cmdCopiarARequisitante"
        Me.cmdCopiarARequisitante.Size = New System.Drawing.Size(36, 36)
        Me.cmdCopiarARequisitante.Text = "ToolStripButton3"
        Me.cmdCopiarARequisitante.ToolTipText = "Copy mail to Requisitioner"
        '
        'cmdOutlook
        '
        Me.cmdOutlook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdOutlook.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendMailToRequisitionerToolStripMenuItem})
        Me.cmdOutlook.Image = CType(resources.GetObject("cmdOutlook.Image"), System.Drawing.Image)
        Me.cmdOutlook.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdOutlook.Name = "cmdOutlook"
        Me.cmdOutlook.Size = New System.Drawing.Size(48, 36)
        Me.cmdOutlook.ToolTipText = "Send mails to MS Outlook"
        '
        'SendMailToRequisitionerToolStripMenuItem
        '
        Me.SendMailToRequisitionerToolStripMenuItem.Name = "SendMailToRequisitionerToolStripMenuItem"
        Me.SendMailToRequisitionerToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.SendMailToRequisitionerToolStripMenuItem.Text = "Send mail to requisitioner"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Select All"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        Me.ToolStripButton2.ToolTipText = "Deselect All"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 39)
        '
        'cmdPOAnalysis
        '
        Me.cmdPOAnalysis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdPOAnalysis.Image = CType(resources.GetObject("cmdPOAnalysis.Image"), System.Drawing.Image)
        Me.cmdPOAnalysis.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPOAnalysis.Name = "cmdPOAnalysis"
        Me.cmdPOAnalysis.Size = New System.Drawing.Size(36, 36)
        Me.cmdPOAnalysis.Text = "ToolStripButton3"
        Me.cmdPOAnalysis.ToolTipText = "Purchases Order Analysis"
        '
        'cmdClosePO
        '
        Me.cmdClosePO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdClosePO.Image = CType(resources.GetObject("cmdClosePO.Image"), System.Drawing.Image)
        Me.cmdClosePO.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdClosePO.Name = "cmdClosePO"
        Me.cmdClosePO.Size = New System.Drawing.Size(36, 36)
        Me.cmdClosePO.Text = "ToolStripButton3"
        Me.cmdClosePO.ToolTipText = "Close Item"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 39)
        '
        'cmdConfirm
        '
        Me.cmdConfirm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdConfirm.Image = CType(resources.GetObject("cmdConfirm.Image"), System.Drawing.Image)
        Me.cmdConfirm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdConfirm.Name = "cmdConfirm"
        Me.cmdConfirm.Size = New System.Drawing.Size(36, 36)
        Me.cmdConfirm.Text = "ToolStripButton3"
        Me.cmdConfirm.ToolTipText = "Confirm PO's"
        '
        'StbEstado
        '
        Me.StbEstado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.txtTotalPO, Me.ToolStripStatusLabel2, Me.txtRow, Me.lblWorking, Me.pbProgress})
        Me.StbEstado.Location = New System.Drawing.Point(0, 432)
        Me.StbEstado.Name = "StbEstado"
        Me.StbEstado.Size = New System.Drawing.Size(864, 22)
        Me.StbEstado.TabIndex = 36
        Me.StbEstado.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(80, 17)
        Me.ToolStripStatusLabel1.Text = "Total de PO's:"
        '
        'txtTotalPO
        '
        Me.txtTotalPO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTotalPO.ForeColor = System.Drawing.Color.Maroon
        Me.txtTotalPO.Name = "txtTotalPO"
        Me.txtTotalPO.Size = New System.Drawing.Size(98, 17)
        Me.txtTotalPO.Text = "<Total de PO's>"
        Me.txtTotalPO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripStatusLabel2.ForeColor = System.Drawing.Color.Navy
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(34, 17)
        Me.ToolStripStatusLabel2.Text = "Row:"
        '
        'txtRow
        '
        Me.txtRow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtRow.ForeColor = System.Drawing.Color.Navy
        Me.txtRow.Name = "txtRow"
        Me.txtRow.Size = New System.Drawing.Size(525, 17)
        Me.txtRow.Spring = True
        Me.txtRow.Text = "0"
        Me.txtRow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblWorking
        '
        Me.lblWorking.Name = "lblWorking"
        Me.lblWorking.Size = New System.Drawing.Size(10, 17)
        Me.lblWorking.Text = " "
        '
        'pbProgress
        '
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(100, 16)
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cboEstacionarios)
        Me.GroupBox4.Controls.Add(Me.cboIdioma)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.lblNombre)
        Me.GroupBox4.Location = New System.Drawing.Point(272, 40)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(309, 85)
        Me.GroupBox4.TabIndex = 38
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Language And Stationary"
        '
        'cboEstacionarios
        '
        Me.cboEstacionarios.FormattingEnabled = True
        Me.cboEstacionarios.Location = New System.Drawing.Point(65, 39)
        Me.cboEstacionarios.Name = "cboEstacionarios"
        Me.cboEstacionarios.Size = New System.Drawing.Size(231, 21)
        Me.cboEstacionarios.TabIndex = 8
        '
        'cboIdioma
        '
        Me.cboIdioma.FormattingEnabled = True
        Me.cboIdioma.Location = New System.Drawing.Point(65, 14)
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
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cmdFiltrar)
        Me.GroupBox2.Controls.Add(Me.txtBuscar)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(581, 39)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(271, 86)
        Me.GroupBox2.TabIndex = 39
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Filter"
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
        'frm030
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(864, 454)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.StbEstado)
        Me.Controls.Add(Me.dtgRequisiciones)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.tlbHerramientas)
        Me.HelpButton = True
        Me.Name = "frm030"
        Me.Text = "[030] Purchase Order Follow up"
        CType(Me.dtgRequisiciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tlbHerramientas.ResumeLayout(False)
        Me.tlbHerramientas.PerformLayout()
        Me.StbEstado.ResumeLayout(False)
        Me.StbEstado.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgRequisiciones As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboSAPBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboVariantes As System.Windows.Forms.ComboBox
    Friend WithEvents lblVariantes As System.Windows.Forms.Label
    Friend WithEvents tlbHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdDesbloquear As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdComentarios As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCampos As System.Windows.Forms.ToolStripButton
    Friend WithEvents StbEstado As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtTotalPO As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdAgregar As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdRefrescar As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cboEstacionarios As System.Windows.Forms.ComboBox
    Friend WithEvents cboIdioma As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents WaitWindow As System.Windows.Forms.Timer
    Friend WithEvents pbProgress As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblWorking As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdFiltrar As System.Windows.Forms.Button
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtRow As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmdCopiarARequisitante As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdOutlook As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SendMailToRequisitionerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdPOAnalysis As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdClosePO As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdDowload As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents DownloadWithPurchReqInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdShowGR103 As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkFinalInvoice As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdConfirm As System.Windows.Forms.ToolStripButton
    Friend WithEvents BG As System.ComponentModel.BackgroundWorker
End Class
