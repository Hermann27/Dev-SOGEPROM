Public Class frmChargement
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value = ProgressBar1.Value + 1
        End If
        If ProgressBar1.Value > 0 And ProgressBar1.Value < 20 Then
            Label1.Text = "Detection des Parametres De Connexion"
        End If
        If ProgressBar1.Value > 20 And ProgressBar1.Value < 40 Then
            Label1.Text = ""
        End If
        If ProgressBar1.Value > 40 And ProgressBar1.Value < 70 Then
            Label1.Text = "Mise a Jour des Fichiers"
        End If
        If ProgressBar1.Value > 70 And ProgressBar1.Value < 90 Then
            Label1.Text = "Connexion au Serveur"
        End If
        If ProgressBar1.Value > 90 And ProgressBar1.Value <= 100 Then
            Label4.Text = "Chargement de l'object Metiers 100"

        End If
        If ProgressBar1.Value > 95 And ProgressBar1.Value <= 100 Then
            Label3.Text = "Patienter Quelques Secondes ...SVP..."
        End If
        If ProgressBar1.Value = 100 Then
            Timer2.Enabled = False
            Timer3.Enabled = False
        End If
        Label2.Text = ProgressBar1.Value & " %"
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim RepAccess As Object
        If ProgressBar1.Value = 100 Then
            SqlData = Connected()
            If SqlData = True Then
                Reglement.Show()
                Me.Close()
            Else
                Timer2.Enabled = False
                Timer3.Enabled = False
                Timer1.Enabled = False
                RepAccess = MsgBox("Erreur de Connexion à SQL Server " & Chr(13) & "" & Chr(13) & "Modifiez Le Fichier de Configuration", MsgBoxStyle.YesNo, "Connexion au Serveur SQL")
                If MsgBoxResult.Yes = RepAccess Then
                    End
                Else
                    End
                End If
            End If
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Timer1.Enabled = True
        Timer2.Enabled = True
    End Sub

    Private Sub frmChargement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = False
        Timer2.Enabled = False
        Call LirefichierConfig()
    End Sub
End Class
