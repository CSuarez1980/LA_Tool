<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm035
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm035))
        Me.dtgRequisiciones = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdDowload = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.cmdAgrupar = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripSplitButton
        Me.mnuXPDtaGrid = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuXPDtaTable = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.cmdSetTaxCode = New System.Windows.Forms.ToolStripSplitButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdGetInfo = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblHT_HN = New System.Windows.Forms.Label
        Me.cboHT_HN = New System.Windows.Forms.ComboBox
        Me.cmdCheckIRVendor = New System.Windows.Forms.ToolStripButton
        CType(Me.dtgRequisiciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgRequisiciones
        '
        Me.dtgRequisiciones.AllowUserToAddRows = False
        Me.dtgRequisiciones.AllowUserToOrderColumns = True
        Me.dtgRequisiciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgRequisiciones.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dtgRequisiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dtgRequisiciones.DefaultCellStyle = DataGridViewCellStyle6
        Me.dtgRequisiciones.Location = New System.Drawing.Point(12, 71)
        Me.dtgRequisiciones.MultiSelect = False
        Me.dtgRequisiciones.Name = "dtgRequisiciones"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgRequisiciones.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightSkyBlue
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.dtgRequisiciones.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dtgRequisiciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dtgRequisiciones.Size = New System.Drawing.Size(845, 227)
        Me.dtgRequisiciones.TabIndex = 3
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDowload, Me.ToolStripSeparator1, Me.ToolStripButton1, Me.ToolStripSeparator2, Me.ToolStripButton2, Me.cmdAgrupar, Me.ToolStripSeparator4, Me.ToolStripButton4, Me.ToolStripSeparator3, Me.ToolStripButton3, Me.cmdSetTaxCode, Me.ToolStripSeparator5, Me.cmdGetInfo, Me.cmdCheckIRVendor})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(869, 39)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdDowload
        '
        Me.cmdDowload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdDowload.Image = CType(resources.GetObject("cmdDowload.Image"), System.Drawing.Image)
        Me.cmdDowload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDowload.Name = "cmdDowload"
        Me.cmdDowload.Size = New System.Drawing.Size(36, 36)
        Me.cmdDowload.Text = "ToolStripButton1"
        Me.cmdDowload.ToolTipText = "Iniciar Creación"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        Me.ToolStripButton1.ToolTipText = "Selección de Campos"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Enabled = False
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        Me.ToolStripButton2.ToolTipText = "HeaderText & HeaderNote"
        '
        'cmdAgrupar
        '
        Me.cmdAgrupar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdAgrupar.Image = CType(resources.GetObject("cmdAgrupar.Image"), System.Drawing.Image)
        Me.cmdAgrupar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAgrupar.Name = "cmdAgrupar"
        Me.cmdAgrupar.Size = New System.Drawing.Size(36, 36)
        Me.cmdAgrupar.ToolTipText = "Agrupar requicisiones por vendor"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuXPDtaGrid, Me.mnuXPDtaTable})
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(48, 36)
        Me.ToolStripButton4.Text = "ToolStripButton4"
        Me.ToolStripButton4.ToolTipText = "Exportar a Ms Excel"
        '
        'mnuXPDtaGrid
        '
        Me.mnuXPDtaGrid.Name = "mnuXPDtaGrid"
        Me.mnuXPDtaGrid.Size = New System.Drawing.Size(171, 22)
        Me.mnuXPDtaGrid.Text = "Vista en Pantalla"
        '
        'mnuXPDtaTable
        '
        Me.mnuXPDtaTable.Name = "mnuXPDtaTable"
        Me.mnuXPDtaTable.Size = New System.Drawing.Size(171, 22)
        Me.mnuXPDtaTable.Text = "Reporte Completo"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton3.Text = "ToolStripButton3"
        Me.ToolStripButton3.ToolTipText = "Colocar Mat.Usage, MatOrigin y MatCategory"
        '
        'cmdSetTaxCode
        '
        Me.cmdSetTaxCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSetTaxCode.Image = CType(resources.GetObject("cmdSetTaxCode.Image"), System.Drawing.Image)
        Me.cmdSetTaxCode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSetTaxCode.Name = "cmdSetTaxCode"
        Me.cmdSetTaxCode.Size = New System.Drawing.Size(48, 36)
        Me.cmdSetTaxCode.Text = "ToolStripButton5"
        Me.cmdSetTaxCode.ToolTipText = "Set the correct tax code"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 39)
        '
        'cmdGetInfo
        '
        Me.cmdGetInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdGetInfo.Image = CType(resources.GetObject("cmdGetInfo.Image"), System.Drawing.Image)
        Me.cmdGetInfo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGetInfo.Name = "cmdGetInfo"
        Me.cmdGetInfo.Size = New System.Drawing.Size(36, 36)
        Me.cmdGetInfo.Text = "Get Item Text Info"
        Me.cmdGetInfo.ToolTipText = "Get Item Text Information"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 304)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(869, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(854, 17)
        Me.ToolStripStatusLabel1.Spring = True
        '
        'lblHT_HN
        '
        Me.lblHT_HN.AutoSize = True
        Me.lblHT_HN.Location = New System.Drawing.Point(13, 48)
        Me.lblHT_HN.Name = "lblHT_HN"
        Me.lblHT_HN.Size = New System.Drawing.Size(152, 13)
        Me.lblHT_HN.TabIndex = 6
        Me.lblHT_HN.Text = "Header Text And Header Note"
        '
        'cboHT_HN
        '
        Me.cboHT_HN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHT_HN.FormattingEnabled = True
        Me.cboHT_HN.Location = New System.Drawing.Point(169, 44)
        Me.cboHT_HN.Name = "cboHT_HN"
        Me.cboHT_HN.Size = New System.Drawing.Size(216, 21)
        Me.cboHT_HN.TabIndex = 7
        '
        'cmdCheckIRVendor
        '
        Me.cmdCheckIRVendor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCheckIRVendor.Image = CType(resources.GetObject("cmdCheckIRVendor.Image"), System.Drawing.Image)
        Me.cmdCheckIRVendor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCheckIRVendor.Name = "cmdCheckIRVendor"
        Me.cmdCheckIRVendor.Size = New System.Drawing.Size(36, 36)
        Me.cmdCheckIRVendor.Text = "ToolStripButton5"
        Me.cmdCheckIRVendor.ToolTipText = "Check IR Only Vendors"
        '
        'frm035
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(869, 326)
        Me.Controls.Add(Me.cboHT_HN)
        Me.Controls.Add(Me.lblHT_HN)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.dtgRequisiciones)
        Me.Name = "frm035"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[035] Creación de PO's"
        CType(Me.dtgRequisiciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgRequisiciones As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDowload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents cmdAgrupar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents mnuXPDtaGrid As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuXPDtaTable As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdSetTaxCode As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents lblHT_HN As System.Windows.Forms.Label
    Friend WithEvents cboHT_HN As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdGetInfo As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdCheckIRVendor As System.Windows.Forms.ToolStripButton
End Class
