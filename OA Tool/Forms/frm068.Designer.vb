<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm068
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
        Dim ReportDataSource5 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.FillByToolStrip = New System.Windows.Forms.ToolStrip
        Me.cmdFilter = New System.Windows.Forms.ToolStripButton
        Me.VstSCReportBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LA_ToolDataSet = New OA_Tool.LA_ToolDataSet
        Me.Vst_SC_ReportTableAdapter = New OA_Tool.LA_ToolDataSetTableAdapters.vst_SC_ReportTableAdapter
        Me.cboSAP = New System.Windows.Forms.ComboBox
        Me.cboPlant = New System.Windows.Forms.ComboBox
        Me.cboSpend = New System.Windows.Forms.ComboBox
        Me.cboPOType = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblSpend = New System.Windows.Forms.Label
        Me.lblPOType = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.FillByToolStrip.SuspendLayout()
        CType(Me.VstSCReportBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LA_ToolDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ReportDataSource5.Name = "LA_ToolDataSet_vst_SC_Report"
        ReportDataSource5.Value = Me.VstSCReportBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource5)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "OA_Tool.SC_Report.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(12, 94)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(1117, 410)
        Me.ReportViewer1.TabIndex = 0
        '
        'FillByToolStrip
        '
        Me.FillByToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdFilter})
        Me.FillByToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.FillByToolStrip.Name = "FillByToolStrip"
        Me.FillByToolStrip.Size = New System.Drawing.Size(1141, 25)
        Me.FillByToolStrip.TabIndex = 1
        Me.FillByToolStrip.Text = "FillByToolStrip"
        '
        'cmdFilter
        '
        Me.cmdFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.cmdFilter.Name = "cmdFilter"
        Me.cmdFilter.Size = New System.Drawing.Size(46, 22)
        Me.cmdFilter.Text = "Search"
        '
        'VstSCReportBindingSource
        '
        Me.VstSCReportBindingSource.DataMember = "vst_SC_Report"
        Me.VstSCReportBindingSource.DataSource = Me.LA_ToolDataSet
        '
        'LA_ToolDataSet
        '
        Me.LA_ToolDataSet.DataSetName = "LA_ToolDataSet"
        Me.LA_ToolDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Vst_SC_ReportTableAdapter
        '
        Me.Vst_SC_ReportTableAdapter.ClearBeforeFill = True
        '
        'cboSAP
        '
        Me.cboSAP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSAP.FormattingEnabled = True
        Me.cboSAP.Location = New System.Drawing.Point(65, 40)
        Me.cboSAP.Name = "cboSAP"
        Me.cboSAP.Size = New System.Drawing.Size(137, 21)
        Me.cboSAP.TabIndex = 2
        '
        'cboPlant
        '
        Me.cboPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPlant.FormattingEnabled = True
        Me.cboPlant.Location = New System.Drawing.Point(291, 43)
        Me.cboPlant.Name = "cboPlant"
        Me.cboPlant.Size = New System.Drawing.Size(235, 21)
        Me.cboPlant.TabIndex = 3
        '
        'cboSpend
        '
        Me.cboSpend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpend.FormattingEnabled = True
        Me.cboSpend.Location = New System.Drawing.Point(65, 70)
        Me.cboSpend.Name = "cboSpend"
        Me.cboSpend.Size = New System.Drawing.Size(137, 21)
        Me.cboSpend.TabIndex = 4
        '
        'cboPOType
        '
        Me.cboPOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPOType.FormattingEnabled = True
        Me.cboPOType.Location = New System.Drawing.Point(291, 67)
        Me.cboPOType.Name = "cboPOType"
        Me.cboPOType.Size = New System.Drawing.Size(137, 21)
        Me.cboPOType.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "SAP Box"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(231, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Plant"
        '
        'lblSpend
        '
        Me.lblSpend.AutoSize = True
        Me.lblSpend.Location = New System.Drawing.Point(12, 73)
        Me.lblSpend.Name = "lblSpend"
        Me.lblSpend.Size = New System.Drawing.Size(38, 13)
        Me.lblSpend.TabIndex = 8
        Me.lblSpend.Text = "Spend"
        '
        'lblPOType
        '
        Me.lblPOType.AutoSize = True
        Me.lblPOType.Location = New System.Drawing.Point(231, 70)
        Me.lblPOType.Name = "lblPOType"
        Me.lblPOType.Size = New System.Drawing.Size(49, 13)
        Me.lblPOType.TabIndex = 9
        Me.lblPOType.Text = "PO Type"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(569, 43)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frm068
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1141, 562)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblPOType)
        Me.Controls.Add(Me.lblSpend)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboPOType)
        Me.Controls.Add(Me.cboSpend)
        Me.Controls.Add(Me.cboPlant)
        Me.Controls.Add(Me.cboSAP)
        Me.Controls.Add(Me.FillByToolStrip)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "frm068"
        Me.Text = "frm068"
        Me.FillByToolStrip.ResumeLayout(False)
        Me.FillByToolStrip.PerformLayout()
        CType(Me.VstSCReportBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LA_ToolDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents VstSCReportBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LA_ToolDataSet As OA_Tool.LA_ToolDataSet
    Friend WithEvents Vst_SC_ReportTableAdapter As OA_Tool.LA_ToolDataSetTableAdapters.vst_SC_ReportTableAdapter
    Friend WithEvents FillByToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdFilter As System.Windows.Forms.ToolStripButton
    Friend WithEvents cboSAP As System.Windows.Forms.ComboBox
    Friend WithEvents cboPlant As System.Windows.Forms.ComboBox
    Friend WithEvents cboSpend As System.Windows.Forms.ComboBox
    Friend WithEvents cboPOType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblSpend As System.Windows.Forms.Label
    Friend WithEvents lblPOType As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
