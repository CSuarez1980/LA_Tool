<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm060
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm060))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.lblComment = New System.Windows.Forms.Label
        Me.cmdAttach = New System.Windows.Forms.Button
        Me.txtAttach = New System.Windows.Forms.TextBox
        Me.lblAttach = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdSelectAll = New System.Windows.Forms.ToolStripButton
        Me.cmdClearSelection = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdHistory = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdConfirm = New System.Windows.Forms.ToolStripButton
        Me.dtgVendors = New System.Windows.Forms.DataGridView
        Me.lblSAP = New System.Windows.Forms.Label
        Me.txtSAPBox = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.cboEstacionarios = New System.Windows.Forms.ComboBox
        Me.cboIdioma = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblNombre = New System.Windows.Forms.Label
        Me.lblQuantity = New System.Windows.Forms.Label
        Me.txtQuantity = New System.Windows.Forms.TextBox
        Me.txtPartNumber = New System.Windows.Forms.TextBox
        Me.lblPartNumber = New System.Windows.Forms.Label
        Me.txtManufacter = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtItem = New System.Windows.Forms.TextBox
        Me.txtRequisition = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.txtGica = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.tbrHerramientas = New System.Windows.Forms.ToolStrip
        Me.cmdOutlook = New System.Windows.Forms.ToolStripButton
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dtgVendors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.tbrHerramientas.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.txtComment)
        Me.GroupBox1.Controls.Add(Me.lblComment)
        Me.GroupBox1.Controls.Add(Me.cmdAttach)
        Me.GroupBox1.Controls.Add(Me.txtAttach)
        Me.GroupBox1.Controls.Add(Me.lblAttach)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.lblSAP)
        Me.GroupBox1.Controls.Add(Me.txtSAPBox)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.lblQuantity)
        Me.GroupBox1.Controls.Add(Me.txtQuantity)
        Me.GroupBox1.Controls.Add(Me.txtPartNumber)
        Me.GroupBox1.Controls.Add(Me.lblPartNumber)
        Me.GroupBox1.Controls.Add(Me.txtManufacter)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtItem)
        Me.GroupBox1.Controls.Add(Me.txtRequisition)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtDescription)
        Me.GroupBox1.Controls.Add(Me.txtGica)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(729, 400)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Requisition Detail"
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(277, 84)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(27, 24)
        Me.Button1.TabIndex = 77
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(380, 86)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(334, 69)
        Me.txtComment.TabIndex = 76
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.Location = New System.Drawing.Point(327, 89)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(51, 13)
        Me.lblComment.TabIndex = 75
        Me.lblComment.Text = "Comment"
        '
        'cmdAttach
        '
        Me.cmdAttach.Image = CType(resources.GetObject("cmdAttach.Image"), System.Drawing.Image)
        Me.cmdAttach.Location = New System.Drawing.Point(277, 132)
        Me.cmdAttach.Name = "cmdAttach"
        Me.cmdAttach.Size = New System.Drawing.Size(27, 24)
        Me.cmdAttach.TabIndex = 74
        Me.cmdAttach.UseVisualStyleBackColor = True
        '
        'txtAttach
        '
        Me.txtAttach.BackColor = System.Drawing.Color.White
        Me.txtAttach.Location = New System.Drawing.Point(81, 133)
        Me.txtAttach.Name = "txtAttach"
        Me.txtAttach.ReadOnly = True
        Me.txtAttach.Size = New System.Drawing.Size(193, 20)
        Me.txtAttach.TabIndex = 73
        Me.txtAttach.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblAttach
        '
        Me.lblAttach.AutoSize = True
        Me.lblAttach.Location = New System.Drawing.Point(13, 137)
        Me.lblAttach.Name = "lblAttach"
        Me.lblAttach.Size = New System.Drawing.Size(38, 13)
        Me.lblAttach.TabIndex = 72
        Me.lblAttach.Text = "Attach"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ToolStrip1)
        Me.GroupBox2.Controls.Add(Me.dtgVendors)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 159)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(729, 238)
        Me.GroupBox2.TabIndex = 71
        Me.GroupBox2.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSelectAll, Me.cmdClearSelection, Me.ToolStripSeparator1, Me.cmdHistory, Me.ToolStripSeparator2, Me.cmdConfirm})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 16)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(723, 25)
        Me.ToolStrip1.TabIndex = 27
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdSelectAll
        '
        Me.cmdSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSelectAll.Image = CType(resources.GetObject("cmdSelectAll.Image"), System.Drawing.Image)
        Me.cmdSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSelectAll.Name = "cmdSelectAll"
        Me.cmdSelectAll.Size = New System.Drawing.Size(23, 22)
        Me.cmdSelectAll.Text = "ToolStripButton1"
        Me.cmdSelectAll.ToolTipText = "Select all"
        '
        'cmdClearSelection
        '
        Me.cmdClearSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdClearSelection.Image = CType(resources.GetObject("cmdClearSelection.Image"), System.Drawing.Image)
        Me.cmdClearSelection.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdClearSelection.Name = "cmdClearSelection"
        Me.cmdClearSelection.Size = New System.Drawing.Size(23, 22)
        Me.cmdClearSelection.Text = "ToolStripButton2"
        Me.cmdClearSelection.ToolTipText = "Clear selection"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'cmdHistory
        '
        Me.cmdHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdHistory.Image = CType(resources.GetObject("cmdHistory.Image"), System.Drawing.Image)
        Me.cmdHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdHistory.Name = "cmdHistory"
        Me.cmdHistory.Size = New System.Drawing.Size(23, 22)
        Me.cmdHistory.Text = "ToolStripButton1"
        Me.cmdHistory.ToolTipText = "View history"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'cmdConfirm
        '
        Me.cmdConfirm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdConfirm.Image = CType(resources.GetObject("cmdConfirm.Image"), System.Drawing.Image)
        Me.cmdConfirm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdConfirm.Name = "cmdConfirm"
        Me.cmdConfirm.Size = New System.Drawing.Size(23, 22)
        Me.cmdConfirm.ToolTipText = "Confirm"
        '
        'dtgVendors
        '
        Me.dtgVendors.AllowUserToDeleteRows = False
        Me.dtgVendors.AllowUserToResizeRows = False
        Me.dtgVendors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgVendors.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dtgVendors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dtgVendors.DefaultCellStyle = DataGridViewCellStyle2
        Me.dtgVendors.Location = New System.Drawing.Point(5, 43)
        Me.dtgVendors.Name = "dtgVendors"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgVendors.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dtgVendors.Size = New System.Drawing.Size(719, 195)
        Me.dtgVendors.TabIndex = 26
        '
        'lblSAP
        '
        Me.lblSAP.AutoSize = True
        Me.lblSAP.Location = New System.Drawing.Point(226, 67)
        Me.lblSAP.Name = "lblSAP"
        Me.lblSAP.Size = New System.Drawing.Size(28, 13)
        Me.lblSAP.TabIndex = 70
        Me.lblSAP.Text = "SAP"
        '
        'txtSAPBox
        '
        Me.txtSAPBox.BackColor = System.Drawing.Color.White
        Me.txtSAPBox.Location = New System.Drawing.Point(256, 64)
        Me.txtSAPBox.Name = "txtSAPBox"
        Me.txtSAPBox.ReadOnly = True
        Me.txtSAPBox.Size = New System.Drawing.Size(47, 20)
        Me.txtSAPBox.TabIndex = 69
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cboEstacionarios)
        Me.GroupBox4.Controls.Add(Me.cboIdioma)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.lblNombre)
        Me.GroupBox4.Location = New System.Drawing.Point(438, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(291, 71)
        Me.GroupBox4.TabIndex = 68
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Language and Stationary"
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Language"
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
        'lblQuantity
        '
        Me.lblQuantity.AutoSize = True
        Me.lblQuantity.Location = New System.Drawing.Point(13, 67)
        Me.lblQuantity.Name = "lblQuantity"
        Me.lblQuantity.Size = New System.Drawing.Size(46, 13)
        Me.lblQuantity.TabIndex = 67
        Me.lblQuantity.Text = "Quantity"
        '
        'txtQuantity
        '
        Me.txtQuantity.BackColor = System.Drawing.Color.White
        Me.txtQuantity.Location = New System.Drawing.Point(81, 64)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.ReadOnly = True
        Me.txtQuantity.Size = New System.Drawing.Size(47, 20)
        Me.txtQuantity.TabIndex = 66
        '
        'txtPartNumber
        '
        Me.txtPartNumber.BackColor = System.Drawing.Color.White
        Me.txtPartNumber.Location = New System.Drawing.Point(81, 109)
        Me.txtPartNumber.Name = "txtPartNumber"
        Me.txtPartNumber.Size = New System.Drawing.Size(224, 20)
        Me.txtPartNumber.TabIndex = 65
        '
        'lblPartNumber
        '
        Me.lblPartNumber.AutoSize = True
        Me.lblPartNumber.Location = New System.Drawing.Point(13, 112)
        Me.lblPartNumber.Name = "lblPartNumber"
        Me.lblPartNumber.Size = New System.Drawing.Size(66, 13)
        Me.lblPartNumber.TabIndex = 64
        Me.lblPartNumber.Text = "Part Number"
        '
        'txtManufacter
        '
        Me.txtManufacter.BackColor = System.Drawing.Color.White
        Me.txtManufacter.Location = New System.Drawing.Point(81, 86)
        Me.txtManufacter.Name = "txtManufacter"
        Me.txtManufacter.Size = New System.Drawing.Size(192, 20)
        Me.txtManufacter.TabIndex = 63
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "Manufacter"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(228, 23)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(27, 13)
        Me.Label20.TabIndex = 61
        Me.Label20.Text = "Item"
        '
        'txtItem
        '
        Me.txtItem.BackColor = System.Drawing.Color.White
        Me.txtItem.Location = New System.Drawing.Point(258, 20)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.ReadOnly = True
        Me.txtItem.Size = New System.Drawing.Size(47, 20)
        Me.txtItem.TabIndex = 60
        '
        'txtRequisition
        '
        Me.txtRequisition.BackColor = System.Drawing.Color.White
        Me.txtRequisition.Location = New System.Drawing.Point(81, 20)
        Me.txtRequisition.Name = "txtRequisition"
        Me.txtRequisition.ReadOnly = True
        Me.txtRequisition.Size = New System.Drawing.Size(132, 20)
        Me.txtRequisition.TabIndex = 59
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "Requistion"
        '
        'txtDescription
        '
        Me.txtDescription.BackColor = System.Drawing.Color.White
        Me.txtDescription.Location = New System.Drawing.Point(183, 42)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(248, 20)
        Me.txtDescription.TabIndex = 2
        '
        'txtGica
        '
        Me.txtGica.BackColor = System.Drawing.Color.White
        Me.txtGica.Location = New System.Drawing.Point(81, 42)
        Me.txtGica.Name = "txtGica"
        Me.txtGica.ReadOnly = True
        Me.txtGica.Size = New System.Drawing.Size(100, 20)
        Me.txtGica.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Material"
        '
        'tbrHerramientas
        '
        Me.tbrHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tbrHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdOutlook})
        Me.tbrHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tbrHerramientas.Name = "tbrHerramientas"
        Me.tbrHerramientas.Size = New System.Drawing.Size(745, 39)
        Me.tbrHerramientas.TabIndex = 25
        Me.tbrHerramientas.Text = "ToolStrip1"
        '
        'cmdOutlook
        '
        Me.cmdOutlook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdOutlook.Image = CType(resources.GetObject("cmdOutlook.Image"), System.Drawing.Image)
        Me.cmdOutlook.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdOutlook.Name = "cmdOutlook"
        Me.cmdOutlook.Size = New System.Drawing.Size(36, 36)
        Me.cmdOutlook.Text = "ToolStripButton1"
        Me.cmdOutlook.ToolTipText = "Send to Outlook"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frm060
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(745, 451)
        Me.Controls.Add(Me.tbrHerramientas)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "frm060"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[060] Quotation Process"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dtgVendors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.tbrHerramientas.ResumeLayout(False)
        Me.tbrHerramientas.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents txtRequisition As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtGica As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtManufacter As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tbrHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdOutlook As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblQuantity As System.Windows.Forms.Label
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents txtPartNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblPartNumber As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cboEstacionarios As System.Windows.Forms.ComboBox
    Friend WithEvents cboIdioma As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents lblSAP As System.Windows.Forms.Label
    Friend WithEvents txtSAPBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdClearSelection As System.Windows.Forms.ToolStripButton
    Friend WithEvents dtgVendors As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdAttach As System.Windows.Forms.Button
    Friend WithEvents txtAttach As System.Windows.Forms.TextBox
    Friend WithEvents lblAttach As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents lblComment As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmdConfirm As System.Windows.Forms.ToolStripButton
End Class
