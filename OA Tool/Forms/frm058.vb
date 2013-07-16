Public Class frm058

    Dim Access As New Access.Application
    Dim cn As New OAConnection.Connection


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim VCode As New DataTable


        If Me.txtVendorCode.Text.Length = 0 Then
            MsgBox("Please type vendor code.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If



        VCode = cn.RunSentence("Select Vendor, Name From VendorsG11 Where Vendor = '" & Me.txtVendorCode.Text & "'").Tables(0)
        If VCode.Rows.Count > 0 Then

            SaveFileDialog1.Filter = "Excel Files|*.xls"
            SaveFileDialog1.Title = "Save Blue Form to:"
            SaveFileDialog1.FileName = Me.txtVendorCode.Text & " - " & VCode.Rows(0)("Name")
            SaveFileDialog1.ShowDialog()

            If SaveFileDialog1.FileName <> "" Then
                If Len(Dir(SaveFileDialog1.FileName)) > 0 Then
                    If MsgBox("File already exist!." & Chr(13) & Chr(13) & "  Do you want to replace it?", vbQuestion + vbYesNo, "Replace file?") = vbYes Then
                        Kill(SaveFileDialog1.FileName)
                    Else
                        MsgBox("User stop the process.", vbExclamation, "Canceled")
                        Exit Sub
                    End If
                End If
                Access.Run("GetNewBRBlueForm", Me.txtVendorCode.Text, SaveFileDialog1.FileName)
            Else
                MsgBox("Please select the file name.", MsgBoxStyle.Exclamation)
            End If

            MsgBox("Done!")
        Else
            MsgBox("Vendor code not found.", MsgBoxStyle.Information, "Vendor not found")
        End If


    End Sub

    Private Sub frm058_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Try
            Access.CloseCurrentDatabase()
            Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
            cn.KillProcess("MSAccess")
            cn.KillProcess("Excel")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frm058_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")
    End Sub
End Class