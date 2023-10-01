Option Explicit On
Imports System.IO
Imports Objets100Lib
Imports System.Data.OleDb
Imports System.Text
Imports System.Xml
Module Connect
    Public Declare Ansi Function GetPrivateProfileString Lib "kernel32" _
            Alias "GetPrivateProfileStringA" (ByVal Ka_Pouliyou As String, _
            ByVal K_Pouliyou As String, ByVal K_Wande As String, _
            ByVal K_Hamegni As String, ByVal K_Jamegni As Integer, _
            ByVal K_Djantseu As String) As Long
    Public Declare Ansi Function WritePrivateProfileString Lib "kernel32" _
            Alias "WritePrivateProfileStringA" (ByVal App_Section As String, ByVal App_Cle As String, ByVal App_Valeur As String, ByVal App_Path As String) As Boolean
    Public BaseCpta As New BSCPTAApplication3
    Public Error_journal As StreamWriter
    Public OleExcelConnect As New OleDbConnection
    Public OleConnected As New OleDbConnection
    Public OleExcelAdapter As OleDbDataAdapter
    Public OleExcelDataset As DataSet
    Public Pouliyou_Fichier As String
    Public NomBaseCpta As String
    Public Pathsfilejournal As String
    Public PathsfileSave As String
    Public Reglementjournal As String
    Public ReglementSave As String
    Public NomServersql As String
    Public Mot_Passql As String
    Public Nom_Utilsql As String
    Public Comptabool As Boolean
    Public Accessbool As Boolean
    Public SqlData As Boolean

    Public OleConnenection As OleDbConnection
    Public Function OpenBaseCpta(ByRef BaseCpta As BSCPTAApplication3, ByRef NomBaseCpta As String, Optional ByVal Utilisateur As String = "", Optional ByVal MotDePasse As String = "") As Boolean
        Try
            BaseCpta.Name = NomBaseCpta
            If Utilisateur <> "" Then
                BaseCpta.Loggable.UserName = Utilisateur
                BaseCpta.Loggable.UserPwd = MotDePasse
            End If
            BaseCpta.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Function LireChaine(ByVal Kamen_Fichier_Ini As String, ByVal Pouliyou_Section As String, ByVal Djantcheu_Key As String) As String
        Dim X As Long
        Dim Ham_buffer As String

        Ham_buffer = Space(300)
        X = GetPrivateProfileString(Pouliyou_Section, Djantcheu_Key, "", Ham_buffer, Len(Ham_buffer), Kamen_Fichier_Ini)
        If Len(Trim(Left(Ham_buffer, 295))) > 0 Then
            LireChaine = Left(Trim(Left(Ham_buffer, 295)), Len(Trim(Left(Ham_buffer, 295))) - 1)
        Else
            LireChaine = Nothing
        End If
    End Function
    Public Function LirefichierConfig()
        Pouliyou_Fichier = My.Application.Info.DirectoryPath & "\ConnectAPI.Ini"
        NomBaseCpta = LireChaine(Pouliyou_Fichier, "CONNECTION", "BASE DE DONNEES COMPTA")
        NomServersql = LireChaine(Pouliyou_Fichier, "CONNECTION", "SERVEUR SQL")
        Mot_Passql = LireChaine(Pouliyou_Fichier, "CONNECTION", "MOT DE PASSE SQL")
        Nom_Utilsql = LireChaine(Pouliyou_Fichier, "CONNECTION", "UTILISATEUR SQL")
        LirefichierConfig = Nothing
    End Function
    Public Function Connected() As Boolean
        Try
            'My.Application.Info.DirectoryPath & "\ECRITURECPT.mdb" 
            OleConnenection = New OleDbConnection("provider=SQLOLEDB;UID=" & Trim(Nom_Utilsql) & ";Pwd=" & Trim(Mot_Passql) & ";Initial Catalog=" & Trim(NomBaseCpta) & ";Data Source=" & Trim(NomServersql) & "")
            OleConnenection.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub CreateComboBoxColumn(ByRef Dataobject As DataGridView, ByRef Colname As String, ByRef HeaderName As String)
        Dim ocolumn As New DataGridViewTextBoxColumn
        With ocolumn
            .Name = HeaderName
            .HeaderText = Colname
            .Width = 100
            .Visible = True
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            .ReadOnly = False
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        Dataobject.Columns.Add(ocolumn)
    End Sub
    Public Sub CloseBase()
        Try
            If BaseCpta.IsOpen Then
                BaseCpta.Close()
                End
            Else
                End
            End If
        Catch ex As Exception
            MsgBox("Erreur de Fermeture de la Base Comptabilite!", MsgBoxStyle.Critical, "Fermeture de le Base")
            If MsgBoxResult.Yes = 6 Then
                End
            End If
        End Try
    End Sub
    Public Sub CloseBaseFree()
        Try
            If BaseCpta.IsOpen Then
                BaseCpta.Close()
            Else
            End If
        Catch ex As Exception
            MsgBox("Erreur de Fermeture de la Base Comptabilite!", MsgBoxStyle.Critical, "Fermeture de le Base")
            If MsgBoxResult.Yes = 6 Then
                End
            End If
        End Try
    End Sub
    Public Function GetArrayFile(ByVal sPath As String, Optional ByRef aLines() As String = Nothing) As Object
        GetArrayFile = File.ReadAllLines(sPath, Encoding.Default)
        aLines = GetArrayFile
        Return aLines
    End Function
    Public Function Connected_to_Access() As Boolean
        Try
            OleConnected = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Application.Info.DirectoryPath & "\ECRITURECPT.mdb")
            OleConnected.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function RechercheFormatype(ByRef CheminFile As String) As String
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
End Module
