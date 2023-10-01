Imports System.Data.OleDb
Public Class Frm_EtatDeConexion

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = False
        Timer2.Enabled = False
        Call LirefichierConfig()
        If Connected() = True Then
            VerificationConnexion()
        End If
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value = ProgressBar1.Value + 1
        End If
        If ProgressBar1.Value > 0 And ProgressBar1.Value < 20 Then
        End If
        If ProgressBar1.Value > 20 And ProgressBar1.Value < 40 Then
        End If
        If ProgressBar1.Value > 40 And ProgressBar1.Value < 70 Then
        End If
        If ProgressBar1.Value > 70 And ProgressBar1.Value < 90 Then
        End If
        If ProgressBar1.Value > 90 And ProgressBar1.Value <= 100 Then
        End If
        If ProgressBar1.Value > 95 And ProgressBar1.Value <= 100 Then
        End If
        If ProgressBar1.Value = 100 Then
            Timer2.Enabled = False
            Timer3.Enabled = False
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value = 100 Then
            Timer2.Enabled = False
            Timer3.Enabled = False
            Timer1.Enabled = False
            'Me.Close()
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Timer1.Enabled = True
        Timer2.Enabled = True
    End Sub
    Private Sub VerificationConnexion()
        Dim OleAdaptaterPara As OleDbDataAdapter
        Dim OleParaDataset As DataSet
        Dim OledatablePara As DataTable
        Dim i As Integer
        Dim BaseCatal() As String
        OleAdaptaterPara = New OleDbDataAdapter("select * from SOCIETE", OleConnenection)
        OleParaDataset = New DataSet
        OleAdaptaterPara.Fill(OleParaDataset)
        OledatablePara = OleParaDataset.Tables(0)
        If OledatablePara.Rows.Count <> 0 Then
            For i = 0 To OledatablePara.Rows.Count - 1
                DataConnexion.RowCount = i + 1
                BaseCatal = Split(OledatablePara.Rows(i).Item("SOCSAGE"), ".")
                If TestConnected(OledatablePara.Rows(i).Item("Server"), Trim(BaseCatal(0)), OledatablePara.Rows(i).Item("ServerUser"), OledatablePara.Rows(i).Item("ServerPasse")) = True Then
                    DataConnexion.Rows(i).Cells("Connexion").Value = "Connexion à la Société  " & Trim(BaseCatal(0))
                    DataConnexion.Rows(i).Cells("Reussie").Value = "Reussie....."
                Else
                    DataConnexion.Rows(i).Cells("Connexion").Value = "Connexion à la Société  " & Trim(BaseCatal(0))
                    DataConnexion.Rows(i).Cells("Reussie").Value = "Echec....."
                    DataConnexion.Rows(i).DefaultCellStyle.BackColor = Color.DarkRed
                End If
            Next i
        End If
    End Sub
    Public Function TestConnected(ByRef NomServer As String, ByRef Catalog As String, ByRef Nom_Utsql As String, ByRef Mot_Psql As String) As Boolean
        Dim OleConnectedTest As OleDbConnection
        Try
            OleConnectedTest = New OleDbConnection("provider=SQLOLEDB;Data Source=" & Trim(NomServer) & ";Initial Catalog=" & Trim(Catalog) & ";UID=" & Trim(Nom_Utsql) & ";Pwd=" & Trim(Mot_Psql) & "")
            OleConnectedTest.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
