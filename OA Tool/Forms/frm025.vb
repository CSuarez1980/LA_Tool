Public Class frm025
    Dim cn As New OAConnection.Connection
    Dim Tabla As New DataTable
    Dim Accion As Integer ' -> Variable para controlar la acción que realizará el usuario: Insert = 1 o Update = 2


    Private Sub AllowEdit()
        'Bloquea los botones el toolbar
        Me.cmdEliminar.Enabled = False
        Me.cmdGuardar.Enabled = True
        Me.cmdModificar.Enabled = False
        Me.cmdNuevo.Enabled = False

        'Bloqueo del DataGrid
        Me.dtgVariantes.Enabled = False

        'DesBloquea las cajas de texto
        Me.txtDescripcion.ReadOnly = False
        Me.txtNombre.ReadOnly = False
    End Sub

    Private Sub FinishEdit()
        'Desbloquea los botones el toolbar
        Me.cmdEliminar.Enabled = True
        Me.cmdGuardar.Enabled = False
        Me.cmdModificar.Enabled = True
        Me.cmdNuevo.Enabled = True

        'DesBloqueo del DataGrid
        Me.dtgVariantes.Enabled = True

        'Bloquea las cajas de texto
        Me.txtDescripcion.ReadOnly = True
        Me.txtNombre.ReadOnly = True
    End Sub

    Private Sub frm025_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName, BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
        EstableserData()
    End Sub

    Private Sub cmdNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        AllowEdit()
        Accion = 1
        Me.Text = Me.Text & " - [Crear nueva variante]"
    End Sub

    Private Sub cmdModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        AllowEdit()
        Accion = 2
        Me.Text = Me.Text & " - [Modificar variante]"
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        FinishEdit()
        Select Case Accion
            Case 1
                cn.ExecuteInServer("Insert Into HeaderVariante(TNumber,Nombre,Description,SAPBox,POFilter,BIDefault) Values('" & gsUsuarioPC & "','" & Me.txtNombre.Text & "','" & Me.txtDescripcion.Text & "','" & Me.cboSAPBox.SelectedValue.ToString & "',0,0)")

            Case 2
                cn.ExecuteInServer("Update HeaderVariante set Nombre = '" & Me.txtNombre.Text & "',Description = '" & Me.txtDescripcion.Text & "' Where IDVariante = " & Me.txtID.Text & " and TNumber = '" & gsUsuarioPC & "' And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'")
        End Select

        Accion = 0
        Me.Text = "[025] Variantes por Usuario"
        EstableserData()
    End Sub


    Private Sub EstableserData()

        Tabla = cn.RunSentence("Select * From HeaderVariante Where TNumber = '" & gsUsuarioPC & "' And SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'").Tables(0)

        Me.BindingSource1.DataSource = Tabla
        Me.dtgVariantes.DataSource = Me.BindingSource1

        Me.dtgVariantes.Columns("IDVariante").ReadOnly = True
        Me.dtgVariantes.Columns("TNumber").ReadOnly = True

        Me.txtID.DataBindings.Clear()
        Me.txtNombre.DataBindings.Clear()
        Me.txtDescripcion.DataBindings.Clear()

        Me.txtID.DataBindings.Add("Text", Me.BindingSource1, "IDVariante")
        Me.txtNombre.DataBindings.Add("Text", Me.BindingSource1, "Nombre")
        Me.txtDescripcion.DataBindings.Add("Text", Me.BindingSource1, "Description")
    End Sub

    Private Sub cmdEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click
        If MsgBox("Está seguro que desea eliminar la variante: " & Me.txtNombre.Text & "?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo, "Elimianr Variente") = MsgBoxResult.Yes Then
            cn.ExecuteInServer("Delete From HeaderVariante Where IDVariante = " & Me.txtID.Text & " And TNumber = '" & gsUsuarioPC & "'")
            EstableserData()
        End If
    End Sub

    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            EstableserData()
        End If
    End Sub
End Class