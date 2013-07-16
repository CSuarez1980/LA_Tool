<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm000
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
        Me.txtStatus = New System.Windows.Forms.ListBox
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.chkInicio = New System.Windows.Forms.CheckBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.OADownload = New System.ComponentModel.BackgroundWorker
        Me.VendorDownload = New System.ComponentModel.BackgroundWorker
        Me.MDDownload = New System.ComponentModel.BackgroundWorker
        Me.RequiDownload = New System.ComponentModel.BackgroundWorker
        Me.chkSSO = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblNextUpload = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtStatus
        '
        Me.txtStatus.FormattingEnabled = True
        Me.txtStatus.Location = New System.Drawing.Point(23, 67)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(474, 225)
        Me.txtStatus.TabIndex = 6
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(23, 298)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(474, 23)
        Me.ProgressBar1.TabIndex = 5
        '
        'chkInicio
        '
        Me.chkInicio.AutoSize = True
        Me.chkInicio.Location = New System.Drawing.Point(23, 12)
        Me.chkInicio.Name = "chkInicio"
        Me.chkInicio.Size = New System.Drawing.Size(96, 17)
        Me.chkInicio.TabIndex = 4
        Me.chkInicio.Text = "Iniciar Proceso"
        Me.chkInicio.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'OADownload
        '
        Me.OADownload.WorkerReportsProgress = True
        Me.OADownload.WorkerSupportsCancellation = True
        '
        'RequiDownload
        '
        Me.RequiDownload.WorkerReportsProgress = True
        Me.RequiDownload.WorkerSupportsCancellation = True
        '
        'chkSSO
        '
        Me.chkSSO.AutoSize = True
        Me.chkSSO.Location = New System.Drawing.Point(23, 35)
        Me.chkSSO.Name = "chkSSO"
        Me.chkSSO.Size = New System.Drawing.Size(112, 17)
        Me.chkSSO.TabIndex = 7
        Me.chkSSO.Text = "Utilizar SAP - SSO"
        Me.chkSSO.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(520, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(183, 31)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Next Upload:"
        '
        'lblNextUpload
        '
        Me.lblNextUpload.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNextUpload.ForeColor = System.Drawing.Color.Red
        Me.lblNextUpload.Location = New System.Drawing.Point(529, 113)
        Me.lblNextUpload.Name = "lblNextUpload"
        Me.lblNextUpload.Size = New System.Drawing.Size(174, 32)
        Me.lblNextUpload.TabIndex = 9
        Me.lblNextUpload.Text = "Label2"
        Me.lblNextUpload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frm000
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(725, 345)
        Me.Controls.Add(Me.lblNextUpload)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkSSO)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.chkInicio)
        Me.Name = "frm000"
        Me.Text = "[000] Downloader"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtStatus As System.Windows.Forms.ListBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents chkInicio As System.Windows.Forms.CheckBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents OADownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents VendorDownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents MDDownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents RequiDownload As System.ComponentModel.BackgroundWorker
    Friend WithEvents chkSSO As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblNextUpload As System.Windows.Forms.Label
End Class
