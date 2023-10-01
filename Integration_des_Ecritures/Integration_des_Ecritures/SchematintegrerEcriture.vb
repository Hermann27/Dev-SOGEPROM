Imports System.Data.OleDb
Imports System.IO
Public Class SchematintegrerEcriture
    Private Sub SchematintegrerEcriture_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim BaseBool As Boolean
        Try
            BaseBool = Connected()
            AfficheFormat()
            AfficheSchemasIntegrer()
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AfficheFormat()
        Dim OleAdaptater As OleDbDataAdapter
        Dim OleAfficheDataset As DataSet
        Dim Oledatable As DataTable
        Dim i As Integer
        DataListeSchema.Rows.Clear()
        NomFormat.Items.Clear()
        Try
            OleAdaptater = New OleDbDataAdapter("select * from FORMATECRITURE", OleConnenection)
            OleAfficheDataset = New DataSet
            OleAdaptater.Fill(OleAfficheDataset)
            Oledatable = OleAfficheDataset.Tables(0)
            For i = 0 To Oledatable.Rows.Count - 1
                If Trim(Oledatable.Rows(i).Item("Nom_Format")) <> "" Then
                    NomFormat.Items.AddRange(New String() {Oledatable.Rows(i).Item("Nom_Format")})
                End If
            Next i
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AfficheSchemasIntegrer()
        Dim i As Integer
        Dim OleAdaptaterschema As OleDbDataAdapter
        Dim OleSchemaDataset As DataSet
        Dim OledatableSchema As DataTable
        DataListeIntegrer.Rows.Clear()
        Try
            OleAdaptaterschema = New OleDbDataAdapter("select * from PARAMECRITURE", OleConnenection)
            OleSchemaDataset = New DataSet
            OleAdaptaterschema.Fill(OleSchemaDataset)
            OledatableSchema = OleSchemaDataset.Tables(0)
            DataListeIntegrer.RowCount = OledatableSchema.Rows.Count
            For i = 0 To OledatableSchema.Rows.Count - 1
                DataListeIntegrer.Rows(i).Cells("NameFormat").Value = OledatableSchema.Rows(i).Item("Nom_Format")
                DataListeIntegrer.Rows(i).Cells("NomRepxpor").Value = OledatableSchema.Rows(i).Item("Nom_Fichier")
                DataListeIntegrer.Rows(i).Cells("CheminForma").Value = OledatableSchema.Rows(i).Item("Format")
                DataListeIntegrer.Rows(i).Cells("CheminRepexpor").Value = OledatableSchema.Rows(i).Item("Fichier")
            Next i
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Delete_DataListeSch()
        Dim i As Integer
        Dim OleAdaptaterDelete As OleDbDataAdapter
        Dim OleDeleteDataset As DataSet
        Dim OledatableDelete As DataTable
        Dim OleCommandDelete As OleDbCommand
        Dim DelFile As String
        Try
            For i = 0 To DataListeIntegrer.RowCount - 1
                If DataListeIntegrer.Rows(i).Cells("Supprimer").Value = True Then
                    OleAdaptaterDelete = New OleDbDataAdapter("select * from PARAMECRITURE where Format='" & DataListeIntegrer.Rows(i).Cells("CheminForma").Value & "' and Fichier='" & DataListeIntegrer.Rows(i).Cells("CheminRepexpor").Value & "'", OleConnenection)
                    OleDeleteDataset = New DataSet
                    OleAdaptaterDelete.Fill(OleDeleteDataset)
                    OledatableDelete = OleDeleteDataset.Tables(0)

                    If OledatableDelete.Rows.Count <> 0 Then
                        DelFile = "Delete From PARAMECRITURE where Format='" & DataListeIntegrer.Rows(i).Cells("CheminForma").Value & "' and Fichier='" & DataListeIntegrer.Rows(i).Cells("CheminRepexpor").Value & "'"
                        OleCommandDelete = New OleDbCommand(DelFile)
                        OleCommandDelete.Connection = OleConnenection
                        OleCommandDelete.ExecuteNonQuery()
                    End If
                End If
            Next i
            AfficheSchemasIntegrer()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub EnregistrerLeSchema()
        Dim n As Integer
        Dim OleAdaptaterEnreg As OleDbDataAdapter
        Dim OleEnregDataset As DataSet
        Dim OledatableEnreg As DataTable
        Dim OleCommandEnreg As OleDbCommand
        Dim Insert As Boolean = False
        Dim Insertion As String
        Try
            If DataListeSchema.RowCount >= 0 Then
                For n = 0 To DataListeSchema.RowCount - 1
                    OleAdaptaterEnreg = New OleDbDataAdapter("select * From PARAMECRITURE WHERE Format='" & DataListeSchema.Rows(n).Cells("Chemin").Value & "' and Fichier='" & DataListeSchema.Rows(n).Cells("CheminExport").Value & "'", OleConnenection)
                    OleEnregDataset = New DataSet
                    OleAdaptaterEnreg.Fill(OleEnregDataset)
                    OledatableEnreg = OleEnregDataset.Tables(0)
                    If OledatableEnreg.Rows.Count <> 0 Then
                    Else
                        If Trim(DataListeSchema.Rows(n).Cells("Chemin").Value) <> "" And Trim(DataListeSchema.Rows(n).Cells("CheminExport").Value) <> "" Then
                            Insertion = "Insert Into PARAMECRITURE (Format,Fichier,Nom_Format,Nom_Fichier) VALUES ('" & DataListeSchema.Rows(n).Cells("Chemin").Value & "','" & DataListeSchema.Rows(n).Cells("CheminExport").Value & "','" & Trim(DataListeSchema.Rows(n).Cells("NomFormat").Value) & "','" & DataListeSchema.Rows(n).Cells("DossierExport").Value & "')"
                            OleCommandEnreg = New OleDbCommand(Insertion)
                            OleCommandEnreg.Connection = OleConnenection
                            OleCommandEnreg.ExecuteNonQuery()
                            Insert = True
                        End If
                    End If
                Next n
                If Insert = True Then
                    MsgBox("Insertion Reussie", MsgBoxStyle.Information, "Insertion des Schemas d'integration")
                    DataListeSchema.Rows.Clear()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_Quit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Quit.Click
        Me.Close()
    End Sub
    Private Sub BT_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Save.Click
        Try
            EnregistrerLeSchema()
            AfficheSchemasIntegrer()
        Catch ex As Exception

        End Try
    End Sub
    
    Private Sub BT_DelRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_DelRow.Click
        Dim first As Integer
        Dim last As Integer
        Try
            first = DataListeSchema.Rows.GetFirstRow(DataGridViewElementStates.Displayed)
            last = DataListeSchema.Rows.GetLastRow(DataGridViewElementStates.Displayed)
            If last >= 0 Then
                If last - first >= 0 Then
                    DataListeSchema.Rows.RemoveAt(DataListeSchema.CurrentRow.Index)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_ADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_ADD.Click
        DataListeSchema.Rows.Add()
    End Sub
    Private Sub DataListeSchema_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeSchema.CellClick
        Dim NameFormat As String
        Try
            If e.RowIndex >= 0 Then
                If DataListeSchema.Columns(e.ColumnIndex).Name = "RechercheFichier" Then
                    FolderRepListeFile.Description = "Repertoire des Fichiers à traiter"
                    If FolderRepListeFile.ShowDialog = Windows.Forms.DialogResult.OK Then
                        NameFormat = Trim(FolderRepListeFile.SelectedPath)
                        Do While InStr(Trim(NameFormat), "\") <> 0
                            NameFormat = Strings.Right(NameFormat, Strings.Len(Trim(NameFormat)) - InStr(Trim(NameFormat), "\"))
                        Loop
                        DataListeSchema.Rows(e.RowIndex).Cells("CheminExport").Value = FolderRepListeFile.SelectedPath & "\"
                        DataListeSchema.Rows(e.RowIndex).Cells("DossierExport").Value = NameFormat
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataListeSchema_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeSchema.CellEndEdit
        Dim OleAdaptaterRecher As OleDbDataAdapter
        Dim OleRecherDataset As DataSet
        Dim OledatableRecher As DataTable
        Try
            If e.RowIndex >= 0 Then
                OleAdaptaterRecher = New OleDbDataAdapter("select * from FORMATECRITURE where Nom_Format='" & DataListeSchema.Rows(e.RowIndex).Cells("NomFormat").Value & "'", OleConnenection)
                OleRecherDataset = New DataSet
                OleAdaptaterRecher.Fill(OleRecherDataset)
                OledatableRecher = OleRecherDataset.Tables(0)
                If OledatableRecher.Rows.Count <> 0 Then
                    DataListeSchema.Rows(e.RowIndex).Cells("Chemin").Value = OledatableRecher.Rows(0).Item("Format")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub DataListeSchema_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeSchema.CellContentClick

    End Sub

    Private Sub BT_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Delete.Click
        Try
            Delete_DataListeSch()
        Catch ex As Exception

        End Try
    End Sub
End Class