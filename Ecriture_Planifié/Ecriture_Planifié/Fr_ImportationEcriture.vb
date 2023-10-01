Imports Objets100Lib
Imports System
Imports System.Data.OleDb
Imports System.Collections
Imports System.Windows.Forms
Imports System.IO
Imports System.Xml
Imports Microsoft.VisualBasic
Public Class Fr_ImportationEcriture
    Public ProgresMax As Integer
    Public EcriureCpta As IBOEcriture3
    Public EcriureCptAna As IBOEcritureA3
    Public MontantDebit As Double
    Public MontantCredit As Double
    Public Ecritusens As String
    Public EcrMontant As Object
    Public IndexPrec As Integer
    Public NbreLigne As Double
    Public LogMessage As String
    Public NbreTotal As Double
    Public Codejournal As Object
    Public Filebool As Boolean
    Public DecFormat As Integer
    Public Equilibrer As Boolean
    Public Result As Object
    Public Plan As IBPAnalytique3
    Public Pilindex As New Stack
    Public PlanAna As String
    Public PlanAna2 As String
    Public iRow As Integer
    Public jline As Integer
    Public Message As String
    Public NomFichier As String
    Public sColumnsSepar As String
    Public BasePrecedente As String
    Public BaseComptable As String

    Private Sub AfficheSchemasIntegrer()
        Dim i As Integer
        Dim OleAdaptaterschema As OleDbDataAdapter
        Dim OleSchemaDataset As DataSet
        Dim OledatableSchema As DataTable
        DataListeIntegrer.Rows.Clear()
        iRow = 0
        Try
            OleAdaptaterschema = New OleDbDataAdapter("select * from PARAMECRITURE", OleConnenection)
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
        Dim NomFichier1 As String
        Dim aLines() As String
        Try
            If Directory.Exists(PathDirectory) = True Then
                aLines = Directory.GetFiles(PathDirectory)
                For i = 0 To UBound(aLines)
                    DataListeIntegrer.RowCount = iRow + 1
                    NomFichier1 = Trim(aLines(i))
                    Do While InStr(Trim(NomFichier1), "\") <> 0
                        NomFichier1 = Strings.Right(NomFichier1, Strings.Len(Trim(NomFichier1)) - InStr(Trim(NomFichier1), "\"))
                    Loop
                    DataListeIntegrer.Rows(iRow).Cells("FichierExport").Value = NomFichier1
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
    Private Sub AnalyseLectureDuFichier(ByVal sPathFilexporter As String, ByVal spathFileFormat As String, Optional ByRef sColumnsSepar As String = ControlChars.Tab)
        Dim OleAdaptater As OleDbDataAdapter
        Dim OleAfficheDataset As DataSet
        Dim Oledatable As DataTable
        Dim m As Integer
        Dim j As Integer
        Dim jColD As Integer
        Dim iColPosition As Integer
        Dim iColGauchetxt As String
        Dim i As Integer
        Dim iLine As Integer
        Dim aRows() As String
        Dim aCols() As String
        Initialiser()
        iLine = 0
        aRows = Nothing
        Equilibrer = True
        Try
            LectureFormatChampAligner(spathFileFormat)
            aRows = GetArrayFile(sPathFilexporter, aRows)
            ProgressBar1.Value = ProgressBar1.Minimum
            Datagridaffiche.Rows.Clear()
            NbreTotal = DecFormat
            ProgresMax = UBound(aRows) + 1 - DecFormat
            Error_journal.WriteLine("Nombre de Ligne Traité du Fichier : " & ProgresMax)
            Error_journal.WriteLine("")
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
                    Analyse_Process_Integration()
                    m = iLine
                Else
                    If i = UBound(aRows) Then
                        Me.Refresh()
                        Analyse_Process_Integration()
                        m = iLine
                    End If
                End If
            Next i
            Error_journal.WriteLine("")
            OleAdaptater = New OleDbDataAdapter("select * from JOURNAL", OleConnected)
            OleAfficheDataset = New DataSet
            OleAdaptater.Fill(OleAfficheDataset)
            Oledatable = OleAfficheDataset.Tables(0)
            If Oledatable.Rows.Count <> 0 Then
                For j = 0 To Oledatable.Rows.Count - 1
                    If Oledatable.Rows(j).Item("Credit") <> 0 Or Oledatable.Rows(j).Item("Debit") <> 0 Then
                        If (Oledatable.Rows(j).Item("Credit") - Oledatable.Rows(j).Item("Debit")) = 0 Then
                            Error_journal.WriteLine("Societe :" & Oledatable.Rows(j).Item("Societe") & "  Journal N° " & Oledatable.Rows(j).Item("Cojournal") & " -------------: Equilibrer")
                            Error_journal.WriteLine("Nombre de Ligne Traitées :" & Oledatable.Rows(j).Item("Nbreligne"))
                            Error_journal.WriteLine("Cumul Debit Traité       :" & Oledatable.Rows(j).Item("Debit"))
                            Error_journal.WriteLine("Cumul Credit Traité      :" & Oledatable.Rows(j).Item("Credit"))
                            Error_journal.WriteLine("")
                        Else
                            Label5.Refresh()
                            Label5.Text = "Integration Interrompue! Journal Non Equilibrer..."
                            Equilibrer = False
                            Error_journal.WriteLine("Societe :" & Oledatable.Rows(j).Item("Societe") & "  Journal N° " & Oledatable.Rows(j).Item("Cojournal") & " -------------: Non Equilibrer")
                            Error_journal.WriteLine("Nombre de Ligne Traitées :" & Oledatable.Rows(j).Item("Nbreligne"))
                            Error_journal.WriteLine("Cumul Debit Traité       :" & Oledatable.Rows(j).Item("Debit"))
                            Error_journal.WriteLine("Cumul Credit Traité      :" & Oledatable.Rows(j).Item("Credit"))
                            Error_journal.WriteLine("")
                        End If
                    Else
                        Label5.Refresh()
                        Label5.Text = "Integration Interrompue! Montant Inexistant au Debit et Credit..."
                        Equilibrer = False
                        Error_journal.WriteLine("Societe :" & Oledatable.Rows(j).Item("Societe") & "  Journal N° " & Oledatable.Rows(j).Item("Cojournal") & " -------------: Montant Null au Debit et au Credit")
                        Error_journal.WriteLine("Nombre de Ligne Traitées :" & Oledatable.Rows(j).Item("Nbreligne"))
                        Error_journal.WriteLine("Cumul Debit Traité       :" & Oledatable.Rows(j).Item("Debit"))
                        Error_journal.WriteLine("Cumul Credit Traité      :" & Oledatable.Rows(j).Item("Credit"))
                        Error_journal.WriteLine("")

                    End If
                Next j
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Initialiser()
        ProgresMax = 0
        MontantCredit = 0
        MontantDebit = 0
        NbreLigne = 0
        Label8.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub
    Private Sub Lancement_Du_Fichier(ByVal sPathFilexporter As String, ByVal spathFileFormat As String, Optional ByRef sColumnsSepar As String = ControlChars.Tab)
        Dim m As Integer
        Dim jColD As Integer
        Dim iLine As Integer
        Dim aRows() As String
        Dim iColPosition As Integer
        Dim iColGauchetxt As String
        Dim i As Integer, aCols() As String
        Initialiser()
        iLine = 0
        aRows = Nothing
        Try
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
    Private Sub Lecturelapercu_Du_Fichier(ByVal sPathFilexporter As String, ByVal spathFileFormat As String)
        If RechercheFormatype(spathFileFormat) <> "" Then
            If RechercheFormatype(spathFileFormat) = "Tabulation" Then
                sColumnsSepar = ControlChars.Tab
            Else
                If RechercheFormatype(spathFileFormat) = "Point Virgule" Then
                    sColumnsSepar = ";"
                End If
            End If
            Lecture_Suivant_DuFichierExcel(sPathFilexporter, spathFileFormat, sColumnsSepar)
        End If
    End Sub
    Private Sub Lecture_Suivant_DuFichierExcel(ByVal sPathFilexporter As String, ByVal spathFileFormat As String, Optional ByRef sColumnsSepar As String = ControlChars.Tab)
        Dim jColD As Integer
        Dim iColPosition As Integer
        Dim iColGauchetxt As String
        Dim aRows() As String
        Dim i, m As Integer
        Dim iLine As Integer, aCols() As String

        aRows = Nothing
        Try
            LectureFormatChampAligner(spathFileFormat)
            aRows = GetArrayFile(sPathFilexporter, aRows)
            ProgressBar1.Value = ProgressBar1.Minimum
            Datagridaffiche.Rows.Clear()
            ProgresMax = UBound(aRows) + 1 - DecFormat
            m = 0
            iLine = 0
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
    Private Function LireFichierFormat(ByRef ScheminFileFormat As String, ByRef Colname As String) As Object
        Dim NomColonne As String
        Dim NomEntete As String
        Dim PosLeft As Integer
        Dim poslongueur As Integer
        Dim Defaut As String
        Dim ValeurDefaut As String
        Try
            If Trim(ScheminFileFormat) <> "" Then
                If File.Exists(ScheminFileFormat) = True Then
                    Dim FileXml As New XmlTextReader(Trim(ScheminFileFormat))
                    While (FileXml.Read())
                        If FileXml.LocalName = "ColUse" Then
                            NomColonne = FileXml.ReadString

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
    Private Sub Analyse_Process_Integration()
        Dim i As Integer
        Dim j As Integer
        Dim BaseAdaptater As OleDbDataAdapter
        Dim Basedataset As DataSet
        Dim Basedatatable As DataTable
        Me.Cursor = Cursors.WaitCursor
        BT_integrer.Enabled = False
        If Datagridaffiche.RowCount >= 0 Then
            ProgressBar1.Maximum = ProgresMax
            For i = 0 To Datagridaffiche.RowCount - 1
                NbreTotal = NbreTotal + 1
                Label5.Refresh()
                Label8.Refresh()
                TextBox1.Refresh()
                TextBox2.Refresh()
                TextBox3.Refresh()
                Try
                    If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) <> "" Then
                        If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) <> BasePrecedente Then
                            BaseAdaptater = New OleDbDataAdapter("select * from SOCIETE Where SOCGPS='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) & "'", OleConnenection)
                            Basedataset = New DataSet
                            BaseAdaptater.Fill(Basedataset)
                            Basedatatable = Basedataset.Tables(0)
                            If Basedatatable.Rows.Count <> 0 Then
                                BaseComptable = Basedatatable.Rows(0).Item("SOCSAGE")
                                PlanAna = Basedatatable.Rows(0).Item("PlanAna")
                                PlanAna2 = Basedatatable.Rows(0).Item("PlanAna2")
                                CloseBaseFree()
                                BasePrecedente = Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value)
                                If OpenBaseCpta(BaseCpta, Trim(Basedatatable.Rows(0).Item("FICHIERCPTA")), Trim(Basedatatable.Rows(0).Item("UTILISATEUR")), Trim(Basedatatable.Rows(0).Item("MOTPASSE"))) = True Then
                                    EcriureCpta = BaseCpta.FactoryEcriture.Create
                                    With EcriureCpta
                                        For j = 0 To Datagridaffiche.ColumnCount - 1
                                            If Datagridaffiche.Columns(j).Name = "Section" Then
                                                Plan = Nothing
                                                If BaseCpta.FactoryAnalytique.ExistIntitule("" & Trim(PlanAna) & "") = True Then
                                                    Plan = BaseCpta.FactoryAnalytique.ReadIntitule("" & Trim(PlanAna) & "")
                                                    If BaseCpta.FactoryCompteA.ExistNumero(Plan, Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                    Else
                                                        Error_journal.WriteLine("La Section Analytique1--" & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & "  n'existe pas pour le plan 1")
                                                        Dim PieceInsertion As String
                                                        Dim PieceCommand As OleDbCommand
                                                        Dim PieceAdaptater As OleDbDataAdapter
                                                        Dim Piecedataset As DataSet
                                                        Dim Piecedatatable As DataTable
                                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                        Piecedataset = New DataSet
                                                        PieceAdaptater.Fill(Piecedataset)
                                                        Piecedatatable = Piecedataset.Tables(0)
                                                        If Piecedatatable.Rows.Count = 0 Then
                                                            PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                            PieceCommand = New OleDbCommand(PieceInsertion)
                                                            PieceCommand.Connection = OleConnected
                                                            PieceCommand.ExecuteNonQuery()
                                                        End If
                                                    End If

                                                Else
                                                    Error_journal.WriteLine("Le Plan Analytique--" & Trim(PlanAna) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas")
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            End If

                                            If Datagridaffiche.Columns(j).Name = "Section2" Then
                                                Plan = Nothing
                                                If BaseCpta.FactoryAnalytique.ExistIntitule("" & Trim(PlanAna2) & "") = True Then
                                                    Plan = BaseCpta.FactoryAnalytique.ReadIntitule("" & Trim(PlanAna2) & "")
                                                    If BaseCpta.FactoryCompteA.ExistNumero(Plan, Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                    Else
                                                        Error_journal.WriteLine("La Section Analytique2--" & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas pour le Plan 2")
                                                        Dim PieceInsertion As String
                                                        Dim PieceCommand As OleDbCommand
                                                        Dim PieceAdaptater As OleDbDataAdapter
                                                        Dim Piecedataset As DataSet
                                                        Dim Piecedatatable As DataTable
                                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                        Piecedataset = New DataSet
                                                        PieceAdaptater.Fill(Piecedataset)
                                                        Piecedatatable = Piecedataset.Tables(0)
                                                        If Piecedatatable.Rows.Count = 0 Then
                                                            PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                            PieceCommand = New OleDbCommand(PieceInsertion)
                                                            PieceCommand.Connection = OleConnected
                                                            PieceCommand.ExecuteNonQuery()
                                                        End If
                                                    End If
                                                Else
                                                    Error_journal.WriteLine("Le Plan Analytique--" & Trim(PlanAna2) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas")
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            End If

                                            If Datagridaffiche.Columns(j).Name = "CompteTiers" Then
                                                If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                Else
                                                    Error_journal.WriteLine("N° Compte Tiers--" & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas")
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            End If

                                            If Datagridaffiche.Columns(j).Name = "Montant" Then
                                                If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                    EcrMontant = CDbl(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "."), ",")) / 100
                                                End If
                                            End If

                                            If Datagridaffiche.Columns(j).Name = "CodeJournal" Then
                                                Codejournal = Trim(Datagridaffiche.Item(j, i).Value)
                                                If BaseCpta.FactoryJournal.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                    Dim Insertjournal As String
                                                    Dim OleCommandInsert As OleDbCommand
                                                    Dim OleAdaptaterJournal As OleDbDataAdapter
                                                    Dim OleJournalDataset As DataSet
                                                    Dim OledatableJournal As DataTable
                                                    OleAdaptaterJournal = New OleDbDataAdapter("select * from JOURNAL  where Cojournal='" & Trim(Datagridaffiche.Item(j, i).Value) & "' and Societe='" & Trim(BaseComptable) & "'", OleConnected)
                                                    OleJournalDataset = New DataSet
                                                    OleAdaptaterJournal.Fill(OleJournalDataset)
                                                    OledatableJournal = OleJournalDataset.Tables(0)
                                                    If OledatableJournal.Rows.Count = 0 Then
                                                        Insertjournal = "Insert Into JOURNAL (Cojournal,Societe) VALUES ('" & Trim(Datagridaffiche.Item(j, i).Value) & "','" & Trim(BaseComptable) & "')"
                                                        OleCommandInsert = New OleDbCommand(Insertjournal)
                                                        OleCommandInsert.Connection = OleConnected
                                                        OleCommandInsert.ExecuteNonQuery()
                                                        jline = 0
                                                    End If
                                                Else
                                                    Error_journal.WriteLine("Le Code Journal " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas")
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            End If
                                            If Datagridaffiche.Columns(j).Name = "Sens" Then
                                                If Trim(Datagridaffiche.Item(j, i).Value) = "C" Then
                                                    Ecritusens = "C"
                                                Else
                                                    Ecritusens = "D"
                                                End If
                                            End If
                                            If Datagridaffiche.Columns(j).Name = "DatePiece" Then
                                                If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                    If IsDate(Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 1, 4)) = True Then
                                                    Else
                                                        Error_journal.WriteLine("La Chaine n'a pas pu etre Convertie en date Mouvement  " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & " Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal)
                                                        Dim PieceInsertion As String
                                                        Dim PieceCommand As OleDbCommand
                                                        Dim PieceAdaptater As OleDbDataAdapter
                                                        Dim Piecedataset As DataSet
                                                        Dim Piecedatatable As DataTable
                                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                        Piecedataset = New DataSet
                                                        PieceAdaptater.Fill(Piecedataset)
                                                        Piecedatatable = Piecedataset.Tables(0)
                                                        If Piecedatatable.Rows.Count = 0 Then
                                                            PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                            PieceCommand = New OleDbCommand(PieceInsertion)
                                                            PieceCommand.Connection = OleConnected
                                                            PieceCommand.ExecuteNonQuery()
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If Datagridaffiche.Columns(j).Name = "DateEcheance" Then
                                                If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                    If IsDate(Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 1, 4)) = True Then
                                                    Else
                                                        Error_journal.WriteLine("La Chaine n'a pas pu etre Convertie en date Echeance  " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & " Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal)
                                                        Dim PieceInsertion As String
                                                        Dim PieceCommand As OleDbCommand
                                                        Dim PieceAdaptater As OleDbDataAdapter
                                                        Dim Piecedataset As DataSet
                                                        Dim Piecedatatable As DataTable
                                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                        Piecedataset = New DataSet
                                                        PieceAdaptater.Fill(Piecedataset)
                                                        Piecedatatable = Piecedataset.Tables(0)
                                                        If Piecedatatable.Rows.Count = 0 Then
                                                            PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                            PieceCommand = New OleDbCommand(PieceInsertion)
                                                            PieceCommand.Connection = OleConnected
                                                            PieceCommand.ExecuteNonQuery()
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If Datagridaffiche.Columns(j).Name = "CompteGeneral" Then
                                                Dim BaseIntitule As DataTable
                                                Dim OleIntituleAdapter As OleDbDataAdapter
                                                Dim OleIntituleDataset As DataSet
                                                OleIntituleAdapter = New OleDbDataAdapter("select * from FOURNISSEUR Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                OleIntituleDataset = New DataSet
                                                OleIntituleAdapter.Fill(OleIntituleDataset)
                                                BaseIntitule = OleIntituleDataset.Tables(0)
                                                If BaseIntitule.Rows.Count <> 0 Then
                                                    If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then

                                                        Else
                                                            Error_journal.WriteLine("Le Compte Collectif " & Trim(BaseIntitule.Rows(0).Item("Collectif")) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                            Dim PieceInsertion As String
                                                            Dim PieceCommand As OleDbCommand
                                                            Dim PieceAdaptater As OleDbDataAdapter
                                                            Dim Piecedataset As DataSet
                                                            Dim Piecedatatable As DataTable
                                                            PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                            Piecedataset = New DataSet
                                                            PieceAdaptater.Fill(Piecedataset)
                                                            Piecedatatable = Piecedataset.Tables(0)
                                                            If Piecedatatable.Rows.Count = 0 Then
                                                                PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                PieceCommand = New OleDbCommand(PieceInsertion)
                                                                PieceCommand.Connection = OleConnected
                                                                PieceCommand.ExecuteNonQuery()
                                                            End If
                                                        End If
                                                    Else
                                                        Error_journal.WriteLine("Le Compte Auxiliaire " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                        Dim PieceInsertion As String
                                                        Dim PieceCommand As OleDbCommand
                                                        Dim PieceAdaptater As OleDbDataAdapter
                                                        Dim Piecedataset As DataSet
                                                        Dim Piecedatatable As DataTable
                                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                        Piecedataset = New DataSet
                                                        PieceAdaptater.Fill(Piecedataset)
                                                        Piecedatatable = Piecedataset.Tables(0)
                                                        If Piecedatatable.Rows.Count = 0 Then
                                                            PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                            PieceCommand = New OleDbCommand(PieceInsertion)
                                                            PieceCommand.Connection = OleConnected
                                                            PieceCommand.ExecuteNonQuery()
                                                        End If
                                                    End If
                                                Else
                                                    OleIntituleAdapter = New OleDbDataAdapter("select * from FOURNISSEURS Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                    OleIntituleDataset = New DataSet
                                                    OleIntituleAdapter.Fill(OleIntituleDataset)
                                                    BaseIntitule = OleIntituleDataset.Tables(0)
                                                    If BaseIntitule.Rows.Count <> 0 Then
                                                        If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                            If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                            Else
                                                                Error_journal.WriteLine("Le Compte Collectif " & Trim(BaseIntitule.Rows(0).Item("Collectif")) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                Dim PieceInsertion As String
                                                                Dim PieceCommand As OleDbCommand
                                                                Dim PieceAdaptater As OleDbDataAdapter
                                                                Dim Piecedataset As DataSet
                                                                Dim Piecedatatable As DataTable
                                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                Piecedataset = New DataSet
                                                                PieceAdaptater.Fill(Piecedataset)
                                                                Piecedatatable = Piecedataset.Tables(0)
                                                                If Piecedatatable.Rows.Count = 0 Then
                                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                                    PieceCommand.Connection = OleConnected
                                                                    PieceCommand.ExecuteNonQuery()
                                                                End If
                                                            End If

                                                        Else
                                                            Error_journal.WriteLine("Le Compte Auxiliaire " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                            Dim PieceInsertion As String
                                                            Dim PieceCommand As OleDbCommand
                                                            Dim PieceAdaptater As OleDbDataAdapter
                                                            Dim Piecedataset As DataSet
                                                            Dim Piecedatatable As DataTable
                                                            PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                            Piecedataset = New DataSet
                                                            PieceAdaptater.Fill(Piecedataset)
                                                            Piecedatatable = Piecedataset.Tables(0)
                                                            If Piecedatatable.Rows.Count = 0 Then
                                                                PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                PieceCommand = New OleDbCommand(PieceInsertion)
                                                                PieceCommand.Connection = OleConnected
                                                                PieceCommand.ExecuteNonQuery()
                                                            End If
                                                        End If
                                                    Else
                                                        OleIntituleAdapter = New OleDbDataAdapter("select * from CLIENT Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                        OleIntituleDataset = New DataSet
                                                        OleIntituleAdapter.Fill(OleIntituleDataset)
                                                        BaseIntitule = OleIntituleDataset.Tables(0)
                                                        If BaseIntitule.Rows.Count <> 0 Then
                                                            If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                Else
                                                                    Error_journal.WriteLine("Le Compte Collectif " & Trim(BaseIntitule.Rows(0).Item("Collectif")) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                    Dim PieceInsertion As String
                                                                    Dim PieceCommand As OleDbCommand
                                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                                    Dim Piecedataset As DataSet
                                                                    Dim Piecedatatable As DataTable
                                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                    Piecedataset = New DataSet
                                                                    PieceAdaptater.Fill(Piecedataset)
                                                                    Piecedatatable = Piecedataset.Tables(0)
                                                                    If Piecedatatable.Rows.Count = 0 Then
                                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                                        PieceCommand.Connection = OleConnected
                                                                        PieceCommand.ExecuteNonQuery()
                                                                    End If
                                                                End If

                                                            Else
                                                                Error_journal.WriteLine("Le Compte Auxiliaire " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                Dim PieceInsertion As String
                                                                Dim PieceCommand As OleDbCommand
                                                                Dim PieceAdaptater As OleDbDataAdapter
                                                                Dim Piecedataset As DataSet
                                                                Dim Piecedatatable As DataTable
                                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                Piecedataset = New DataSet
                                                                PieceAdaptater.Fill(Piecedataset)
                                                                Piecedatatable = Piecedataset.Tables(0)
                                                                If Piecedatatable.Rows.Count = 0 Then
                                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                                    PieceCommand.Connection = OleConnected
                                                                    PieceCommand.ExecuteNonQuery()
                                                                End If
                                                            End If
                                                        Else
                                                            OleIntituleAdapter = New OleDbDataAdapter("select * from CLIENTS Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                            OleIntituleDataset = New DataSet
                                                            OleIntituleAdapter.Fill(OleIntituleDataset)
                                                            BaseIntitule = OleIntituleDataset.Tables(0)
                                                            If BaseIntitule.Rows.Count <> 0 Then
                                                                If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                    If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then

                                                                    Else
                                                                        Error_journal.WriteLine("Le Compte Collectif " & Trim(BaseIntitule.Rows(0).Item("Collectif")) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                        Dim PieceInsertion As String
                                                                        Dim PieceCommand As OleDbCommand
                                                                        Dim PieceAdaptater As OleDbDataAdapter
                                                                        Dim Piecedataset As DataSet
                                                                        Dim Piecedatatable As DataTable
                                                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                        Piecedataset = New DataSet
                                                                        PieceAdaptater.Fill(Piecedataset)
                                                                        Piecedatatable = Piecedataset.Tables(0)
                                                                        If Piecedatatable.Rows.Count = 0 Then
                                                                            PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                            PieceCommand = New OleDbCommand(PieceInsertion)
                                                                            PieceCommand.Connection = OleConnected
                                                                            PieceCommand.ExecuteNonQuery()
                                                                        End If
                                                                    End If

                                                                Else
                                                                    Error_journal.WriteLine("Le Compte Auxiliaire " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                    Dim PieceInsertion As String
                                                                    Dim PieceCommand As OleDbCommand
                                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                                    Dim Piecedataset As DataSet
                                                                    Dim Piecedatatable As DataTable
                                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                    Piecedataset = New DataSet
                                                                    PieceAdaptater.Fill(Piecedataset)
                                                                    Piecedatatable = Piecedataset.Tables(0)
                                                                    If Piecedatatable.Rows.Count = 0 Then
                                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                                        PieceCommand.Connection = OleConnected
                                                                        PieceCommand.ExecuteNonQuery()
                                                                    End If
                                                                End If
                                                            Else
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then

                                                                Else
                                                                    Error_journal.WriteLine("Le Compte Numero : " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                    Dim PieceInsertion As String
                                                                    Dim PieceCommand As OleDbCommand
                                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                                    Dim Piecedataset As DataSet
                                                                    Dim Piecedatatable As DataTable
                                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                    Piecedataset = New DataSet
                                                                    PieceAdaptater.Fill(Piecedataset)
                                                                    Piecedatatable = Piecedataset.Tables(0)
                                                                    If Piecedatatable.Rows.Count = 0 Then
                                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                                        PieceCommand.Connection = OleConnected
                                                                        PieceCommand.ExecuteNonQuery()
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                Dim ColinfoBoll As Boolean = False
                                                For Each InfoLib As IBIField In BaseCpta.FactoryEcriture.InfoLibreFields
                                                    If Datagridaffiche.Columns(j).Name = InfoLib.Name Then
                                                        ColinfoBoll = True
                                                    Else

                                                    End If
                                                Next
                                                If ColinfoBoll = False Then
                                                    Error_journal.WriteLine("Le Champs d'info libre Existe dans le Format Mais Pas dans Sage..." & Datagridaffiche.Columns(j).Name & "  Line: " & NbreTotal)
                                                    Error_journal.WriteLine("")
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            End If

                                        Next j
                                        Dim OleAdaptaterDelete As OleDbDataAdapter
                                        Dim OleDeleteDataset As DataSet
                                        Dim OledatableDelete As DataTable
                                        Dim OleCommandDelete As OleDbCommand
                                        OleAdaptaterDelete = New OleDbDataAdapter("select * from JOURNAL  where Cojournal='" & Trim(Codejournal) & "' and Societe='" & Trim(BaseComptable) & "'", OleConnected)
                                        OleDeleteDataset = New DataSet
                                        OleAdaptaterDelete.Fill(OleDeleteDataset)
                                        OledatableDelete = OleDeleteDataset.Tables(0)
                                        If OledatableDelete.Rows.Count <> 0 Then
                                            jline = OledatableDelete.Rows(0).Item("Nbreligne") + 1
                                            Dim UpdatMont As String
                                            Dim Montant As Double
                                            If Ecritusens = "C" Then
                                                Montant = Math.Round(OledatableDelete.Rows(0).Item("Credit") + EcrMontant, 2)
                                                MontantCredit = Math.Round(MontantCredit + EcrMontant, 2)
                                                UpdatMont = "Update JOURNAL set Credit='" & Montant & "',Nbreligne='" & jline & "' where Cojournal='" & Trim(Codejournal) & "' and Societe='" & Trim(BaseComptable) & "'"
                                                OleCommandDelete = New OleDbCommand(UpdatMont)
                                                OleCommandDelete.Connection = OleConnected
                                                OleCommandDelete.ExecuteNonQuery()
                                            Else
                                                Montant = Math.Round(OledatableDelete.Rows(0).Item("Debit") + EcrMontant, 2)
                                                UpdatMont = "Update JOURNAL set Debit='" & Montant & "',Nbreligne='" & jline & "' where Cojournal='" & Trim(Codejournal) & "' and Societe='" & Trim(BaseComptable) & "'"
                                                OleCommandDelete = New OleDbCommand(UpdatMont)
                                                OleCommandDelete.Connection = OleConnected
                                                OleCommandDelete.ExecuteNonQuery()
                                                MontantDebit = Math.Round(MontantDebit + EcrMontant, 2)
                                            End If
                                        End If
                                    End With
                                Else
                                    Error_journal.WriteLine("Echec de Connexion à la base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal)
                                End If
                            Else
                                'Error_journal.WriteLine("La Base GPS  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & " ne Correspond à aucune Base Sage")
                            End If
                        Else
                            If BaseCpta.IsOpen = True Then
                                EcriureCpta = BaseCpta.FactoryEcriture.Create
                                With EcriureCpta
                                    For j = 0 To Datagridaffiche.ColumnCount - 1
                                        If Datagridaffiche.Columns(j).Name = "Section" Then
                                            Plan = Nothing
                                            If BaseCpta.FactoryAnalytique.ExistIntitule("" & Trim(PlanAna) & "") = True Then
                                                Plan = BaseCpta.FactoryAnalytique.ReadIntitule("" & Trim(PlanAna) & "")
                                                If BaseCpta.FactoryCompteA.ExistNumero(Plan, Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                Else
                                                    Error_journal.WriteLine("La Section Analytique1--" & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas pour le plan 1")
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If

                                            Else
                                                Error_journal.WriteLine("Le Plan Analytique--" & Trim(PlanAna) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas")
                                                Dim PieceInsertion As String
                                                Dim PieceCommand As OleDbCommand
                                                Dim PieceAdaptater As OleDbDataAdapter
                                                Dim Piecedataset As DataSet
                                                Dim Piecedatatable As DataTable
                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                Piecedataset = New DataSet
                                                PieceAdaptater.Fill(Piecedataset)
                                                Piecedatatable = Piecedataset.Tables(0)
                                                If Piecedatatable.Rows.Count = 0 Then
                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                    PieceCommand.Connection = OleConnected
                                                    PieceCommand.ExecuteNonQuery()
                                                End If
                                            End If
                                        End If

                                        If Datagridaffiche.Columns(j).Name = "Section2" Then
                                            Plan = Nothing
                                            If BaseCpta.FactoryAnalytique.ExistIntitule("" & Trim(PlanAna2) & "") = True Then
                                                Plan = BaseCpta.FactoryAnalytique.ReadIntitule("" & Trim(PlanAna2) & "")
                                                If BaseCpta.FactoryCompteA.ExistNumero(Plan, Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                Else
                                                    Error_journal.WriteLine("La Section Analytique2--" & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas pour le Plan 2")
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            Else
                                                Error_journal.WriteLine("Le Plan Analytique--" & Trim(PlanAna2) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas")
                                                Dim PieceInsertion As String
                                                Dim PieceCommand As OleDbCommand
                                                Dim PieceAdaptater As OleDbDataAdapter
                                                Dim Piecedataset As DataSet
                                                Dim Piecedatatable As DataTable
                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                Piecedataset = New DataSet
                                                PieceAdaptater.Fill(Piecedataset)
                                                Piecedatatable = Piecedataset.Tables(0)
                                                If Piecedatatable.Rows.Count = 0 Then
                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                    PieceCommand.Connection = OleConnected
                                                    PieceCommand.ExecuteNonQuery()
                                                End If
                                            End If
                                        End If

                                        If Datagridaffiche.Columns(j).Name = "CompteTiers" Then
                                            If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                            Else
                                                Error_journal.WriteLine("N° Compte Tiers--" & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas")
                                                Dim PieceInsertion As String
                                                Dim PieceCommand As OleDbCommand
                                                Dim PieceAdaptater As OleDbDataAdapter
                                                Dim Piecedataset As DataSet
                                                Dim Piecedatatable As DataTable
                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                Piecedataset = New DataSet
                                                PieceAdaptater.Fill(Piecedataset)
                                                Piecedatatable = Piecedataset.Tables(0)
                                                If Piecedatatable.Rows.Count = 0 Then
                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                    PieceCommand.Connection = OleConnected
                                                    PieceCommand.ExecuteNonQuery()
                                                End If
                                            End If
                                        End If

                                        If Datagridaffiche.Columns(j).Name = "Montant" Then
                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                EcrMontant = CDbl(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "."), ",")) / 100
                                            End If
                                        End If

                                        If Datagridaffiche.Columns(j).Name = "CodeJournal" Then
                                            Codejournal = Trim(Datagridaffiche.Item(j, i).Value)
                                            If BaseCpta.FactoryJournal.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                Dim Insertjournal As String
                                                Dim OleCommandInsert As OleDbCommand
                                                Dim OleAdaptaterJournal As OleDbDataAdapter
                                                Dim OleJournalDataset As DataSet
                                                Dim OledatableJournal As DataTable
                                                OleAdaptaterJournal = New OleDbDataAdapter("select * from JOURNAL  where Cojournal='" & Trim(Datagridaffiche.Item(j, i).Value) & "' and Societe='" & Trim(BaseComptable) & "'", OleConnected)
                                                OleJournalDataset = New DataSet
                                                OleAdaptaterJournal.Fill(OleJournalDataset)
                                                OledatableJournal = OleJournalDataset.Tables(0)
                                                If OledatableJournal.Rows.Count = 0 Then
                                                    Insertjournal = "Insert Into JOURNAL (Cojournal,Societe) VALUES ('" & Trim(Datagridaffiche.Item(j, i).Value) & "','" & Trim(BaseComptable) & "')"
                                                    OleCommandInsert = New OleDbCommand(Insertjournal)
                                                    OleCommandInsert.Connection = OleConnected
                                                    OleCommandInsert.ExecuteNonQuery()
                                                    jline = 0
                                                End If
                                            Else
                                                Error_journal.WriteLine("Le Code Journal " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas")
                                                Dim PieceInsertion As String
                                                Dim PieceCommand As OleDbCommand
                                                Dim PieceAdaptater As OleDbDataAdapter
                                                Dim Piecedataset As DataSet
                                                Dim Piecedatatable As DataTable
                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                Piecedataset = New DataSet
                                                PieceAdaptater.Fill(Piecedataset)
                                                Piecedatatable = Piecedataset.Tables(0)
                                                If Piecedatatable.Rows.Count = 0 Then
                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                    PieceCommand.Connection = OleConnected
                                                    PieceCommand.ExecuteNonQuery()
                                                End If
                                            End If
                                        End If
                                        If Datagridaffiche.Columns(j).Name = "Sens" Then
                                            If Trim(Datagridaffiche.Item(j, i).Value) = "C" Then
                                                Ecritusens = "C"
                                            Else
                                                Ecritusens = "D"
                                            End If
                                        End If
                                        If Datagridaffiche.Columns(j).Name = "DatePiece" Then
                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                If IsDate(Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 1, 4)) = True Then
                                                Else
                                                    Error_journal.WriteLine("La Chaine n'a pas pu etre Convertie en date Mouvement " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & " Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal)
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            End If
                                        End If
                                        If Datagridaffiche.Columns(j).Name = "DateEcheance" Then
                                            If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                If IsDate(Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 1, 4)) = True Then
                                                Else
                                                    Error_journal.WriteLine("La Chaine n'a pas pu etre Convertie en date Echeance " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & " Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal)
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            End If
                                        End If
                                        If Datagridaffiche.Columns(j).Name = "CompteGeneral" Then
                                            Dim BaseIntitule As DataTable
                                            Dim OleIntituleAdapter As OleDbDataAdapter
                                            Dim OleIntituleDataset As DataSet
                                            OleIntituleAdapter = New OleDbDataAdapter("select * from FOURNISSEUR Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                            OleIntituleDataset = New DataSet
                                            OleIntituleAdapter.Fill(OleIntituleDataset)
                                            BaseIntitule = OleIntituleDataset.Tables(0)
                                            If BaseIntitule.Rows.Count <> 0 Then
                                                If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                    If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then

                                                    Else
                                                        Error_journal.WriteLine("Le Compte Collectif " & Trim(BaseIntitule.Rows(0).Item("Collectif")) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                        Dim PieceInsertion As String
                                                        Dim PieceCommand As OleDbCommand
                                                        Dim PieceAdaptater As OleDbDataAdapter
                                                        Dim Piecedataset As DataSet
                                                        Dim Piecedatatable As DataTable
                                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                        Piecedataset = New DataSet
                                                        PieceAdaptater.Fill(Piecedataset)
                                                        Piecedatatable = Piecedataset.Tables(0)
                                                        If Piecedatatable.Rows.Count = 0 Then
                                                            PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                            PieceCommand = New OleDbCommand(PieceInsertion)
                                                            PieceCommand.Connection = OleConnected
                                                            PieceCommand.ExecuteNonQuery()
                                                        End If
                                                    End If
                                                Else
                                                    Error_journal.WriteLine("Le Compte Auxiliaire " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                    Dim PieceInsertion As String
                                                    Dim PieceCommand As OleDbCommand
                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                    Dim Piecedataset As DataSet
                                                    Dim Piecedatatable As DataTable
                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                    Piecedataset = New DataSet
                                                    PieceAdaptater.Fill(Piecedataset)
                                                    Piecedatatable = Piecedataset.Tables(0)
                                                    If Piecedatatable.Rows.Count = 0 Then
                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                        PieceCommand.Connection = OleConnected
                                                        PieceCommand.ExecuteNonQuery()
                                                    End If
                                                End If
                                            Else
                                                OleIntituleAdapter = New OleDbDataAdapter("select * from FOURNISSEURS Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                OleIntituleDataset = New DataSet
                                                OleIntituleAdapter.Fill(OleIntituleDataset)
                                                BaseIntitule = OleIntituleDataset.Tables(0)
                                                If BaseIntitule.Rows.Count <> 0 Then
                                                    If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                        Else
                                                            Error_journal.WriteLine("Le Compte Collectif " & Trim(BaseIntitule.Rows(0).Item("Collectif")) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                            Dim PieceInsertion As String
                                                            Dim PieceCommand As OleDbCommand
                                                            Dim PieceAdaptater As OleDbDataAdapter
                                                            Dim Piecedataset As DataSet
                                                            Dim Piecedatatable As DataTable
                                                            PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                            Piecedataset = New DataSet
                                                            PieceAdaptater.Fill(Piecedataset)
                                                            Piecedatatable = Piecedataset.Tables(0)
                                                            If Piecedatatable.Rows.Count = 0 Then
                                                                PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                PieceCommand = New OleDbCommand(PieceInsertion)
                                                                PieceCommand.Connection = OleConnected
                                                                PieceCommand.ExecuteNonQuery()
                                                            End If
                                                        End If

                                                    Else
                                                        Error_journal.WriteLine("Le Compte Auxiliaire " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                        Dim PieceInsertion As String
                                                        Dim PieceCommand As OleDbCommand
                                                        Dim PieceAdaptater As OleDbDataAdapter
                                                        Dim Piecedataset As DataSet
                                                        Dim Piecedatatable As DataTable
                                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                        Piecedataset = New DataSet
                                                        PieceAdaptater.Fill(Piecedataset)
                                                        Piecedatatable = Piecedataset.Tables(0)
                                                        If Piecedatatable.Rows.Count = 0 Then
                                                            PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                            PieceCommand = New OleDbCommand(PieceInsertion)
                                                            PieceCommand.Connection = OleConnected
                                                            PieceCommand.ExecuteNonQuery()
                                                        End If
                                                    End If
                                                Else
                                                    OleIntituleAdapter = New OleDbDataAdapter("select * from CLIENT Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                    OleIntituleDataset = New DataSet
                                                    OleIntituleAdapter.Fill(OleIntituleDataset)
                                                    BaseIntitule = OleIntituleDataset.Tables(0)
                                                    If BaseIntitule.Rows.Count <> 0 Then
                                                        If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                            If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                            Else
                                                                Error_journal.WriteLine("Le Compte Collectif " & Trim(BaseIntitule.Rows(0).Item("Collectif")) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                Dim PieceInsertion As String
                                                                Dim PieceCommand As OleDbCommand
                                                                Dim PieceAdaptater As OleDbDataAdapter
                                                                Dim Piecedataset As DataSet
                                                                Dim Piecedatatable As DataTable
                                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                Piecedataset = New DataSet
                                                                PieceAdaptater.Fill(Piecedataset)
                                                                Piecedatatable = Piecedataset.Tables(0)
                                                                If Piecedatatable.Rows.Count = 0 Then
                                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                                    PieceCommand.Connection = OleConnected
                                                                    PieceCommand.ExecuteNonQuery()
                                                                End If
                                                            End If

                                                        Else
                                                            Error_journal.WriteLine("Le Compte Auxiliaire " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                            Dim PieceInsertion As String
                                                            Dim PieceCommand As OleDbCommand
                                                            Dim PieceAdaptater As OleDbDataAdapter
                                                            Dim Piecedataset As DataSet
                                                            Dim Piecedatatable As DataTable
                                                            PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                            Piecedataset = New DataSet
                                                            PieceAdaptater.Fill(Piecedataset)
                                                            Piecedatatable = Piecedataset.Tables(0)
                                                            If Piecedatatable.Rows.Count = 0 Then
                                                                PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                PieceCommand = New OleDbCommand(PieceInsertion)
                                                                PieceCommand.Connection = OleConnected
                                                                PieceCommand.ExecuteNonQuery()
                                                            End If
                                                        End If
                                                    Else
                                                        OleIntituleAdapter = New OleDbDataAdapter("select * from CLIENTS Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                        OleIntituleDataset = New DataSet
                                                        OleIntituleAdapter.Fill(OleIntituleDataset)
                                                        BaseIntitule = OleIntituleDataset.Tables(0)
                                                        If BaseIntitule.Rows.Count <> 0 Then
                                                            If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then

                                                                Else
                                                                    Error_journal.WriteLine("Le Compte Collectif " & Trim(BaseIntitule.Rows(0).Item("Collectif")) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                    Dim PieceInsertion As String
                                                                    Dim PieceCommand As OleDbCommand
                                                                    Dim PieceAdaptater As OleDbDataAdapter
                                                                    Dim Piecedataset As DataSet
                                                                    Dim Piecedatatable As DataTable
                                                                    PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                    Piecedataset = New DataSet
                                                                    PieceAdaptater.Fill(Piecedataset)
                                                                    Piecedatatable = Piecedataset.Tables(0)
                                                                    If Piecedatatable.Rows.Count = 0 Then
                                                                        PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                        PieceCommand = New OleDbCommand(PieceInsertion)
                                                                        PieceCommand.Connection = OleConnected
                                                                        PieceCommand.ExecuteNonQuery()
                                                                    End If
                                                                End If

                                                            Else
                                                                Error_journal.WriteLine("Le Compte Auxiliaire " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                Dim PieceInsertion As String
                                                                Dim PieceCommand As OleDbCommand
                                                                Dim PieceAdaptater As OleDbDataAdapter
                                                                Dim Piecedataset As DataSet
                                                                Dim Piecedatatable As DataTable
                                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                Piecedataset = New DataSet
                                                                PieceAdaptater.Fill(Piecedataset)
                                                                Piecedatatable = Piecedataset.Tables(0)
                                                                If Piecedatatable.Rows.Count = 0 Then
                                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                                    PieceCommand.Connection = OleConnected
                                                                    PieceCommand.ExecuteNonQuery()
                                                                End If
                                                            End If
                                                        Else
                                                            If BaseCpta.FactoryCompteG.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then

                                                            Else
                                                                Error_journal.WriteLine("Le Compte Numero : " & Trim(Datagridaffiche.Item(j, i).Value) & "  Base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal & " n'existe pas dans Sage")
                                                                Dim PieceInsertion As String
                                                                Dim PieceCommand As OleDbCommand
                                                                Dim PieceAdaptater As OleDbDataAdapter
                                                                Dim Piecedataset As DataSet
                                                                Dim Piecedatatable As DataTable
                                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                                Piecedataset = New DataSet
                                                                PieceAdaptater.Fill(Piecedataset)
                                                                Piecedatatable = Piecedataset.Tables(0)
                                                                If Piecedatatable.Rows.Count = 0 Then
                                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                                    PieceCommand.Connection = OleConnected
                                                                    PieceCommand.ExecuteNonQuery()
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                        If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                            Dim ColinfoBoll As Boolean = False
                                            For Each InfoLib As IBIField In BaseCpta.FactoryEcriture.InfoLibreFields
                                                If Datagridaffiche.Columns(j).Name = InfoLib.Name Then
                                                    ColinfoBoll = True
                                                Else

                                                End If
                                            Next
                                            If ColinfoBoll = False Then
                                                Error_journal.WriteLine("Le Champs d'info libre Existe dans le Format Mais Pas dans Sage..." & Datagridaffiche.Columns(j).Name & "  Line: " & NbreTotal)
                                                Error_journal.WriteLine("")
                                                Dim PieceInsertion As String
                                                Dim PieceCommand As OleDbCommand
                                                Dim PieceAdaptater As OleDbDataAdapter
                                                Dim Piecedataset As DataSet
                                                Dim Piecedatatable As DataTable
                                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                                Piecedataset = New DataSet
                                                PieceAdaptater.Fill(Piecedataset)
                                                Piecedatatable = Piecedataset.Tables(0)
                                                If Piecedatatable.Rows.Count = 0 Then
                                                    PieceInsertion = "Insert Into PIECEANO (Piece) VALUES ('" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "')"
                                                    PieceCommand = New OleDbCommand(PieceInsertion)
                                                    PieceCommand.Connection = OleConnected
                                                    PieceCommand.ExecuteNonQuery()
                                                End If
                                            End If
                                        End If

                                    Next j
                                    Dim OleAdaptaterDelete As OleDbDataAdapter
                                    Dim OleDeleteDataset As DataSet
                                    Dim OledatableDelete As DataTable
                                    Dim OleCommandDelete As OleDbCommand
                                    OleAdaptaterDelete = New OleDbDataAdapter("select * from JOURNAL  where Cojournal='" & Trim(Codejournal) & "' and Societe='" & Trim(BaseComptable) & "'", OleConnected)
                                    OleDeleteDataset = New DataSet
                                    OleAdaptaterDelete.Fill(OleDeleteDataset)
                                    OledatableDelete = OleDeleteDataset.Tables(0)
                                    If OledatableDelete.Rows.Count <> 0 Then
                                        jline = OledatableDelete.Rows(0).Item("Nbreligne") + 1
                                        Dim UpdatMont As String
                                        Dim Montant As Double
                                        If Ecritusens = "C" Then
                                            Montant = Math.Round(OledatableDelete.Rows(0).Item("Credit") + EcrMontant, 2)
                                            MontantCredit = Math.Round(MontantCredit + EcrMontant, 2)
                                            UpdatMont = "Update JOURNAL set Credit='" & Montant & "',Nbreligne='" & jline & "' where Cojournal='" & Trim(Codejournal) & "' and Societe='" & Trim(BaseComptable) & "'"
                                            OleCommandDelete = New OleDbCommand(UpdatMont)
                                            OleCommandDelete.Connection = OleConnected
                                            OleCommandDelete.ExecuteNonQuery()
                                        Else
                                            Montant = Math.Round(OledatableDelete.Rows(0).Item("Debit") + EcrMontant, 2)
                                            UpdatMont = "Update JOURNAL set Debit='" & Montant & "',Nbreligne='" & jline & "' where Cojournal='" & Trim(Codejournal) & "' and Societe='" & Trim(BaseComptable) & "'"
                                            OleCommandDelete = New OleDbCommand(UpdatMont)
                                            OleCommandDelete.Connection = OleConnected
                                            OleCommandDelete.ExecuteNonQuery()
                                            MontantDebit = Math.Round(MontantDebit + EcrMontant, 2)
                                        End If
                                    End If
                                End With
                            Else
                                Error_journal.WriteLine("Echec de Connexion à la base Comptable  " & BaseComptable & "  Journal " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("CodeJournal").ColumnIndex, i).Value) & "  Piece " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "  Line: " & NbreTotal)
                            End If
                            End If
                    End If
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    NbreLigne = NbreLigne + 1
                    Label8.Text = NbreLigne & "/" & ProgresMax
                    Label5.Refresh()
                    Label8.Refresh()
                    TextBox1.Refresh()
                    TextBox2.Refresh()
                    TextBox3.Refresh()
                Catch ex As Exception
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    NbreLigne = NbreLigne + 1
                    Label8.Text = NbreLigne & "/" & ProgresMax
                    Label8.Refresh()
                End Try
            Next i
            TextBox1.Text = MontantDebit
            TextBox2.Text = MontantCredit
            TextBox3.Text = Math.Round(TextBox2.Text - TextBox1.Text, 2)
            Datagridaffiche.Rows.Clear()
        End If
        Me.Cursor = Cursors.Default
        BT_integrer.Enabled = True
    End Sub
    Private Sub Integrer_Ecriture()
        Dim i As Integer
        Dim j As Integer
        Dim BaseAdaptater As OleDbDataAdapter
        Dim Basedataset As DataSet
        Dim Basedatatable As DataTable
        Me.Cursor = Cursors.WaitCursor
        BT_integrer.Enabled = False
        If Datagridaffiche.RowCount >= 0 Then
            ProgressBar1.Maximum = ProgresMax
            For i = 0 To Datagridaffiche.RowCount - 1
                Label5.Refresh()
                Label8.Refresh()
                TextBox1.Refresh()
                TextBox2.Refresh()
                TextBox3.Refresh()
                Label5.Text = "Verification Terminée! Integration En Cours..."
                Try
                    If CDbl(Join(Split(Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Montant").ColumnIndex, i).Value), "."), ",")) <> 0 Then
                        If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) <> "" Then
                            If Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) <> BasePrecedente Then
                                BaseAdaptater = New OleDbDataAdapter("select * from SOCIETE Where SOCGPS='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) & "'", OleConnenection)
                                Basedataset = New DataSet
                                BaseAdaptater.Fill(Basedataset)
                                Basedatatable = Basedataset.Tables(0)
                                If Basedatatable.Rows.Count <> 0 Then
                                    BaseComptable = Basedatatable.Rows(0).Item("SOCSAGE")
                                    PlanAna = Basedatatable.Rows(0).Item("PlanAna")
                                    PlanAna2 = Basedatatable.Rows(0).Item("PlanAna2")
                                    CloseBaseFree()
                                    BasePrecedente = Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value)
                                    If OpenBaseCpta(BaseCpta, Trim(Basedatatable.Rows(0).Item("FICHIERCPTA")), Trim(Basedatatable.Rows(0).Item("UTILISATEUR")), Trim(Basedatatable.Rows(0).Item("MOTPASSE"))) = True Then
                                        Dim PieceAdaptater As OleDbDataAdapter
                                        Dim Piecedataset As DataSet
                                        Dim Piecedatatable As DataTable
                                        PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                        Piecedataset = New DataSet
                                        PieceAdaptater.Fill(Piecedataset)
                                        Piecedatatable = Piecedataset.Tables(0)
                                        If Piecedatatable.Rows.Count = 0 Then
                                            EcriureCpta = BaseCpta.FactoryEcriture.Create
                                            With EcriureCpta
                                                For j = 0 To Datagridaffiche.ColumnCount - 1
                                                    If Datagridaffiche.Columns(j).Name = "Facture" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .EC_RefPiece = Trim(Datagridaffiche.Item(j, i).Value)
                                                        End If
                                                    End If

                                                    If Datagridaffiche.Columns(j).Name = "CompteTiers" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                        End If
                                                    End If
                                                    If Datagridaffiche.Columns(j).Name = "LibelleEcriture" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .EC_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                        End If
                                                    End If
                                                    If Datagridaffiche.Columns(j).Name = "MontantDevise" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .EC_Devise = CDbl(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "."), ","))
                                                        End If
                                                    End If
                                                    If Datagridaffiche.Columns(j).Name = "Montant" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .EC_Montant = CDbl(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "."), ",")) / 100
                                                            EcrMontant = CDbl(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "."), ",")) / 100
                                                        End If
                                                    End If

                                                    If Datagridaffiche.Columns(j).Name = "CodeJournal" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .Journal = BaseCpta.FactoryJournal.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                        End If
                                                    End If
                                                    If Datagridaffiche.Columns(j).Name = "DatePiece" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .Date = CDate(Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 1, 4))
                                                        End If
                                                    End If
                                                    If Datagridaffiche.Columns(j).Name = "Sens" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            If Trim(Datagridaffiche.Item(j, i).Value) = "C" Then
                                                                .EC_Sens = EcritureSensType.EcritureSensTypeCredit
                                                                Ecritusens = "C"
                                                            Else
                                                                .EC_Sens = EcritureSensType.EcritureSensTypeDebit
                                                                Ecritusens = "D"
                                                            End If
                                                        End If
                                                    End If
                                                    If Datagridaffiche.Columns(j).Name = "Piece" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .EC_Piece = Join(Split(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "-"), ""), "/"), "")
                                                        End If
                                                    End If

                                                    If Datagridaffiche.Columns(j).Name = "DateEcheance" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                            .EC_Echeance = CDate(Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 1, 4))
                                                            .Reglement = BaseCpta.FactoryReglement.List.Item(1)
                                                        End If
                                                    End If
                                                    If Datagridaffiche.Columns(j).Name = "CompteGeneral" Then
                                                        Dim BaseIntitule As DataTable
                                                        Dim OleIntituleAdapter As OleDbDataAdapter
                                                        Dim OleIntituleDataset As DataSet
                                                        OleIntituleAdapter = New OleDbDataAdapter("select * from FOURNISSEUR Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                        OleIntituleDataset = New DataSet
                                                        OleIntituleAdapter.Fill(OleIntituleDataset)
                                                        BaseIntitule = OleIntituleDataset.Tables(0)
                                                        If BaseIntitule.Rows.Count <> 0 Then
                                                            If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                    .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                    .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(BaseIntitule.Rows(0).Item("Collectif")))
                                                                End If
                                                            End If
                                                        Else
                                                            OleIntituleAdapter = New OleDbDataAdapter("select * from FOURNISSEURS Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                            OleIntituleDataset = New DataSet
                                                            OleIntituleAdapter.Fill(OleIntituleDataset)
                                                            BaseIntitule = OleIntituleDataset.Tables(0)
                                                            If BaseIntitule.Rows.Count <> 0 Then
                                                                If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                    If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                        .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                        .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(BaseIntitule.Rows(0).Item("Collectif")))
                                                                    End If
                                                                End If
                                                            Else
                                                                OleIntituleAdapter = New OleDbDataAdapter("select * from CLIENT Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                                OleIntituleDataset = New DataSet
                                                                OleIntituleAdapter.Fill(OleIntituleDataset)
                                                                BaseIntitule = OleIntituleDataset.Tables(0)
                                                                If BaseIntitule.Rows.Count <> 0 Then
                                                                    If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                            .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                            .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(BaseIntitule.Rows(0).Item("Collectif")))
                                                                        End If
                                                                    End If
                                                                Else
                                                                    OleIntituleAdapter = New OleDbDataAdapter("select * from CLIENTS Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                                    OleIntituleDataset = New DataSet
                                                                    OleIntituleAdapter.Fill(OleIntituleDataset)
                                                                    BaseIntitule = OleIntituleDataset.Tables(0)
                                                                    If BaseIntitule.Rows.Count <> 0 Then
                                                                        If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                            If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                                .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                                .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(BaseIntitule.Rows(0).Item("Collectif")))
                                                                            End If
                                                                        End If
                                                                    Else
                                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                            .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                    If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                        Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                    End If
                                                Next j
                                                If Ecritusens = "C" Then
                                                    MontantCredit = Math.Round(MontantCredit + EcrMontant, 2)
                                                Else
                                                    MontantDebit = Math.Round(MontantDebit + EcrMontant, 2)
                                                End If

                                                .Write()
                                                .CouldModified()
                                                If BaseCpta.FactoryEcriture.ExistNumero(.EC_No) = True Then
                                                    Dim EcriturelibreA As IBOEcriture3
                                                    EcriturelibreA = BaseCpta.FactoryEcriture.ReadNumero(.EC_No)
                                                    With EcriturelibreA
                                                        Do While Pilindex.Count > 0
                                                            Dim OleInfoAdapter As OleDbDataAdapter
                                                            Dim OleInfoDataset As DataSet
                                                            Dim BaseInfo As DataTable
                                                            Dim Pindex As Integer
                                                            Pindex = Pilindex.Peek
                                                            OleInfoAdapter = New OleDbDataAdapter("select * from COECRITURE Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                            OleInfoDataset = New DataSet
                                                            OleInfoAdapter.Fill(OleInfoDataset)
                                                            BaseInfo = OleInfoDataset.Tables(0)
                                                            If BaseInfo.Rows.Count <> 0 Then
                                                                If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                    If Trim(Datagridaffiche.Item(Pindex, i).Value) <> "" Then
                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                    Else
                                                                        .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(BaseInfo.Rows(0).Item("Valeur"))
                                                                    End If
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
                                                        .CouldModified()
                                                        EcriturelibreA = Nothing
                                                    End With
                                                End If
                                                If Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Section").Index, i).Value) <> "" Then
                                                    If BaseCpta.FactoryAnalytique.ExistIntitule("" & Trim(PlanAna) & "") = True Then
                                                        Plan = Nothing
                                                        Plan = BaseCpta.FactoryAnalytique.ReadIntitule("" & Trim(PlanAna) & "")
                                                        EcriureCptAna = Nothing
                                                        EcriureCptAna = EcriureCpta.FactoryEcritureA.Create
                                                        With EcriureCptAna
                                                            .CompteA = BaseCpta.FactoryCompteA.ReadNumero(Plan, Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Section").Index, i).Value))
                                                            If Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Montant").Index, i).Value) <> "" Then
                                                                .EA_Montant = CDbl(Join(Split(Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Montant").Index, i).Value), "."), ",")) / 100
                                                            End If
                                                            .Write()
                                                            .CouldModified()
                                                        End With
                                                    End If
                                                End If

                                                If Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Section2").Index, i).Value) <> "" Then
                                                    If BaseCpta.FactoryAnalytique.ExistIntitule("" & Trim(PlanAna2) & "") = True Then
                                                        Plan = Nothing
                                                        Plan = BaseCpta.FactoryAnalytique.ReadIntitule("" & Trim(PlanAna2) & "")
                                                        EcriureCptAna = Nothing
                                                        EcriureCptAna = EcriureCpta.FactoryEcritureA.Create
                                                        With EcriureCptAna
                                                            .CompteA = BaseCpta.FactoryCompteA.ReadNumero(Plan, Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Section2").Index, i).Value))
                                                            If Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Montant").Index, i).Value) <> "" Then
                                                                .EA_Montant = CDbl(Join(Split(Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Montant").Index, i).Value), "."), ",")) / 100
                                                            End If
                                                            .Write()
                                                            .CouldModified()
                                                        End With
                                                    End If
                                                End If
                                                Pilindex.Clear()
                                            End With
                                        End If
                                    Else
                                        'Error_journal.WriteLine("Echec de Connexion à la base Comptable Sage" & BaseComptable)
                                    End If
                                Else
                                    'Error_journal.WriteLine("La Base GPS  " & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Societe").ColumnIndex, i).Value) & " ne Correspond à aucune Base Sage")
                                End If
                            Else
                                Dim PieceAdaptater As OleDbDataAdapter
                                Dim Piecedataset As DataSet
                                Dim Piecedatatable As DataTable
                                PieceAdaptater = New OleDbDataAdapter("select * from PIECEANO Where Piece='" & Trim(Datagridaffiche.Item(Datagridaffiche.Rows(i).Cells("Piece").ColumnIndex, i).Value) & "'", OleConnected)
                                Piecedataset = New DataSet
                                PieceAdaptater.Fill(Piecedataset)
                                Piecedatatable = Piecedataset.Tables(0)
                                If Piecedatatable.Rows.Count = 0 Then
                                    If BaseCpta.IsOpen = True Then
                                        EcriureCpta = BaseCpta.FactoryEcriture.Create
                                        With EcriureCpta
                                            For j = 0 To Datagridaffiche.ColumnCount - 1
                                                If Datagridaffiche.Columns(j).Name = "Facture" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .EC_RefPiece = Trim(Datagridaffiche.Item(j, i).Value)
                                                    End If
                                                End If

                                                If Datagridaffiche.Columns(j).Name = "CompteTiers" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                    End If
                                                End If
                                                If Datagridaffiche.Columns(j).Name = "LibelleEcriture" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .EC_Intitule = Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 35)
                                                    End If
                                                End If
                                                If Datagridaffiche.Columns(j).Name = "Piece" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .EC_Piece = Join(Split(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "-"), ""), "/"), "")
                                                    End If
                                                End If

                                                If Datagridaffiche.Columns(j).Name = "MontantDevise" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .EC_Devise = CDbl(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "."), ","))
                                                    End If
                                                End If
                                                If Datagridaffiche.Columns(j).Name = "Montant" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .EC_Montant = CDbl(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "."), ",")) / 100
                                                        EcrMontant = CDbl(Join(Split(Trim(Datagridaffiche.Item(j, i).Value), "."), ",")) / 100
                                                    End If
                                                End If

                                                If Datagridaffiche.Columns(j).Name = "CodeJournal" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .Journal = BaseCpta.FactoryJournal.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                    End If
                                                End If
                                                If Datagridaffiche.Columns(j).Name = "DatePiece" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .Date = CDate(Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 1, 4))
                                                    End If
                                                End If
                                                If Datagridaffiche.Columns(j).Name = "Sens" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        If Trim(Datagridaffiche.Item(j, i).Value) = "C" Then
                                                            .EC_Sens = EcritureSensType.EcritureSensTypeCredit
                                                            Ecritusens = "C"
                                                        Else
                                                            .EC_Sens = EcritureSensType.EcritureSensTypeDebit
                                                            Ecritusens = "D"
                                                        End If
                                                    End If
                                                End If

                                                If Datagridaffiche.Columns(j).Name = "DateEcheance" Then
                                                    If Trim(Datagridaffiche.Item(j, i).Value) <> "" Then
                                                        .EC_Echeance = CDate(Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 7, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 5, 2) & "/" & Strings.Mid(Trim(Datagridaffiche.Item(j, i).Value), 1, 4))
                                                        .Reglement = BaseCpta.FactoryReglement.List.Item(1)
                                                    End If
                                                End If
                                                If Datagridaffiche.Columns(j).Name = "CompteGeneral" Then
                                                    Dim BaseIntitule As DataTable
                                                    Dim OleIntituleAdapter As OleDbDataAdapter
                                                    Dim OleIntituleDataset As DataSet
                                                    OleIntituleAdapter = New OleDbDataAdapter("select * from FOURNISSEUR Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                    OleIntituleDataset = New DataSet
                                                    OleIntituleAdapter.Fill(OleIntituleDataset)
                                                    BaseIntitule = OleIntituleDataset.Tables(0)
                                                    If BaseIntitule.Rows.Count <> 0 Then
                                                        If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                            If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(BaseIntitule.Rows(0).Item("Collectif")))
                                                            End If
                                                        End If
                                                    Else
                                                        OleIntituleAdapter = New OleDbDataAdapter("select * from FOURNISSEURS Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                        OleIntituleDataset = New DataSet
                                                        OleIntituleAdapter.Fill(OleIntituleDataset)
                                                        BaseIntitule = OleIntituleDataset.Tables(0)
                                                        If BaseIntitule.Rows.Count <> 0 Then
                                                            If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                    .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                    .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(BaseIntitule.Rows(0).Item("Collectif")))
                                                                End If
                                                            End If
                                                        Else
                                                            OleIntituleAdapter = New OleDbDataAdapter("select * from CLIENT Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                            OleIntituleDataset = New DataSet
                                                            OleIntituleAdapter.Fill(OleIntituleDataset)
                                                            BaseIntitule = OleIntituleDataset.Tables(0)
                                                            If BaseIntitule.Rows.Count <> 0 Then
                                                                If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                    If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                        .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                        .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(BaseIntitule.Rows(0).Item("Collectif")))
                                                                    End If
                                                                End If
                                                            Else
                                                                OleIntituleAdapter = New OleDbDataAdapter("select * from CLIENTS Where Plant='" & Strings.Left(Trim(Datagridaffiche.Item(j, i).Value), 2) & "' ", OleConnenection)
                                                                OleIntituleDataset = New DataSet
                                                                OleIntituleAdapter.Fill(OleIntituleDataset)
                                                                BaseIntitule = OleIntituleDataset.Tables(0)
                                                                If BaseIntitule.Rows.Count <> 0 Then
                                                                    If BaseCpta.FactoryTiers.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                        If BaseCpta.FactoryCompteG.ExistNumero(Trim(BaseIntitule.Rows(0).Item("Collectif"))) = True Then
                                                                            .Tiers = BaseCpta.FactoryTiers.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                            .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(BaseIntitule.Rows(0).Item("Collectif")))
                                                                        End If
                                                                    End If
                                                                Else
                                                                    If BaseCpta.FactoryCompteG.ExistNumero(Trim(Datagridaffiche.Item(j, i).Value)) = True Then
                                                                        .CompteG = BaseCpta.FactoryCompteG.ReadNumero(Trim(Datagridaffiche.Item(j, i).Value))
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                                If Trim(Strings.Right(Trim(Datagridaffiche.Columns(j).HeaderText), 3)) = "oui" Then
                                                    Pilindex.Push(Datagridaffiche.Columns(j).Index)
                                                End If
                                            Next j
                                            If Ecritusens = "C" Then
                                                MontantCredit = Math.Round(MontantCredit + EcrMontant, 2)
                                            Else
                                                MontantDebit = Math.Round(MontantDebit + EcrMontant, 2)
                                            End If

                                            .Write()
                                            .CouldModified()
                                            If BaseCpta.FactoryEcriture.ExistNumero(.EC_No) = True Then
                                                Dim EcriturelibreA As IBOEcriture3
                                                EcriturelibreA = BaseCpta.FactoryEcriture.ReadNumero(.EC_No)
                                                With EcriturelibreA
                                                    Do While Pilindex.Count > 0
                                                        Dim OleInfoAdapter As OleDbDataAdapter
                                                        Dim OleInfoDataset As DataSet
                                                        Dim BaseInfo As DataTable
                                                        Dim Pindex As Integer
                                                        Pindex = Pilindex.Peek
                                                        OleInfoAdapter = New OleDbDataAdapter("select * from COECRITURE Where Colonne='" & Trim(Datagridaffiche.Columns(Pilindex.Pop).Name) & "'", OleConnenection)
                                                        OleInfoDataset = New DataSet
                                                        OleInfoAdapter.Fill(OleInfoDataset)
                                                        BaseInfo = OleInfoDataset.Tables(0)
                                                        If BaseInfo.Rows.Count <> 0 Then
                                                            If Trim(BaseInfo.Rows(0).Item("Type")) = "Chaine" Then
                                                                If Trim(Datagridaffiche.Item(Pindex, i).Value) <> "" Then
                                                                    .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(Datagridaffiche.Item(Pindex, i).Value)
                                                                Else
                                                                    .InfoLibre.Item("" & Trim(Datagridaffiche.Columns(Pindex).Name) & "") = Trim(BaseInfo.Rows(0).Item("Valeur"))
                                                                End If
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
                                                    .CouldModified()
                                                    EcriturelibreA = Nothing
                                                End With
                                            End If
                                            If Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Section").Index, i).Value) <> "" Then
                                                If BaseCpta.FactoryAnalytique.ExistIntitule("" & Trim(PlanAna) & "") = True Then
                                                    Plan = Nothing
                                                    Plan = BaseCpta.FactoryAnalytique.ReadIntitule("" & Trim(PlanAna) & "")
                                                    EcriureCptAna = Nothing
                                                    EcriureCptAna = EcriureCpta.FactoryEcritureA.Create
                                                    With EcriureCptAna
                                                        .CompteA = BaseCpta.FactoryCompteA.ReadNumero(Plan, Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Section").Index, i).Value))
                                                        If Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Montant").Index, i).Value) <> "" Then
                                                            .EA_Montant = CDbl(Join(Split(Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Montant").Index, i).Value), "."), ",")) / 100
                                                        End If
                                                        .Write()
                                                        .CouldModified()
                                                    End With
                                                End If
                                            End If

                                            If Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Section2").Index, i).Value) <> "" Then
                                                If BaseCpta.FactoryAnalytique.ExistIntitule("" & Trim(PlanAna2) & "") = True Then
                                                    Plan = Nothing
                                                    Plan = BaseCpta.FactoryAnalytique.ReadIntitule("" & Trim(PlanAna2) & "")
                                                    EcriureCptAna = Nothing
                                                    EcriureCptAna = EcriureCpta.FactoryEcritureA.Create
                                                    With EcriureCptAna
                                                        .CompteA = BaseCpta.FactoryCompteA.ReadNumero(Plan, Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Section2").Index, i).Value))
                                                        If Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Montant").Index, i).Value) <> "" Then
                                                            .EA_Montant = CDbl(Join(Split(Trim(Datagridaffiche.Item(Datagridaffiche.Columns("Montant").Index, i).Value), "."), ",")) / 100
                                                        End If
                                                        .Write()
                                                        .CouldModified()
                                                    End With
                                                End If
                                            End If
                                            Pilindex.Clear()
                                        End With
                                    End If
                                End If
                            End If
                        End If
                        ProgressBar1.Value = ProgressBar1.Value + 1
                        NbreLigne = NbreLigne + 1
                        Label8.Text = NbreLigne & "/" & ProgresMax
                        Label8.Refresh()
                        TextBox1.Refresh()
                        TextBox2.Refresh()
                        TextBox3.Refresh()
                    End If
                Catch ex As Exception
                    ProgressBar1.Value = ProgressBar1.Value + 1
                    NbreLigne = NbreLigne + 1
                    Label8.Text = NbreLigne & "/" & ProgresMax
                    Label8.Refresh()
                    CloseBaseFree()
                    Error_journal.WriteLine("Message Syteme   " & ex.Message)
                End Try
            Next i
            TextBox1.Text = MontantDebit
            TextBox2.Text = MontantCredit
            TextBox3.Text = Math.Round(TextBox2.Text - TextBox1.Text, 2)
            Datagridaffiche.Rows.Clear()
        End If
        Me.Cursor = Cursors.Default
        BT_integrer.Enabled = True

    End Sub
    Private Sub BT_Quitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub Journal()
        Try
            Dim Deletjour As String
            Dim OleCommandDelete As OleDbCommand
            Deletjour = "delete * from JOURNAL"
            OleCommandDelete = New OleDbCommand(Deletjour)
            OleCommandDelete.Connection = OleConnected
            OleCommandDelete.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PieceAnomalie()
        Try
            Dim Deletjour As String
            Dim OleCommandDelete As OleDbCommand
            Deletjour = "delete * from PIECEANO"
            OleCommandDelete = New OleDbCommand(Deletjour)
            OleCommandDelete.Connection = OleConnected
            OleCommandDelete.ExecuteNonQuery()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BT_integrer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_integrer.Click
        Dim i As Integer
        Dim Fichieratraiter As String
        Dim MailAdresse As String
        MailAdresse = Nothing
        Fichieratraiter = Nothing
        Filebool = True
        Try
            If DataListeIntegrer.RowCount > 0 Then
                Fichieratraiter = Pathsfilejournal & "LOGIMPORT_ECR" & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]"
                Error_journal = File.AppendText(Fichieratraiter & ".txt")
                For i = 0 To DataListeIntegrer.RowCount - 1
                    If Filebool = False Then
                        Exit Sub
                    End If
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
                            Verification_Du_Fichier(DataListeIntegrer.Rows(i).Cells("CheminExport").Value, DataListeIntegrer.Rows(i).Cells("Chemin").Value)
                            If Equilibrer = True Then
                                Label5.Refresh()
                                Label5.Text = "Verification Terminée! Integration En Cours"
                                Integration_Du_Fichier(DataListeIntegrer.Rows(i).Cells("CheminExport").Value, DataListeIntegrer.Rows(i).Cells("Chemin").Value)
                                Dim FileName As String
                                FileName = DataListeIntegrer.Rows(i).Cells("CheminExport").Value
                                Do While InStr(Trim(FileName), "\") <> 0
                                    FileName = Strings.Right(FileName, Strings.Len(Trim(FileName)) - InStr(Trim(FileName), "\"))
                                Loop
                                File.Move(DataListeIntegrer.Rows(i).Cells("CheminExport").Value, PathsfileSave & "" & Join(Split(FileName, "."), "_" & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]."))
                                DataListeIntegrer.Rows(i).Cells("Valider").Value = False
                                Error_journal.WriteLine("Fin de Traitement du Fichier : " & NomFichier & "  Date Fin d'import: " & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]")
                                Error_journal.WriteLine("")
                                Error_journal.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")

                            Else
                                Error_journal.WriteLine("Fichier Non Traité: " & NomFichier & "  Date Fin d'analyse: " & Strings.Right(DateAndTime.Year(Now), 2) & "" & Format(DateAndTime.Month(Now), "00") & "" & Format(DateAndTime.Day(Now), "00") & "[" & "" & Format(DateAndTime.Hour(Now), "00") & "-" & Format(DateAndTime.Minute(Now), "00") & "-" & Format(DateAndTime.Second(Now), "00") & "]")
                                Error_journal.WriteLine("")
                                Error_journal.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------")
                            End If
                        End If
                    End If
                    Journal()
                    PieceAnomalie()
                    Message = Nothing
                Next i
                Error_journal.Close()
            End If
            AfficheSchemasIntegrer()
            CloseBaseFree()
        Catch ex As Exception
            CloseBaseFree()
        End Try

    End Sub
    Private Sub Verification_Du_Fichier(ByVal sPathFilexporter As String, ByVal spathFileFormat As String)
        If RechercheFormatype(spathFileFormat) <> "" Then
            If RechercheFormatype(spathFileFormat) = "Tabulation" Then
                sColumnsSepar = ControlChars.Tab
            Else
                If RechercheFormatype(spathFileFormat) = "Point Virgule" Then
                    sColumnsSepar = ";"
                End If
            End If
            AnalyseLectureDuFichier(sPathFilexporter, spathFileFormat, sColumnsSepar)
        End If
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
            Lancement_Du_Fichier(sPathFilexporter, spathFileFormat, sColumnsSepar)
        End If
    End Sub
    Private Sub DataListeIntegrer_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeIntegrer.CellClick
        Try
            If e.RowIndex >= 0 Then
                If DataListeIntegrer.Columns(e.ColumnIndex).Name = "Valider" Then
                    IndexPrec = e.RowIndex
                    TxtChemin.Text = ""
                    TxtFormat.Text = ""
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Fr_ImportationEcriture_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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
    Private Sub Fr_Importation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Connected() = True Then
                AfficheImport()
                AfficheSchemasIntegrer()
                Journal()
                PieceAnomalie()
                Initialiser()
                Datagridaffiche.Rows.Clear()
                Datagridaffiche.Columns.Clear()
                TxtChemin.Text = ""
                TxtFormat.Text = ""
                ProgressBar1.Value = ProgressBar1.Minimum
                Me.WindowState = FormWindowState.Maximized
                Pilindex.Clear()
                GroupBox6.Width = Me.Width
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BT_Apercue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If TxtChemin.Text <> "" And TxtFormat.Text <> "" Then
                If File.Exists(TxtChemin.Text) = True And File.Exists(TxtFormat.Text) Then
                    Lecture_Suivant_DuFichierExcel(TxtChemin.Text, TxtFormat.Text)
                End If
            Else
                If DataListeIntegrer.Rows(IndexPrec).Cells("Valider").Value = True Then
                    If File.Exists(DataListeIntegrer.Rows(IndexPrec).Cells("Chemin").Value) = True And File.Exists(DataListeIntegrer.Rows(IndexPrec).Cells("CheminExport").Value) Then
                        Lecture_Suivant_DuFichierExcel(DataListeIntegrer.Rows(IndexPrec).Cells("CheminExport").Value, DataListeIntegrer.Rows(IndexPrec).Cells("Chemin").Value)
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value = 100 Then
            Try
                Timer2.Enabled = False
                Timer3.Enabled = False
                Timer1.Enabled = False
                BT_integrer_Click(sender, e)
                CloseBase()
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value = ProgressBar1.Value + 1
        End If
        If ProgressBar1.Value = 100 Then
            Timer2.Enabled = False
            Timer3.Enabled = False
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Timer1.Enabled = True
        Timer2.Enabled = True
    End Sub
End Class
