<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm023
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm023))
        Me.grpPerfiles = New System.Windows.Forms.GroupBox
        Me.dtgPerfiles = New System.Windows.Forms.DataGridView
        Me.cboPerfiles = New System.Windows.Forms.ComboBox
        Me.lblPefil = New System.Windows.Forms.Label
        Me.tbrHerramientas = New System.Windows.Forms.ToolStrip
        Me.cmdSave = New System.Windows.Forms.ToolStripButton
        Me.grpPerfiles.SuspendLayout()
        CType(Me.dtgPerfiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbrHerramientas.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpPerfiles
        '
        Me.grpPerfiles.Controls.Add(Me.dtgPerfiles)
        Me.grpPerfiles.Controls.Add(Me.cboPerfiles)
        Me.grpPerfiles.Controls.Add(Me.lblPefil)
        Me.grpPerfiles.Location = New System.Drawing.Point(6, 41)
        Me.grpPerfiles.Name = "grpPerfiles"
        Me.grpPerfiles.Size = New System.Drawing.Size(548, 383)
        Me.grpPerfiles.TabIndex = 2
        Me.grpPerfiles.TabStop = False
        '
        'dtgPerfiles
        '
        Me.dtgPerfiles.AllowUserToAddRows = False
        Me.dtgPerfiles.AllowUserToDeleteRows = False
        Me.dtgPerfiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dtgPerfiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgPerfiles.Location = New System.Drawing.Point(6, 42)
        Me.dtgPerfiles.Name = "dtgPerfiles"
        Me.dtgPerfiles.Size = New System.Drawing.Size(536, 335)
        Me.dtgPerfiles.TabIndex = 4
        '
        'cboPerfiles
        '
        Me.cboPerfiles.FormattingEnabled = True
        Me.cboPerfiles.Location = New System.Drawing.Point(53, 15)
        Me.cboPerfiles.Name = "cboPerfiles"
        Me.cboPerfiles.Size = New System.Drawing.Size(185, 21)
        Me.cboPerfiles.TabIndex = 3
        '
        'lblPefil
        '
        Me.lblPefil.AutoSize = True
        Me.lblPefil.Location = New System.Drawing.Point(17, 18)
        Me.lblPefil.Name = "lblPefil"
        Me.lblPefil.Size = New System.Drawing.Size(30, 13)
        Me.lblPefil.TabIndex = 2
        Me.lblPefil.Text = "Perfil"
        '
        'tbrHerramientas
        '
        Me.tbrHerramientas.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tbrHerramientas.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSave})
        Me.tbrHerramientas.Location = New System.Drawing.Point(0, 0)
        Me.tbrHerramientas.Name = "tbrHerramientas"
        Me.tbrHerramientas.Size = New System.Drawing.Size(562, 39)
        Me.tbrHerramientas.TabIndex = 3
        Me.tbrHerramientas.Text = "ToolStrip1"
        '
        'cmdSave
        '
        Me.cmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(36, 36)
        Me.cmdSave.ToolTipText = "Guardar Perfil"
        '
        'frm023
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(562, 436)
        Me.Controls.Add(Me.tbrHerramientas)
        Me.Controls.Add(Me.grpPerfiles)
        Me.Name = "frm023"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "[023] Configuración de Perfiles"
        Me.grpPerfiles.ResumeLayout(False)
        Me.grpPerfiles.PerformLayout()
        CType(Me.dtgPerfiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbrHerramientas.ResumeLayout(False)
        Me.tbrHerramientas.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpPerfiles As System.Windows.Forms.GroupBox
    Friend WithEvents cboPerfiles As System.Windows.Forms.ComboBox
    Friend WithEvents lblPefil As System.Windows.Forms.Label
    Friend WithEvents tbrHerramientas As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents dtgPerfiles As System.Windows.Forms.DataGridView
End Class
