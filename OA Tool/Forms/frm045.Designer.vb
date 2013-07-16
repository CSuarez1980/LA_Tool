<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm045
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm045))
        Me.ToolBar = New System.Windows.Forms.ToolStrip
        Me.cmdDowload = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnExcel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.StbEstado = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtTotalPO = New System.Windows.Forms.ToolStripStatusLabel
        Me.txtEstado = New System.Windows.Forms.ToolStripStatusLabel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboSAPBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboVariantes = New System.Windows.Forms.ComboBox
        Me.lblVariantes = New System.Windows.Forms.Label
        Me.dtgPOReport = New System.Windows.Forms.DataGridView
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.ToolBar.SuspendLayout()
        Me.StbEstado.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dtgPOReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdDowload, Me.ToolStripSeparator1, Me.btnExcel, Me.ToolStripSeparator5, Me.ToolStripButton1})
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.Size = New System.Drawing.Size(931, 39)
        Me.ToolBar.TabIndex = 53
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
        Me.cmdDowload.ToolTipText = "Download órdenes de compra"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'btnExcel
        '
        Me.btnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnExcel.Image = CType(resources.GetObject("btnExcel.Image"), System.Drawing.Image)
        Me.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(36, 36)
        Me.btnExcel.Text = "ToolStripButton1"
        Me.btnExcel.ToolTipText = "Exporta a MS Excel"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 39)
        '
        'StbEstado
        '
        Me.StbEstado.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.txtTotalPO, Me.txtEstado})
        Me.StbEstado.Location = New System.Drawing.Point(0, 540)
        Me.StbEstado.Name = "StbEstado"
        Me.StbEstado.Size = New System.Drawing.Size(931, 22)
        Me.StbEstado.TabIndex = 55
        Me.StbEstado.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(77, 17)
        Me.ToolStripStatusLabel1.Text = "PO Line Items:"
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
        Me.txtEstado.Size = New System.Drawing.Size(741, 17)
        Me.txtEstado.Spring = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboSAPBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cboVariantes)
        Me.GroupBox1.Controls.Add(Me.lblVariantes)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(253, 71)
        Me.GroupBox1.TabIndex = 48
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
        Me.Label1.Location = New System.Drawing.Point(7, 22)
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
        Me.lblVariantes.Location = New System.Drawing.Point(7, 46)
        Me.lblVariantes.Name = "lblVariantes"
        Me.lblVariantes.Size = New System.Drawing.Size(45, 13)
        Me.lblVariantes.TabIndex = 25
        Me.lblVariantes.Text = "Variants"
        '
        'dtgPOReport
        '
        Me.dtgPOReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgPOReport.Location = New System.Drawing.Point(7, 115)
        Me.dtgPOReport.Name = "dtgPOReport"
        Me.dtgPOReport.Size = New System.Drawing.Size(912, 422)
        Me.dtgPOReport.TabIndex = 56
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'frm045
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(931, 562)
        Me.Controls.Add(Me.dtgPOReport)
        Me.Controls.Add(Me.ToolBar)
        Me.Controls.Add(Me.StbEstado)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frm045"
        Me.Text = "[045] Open Orders Comments Report"
        Me.ToolBar.ResumeLayout(False)
        Me.ToolBar.PerformLayout()
        Me.StbEstado.ResumeLayout(False)
        Me.StbEstado.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dtgPOReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdDowload As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StbEstado As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtTotalPO As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtEstado As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboSAPBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboVariantes As System.Windows.Forms.ComboBox
    Friend WithEvents lblVariantes As System.Windows.Forms.Label
    Friend WithEvents dtgPOReport As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
End Class
