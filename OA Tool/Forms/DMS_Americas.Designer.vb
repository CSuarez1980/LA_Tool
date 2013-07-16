<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAmericasDMS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAmericasDMS))
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Dim ReportDataSource3 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Dim ReportDataSource4 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Dim ReportDataSource5 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Dim ReportDataSource6 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Dim ReportDataSource7 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource
        Me.REQtoPO10MBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DMS_DS = New OA_Tool.DMS_DS
        Me.REQtoPO10M100MBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.REQtoPO100MBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.POConfirmationMMRNCNFBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PastDueItemsMMRBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.PastDueItemsFFTBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TriggerStep2BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblStatusl = New System.Windows.Forms.Label
        Me.ExcelButton1 = New System.Windows.Forms.Button
        Me.btnLoad = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblTitle = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.REQtoPO_10MTableAdapter = New OA_Tool.DMS_DSTableAdapters.REQtoPO_10MTableAdapter
        Me.REQtoPO_10M100MTableAdapter = New OA_Tool.DMS_DSTableAdapters.REQtoPO_10M100MTableAdapter
        Me.REQtoPO_100MTableAdapter = New OA_Tool.DMS_DSTableAdapters.REQtoPO_100MTableAdapter
        Me.POConfirmation_MMR_NCNFTableAdapter = New OA_Tool.DMS_DSTableAdapters.POConfirmation_MMR_NCNFTableAdapter
        Me.PastDueItems_MMRTableAdapter = New OA_Tool.DMS_DSTableAdapters.PastDueItems_MMRTableAdapter
        Me.PastDueItems_FFTTableAdapter = New OA_Tool.DMS_DSTableAdapters.PastDueItems_FFTTableAdapter
        Me.Trigger_Step2TableAdapter = New OA_Tool.DMS_DSTableAdapters.Trigger_Step2TableAdapter
        Me.TCMain = New System.Windows.Forms.TabControl
        Me.TP1 = New System.Windows.Forms.TabPage
        Me.RV1 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.TP2 = New System.Windows.Forms.TabPage
        Me.RV2 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.TP3 = New System.Windows.Forms.TabPage
        Me.RV3 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.TP4 = New System.Windows.Forms.TabPage
        Me.RV4 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.TP5 = New System.Windows.Forms.TabPage
        Me.RV5 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.TP6 = New System.Windows.Forms.TabPage
        Me.RV6 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.TP7 = New System.Windows.Forms.TabPage
        Me.RV7 = New Microsoft.Reporting.WinForms.ReportViewer
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdFilter = New System.Windows.Forms.ToolStripButton
        CType(Me.REQtoPO10MBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DMS_DS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.REQtoPO10M100MBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.REQtoPO100MBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.POConfirmationMMRNCNFBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PastDueItemsMMRBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PastDueItemsFFTBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TriggerStep2BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.TCMain.SuspendLayout()
        Me.TP1.SuspendLayout()
        Me.TP2.SuspendLayout()
        Me.TP3.SuspendLayout()
        Me.TP4.SuspendLayout()
        Me.TP5.SuspendLayout()
        Me.TP6.SuspendLayout()
        Me.TP7.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'REQtoPO10MBindingSource
        '
        Me.REQtoPO10MBindingSource.DataMember = "REQtoPO_10M"
        Me.REQtoPO10MBindingSource.DataSource = Me.DMS_DS
        '
        'DMS_DS
        '
        Me.DMS_DS.DataSetName = "DMS_DS"
        Me.DMS_DS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'REQtoPO10M100MBindingSource
        '
        Me.REQtoPO10M100MBindingSource.DataMember = "REQtoPO_10M100M"
        Me.REQtoPO10M100MBindingSource.DataSource = Me.DMS_DS
        '
        'REQtoPO100MBindingSource
        '
        Me.REQtoPO100MBindingSource.DataMember = "REQtoPO_100M"
        Me.REQtoPO100MBindingSource.DataSource = Me.DMS_DS
        '
        'POConfirmationMMRNCNFBindingSource
        '
        Me.POConfirmationMMRNCNFBindingSource.DataMember = "POConfirmation_MMR_NCNF"
        Me.POConfirmationMMRNCNFBindingSource.DataSource = Me.DMS_DS
        '
        'PastDueItemsMMRBindingSource
        '
        Me.PastDueItemsMMRBindingSource.DataMember = "PastDueItems_MMR"
        Me.PastDueItemsMMRBindingSource.DataSource = Me.DMS_DS
        '
        'PastDueItemsFFTBindingSource
        '
        Me.PastDueItemsFFTBindingSource.DataMember = "PastDueItems_FFT"
        Me.PastDueItemsFFTBindingSource.DataSource = Me.DMS_DS
        '
        'TriggerStep2BindingSource
        '
        Me.TriggerStep2BindingSource.DataMember = "Trigger_Step2"
        Me.TriggerStep2BindingSource.DataSource = Me.DMS_DS
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblStatusl)
        Me.Panel2.Controls.Add(Me.ExcelButton1)
        Me.Panel2.Controls.Add(Me.btnLoad)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 443)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(938, 62)
        Me.Panel2.TabIndex = 1
        '
        'lblStatusl
        '
        Me.lblStatusl.AutoSize = True
        Me.lblStatusl.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusl.Location = New System.Drawing.Point(118, 43)
        Me.lblStatusl.Name = "lblStatusl"
        Me.lblStatusl.Size = New System.Drawing.Size(0, 14)
        Me.lblStatusl.TabIndex = 23
        '
        'ExcelButton1
        '
        Me.ExcelButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExcelButton1.Image = CType(resources.GetObject("ExcelButton1.Image"), System.Drawing.Image)
        Me.ExcelButton1.Location = New System.Drawing.Point(820, 17)
        Me.ExcelButton1.Name = "ExcelButton1"
        Me.ExcelButton1.Size = New System.Drawing.Size(53, 41)
        Me.ExcelButton1.TabIndex = 22
        Me.ExcelButton1.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnLoad.Location = New System.Drawing.Point(879, 17)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(53, 41)
        Me.btnLoad.TabIndex = 21
        Me.btnLoad.Text = "Reload"
        Me.btnLoad.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 40)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(107, 17)
        Me.PictureBox1.TabIndex = 20
        Me.PictureBox1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblTitle)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(938, 21)
        Me.Panel3.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(836, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(102, 14)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Americas DMS"
        '
        'Timer1
        '
        Me.Timer1.Interval = 50
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'REQtoPO_10MTableAdapter
        '
        Me.REQtoPO_10MTableAdapter.ClearBeforeFill = True
        '
        'REQtoPO_10M100MTableAdapter
        '
        Me.REQtoPO_10M100MTableAdapter.ClearBeforeFill = True
        '
        'REQtoPO_100MTableAdapter
        '
        Me.REQtoPO_100MTableAdapter.ClearBeforeFill = True
        '
        'POConfirmation_MMR_NCNFTableAdapter
        '
        Me.POConfirmation_MMR_NCNFTableAdapter.ClearBeforeFill = True
        '
        'PastDueItems_MMRTableAdapter
        '
        Me.PastDueItems_MMRTableAdapter.ClearBeforeFill = True
        '
        'PastDueItems_FFTTableAdapter
        '
        Me.PastDueItems_FFTTableAdapter.ClearBeforeFill = True
        '
        'Trigger_Step2TableAdapter
        '
        Me.Trigger_Step2TableAdapter.ClearBeforeFill = True
        '
        'TCMain
        '
        Me.TCMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TCMain.Controls.Add(Me.TP1)
        Me.TCMain.Controls.Add(Me.TP2)
        Me.TCMain.Controls.Add(Me.TP3)
        Me.TCMain.Controls.Add(Me.TP4)
        Me.TCMain.Controls.Add(Me.TP5)
        Me.TCMain.Controls.Add(Me.TP6)
        Me.TCMain.Controls.Add(Me.TP7)
        Me.TCMain.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TCMain.Location = New System.Drawing.Point(3, 63)
        Me.TCMain.Name = "TCMain"
        Me.TCMain.SelectedIndex = 0
        Me.TCMain.Size = New System.Drawing.Size(935, 374)
        Me.TCMain.TabIndex = 24
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.Transparent
        Me.TP1.Controls.Add(Me.RV1)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Name = "TP1"
        Me.TP1.Padding = New System.Windows.Forms.Padding(3)
        Me.TP1.Size = New System.Drawing.Size(927, 348)
        Me.TP1.TabIndex = 0
        Me.TP1.Text = "REQ < $2,500"
        Me.TP1.UseVisualStyleBackColor = True
        '
        'RV1
        '
        Me.RV1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "DS_REQtoPO_10M"
        ReportDataSource1.Value = Me.REQtoPO10MBindingSource
        Me.RV1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.RV1.LocalReport.ReportEmbeddedResource = "OA_Tool.RQ2PO10M.rdlc"
        Me.RV1.Location = New System.Drawing.Point(3, 3)
        Me.RV1.Name = "RV1"
        Me.RV1.Size = New System.Drawing.Size(921, 342)
        Me.RV1.TabIndex = 0
        '
        'TP2
        '
        Me.TP2.Controls.Add(Me.RV2)
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Padding = New System.Windows.Forms.Padding(3)
        Me.TP2.Size = New System.Drawing.Size(927, 348)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "REQ >= $2,500 & < $20,000"
        Me.TP2.UseVisualStyleBackColor = True
        '
        'RV2
        '
        Me.RV2.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource2.Name = "DS_REQtoPO_10M100M"
        ReportDataSource2.Value = Me.REQtoPO10M100MBindingSource
        Me.RV2.LocalReport.DataSources.Add(ReportDataSource2)
        Me.RV2.LocalReport.ReportEmbeddedResource = "OA_Tool.RQ2PO10M100M.rdlc"
        Me.RV2.Location = New System.Drawing.Point(3, 3)
        Me.RV2.Name = "RV2"
        Me.RV2.Size = New System.Drawing.Size(921, 342)
        Me.RV2.TabIndex = 1
        '
        'TP3
        '
        Me.TP3.Controls.Add(Me.RV3)
        Me.TP3.Location = New System.Drawing.Point(4, 22)
        Me.TP3.Name = "TP3"
        Me.TP3.Padding = New System.Windows.Forms.Padding(3)
        Me.TP3.Size = New System.Drawing.Size(927, 348)
        Me.TP3.TabIndex = 2
        Me.TP3.Text = "REQ >= $20,000M "
        Me.TP3.UseVisualStyleBackColor = True
        '
        'RV3
        '
        Me.RV3.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource3.Name = "DS_REQtoPO_100M"
        ReportDataSource3.Value = Me.REQtoPO100MBindingSource
        Me.RV3.LocalReport.DataSources.Add(ReportDataSource3)
        Me.RV3.LocalReport.ReportEmbeddedResource = "OA_Tool.RQ2PO100M.rdlc"
        Me.RV3.Location = New System.Drawing.Point(3, 3)
        Me.RV3.Name = "RV3"
        Me.RV3.Size = New System.Drawing.Size(921, 342)
        Me.RV3.TabIndex = 1
        '
        'TP4
        '
        Me.TP4.Controls.Add(Me.RV4)
        Me.TP4.Location = New System.Drawing.Point(4, 22)
        Me.TP4.Name = "TP4"
        Me.TP4.Padding = New System.Windows.Forms.Padding(3)
        Me.TP4.Size = New System.Drawing.Size(927, 348)
        Me.TP4.TabIndex = 3
        Me.TP4.Text = "MMR Confirmation"
        Me.TP4.UseVisualStyleBackColor = True
        '
        'RV4
        '
        Me.RV4.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource4.Name = "DS_POConfirmation_MMR_NCNF"
        ReportDataSource4.Value = Me.POConfirmationMMRNCNFBindingSource
        Me.RV4.LocalReport.DataSources.Add(ReportDataSource4)
        Me.RV4.LocalReport.ReportEmbeddedResource = "OA_Tool.MMRNCNF.rdlc"
        Me.RV4.Location = New System.Drawing.Point(3, 3)
        Me.RV4.Name = "RV4"
        Me.RV4.Size = New System.Drawing.Size(921, 342)
        Me.RV4.TabIndex = 1
        '
        'TP5
        '
        Me.TP5.Controls.Add(Me.RV5)
        Me.TP5.Location = New System.Drawing.Point(4, 22)
        Me.TP5.Name = "TP5"
        Me.TP5.Padding = New System.Windows.Forms.Padding(3)
        Me.TP5.Size = New System.Drawing.Size(927, 348)
        Me.TP5.TabIndex = 4
        Me.TP5.Text = "MMR Past Dues"
        Me.TP5.UseVisualStyleBackColor = True
        '
        'RV5
        '
        Me.RV5.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource5.Name = "DS_PastDueItems_MMR"
        ReportDataSource5.Value = Me.PastDueItemsMMRBindingSource
        Me.RV5.LocalReport.DataSources.Add(ReportDataSource5)
        Me.RV5.LocalReport.ReportEmbeddedResource = "OA_Tool.MMRPastDues.rdlc"
        Me.RV5.Location = New System.Drawing.Point(3, 3)
        Me.RV5.Name = "RV5"
        Me.RV5.Size = New System.Drawing.Size(921, 342)
        Me.RV5.TabIndex = 1
        '
        'TP6
        '
        Me.TP6.Controls.Add(Me.RV6)
        Me.TP6.Location = New System.Drawing.Point(4, 22)
        Me.TP6.Name = "TP6"
        Me.TP6.Padding = New System.Windows.Forms.Padding(3)
        Me.TP6.Size = New System.Drawing.Size(927, 348)
        Me.TP6.TabIndex = 5
        Me.TP6.Text = "FFT Past Dues"
        Me.TP6.UseVisualStyleBackColor = True
        '
        'RV6
        '
        Me.RV6.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource6.Name = "DS_PastDueItems_FFT"
        ReportDataSource6.Value = Me.PastDueItemsFFTBindingSource
        Me.RV6.LocalReport.DataSources.Add(ReportDataSource6)
        Me.RV6.LocalReport.ReportEmbeddedResource = "OA_Tool.FFTPastDues.rdlc"
        Me.RV6.Location = New System.Drawing.Point(3, 3)
        Me.RV6.Name = "RV6"
        Me.RV6.Size = New System.Drawing.Size(921, 342)
        Me.RV6.TabIndex = 1
        '
        'TP7
        '
        Me.TP7.Controls.Add(Me.RV7)
        Me.TP7.Location = New System.Drawing.Point(4, 22)
        Me.TP7.Name = "TP7"
        Me.TP7.Padding = New System.Windows.Forms.Padding(3)
        Me.TP7.Size = New System.Drawing.Size(927, 348)
        Me.TP7.TabIndex = 6
        Me.TP7.Text = "Trigger Step2"
        Me.TP7.UseVisualStyleBackColor = True
        '
        'RV7
        '
        Me.RV7.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource7.Name = "DS_Trigger_Step2"
        ReportDataSource7.Value = Me.TriggerStep2BindingSource
        Me.RV7.LocalReport.DataSources.Add(ReportDataSource7)
        Me.RV7.LocalReport.ReportEmbeddedResource = "OA_Tool.TriggerStep2.rdlc"
        Me.RV7.Location = New System.Drawing.Point(3, 3)
        Me.RV7.Name = "RV7"
        Me.RV7.Size = New System.Drawing.Size(921, 342)
        Me.RV7.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdFilter})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 21)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(938, 39)
        Me.ToolStrip1.TabIndex = 25
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdFilter
        '
        Me.cmdFilter.Checked = True
        Me.cmdFilter.CheckOnClick = True
        Me.cmdFilter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cmdFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdFilter.Image = Global.OA_Tool.My.Resources.Resources.down
        Me.cmdFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdFilter.Name = "cmdFilter"
        Me.cmdFilter.Size = New System.Drawing.Size(36, 36)
        Me.cmdFilter.Text = "ToolStripButton2"
        Me.cmdFilter.ToolTipText = "Get just my report"
        '
        'frmAmericasDMS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(938, 505)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.TCMain)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmAmericasDMS"
        Me.Text = "DMS Americas Reporting"
        CType(Me.REQtoPO10MBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DMS_DS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.REQtoPO10M100MBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.REQtoPO100MBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.POConfirmationMMRNCNFBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PastDueItemsMMRBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PastDueItemsFFTBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TriggerStep2BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TCMain.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP2.ResumeLayout(False)
        Me.TP3.ResumeLayout(False)
        Me.TP4.ResumeLayout(False)
        Me.TP5.ResumeLayout(False)
        Me.TP6.ResumeLayout(False)
        Me.TP7.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ExcelButton1 As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblStatusl As System.Windows.Forms.Label
    Friend WithEvents REQtoPO10MBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DMS_DS As OA_Tool.DMS_DS
    Friend WithEvents REQtoPO_10MTableAdapter As OA_Tool.DMS_DSTableAdapters.REQtoPO_10MTableAdapter
    Friend WithEvents REQtoPO10M100MBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents REQtoPO_10M100MTableAdapter As OA_Tool.DMS_DSTableAdapters.REQtoPO_10M100MTableAdapter
    Friend WithEvents REQtoPO100MBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents REQtoPO_100MTableAdapter As OA_Tool.DMS_DSTableAdapters.REQtoPO_100MTableAdapter
    Friend WithEvents POConfirmationMMRNCNFBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents POConfirmation_MMR_NCNFTableAdapter As OA_Tool.DMS_DSTableAdapters.POConfirmation_MMR_NCNFTableAdapter
    Friend WithEvents PastDueItemsMMRBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PastDueItems_MMRTableAdapter As OA_Tool.DMS_DSTableAdapters.PastDueItems_MMRTableAdapter
    Friend WithEvents PastDueItemsFFTBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents PastDueItems_FFTTableAdapter As OA_Tool.DMS_DSTableAdapters.PastDueItems_FFTTableAdapter
    Friend WithEvents TriggerStep2BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Trigger_Step2TableAdapter As OA_Tool.DMS_DSTableAdapters.Trigger_Step2TableAdapter
    Friend WithEvents TCMain As System.Windows.Forms.TabControl
    Friend WithEvents TP1 As System.Windows.Forms.TabPage
    Friend WithEvents RV1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents TP2 As System.Windows.Forms.TabPage
    Friend WithEvents RV2 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents TP3 As System.Windows.Forms.TabPage
    Friend WithEvents RV3 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents TP4 As System.Windows.Forms.TabPage
    Friend WithEvents RV4 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents TP5 As System.Windows.Forms.TabPage
    Friend WithEvents RV5 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents TP6 As System.Windows.Forms.TabPage
    Friend WithEvents RV6 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents TP7 As System.Windows.Forms.TabPage
    Friend WithEvents RV7 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdFilter As System.Windows.Forms.ToolStripButton
End Class
