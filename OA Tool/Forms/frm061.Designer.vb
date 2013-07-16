<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm061
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
        Me.grpHistory = New System.Windows.Forms.GroupBox
        Me.dtgHistory = New System.Windows.Forms.DataGridView
        Me.grpHistory.SuspendLayout()
        CType(Me.dtgHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpHistory
        '
        Me.grpHistory.Controls.Add(Me.dtgHistory)
        Me.grpHistory.Location = New System.Drawing.Point(2, 3)
        Me.grpHistory.Name = "grpHistory"
        Me.grpHistory.Size = New System.Drawing.Size(726, 232)
        Me.grpHistory.TabIndex = 0
        Me.grpHistory.TabStop = False
        Me.grpHistory.Text = "History"
        '
        'dtgHistory
        '
        Me.dtgHistory.AllowUserToAddRows = False
        Me.dtgHistory.AllowUserToDeleteRows = False
        Me.dtgHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dtgHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgHistory.Location = New System.Drawing.Point(7, 20)
        Me.dtgHistory.Name = "dtgHistory"
        Me.dtgHistory.Size = New System.Drawing.Size(710, 203)
        Me.dtgHistory.TabIndex = 0
        '
        'frm061
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(731, 238)
        Me.Controls.Add(Me.grpHistory)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm061"
        Me.Text = "[061] History"
        Me.grpHistory.ResumeLayout(False)
        CType(Me.dtgHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpHistory As System.Windows.Forms.GroupBox
    Friend WithEvents dtgHistory As System.Windows.Forms.DataGridView
End Class
