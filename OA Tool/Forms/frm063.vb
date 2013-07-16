Public Class frm063
    Private cn As New OAConnection.Connection
    Private Table As New DataTable
    Const sSAP As String = "L7P"

    Private Sub txtOA_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOA.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim OA As New DataTable

            Clear()
            OA = cn.RunSentence("SELECT OA, Planta, Moneda, Vendor, Name, Inicio, Fin, PG, PO FROM vstContratos Where OA = '" & Me.txtOA.Text & "'").Tables(0)

            If OA.Rows.Count > 0 Then
                Me.txtPGrp.Text = OA.Rows(0)("PG")
                Me.txtPlant.Text = OA.Rows(0)("Planta")
                Me.txtPOrg.Text = OA.Rows(0)("PO")
                Me.txtValEnd.Text = OA.Rows(0)("Fin")
                Me.txtValStart.Text = OA.Rows(0)("Inicio")
                Me.txtVendor.Text = OA.Rows(0)("Vendor")
                Me.txtVendorName.Text = OA.Rows(0)("Name")
                Me.txtCurrency.Text = OA.Rows(0)("Moneda")
                Me.dtgMateriales.ReadOnly = False
                Me.dtgMateriales.Enabled = True
            Else
                MsgBox("Outline Agreement number not found.", MsgBoxStyle.Exclamation)
                Me.dtgMateriales.ReadOnly = True
                Me.dtgMateriales.Enabled = False
            End If

        End If
    End Sub

    Private Sub Clear()
        Me.txtPGrp.Text = ""
        Me.txtPlant.Text = ""
        Me.txtPOrg.Text = ""
        Me.txtValEnd.Text = ""
        Me.txtValStart.Text = ""
        Me.txtVendor.Text = ""

        Table.Clear()
    End Sub


    Private Sub frm063_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Table = cn.RunSentence("Select * from tmpIncludeMaterials").Tables(0)

        Me.BS.DataSource = Table
        Me.dtgMateriales.DataSource = BS

    End Sub

    Private Sub dtgMateriales_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgMateriales.CellEndEdit

        dtgMateriales.EndEdit()
        If Not DBNull.Value.Equals(dtgMateriales.CurrentCell.Value) AndAlso dtgMateriales.CurrentRow.Cells("Material").Value <> "" AndAlso dtgMateriales.Columns(dtgMateriales.CurrentCell.ColumnIndex).Name = "Material" Then
            Dim dtMt As New DataTable

            dtMt = cn.RunSentence("Select OA, Item  From DetalleContrato Where Material = " & dtgMateriales.CurrentCell.Value & " And Planta = '" & Me.txtPlant.Text & "'").Tables(0)

            If dtMt.Rows.Count > 0 Then
                Dim OAs As String = ""
                Dim row As DataRow

                For Each row In dtMt.Rows
                    OAs = Chr(13) & row("OA") & " - " & row("Item")
                Next

                OAs = "The material: " & dtgMateriales.CurrentCell.Value & " is already on OAs: " & Chr(13) & OAs & Chr(13) & Chr(13) & "Please first remove the material from others OAs and try again."

                MsgBox(OAs, MsgBoxStyle.Exclamation)
                If Not chkForceInclude.Checked Then
                    dtgMateriales.CurrentCell.Value = ""
                End If

            End If
        End If
    End Sub

    Private Sub frm063_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgMateriales.Width = Me.Width - 30
        Me.dtgMateriales.Height = Me.Height - 200
    End Sub

    Private Sub cmdRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRun.Click

        Dim row As DataRow
        Dim r2 As DataRow
        Dim er As Boolean = False
        Dim message As String = ""
        dtgMateriales.EndEdit()
        BS.EndEdit()



        For Each row In Table.Rows
            Dim iOA As New SAPCOM.OAChanges(sSAP, gsUsuarioPC)
            iOA.OANumber = Me.txtOA.Text

            'iOA.AddMaterial(row("Material"), "10000", row("Price"), row("PDT"), Me.txtPlant.Text, "0001", "")
            iOA.CommitChanges()

            If Not iOA.Success Then
                er = True
                Dim m As String

                For Each m In iOA.Results
                    message = message & m & Chr(13)
                Next
            End If
            'iOA.EndSession()
        Next

        If Not er Then
            Dim rOA As New SAPCOM.EKPO_Report(sSAP, gsUsuarioPC, AppId)

            rOA.IncludeDocument(Me.txtOA.Text)
            rOA.DeletionIndicator = False
            rOA.Execute()
            If rOA.Success Then
                If rOA.ErrMessage = Nothing Then
                    For Each row In Table.Rows
                        For Each r2 In rOA.Data.Rows
                            If row("Material") = r2("Material") Then
                                row("New Item") = r2("Item Number")
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If


            'Start SAP GUI:
            Dim SAP As New SAPConection.SAPGUI(sSAP, gsUsuarioPC, "", True, SAPConfig)

            'Load tools assambly:
            Dim SAPTool As New SAPConection.SAPTools

            'Generate the CSV File:
            CreateCSVFile()

            SAPTool.SyncOA_MD(SAP, "OA_CSV.CSV")

            'Generate the source list
            For Each row In Table.Rows
                If Not DBNull.Value.Equals(row("New Item")) Then
                    If row("Auto PO") Then ' If autopo column is checked then set the master data auto po and source list check and also generate the oa source list
                        SAPTool.FixMasterDataChecks(SAP, row("Material"), row("Plant"))
                        SAPTool.FixSourceListOA(SAP, row("Material"), Me.txtPlant.Text, Me.txtValStart.Text, Me.txtValEnd.Text, Me.txtVendor.Text, Me.txtPOrg.Text, Me.txtOA.Text, row("New Item"))
                    End If
                End If
            Next

            SAP.Close()
            message = "Done."
        Else
            message = "Errors was found while trying to include materials: " & Chr(13) & Chr(13) & message
        End If

        MsgBox(message, MsgBoxStyle.Information)

    End Sub

    Private Sub CreateCSVFile()
        Const File As String = "C:\OA_CSV.CSV"
        Dim sw As New System.IO.StreamWriter(File)

        sw.WriteLine("Outline Agreement Price/Text Update,,,,,,,,,,,,,,,,,,,")
        sw.WriteLine(",,,,,,,,,,,,,,,,,,,")
        sw.WriteLine("Vendor Number," & txtVendor.Text & ",," & txtVendorName.Text & ",,,,,,,,,,,,,,,,")
        sw.WriteLine("Purchasing Org.," & Me.txtPOrg.Text & ",,IMPORTANT: DO NOT USE COMMAS OR QUOTES WHEN ENTERING DATA,,,,,,,,,,,,,,,,")
        sw.WriteLine("Outline Agreement," & Me.txtOA.Text & ",,,,,,,,,,,,,,,,,,")
        sw.WriteLine(",,,,,,,,,,,,,,,,,,,")
        sw.WriteLine(",SAP Material,Lead,Current,New Target,Creation,Price,Condition,New,Effective,Current,Non SAP Plt,Vendor Material,Preferred Manufacturer,Preferred Manufacturer,Preferred Manufacturer,Preferred Manufacturer,Preferred Manufacturer,Preferred Manufacturer,Preferred Manufacturer")
        sw.WriteLine("SAP Material #,Description,Time,Target Qty,Qty,Date,Condition,Description,Price,Date,Price,Release Qty,Number,Name,Material #,Text Field #1,Text Field #2,Text Field #3,Text Field #4,Text Field #5")
        Dim Row As DataRow

        For Each Row In Table.Rows
            If Not DBNull.Value.Equals(Row("New Item")) Then
                sw.WriteLine(Row("Material") & ",," & Row("PDT") & ",10000,10000,,PB00,Gross Price," & Row("Price") & "," & Microsoft.VisualBasic.Format(Now.Date.AddDays(1), "MM/dd/yy") & ",,,,,,,,,")
            End If
        Next

        sw.Close()

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CreateCSVFile()

        Dim SAP As New SAPConection.SAPGUI(sSAP, gsUsuarioPC, "", True, SAPConfig)
        Dim SAPTool As New SAPConection.SAPTools
        Dim Row As DataRow

        dtgMateriales.EndEdit()
        BS.EndEdit()


        For Each Row In Table.Rows
            If Not DBNull.Value.Equals(Row("New Item")) Then
                SAPTool.FixSourceListOA(SAP, Row("Material"), Me.txtPlant.Text, Me.txtValStart.Text, Me.txtValEnd.Text, Me.txtVendor.Text, Me.txtPOrg.Text, Me.txtOA.Text, Row("New Item"))

            End If
        Next




        SAP.Close()

        MsgBox("Done!")

    End Sub
End Class