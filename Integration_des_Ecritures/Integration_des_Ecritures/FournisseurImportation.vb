Imports Objets100Lib
Imports System
Imports System.Data.OleDb
Imports System.Collections
Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Imports Microsoft.VisualBasic
Public Class FournisseurImportation
    Public ProgresMax As Integer
    Public EcriureTier As IBOTiersPart3
    Dim EcriturelibreA As IBOTiersPart3
    Public IndexPrec As Integer
    Public NbreLigne As Double
    Public LogMessage As String
    Public NbreTotal As Double
    Public CodeAuxil As Object
    Public Filebool As Boolean
    Public DecFormat As Integer
    Public Result As Object
    Public iRow As Integer
    Public jline As Integer
    Public Message As String
    Public Pilindex As New Stack
    Public CodBanq As Object
    Public CleR As Object
    Public CompBanq As Object
    Public Codeguichet As Object
    Public Intibanq As Object
    Public AdresBanq As Object
    Public BicBanq As Object
    Public CodPostbanq As Object
    Public CommentBanq As Object
    Public ComplBanq As Object
    Public IbanBanq As Object
    Public PaysBanq As Object
    Public VilBanq As Object
    Public sColumnsSepar As String
    Public BasePrecedente As String
    Public BaseComptable As String
    Public ModeleR As String

    Private Sub AfficheSchemasIntegrer()
        Dim i As Integer
        Dim OleAdaptaterschema As OleDbDataAdapter
        Dim OleSchemaDataset As DataSet
        Dim OledatableSchema As DataTable
        DataListeIntegrer.Rows.Clear()
        iRow = 0
        Try
            OleAdaptaterschema = New OleDbDataAdapter("select * from PARATIERS", OleConnenection)
            OleSchemaDataset = New DataSet
            OleAdaptaterschema.Fill(OleSchemaDataset)
            OledatableSchema = OleSchemaDataset.Tables(0)
            For i = 0 To OledatableSchema.Rows.Count - 1
                OuvreLaListedeFichier(OledatableSchema.Rows(i).Item("Nom_Format"), OledatableSchema.Rows(i).Item("Format"), OledatableSchema.Rows(i).Item("Nom_Fichier"), OledatableSchema.Rows(i).Item("Fichier"), OledatableSchema.Rows(i).Item("Nom_Fichier"))
            Next i
        Catch ex As Exception

        End Try
    End Sub
    Private Sub OuvreLaListedeFichier(ByRef Formatname As String, ByRef PathFormat As String, ByRef NameDirectory As String, ByRef PathDirectory As String, ByRef Repert As String)
        Dim i As Integer
        Dim NomFichier As String
        Dim aLines() As String
        Try
            If Directory.Exists(PathDirectory) = True Then
                aLines = Directory.GetFiles(PathDirectory)
                For i = 0 To UBound(aLines)
                    DataListeIntegrer.RowCount = iRow + 1
                    NomFichier = Trim(aLines(i))
                    Do While InStr(Trim(NomFichier), "\") <> 0
                        NomFichier = Strings.Right(NomFichier, Strings.Len(Trim(NomFichier)) - InStr(Trim(NomFichier), "\"))
                    Loop
                    DataListeIntegrer.Rows(iRow).Cells("FichierExport").Value = NomFichier
                    DataListeIntegrer.Rows(iRow).Cells("CheminExport").Value = aLines(i)
                    DataListeIntegrer.Rows(iRow).Cells("Chemin").Value = PathFormat
                    DataListeIntegrer.Rows(iRow).Cells("NomFormat").Value = Formatname
                    DataListeIntegrer.Rows(iRow).Cells("Dossier").Value = Repert
                    DataListeIntegrer.Rows(iRow).Cells("Valider").Value = True
                    iRow = iRow + 1
                Next i
                aLines = Nothing
            Else
                MsgBox("Ce Repertoire n'est pas valide! " & PathDirectory, MsgBoxStyle.Information, "Repertoire des Fichiers à Traiter")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Initialiser()
        ProgresMax = 0
        NbreLigne = 0
        Label8.Text = ""
    End Sub
    Private Sub Modification_Du_Fichier(ByVal sPathFilexporter As String, ByVal spathFileFormat As String)
        If RechercheFormatype(spathFileFormat) <> "" Then
            If RechercheFormatype(spathFileFormat) = "Tabulation" Then
                sColumnsSepar = ControlChars.Tab
            Else
                If RechercheFormatype(spathFileFormat) = "Point Virgule" Then
                    sColumnsSepar = ";"
                End If
            End If
            Modification_Au_Format_Aligné(sPathFilexporter, spathFileFormat, sColumnsSepar)
        End If
    End Sub
    Private Sub Modification_Au_Format_Aligné(ByVal sPathFilexporter As String, ByVal spathFileFormat As String, Optional ByRef sColumnsSepar As String = ControlChars.Tab)
        Dim m As Integer
        Dim jColD As Integer
        Dim iLine As Integer
        Dim aRows() As String
        Dim iColPosition As Integer
        Dim iColGauchetxt As String
        Dim i As Integer, aCols() As String
        Try
            Initialiser()
            iLine = 0
            aRows = Nothing
            LectureFormatChampAligner(spathFileFormat)
            aRows = GetArrayFile(sPathFilexporter, aRows)
            ProgressBar1.Value = ProgressBar1.Minimum
            Datagridaffiche.Rows.Clear()
            NbreTotal = DecFormat
            ProgresMax = UBound(aRows) + 1 - DecFormat
            m = 0
            For i = DecFormat To UBound(aRows)
                aCols = Split(aRows(i), sColumnsSepar)
                Datagridaffiche.RowCount = iLine + 1 - m
                For jColD = 0 To Datagridaffiche.ColumnCount - 1
                    iColGauchetxt = Trim(Strings.Right(Trim(Datagridaffiche.Columns(jColD).HeaderText), 3))
                    iColPosition = CInt(Strings.Right(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1), Strings.Len(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1)) - InStr(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1), "(")))
                    If iColPosition <> 0 Then
                        If iColPosition <= (UBound(aCols) + 1) Then
                            Datagridaffiche.Item(jColD, iLine - m).Value = Trim(aCols(iColPosition - 1))
                        Else
                            Datagridaffiche.Item(jColD, iLine - m).Value = ""
                        End If
                    Else
                        Datagridaffiche.Item(jColD, iLine - m).Value = LireFichierFormatAligner(spathFileFormat, Datagridaffiche.Columns(jColD).Name)
                    End If
                Next jColD
                iLine = iLine + 1
                If i Mod 10 = 0 Then
                    Me.Refresh()
                    Modification_Ecriture()
                    m = iLine
                Else
                    If i = UBound(aRows) Then
                        Me.Refresh()
                        Modification_Ecriture()
                        m = iLine
                    End If
                End If
            Next i
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Modification_Ecriture()
        Dim i As Integer
        Dim j As Integer
        Dim n As Integer
        Dim BaseAdaptater As OleDbDataAdapter
        Dim Basedataset As DataSet
        Dim Basedatatable As DataTable
        Dim Coladaptater As OleDbDataAdapter
        Dim Coldataset As DataSet
        Dim Coldatatable As DataTable
        Renitialisateur()
        Me.Cursor = Cursors.WaitCursor
        BT_integrer.Enabled = False
        If Datagridaffiche.RowCount >= 0 Then
            ProgressBar1.Maximum = ProgresMax
            For i = 0 To Datagridaffiche.RowCount - 1
                Label5.Refresh()
                Label5.Text = "Verification Terminée! Modification En Cours..."
                Try

                    If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) <> "" Then
                        If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) <> BasePrecedente Then
                            BaseAdaptater = New OleDbDataAdapter("select * from SOCIETE Where SOCGPS='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) & "'", OleConnenection)
                            Basedataset = New DataSet
                            BaseAdaptater.Fill(Basedataset)
                            Basedatatable = Basedataset.Tables(0)
                            If Basedatatable.Rows.Count <> 0 Then
                                BaseComptable = Basedatatable.Rows(0).Item("SOCSAGE")
                                CloseBaseFree()
                                BasePrecedente = Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value)
                                If OpenBaseCpta(BaseCpta, Trim(Basedatatable.Rows(0).Item("FICHIERCPTA")), Trim(Basedatatable.Rows(0).Item("UTILISATEUR")), Trim(Basedatatable.Rows(0).Item("MOTPASSE"))) = True Then
                                    Error_journal.WriteLine("Traitement du Tier " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) & "  Base Comptable Sage :" & BaseComptable & "  Ligne :" & NbreLigne + 1)
                                    If Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) = "F" Then
                                        Coladaptater = New OleDbDataAdapter("select * from FOURNISSEUR", OleConnenection)
                                        Coldataset = New DataSet
                                        Coladaptater.Fill(Coldataset)
                                        Coldatatable = Coldataset.Tables(0)
                                        If Coldatatable.Rows.Count <> 0 Then
                                            Try
                                                For n = 0 To Coldatatable.Rows.Count - 1
                                                    If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) <> "" Then
                                                        If BaseCpta.FactoryTiers.ExistNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) = True Then
                                                            EcriureTier = Nothing
                                                            EcriureTier = BaseCpta.FactoryFournisseur.ReadNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""))
                                                            Try
                                                                With EcriureTier
                                                                    If Trim(Coldatatable.Rows(n).Item("Collectif")) <> "" Then
                                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(Coldatatable.Rows(n).Item("Collectif"))) = True Then
                                                                            .CompteGPrinc = BaseCpta.FactoryCompteG.ReadNumero(Trim(Coldatatable.Rows(n).Item("Collectif")))
                                                                        Else
                                                                            Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans Sage")
                                                                        End If
                                                                    Else
                                                                        Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans le Parametrage")
                                                                    End If

                                                                    For j = 0 To Datagridaffiche.ColumnCount - 1
                                                                        If Datagridaffiche.Columns(j).Name = "Adresse" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                    If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                        .Adresse.Adresse = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                    Else
                                                                                        .Adresse.Adresse = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Mail" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                                    .Telecom.EMail = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.EMail = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "APE" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    .CT_Ape = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Ape = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Classement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                                    .CT_Classement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Classement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodePostal" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 8 Then
                                                                                    .Adresse.CodePostal = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.CodePostal = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 8)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeRegion" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                                    .Adresse.CodeRegion = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.CodeRegion = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Commentaire" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Commentaire = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Commentaire = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Complement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .Adresse.Complement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Complement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Contact" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Contact = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Contact = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Identifiant" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                                    .CT_Identifiant = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Identifiant = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "ModeReglement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                Dim OleAdaptaterMod As OleDbDataAdapter
                                                                                Dim OleModDataset As DataSet
                                                                                Dim OledatableMod As DataTable
                                                                                OleAdaptaterMod = New OleDbDataAdapter("select * from MODELEREGLEMENT where Code='" & Trim(Datagridaffiche.Item(j, i).Value) & "'", OleConnenection)
                                                                                OleModDataset = New DataSet
                                                                                OleAdaptaterMod.Fill(OleModDataset)
                                                                                OledatableMod = OleModDataset.Tables(0)
                                                                                If OledatableMod.Rows.Count <> 0 Then
                                                                                    If BaseCpta.FactoryModeleReglement.ExistIntitule(Trim(OledatableMod.Rows(0).Item("Intitule"))) = True Then
                                                                                        ModeleR = Trim(OledatableMod.Rows(0).Item("Intitule"))
                                                                                    Else
                                                                                        Error_journal.WriteLine("L'Intitule de Reglement " & Trim(OledatableMod.Rows(0).Item("Intitule")) & " Inexistant dans Sage")
                                                                                        ModeleR = Nothing
                                                                                    End If
                                                                                Else
                                                                                    Error_journal.WriteLine("Le Code de Reglement Pragma " & Trim(Datagridaffiche.Item(j, i).Value) & " Inexistant dans le Parametrage")
                                                                                    ModeleR = Nothing
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Intitule" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Intitule = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                    Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                    Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                End If
                                                                            Else
                                                                                Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Langue" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Langue = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Langue = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "BanquePrincipale" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "Devise" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If BaseCpta.FactoryDevise.ExistIntitule(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                                    .Devise = BaseCpta.FactoryDevise.ReadIntitule(Trim(Datagridaffiche.Item(j, i).Value))
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "NomSite" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                                    .Telecom.Site = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Site = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Pays" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .Adresse.Pays = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Pays = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Qualite" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                                    .CT_Qualite = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Qualite = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Siret" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                                    .CT_Siret = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Siret = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "Telecopie" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 21 Then
                                                                                    .Telecom.Telecopie = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Telecopie = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 21)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Telephone" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                                    .Telecom.Telephone = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Telephone = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Ville" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                    .Adresse.Ville = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Ville = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "AdresseBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                    AdresBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    AdresBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    CodBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CodBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "BIC" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    BicBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    BicBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CleRib" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 2 Then
                                                                                    CleR = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CleR = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodePostBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    CodPostbanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CodPostbanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CommentBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    CommentBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CommentBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CompleBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then

                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    ComplBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    ComplBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CompteBancaire" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 11 Then
                                                                                    CompBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CompBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 11)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeGuichet" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    Codeguichet = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    Codeguichet = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "IBAN" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    IbanBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    IbanBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "IntituleBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    Intibanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    Intibanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "PaysBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    PaysBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    PaysBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "VilBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    VilBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    VilBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    Next j
                                                                    If Trim(Message) <> "" Then
                                                                    Else
                                                                        LogMessage = "L'Intitule du Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  est vide Ligne  " & NbreLigne + 1
                                                                    End If
                                                                    .Write()
                                                                    Error_journal.WriteLine("Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Modifié")
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            .TiersPayeur = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            .Write()
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Rattachement du Tierspayeur : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            If BaseCpta.FactoryModeleReglement.ExistIntitule(ModeleR) = True Then
                                                                                .ModeleReglement = BaseCpta.FactoryModeleReglement.ReadIntitule(ModeleR)
                                                                                .WriteDefault()
                                                                            End If
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Rattachement du Modele de Reglement : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            EcriturelibreA = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            With EcriturelibreA
                                                                                Do While Pilindex.Count > 0
                                                                                    Dim OleInfoAdapter As OleDbDataAdapter
                                                                                    Dim OleInfoDataset As DataSet
                                                                                    Dim BaseInfo As DataTable
                                                                                    Dim Pindex As Integer
                                                                                    Pindex = Pilindex.Peek
                                                                                    OleInfoAdapter = New OleDbDataAdapter("select * from COTIERS Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                                                    OleInfoDataset = New DataSet
                                                                                    OleInfoAdapter.Fill(OleInfoDataset)
                                                                                    BaseInfo = OleInfoDataset.Tables(0)
                                                                                    If BaseInfo.Rows.Count <> 0 Then
                                                                                        If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                                            .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                        Else
                                                                                            If Trim(BaseInfo.Rows(0).Item("Type")) = "Numerique" Then
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDbl(Datagridaffiche.Item(Pindex, i).Value)
                                                                                            Else
                                                                                                If BaseInfo.Rows(0).Item("Type") = "Date" Then
                                                                                                    If Strings.Len(Trim(Datagridaffiche.Item(Pindex, i).Value)) = 8 Then
                                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                                    Else
                                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                                    End If
                                                                                                End If
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                    Pindex = Nothing
                                                                                Loop
                                                                                .Write()
                                                                                Error_journal.WriteLine("Information Libre du Tier : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Modifiée")
                                                                                EcriturelibreA = Nothing
                                                                            End With
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Information Libre : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    Pilindex.Clear()
                                                                End With
                                                            Catch ex As Exception

                                                            End Try

                                                        Else
                                                            Error_journal.WriteLine("Le Tier de Numero de Compte : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Rattaché à un compte Collectif n'existe pas")
                                                        End If
                                                    Else
                                                        Error_journal.WriteLine("Impossible de Traiter le Tier car son Code est vide")
                                                    End If
                                                Next n
                                                Label8.Refresh()
                                                MenuApplication.TreeView.Refresh()
                                                Datagridaffiche.Refresh()
                                            Catch ex As Exception
                                                Error_journal.WriteLine("Message Systeme :" & ex.Message)
                                            End Try
                                        End If
                                    Else 'Fournisseur specifique ddddddddddddddddddddddddddddddddddddddd

                                        Coladaptater = New OleDbDataAdapter("select * from FOURNISSEURS", OleConnenection)
                                        Coldataset = New DataSet
                                        Coladaptater.Fill(Coldataset)
                                        Coldatatable = Coldataset.Tables(0)
                                        If Coldatatable.Rows.Count <> 0 Then
                                            Try
                                                For n = 0 To Coldatatable.Rows.Count - 1
                                                    If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) <> "" Then
                                                        If BaseCpta.FactoryTiers.ExistNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) = True Then
                                                            EcriureTier = Nothing
                                                            EcriureTier = BaseCpta.FactoryFournisseur.ReadNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""))
                                                            Try
                                                                With EcriureTier
                                                                    If Trim(Coldatatable.Rows(n).Item("Collectif")) <> "" Then
                                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(Coldatatable.Rows(n).Item("Collectif"))) = True Then
                                                                            .CompteGPrinc = BaseCpta.FactoryCompteG.ReadNumero(Trim(Coldatatable.Rows(n).Item("Collectif")))
                                                                        Else
                                                                            Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans Sage")
                                                                        End If
                                                                    Else
                                                                        Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans le Parametrage")
                                                                    End If


                                                                    For j = 0 To Datagridaffiche.ColumnCount - 1
                                                                        If Datagridaffiche.Columns(j).Name = "Adresse" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                    If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                        .Adresse.Adresse = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                    Else
                                                                                        .Adresse.Adresse = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Mail" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                                    .Telecom.EMail = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.EMail = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "APE" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    .CT_Ape = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Ape = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Classement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                                    .CT_Classement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Classement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodePostal" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 8 Then
                                                                                    .Adresse.CodePostal = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.CodePostal = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 8)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeRegion" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                                    .Adresse.CodeRegion = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.CodeRegion = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Commentaire" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Commentaire = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Commentaire = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Complement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .Adresse.Complement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Complement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Contact" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Contact = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Contact = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Identifiant" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                                    .CT_Identifiant = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Identifiant = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "ModeReglement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                Dim OleAdaptaterMod As OleDbDataAdapter
                                                                                Dim OleModDataset As DataSet
                                                                                Dim OledatableMod As DataTable
                                                                                OleAdaptaterMod = New OleDbDataAdapter("select * from MODELEREGLEMENT where Code='" & Trim(Datagridaffiche.Item(j, i).Value) & "'", OleConnenection)
                                                                                OleModDataset = New DataSet
                                                                                OleAdaptaterMod.Fill(OleModDataset)
                                                                                OledatableMod = OleModDataset.Tables(0)
                                                                                If OledatableMod.Rows.Count <> 0 Then
                                                                                    If BaseCpta.FactoryModeleReglement.ExistIntitule(Trim(OledatableMod.Rows(0).Item("Intitule"))) = True Then
                                                                                        ModeleR = Trim(OledatableMod.Rows(0).Item("Intitule"))
                                                                                    Else
                                                                                        Error_journal.WriteLine("L'Intitule de Reglement " & Trim(OledatableMod.Rows(0).Item("Intitule")) & " Inexistant dans Sage")
                                                                                        ModeleR = Nothing
                                                                                    End If
                                                                                Else
                                                                                    Error_journal.WriteLine("Le Code de Reglement Pragma " & Trim(Datagridaffiche.Item(j, i).Value) & " Inexistant dans le Parametrage")
                                                                                    ModeleR = Nothing
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Intitule" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Intitule = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                    Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                    Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                End If
                                                                            Else
                                                                                Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Langue" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Langue = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Langue = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "BanquePrincipale" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "Devise" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If BaseCpta.FactoryDevise.ExistIntitule(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                                    .Devise = BaseCpta.FactoryDevise.ReadIntitule(Trim(Datagridaffiche.Item(j, i).Value))
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "NomSite" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                                    .Telecom.Site = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Site = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Pays" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .Adresse.Pays = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Pays = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Qualite" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                                    .CT_Qualite = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Qualite = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Siret" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                                    .CT_Siret = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Siret = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "Telecopie" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 21 Then
                                                                                    .Telecom.Telecopie = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Telecopie = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 21)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Telephone" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                                    .Telecom.Telephone = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Telephone = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Ville" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                    .Adresse.Ville = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Ville = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "AdresseBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                    AdresBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    AdresBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    CodBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CodBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "BIC" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    BicBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    BicBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CleRib" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 2 Then
                                                                                    CleR = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CleR = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodePostBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    CodPostbanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CodPostbanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CommentBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    CommentBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CommentBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CompleBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then

                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    ComplBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    ComplBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CompteBancaire" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 11 Then
                                                                                    CompBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CompBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 11)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeGuichet" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    Codeguichet = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    Codeguichet = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "IBAN" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    IbanBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    IbanBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "IntituleBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    Intibanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    Intibanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "PaysBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    PaysBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    PaysBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "VilBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    VilBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    VilBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    Next j
                                                                    If Trim(Message) <> "" Then
                                                                    Else
                                                                        LogMessage = "L'Intitule du Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  est vide Ligne  " & NbreLigne + 1
                                                                    End If
                                                                    .Write()

                                                                    Error_journal.WriteLine("Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Modifié")
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            .TiersPayeur = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            .Write()
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Rattachement du Tierspayeur : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            If BaseCpta.FactoryModeleReglement.ExistIntitule(ModeleR) = True Then
                                                                                .ModeleReglement = BaseCpta.FactoryModeleReglement.ReadIntitule(ModeleR)
                                                                                .WriteDefault()
                                                                            End If
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Rattachement du Modele de Reglement : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            EcriturelibreA = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            With EcriturelibreA
                                                                                Do While Pilindex.Count > 0
                                                                                    Dim OleInfoAdapter As OleDbDataAdapter
                                                                                    Dim OleInfoDataset As DataSet
                                                                                    Dim BaseInfo As DataTable
                                                                                    Dim Pindex As Integer
                                                                                    Pindex = Pilindex.Peek
                                                                                    OleInfoAdapter = New OleDbDataAdapter("select * from COTIERS Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                                                    OleInfoDataset = New DataSet
                                                                                    OleInfoAdapter.Fill(OleInfoDataset)
                                                                                    BaseInfo = OleInfoDataset.Tables(0)
                                                                                    If BaseInfo.Rows.Count <> 0 Then
                                                                                        If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                                            .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                        Else
                                                                                            If Trim(BaseInfo.Rows(0).Item("Type")) = "Numerique" Then
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDbl(Datagridaffiche.Item(Pindex, i).Value)
                                                                                            Else
                                                                                                If BaseInfo.Rows(0).Item("Type") = "Date" Then
                                                                                                    If Strings.Len(Trim(Datagridaffiche.Item(Pindex, i).Value)) = 8 Then
                                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                                    Else
                                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                                    End If
                                                                                                End If
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                    Pindex = Nothing
                                                                                Loop
                                                                                .Write()
                                                                                Error_journal.WriteLine("Information Libre du Tier : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Modifiée")
                                                                                EcriturelibreA = Nothing
                                                                            End With
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Information Libre : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    Pilindex.Clear()
                                                                End With
                                                            Catch ex As Exception

                                                            End Try

                                                        Else
                                                            Error_journal.WriteLine("Le Tier de Numero de Compte : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Rattaché à un compte Collectif n'existe pas")
                                                        End If
                                                    Else
                                                        Error_journal.WriteLine("Impossible de Traiter le Tier car son Code est vide")
                                                    End If
                                                Next n
                                                Label8.Refresh()
                                                MenuApplication.TreeView.Refresh()
                                                Datagridaffiche.Refresh()
                                            Catch ex As Exception
                                                Error_journal.WriteLine("Message Systeme :" & ex.Message)
                                            End Try
                                        End If
                                    End If
                                Else
                                    Error_journal.WriteLine("Echec de Connexion à la base Comptable " & Basedatatable.Rows(0).Item("SOCSAGE") & "  Ligne :" & NbreLigne + 1)
                                End If
                            Else
                                'Error_journal.WriteLine("La base Comptable ne Correspond à aucune base Sage " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) & "  Ligne :" & NbreLigne + 1)
                            End If
                        Else    'La Societe est la meme
                            Error_journal.WriteLine("Traitement du Tier " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) & "  Base Comptable Sage :" & BaseComptable & "  Ligne :" & NbreLigne + 1)
                            If Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) = "F" Then
                                Coladaptater = New OleDbDataAdapter("select * from FOURNISSEUR", OleConnenection)
                                Coldataset = New DataSet
                                Coladaptater.Fill(Coldataset)
                                Coldatatable = Coldataset.Tables(0)
                                If Coldatatable.Rows.Count <> 0 Then
                                    Try
                                        For n = 0 To Coldatatable.Rows.Count - 1
                                            If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) <> "" Then
                                                If BaseCpta.FactoryTiers.ExistNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) = True Then
                                                    EcriureTier = Nothing
                                                    EcriureTier = BaseCpta.FactoryFournisseur.ReadNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""))
                                                    Try
                                                        With EcriureTier
                                                            If Trim(Coldatatable.Rows(n).Item("Collectif")) <> "" Then
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(Coldatatable.Rows(n).Item("Collectif"))) = True Then
                                                                    .CompteGPrinc = BaseCpta.FactoryCompteG.ReadNumero(Trim(Coldatatable.Rows(n).Item("Collectif")))
                                                                Else
                                                                    Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans Sage")
                                                                End If
                                                            Else
                                                                Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans le Parametrage")
                                                            End If

                                                            For j = 0 To Datagridaffiche.ColumnCount - 1
                                                                If Datagridaffiche.Columns(j).Name = "Adresse" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                            If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                .Adresse.Adresse = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            Else
                                                                                .Adresse.Adresse = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Mail" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                            .Telecom.EMail = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.EMail = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "APE" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            .CT_Ape = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Ape = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Classement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                            .CT_Classement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Classement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodePostal" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 8 Then
                                                                            .Adresse.CodePostal = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.CodePostal = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 8)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeRegion" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                            .Adresse.CodeRegion = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.CodeRegion = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Commentaire" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Commentaire = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Commentaire = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Complement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .Adresse.Complement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Complement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Contact" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Contact = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Contact = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Identifiant" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                            .CT_Identifiant = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Identifiant = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "ModeReglement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        Dim OleAdaptaterMod As OleDbDataAdapter
                                                                        Dim OleModDataset As DataSet
                                                                        Dim OledatableMod As DataTable
                                                                        OleAdaptaterMod = New OleDbDataAdapter("select * from MODELEREGLEMENT where Code='" & Trim(Datagridaffiche.Item(j, i).Value) & "'", OleConnenection)
                                                                        OleModDataset = New DataSet
                                                                        OleAdaptaterMod.Fill(OleModDataset)
                                                                        OledatableMod = OleModDataset.Tables(0)
                                                                        If OledatableMod.Rows.Count <> 0 Then
                                                                            If BaseCpta.FactoryModeleReglement.ExistIntitule(Trim(OledatableMod.Rows(0).Item("Intitule"))) = True Then
                                                                                ModeleR = Trim(OledatableMod.Rows(0).Item("Intitule"))
                                                                            Else
                                                                                Error_journal.WriteLine("L'Intitule de Reglement " & Trim(OledatableMod.Rows(0).Item("Intitule")) & " Inexistant dans Sage")
                                                                                ModeleR = Nothing
                                                                            End If
                                                                        Else
                                                                            Error_journal.WriteLine("Le Code de Reglement Pragma " & Trim(Datagridaffiche.Item(j, i).Value) & " Inexistant dans le Parametrage")
                                                                            ModeleR = Nothing
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Intitule" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Intitule = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                            Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        End If
                                                                    Else
                                                                        Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Langue" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Langue = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Langue = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "BanquePrincipale" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "Devise" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If BaseCpta.FactoryDevise.ExistIntitule(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                            .Devise = BaseCpta.FactoryDevise.ReadIntitule(Trim(Datagridaffiche.Item(j, i).Value))
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "NomSite" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                            .Telecom.Site = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Site = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Pays" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .Adresse.Pays = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Pays = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Qualite" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                            .CT_Qualite = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Qualite = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Siret" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                            .CT_Siret = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Siret = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "Telecopie" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 21 Then
                                                                            .Telecom.Telecopie = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Telecopie = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 21)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Telephone" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                            .Telecom.Telephone = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Telephone = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Ville" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                            .Adresse.Ville = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Ville = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "AdresseBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                            AdresBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            AdresBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            CodBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CodBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "BIC" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            BicBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            BicBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CleRib" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 2 Then
                                                                            CleR = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CleR = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodePostBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            CodPostbanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CodPostbanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CommentBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            CommentBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CommentBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CompleBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then

                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            ComplBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            ComplBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CompteBancaire" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 11 Then
                                                                            CompBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CompBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 11)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeGuichet" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            Codeguichet = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            Codeguichet = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "IBAN" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            IbanBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            IbanBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "IntituleBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            Intibanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            Intibanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "PaysBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            PaysBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            PaysBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "VilBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            VilBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            VilBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                            Next j
                                                            If Trim(Message) <> "" Then
                                                            Else
                                                                LogMessage = "L'Intitule du Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  est vide Ligne  " & NbreLigne + 1
                                                            End If
                                                            .Write()
                                                            Error_journal.WriteLine("Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Modifié")
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    .TiersPayeur = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    .Write()
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Rattachement du Tierspayeur : " & ex.Message)
                                                                End Try
                                                            End If
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    If BaseCpta.FactoryModeleReglement.ExistIntitule(ModeleR) = True Then
                                                                        .ModeleReglement = BaseCpta.FactoryModeleReglement.ReadIntitule(ModeleR)
                                                                        .WriteDefault()
                                                                    End If
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Rattachement du Modele de Reglement : " & ex.Message)
                                                                End Try
                                                            End If
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    EcriturelibreA = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    With EcriturelibreA
                                                                        Do While Pilindex.Count > 0
                                                                            Dim OleInfoAdapter As OleDbDataAdapter
                                                                            Dim OleInfoDataset As DataSet
                                                                            Dim BaseInfo As DataTable
                                                                            Dim Pindex As Integer
                                                                            Pindex = Pilindex.Peek
                                                                            OleInfoAdapter = New OleDbDataAdapter("select * from COTIERS Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                                            OleInfoDataset = New DataSet
                                                                            OleInfoAdapter.Fill(OleInfoDataset)
                                                                            BaseInfo = OleInfoDataset.Tables(0)
                                                                            If BaseInfo.Rows.Count <> 0 Then
                                                                                If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                                    .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                Else
                                                                                    If Trim(BaseInfo.Rows(0).Item("Type")) = "Numerique" Then
                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDbl(Datagridaffiche.Item(Pindex, i).Value)
                                                                                    Else
                                                                                        If BaseInfo.Rows(0).Item("Type") = "Date" Then
                                                                                            If Strings.Len(Trim(Datagridaffiche.Item(Pindex, i).Value)) = 8 Then
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                            Else
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            Pindex = Nothing
                                                                        Loop
                                                                        .Write()
                                                                        Error_journal.WriteLine("Information Libre du Tier : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Modifiée")
                                                                        EcriturelibreA = Nothing
                                                                    End With
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Information Libre : " & ex.Message)
                                                                End Try
                                                            End If
                                                            Pilindex.Clear()
                                                        End With
                                                    Catch ex As Exception

                                                    End Try

                                                Else
                                                    Error_journal.WriteLine("Le Tier de Numero de Compte : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Rattaché à un compte Collectif n'existe pas")
                                                End If
                                            Else
                                                Error_journal.WriteLine("Impossible de Traiter le Tier car son Code est vide")
                                            End If
                                        Next n
                                        Label8.Refresh()
                                        MenuApplication.TreeView.Refresh()
                                        Datagridaffiche.Refresh()
                                    Catch ex As Exception
                                        Error_journal.WriteLine("Message Systeme : " & ex.Message)
                                    End Try

                                End If
                            Else 'Fournisseur specifique fffffffffffffffffffffffffffffffffff
                                Coladaptater = New OleDbDataAdapter("select * from FOURNISSEURS", OleConnenection)
                                Coldataset = New DataSet
                                Coladaptater.Fill(Coldataset)
                                Coldatatable = Coldataset.Tables(0)
                                If Coldatatable.Rows.Count <> 0 Then
                                    Try
                                        For n = 0 To Coldatatable.Rows.Count - 1
                                            If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) <> "" Then
                                                If BaseCpta.FactoryTiers.ExistNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) = True Then
                                                    EcriureTier = Nothing
                                                    EcriureTier = BaseCpta.FactoryFournisseur.ReadNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""))
                                                    Try
                                                        With EcriureTier
                                                            If Trim(Coldatatable.Rows(n).Item("Collectif")) <> "" Then
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(Coldatatable.Rows(n).Item("Collectif"))) = True Then
                                                                    .CompteGPrinc = BaseCpta.FactoryCompteG.ReadNumero(Trim(Coldatatable.Rows(n).Item("Collectif")))
                                                                Else
                                                                    Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans Sage")
                                                                End If
                                                            Else
                                                                Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans le Parametrage")
                                                            End If

                                                            For j = 0 To Datagridaffiche.ColumnCount - 1
                                                                If Datagridaffiche.Columns(j).Name = "Adresse" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                            If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                .Adresse.Adresse = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            Else
                                                                                .Adresse.Adresse = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Mail" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                            .Telecom.EMail = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.EMail = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "APE" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            .CT_Ape = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Ape = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Classement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                            .CT_Classement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Classement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodePostal" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 8 Then
                                                                            .Adresse.CodePostal = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.CodePostal = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 8)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeRegion" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                            .Adresse.CodeRegion = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.CodeRegion = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Commentaire" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Commentaire = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Commentaire = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Complement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .Adresse.Complement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Complement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Contact" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Contact = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Contact = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Identifiant" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                            .CT_Identifiant = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Identifiant = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "ModeReglement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        Dim OleAdaptaterMod As OleDbDataAdapter
                                                                        Dim OleModDataset As DataSet
                                                                        Dim OledatableMod As DataTable
                                                                        OleAdaptaterMod = New OleDbDataAdapter("select * from MODELEREGLEMENT where Code='" & Trim(Datagridaffiche.Item(j, i).Value) & "'", OleConnenection)
                                                                        OleModDataset = New DataSet
                                                                        OleAdaptaterMod.Fill(OleModDataset)
                                                                        OledatableMod = OleModDataset.Tables(0)
                                                                        If OledatableMod.Rows.Count <> 0 Then
                                                                            If BaseCpta.FactoryModeleReglement.ExistIntitule(Trim(OledatableMod.Rows(0).Item("Intitule"))) = True Then
                                                                                ModeleR = Trim(OledatableMod.Rows(0).Item("Intitule"))
                                                                            Else
                                                                                Error_journal.WriteLine("L'Intitule de Reglement " & Trim(OledatableMod.Rows(0).Item("Intitule")) & " Inexistant dans Sage")
                                                                                ModeleR = Nothing
                                                                            End If
                                                                        Else
                                                                            Error_journal.WriteLine("Le Code de Reglement Pragma " & Trim(Datagridaffiche.Item(j, i).Value) & " Inexistant dans le Parametrage")
                                                                            ModeleR = Nothing
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Intitule" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Intitule = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                            Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        End If
                                                                    Else
                                                                        Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Langue" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Langue = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Langue = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "BanquePrincipale" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "Devise" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If BaseCpta.FactoryDevise.ExistIntitule(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                            .Devise = BaseCpta.FactoryDevise.ReadIntitule(Trim(Datagridaffiche.Item(j, i).Value))
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "NomSite" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                            .Telecom.Site = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Site = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Pays" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .Adresse.Pays = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Pays = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Qualite" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                            .CT_Qualite = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Qualite = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Siret" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                            .CT_Siret = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Siret = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "Telecopie" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 21 Then
                                                                            .Telecom.Telecopie = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Telecopie = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 21)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Telephone" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                            .Telecom.Telephone = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Telephone = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Ville" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                            .Adresse.Ville = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Ville = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "AdresseBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                            AdresBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            AdresBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            CodBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CodBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "BIC" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            BicBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            BicBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CleRib" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 2 Then
                                                                            CleR = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CleR = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodePostBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            CodPostbanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CodPostbanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CommentBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            CommentBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CommentBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CompleBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then

                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            ComplBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            ComplBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CompteBancaire" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 11 Then
                                                                            CompBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CompBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 11)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeGuichet" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            Codeguichet = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            Codeguichet = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "IBAN" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            IbanBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            IbanBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "IntituleBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            Intibanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            Intibanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "PaysBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            PaysBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            PaysBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "VilBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            VilBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            VilBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                            Next j
                                                            If Trim(Message) <> "" Then
                                                            Else
                                                                LogMessage = "L'Intitule du Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  est vide Ligne  " & NbreLigne + 1
                                                            End If
                                                            .Write()
                                                            Error_journal.WriteLine("Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Modifié")
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    .TiersPayeur = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    .Write()
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Rattachement du Tierspayeur : " & ex.Message)
                                                                End Try
                                                            End If
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    If BaseCpta.FactoryModeleReglement.ExistIntitule(ModeleR) = True Then
                                                                        .ModeleReglement = BaseCpta.FactoryModeleReglement.ReadIntitule(ModeleR)
                                                                        .WriteDefault()
                                                                    End If
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Rattachement du Modele de Reglement : " & ex.Message)
                                                                End Try
                                                            End If
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    EcriturelibreA = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    With EcriturelibreA
                                                                        Do While Pilindex.Count > 0
                                                                            Dim OleInfoAdapter As OleDbDataAdapter
                                                                            Dim OleInfoDataset As DataSet
                                                                            Dim BaseInfo As DataTable
                                                                            Dim Pindex As Integer
                                                                            Pindex = Pilindex.Peek
                                                                            OleInfoAdapter = New OleDbDataAdapter("select * from COTIERS Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                                            OleInfoDataset = New DataSet
                                                                            OleInfoAdapter.Fill(OleInfoDataset)
                                                                            BaseInfo = OleInfoDataset.Tables(0)
                                                                            If BaseInfo.Rows.Count <> 0 Then
                                                                                If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                                    .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                Else
                                                                                    If Trim(BaseInfo.Rows(0).Item("Type")) = "Numerique" Then
                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDbl(Datagridaffiche.Item(Pindex, i).Value)
                                                                                    Else
                                                                                        If BaseInfo.Rows(0).Item("Type") = "Date" Then
                                                                                            If Strings.Len(Trim(Datagridaffiche.Item(Pindex, i).Value)) = 8 Then
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                            Else
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            Pindex = Nothing
                                                                        Loop
                                                                        .Write()
                                                                        Error_journal.WriteLine("Information Libre du Tier : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Modifiée")
                                                                        EcriturelibreA = Nothing
                                                                    End With
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Information Libre : " & ex.Message)
                                                                End Try
                                                            End If
                                                            Pilindex.Clear()
                                                        End With
                                                    Catch ex As Exception

                                                    End Try

                                                Else
                                                    Error_journal.WriteLine("Le Tier de Numero de Compte : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Rattaché à un compte Collectif n'existe pas")
                                                End If
                                            Else
                                                Error_journal.WriteLine("Impossible de Traiter le Tier car son Code est vide")
                                            End If
                                        Next n
                                        Label8.Refresh()
                                        MenuApplication.TreeView.Refresh()
                                        Datagridaffiche.Refresh()
                                    Catch ex As Exception
                                        Error_journal.WriteLine("Message Systeme: " & ex.Message)
                                    End Try
                                End If
                            End If
                        End If
                    End If 'Fin de comparaison de la societe
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    NbreLigne = NbreLigne + 1
                    Label8.Text = NbreLigne & "/" & ProgresMax
                    Label8.Refresh()
                Catch ex As Exception
                    Error_journal.WriteLine("Erreur de Creation du Tier  " & ex.Message)
                    Error_journal.WriteLine("Ou " & LogMessage)
                    Error_journal.WriteLine(" ")
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    NbreLigne = NbreLigne + 1
                    Label8.Text = NbreLigne & "/" & ProgresMax
                End Try
            Next i
            Datagridaffiche.Rows.Clear()
        End If
        Datagridaffiche.Rows.Clear()
        Me.Cursor = Cursors.Default
        BT_integrer.Enabled = True
    End Sub
    Private Sub BT_Apercue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Apercue.Click
        Try
            If TxtChemin.Text <> "" And TxtFormat.Text <> "" Then
                If File.Exists(TxtChemin.Text) = True And File.Exists(TxtFormat.Text) Then
                    Lecture_Du_Fichier(TxtChemin.Text, TxtFormat.Text)
                End If
            Else
                If DataListeIntegrer.Rows(IndexPrec).Cells("Valider").Value = True Then
                    If File.Exists(DataListeIntegrer.Rows(IndexPrec).Cells("Chemin").Value) = True And File.Exists(DataListeIntegrer.Rows(IndexPrec).Cells("CheminExport").Value) Then
                        Lecture_Du_Fichier(DataListeIntegrer.Rows(IndexPrec).Cells("CheminExport").Value, DataListeIntegrer.Rows(IndexPrec).Cells("Chemin").Value)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Lecture_Du_Fichier(ByVal sPathFilexporter As String, ByVal spathFileFormat As String)
        If RechercheFormatype(spathFileFormat) <> "" Then
            If RechercheFormatype(spathFileFormat) = "Tabulation" Then
                sColumnsSepar = ControlChars.Tab
            Else
                If RechercheFormatype(spathFileFormat) = "Point Virgule" Then
                    sColumnsSepar = ";"
                End If
            End If
            Lecture_Au_Format_Aligné(sPathFilexporter, spathFileFormat, sColumnsSepar)
        End If
    End Sub
    Private Sub Lecture_Au_Format_Aligné(ByVal sPathFilexporter As String, ByVal spathFileFormat As String, Optional ByRef sColumnsSepar As String = ControlChars.Tab)
        Dim m As Integer
        Dim jColD As Integer
        Dim iLine As Integer
        Dim aRows() As String
        Dim iColPosition As Integer
        Dim iColGauchetxt As String
        Dim i As Integer, aCols() As String
        Try
            Initialiser()
            iLine = 0
            aRows = Nothing
            LectureFormatChampAligner(spathFileFormat)
            aRows = GetArrayFile(sPathFilexporter, aRows)
            ProgressBar1.Value = ProgressBar1.Minimum
            Datagridaffiche.Rows.Clear()
            NbreTotal = DecFormat
            ProgresMax = UBound(aRows) + 1 - DecFormat
            m = 0
            For i = DecFormat To UBound(aRows)
                aCols = Split(aRows(i), sColumnsSepar)
                Datagridaffiche.RowCount = iLine + 1 - m
                For jColD = 0 To Datagridaffiche.ColumnCount - 1
                    iColGauchetxt = Trim(Strings.Right(Trim(Datagridaffiche.Columns(jColD).HeaderText), 3))
                    iColPosition = CInt(Strings.Right(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1), Strings.Len(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1)) - InStr(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1), "(")))
                    If iColPosition <> 0 Then
                        If iColPosition <= (UBound(aCols) + 1) Then
                            Datagridaffiche.Item(jColD, iLine - m).Value = Trim(aCols(iColPosition - 1))
                        Else
                            Datagridaffiche.Item(jColD, iLine - m).Value = ""
                        End If
                    Else
                        Datagridaffiche.Item(jColD, iLine - m).Value = LireFichierFormatAligner(spathFileFormat, Datagridaffiche.Columns(jColD).Name)
                    End If
                Next jColD
                iLine = iLine + 1
                If i >= 500 Then
                    Exit Sub
                End If
            Next i
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Integration_Du_Fichier(ByVal sPathFilexporter As String, ByVal spathFileFormat As String)
        If RechercheFormatype(spathFileFormat) <> "" Then
            If RechercheFormatype(spathFileFormat) = "Tabulation" Then
                sColumnsSepar = ControlChars.Tab
            Else
                If RechercheFormatype(spathFileFormat) = "Point Virgule" Then
                    sColumnsSepar = ";"
                End If
            End If
            Integration_Au_Format_Aligné(sPathFilexporter, spathFileFormat, sColumnsSepar)
        End If
    End Sub
    Private Sub Integration_Au_Format_Aligné(ByVal sPathFilexporter As String, ByVal spathFileFormat As String, Optional ByRef sColumnsSepar As String = ControlChars.Tab)
        Dim m As Integer
        Dim jColD As Integer
        Dim iLine As Integer
        Dim aRows() As String
        Dim iColPosition As Integer
        Dim iColGauchetxt As String
        Dim i As Integer, aCols() As String
        Try
            Initialiser()
            iLine = 0
            aRows = Nothing
            LectureFormatChampAligner(spathFileFormat)
            aRows = GetArrayFile(sPathFilexporter, aRows)
            ProgressBar1.Value = ProgressBar1.Minimum
            Datagridaffiche.Rows.Clear()
            NbreTotal = DecFormat
            ProgresMax = UBound(aRows) + 1 - DecFormat
            m = 0
            For i = DecFormat To UBound(aRows)
                aCols = Split(aRows(i), sColumnsSepar)
                Datagridaffiche.RowCount = iLine + 1 - m
                For jColD = 0 To Datagridaffiche.ColumnCount - 1
                    iColGauchetxt = Trim(Strings.Right(Trim(Datagridaffiche.Columns(jColD).HeaderText), 3))
                    iColPosition = CInt(Strings.Right(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1), Strings.Len(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1)) - InStr(Strings.Left(Datagridaffiche.Columns(jColD).HeaderText, InStr(Datagridaffiche.Columns(jColD).HeaderText, ")") - 1), "(")))
                    If iColPosition <> 0 Then
                        If iColPosition <= (UBound(aCols) + 1) Then
                            Datagridaffiche.Item(jColD, iLine - m).Value = Trim(aCols(iColPosition - 1))
                        Else
                            Datagridaffiche.Item(jColD, iLine - m).Value = ""
                        End If
                    Else
                        Datagridaffiche.Item(jColD, iLine - m).Value = LireFichierFormatAligner(spathFileFormat, Datagridaffiche.Columns(jColD).Name)
                    End If
                Next jColD
                iLine = iLine + 1
                If i Mod 10 = 0 Then
                    Me.Refresh()
                    Integrer_Ecriture()
                    m = iLine
                Else
                    If i = UBound(aRows) Then
                        Me.Refresh()
                        Integrer_Ecriture()
                        m = iLine
                    End If
                End If
            Next i
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LectureFormatChampAligner(ByVal PathFileFormat As String)
        Dim NomColonne As String
        Dim NomEntete As String
        Dim PosLeft As Integer
        Dim Libre As String
        Dim Defaut As String
        Dim ValeurDefaut As String
        Dim Delimit As String
        Datagridaffiche.Rows.Clear()
        Datagridaffiche.Columns.Clear()
        Dim FileXml As New XmlTextReader(PathFileFormat)
        Try
            While (FileXml.Read())
                If FileXml.LocalName = "ColUse" Then
                    NomColonne = FileXml.ReadString
                    FileXml.Read()

                    FileXml.Read()
                    Delimit = FileXml.ReadString

                    FileXml.Read()
                    NomEntete = FileXml.ReadString

                    FileXml.Read()
                    PosLeft = FileXml.ReadString

                    FileXml.Read()
                    Libre = FileXml.ReadString

                    FileXml.Read()
                    Defaut = FileXml.ReadString

                    FileXml.Read()
                    ValeurDefaut = FileXml.ReadString

                    FileXml.Read()
                    DecFormat = FileXml.ReadString
                    If (NomColonne <> "" And NomEntete <> "") And (CInt(PosLeft) >= 0 And Trim(Libre) <> "") Then
                        CreateComboBoxColumn(Datagridaffiche, NomColonne & "-->(" & PosLeft & ")" & "->" & Libre, NomEntete)
                    End If
                End If
            End While
        Catch ex As Exception

        End Try
    End Sub
    Private Function LireFichierFormatAligner(ByRef ScheminFileFormat As String, ByRef Colname As String) As Object
        Dim NomColonne As String
        Dim NomEntete As String
        Dim PosLeft As Integer
        Dim Defaut As String
        Dim ValeurDefaut As String
        Dim Delimit As String
        Dim Libre As String
        Try
            If Trim(ScheminFileFormat) <> "" Then
                If File.Exists(ScheminFileFormat) = True Then
                    Dim FileXml As New XmlTextReader(Trim(ScheminFileFormat))
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
                            Libre = FileXml.ReadString

                            FileXml.Read()
                            Defaut = FileXml.ReadString

                            FileXml.Read()
                            ValeurDefaut = FileXml.ReadString
                            If Colname = NomEntete Then
                                Result = ValeurDefaut
                            End If
                        End If
                    End While
                    FileXml.Close()
                End If
            End If
        Catch ex As Exception

        End Try
        Return Result
    End Function
    Private Sub Renitialisateur()
        CodBanq = ""
        CleR = ""
        CompBanq = ""
        Codeguichet = ""
        Intibanq = ""
        AdresBanq = ""
        BicBanq = ""
        CodPostbanq = ""
        CommentBanq = ""
        ComplBanq = ""
        IbanBanq = ""
        PaysBanq = ""
        VilBanq = ""
        CodeAuxil = Nothing
    End Sub
    Private Sub Integrer_Ecriture()
        Dim i As Integer
        Dim j As Integer
        Dim n As Integer
        Dim BaseAdaptater As OleDbDataAdapter
        Dim Basedataset As DataSet
        Dim Basedatatable As DataTable
        Dim Coladaptater As OleDbDataAdapter
        Dim Coldataset As DataSet
        Dim Coldatatable As DataTable
        Renitialisateur()
        Me.Cursor = Cursors.WaitCursor
        BT_integrer.Enabled = False
        If Datagridaffiche.RowCount >= 0 Then
            ProgressBar1.Maximum = ProgresMax
            For i = 0 To Datagridaffiche.RowCount - 1
                Label5.Refresh()
                Label5.Text = "Verification Terminée! Integration En Cours..."
                Try

                    If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) <> "" Then
                        If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) <> BasePrecedente Then
                            BaseAdaptater = New OleDbDataAdapter("select * from SOCIETE Where SOCGPS='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) & "'", OleConnenection)
                            Basedataset = New DataSet
                            BaseAdaptater.Fill(Basedataset)
                            Basedatatable = Basedataset.Tables(0)
                            If Basedatatable.Rows.Count <> 0 Then
                                BaseComptable = Basedatatable.Rows(0).Item("SOCSAGE")
                                CloseBaseFree()
                                BasePrecedente = Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value)
                                If OpenBaseCpta(BaseCpta, Trim(Basedatatable.Rows(0).Item("FICHIERCPTA")), Trim(Basedatatable.Rows(0).Item("UTILISATEUR")), Trim(Basedatatable.Rows(0).Item("MOTPASSE"))) = True Then
                                    Error_journal.WriteLine("Traitement du Tier " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) & "  Base Comptable Sage :" & BaseComptable & "  Ligne :" & NbreLigne + 1)
                                    If Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) = "F" Then
                                        Coladaptater = New OleDbDataAdapter("select * from FOURNISSEUR", OleConnenection)
                                        Coldataset = New DataSet
                                        Coladaptater.Fill(Coldataset)
                                        Coldatatable = Coldataset.Tables(0)
                                        If Coldatatable.Rows.Count <> 0 Then
                                            Try
                                                For n = 0 To Coldatatable.Rows.Count - 1
                                                    If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) <> "" Then
                                                        EcriureTier = Nothing
                                                        EcriureTier = BaseCpta.FactoryFournisseur.Create
                                                        If BaseCpta.FactoryTiers.ExistNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) = False Then
                                                            Try
                                                                With EcriureTier
                                                                    If Trim(Coldatatable.Rows(n).Item("Collectif")) <> "" Then
                                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(Coldatatable.Rows(n).Item("Collectif"))) = True Then
                                                                            .CompteGPrinc = BaseCpta.FactoryCompteG.ReadNumero(Trim(Coldatatable.Rows(n).Item("Collectif")))
                                                                        Else
                                                                            Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans Sage")
                                                                        End If
                                                                    Else
                                                                        Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans le Parametrage")
                                                                    End If
                                                                    For j = 0 To Datagridaffiche.ColumnCount - 1
                                                                        If Datagridaffiche.Columns(j).Name = "Compte" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) <= 17 Then
                                                                                    .CT_Num = Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")
                                                                                    CodeAuxil = Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")
                                                                                Else
                                                                                    .CT_Num = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                                    CodeAuxil = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                                    .TiersPayeur = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Adresse" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                    If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                        .Adresse.Adresse = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                    Else
                                                                                        .Adresse.Adresse = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Mail" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                                    .Telecom.EMail = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.EMail = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "APE" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    .CT_Ape = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Ape = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Classement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                                    .CT_Classement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Classement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodePostal" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 8 Then
                                                                                    .Adresse.CodePostal = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.CodePostal = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 8)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeRegion" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                                    .Adresse.CodeRegion = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.CodeRegion = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Commentaire" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Commentaire = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Commentaire = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Complement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .Adresse.Complement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Complement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Contact" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Contact = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Contact = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Identifiant" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                                    .CT_Identifiant = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Identifiant = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "ModeReglement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                Dim OleAdaptaterMod As OleDbDataAdapter
                                                                                Dim OleModDataset As DataSet
                                                                                Dim OledatableMod As DataTable
                                                                                OleAdaptaterMod = New OleDbDataAdapter("select * from MODELEREGLEMENT where Code='" & Trim(Datagridaffiche.Item(j, i).Value) & "'", OleConnenection)
                                                                                OleModDataset = New DataSet
                                                                                OleAdaptaterMod.Fill(OleModDataset)
                                                                                OledatableMod = OleModDataset.Tables(0)
                                                                                If OledatableMod.Rows.Count <> 0 Then
                                                                                    If BaseCpta.FactoryModeleReglement.ExistIntitule(Trim(OledatableMod.Rows(0).Item("Intitule"))) = True Then
                                                                                        ModeleR = Trim(OledatableMod.Rows(0).Item("Intitule"))
                                                                                    Else
                                                                                        Error_journal.WriteLine("L'Intitule de Reglement " & Trim(OledatableMod.Rows(0).Item("Intitule")) & " Inexistant dans Sage")
                                                                                        ModeleR = Nothing
                                                                                    End If
                                                                                Else
                                                                                    Error_journal.WriteLine("Le Code de Reglement Pragma " & Trim(Datagridaffiche.Item(j, i).Value) & " Inexistant dans le Parametrage")
                                                                                    ModeleR = Nothing
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Intitule" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Intitule = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                    Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                    Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                End If
                                                                            Else
                                                                                Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Langue" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Langue = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Langue = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "BanquePrincipale" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                            End If
                                                                        End If


                                                                        If Datagridaffiche.Columns(j).Name = "Devise" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If BaseCpta.FactoryDevise.ExistIntitule(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                                    .Devise = BaseCpta.FactoryDevise.ReadIntitule(Trim(Datagridaffiche.Item(j, i).Value))
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "NomSite" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                                    .Telecom.Site = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Site = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Pays" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .Adresse.Pays = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Pays = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Qualite" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                                    .CT_Qualite = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Qualite = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Siret" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                                    .CT_Siret = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Siret = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "Telecopie" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 21 Then
                                                                                    .Telecom.Telecopie = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Telecopie = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 21)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Telephone" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                                    .Telecom.Telephone = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Telephone = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Ville" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                    .Adresse.Ville = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Ville = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "AdresseBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                    AdresBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    AdresBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    CodBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CodBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "BIC" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    BicBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    BicBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CleRib" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 2 Then
                                                                                    CleR = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CleR = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodePostBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    CodPostbanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CodPostbanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CommentBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    CommentBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CommentBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CompleBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then

                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    ComplBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    ComplBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CompteBancaire" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 11 Then
                                                                                    CompBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CompBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 11)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeGuichet" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    Codeguichet = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    Codeguichet = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "IBAN" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    IbanBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    IbanBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "IntituleBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    Intibanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    Intibanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "PaysBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    PaysBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    PaysBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "VilBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    VilBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    VilBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    Next j
                                                                    If Trim(Message) <> "" Then
                                                                    Else
                                                                        LogMessage = "L'Intitule du Tier " & Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1) & "  est vide Ligne  " & NbreLigne + 1
                                                                    End If
                                                                    .Write()
                                                                    Error_journal.WriteLine("Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Créé")
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            .TiersPayeur = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            .Write()
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Rattachement du Tierspayeur : " & ex.Message)
                                                                        End Try
                                                                    End If

                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            Dim BanqueTier As IBOTiersBanque3
                                                                            Dim BankTier As IBOTiersPart3
                                                                            BankTier = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            BanqueTier = BankTier.FactoryTiersBanque.Create
                                                                            If (Trim(Intibanq) <> "" And Trim(CompBanq) <> "") And (Trim(Codeguichet) <> "" And Trim(CodBanq) <> "") Then
                                                                                With BanqueTier
                                                                                    .BT_Compte = CompBanq
                                                                                    .BT_Guichet = Codeguichet
                                                                                    .BT_Intitule = Intibanq
                                                                                    .BT_Cle = CleR
                                                                                    .BT_Banque = CodBanq
                                                                                    .Adresse.Adresse = AdresBanq
                                                                                    .BT_Bic = BicBanq
                                                                                    .Adresse.CodePostal = CodPostbanq
                                                                                    .BT_Commentaire = CommentBanq
                                                                                    .Adresse.Complement = ComplBanq
                                                                                    .BT_IBAN = IbanBanq
                                                                                    .Adresse.Pays = PaysBanq
                                                                                    .Adresse.Ville = VilBanq
                                                                                    LogMessage = "Erreur de Création de la Banque rattachée au Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Créé  Erreur de cohérence des données" & NbreLigne + 1
                                                                                    .Write()
                                                                                End With
                                                                            Else
                                                                                Error_journal.WriteLine("Impossible de Créer la Banque rattachée au Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Créé :  N°Compte bancaire et Intitulé bancaire et Code Guichet et Code Banque Obligatoires  " & NbreLigne + 1)
                                                                            End If

                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Banque du Tier : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            If BaseCpta.FactoryModeleReglement.ExistIntitule(ModeleR) = True Then
                                                                                .ModeleReglement = BaseCpta.FactoryModeleReglement.ReadIntitule(ModeleR)
                                                                                .WriteDefault()
                                                                            End If
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Rattachement du Modele de Reglement : " & ex.Message)
                                                                        End Try
                                                                    End If

                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            EcriturelibreA = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            With EcriturelibreA
                                                                                Do While Pilindex.Count > 0
                                                                                    Dim OleInfoAdapter As OleDbDataAdapter
                                                                                    Dim OleInfoDataset As DataSet
                                                                                    Dim BaseInfo As DataTable
                                                                                    Dim Pindex As Integer
                                                                                    Pindex = Pilindex.Peek
                                                                                    OleInfoAdapter = New OleDbDataAdapter("select * from COTIERS Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                                                    OleInfoDataset = New DataSet
                                                                                    OleInfoAdapter.Fill(OleInfoDataset)
                                                                                    BaseInfo = OleInfoDataset.Tables(0)
                                                                                    Dim test As Object
                                                                                    If BaseInfo.Rows.Count <> 0 Then
                                                                                        If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                                            test = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                            .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                        Else
                                                                                            If Trim(BaseInfo.Rows(0).Item("Type")) = "Numerique" Then
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDbl(Datagridaffiche.Item(Pindex, i).Value)
                                                                                            Else
                                                                                                If BaseInfo.Rows(0).Item("Type") = "Date" Then
                                                                                                    If Strings.Len(Trim(Datagridaffiche.Item(Pindex, i).Value)) = 8 Then
                                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                                    Else
                                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                                    End If
                                                                                                End If
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                    Pindex = Nothing
                                                                                Loop
                                                                                .Write()
                                                                                EcriturelibreA = Nothing
                                                                            End With
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Information Libre : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    Pilindex.Clear()
                                                                End With
                                                            Catch ex As Exception

                                                            End Try

                                                        Else
                                                            Error_journal.WriteLine("Le Tier de Numero de Compte : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Rattaché à un compte Collectif existe déja")
                                                        End If
                                                    Else
                                                        Error_journal.WriteLine("Impossible de Traiter le Tier car son Code est vide")
                                                    End If
                                                Next n
                                                Label8.Refresh()
                                                MenuApplication.TreeView.Refresh()
                                                Datagridaffiche.Refresh()
                                            Catch ex As Exception
                                                Error_journal.WriteLine("Message Systeme :" & ex.Message)
                                            End Try
                                        End If
                                    Else 'Fournisseur specifique ddddddddddddddddddddddddddddddddddddddd

                                        Coladaptater = New OleDbDataAdapter("select * from FOURNISSEURS", OleConnenection)
                                        Coldataset = New DataSet
                                        Coladaptater.Fill(Coldataset)
                                        Coldatatable = Coldataset.Tables(0)
                                        If Coldatatable.Rows.Count <> 0 Then
                                            Try
                                                For n = 0 To Coldatatable.Rows.Count - 1
                                                    If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) <> "" Then
                                                        EcriureTier = Nothing
                                                        EcriureTier = BaseCpta.FactoryFournisseur.Create
                                                        If BaseCpta.FactoryTiers.ExistNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) = False Then
                                                            Try
                                                                With EcriureTier
                                                                    If Trim(Coldatatable.Rows(n).Item("Collectif")) <> "" Then
                                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(Coldatatable.Rows(n).Item("Collectif"))) = True Then
                                                                            .CompteGPrinc = BaseCpta.FactoryCompteG.ReadNumero(Trim(Coldatatable.Rows(n).Item("Collectif")))
                                                                        Else
                                                                            Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans Sage")
                                                                        End If
                                                                    Else
                                                                        Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans le Parametrage")
                                                                    End If

                                                                    For j = 0 To Datagridaffiche.ColumnCount - 1
                                                                        If Datagridaffiche.Columns(j).Name = "Compte" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) <= 17 Then
                                                                                    .CT_Num = Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")
                                                                                    CodeAuxil = Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")
                                                                                Else
                                                                                    .CT_Num = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                                    CodeAuxil = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Adresse" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                    If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                        .Adresse.Adresse = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                    Else
                                                                                        .Adresse.Adresse = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Mail" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                                    .Telecom.EMail = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.EMail = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "APE" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    .CT_Ape = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Ape = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Classement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                                    .CT_Classement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Classement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodePostal" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 8 Then
                                                                                    .Adresse.CodePostal = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.CodePostal = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 8)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeRegion" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                                    .Adresse.CodeRegion = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.CodeRegion = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Commentaire" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Commentaire = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Commentaire = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Complement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .Adresse.Complement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Complement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Contact" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Contact = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Contact = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Identifiant" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                                    .CT_Identifiant = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Identifiant = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "ModeReglement" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                Dim OleAdaptaterMod As OleDbDataAdapter
                                                                                Dim OleModDataset As DataSet
                                                                                Dim OledatableMod As DataTable
                                                                                OleAdaptaterMod = New OleDbDataAdapter("select * from MODELEREGLEMENT where Code='" & Trim(Datagridaffiche.Item(j, i).Value) & "'", OleConnenection)
                                                                                OleModDataset = New DataSet
                                                                                OleAdaptaterMod.Fill(OleModDataset)
                                                                                OledatableMod = OleModDataset.Tables(0)
                                                                                If OledatableMod.Rows.Count <> 0 Then
                                                                                    If BaseCpta.FactoryModeleReglement.ExistIntitule(Trim(OledatableMod.Rows(0).Item("Intitule"))) = True Then
                                                                                        ModeleR = Trim(OledatableMod.Rows(0).Item("Intitule"))
                                                                                    Else
                                                                                        Error_journal.WriteLine("L'Intitule de Reglement " & Trim(OledatableMod.Rows(0).Item("Intitule")) & " Inexistant dans Sage")
                                                                                        ModeleR = Nothing
                                                                                    End If
                                                                                Else
                                                                                    Error_journal.WriteLine("Le Code de Reglement Pragma " & Trim(Datagridaffiche.Item(j, i).Value) & " Inexistant dans le Parametrage")
                                                                                    ModeleR = Nothing
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Intitule" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Intitule = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                    Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                    Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                End If
                                                                            Else
                                                                                Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Langue" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .CT_Langue = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Langue = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "BanquePrincipale" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "Devise" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If BaseCpta.FactoryDevise.ExistIntitule(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                                    .Devise = BaseCpta.FactoryDevise.ReadIntitule(Trim(Datagridaffiche.Item(j, i).Value))
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "NomSite" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                                    .Telecom.Site = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Site = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Pays" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    .Adresse.Pays = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Pays = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Qualite" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                                    .CT_Qualite = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Qualite = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Siret" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                                    .CT_Siret = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .CT_Siret = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "Telecopie" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 21 Then
                                                                                    .Telecom.Telecopie = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Telecopie = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 21)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Telephone" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                                    .Telecom.Telephone = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Telecom.Telephone = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "Ville" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                    .Adresse.Ville = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    .Adresse.Ville = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "AdresseBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                    AdresBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    AdresBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    CodBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CodBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "BIC" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    BicBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    BicBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CleRib" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 2 Then
                                                                                    CleR = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CleR = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodePostBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    CodPostbanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CodPostbanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CommentBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    CommentBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CommentBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CompleBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then

                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    ComplBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    ComplBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CompteBancaire" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 11 Then
                                                                                    CompBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    CompBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 11)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "CodeGuichet" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                                    Codeguichet = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    Codeguichet = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "IBAN" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    IbanBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    IbanBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                        If Datagridaffiche.Columns(j).Name = "IntituleBanque" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    Intibanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    Intibanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "PaysBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    PaysBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    PaysBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If Datagridaffiche.Columns(j).Name = "VilBan" Then
                                                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                                If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                                    VilBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                                Else
                                                                                    VilBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    Next j
                                                                    If Trim(Message) <> "" Then
                                                                    Else
                                                                        LogMessage = "L'Intitule du Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  est vide Ligne  " & NbreLigne + 1
                                                                    End If
                                                                    .Write()
                                                                    Error_journal.WriteLine("Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Créé")
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            .TiersPayeur = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            .Write()
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Rattachement du Tierspayeur : " & ex.Message)
                                                                        End Try
                                                                    End If

                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            Dim BanqueTier As IBOTiersBanque3
                                                                            Dim BankTier As IBOTiersPart3
                                                                            BankTier = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            BanqueTier = BankTier.FactoryTiersBanque.Create
                                                                            If (Trim(Intibanq) <> "" And Trim(CompBanq) <> "") And (Trim(Codeguichet) <> "" And Trim(CodBanq) <> "") Then
                                                                                With BanqueTier
                                                                                    .BT_Compte = CompBanq
                                                                                    .BT_Guichet = Codeguichet
                                                                                    .BT_Intitule = Intibanq
                                                                                    .BT_Cle = CleR
                                                                                    .BT_Banque = CodBanq
                                                                                    .Adresse.Adresse = AdresBanq
                                                                                    .BT_Bic = BicBanq
                                                                                    .Adresse.CodePostal = CodPostbanq
                                                                                    .BT_Commentaire = CommentBanq
                                                                                    .Adresse.Complement = ComplBanq
                                                                                    .BT_IBAN = IbanBanq
                                                                                    .Adresse.Pays = PaysBanq
                                                                                    .Adresse.Ville = VilBanq
                                                                                    LogMessage = "Erreur de Création de la Banque rattachée au Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Créé  Erreur de cohérence des données" & NbreLigne + 1
                                                                                    .Write()
                                                                                End With
                                                                            Else
                                                                                Error_journal.WriteLine("Impossible de Créer la Banque rattachée au Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Créé :  N°Compte bancaire et Intitulé bancaire et Code Guichet et Code Banque Obligatoires  " & NbreLigne + 1)
                                                                            End If

                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Banque du Tier : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            If BaseCpta.FactoryModeleReglement.ExistIntitule(ModeleR) = True Then
                                                                                .ModeleReglement = BaseCpta.FactoryModeleReglement.ReadIntitule(ModeleR)
                                                                                .WriteDefault()
                                                                            End If
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Rattachement du Modele de Reglement : " & ex.Message)
                                                                        End Try
                                                                    End If

                                                                    If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                        Try
                                                                            EcriturelibreA = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                            With EcriturelibreA
                                                                                Do While Pilindex.Count > 0
                                                                                    Dim OleInfoAdapter As OleDbDataAdapter
                                                                                    Dim OleInfoDataset As DataSet
                                                                                    Dim BaseInfo As DataTable
                                                                                    Dim Pindex As Integer
                                                                                    Pindex = Pilindex.Peek
                                                                                    OleInfoAdapter = New OleDbDataAdapter("select * from COTIERS Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                                                    OleInfoDataset = New DataSet
                                                                                    OleInfoAdapter.Fill(OleInfoDataset)
                                                                                    BaseInfo = OleInfoDataset.Tables(0)
                                                                                    If BaseInfo.Rows.Count <> 0 Then
                                                                                        If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                                            .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                        Else
                                                                                            If Trim(BaseInfo.Rows(0).Item("Type")) = "Numerique" Then
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDbl(Datagridaffiche.Item(Pindex, i).Value)
                                                                                            Else
                                                                                                If BaseInfo.Rows(0).Item("Type") = "Date" Then
                                                                                                    If Strings.Len(Trim(Datagridaffiche.Item(Pindex, i).Value)) = 8 Then
                                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                                    Else
                                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                                    End If
                                                                                                End If
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                    Pindex = Nothing
                                                                                Loop
                                                                                .Write()
                                                                                EcriturelibreA = Nothing
                                                                            End With
                                                                        Catch ex As Exception
                                                                            Error_journal.WriteLine("Message Systeme Information Libre : " & ex.Message)
                                                                        End Try
                                                                    End If
                                                                    Pilindex.Clear()
                                                                End With
                                                            Catch ex As Exception

                                                            End Try
                                                        Else
                                                            Error_journal.WriteLine("Le Tier de Numero de Compte : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Rattaché à un compte Collectif existe déja")
                                                        End If
                                                    Else
                                                        Error_journal.WriteLine("Impossible de Traiter le Tier car son Code est vide")
                                                    End If
                                                Next n
                                                Label8.Refresh()
                                                MenuApplication.TreeView.Refresh()
                                                Datagridaffiche.Refresh()
                                            Catch ex As Exception
                                                Error_journal.WriteLine("Message Systeme :" & ex.Message)
                                            End Try
                                        End If
                                    End If
                                Else
                                    Error_journal.WriteLine("Echec de Connexion à la base Comptable " & Basedatatable.Rows(0).Item("SOCSAGE") & "  Ligne :" & NbreLigne + 1)
                                End If
                            Else
                                'Error_journal.WriteLine("La base Comptable ne Correspond à aucune base Sage " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) & "  Ligne :" & NbreLigne + 1)
                            End If
                        Else    'La Societe est la meme
                            Error_journal.WriteLine("Traitement du Tier " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) & "  Base Comptable Sage :" & BaseComptable & "  Ligne :" & NbreLigne + 1)
                            If Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) = "F" Then
                                Coladaptater = New OleDbDataAdapter("select * from FOURNISSEUR", OleConnenection)
                                Coldataset = New DataSet
                                Coladaptater.Fill(Coldataset)
                                Coldatatable = Coldataset.Tables(0)
                                If Coldatatable.Rows.Count <> 0 Then
                                    Try
                                        For n = 0 To Coldatatable.Rows.Count - 1
                                            If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) <> "" Then
                                                EcriureTier = Nothing
                                                EcriureTier = BaseCpta.FactoryFournisseur.Create
                                                If BaseCpta.FactoryTiers.ExistNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) = False Then
                                                    Try
                                                        With EcriureTier
                                                            If Trim(Coldatatable.Rows(n).Item("Collectif")) <> "" Then
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(Coldatatable.Rows(n).Item("Collectif"))) = True Then
                                                                    .CompteGPrinc = BaseCpta.FactoryCompteG.ReadNumero(Trim(Coldatatable.Rows(n).Item("Collectif")))
                                                                Else
                                                                    Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans Sage")
                                                                End If
                                                            Else
                                                                Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans le Parametrage")
                                                            End If

                                                            For j = 0 To Datagridaffiche.ColumnCount - 1
                                                                If Datagridaffiche.Columns(j).Name = "Compte" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) <= 17 Then
                                                                            .CT_Num = Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")
                                                                            CodeAuxil = Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")
                                                                        Else
                                                                            .CT_Num = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                            CodeAuxil = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Adresse" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                            If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                .Adresse.Adresse = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            Else
                                                                                .Adresse.Adresse = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Mail" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                            .Telecom.EMail = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.EMail = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "APE" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            .CT_Ape = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Ape = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Classement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                            .CT_Classement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Classement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodePostal" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 8 Then
                                                                            .Adresse.CodePostal = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.CodePostal = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 8)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeRegion" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                            .Adresse.CodeRegion = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.CodeRegion = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Commentaire" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Commentaire = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Commentaire = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Complement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .Adresse.Complement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Complement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Contact" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Contact = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Contact = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Identifiant" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                            .CT_Identifiant = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Identifiant = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "ModeReglement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        Dim OleAdaptaterMod As OleDbDataAdapter
                                                                        Dim OleModDataset As DataSet
                                                                        Dim OledatableMod As DataTable
                                                                        OleAdaptaterMod = New OleDbDataAdapter("select * from MODELEREGLEMENT where Code='" & Trim(Datagridaffiche.Item(j, i).Value) & "'", OleConnenection)
                                                                        OleModDataset = New DataSet
                                                                        OleAdaptaterMod.Fill(OleModDataset)
                                                                        OledatableMod = OleModDataset.Tables(0)
                                                                        If OledatableMod.Rows.Count <> 0 Then
                                                                            If BaseCpta.FactoryModeleReglement.ExistIntitule(Trim(OledatableMod.Rows(0).Item("Intitule"))) = True Then
                                                                                ModeleR = Trim(OledatableMod.Rows(0).Item("Intitule"))
                                                                            Else
                                                                                Error_journal.WriteLine("L'Intitule de Reglement " & Trim(OledatableMod.Rows(0).Item("Intitule")) & " Inexistant dans Sage")
                                                                                ModeleR = Nothing
                                                                            End If
                                                                        Else
                                                                            Error_journal.WriteLine("Le Code de Reglement Pragma " & Trim(Datagridaffiche.Item(j, i).Value) & " Inexistant dans le Parametrage")
                                                                            ModeleR = Nothing
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Intitule" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Intitule = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                            Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        End If
                                                                    Else
                                                                        Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Langue" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Langue = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Langue = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "BanquePrincipale" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                    End If
                                                                End If


                                                                If Datagridaffiche.Columns(j).Name = "Devise" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If BaseCpta.FactoryDevise.ExistIntitule(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                            .Devise = BaseCpta.FactoryDevise.ReadIntitule(Trim(Datagridaffiche.Item(j, i).Value))
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "NomSite" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                            .Telecom.Site = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Site = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Pays" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .Adresse.Pays = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Pays = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Qualite" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                            .CT_Qualite = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Qualite = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Siret" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                            .CT_Siret = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Siret = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "Telecopie" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 21 Then
                                                                            .Telecom.Telecopie = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Telecopie = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 21)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Telephone" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                            .Telecom.Telephone = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Telephone = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Ville" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                            .Adresse.Ville = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Ville = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "AdresseBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                            AdresBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            AdresBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            CodBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CodBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "BIC" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            BicBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            BicBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CleRib" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 2 Then
                                                                            CleR = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CleR = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodePostBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            CodPostbanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CodPostbanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CommentBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            CommentBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CommentBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CompleBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then

                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            ComplBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            ComplBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CompteBancaire" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 11 Then
                                                                            CompBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CompBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 11)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeGuichet" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            Codeguichet = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            Codeguichet = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "IBAN" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            IbanBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            IbanBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "IntituleBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            Intibanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            Intibanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "PaysBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            PaysBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            PaysBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "VilBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            VilBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            VilBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                            Next j
                                                            If Trim(Message) <> "" Then
                                                            Else
                                                                LogMessage = "L'Intitule du Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  est vide Ligne  " & NbreLigne + 1
                                                            End If
                                                            .Write()
                                                            Error_journal.WriteLine("Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Créé")
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    .TiersPayeur = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    .Write()
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Rattachement du Tierspayeur : " & ex.Message)
                                                                End Try
                                                            End If

                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    Dim BanqueTier As IBOTiersBanque3
                                                                    Dim BankTier As IBOTiersPart3
                                                                    BankTier = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    BanqueTier = BankTier.FactoryTiersBanque.Create
                                                                    If (Trim(Intibanq) <> "" And Trim(CompBanq) <> "") And (Trim(Codeguichet) <> "" And Trim(CodBanq) <> "") Then
                                                                        With BanqueTier
                                                                            .BT_Compte = CompBanq
                                                                            .BT_Guichet = Codeguichet
                                                                            .BT_Intitule = Intibanq
                                                                            .BT_Cle = CleR
                                                                            .BT_Banque = CodBanq
                                                                            .Adresse.Adresse = AdresBanq
                                                                            .BT_Bic = BicBanq
                                                                            .Adresse.CodePostal = CodPostbanq
                                                                            .BT_Commentaire = CommentBanq
                                                                            .Adresse.Complement = ComplBanq
                                                                            .BT_IBAN = IbanBanq
                                                                            .Adresse.Pays = PaysBanq
                                                                            .Adresse.Ville = VilBanq
                                                                            LogMessage = "Erreur de Création de la Banque rattachée au Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Créé  Erreur de cohérence des données" & NbreLigne + 1
                                                                            .Write()
                                                                        End With
                                                                    Else
                                                                        Error_journal.WriteLine("Impossible de Créer la Banque rattachée au Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Créé :  N°Compte bancaire et Intitulé bancaire et Code Guichet et Code Banque Obligatoires  " & NbreLigne + 1)
                                                                    End If

                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Banque du Tier : " & ex.Message)
                                                                End Try
                                                            End If
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    If BaseCpta.FactoryModeleReglement.ExistIntitule(ModeleR) = True Then
                                                                        .ModeleReglement = BaseCpta.FactoryModeleReglement.ReadIntitule(ModeleR)
                                                                        .WriteDefault()
                                                                    End If
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Rattachement du Modele de Reglement : " & ex.Message)
                                                                End Try
                                                            End If

                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    EcriturelibreA = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    With EcriturelibreA
                                                                        Do While Pilindex.Count > 0
                                                                            Dim OleInfoAdapter As OleDbDataAdapter
                                                                            Dim OleInfoDataset As DataSet
                                                                            Dim BaseInfo As DataTable
                                                                            Dim Pindex As Integer
                                                                            Pindex = Pilindex.Peek
                                                                            OleInfoAdapter = New OleDbDataAdapter("select * from COTIERS Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                                            OleInfoDataset = New DataSet
                                                                            OleInfoAdapter.Fill(OleInfoDataset)
                                                                            BaseInfo = OleInfoDataset.Tables(0)
                                                                            If BaseInfo.Rows.Count <> 0 Then
                                                                                If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                                    .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                Else
                                                                                    If Trim(BaseInfo.Rows(0).Item("Type")) = "Numerique" Then
                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDbl(Datagridaffiche.Item(Pindex, i).Value)
                                                                                    Else
                                                                                        If BaseInfo.Rows(0).Item("Type") = "Date" Then
                                                                                            If Strings.Len(Trim(Datagridaffiche.Item(Pindex, i).Value)) = 8 Then
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                            Else
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            Pindex = Nothing
                                                                        Loop
                                                                        .Write()
                                                                        EcriturelibreA = Nothing
                                                                    End With
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Information Libre : " & ex.Message)
                                                                End Try
                                                            End If
                                                            Pilindex.Clear()
                                                        End With
                                                    Catch ex As Exception

                                                    End Try

                                                Else
                                                    Error_journal.WriteLine("Le Tier de Numero de Compte : " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Rattaché à un compte Collectif existe déja")
                                                End If
                                            Else
                                                Error_journal.WriteLine("Impossible de Traiter le Tier car son Code est vide")
                                            End If
                                        Next n
                                        Label8.Refresh()
                                        MenuApplication.TreeView.Refresh()
                                        Datagridaffiche.Refresh()
                                    Catch ex As Exception
                                        Error_journal.WriteLine("Message Systeme : " & ex.Message)
                                    End Try

                                End If
                            Else 'Fournisseur specifique fffffffffffffffffffffffffffffffffff
                                Coladaptater = New OleDbDataAdapter("select * from FOURNISSEURS", OleConnenection)
                                Coldataset = New DataSet
                                Coladaptater.Fill(Coldataset)
                                Coldatatable = Coldataset.Tables(0)
                                If Coldatatable.Rows.Count <> 0 Then
                                    Try
                                        For n = 0 To Coldatatable.Rows.Count - 1
                                            If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value) <> "" Then
                                                EcriureTier = Nothing
                                                EcriureTier = BaseCpta.FactoryFournisseur.Create
                                                If BaseCpta.FactoryTiers.ExistNumero(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) = False Then
                                                    Try
                                                        With EcriureTier
                                                            If Trim(Coldatatable.Rows(n).Item("Collectif")) <> "" Then
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(Coldatatable.Rows(n).Item("Collectif"))) = True Then
                                                                    .CompteGPrinc = BaseCpta.FactoryCompteG.ReadNumero(Trim(Coldatatable.Rows(n).Item("Collectif")))
                                                                Else
                                                                    Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans Sage")
                                                                End If
                                                            Else
                                                                Error_journal.WriteLine("Le Compte Collectif " & Trim(Coldatatable.Rows(n).Item("Collectif")) & " Inexistant dans le Parametrage")
                                                            End If

                                                            For j = 0 To Datagridaffiche.ColumnCount - 1
                                                                If Datagridaffiche.Columns(j).Name = "Compte" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")) <= 17 Then
                                                                            .CT_Num = Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")
                                                                            CodeAuxil = Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "")
                                                                        Else
                                                                            .CT_Num = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                            CodeAuxil = Strings.Left(Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), ""), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Adresse" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                            If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                                .Adresse.Adresse = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            Else
                                                                                .Adresse.Adresse = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Mail" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                            .Telecom.EMail = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.EMail = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "APE" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            .CT_Ape = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Ape = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Classement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                            .CT_Classement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Classement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodePostal" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 8 Then
                                                                            .Adresse.CodePostal = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.CodePostal = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 8)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeRegion" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                            .Adresse.CodeRegion = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.CodeRegion = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Commentaire" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Commentaire = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Commentaire = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Complement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .Adresse.Complement = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Complement = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Contact" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Contact = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Contact = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Identifiant" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 25 Then
                                                                            .CT_Identifiant = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Identifiant = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 25)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "ModeReglement" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        Dim OleAdaptaterMod As OleDbDataAdapter
                                                                        Dim OleModDataset As DataSet
                                                                        Dim OledatableMod As DataTable
                                                                        OleAdaptaterMod = New OleDbDataAdapter("select * from MODELEREGLEMENT where Code='" & Trim(Datagridaffiche.Item(j, i).Value) & "'", OleConnenection)
                                                                        OleModDataset = New DataSet
                                                                        OleAdaptaterMod.Fill(OleModDataset)
                                                                        OledatableMod = OleModDataset.Tables(0)
                                                                        If OledatableMod.Rows.Count <> 0 Then
                                                                            If BaseCpta.FactoryModeleReglement.ExistIntitule(Trim(OledatableMod.Rows(0).Item("Intitule"))) = True Then
                                                                                ModeleR = Trim(OledatableMod.Rows(0).Item("Intitule"))
                                                                            Else
                                                                                Error_journal.WriteLine("L'Intitule de Reglement " & Trim(OledatableMod.Rows(0).Item("Intitule")) & " Inexistant dans Sage")
                                                                                ModeleR = Nothing
                                                                            End If
                                                                        Else
                                                                            Error_journal.WriteLine("Le Code de Reglement Pragma " & Trim(Datagridaffiche.Item(j, i).Value) & " Inexistant dans le Parametrage")
                                                                            ModeleR = Nothing
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Intitule" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Intitule = Trim(Datagridaffiche.Item(j, i).Value)
                                                                            Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                            Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        End If
                                                                    Else
                                                                        Message = Trim(Datagridaffiche.Item(j, i).Value)
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Langue" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .CT_Langue = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Langue = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "BanquePrincipale" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                    End If
                                                                End If


                                                                If Datagridaffiche.Columns(j).Name = "Devise" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If BaseCpta.FactoryDevise.ExistIntitule(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                            .Devise = BaseCpta.FactoryDevise.ReadIntitule(Trim(Datagridaffiche.Item(j, i).Value))
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "NomSite" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 69 Then
                                                                            .Telecom.Site = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Site = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 69)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Pays" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            .Adresse.Pays = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Pays = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Qualite" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 17 Then
                                                                            .CT_Qualite = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Qualite = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 17)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Siret" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                            .CT_Siret = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .CT_Siret = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "Telecopie" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 21 Then
                                                                            .Telecom.Telecopie = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Telecopie = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 21)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Telephone" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 15 Then
                                                                            .Telecom.Telephone = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Telecom.Telephone = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 15)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "Ville" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                            .Adresse.Ville = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            .Adresse.Ville = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "AdresseBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 30 Then
                                                                            AdresBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            AdresBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 30)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            CodBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CodBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "BIC" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            BicBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            BicBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CleRib" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 2 Then
                                                                            CleR = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CleR = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodePostBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            CodPostbanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CodPostbanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CommentBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            CommentBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CommentBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CompleBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then

                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            ComplBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            ComplBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CompteBancaire" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 11 Then
                                                                            CompBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            CompBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 11)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "CodeGuichet" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 5 Then
                                                                            Codeguichet = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            Codeguichet = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 5)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "IBAN" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            IbanBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            IbanBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                                If Datagridaffiche.Columns(j).Name = "IntituleBanque" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            Intibanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            Intibanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "PaysBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            PaysBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            PaysBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If

                                                                If Datagridaffiche.Columns(j).Name = "VilBan" Then
                                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                                        If Strings.Len(Trim(Datagridaffiche.Item(j, i).Value)) <= 35 Then
                                                                            VilBanq = Trim(Datagridaffiche.Item(j, i).Value)
                                                                        Else
                                                                            VilBanq = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                                        End If
                                                                    End If
                                                                End If
                                                            Next j
                                                            If Trim(Message) <> "" Then
                                                            Else
                                                                LogMessage = "L'Intitule du Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  est vide Ligne  " & NbreLigne + 1
                                                            End If
                                                            .Write()
                                                            Error_journal.WriteLine("Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & " Créé")
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    .TiersPayeur = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    .Write()
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Rattachement du Tierspayeur : " & ex.Message)
                                                                End Try
                                                            End If
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    Dim BanqueTier As IBOTiersBanque3
                                                                    Dim BankTier As IBOTiersPart3
                                                                    BankTier = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    BanqueTier = BankTier.FactoryTiersBanque.Create
                                                                    If (Trim(Intibanq) <> "" And Trim(CompBanq) <> "") And (Trim(Codeguichet) <> "" And Trim(CodBanq) <> "") Then
                                                                        With BanqueTier
                                                                            .BT_Compte = CompBanq
                                                                            .BT_Guichet = Codeguichet
                                                                            .BT_Intitule = Intibanq
                                                                            .BT_Cle = CleR
                                                                            .BT_Banque = CodBanq
                                                                            .Adresse.Adresse = AdresBanq
                                                                            .BT_Bic = BicBanq
                                                                            .Adresse.CodePostal = CodPostbanq
                                                                            .BT_Commentaire = CommentBanq
                                                                            .Adresse.Complement = ComplBanq
                                                                            .BT_IBAN = IbanBanq
                                                                            .Adresse.Pays = PaysBanq
                                                                            .Adresse.Ville = VilBanq
                                                                            LogMessage = "Erreur de Création de la Banque rattachée au Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Créé  Erreur de cohérence des données" & NbreLigne + 1
                                                                            .Write()
                                                                        End With
                                                                    Else
                                                                        Error_journal.WriteLine("Impossible de Créer la Banque rattachée au Tier " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Créé :  N°Compte bancaire et Intitulé bancaire et Code Guichet et Code Banque Obligatoires  " & NbreLigne + 1)
                                                                    End If

                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Banque du Tier : " & ex.Message)
                                                                End Try
                                                            End If
                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    If BaseCpta.FactoryModeleReglement.ExistIntitule(ModeleR) = True Then
                                                                        .ModeleReglement = BaseCpta.FactoryModeleReglement.ReadIntitule(ModeleR)
                                                                        .WriteDefault()
                                                                    End If
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Rattachement du Modele de Reglement : " & ex.Message)
                                                                End Try
                                                            End If

                                                            If BaseCpta.FactoryTiers.ExistNumero(.CT_Num) = True Then
                                                                Try
                                                                    EcriturelibreA = BaseCpta.FactoryTiers.ReadNumero(.CT_Num)
                                                                    With EcriturelibreA
                                                                        Do While Pilindex.Count > 0
                                                                            Dim OleInfoAdapter As OleDbDataAdapter
                                                                            Dim OleInfoDataset As DataSet
                                                                            Dim BaseInfo As DataTable
                                                                            Dim Pindex As Integer
                                                                            Pindex = Pilindex.Peek
                                                                            OleInfoAdapter = New OleDbDataAdapter("select * from COTIERS Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                                            OleInfoDataset = New DataSet
                                                                            OleInfoAdapter.Fill(OleInfoDataset)
                                                                            BaseInfo = OleInfoDataset.Tables(0)
                                                                            If BaseInfo.Rows.Count <> 0 Then
                                                                                If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                                    .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                                Else
                                                                                    If Trim(BaseInfo.Rows(0).Item("Type")) = "Numerique" Then
                                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDbl(Datagridaffiche.Item(Pindex, i).Value)
                                                                                    Else
                                                                                        If BaseInfo.Rows(0).Item("Type") = "Date" Then
                                                                                            If Strings.Len(Trim(Datagridaffiche.Item(Pindex, i).Value)) = 8 Then
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                            Else
                                                                                                .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = CDate(Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(Pindex, i).Value), 1, 4))
                                                                                            End If
                                                                                        End If
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                            Pindex = Nothing
                                                                        Loop
                                                                        .Write()
                                                                        EcriturelibreA = Nothing
                                                                    End With
                                                                Catch ex As Exception
                                                                    Error_journal.WriteLine("Message Systeme Information Libre : " & ex.Message)
                                                                End Try
                                                            End If
                                                            Pilindex.Clear()

                                                        End With
                                                    Catch ex As Exception

                                                    End Try

                                                Else
                                                    Error_journal.WriteLine("Le Tier de Numero de Compte Rattaché à un compte Collectif: " & Join(Split(Strings.Left(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), 1) & "" & Strings.Right(Trim(Coldatatable.Rows(n).Item("Plant")), 1) & "" & Strings.Right(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value), Strings.Len(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Compte").ColumnIndex, i).Value)) - 1), " "), "") & "  Existe déja")
                                                End If
                                            Else
                                                Error_journal.WriteLine("Impossible de Traiter le Tier car son Code est vide")
                                            End If
                                        Next n
                                        Label8.Refresh()
                                        MenuApplication.TreeView.Refresh()
                                        Datagridaffiche.Refresh()
                                    Catch ex As Exception
                                        Error_journal.WriteLine("Message Systeme: " & ex.Message)
                                    End Try
                                End If
                            End If
                        End If
                    End If 'Fin de comparaison de la societe
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    NbreLigne = NbreLigne + 1
                    Label8.Text = NbreLigne & "/" & ProgresMax
                    Label8.Refresh()
                Catch ex As Exception
                    Error_journal.WriteLine("Erreur de Creation du Tier  " & ex.Message)
                    Error_journal.WriteLine("Ou " & LogMessage)
                    Error_journal.WriteLine(" ")
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    NbreLigne = NbreLigne + 1
                    Label8.Text = NbreLigne & "/" & ProgresMax
                End Try
            Next i
            Datagridaffiche.Rows.Clear()
        End If
        Datagridaffiche.Rows.Clear()
        Me.Cursor = Cursors.Default
        BT_integrer.Enabled = True
    End Sub
    Private Sub BT_Quitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Quitter.Click
        Me.Close()
    End Sub
    Private Sub BT_integrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_integrer.Click
        Dim i As Integer
        Dim NomFichier As String
        Dim Fichieratraiter As String
        Fichieratraiter = Nothing
        Try
            'Dim j As Integer
            'Dim aLines() As String
            'If Directory.Exists(Pathsfilejournal) = True Then
            '    aLines = Directory.GetFiles(Pathsfilejournal)
            '    For j = 0 To UBound(aLines)
            '        File.Delete(aLines(j))
            '    Next j
            'End If
            If DataListeIntegrer.RowCount >= 0 Then
                Fichieratraiter = Pathsfilejournal & "LOGIMPORT_FRS" & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]"
                Error_journal = File.AppendText(Fichieratraiter & ".txt")
                For i = 0 To DataListeIntegrer.RowCount - 1
                    If DataListeIntegrer.Rows(i).Cells("Valider").Value = True Then
                        If File.Exists(DataListeIntegrer.Rows(i).Cells("Chemin").Value) = True And File.Exists(DataListeIntegrer.Rows(i).Cells("CheminExport").Value) = True Then
                            Label5.Refresh()
                            Label5.Text = "Lancement des Integrations! Verification des Ecritures..."
                            TxtChemin.Text = DataListeIntegrer.Rows(i).Cells("CheminExport").Value
                            TxtFormat.Text = DataListeIntegrer.Rows(i).Cells("Chemin").Value
                            NomFichier = Trim(DataListeIntegrer.Rows(i).Cells("CheminExport").Value)
                            Do While InStr(Trim(NomFichier), "\") <> 0
                                NomFichier = Strings.Right(NomFichier, Strings.Len(Trim(NomFichier)) - InStr(Trim(NomFichier), "\"))
                            Loop
                            Error_journal.WriteLine("Fichier à Traité: " & NomFichier & "  Date d'import: " & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]")
                            Error_journal.WriteLine("")
                            Label5.Refresh()
                            Label5.Text = "Verification Terminée! Integration En Cours"
                            Integration_Du_Fichier(DataListeIntegrer.Rows(i).Cells("CheminExport").Value, DataListeIntegrer.Rows(i).Cells("Chemin").Value)
                            Modification_Du_Fichier(DataListeIntegrer.Rows(i).Cells("CheminExport").Value, DataListeIntegrer.Rows(i).Cells("Chemin").Value)
                            File.Move(DataListeIntegrer.Rows(i).Cells("CheminExport").Value, PathsfileSave & "" & Join(Split(NomFichier, "."), "_" & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]."))
                            DataListeIntegrer.Rows(i).Cells("Valider").Value = False
                            Label5.Refresh()
                            Label5.Text = "Integration Terminée! Suppression des Fichiers exécutée..."
                            Error_journal.WriteLine("Fin de Traitement du Fichier : " & NomFichier & "  Date Fin d'import: " & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]")
                            Error_journal.WriteLine("")
                            Error_journal.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                        End If
                    End If
                Next i
                Error_journal.Close()
            End If
            AfficheSchemasIntegrer()
        Catch ex As Exception
        End Try

    End Sub
    Private Sub DataListeIntegrer_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeIntegrer.CellClick
        If e.RowIndex >= 0 Then
            If DataListeIntegrer.Columns(e.ColumnIndex).Name = "Valider" Then
                IndexPrec = e.RowIndex
                TxtChemin.Text = ""
                TxtFormat.Text = ""
            End If
        End If
    End Sub
    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub FournisseurImportation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Connected() = True Then
            AfficheSchemasIntegrer()
            Initialiser()
            Datagridaffiche.Rows.Clear()
            Datagridaffiche.Columns.Clear()
            Pilindex.Clear()
            Me.WindowState = FormWindowState.Maximized
            GroupBox6.Width = Me.Width
        End If
    End Sub

    Private Sub DataListeIntegrer_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeIntegrer.CellContentClick

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim i As Integer
    '    Dim NomFichier As String
    '    Dim Fichieratraiter As String
    '    Fichieratraiter = Nothing
    '    Try
    '        'Dim j As Integer
    '        'Dim aLines() As String
    '        'If Directory.Exists(Pathsfilejournal) = True Then
    '        '    aLines = Directory.GetFiles(Pathsfilejournal)
    '        '    For j = 0 To UBound(aLines)
    '        '        File.Delete(aLines(j))
    '        '    Next j
    '        'End If
    '        If DataListeIntegrer.RowCount >= 0 Then
    '            Fichieratraiter = Pathsfilejournal & "LOGIMPORT_FRS" & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]"
    '            Error_journal = File.AppendText(Fichieratraiter & ".txt")
    '            For i = 0 To DataListeIntegrer.RowCount - 1
    '                If DataListeIntegrer.Rows(i).Cells("Valider").Value = True Then
    '                    If File.Exists(DataListeIntegrer.Rows(i).Cells("Chemin").Value) = True And File.Exists(DataListeIntegrer.Rows(i).Cells("CheminExport").Value) = True Then
    '                        Label5.Refresh()
    '                        Label5.Text = "Lancement des Modifications! Verification des Ecritures..."
    '                        TxtChemin.Text = DataListeIntegrer.Rows(i).Cells("CheminExport").Value
    '                        TxtFormat.Text = DataListeIntegrer.Rows(i).Cells("Chemin").Value
    '                        NomFichier = Trim(DataListeIntegrer.Rows(i).Cells("CheminExport").Value)
    '                        Do While InStr(Trim(NomFichier), "\") <> 0
    '                            NomFichier = Strings.Right(NomFichier, Strings.Len(Trim(NomFichier)) - InStr(Trim(NomFichier), "\"))
    '                        Loop
    '                        Error_journal.WriteLine("Fichier à Traité: " & NomFichier & "  Date d'import: " & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]")
    '                        Error_journal.WriteLine("")
    '                        Label5.Refresh()
    '                        Label5.Text = "Verification Terminée! Integration En Cours"
    '                        Modification_Du_Fichier(DataListeIntegrer.Rows(i).Cells("CheminExport").Value, DataListeIntegrer.Rows(i).Cells("Chemin").Value)
    '                        File.Move(DataListeIntegrer.Rows(i).Cells("CheminExport").Value, PathsfileSave & "" & Join(Split(NomFichier, "."), "_" & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]."))
    '                        DataListeIntegrer.Rows(i).Cells("Valider").Value = False
    '                        Label5.Refresh()
    '                        Label5.Text = "Modification Terminée! Suppression des Fichiers exécutée..."
    '                        Error_journal.WriteLine("Fin de Traitement du Fichier : " & NomFichier & "  Date Fin d'import: " & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]")
    '                        Error_journal.WriteLine("")
    '                        Error_journal.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
    '                    End If
    '                End If
    '            Next i
    '            Error_journal.Close()
    '        End If
    '        AfficheSchemasIntegrer()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub SplitContainer2_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer2.Panel2.Paint

    End Sub
End Class
