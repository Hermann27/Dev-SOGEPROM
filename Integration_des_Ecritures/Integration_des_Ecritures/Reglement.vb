Imports System.Data.OleDb
Imports System.IO
Imports Objets100Lib
Public Class Reglement
    Public OleReglement As OleDbConnection
    Public Controle As String
    Public ValeurControle As String
    Public Genereration As String
    Public Lettrage As String
    Public Line_Reglement As New Stack(Of String)
    Public File_Reglement As StreamWriter
    Public File_Rejournal As StreamWriter

    Private Sub AfficheSchemasConso()
        Try
            Dim i As Integer
            Dim OleAdaptaterschema As OleDbDataAdapter
            Dim OleSchemaDataset As DataSet
            Dim OledatableSchema As DataTable
            DataListeIntegrer.Rows.Clear()
            Dim BaseCatal() As String
            OleAdaptaterschema = New OleDbDataAdapter("select * from SOCIETE", OleConnenection)
            OleSchemaDataset = New DataSet
            OleAdaptaterschema.Fill(OleSchemaDataset)
            OledatableSchema = OleSchemaDataset.Tables(0)
            DataListeIntegrer.RowCount = OledatableSchema.Rows.Count
            For i = 0 To OledatableSchema.Rows.Count - 1
                DataListeIntegrer.Rows(i).Cells("Societe1").Value = OledatableSchema.Rows(i).Item("SOCGPS")
                DataListeIntegrer.Rows(i).Cells("Base1").Value = OledatableSchema.Rows(i).Item("SOCSAGE")
                BaseCatal = Split(OledatableSchema.Rows(i).Item("SOCSAGE"), ".")
                DataListeIntegrer.Rows(i).Cells("Fichier1").Value = Trim(BaseCatal(0))
                DataListeIntegrer.Rows(i).Cells("Server1").Value = Trim(OledatableSchema.Rows(i).Item("Server"))
                DataListeIntegrer.Rows(i).Cells("Utilisateur1").Value = Trim(OledatableSchema.Rows(i).Item("ServerUser"))
                DataListeIntegrer.Rows(i).Cells("Passe1").Value = Trim(OledatableSchema.Rows(i).Item("ServerPasse"))
                DataListeIntegrer.Rows(i).Cells("Fichier").Value = Trim(OledatableSchema.Rows(i).Item("FICHIERCPTA"))
                DataListeIntegrer.Rows(i).Cells("Utilisateur").Value = Trim(OledatableSchema.Rows(i).Item("UTILISATEUR"))
                DataListeIntegrer.Rows(i).Cells("Passe").Value = Trim(OledatableSchema.Rows(i).Item("MOTPASSE"))
            Next i
        Catch ex As Exception
            MsgBox("Message Systeme: " & ex.Message, MsgBoxStyle.Information, "Societes Comptables")
        End Try
    End Sub
    Private Sub BT_Quit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Quit.Click
        Me.Close()
    End Sub

    Private Sub Reglement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim LibreOleAdaptater As OleDbDataAdapter
        Dim Libredataset As DataSet
        Dim Libredatabase As DataTable
        Dim i As Integer
        If Connected() = True Then
            AfficheSchemasConso()
            Line_Reglement.Clear()
            LibreOleAdaptater = New OleDbDataAdapter("select * from REGLEMENT", OleConnenection)
            Libredataset = New DataSet
            LibreOleAdaptater.Fill(Libredataset)
            Libredatabase = Libredataset.Tables(0)
            If Libredatabase.Rows.Count <> 0 Then
                For i = 0 To Libredatabase.Rows.Count - 1
                    If Trim(Libredatabase.Rows(i).Item("Libelle")) = "Date Generation" Then
                        Genereration = Trim(Libredatabase.Rows(i).Item("Colonne"))
                    End If
                    If Trim(Libredatabase.Rows(i).Item("Libelle")) = "Zone Libre Flag" Then
                        Controle = Trim(Libredatabase.Rows(i).Item("Colonne"))
                        ValeurControle = Trim(Libredatabase.Rows(i).Item("Valeur"))
                    End If
                    If Trim(Libredatabase.Rows(i).Item("Libelle")) = "Lettrage GPS" Then
                        Lettrage = Trim(Libredatabase.Rows(i).Item("Colonne"))
                    End If
                Next i
            End If
        End If
        Me.WindowState = FormWindowState.Maximized
        GroupBox4.Width = Me.Width
    End Sub

    Private Sub BT_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Save.Click
        Me.Cursor = Cursors.WaitCursor
        BT_Save.Enabled = False
        Generation_des_Reglements()
        BT_Save.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub Generation_des_Reglements()
        Try
            'Dim OleAdaptaterRe As OleDbDataAdapter
            'Dim OleReDataset As DataSet
            'Dim OledatableRe As DataTable
            Dim Fich() As String
            Dim FicReglement As String
            Dim Line_Number As Integer
            Dim i As Integer
            Dim k As Integer
            Dim aLines() As String
            If Directory.Exists(PathsfileExport) = True Then
                aLines = Directory.GetFiles(PathsfileExport)
                For i = 0 To UBound(aLines)
                    File.Delete(aLines(i))
                Next i
            End If
            File_Rejournal = File.AppendText(Reglementjournal & "LOGEXPORT_REGL_" & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "].txt")
            FicReglement = PathsfileExport & "RGL_" & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "].txt"
            ProgressBar1.Value = ProgressBar1.Minimum
            ProgressBar1.Maximum = DataListeIntegrer.RowCount
            For k = 0 To DataListeIntegrer.RowCount - 1
                Line_Number = 0
                Me.Refresh()
                MenuApplication.TreeView.Refresh()
                MenuApplication.Refresh()
                If DataListeIntegrer.Rows(k).Cells("Supprimer").Value = True Then
                    If ReglementConnected(Trim(DataListeIntegrer.Rows(k).Cells("Server1").Value), Trim(DataListeIntegrer.Rows(k).Cells("Fichier1").Value), Trim(DataListeIntegrer.Rows(k).Cells("Utilisateur1").Value), Trim(DataListeIntegrer.Rows(k).Cells("Passe1").Value)) = True Then
                        CloseBaseFree()
                        If OpenBaseCpta(BaseCpta, Trim(DataListeIntegrer.Rows(k).Cells("Fichier").Value), Trim(DataListeIntegrer.Rows(k).Cells("Utilisateur").Value), Trim(DataListeIntegrer.Rows(k).Cells("Passe").Value)) = True Then
                            If Verification(BaseCpta, Genereration) = True Then
                                If Verification(BaseCpta, Lettrage) = True Then
                                    If Verification(BaseCpta, Controle) = True Then
                                        Dim OleAdaptaterRecrit As OleDbDataAdapter
                                        Dim OleRecritDataset As DataSet
                                        Dim OledatableRecrit As DataTable
                                        Dim j As Integer
                                        OleAdaptaterRecrit = New OleDbDataAdapter("select * from F_ECRITUREC WHERE  (" & Lettrage & "<>'' And EC_Lettrage<>'') and (" & Controle & " IS NULL OR " & Controle & "='')", OleReglement)
                                        OleRecritDataset = New DataSet
                                        OleAdaptaterRecrit.Fill(OleRecritDataset)
                                        OledatableRecrit = OleRecritDataset.Tables(0)
                                        If OledatableRecrit.Rows.Count <> 0 Then
                                            For j = 0 To OledatableRecrit.Rows.Count - 1
                                                Dim Ecriture As IBOEcriture3
                                                If BaseCpta.FactoryEcriture.ExistNumero(CDbl(OledatableRecrit.Rows(j).Item("EC_No"))) = True Then
                                                    If Convert.IsDBNull(OledatableRecrit.Rows(j).Item("CT_Num")) = False Then
                                                        If 6 - Len(Trim(OledatableRecrit.Rows(j).Item("EC_No"))) >= 0 Then
                                                            Dim test As Object = Trim(OledatableRecrit.Rows(j).Item("EC_No"))
                                                            If CDbl(OledatableRecrit.Rows(j).Item("EC_MontantRegle")) = 0 Then
                                                                Line_Reglement.Push(Trim(DataListeIntegrer.Rows(k).Cells("Societe1").Value) & "" & ControlChars.Tab & "" & Strings.Left(Trim(OledatableRecrit.Rows(j).Item("CT_Num")), 1) & "" & Strings.Right(Trim(OledatableRecrit.Rows(j).Item("CT_Num")), Strings.Len(Trim(OledatableRecrit.Rows(j).Item("CT_Num"))) - 2) & "" & ControlChars.Tab & "" & (CDbl(OledatableRecrit.Rows(j).Item("EC_Montant")) * 100) & "" & ControlChars.Tab & "" & DateAndTime.Year(OledatableRecrit.Rows(j).Item("EC_DateRegle")) & "" & Strings.Format(DateAndTime.Month(OledatableRecrit.Rows(j).Item("EC_DateRegle")), "00") & "" & Strings.Format(DateAndTime.Day(OledatableRecrit.Rows(j).Item("EC_DateRegle")), "00") & "" & ControlChars.Tab & OledatableRecrit.Rows(j).Item("" & Lettrage & "") & "" & ControlChars.Tab & Strings.Left(Trim(DataListeIntegrer.Rows(k).Cells("Societe1").Value), 3) & "" & Strings.StrDup(6 - Len(Trim(OledatableRecrit.Rows(j).Item("EC_No"))), "0") & "" & Trim(OledatableRecrit.Rows(j).Item("EC_No")))
                                                            Else
                                                                Line_Reglement.Push(Trim(DataListeIntegrer.Rows(k).Cells("Societe1").Value) & "" & ControlChars.Tab & "" & Strings.Left(Trim(OledatableRecrit.Rows(j).Item("CT_Num")), 1) & "" & Strings.Right(Trim(OledatableRecrit.Rows(j).Item("CT_Num")), Strings.Len(Trim(OledatableRecrit.Rows(j).Item("CT_Num"))) - 2) & "" & ControlChars.Tab & "" & (CDbl(OledatableRecrit.Rows(j).Item("EC_MontantRegle")) * 100) & "" & ControlChars.Tab & "" & DateAndTime.Year(OledatableRecrit.Rows(j).Item("EC_DateRegle")) & "" & Strings.Format(DateAndTime.Month(OledatableRecrit.Rows(j).Item("EC_DateRegle")), "00") & "" & Strings.Format(DateAndTime.Day(OledatableRecrit.Rows(j).Item("EC_DateRegle")), "00") & "" & ControlChars.Tab & OledatableRecrit.Rows(j).Item("" & Lettrage & "") & "" & ControlChars.Tab & Strings.Left(Trim(DataListeIntegrer.Rows(k).Cells("Societe1").Value), 3) & "" & Strings.StrDup(6 - Len(Trim(OledatableRecrit.Rows(j).Item("EC_No"))), "0") & "" & Trim(OledatableRecrit.Rows(j).Item("EC_No")))
                                                            End If
                                                            Ecriture = BaseCpta.FactoryEcriture.ReadNumero(CDbl(OledatableRecrit.Rows(j).Item("EC_No")))
                                                            With Ecriture
                                                                .InfoLibre("" & Genereration & "") = Now
                                                                .InfoLibre("" & Controle & "") = Trim(ValeurControle)
                                                                .Write()
                                                                Line_Number = Line_Number + 1
                                                            End With

                                                        Else
                                                            If CDbl(OledatableRecrit.Rows(j).Item("EC_MontantRegle")) = 0 Then
                                                                Line_Reglement.Push(Trim(DataListeIntegrer.Rows(k).Cells("Societe1").Value) & "" & ControlChars.Tab & "" & Strings.Left(Trim(OledatableRecrit.Rows(j).Item("CT_Num")), 1) & "" & Strings.Right(Trim(OledatableRecrit.Rows(j).Item("CT_Num")), Strings.Len(Trim(OledatableRecrit.Rows(j).Item("CT_Num"))) - 2) & "" & ControlChars.Tab & "" & (CDbl(OledatableRecrit.Rows(j).Item("EC_Montant")) * 100) & "" & ControlChars.Tab & "" & DateAndTime.Year(OledatableRecrit.Rows(j).Item("EC_DateRegle")) & "" & Strings.Format(DateAndTime.Month(OledatableRecrit.Rows(j).Item("EC_DateRegle")), "00") & "" & Strings.Format(DateAndTime.Day(OledatableRecrit.Rows(j).Item("EC_DateRegle")), "00") & "" & ControlChars.Tab & OledatableRecrit.Rows(j).Item("" & Lettrage & "") & "" & ControlChars.Tab & Strings.Left(Trim(DataListeIntegrer.Rows(k).Cells("Societe1").Value), 3) & "" & Strings.Left(Trim(OledatableRecrit.Rows(j).Item("EC_No")), 6))
                                                            Else
                                                                Line_Reglement.Push(Trim(DataListeIntegrer.Rows(k).Cells("Societe1").Value) & "" & ControlChars.Tab & "" & Strings.Left(Trim(OledatableRecrit.Rows(j).Item("CT_Num")), 1) & "" & Strings.Right(Trim(OledatableRecrit.Rows(j).Item("CT_Num")), Strings.Len(Trim(OledatableRecrit.Rows(j).Item("CT_Num"))) - 2) & "" & ControlChars.Tab & "" & (CDbl(OledatableRecrit.Rows(j).Item("EC_MontantRegle")) * 100) & "" & ControlChars.Tab & "" & DateAndTime.Year(OledatableRecrit.Rows(j).Item("EC_DateRegle")) & "" & Strings.Format(DateAndTime.Month(OledatableRecrit.Rows(j).Item("EC_DateRegle")), "00") & "" & Strings.Format(DateAndTime.Day(OledatableRecrit.Rows(j).Item("EC_DateRegle")), "00") & "" & ControlChars.Tab & OledatableRecrit.Rows(j).Item("" & Lettrage & "") & "" & ControlChars.Tab & Strings.Left(Trim(DataListeIntegrer.Rows(k).Cells("Societe1").Value), 3) & "" & Strings.Left(Trim(OledatableRecrit.Rows(j).Item("EC_No")), 6))
                                                            End If
                                                            Ecriture = BaseCpta.FactoryEcriture.ReadNumero(CDbl(OledatableRecrit.Rows(j).Item("EC_No")))
                                                            With Ecriture
                                                                .InfoLibre("" & Genereration & "") = Now
                                                                .InfoLibre("" & Controle & "") = Trim(ValeurControle)
                                                                .Write()
                                                                Line_Number = Line_Number + 1
                                                            End With

                                                        End If
                                                    Else
                                                        File_Rejournal.WriteLine("Le Code Tiers est Obligatoire N°ECR " & CDbl(OledatableRecrit.Rows(j).Item("EC_No")) & "  Base Comptable" & Trim(DataListeIntegrer.Rows(k).Cells("Fichier").Value))
                                                    End If
                                                End If
                                            Next j
                                        End If
                                    Else
                                        File_Rejournal.WriteLine("La zone d'information libre  " & Controle & "  Base Comptable" & Trim(DataListeIntegrer.Rows(k).Cells("Fichier").Value) & "  n'existe pas dans Sage 100")
                                    End If
                                Else
                                    File_Rejournal.WriteLine("La zone d'information libre  " & Lettrage & " Base Comptable" & Trim(DataListeIntegrer.Rows(k).Cells("Fichier").Value) & "  n'existe pas dans Sage 100")
                                End If
                            Else
                                File_Rejournal.WriteLine("La zone d'information libre  " & Genereration & " Base Comptable" & Trim(DataListeIntegrer.Rows(k).Cells("Fichier").Value) & "  n'existe pas dans Sage 100")
                            End If
                        Else
                            File_Rejournal.WriteLine("Echec de Connexion à la base Comptable Sage 100  :" & Trim(DataListeIntegrer.Rows(k).Cells("Fichier").Value) & "  Utilisateur  " & Trim(DataListeIntegrer.Rows(k).Cells("Utilisateur").Value) & "  Mot de Passe  " & Trim(DataListeIntegrer.Rows(k).Cells("Passe").Value))
                        End If
                    Else
                        File_Rejournal.WriteLine("Echec de Connexion à SQL SERVER! Server" & Trim(DataListeIntegrer.Rows(k).Cells("Server1").Value) & "  Base SQL  " & Trim(DataListeIntegrer.Rows(k).Cells("Fichier1").Value) & "  Utilisateur  " & Trim(DataListeIntegrer.Rows(k).Cells("Utilisateur1").Value) & "  Mot de Passe  " & Trim(DataListeIntegrer.Rows(k).Cells("Passe1").Value))
                    End If
                    File_Rejournal.WriteLine("Nombre de Ligne d'Ecritures Lettrées: " & Line_Number & "  Fichier Compta  " & Trim(DataListeIntegrer.Rows(k).Cells("Fichier").Value))
                End If
                ProgressBar1.Value = ProgressBar1.Value + 1
            Next k
            If Line_Reglement.Count <> 0 Then
                File_Reglement = File.AppendText(FicReglement)
                Fich = Split(FicReglement, "\")
                File_Reglement.WriteLine(Line_Reglement.Count + 2)
                File_Reglement.WriteLine(Fich(UBound(Fich)))
                Do While Line_Reglement.Count > 0
                    File_Reglement.WriteLine(Line_Reglement.Pop)
                Loop
                File_Reglement.Close()
                If Directory.Exists(ReglementSave) = True Then
                    File.Copy(FicReglement, ReglementSave & "\" & Fich(UBound(Fich)))
                End If
                Line_Reglement.Clear()
            End If
            CloseBaseFree()
        Catch ex As Exception
            File_Rejournal.WriteLine(ex.Message)
            CloseBaseFree()
        End Try
        File_Rejournal.Close()
    End Sub
    Private Function Verification(ByRef BasCpta As BSCPTAApplication3, ByRef Champ1 As String) As Boolean
        Dim ColinfoBoll As Boolean = False
        For Each InfoLib As IBIField In BasCpta.FactoryEcriture.InfoLibreFields
            If Champ1 = InfoLib.Name Then
                ColinfoBoll = True
            Else

            End If
        Next
        Return ColinfoBoll
    End Function
    Private Function ReglementConnected(ByRef NomServer As String, ByRef Catalog As String, ByRef Nom_Utsql As String, ByRef Mot_Psql As String) As Boolean
        Try
            OleReglement = New OleDbConnection("provider=SQLOLEDB;Data Source=" & Trim(NomServer) & ";Initial Catalog=" & Trim(Catalog) & ";UID=" & Trim(Nom_Utsql) & ";Pwd=" & Trim(Mot_Psql) & "")
            OleReglement.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub BT_Select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Select.Click
        Dim i As Integer
        For i = 0 To DataListeIntegrer.RowCount - 1
            DataListeIntegrer.Rows(i).Cells("Supprimer").Value = True
        Next i
    End Sub

    Private Sub BT_Deselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Deselect.Click
        Dim i As Integer
        For i = 0 To DataListeIntegrer.RowCount - 1
            DataListeIntegrer.Rows(i).Cells("Supprimer").Value = False
        Next i
    End Sub
End Class