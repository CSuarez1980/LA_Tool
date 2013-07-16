<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm016
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
        Me.dtgTracking = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtgPorVencer = New System.Windows.Forms.DataGridView
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtgVencidos = New System.Windows.Forms.DataGridView
        CType(Me.dtgTracking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtgPorVencer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtgVencidos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtgTracking
        '
        Me.dtgTracking.AllowUserToAddRows = False
        Me.dtgTracking.AllowUserToResizeRows = False
        Me.dtgTracking.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dtgTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgTracking.Location = New System.Drawing.Point(5, 62)
        Me.dtgTracking.Name = "dtgTracking"
        Me.dtgTracking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dtgTracking.Size = New System.Drawing.Size(515, 344)
        Me.dtgTracking.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(195, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Tiempo promedio en días en cada paso"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(533, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(156, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Contratos por vencer por región"
        '
        'dtgPorVencer
        '
        Me.dtgPorVencer.AllowUserToAddRows = False
        Me.dtgPorVencer.AllowUserToResizeRows = False
        Me.dtgPorVencer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dtgPorVencer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgPorVencer.Location = New System.Drawing.Point(536, 62)
        Me.dtgPorVencer.Name = "dtgPorVencer"
        Me.dtgPorVencer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dtgPorVencer.Size = New System.Drawing.Size(190, 115)
        Me.dtgPorVencer.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(533, 233)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(151, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Contratos vencidos  por región"
        '
        'dtgVencidos
        '
        Me.dtgVencidos.AllowUserToAddRows = False
        Me.dtgVencidos.AllowUserToResizeRows = False
        Me.dtgVencidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dtgVencidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtgVencidos.Location = New System.Drawing.Point(536, 251)
        Me.dtgVencidos.Name = "dtgVencidos"
        Me.dtgVencidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dtgVencidos.Size = New System.Drawing.Size(190, 118)
        Me.dtgVencidos.TabIndex = 19
        '
        'frm016
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(738, 419)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtgVencidos)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtgPorVencer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtgTracking)
        Me.Name = "frm016"
        Me.Text = "[016] Seguimieto  por región"
        CType(Me.dtgTracking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtgPorVencer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtgVencidos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtgTracking As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtgPorVencer As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtgVencidos As System.Windows.Forms.DataGridView
End Class
