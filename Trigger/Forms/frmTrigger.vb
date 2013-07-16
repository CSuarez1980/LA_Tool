Imports System.Xml
Imports System.Windows.Forms

Public Class frmTrigger
    Dim cn As New OAConnection.Connection
    Dim TriggerInfo As DataTable
    Dim Tools As New Trigger.Tools
    Dim liColumnFrozen%

    Private Sub btnSapProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Mysap As New SAPConection.c_SAP(Me.txtUser.Text, Me.txtPwr.Text, Me.cboSAPBox.Text)

        Dim Plantas As New DataTable
        Dim TriggerData As DataTable

        Plantas = cn.GetActivesPlants.Tables(0)

        'Mysap.OpenConnection(SAPConfig)
        MsgBox("Favor contactar al administrador del sistema.")

        If Mysap.DownloadTrigger(Plantas) Then
            TriggerData = Tools.ImportTriggerData(Mysap.DownloadDirectory & "Trigger.txt")
        End If

        Mysap.CloseConnection()
        Mysap = Nothing

        Me.dtgTrigger.DataSource = TriggerData
        Me.txtMateriales.Text = TriggerData.Rows.Count

    End Sub

    Private Sub frmTrigger_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        WriteXML()
    End Sub

    Private Sub Trigger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Table As New DataTable

        Table = cn.RunSentence("Select BoxLongName From SAPBox").Tables(0)

        If Table.Rows.Count > 0 Then
            Dim i%
            For i = 0 To Table.Rows.Count - 1
                cboSAPBox.Items.Add(Table.Rows(i).Item("BoxLongName"))
            Next
        End If

        Table = Tools.GetTriggerTableStructure

        Me.dtgTrigger.DataSource = Table
        Me.dtgTrigger.Columns.Insert(0, cn.AddCheckBoxColumn("AutoPO", "AutoPO"))
        Me.dtgTrigger.Columns.Insert(1, cn.AddCheckBoxColumn("S_List", "S_List"))
        Me.dtgTrigger.Columns.Insert(2, cn.AddCheckBoxColumn("New_Contrato", "New_Contrato"))
        Me.dtgTrigger.Columns.Insert(2, cn.AddCheckBoxColumn("Acq_Req", "Acq_Req"))

        ReadXML()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearchTriggerFile.Click
        'Dim Mysap As New SAPConection.c_SAP(Me.txtUser.Text, Me.txtPwr.Text, Me.cboSAPBox.Text)
        'Dim Tabla As DataTable

        'Mysap.OpenConnection()
        'If Mysap.DownloadSourceList Then
        ' Dim Tools As New SAPConection.c_SAPTools

        'Tabla = Tools.ImportSourceListData(Mysap.DownloadDirectory & "SourceList.txt")

        'End If
        'Mysap.CloseConnection()

        Dim FileName$
        FileName = ""
        OpenFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        OpenFile.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (OpenFile.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFile.FileName
        End If

        If FileName <> "" Then
            Dim Trigger As New Trigger.Tools


            TriggerInfo = Trigger.ImportTriggerData(FileName)

            If TriggerInfo.Rows.Count > 0 Then
                Me.dtgTrigger.DataSource = TriggerInfo
                Me.txtMateriales.Text = TriggerInfo.Rows.Count
                Me.cmdVendor.Enabled = True
                Me.cmdExcel.Enabled = True
                Me.cmdRunScript.Enabled = True

            Else
                Me.cmdVendor.Enabled = False
                Me.cmdExcel.Enabled = False
                Me.cmdRunScript.Enabled = False
            End If

            LockGrid()
        End If

        ReadXML()
    End Sub

    Private Sub WriteXML()
        Dim config As New ConfigGrid

        config.Gica = Me.dtgTrigger.Columns("gica").DisplayIndex
        config.Material = Me.dtgTrigger.Columns("Material").DisplayIndex
        config.Planta = Me.dtgTrigger.Columns("Planta").DisplayIndex
        config.P_Org = Me.dtgTrigger.Columns("P_Org").DisplayIndex
        config.P_Grp = Me.dtgTrigger.Columns("P_Grp").DisplayIndex
        config.AutoPO = Me.dtgTrigger.Columns("AutoPO").DisplayIndex
        config.S_List = Me.dtgTrigger.Columns("S_List").DisplayIndex
        config.TaxCode = Me.dtgTrigger.Columns("TaxCode").DisplayIndex
        config.Acq_Req = Me.dtgTrigger.Columns("Acq_Req").DisplayIndex
        config.Manu_Name = Me.dtgTrigger.Columns("Manu_Name").DisplayIndex
        config.Part_Number = Me.dtgTrigger.Columns("Part_Number").DisplayIndex
        config.Vendor = Me.dtgTrigger.Columns("Vendor").DisplayIndex
        config.Contrato = Me.dtgTrigger.Columns("Contrato").DisplayIndex
        config.New_Contrato = Me.dtgTrigger.Columns("New_Contrato").DisplayIndex
        config.Precio = Me.dtgTrigger.Columns("Precio").DisplayIndex
        config.PDT = Me.dtgTrigger.Columns("PDT").DisplayIndex
        config.Currency = Me.dtgTrigger.Columns("Currency").DisplayIndex
        config.Dias = Me.dtgTrigger.Columns("Dias").DisplayIndex
        config.Mat_Origin = Me.dtgTrigger.Columns("Mat_Origin").DisplayIndex
        config.Control_Code = Me.dtgTrigger.Columns("Control_Code").DisplayIndex
        config.Info_Update = Me.dtgTrigger.Columns("Info_Update").DisplayIndex
        config.Mat_Group = Me.dtgTrigger.Columns("Mat_Group").DisplayIndex

        Dim writer As New System.Xml.Serialization.XmlSerializer(GetType(ConfigGrid))
        Dim file As New System.IO.StreamWriter(My.Application.Info.DirectoryPath & "\TriggerConfig.xml")
        writer.Serialize(file, config)
        file.Close()
    End Sub

    Public Sub ReadXML()
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\TriggerConfig.xml") Then
            Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(ConfigGrid))
            Dim file As New System.IO.StreamReader(My.Application.Info.DirectoryPath & "\TriggerConfig.xml")
            Dim MyConfig As ConfigGrid

            MyConfig = CType(reader.Deserialize(file), ConfigGrid)
            file.Close()

            Me.dtgTrigger.Columns("Gica").DisplayIndex = MyConfig.Gica
            Me.dtgTrigger.Columns("Material").DisplayIndex = MyConfig.Material
            Me.dtgTrigger.Columns("Planta").DisplayIndex = MyConfig.Planta
            Me.dtgTrigger.Columns("P_Org").DisplayIndex = MyConfig.P_Org
            Me.dtgTrigger.Columns("P_Grp").DisplayIndex = MyConfig.P_Grp
            Me.dtgTrigger.Columns("AutoPO").DisplayIndex = MyConfig.AutoPO
            Me.dtgTrigger.Columns("S_List").DisplayIndex = MyConfig.S_List
            Me.dtgTrigger.Columns("TaxCode").DisplayIndex = MyConfig.TaxCode
            Me.dtgTrigger.Columns("Acq_Req").DisplayIndex = MyConfig.Acq_Req
            Me.dtgTrigger.Columns("Manu_Name").DisplayIndex = MyConfig.Manu_Name
            Me.dtgTrigger.Columns("Part_Number").DisplayIndex = MyConfig.Part_Number
            Me.dtgTrigger.Columns("Vendor").DisplayIndex = MyConfig.Vendor
            Me.dtgTrigger.Columns("Contrato").DisplayIndex = MyConfig.Contrato
            Me.dtgTrigger.Columns("New_Contrato").DisplayIndex = MyConfig.New_Contrato
            Me.dtgTrigger.Columns("Precio").DisplayIndex = MyConfig.Precio
            Me.dtgTrigger.Columns("PDT").DisplayIndex = MyConfig.PDT
            Me.dtgTrigger.Columns("Currency").DisplayIndex = MyConfig.Currency
            Me.dtgTrigger.Columns("Dias").DisplayIndex = MyConfig.Dias
            Me.dtgTrigger.Columns("Mat_Origin").DisplayIndex = MyConfig.Mat_Origin
            Me.dtgTrigger.Columns("Control_Code").DisplayIndex = MyConfig.Control_Code
            Me.dtgTrigger.Columns("Info_Update").DisplayIndex = MyConfig.Info_Update
            Me.dtgTrigger.Columns("Mat_Group").DisplayIndex = MyConfig.Mat_Group
        End If
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        Dim FileName$

        FileName = ""
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialog.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = SaveFileDialog.FileName
        End If

        If FileName <> "" Then
            cn.ExportDataTableToXL(TriggerInfo, FileName)
        End If
    End Sub

    Private Sub frmTrigger_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgTrigger.Width = Me.Width - 25
        Me.dtgTrigger.Height = Me.Height - 150
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        WriteXML()
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ReadXML()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRunScript.Click
        Dim sap As New SAPConection.c_SAP(Me.txtUser.Text, Me.txtPwr.Text, Me.cboSAPBox.Text)
        Dim Mat As New Trigger.ConfigGrid
        Dim Cont As Integer = 0

        Me.dtgTrigger.EndEdit()

        'sap.OpenConnection(SAPConfig)
        MsgBox("Favor contactar al administrador del sistema.")

        If sap.PrepararTriggerForm Then
            For Cont = 0 To Me.dtgTrigger.Rows.Count - 1
                Mat.Gica = Me.dtgTrigger.Rows(Cont).Cells("gica").Value
                Mat.Material = Me.dtgTrigger.Rows(Cont).Cells("Material").Value
                Mat.Planta = Me.dtgTrigger.Rows(Cont).Cells("Planta").Value
                Mat.P_Org = Me.dtgTrigger.Rows(Cont).Cells("P_Org").Value
                Mat.P_Grp = Me.dtgTrigger.Rows(Cont).Cells("P_Grp").Value
                Mat.AutoPO = Me.dtgTrigger.Rows(Cont).Cells("AutoPO").Value
                Mat.S_List = Me.dtgTrigger.Rows(Cont).Cells("S_List").Value
                Mat.TaxCode = Me.dtgTrigger.Rows(Cont).Cells("TaxCode").Value
                Mat.Acq_Req = Me.dtgTrigger.Rows(Cont).Cells("Acq_Req").Value
                Mat.Manu_Name = Me.dtgTrigger.Rows(Cont).Cells("Manu_Name").Value
                Mat.Part_Number = Me.dtgTrigger.Rows(Cont).Cells("Part_Number").Value
                Mat.Vendor = Me.dtgTrigger.Rows(Cont).Cells("Vendor").Value
                Mat.Contrato = Me.dtgTrigger.Rows(Cont).Cells("Contrato").Value
                Mat.New_Contrato = Me.dtgTrigger.Rows(Cont).Cells("New_Contrato").Value
                Mat.Precio = Me.dtgTrigger.Rows(Cont).Cells("Precio").Value
                Mat.PDT = Me.dtgTrigger.Rows(Cont).Cells("PDT").Value
                Mat.Currency = Me.dtgTrigger.Rows(Cont).Cells("Currency").Value
                Mat.Dias = Me.dtgTrigger.Rows(Cont).Cells("Dias").Value
                Mat.Mat_Origin = Me.dtgTrigger.Rows(Cont).Cells("Mat_Origin").Value
                Mat.Control_Code = Me.dtgTrigger.Rows(Cont).Cells("Control_Code").Value
                Mat.Info_Update = Me.dtgTrigger.Rows(Cont).Cells("Info_Update").Value
                Mat.Mat_Group = Me.dtgTrigger.Rows(Cont).Cells("Mat_Group").Value
                Mat.ErrorMessage = ""

                If Not sap.UploadValuesOnTrigger(Mat, cn.GetUserId) Then
                    'Me.dtgTrigger.Rows(Cont).Visible = False
                    Me.dtgTrigger.Rows(Cont).DefaultCellStyle.BackColor = Drawing.Color.LightSalmon
                    Me.dtgTrigger.Rows(Cont).Cells("ErrorMessage").Value = Mat.ErrorMessage
                End If

            Next
            sap.SaveTriggerData()

        Else
            MsgBox("Error al intentar ingresar a Trigger.", MsgBoxStyle.Critical)
        End If

        sap.CloseConnection()

    End Sub

    Private Sub cmdVendor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVendor.Click
        Dim I As Integer
        Dim cn As New OAConnection.Connection
        Dim Contratos As DataTable

        For I = 0 To Me.dtgTrigger.Rows.Count - 1
            If Me.dtgTrigger.Rows(I).Cells("Vendor").Value.Trim <> "" Then


                Contratos = cn.RunSentence("Select top 1 OA From vstContratos Where Vendor = " & Me.dtgTrigger.Rows(I).Cells("Vendor").Value & " and Planta = '" & Me.dtgTrigger.Rows(I).Cells("Planta").Value & "'").Tables(0)
                If Contratos.Rows.Count > 0 Then
                    Me.dtgTrigger.Rows(I).Cells("Contrato").Value = Contratos.Rows(0).Item("OA")
                End If
                'If Double.TryParse(cn.RunSentence("Select top 1 OA From vstContratos Where Vendor = " & Me.dtgTrigger.Rows(I).Cells("Vendor").Value & " and Planta = '" & Me.dtgTrigger.Rows(I).Cells("Planta").Value & "'").Tables(0).Rows(0).Item(0), OA) Then

                '          End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' Bloquea una columna del grid
    ''' </summary>
    ''' <param name="ColumnName">Nombre de la columna que va a ser bloqueada</param>
    ''' <remarks></remarks>
    Private Sub LockColumn(ByVal ColumnName$)
        Dim i As Integer

        For i = 0 To Me.dtgTrigger.Rows.Count - 1
            Me.dtgTrigger.Rows(i).Cells(ColumnName).ReadOnly = True
            Me.dtgTrigger.Rows(i).Cells(ColumnName).Style.BackColor = Drawing.Color.LightGray
        Next

    End Sub

    Private Sub LockGrid()
        LockColumn("Gica")
        LockColumn("Material")
        LockColumn("Planta")
        LockColumn("Manu_Name")
        LockColumn("Part_Number")
        LockColumn("Currency")
        LockColumn("Mat_Group")
    End Sub

    Private Sub dtgTrigger_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dtgTrigger.ColumnHeaderMouseClick
        LockGrid()
    End Sub

    Private Sub cmdFreese_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFreese.Click

        If Me.cmdFreese.Checked Then
            Me.dtgTrigger.Columns(liColumnFrozen).Frozen = False
        End If

        Dim i%
        Dim j%
        Dim Found As Boolean

        Found = False
        For i = 0 To Me.dtgTrigger.Rows.Count - 1
            If Found Then
                Exit For
            End If

            For j = 0 To Me.dtgTrigger.Columns.Count - 1
                If Not Found Then
                    If Me.dtgTrigger.Rows(i).Cells(j).Selected Then
                        liColumnFrozen = j
                        Found = True
                    End If
                End If
            Next
        Next

        Me.dtgTrigger.Columns(liColumnFrozen).Frozen = True
    End Sub
End Class