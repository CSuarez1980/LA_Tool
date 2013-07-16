<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm078
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
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Me.get_SC_ReportBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LA_ToolDataSet1 = New OA_Tool.LA_ToolDataSet1
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.get_SC_ReportTableAdapter = New OA_Tool.LA_ToolDataSet1TableAdapters.get_SC_ReportTableAdapter
        CType(Me.get_SC_ReportBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LA_ToolDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'get_SC_ReportBindingSource
        '
        Me.get_SC_ReportBindingSource.DataMember = "get_SC_Report"
        Me.get_SC_ReportBindingSource.DataSource = Me.LA_ToolDataSet1
        '
        'LA_ToolDataSet1
        '
        Me.LA_ToolDataSet1.DataSetName = "LA_ToolDataSet1"
        Me.LA_ToolDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ReportViewer1
        '
        ReportDataSource1.Name = "LA_ToolDataSet1_get_SC_Report"
        ReportDataSource1.Value = Me.get_SC_ReportBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "OA_Tool.SC_Report_Sumary.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(12, 246)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(726, 80)
        Me.ReportViewer1.TabIndex = 0
        '
        'get_SC_ReportTableAdapter
        '
        Me.get_SC_ReportTableAdapter.ClearBeforeFill = True
        '
        'frm078
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(758, 346)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "frm078"
        Me.Text = "frm078"
        CType(Me.get_SC_ReportBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LA_ToolDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents get_SC_ReportBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LA_ToolDataSet1 As OA_Tool.LA_ToolDataSet1
    Friend WithEvents get_SC_ReportTableAdapter As OA_Tool.LA_ToolDataSet1TableAdapters.get_SC_ReportTableAdapter
End Class
