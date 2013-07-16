<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrigger
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTrigger))
        Me.dtgTrigger = New System.Windows.Forms.DataGridView
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblMateriales = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtMateriales = New System.Windows.Forms.ToolStripStatusLabel
        Me.OpenFile = New System.Windows.Forms.OpenFileDialog
        Me.cmdSearchTriggerFile = New System.Windows.Forms.ToolStripButton
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdRunScript = New System.Windows.Forms.ToolStripButton
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdVendor = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdFreese = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.lblSAPBox = New System.Windows.Forms.ToolStripLabel
        Me.cboSAPBox = New System.Windows.Forms.ToolStripComboBox
        Me.txtpwr = New System.Windows.Forms.TextBox
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        CType(Me.dtgTrigger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgTrigger
        '
        Me.dtgTrigger.AllowUserToAddRows = False
        Me.dtgTrigger.AllowUserToOrderColumns = True
        Me.dtgTrigger.AllowUserToResizeRows = False
        Me.dtgTrigger.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dtgTrigger.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.dtgTrigger.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgTrigger.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dtgTrigger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dtgTrigger.DefaultCellStyle = DataGridViewCellStyle2
        Me.dtgTrigger.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dtgTrigger.Location = New System.Drawing.Point(10, 75)
        Me.dtgTrigger.Name = "dtgTrigger"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgTrigger.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dtgTrigger.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dtgTrigger.Size = New System.Drawing.Size(917, 346)
        Me.dtgTrigger.TabIndex = 21
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblMateriales, Me.txtMateriales})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 433)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(935, 26)
        Me.StatusStrip1.TabIndex = 22
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblMateriales
        '
        Me.lblMateriales.Name = "lblMateriales"
        Me.lblMateriales.Size = New System.Drawing.Size(98, 21)
        Me.lblMateriales.Text = "Total de Materiales"
        '
        'txtMateriales
        '
        Me.txtMateriales.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.txtMateriales.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.txtMateriales.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.txtMateriales.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtMateriales.ForeColor = System.Drawing.Color.Blue
        Me.txtMateriales.Name = "txtMateriales"
        Me.txtMateriales.Size = New System.Drawing.Size(21, 21)
        Me.txtMateriales.Text = "0"
        '
        'OpenFile
        '
        Me.OpenFile.DefaultExt = "xls"
        Me.OpenFile.Filter = """Excel files|*.xls"""
        '
        'cmdSearchTriggerFile
        '
        Me.cmdSearchTriggerFile.BackgroundImage = CType(resources.GetObject("cmdSearchTriggerFile.BackgroundImage"), System.Drawing.Image)
        Me.cmdSearchTriggerFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSearchTriggerFile.Image = CType(resources.GetObject("cmdSearchTriggerFile.Image"), System.Drawing.Image)
        Me.cmdSearchTriggerFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSearchTriggerFile.Name = "cmdSearchTriggerFile"
        Me.cmdSearchTriggerFile.Size = New System.Drawing.Size(36, 36)
        Me.cmdSearchTriggerFile.Text = "ToolStripButton4"
        Me.cmdSearchTriggerFile.ToolTipText = "Search Trigger File"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdRunScript, Me.cmdSearchTriggerFile, Me.cmdExcel, Me.ToolStripButton1, Me.cmdVendor, Me.ToolStripSeparator2, Me.cmdFreese, Me.ToolStripSeparator3, Me.lblSAPBox, Me.cboSAPBox})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(935, 39)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdRunScript
        '
        Me.cmdRunScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdRunScript.Enabled = False
        Me.cmdRunScript.Image = CType(resources.GetObject("cmdRunScript.Image"), System.Drawing.Image)
        Me.cmdRunScript.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdRunScript.Name = "cmdRunScript"
        Me.cmdRunScript.Size = New System.Drawing.Size(36, 36)
        Me.cmdRunScript.Text = "ToolStripButton2"
        Me.cmdRunScript.ToolTipText = "Descargar Trigger Report"
        '
        'cmdExcel
        '
        Me.cmdExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExcel.Enabled = False
        Me.cmdExcel.Image = CType(resources.GetObject("cmdExcel.Image"), System.Drawing.Image)
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(36, 36)
        Me.cmdExcel.Text = "ToolStripButton3"
        Me.cmdExcel.ToolTipText = "Export to Excel"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdVendor
        '
        Me.cmdVendor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdVendor.Enabled = False
        Me.cmdVendor.Image = CType(resources.GetObject("cmdVendor.Image"), System.Drawing.Image)
        Me.cmdVendor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdVendor.Name = "cmdVendor"
        Me.cmdVendor.Size = New System.Drawing.Size(36, 36)
        Me.cmdVendor.Text = "ToolStripButton3"
        Me.cmdVendor.ToolTipText = "Search for vendor's contract"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'cmdFreese
        '
        Me.cmdFreese.CheckOnClick = True
        Me.cmdFreese.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFreese.Image = CType(resources.GetObject("cmdFreese.Image"), System.Drawing.Image)
        Me.cmdFreese.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdFreese.Name = "cmdFreese"
        Me.cmdFreese.Size = New System.Drawing.Size(36, 36)
        Me.cmdFreese.Text = "ToolStripButton3"
        Me.cmdFreese.ToolTipText = "Freese Column"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'lblSAPBox
        '
        Me.lblSAPBox.Name = "lblSAPBox"
        Me.lblSAPBox.Size = New System.Drawing.Size(51, 36)
        Me.lblSAPBox.Text = "SAP Box:"
        '
        'cboSAPBox
        '
        Me.cboSAPBox.AutoSize = False
        Me.cboSAPBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSAPBox.Name = "cboSAPBox"
        Me.cboSAPBox.Size = New System.Drawing.Size(150, 21)
        '
        'txtpwr
        '
        Me.txtpwr.Location = New System.Drawing.Point(215, 49)
        Me.txtpwr.Name = "txtpwr"
        Me.txtpwr.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpwr.Size = New System.Drawing.Size(145, 20)
        Me.txtpwr.TabIndex = 26
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(46, 49)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(100, 20)
        Me.txtUser.TabIndex = 25
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(162, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(21, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "T#"
        '
        'frmTrigger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 459)
        Me.Controls.Add(Me.txtpwr)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.dtgTrigger)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmTrigger"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Trigger"
        CType(Me.dtgTrigger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgTrigger As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblMateriales As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtMateriales As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents OpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdSearchTriggerFile As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdRunScript As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdVendor As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cmdFreese As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblSAPBox As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cboSAPBox As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents txtpwr As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
