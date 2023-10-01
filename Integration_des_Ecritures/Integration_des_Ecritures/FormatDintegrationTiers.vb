Imports Objets100Lib
Imports System
Imports System.Data.OleDb
Imports System.Collections
Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Imports Microsoft.VisualBasic
Public Class FormatDintegrationTiers
    Public Libel As String
    Public CellCol As Integer
    Public Index As Integer
    Public IndexPrec As Integer
    Public Valeur As Integer
    Private Sub AfficheColDispo()
        Dim i As Integer
        Dim OleAdaptater As OleDbDataAdapter
        Dim OleAfficheDataset As DataSet
        Dim Oledatable As DataTable
        DataDispo.Rows.Clear()
        Try
            OleAdaptater = New OleDbDataAdapter("select * from COTIERS", OleConnenection)
            OleAfficheDataset = New DataSet
            OleAdaptater.Fill(OleAfficheDataset)
            Oledatable = OleAfficheDataset.Tables(0)
            DataDispo.RowCount = Oledatable.Rows.Count
            For i = 0 To Oledatable.Rows.Count - 1
                DataDispo.Rows(i).Cells("ColDispo").Value = Oledatable.Rows(i).Item("Libelle")
                DataDispo.Rows(i).Cells("LibelleDispo").Value = Oledatable.Rows(i).Item("Colonne")
            Next i

        Catch ex As Exception

        End Try
    End Sub
    Private Sub CrerUnFormatColParametre()
        Dim j As Integer
        Dim SaveFormat As String
        Dim NameFormat() As String
        Dim OleAdaptaterFormat As OleDbDataAdapter
        Dim OleFormatDataset As DataSet
        Dim OledatableFormat As DataTable
        Dim OleCommandSave As OleDbCommand
        SaveFileXml.Filter = "Fichier Xml (*.Xml)|*.Xml"
        SaveFileXml.Title = "Enregistrer le Format d'integration"
        SaveFileXml.FileName = Cmb_Format.Text
        Try
            If SaveFileXml.ShowDialog = Windows.Forms.DialogResult.OK Then
                If SaveFileXml.FileName <> "" Then
                    NameFormat = Split(Trim(SaveFileXml.FileName), "\")
                    OleAdaptaterFormat = New OleDbDataAdapter("select * from FORMATIERS where Nom_Format='" & Trim(NameFormat(UBound(NameFormat))) & "'", OleConnenection)
                    OleFormatDataset = New DataSet
                    OleAdaptaterFormat.Fill(OleFormatDataset)
                    OledatableFormat = OleFormatDataset.Tables(0)
                    If OledatableFormat.Rows.Count = 0 Then
                        SaveFileXml.CheckPathExists = True
                        Dim Filexml As New XmlTextWriter(SaveFileXml.FileName, Nothing)
                        Filexml.WriteStartDocument()
                        Filexml.WriteStartElement("FORMAT_TIERDEL")
                        For j = 0 To DataSelect.RowCount - 1
                            Filexml.WriteStartElement("ColUse")
                            Filexml.WriteString(CStr(DataSelect.Rows(j).Cells("Colonne").Value))
                            Filexml.WriteEndElement()

                            Filexml.WriteStartElement("Delimiter")
                            Filexml.WriteString(CStr(Trim(Cmb_Del.Text)))
                            Filexml.WriteEndElement()

                            Filexml.WriteStartElement("Libelle")
                            Filexml.WriteString(CStr(DataSelect.Rows(j).Cells("Libelle").Value))
                            Filexml.WriteEndElement()
                            If IsNumeric(DataSelect.Rows(j).Cells("Positions").Value) Then
                                Filexml.WriteStartElement("iPosLeft")
                                Filexml.WriteString(CInt(DataSelect.Rows(j).Cells("Positions").Value))
                                Filexml.WriteEndElement()
                            Else
                                Filexml.WriteStartElement("iPosLeft")
                                Filexml.WriteString(CInt("0"))
                                Filexml.WriteEndElement()
                            End If
                            If DataSelect.Rows(j).Cells("Info").Value = True Then
                                Filexml.WriteStartElement("Info")
                                Filexml.WriteString("oui")
                                Filexml.WriteEndElement()
                            Else
                                Filexml.WriteStartElement("Info")
                                Filexml.WriteString("non")
                                Filexml.WriteEndElement()
                            End If
                            If DataSelect.Rows(j).Cells("Defaut").Value = True Then
                                Filexml.WriteStartElement("Defaut")
                                Filexml.WriteString("1")
                                Filexml.WriteEndElement()
                            Else
                                Filexml.WriteStartElement("Defaut")
                                Filexml.WriteString("0")
                                Filexml.WriteEndElement()
                            End If
                            Filexml.WriteStartElement("ValeurDefaut")
                            Filexml.WriteString(DataSelect.Rows(j).Cells("ValeurDefaut").Value)
                            Filexml.WriteEndElement()

                            Filexml.WriteStartElement("Decalage")
                            Filexml.WriteString(NumUpDown.Value)
                            Filexml.WriteEndElement()
                        Next j
                        Filexml.WriteEndElement()
                        Filexml.Close()
                        SaveFormat = "Insert Into FORMATIERS (Format,Nom_Format) VALUES ('" & SaveFileXml.FileName & "','" & Trim(NameFormat(UBound(NameFormat))) & "')"
                        OleCommandSave = New OleDbCommand(SaveFormat)
                        OleCommandSave.Connection = OleConnenection
                        OleCommandSave.ExecuteNonQuery()
                    Else
                        If OledatableFormat.Rows.Count <> 0 And SaveFileXml.FileName = OledatableFormat.Rows(0).Item("Format") Then
                            SaveFileXml.CheckPathExists = True
                            File.Delete(SaveFileXml.FileName)
                            Dim Filexml As New XmlTextWriter(SaveFileXml.FileName, Nothing)
                            Filexml.WriteStartDocument()
                            Filexml.WriteStartElement("FORMAT_TIERDEL")
                            For j = 0 To DataSelect.RowCount - 1

                                Filexml.WriteStartElement("ColUse")
                                Filexml.WriteString(CStr(DataSelect.Rows(j).Cells("Colonne").Value))
                                Filexml.WriteEndElement()

                                Filexml.WriteStartElement("Delimiter")
                                Filexml.WriteString(CStr(Trim(Cmb_Del.Text)))
                                Filexml.WriteEndElement()

                                Filexml.WriteStartElement("Libelle")
                                Filexml.WriteString(CStr(DataSelect.Rows(j).Cells("Libelle").Value))
                                Filexml.WriteEndElement()
                                If IsNumeric(DataSelect.Rows(j).Cells("Positions").Value) Then
                                    Filexml.WriteStartElement("iPosLeft")
                                    Filexml.WriteString(CInt(DataSelect.Rows(j).Cells("Positions").Value))
                                    Filexml.WriteEndElement()
                                Else
                                    Filexml.WriteStartElement("iPosLeft")
                                    Filexml.WriteString(CInt("0"))
                                    Filexml.WriteEndElement()
                                End If
                                If DataSelect.Rows(j).Cells("Info").Value = True Then
                                    Filexml.WriteStartElement("Info")
                                    Filexml.WriteString("oui")
                                    Filexml.WriteEndElement()
                                Else
                                    Filexml.WriteStartElement("Info")
                                    Filexml.WriteString("non")
                                    Filexml.WriteEndElement()
                                End If
                                If DataSelect.Rows(j).Cells("Defaut").Value = True Then
                                    Filexml.WriteStartElement("Defaut")
                                    Filexml.WriteString("1")
                                    Filexml.WriteEndElement()
                                Else
                                    Filexml.WriteStartElement("Defaut")
                                    Filexml.WriteString("0")
                                    Filexml.WriteEndElement()
                                End If
                                Filexml.WriteStartElement("ValeurDefaut")
                                Filexml.WriteString(DataSelect.Rows(j).Cells("ValeurDefaut").Value)
                                Filexml.WriteEndElement()


                                Filexml.WriteStartElement("Decalage")
                                Filexml.WriteString(NumUpDown.Value)
                                Filexml.WriteEndElement()

                            Next j
                            Filexml.WriteEndElement()
                            Filexml.Close()
                        Else
                            MsgBox("Le Fichier " & Trim(NameFormat(UBound(NameFormat))) & " existe déja! Duplication Impossible", MsgBoxStyle.Information, "Format d'integration")

                        End If
                    End If
                Else
                    MsgBox("Saisir un Nom de Fichier", MsgBoxStyle.Information, "Format d'integration")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AfficheFormat()
        Dim OleAdaptaterAfficheFormat As OleDbDataAdapter
        Dim OleAfficheFormatDataset As DataSet
        Dim OledatableAfficheFormat As DataTable
        Dim i As Integer
        Cmb_Format.Items.Clear()
        Try
            OleAdaptaterAfficheFormat = New OleDbDataAdapter("select * from FORMATIERS", OleConnenection)
            OleAfficheFormatDataset = New DataSet
            OleAdaptaterAfficheFormat.Fill(OleAfficheFormatDataset)
            OledatableAfficheFormat = OleAfficheFormatDataset.Tables(0)
            For i = 0 To OledatableAfficheFormat.Rows.Count - 1
                If RechercheFormatype(Trim(OledatableAfficheFormat.Rows(i).Item("Format"))) <> "" Then
                    If Trim(OledatableAfficheFormat.Rows(i).Item("Format")) <> "" Then
                        Cmb_Format.Items.Add(OledatableAfficheFormat.Rows(i).Item("Nom_Format"))
                    End If
                End If
            Next i
            If OledatableAfficheFormat.Rows.Count <> 0 Then
                If Trim(OledatableAfficheFormat.Rows(0).Item("Format")) <> "" Then
                    Cmb_Format.Text = OledatableAfficheFormat.Rows(0).Item("Nom_Format")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteFormat()
        Dim DelFormat As String
        Dim i As Integer
        Dim Delschema As String
        Dim OleAdaptaterDeleteFormat As OleDbDataAdapter
        Dim OleDeleteFormatDataset As DataSet
        Dim OledatableDeleteFormat As DataTable
        Dim OleComDeleteFor As OleDbCommand
        Try
            If File.Exists(Txt_Chemin.Text) = True Then
                DelFormat = "Delete  from FORMATIERS where Format='" & Txt_Chemin.Text & "'"
                OleComDeleteFor = New OleDbCommand(DelFormat)
                OleComDeleteFor.Connection = OleConnenection
                OleComDeleteFor.ExecuteNonQuery()
                File.Delete(Txt_Chemin.Text)
                OleAdaptaterDeleteFormat = New OleDbDataAdapter("select * from PARATIERS where Format='" & Txt_Chemin.Text & "'", OleConnenection)
                OleDeleteFormatDataset = New DataSet
                OleAdaptaterDeleteFormat.Fill(OleDeleteFormatDataset)
                OledatableDeleteFormat = OleDeleteFormatDataset.Tables(0)
                If OledatableDeleteFormat.Rows.Count <> 0 Then
                    For i = 0 To OledatableDeleteFormat.Rows.Count - 1
                        Delschema = "delete from PARATIERS where Format='" & Txt_Chemin.Text & "' and Fichier='" & OledatableDeleteFormat.Rows(i).Item("Fichier") & "'"
                        OleComDeleteFor = New OleDbCommand(Delschema)
                        OleComDeleteFor.Connection = OleConnenection
                        OleComDeleteFor.ExecuteNonQuery()
                    Next i
                End If
                AfficheFormat()
                'Cmb_Format.Text = ""
                'Txt_Chemin.Text = ""
                AffichFormatModifiable()
            Else
                MsgBox("Nom du Format inexistant!", MsgBoxStyle.Information, "Format d'integration")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AffichFormatModifiable()
        Dim NomColonne As String
        Dim NomEntete As String
        Dim PosLeft As Integer
        Dim poslongueur As String
        Dim Defaut As String
        Dim ValeurDefaut As String
        Dim Decal As Object
        Dim i As Integer
        Dim Delimit As String
        DataSelect.Rows.Clear()
        Try
            If Trim(Txt_Chemin.Text) <> "" Then
                If File.Exists(Txt_Chemin.Text) = True Then
                    Dim FileXml As New XmlTextReader(Trim(Txt_Chemin.Text))
                    i = 0
                    While (FileXml.Read())
                        If FileXml.LocalName = "ColUse" Then
                            NomColonne = FileXml.ReadString

                            FileXml.Read()
                            Delimit = FileXml.ReadString

                            FileXml.Read()
                            NomEntete = FileXml.ReadString

                            FileXml.Read()
                            PosLeft = FileXml.ReadString

                            FileXml.Read()
                            poslongueur = FileXml.ReadString

                            FileXml.Read()
                            Defaut = FileXml.ReadString

                            FileXml.Read()
                            ValeurDefaut = FileXml.ReadString

                            FileXml.Read()
                            Decal = FileXml.ReadString

                            If (NomColonne <> "" And NomEntete <> "") Then
                                DataSelect.RowCount = i + 1
                                DataSelect.Rows(i).Cells("Libelle").Value = NomEntete
                                DataSelect.Rows(i).Cells("Colonne").Value = NomColonne
                                If Defaut = "0" Then
                                    DataSelect.Rows(i).Cells("Positions").Value = PosLeft
                                    If poslongueur = "oui" Then
                                        DataSelect.Rows(i).Cells("Info").Value = True
                                        DataSelect.Rows(i).Cells("Defaut").Value = False
                                        DataSelect.Rows(i).Cells("ValeurDefaut").ReadOnly = True
                                    Else
                                        DataSelect.Rows(i).Cells("Info").Value = False
                                        DataSelect.Rows(i).Cells("Defaut").Value = False
                                        DataSelect.Rows(i).Cells("ValeurDefaut").ReadOnly = True
                                    End If
                                Else
                                    If poslongueur = "oui" Then
                                        DataSelect.Rows(i).Cells("Info").Value = True
                                    Else
                                        DataSelect.Rows(i).Cells("Info").Value = False
                                    End If
                                    DataSelect.Rows(i).Cells("Defaut").Value = True
                                    DataSelect.Rows(i).Cells("Positions").ReadOnly = True
                                    DataSelect.Rows(i).Cells("ValeurDefaut").Value = ValeurDefaut
                                End If

                                NumUpDown.Value = Decal
                                i = i + 1
                            End If
                        End If
                    End While
                    FileXml.Close()
                Else
                    MsgBox("Nom du Format inexistant!", MsgBoxStyle.Information, "Format d'integration")
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub MonterLigne()
        Try
            If Index >= 1 Then
                DataSelect.Rows.Insert(Index - 1, DataSelect.Rows(Index).Cells("Colonne").Value, DataSelect.Rows(Index).Cells("Positions").Value, DataSelect.Rows(Index).Cells("Info").Value, DataSelect.Rows(Index).Cells("Defaut").Value, DataSelect.Rows(Index).Cells("ValeurDefaut").Value, DataSelect.Rows(Index).Cells("Libelle").Value)
                DataSelect.Rows.RemoveAt(Index + 1)
                DataSelect.Rows(Index - 1).Selected = True
                If Index >= 1 Then
                    Index = Index - 1
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function RechercheFormatype(ByRef CheminFile As String) As String
        Dim NomColonne As String
        Dim Delimit As String
        Delimit = ""
        Try
            If Trim(CheminFile) <> "" Then
                If File.Exists(CheminFile) = True Then
                    Dim FileXml As New XmlTextReader(Trim(CheminFile))
                    While (FileXml.Read())
                        If FileXml.LocalName = "ColUse" Then
                            NomColonne = FileXml.ReadString

                            FileXml.Read()
                            Delimit = FileXml.ReadString
                        End If
                    End While
                    FileXml.Close()
                    Return Delimit
                Else
                    MsgBox("Chemin du Format inexistant!", MsgBoxStyle.Information, "Format d'integration")
                    Return Delimit
                End If
                Return Delimit
            End If
            Return Delimit
        Catch ex As Exception
            Return Delimit
        End Try

    End Function
    Private Sub DescendreLigne()
        Try
            If Index < DataSelect.RowCount - 1 Then
                DataSelect.Rows.Insert(Index, DataSelect.Rows(Index + 1).Cells("Colonne").Value, DataSelect.Rows(Index + 1).Cells("Positions").Value, DataSelect.Rows(Index + 1).Cells("Info").Value, DataSelect.Rows(Index + 1).Cells("Defaut").Value, DataSelect.Rows(Index + 1).Cells("ValeurDefaut").Value, DataSelect.Rows(Index + 1).Cells("Libelle").Value)
                DataSelect.Rows.RemoveAt(Index + 2)
                DataSelect.Rows(Index + 1).Selected = True
                If Index < DataSelect.RowCount - 1 Then
                    Index = Index + 1
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub DeleteColDispo()
        Dim i As Integer
        Dim Colbool As Boolean = False
        Dim OleAdaptaterDeleteDispo As OleDbDataAdapter
        Dim OleDeleteDispoDataset As DataSet
        Dim OledatableDeleteDispo As DataTable
        Try
            For i = 0 To DataSelect.RowCount - 1
                If DataDispo.Rows(CellCol).Cells("ColDispo").Value = DataSelect.Rows(i).Cells("Colonne").Value Then
                    Colbool = True
                Else
                End If
            Next i
            If Colbool = False Then
                OleAdaptaterDeleteDispo = New OleDbDataAdapter("select * from COTIERS where Libelle='" & DataDispo.Rows(CellCol).Cells("ColDispo").Value & "'", OleConnenection)
                OleDeleteDispoDataset = New DataSet
                OleAdaptaterDeleteDispo.Fill(OleDeleteDispoDataset)
                OledatableDeleteDispo = OleDeleteDispoDataset.Tables(0)
                If OledatableDeleteDispo.Rows.Count <> 0 Then
                    Dim test As Object
                    test = OledatableDeleteDispo.Rows(0).Item("Libre")
                    If OledatableDeleteDispo.Rows(0).Item("Libre") = True Then
                        DataSelect.Rows.Add(DataDispo.Rows(CellCol).Cells("ColDispo").Value, Nothing, True, Nothing, Nothing, DataDispo.Rows(CellCol).Cells("LibelleDispo").Value)
                    Else
                        DataSelect.Rows.Add(DataDispo.Rows(CellCol).Cells("ColDispo").Value, Nothing, False, Nothing, Nothing, DataDispo.Rows(CellCol).Cells("LibelleDispo").Value)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub DeleteColSelect()
        Dim first As Integer
        Dim last As Integer
        Try
            first = DataSelect.Rows.GetFirstRow(DataGridViewElementStates.Displayed)
            last = DataSelect.Rows.GetLastRow(DataGridViewElementStates.Displayed)
            If last >= 0 Then
                If last - first >= 0 Then
                    DataSelect.Rows.RemoveAt(DataSelect.CurrentRow.Index)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub FormatDintegrationTiers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim BaseBool As Boolean
            BaseBool = Connected()
            AfficheColDispo()
            AfficheFormat()
            Me.WindowState = FormWindowState.Maximized
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataDispo_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataDispo.CellClick
        Try
            If e.RowIndex >= 0 Then
                CellCol = e.RowIndex
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Cmb_Format_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_Format.SelectedIndexChanged
        Dim OleAdaptaterCmb As OleDbDataAdapter
        Dim OleCmbDataset As DataSet
        Dim OledatableCmb As DataTable
        Try
            OleAdaptaterCmb = New OleDbDataAdapter("select * from FORMATIERS where Nom_Format='" & Trim(Cmb_Format.Text) & "'", OleConnenection)
            OleCmbDataset = New DataSet
            OleAdaptaterCmb.Fill(OleCmbDataset)
            OledatableCmb = OleCmbDataset.Tables(0)
            If OledatableCmb.Rows.Count <> 0 Then
                Txt_Chemin.Text = OledatableCmb.Rows(0).Item("Format")
            End If
            AffichFormatModifiable()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_DelDispo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_DelDispo.Click
        Try
            DeleteColDispo()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_DelSelt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_DelSelt.Click
        Try
            DeleteColSelect()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_UP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_UP.Click
        Try
            MonterLigne()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Down.Click
        Try
            DescendreLigne()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_SaveFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_SaveFormat.Click
        Try
            CrerUnFormatColParametre()
            AfficheFormat()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_DelForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_DelForm.Click
        Try
            DeleteFormat()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataSelect_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataSelect.CellClick
        Try
            If e.RowIndex >= 0 Then
                Index = e.RowIndex
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Cmb_Format_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Format.TextChanged
        Dim OleAdaptaterCmb As OleDbDataAdapter
        Dim OleCmbDataset As DataSet
        Dim OledatableCmb As DataTable
        Try
            OleAdaptaterCmb = New OleDbDataAdapter("select * from FORMATIERS where Nom_Format='" & Trim(Cmb_Format.Text) & "'", OleConnenection)
            OleCmbDataset = New DataSet
            OleAdaptaterCmb.Fill(OleCmbDataset)
            OledatableCmb = OleCmbDataset.Tables(0)
            If OledatableCmb.Rows.Count <> 0 Then
                Txt_Chemin.Text = OledatableCmb.Rows(0).Item("Format")
            End If
            AffichFormatModifiable()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BT_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_New.Click
        DataSelect.Rows.Clear()
        Txt_Chemin.Text = ""
        Cmb_Format.Text = ""
        NumUpDown.Value = 0
    End Sub

    Private Sub DataSelect_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataSelect.CellContentClick

    End Sub

    Private Sub DataSelect_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataSelect.CellEndEdit
        Try
            If e.RowIndex >= 0 Then
                If DataSelect.Columns(e.ColumnIndex).Name = "Defaut" Then
                    If DataSelect.Rows(e.RowIndex).Cells("Defaut").Value = True Then
                        DataSelect.Rows(e.RowIndex).Cells("Positions").Value = 0
                        DataSelect.Rows(e.RowIndex).Cells("Positions").ReadOnly = True
                        DataSelect.Rows(e.RowIndex).Cells("ValeurDefaut").ReadOnly = False
                    Else
                        DataSelect.Rows(e.RowIndex).Cells("Positions").ReadOnly = False
                        DataSelect.Rows(e.RowIndex).Cells("ValeurDefaut").ReadOnly = True
                        DataSelect.Rows(e.RowIndex).Cells("ValeurDefaut").Value = ""
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class