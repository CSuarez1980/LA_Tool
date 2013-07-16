<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm059
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm059))
        Me.grpManufacter = New System.Windows.Forms.GroupBox
        Me.cboManufacter = New System.Windows.Forms.ComboBox
        Me.lblManufacter = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.dtgVendors = New System.Windows.Forms.DataGridView
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.BS = New System.Windows.Forms.BindingSource(Me.components)
        Me.grpManufacter.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dtgVendors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpManufacter
        '
        Me.grpManufacter.Controls.Add(Me.cboManufacter)
        Me.grpManufacter.Controls.Add(Me.lblManufacter)
        Me.grpManufacter.Location = New System.Drawing.Point(12, 41)
        Me.grpManufacter.Name = "grpManufacter"
        Me.grpManufacter.Size = New System.Drawing.Size(427, 54)
        Me.grpManufacter.TabIndex = 1
        Me.grpManufacter.TabStop = False
        Me.grpManufacter.Text = "Manufacter selection"
        '
        'cboManufacter
        '
        Me.cboManufacter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboManufacter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboManufacter.FormattingEnabled = True
        Me.cboManufacter.Location = New System.Drawing.Point(85, 22)
        Me.cboManufacter.Name = "cboManufacter"
        Me.cboManufacter.Size = New System.Drawing.Size(315, 21)
        Me.cboManufacter.TabIndex = 2
        '
        'lblManufacter
        '
        Me.lblManufacter.AutoSize = True
        Me.lblManufacter.Location = New System.Drawing.Point(6, 26)
        Me.lblManufacter.Name = "lblManufacter"
        Me.lblManufacter.Size = New System.Drawing.Size(61, 13)
        Me.lblManufacter.TabIndex = 1
        Me.lblManufacter.Text = "Manufacter"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(448, 39)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'dtgVendors
        '
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
        Me.dtgVendors.Location = New System.Drawing.Point(13, 101)
        Me.dtgVendors.Name = "dtgVendors"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgVendors.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dtgVendors.Size = New System.Drawing.Size(426, 252)
        Me.dtgVendors.TabIndex = 3
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.ToolTipText = "Save"
        '
        'frm059
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(448, 365)
        Me.Controls.Add(Me.dtgVendors)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grpManufacter)
        Me.Name = "frm059"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[059] Quotation setting"
        Me.grpManufacter.ResumeLayout(False)
        Me.grpManufacter.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dtgVendors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpManufacter As System.Windows.Forms.GroupBox
    Friend WithEvents cboManufacter As System.Windows.Forms.ComboBox
    Friend WithEvents lblManufacter As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents dtgVendors As System.Windows.Forms.DataGridView
    Friend WithEvents BS As System.Windows.Forms.BindingSource
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
End Class
