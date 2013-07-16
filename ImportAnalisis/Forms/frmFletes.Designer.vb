<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFletes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFletes))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdGuardar = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdExportar = New System.Windows.Forms.ToolStripButton
        Me.cmdImportar = New System.Windows.Forms.ToolStripButton
        Me.grpFletes = New System.Windows.Forms.GroupBox
        Me.dtgFletes = New System.Windows.Forms.DataGridView
        Me.Label7 = New System.Windows.Forms.Label
        Me.cboMedio = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboOrigen = New System.Windows.Forms.ComboBox
        Me.lblDestino = New System.Windows.Forms.Label
        Me.cboDestino = New System.Windows.Forms.ComboBox
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.ToolStrip1.SuspendLayout()
        Me.grpFletes.SuspendLayout()
        CType(Me.dtgFletes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdGuardar, Me.ToolStripSeparator1, Me.cmdExportar, Me.cmdImportar})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(790, 39)
        Me.ToolStrip1.TabIndex = 22
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdGuardar
        '
        Me.cmdGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdGuardar.Image = CType(resources.GetObject("cmdGuardar.Image"), System.Drawing.Image)
        Me.cmdGuardar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdGuardar.Name = "cmdGuardar"
        Me.cmdGuardar.Size = New System.Drawing.Size(36, 36)
        Me.cmdGuardar.Text = "ToolStripButton1"
        Me.cmdGuardar.ToolTipText = "Guardar"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdExportar
        '
        Me.cmdExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExportar.Image = CType(resources.GetObject("cmdExportar.Image"), System.Drawing.Image)
        Me.cmdExportar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExportar.Name = "cmdExportar"
        Me.cmdExportar.Size = New System.Drawing.Size(36, 36)
        Me.cmdExportar.Text = "Exportar Template a Excel"
        '
        'cmdImportar
        '
        Me.cmdImportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdImportar.Image = CType(resources.GetObject("cmdImportar.Image"), System.Drawing.Image)
        Me.cmdImportar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdImportar.Name = "cmdImportar"
        Me.cmdImportar.Size = New System.Drawing.Size(36, 36)
        Me.cmdImportar.Text = "Importar Template de Excel"
        '
        'grpFletes
        '
        Me.grpFletes.Controls.Add(Me.dtgFletes)
        Me.grpFletes.Controls.Add(Me.Label7)
        Me.grpFletes.Controls.Add(Me.cboMedio)
        Me.grpFletes.Controls.Add(Me.Label1)
        Me.grpFletes.Controls.Add(Me.cboOrigen)
        Me.grpFletes.Controls.Add(Me.lblDestino)
        Me.grpFletes.Controls.Add(Me.cboDestino)
        Me.grpFletes.Location = New System.Drawing.Point(12, 42)
        Me.grpFletes.Name = "grpFletes"
        Me.grpFletes.Size = New System.Drawing.Size(766, 405)
        Me.grpFletes.TabIndex = 23
        Me.grpFletes.TabStop = False
        Me.grpFletes.Text = "Variables de Importación"
        '
        'dtgFletes
        '
        Me.dtgFletes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgFletes.Location = New System.Drawing.Point(11, 102)
        Me.dtgFletes.Name = "dtgFletes"
        Me.dtgFletes.Size = New System.Drawing.Size(744, 297)
        Me.dtgFletes.TabIndex = 28
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Modo Envío"
        '
        'cboMedio
        '
        Me.cboMedio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMedio.FormattingEnabled = True
        Me.cboMedio.Location = New System.Drawing.Point(81, 21)
        Me.cboMedio.Name = "cboMedio"
        Me.cboMedio.Size = New System.Drawing.Size(143, 21)
        Me.cboMedio.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Origen"
        '
        'cboOrigen
        '
        Me.cboOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrigen.FormattingEnabled = True
        Me.cboOrigen.Location = New System.Drawing.Point(81, 75)
        Me.cboOrigen.Name = "cboOrigen"
        Me.cboOrigen.Size = New System.Drawing.Size(142, 21)
        Me.cboOrigen.TabIndex = 24
        '
        'lblDestino
        '
        Me.lblDestino.AutoSize = True
        Me.lblDestino.Location = New System.Drawing.Point(13, 52)
        Me.lblDestino.Name = "lblDestino"
        Me.lblDestino.Size = New System.Drawing.Size(43, 13)
        Me.lblDestino.TabIndex = 23
        Me.lblDestino.Text = "Destino"
        '
        'cboDestino
        '
        Me.cboDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDestino.FormattingEnabled = True
        Me.cboDestino.Location = New System.Drawing.Point(81, 48)
        Me.cboDestino.Name = "cboDestino"
        Me.cboDestino.Size = New System.Drawing.Size(143, 21)
        Me.cboDestino.TabIndex = 22
        '
        'frmFletes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 459)
        Me.Controls.Add(Me.grpFletes)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmFletes"
        Me.Text = "frmFletes"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.grpFletes.ResumeLayout(False)
        Me.grpFletes.PerformLayout()
        CType(Me.dtgFletes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdGuardar As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdExportar As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdImportar As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpFletes As System.Windows.Forms.GroupBox
    Friend WithEvents dtgFletes As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboMedio As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboOrigen As System.Windows.Forms.ComboBox
    Friend WithEvents lblDestino As System.Windows.Forms.Label
    Friend WithEvents cboDestino As System.Windows.Forms.ComboBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
End Class
