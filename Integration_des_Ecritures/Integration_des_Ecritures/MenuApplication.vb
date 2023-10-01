Imports System.Windows.Forms
Imports System.IO
<System.Runtime.InteropServices.ComVisible(True)> Public Class MenuApplication
    Private TreeAfterSelect As String

    Private Sub MenuApplication_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CloseBase()
    End Sub

    Private Sub MenuApplication_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Connected() = True Then
            AfficheImport()
            AfficheExport()
            ToolStripStatusLabel3.Text = DateAndTime.Today
            ToolStripStatusLabel5.Text = DateAndTime.TimeOfDay
            Me.Text = "Application d'integration des Ecritures et Création des Tiers< Utilisateur Connecté:[" & Nom_Utilsql & "]<>Connexion à la base de Parametrage< [" & NomBaseCpta & "]>"
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
                PathsfileSave = Trim(RepOledatable.Rows(0).Item("Sauvegarde"))
                Pathsfilejournal = Trim(RepOledatable.Rows(0).Item("Journal"))
            Else
                PathsfileSave = "C:\"
                Pathsfilejournal = "C:\"
            End If
        Else
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
                ReglementSave = Trim(RepeOledatable.Rows(0).Item("Sauvegarde"))
                Reglementjournal = Trim(RepeOledatable.Rows(0).Item("Journal"))
                PathsfileExport = Trim(RepeOledatable.Rows(0).Item("Export"))
            Else
                ReglementSave = "C:\"
                Reglementjournal = "C:\"
                PathsfileExport = "C:\"
            End If
        Else
            ReglementSave = "C:\"
            Reglementjournal = "C:\"
            PathsfileExport = "C:\"
        End If

    End Sub
    Private Sub MenuFermerFichier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFermerFichier.Click
        CloseBase()
    End Sub

    Private Sub MenuConfigureFichier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuConfigureFichier.Click
        Frm_Position.Show()
    End Sub

    Private Sub MenuJounalFichier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuJounalFichier.Click
        Journalisation.MdiParent = Me
        Journalisation.Show()
    End Sub

    Private Sub MenuFormatEcriture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFormatEcriture.Click
        FormatDintegrationEcriture.MdiParent = Me
        FormatDintegrationEcriture.Show()
    End Sub

    Private Sub MenuRepertoireEcriture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuRepertoireEcriture.Click
        SchematintegrerEcriture.MdiParent = Me
        SchematintegrerEcriture.Show()
    End Sub

    Private Sub MenuAjoutForEcriture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuAjoutForEcriture.Click
        AjouterUnFormatEcriture.MdiParent = Me
        AjouterUnFormatEcriture.Show()
    End Sub

    Private Sub MenuInfolibreEcriture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuInfolibreEcriture.Click
        Ecriturelibre.Show()
    End Sub

    Private Sub MenuFormatFournisseur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuFormatFournisseur.Click
        FormatDintegrationTiers.MdiParent = Me
        FormatDintegrationTiers.Show()
    End Sub

    Private Sub MenuRepertoireFournisseur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuRepertoireFournisseur.Click
        SchematintegrerFournisseur.MdiParent = Me
        SchematintegrerFournisseur.Show()
    End Sub

    Private Sub MenuAjoutFormatFournisseur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuAjoutFormatFournisseur.Click
        AjouterUnFormatTiers.MdiParent = Me
        AjouterUnFormatTiers.Show()
    End Sub

    Private Sub MenuPlanFournisseur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuPlanFournisseur.Click
        Frm_Fournisseur.MdiParent = Me
        Frm_Fournisseur.Show()
    End Sub

    Private Sub MenuPlanSpecifiqueFournisseur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuPlanSpecifiqueFournisseur.Click
        Frm_Forunisseurspecifique.MdiParent = Me
        Frm_Forunisseurspecifique.Show()
    End Sub

    Private Sub MenuInfolibreFournisseur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuInfolibreFournisseur.Click
        TiersFournisseurlibre.Show()
    End Sub

    Private Sub MenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem20.Click
        FormatDintegrationTiers.MdiParent = Me
        FormatDintegrationTiers.Show()
    End Sub
    Private Sub MenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem21.Click
        ClientSchematintegrer.MdiParent = Me
        ClientSchematintegrer.Show()
    End Sub

    Private Sub MenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem22.Click
        AjouterUnFormatTiers.MdiParent = Me
        AjouterUnFormatTiers.Show()
    End Sub

    Private Sub MenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem23.Click
        Frm_Client.MdiParent = Me
        Frm_Client.Show()
    End Sub

    Private Sub MenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem24.Click
        Frm_Clientspecifique.MdiParent = Me
        Frm_Clientspecifique.Show()
    End Sub

    Private Sub MenuItem25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem25.Click
        TiersFournisseurlibre.Show()
    End Sub

    Private Sub MenuParametrageSociete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuParametrageSociete.Click
        Parametrage.MdiParent = Me
        Parametrage.Show()
    End Sub

    Private Sub MenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem13.Click
        Fr_ImportationEcriture.MdiParent = Me
        Fr_ImportationEcriture.Show()
    End Sub

    Private Sub MenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem15.Click
        FournisseurImportation.MdiParent = Me
        FournisseurImportation.Show()
    End Sub

    Private Sub MenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem17.Click
        ClientImportation.MdiParent = Me
        ClientImportation.Show()
    End Sub

    Private Sub TreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView.AfterSelect

    End Sub

    Private Sub TreeView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeView.Click
        Select Case TreeAfterSelect

            Case "Ecritures\Format"
                FormatDintegrationEcriture.MdiParent = Me
                FormatDintegrationEcriture.Show()

            Case "Ecritures\Repertoires"
                SchematintegrerEcriture.MdiParent = Me
                SchematintegrerEcriture.Show()

            Case "Ecritures\Ajout Format"
                AjouterUnFormatEcriture.MdiParent = Me
                AjouterUnFormatEcriture.Show()

            Case "Ecritures\Infos Libres"
                  Ecriturelibre.Show()

            Case "Fournisseurs\Format"
                FormatDintegrationTiers.MdiParent = Me
                FormatDintegrationTiers.Show()

            Case "Fournisseurs\Repertoires"
                SchematintegrerFournisseur.MdiParent = Me
                SchematintegrerFournisseur.Show()

            Case "Fournisseurs\Ajout Format"
                AjouterUnFormatTiers.MdiParent = Me
                AjouterUnFormatTiers.Show()

            Case "Fournisseurs\Plan Auxiliaire"
                Frm_Fournisseur.MdiParent = Me
                Frm_Fournisseur.Show()

            Case "Fournisseurs\Plan Specifique"
                Frm_Forunisseurspecifique.MdiParent = Me
                Frm_Forunisseurspecifique.Show()

            Case "Fournisseurs\Infos Libres"
                TiersFournisseurlibre.Show()


            Case "Acquereur\Format"
                FormatDintegrationTiers.MdiParent = Me
                FormatDintegrationTiers.Show()

            Case "Acquereur\Repertoires"
                ClientSchematintegrer.MdiParent = Me
                ClientSchematintegrer.Show()

            Case "Acquereur\Ajout Format"
                AjouterUnFormatTiers.MdiParent = Me
                AjouterUnFormatTiers.Show()

            Case "Acquereur\Plan Auxiliaire"
                Frm_Client.MdiParent = Me
                Frm_Client.Show()

            Case "Acquereur\Plan Specifique"
                Frm_Clientspecifique.MdiParent = Me
                Frm_Clientspecifique.Show()

            Case "Acquereur\Infos Libres"
                TiersFournisseurlibre.Show()


            Case "Integration\Ecritures"
                Fr_ImportationEcriture.MdiParent = Me
                Fr_ImportationEcriture.Show()

            Case "Integration\Fournisseurs"
                FournisseurImportation.MdiParent = Me
                FournisseurImportation.Show()

            Case "Integration\Acquereurs"
                ClientImportation.MdiParent = Me
                ClientImportation.Show()

            Case "Reglement\Test de Connexion"
                Frm_EtatDeConexion.MdiParent = Me
                Frm_EtatDeConexion.Show()

            Case "Reglement\Info Libre"
                Reglementlibre.Show()

            Case "Reglement\Reglement"
                Reglement.MdiParent = Me
                Reglement.Show()

            Case "Societe Comptable"
                Parametrage.MdiParent = Me
                Parametrage.Show()

            Case "Configuration"
                Frm_Position.Show()

            Case "Journalisation\Import"
                Journalisation.MdiParent = Me
                Journalisation.Show()

            Case "Journalisation\Export"
                ExportJournalisation.MdiParent = Me
                ExportJournalisation.Show()

            Case "Sauvegarde/Journal"
                RepertoireImport.MdiParent = Me
                RepertoireImport.Show()

            Case "Quitter"
                CloseBase()
        End Select
    End Sub

    Private Sub TreeView_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView.NodeMouseClick
        TreeAfterSelect = e.Node.FullPath
    End Sub

    Private Sub MenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem19.Click
        Frm_EtatDeConexion.MdiParent = Me
        Frm_EtatDeConexion.Show()
    End Sub

    Private Sub MenuItem31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem31.Click
        Reglement.MdiParent = Me
        Reglement.Show()
    End Sub

    Private Sub MenuItem32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem32.Click
        Reglementlibre.Show()
    End Sub

    Private Sub MenuItem36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RepertoireImport.Show()
    End Sub

    Private Sub MenuItem35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RepertoireImport.Show()
    End Sub

    Private Sub MenuItem34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RepertoireImport.Show()
    End Sub

    Private Sub MenuItem33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RepertoireImport.Show()
    End Sub

    Private Sub MenuItem40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem40.Click
        ExportJournalisation.MdiParent = Me
        ExportJournalisation.Show()
    End Sub

    Private Sub MenuItem45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem45.Click
        ModeleReglement.MdiParent = Me
        ModeleReglement.Show()
    End Sub

    Private Sub MenuItem47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem47.Click
        ModeleReglement.MdiParent = Me
        ModeleReglement.Show()
    End Sub

    Private Sub MenuItem49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub MenuItem49_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem49.Click
        RepertoireImport.Show()
    End Sub
End Class
