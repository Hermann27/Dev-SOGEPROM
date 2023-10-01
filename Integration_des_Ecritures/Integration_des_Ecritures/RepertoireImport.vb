Imports System.IO
Public Class RepertoireImport

    Private Sub RepertoireImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Connected() = True Then
            AfficheImport()
            AfficheExport()
        End If
    End Sub
    Private Sub AfficheImport()
        Dim RepOleAdaptater As OleDbDataAdapter
        Dim RepOleDataset As DataSet
        Dim RepOledatable As DataTable
        RepOleAdaptater = New OleDbDataAdapter("select * from SAUVEIMPORT", OleConnenection)
        RepOleDataset = New DataSet
        RepOleAdaptater.Fill(RepOleDataset)
        RepOledatable = RepOleDataset.Tables(0)
        If RepOledatable.Rows.Count <> 0 Then
            If Directory.Exists(Trim(RepOledatable.Rows(0).Item("Sauvegarde"))) = True And Directory.Exists(Trim(RepOledatable.Rows(0).Item("Journal"))) = True Then
                TxtFilejr.Text = Trim(RepOledatable.Rows(0).Item("Journal"))
                Txt_Rep.Text = Trim(RepOledatable.Rows(0).Item("Sauvegarde"))
                PathsfileSave = Trim(RepOledatable.Rows(0).Item("Sauvegarde"))
                Pathsfilejournal = Trim(RepOledatable.Rows(0).Item("Journal"))
            Else
                PathsfileSave = "C:\"
                Pathsfilejournal = "C:\"
                TxtFilejr.Text = "C:\"
                Txt_Rep.Text = "C:\"
            End If
        Else
            TxtFilejr.Text = "C:\"
            Txt_Rep.Text = "C:\"
            PathsfileSave = "C:\"
            Pathsfilejournal = "C:\"
        End If

    End Sub
    Private Sub AfficheExport()
        Dim RepeOleAdaptater As OleDbDataAdapter
        Dim RepeOleDataset As DataSet
        Dim RepeOledatable As DataTable
        RepeOleAdaptater = New OleDbDataAdapter("select * from SAUVEREGLEMENT", OleConnenection)
        RepeOleDataset = New DataSet
        RepeOleAdaptater.Fill(RepeOleDataset)
        RepeOledatable = RepeOleDataset.Tables(0)
        If RepeOledatable.Rows.Count <> 0 Then
            If Directory.Exists(Trim(RepeOledatable.Rows(0).Item("Sauvegarde"))) = True And Directory.Exists(Trim(RepeOledatable.Rows(0).Item("Journal"))) = True Then
                Txt_Jexport.Text = Trim(RepeOledatable.Rows(0).Item("Journal"))
                Txt_Sexport.Text = Trim(RepeOledatable.Rows(0).Item("Sauvegarde"))
                Textexport.Text = Trim(RepeOledatable.Rows(0).Item("Export"))
                PathsfileExport = Trim(RepeOledatable.Rows(0).Item("Export"))
                ReglementSave = Trim(RepeOledatable.Rows(0).Item("Sauvegarde"))
                Reglementjournal = Trim(RepeOledatable.Rows(0).Item("Journal"))
            Else
                ReglementSave = "C:\"
                Reglementjournal = "C:\"
                Txt_Jexport.Text = "C:\"
                Txt_Sexport.Text = "C:\"
                Textexport.Text = "C:\"
                PathsfileExport = "C:\"
            End If
        Else
            Txt_Jexport.Text = "C:\"
            Txt_Sexport.Text = "C:\"
            ReglementSave = "C:\"
            Reglementjournal = "C:\"
            Textexport.Text = "C:\"
            PathsfileExport = "C:\"
        End If

    End Sub
    Private Sub CreationImport()
        Dim OleAdaptaterEnrege As OleDbDataAdapter
        Dim OleEnregeDataset As DataSet
        Dim OledatableEnrege As DataTable
        Dim OleCommandEnrege As OleDbCommand
        Dim Insert As Boolean = False
        Dim Insertion As String
        Try
            If Trim(TxtFilejr.Text) <> "" And Trim(Txt_Rep.Text) <> "" Then
                OleAdaptaterEnrege = New OleDbDataAdapter("select * From SAUVEIMPORT", OleConnenection)
                OleEnregeDataset = New DataSet
                OleAdaptaterEnrege.Fill(OleEnregeDataset)
                OledatableEnrege = OleEnregeDataset.Tables(0)
                If OledatableEnrege.Rows.Count <> 0 Then
                    MsgBox("Impossible! Il Existe  déja un Repertoire de Sauvegarde et de Journalisation", MsgBoxStyle.Information, "Creation Repertoire")
                Else
                    Insertion = "Insert Into SAUVEIMPORT (Sauvegarde,Journal) VALUES ('" & Trim(Txt_Rep.Text) & "','" & Trim(TxtFilejr.Text) & "')"
                    OleCommandEnrege = New OleDbCommand(Insertion)
                    OleCommandEnrege.Connection = OleConnenection
                    OleCommandEnrege.ExecuteNonQuery()
                    MsgBox("Insertion Reussie", MsgBoxStyle.Information, "Creation Repertoire")
                    AfficheImport()
                End If
            Else
                MsgBox("Information Inexistant", MsgBoxStyle.Information, "Creation Repertoire")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub supprimeImport()
        Dim OleAdaptaterDeleted As OleDbDataAdapter
        Dim OleDeletedDataset As DataSet
        Dim OledatableDeleted As DataTable
        Dim OleCommandDeleted As OleDbCommand
        Dim DelFile As String
        Try
            OleAdaptaterDeleted = New OleDbDataAdapter("select * From SAUVEIMPORT", OleConnenection)
            OleDeletedDataset = New DataSet
            OleAdaptaterDeleted.Fill(OleDeletedDataset)
            OledatableDeleted = OleDeletedDataset.Tables(0)
            If OledatableDeleted.Rows.Count <> 0 Then
                DelFile = "Delete From SAUVEIMPORT where Sauvegarde='" & Trim(OledatableDeleted.Rows(0).Item("Sauvegarde")) & "'"
                OleCommandDeleted = New OleDbCommand(DelFile)
                OleCommandDeleted.Connection = OleConnenection
                OleCommandDeleted.ExecuteNonQuery()
                MsgBox("Suppression Reussie", MsgBoxStyle.Information, "Suppression Repertoire")
                AfficheImport()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CreationExport()
        Dim OleAdaptaterEnreg As OleDbDataAdapter
        Dim OleEnregDataset As DataSet
        Dim OledatableEnreg As DataTable
        Dim OleCommandEnreg As OleDbCommand
        Dim Insert As Boolean = False
        Dim Insertion As String
        Try
            If Trim(Txt_Jexport.Text) <> "" And Trim(Txt_Sexport.Text) <> "" And Trim(Textexport.Text) <> "" Then
                OleAdaptaterEnreg = New OleDbDataAdapter("select * From SAUVEREGLEMENT", OleConnenection)
                OleEnregDataset = New DataSet
                OleAdaptaterEnreg.Fill(OleEnregDataset)
                OledatableEnreg = OleEnregDataset.Tables(0)
                If OledatableEnreg.Rows.Count <> 0 Then
                    MsgBox("Impossible! Il Existe  déja un Repertoire de Sauvegarde et de Journalisation", MsgBoxStyle.Information, "Creation Repertoire")
                Else
                    Insertion = "Insert Into SAUVEREGLEMENT (Sauvegarde,Journal,Export) VALUES ('" & Trim(Txt_Sexport.Text) & "','" & Trim(Txt_Jexport.Text) & "','" & Trim(Textexport.Text) & "')"
                    OleCommandEnreg = New OleDbCommand(Insertion)
                    OleCommandEnreg.Connection = OleConnenection
                    OleCommandEnreg.ExecuteNonQuery()
                    MsgBox("Insertion Reussie", MsgBoxStyle.Information, "Creation Repertoire")
                    AfficheExport()
                End If
            Else
                MsgBox("Information Inexistant", MsgBoxStyle.Information, "Creation Repertoire")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub supprimeExport()
        Dim OleAdaptaterDelete As OleDbDataAdapter
        Dim OleDeleteDataset As DataSet
        Dim OledatableDelete As DataTable
        Dim OleCommandDelete As OleDbCommand
        Dim DelFile As String
        Try
            OleAdaptaterDelete = New OleDbDataAdapter("select * From SAUVEREGLEMENT", OleConnenection)
            OleDeleteDataset = New DataSet
            OleAdaptaterDelete.Fill(OleDeleteDataset)
            OledatableDelete = OleDeleteDataset.Tables(0)
            If OledatableDelete.Rows.Count <> 0 Then
                DelFile = "Delete From SAUVEREGLEMENT where Sauvegarde='" & Trim(OledatableDelete.Rows(0).Item("Sauvegarde")) & "'"
                OleCommandDelete = New OleDbCommand(DelFile)
                OleCommandDelete.Connection = OleConnenection
                OleCommandDelete.ExecuteNonQuery()
                MsgBox("Suppression Reussie", MsgBoxStyle.Information, "Suppression Repertoire")
                AfficheExport()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub BT_FicJournal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_FicJournal.Click
        FolderRepjournal.Description = "Repertoire de Journalisation d'import"
        FolderRepjournal.ShowNewFolderButton = True
        FolderRepjournal.SelectedPath = Nothing
        If FolderRepjournal.ShowDialog = Windows.Forms.DialogResult.OK Then
            TxtFilejr.Text = FolderRepjournal.SelectedPath & "\"
        End If
    End Sub

    Private Sub BT_FicRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_FicRep.Click
        FolderRepsaving.Description = "Repertoire de Sauvegarde d'import"
        FolderRepsaving.ShowNewFolderButton = True
        FolderRepsaving.SelectedPath = Nothing
        If FolderRepsaving.ShowDialog = Windows.Forms.DialogResult.OK Then
            Txt_Rep.Text = FolderRepsaving.SelectedPath & "\"
        End If
    End Sub

    Private Sub Bt_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt_export.Click
        FolderRepjournal.Description = "Repertoire de Journalisation d'export"
        FolderRepjournal.ShowNewFolderButton = True
        FolderRepjournal.SelectedPath = Nothing
        If FolderRepjournal.ShowDialog = Windows.Forms.DialogResult.OK Then
            Txt_Jexport.Text = FolderRepjournal.SelectedPath & "\"
        End If
    End Sub

    Private Sub Bt_Exportsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt_Exportsave.Click
        FolderRepsaving.Description = "Repertoire d'archivage des exports"
        FolderRepsaving.ShowNewFolderButton = True
        FolderRepsaving.SelectedPath = Nothing
        If FolderRepsaving.ShowDialog = Windows.Forms.DialogResult.OK Then
            Txt_Sexport.Text = FolderRepsaving.SelectedPath & "\"
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Btsupexp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btsupexp.Click
        supprimeExport()
    End Sub

    Private Sub Btsupimp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btsupimp.Click
        supprimeImport()
    End Sub

    Private Sub Btcreerexp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btcreerexp.Click
        CreationExport()
    End Sub

    Private Sub Btcreerimp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btcreerimp.Click
        CreationImport()
    End Sub

    Private Sub Btexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btexport.Click
        FolderRepsaving.Description = "Repertoire des exports"
        FolderRepsaving.ShowNewFolderButton = True
        FolderRepsaving.SelectedPath = Nothing
        If FolderRepsaving.ShowDialog = Windows.Forms.DialogResult.OK Then
            Textexport.Text = FolderRepsaving.SelectedPath & "\"
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        UpdateExport()
    End Sub
    Private Sub UpdateExport()
        Dim OleAdaptaterEnreg As OleDbDataAdapter
        Dim OleEnregDataset As DataSet
        Dim OledatableEnreg As DataTable
        Dim OleCommandEnreg As OleDbCommand
        Dim Insert As Boolean = False
        Dim Insertion As String
        Try
            OleAdaptaterEnreg = New OleDbDataAdapter("select * From SAUVEREGLEMENT", OleConnenection)
            OleEnregDataset = New DataSet
            OleAdaptaterEnreg.Fill(OleEnregDataset)
            OledatableEnreg = OleEnregDataset.Tables(0)
            If OledatableEnreg.Rows.Count <> 0 Then
                Insertion = "Update SAUVEREGLEMENT SET Sauvegarde='" & Trim(Txt_Sexport.Text) & "',Journal='" & Trim(Txt_Jexport.Text) & "',Export='" & Trim(Textexport.Text) & "'"
                OleCommandEnreg = New OleDbCommand(Insertion)
                OleCommandEnreg.Connection = OleConnenection
                OleCommandEnreg.ExecuteNonQuery()
                MsgBox("Modification Reussie", MsgBoxStyle.Information, "Creation Repertoire")
                AfficheExport()
            Else
                MsgBox("Impossible! Il n'existe pas de Repertoire de Sauvegarde et de Journalisation", MsgBoxStyle.Information, "Creation Repertoire")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        UpdateImport()
    End Sub
    Private Sub UpdateImport()
        Dim OleAdaptaterEnrege As OleDbDataAdapter
        Dim OleEnregeDataset As DataSet
        Dim OledatableEnrege As DataTable
        Dim OleCommandEnrege As OleDbCommand
        Dim Insert As Boolean = False
        Dim Insertion As String
        Try
            OleAdaptaterEnrege = New OleDbDataAdapter("select * From SAUVEIMPORT", OleConnenection)
            OleEnregeDataset = New DataSet
            OleAdaptaterEnrege.Fill(OleEnregeDataset)
            OledatableEnrege = OleEnregeDataset.Tables(0)
            If OledatableEnrege.Rows.Count <> 0 Then
                Insertion = "Update SAUVEIMPORT SET Sauvegarde='" & Trim(Txt_Rep.Text) & "',Journal='" & Trim(TxtFilejr.Text) & "'"
                OleCommandEnrege = New OleDbCommand(Insertion)
                OleCommandEnrege.Connection = OleConnenection
                OleCommandEnrege.ExecuteNonQuery()
                MsgBox("Modification Reussie", MsgBoxStyle.Information, "Creation Repertoire")
                AfficheImport()
            Else

                MsgBox("Impossible! Il n'existe aucun Repertoire de Sauvegarde et de Journalisation", MsgBoxStyle.Information, "Creation Repertoire")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class