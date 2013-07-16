<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm051
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm051))
        Me.dtgAPTrade = New System.Windows.Forms.DataGridView
        Me.tblHerramientas = New System.Windows.Forms.ToolStrip
        Me.cmdExportTemplate = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdSearch = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdUpload = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        CType(Me.dtgAPTrade, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblHerramientas.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtgAPTrade
        '
        Me.dtgAPTrade.AllowUserToAddRows = False
        Me.dtgAPTrade.AllowUserToOrderColumns = True
        Me.dtgAPTrade.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgAPTrade.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dtgAPTrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dtgAPTrade.DefaultCellStyle = DataGridViewCellStyle2
        Me.dtgAPTrade.Location = New System.Drawing.Point(12, 42)
        Me.dtgAPTrade.Name = "dtgAPTrade"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dtgAPTrade.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dtgAPTrade.Size = New System.Drawing.Size(806, 394)
        Me.dtgAPTrade.TabIndex = 5
        '
        'tblHerramientas
        '
        Me.tblHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdExportTemplate, Me.ToolStripSeparator1, Me.cmdSearch, Me.ToolStripSeparator2, Me.cmdUpload})
        Me.tblHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tblHerramientas.Name = "tblHerramientas"
        Me.tblHerramientas.Size = New System.Drawing.Size(830, 39)
        Me.tblHerramientas.TabIndex = 4
        Me.tblHerramientas.Text = "ToolStrip1"
        '
        'cmdExportTemplate
        '
        Me.cmdExportTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExportTemplate.Image = CType(resources.GetObject("cmdExportTemplate.Image"), System.Drawing.Image)
        Me.cmdExportTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExportTemplate.Name = "cmdExportTemplate"
        Me.cmdExportTemplate.Size = New System.Drawing.Size(36, 36)
        Me.cmdExportTemplate.Text = "ToolStripButton1"
        Me.cmdExportTemplate.ToolTipText = "Export APTray template"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdSearch
        '
        Me.cmdSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(36, 36)
        Me.cmdSearch.Text = "ToolStripButton1"
        Me.cmdSearch.ToolTipText = "Search APTrade"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'cmdUpload
        '
        Me.cmdUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdUpload.Image = CType(resources.GetObject("cmdUpload.Image"), System.Drawing.Image)
        Me.cmdUpload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdUpload.Name = "cmdUpload"
        Me.cmdUpload.Size = New System.Drawing.Size(36, 36)
        Me.cmdUpload.Text = "ToolStripButton1"
        Me.cmdUpload.ToolTipText = "Upload to Server"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 439)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(830, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'frm051
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 461)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.dtgAPTrade)
        Me.Controls.Add(Me.tblHerramientas)
        Me.Name = "frm051"
        Me.Text = "[051] Upload APTrade"
        CType(Me.dtgAPTrade, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblHerramientas.ResumeLayout(False)
        Me.tblHerramientas.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgAPTrade As System.Windows.Forms.DataGridView
    Friend WithEvents tblHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdUpload As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdExportTemplate As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
End Class
