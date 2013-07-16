<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm026
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm026))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.cmdGuardar = New System.Windows.Forms.ToolStripButton
        Me.cmdCopyComment = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdCancelar = New System.Windows.Forms.ToolStripButton
        Me.txtComentarios = New System.Windows.Forms.TextBox
        Me.lblRequisicion = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtRequisicion = New System.Windows.Forms.TextBox
        Me.txtReqItem = New System.Windows.Forms.TextBox
        Me.txtGica = New System.Windows.Forms.TextBox
        Me.txtMaterial = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtPlanta = New System.Windows.Forms.TextBox
        Me.lblPlanta = New System.Windows.Forms.Label
        Me.txtMatGrp = New System.Windows.Forms.TextBox
        Me.lblMatGrp = New System.Windows.Forms.Label
        Me.txtPGrp = New System.Windows.Forms.TextBox
        Me.lblPurchGrp = New System.Windows.Forms.Label
        Me.txtPOrg = New System.Windows.Forms.TextBox
        Me.lblPurchOrg = New System.Windows.Forms.Label
        Me.txtSAPBox = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdGuardar, Me.cmdCopyComment, Me.ToolStripSeparator1, Me.cmdCancelar})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(618, 39)
        Me.ToolStrip1.TabIndex = 0
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
        Me.cmdGuardar.ToolTipText = " Guardar Comentario"
        '
        'cmdCopyComment
        '
        Me.cmdCopyComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCopyComment.Image = CType(resources.GetObject("cmdCopyComment.Image"), System.Drawing.Image)
        Me.cmdCopyComment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCopyComment.Name = "cmdCopyComment"
        Me.cmdCopyComment.Size = New System.Drawing.Size(36, 36)
        Me.cmdCopyComment.Text = "ToolStripButton1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'cmdCancelar
        '
        Me.cmdCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCancelar.Image = CType(resources.GetObject("cmdCancelar.Image"), System.Drawing.Image)
        Me.cmdCancelar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(36, 36)
        Me.cmdCancelar.Text = "ToolStripButton1"
        Me.cmdCancelar.ToolTipText = "Eliminar comentario"
        '
        'txtComentarios
        '
        Me.txtComentarios.Location = New System.Drawing.Point(9, 195)
        Me.txtComentarios.Multiline = True
        Me.txtComentarios.Name = "txtComentarios"
        Me.txtComentarios.Size = New System.Drawing.Size(597, 153)
        Me.txtComentarios.TabIndex = 8
        '
        'lblRequisicion
        '
        Me.lblRequisicion.AutoSize = True
        Me.lblRequisicion.Location = New System.Drawing.Point(8, 49)
        Me.lblRequisicion.Name = "lblRequisicion"
        Me.lblRequisicion.Size = New System.Drawing.Size(62, 13)
        Me.lblRequisicion.TabIndex = 7
        Me.lblRequisicion.Text = "Requisicion"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Material"
        '
        'txtRequisicion
        '
        Me.txtRequisicion.Location = New System.Drawing.Point(76, 45)
        Me.txtRequisicion.Name = "txtRequisicion"
        Me.txtRequisicion.ReadOnly = True
        Me.txtRequisicion.Size = New System.Drawing.Size(122, 20)
        Me.txtRequisicion.TabIndex = 9
        '
        'txtReqItem
        '
        Me.txtReqItem.Location = New System.Drawing.Point(200, 45)
        Me.txtReqItem.Name = "txtReqItem"
        Me.txtReqItem.ReadOnly = True
        Me.txtReqItem.Size = New System.Drawing.Size(47, 20)
        Me.txtReqItem.TabIndex = 10
        '
        'txtGica
        '
        Me.txtGica.Location = New System.Drawing.Point(76, 91)
        Me.txtGica.Name = "txtGica"
        Me.txtGica.ReadOnly = True
        Me.txtGica.Size = New System.Drawing.Size(102, 20)
        Me.txtGica.TabIndex = 11
        '
        'txtMaterial
        '
        Me.txtMaterial.AutoSize = True
        Me.txtMaterial.Location = New System.Drawing.Point(181, 95)
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.Size = New System.Drawing.Size(56, 13)
        Me.txtMaterial.TabIndex = 12
        Me.txtMaterial.Text = "<Material>"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtPlanta)
        Me.GroupBox1.Controls.Add(Me.lblPlanta)
        Me.GroupBox1.Controls.Add(Me.txtMatGrp)
        Me.GroupBox1.Controls.Add(Me.lblMatGrp)
        Me.GroupBox1.Controls.Add(Me.txtPGrp)
        Me.GroupBox1.Controls.Add(Me.lblPurchGrp)
        Me.GroupBox1.Controls.Add(Me.txtPOrg)
        Me.GroupBox1.Controls.Add(Me.lblPurchOrg)
        Me.GroupBox1.Controls.Add(Me.txtSAPBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtMaterial)
        Me.GroupBox1.Controls.Add(Me.txtGica)
        Me.GroupBox1.Controls.Add(Me.txtReqItem)
        Me.GroupBox1.Controls.Add(Me.txtRequisicion)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblRequisicion)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(600, 121)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Comentario:"
        '
        'txtPlanta
        '
        Me.txtPlanta.Location = New System.Drawing.Point(76, 68)
        Me.txtPlanta.Name = "txtPlanta"
        Me.txtPlanta.ReadOnly = True
        Me.txtPlanta.Size = New System.Drawing.Size(102, 20)
        Me.txtPlanta.TabIndex = 24
        '
        'lblPlanta
        '
        Me.lblPlanta.AutoSize = True
        Me.lblPlanta.Location = New System.Drawing.Point(8, 72)
        Me.lblPlanta.Name = "lblPlanta"
        Me.lblPlanta.Size = New System.Drawing.Size(37, 13)
        Me.lblPlanta.TabIndex = 23
        Me.lblPlanta.Text = "Planta"
        '
        'txtMatGrp
        '
        Me.txtMatGrp.Location = New System.Drawing.Point(472, 67)
        Me.txtMatGrp.Name = "txtMatGrp"
        Me.txtMatGrp.ReadOnly = True
        Me.txtMatGrp.Size = New System.Drawing.Size(122, 20)
        Me.txtMatGrp.TabIndex = 20
        '
        'lblMatGrp
        '
        Me.lblMatGrp.AutoSize = True
        Me.lblMatGrp.Location = New System.Drawing.Point(404, 71)
        Me.lblMatGrp.Name = "lblMatGrp"
        Me.lblMatGrp.Size = New System.Drawing.Size(51, 13)
        Me.lblMatGrp.TabIndex = 19
        Me.lblMatGrp.Text = "Mat. Grp."
        '
        'txtPGrp
        '
        Me.txtPGrp.Location = New System.Drawing.Point(472, 45)
        Me.txtPGrp.Name = "txtPGrp"
        Me.txtPGrp.ReadOnly = True
        Me.txtPGrp.Size = New System.Drawing.Size(122, 20)
        Me.txtPGrp.TabIndex = 18
        '
        'lblPurchGrp
        '
        Me.lblPurchGrp.AutoSize = True
        Me.lblPurchGrp.Location = New System.Drawing.Point(404, 49)
        Me.lblPurchGrp.Name = "lblPurchGrp"
        Me.lblPurchGrp.Size = New System.Drawing.Size(37, 13)
        Me.lblPurchGrp.TabIndex = 17
        Me.lblPurchGrp.Text = "P. Grp"
        '
        'txtPOrg
        '
        Me.txtPOrg.Location = New System.Drawing.Point(472, 23)
        Me.txtPOrg.Name = "txtPOrg"
        Me.txtPOrg.ReadOnly = True
        Me.txtPOrg.Size = New System.Drawing.Size(122, 20)
        Me.txtPOrg.TabIndex = 16
        '
        'lblPurchOrg
        '
        Me.lblPurchOrg.AutoSize = True
        Me.lblPurchOrg.Location = New System.Drawing.Point(404, 27)
        Me.lblPurchOrg.Name = "lblPurchOrg"
        Me.lblPurchOrg.Size = New System.Drawing.Size(37, 13)
        Me.lblPurchOrg.TabIndex = 15
        Me.lblPurchOrg.Text = "P. Org"
        '
        'txtSAPBox
        '
        Me.txtSAPBox.Location = New System.Drawing.Point(76, 23)
        Me.txtSAPBox.Name = "txtSAPBox"
        Me.txtSAPBox.ReadOnly = True
        Me.txtSAPBox.Size = New System.Drawing.Size(102, 20)
        Me.txtSAPBox.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "SAP Box"
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(82, 169)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(177, 20)
        Me.txtStatus.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 172)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Status"
        '
        'frm026
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(618, 363)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtComentarios)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm026"
        Me.Text = "[026] Comentarios de Requisiciones"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdGuardar As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtComentarios As System.Windows.Forms.TextBox
    Friend WithEvents lblRequisicion As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRequisicion As System.Windows.Forms.TextBox
    Friend WithEvents txtReqItem As System.Windows.Forms.TextBox
    Friend WithEvents txtGica As System.Windows.Forms.TextBox
    Friend WithEvents txtMaterial As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdCancelar As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtSAPBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPlanta As System.Windows.Forms.TextBox
    Friend WithEvents lblPlanta As System.Windows.Forms.Label
    Friend WithEvents txtMatGrp As System.Windows.Forms.TextBox
    Friend WithEvents lblMatGrp As System.Windows.Forms.Label
    Friend WithEvents txtPGrp As System.Windows.Forms.TextBox
    Friend WithEvents lblPurchGrp As System.Windows.Forms.Label
    Friend WithEvents txtPOrg As System.Windows.Forms.TextBox
    Friend WithEvents lblPurchOrg As System.Windows.Forms.Label
    Friend WithEvents cmdCopyComment As System.Windows.Forms.ToolStripButton
End Class
