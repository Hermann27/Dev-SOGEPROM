Imports System.IO
Imports System.Data.OleDb
Public Class AjouterUnFormatTiers
    Private Sub DataListeFormat_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub BT_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Del.Click
        DataListeFormat.Rows.Add()
    End Sub

    Private Sub BT_ADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_ADD.Click
        Dim first As Integer
        Dim last As Integer
        first = DataListeFormat.Rows.GetFirstRow(DataGridViewElementStates.Displayed)
        last = DataListeFormat.Rows.GetLastRow(DataGridViewElementStates.Displayed)
        If last >= 0 Then
            If last - first >= 0 Then
                DataListeFormat.Rows.RemoveAt(DataListeFormat.CurrentRow.Index)
            End If
        End If
    End Sub

    Private Sub DataListeFormat_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeFormat.CellClick
        Dim NameFormat As String
        If e.RowIndex >= 0 Then
            If DataListeFormat.Columns(e.ColumnIndex).Name = "Fichier" Then
                OpenFileFormat.Filter = "Fichier texte (*.Xml)|*.Xml"
                OpenFileFormat.FileName = Nothing
                If OpenFileFormat.ShowDialog = Windows.Forms.DialogResult.OK Then
                    NameFormat = Trim(OpenFileFormat.FileName)
                    Do While InStr(Trim(NameFormat), "\") <> 0
                        NameFormat = Strings.Right(NameFormat, Strings.Len(Trim(NameFormat)) - InStr(Trim(NameFormat), "\"))
                    Loop
                    DataListeFormat.Rows(e.RowIndex).Cells("Chemin").Value = OpenFileFormat.FileName
                    DataListeFormat.Rows(e.RowIndex).Cells("Format").Value = NameFormat
                End If
            End If
        End If
    End Sub
    Private Sub AjouterUnFormatTiers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim BaseBool As Boolean
        BaseBool = Connected()
        DataListeFormat.Rows.Clear()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub AjouterUnFormat_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        BT_Quit.Location = New Point(SplitContainer1.Panel2.Width / 2 - 20, SplitContainer1.Panel2.Height / 2 - 10)
        BT_Save.Location = New Point(SplitContainer1.Panel2.Width / 2 + 100, SplitContainer1.Panel2.Height / 2 - 10)

    End Sub
    Private Sub BT_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Save.Click
        Dim n As Integer
        Dim SaveFormat As String
        Dim OleCommandSave As OleDbCommand
        Dim OleAdaptaterFormat As OleDbDataAdapter
        Dim OleFormatDataset As DataSet
        Dim OledatableFormat As DataTable
        Try
            For n = 0 To DataListeFormat.RowCount - 1
                OleAdaptaterFormat = New OleDbDataAdapter("select * from FORMATIERS where Nom_Format='" & Trim(DataListeFormat.Rows(n).Cells("Format").Value) & "'", OleConnenection)
                OleFormatDataset = New DataSet
                OleAdaptaterFormat.Fill(OleFormatDataset)
                OledatableFormat = OleFormatDataset.Tables(0)
                If OledatableFormat.Rows.Count = 0 Then
                    SaveFormat = "Insert Into FORMATIERS (Format,Nom_Format) VALUES ('" & DataListeFormat.Rows(n).Cells("Chemin").Value & "','" & DataListeFormat.Rows(n).Cells("Format").Value & "')"
                    OleCommandSave = New OleDbCommand(SaveFormat)
                    OleCommandSave.Connection = OleConnenection
                    OleCommandSave.ExecuteNonQuery()
                End If
            Next n
            MsgBox("Format Crée", MsgBoxStyle.Information, "Ajouter un Format")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub BT_Quit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Quit.Click
        Me.Close()
    End Sub
End Class