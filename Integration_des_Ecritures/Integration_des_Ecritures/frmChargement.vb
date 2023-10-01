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
        Dim RepCompta As Object
        Dim RepAccess As Object
        If ProgressBar1.Value = 100 Then
            SqlData = Connected()
            Accessbool = Connected_to_Access()
            If SqlData = True Then
                If Accessbool = True Then
                    MenuApplication.Show()
                    Me.Close()
                Else
                    MenuApplication.MenuItem13.Enabled = False
                    Fr_ImportationEcriture.BT_integrer.Enabled = False
                    Timer2.Enabled = False
                    Timer3.Enabled = False
                    Timer1.Enabled = False
                    RepCompta = MsgBox("Erreur de Connexion: la base Access a ete déplacée  " & Chr(13) & "" & Chr(13) & "Cliquer sur OK pour Continuer" & Chr(13) & "" & Chr(13) & "                 Ou Sur  " & Chr(13) & "" & Chr(13) & "Annuler Pour Quitter Le Programme", MsgBoxStyle.YesNo, "Connexion à la base Access")
                    If RepCompta = MsgBoxResult.Yes Then
                        MenuApplication.Show()
                        Me.Close()
                    Else
                        End
                    End If
                End If
            Else
                MenuApplication.MenuAjoutForEcriture.Enabled = False
                MenuApplication.MenuAjoutFormatFournisseur.Enabled = False
                MenuApplication.MenuFormatEcriture.Enabled = False
                MenuApplication.MenuFormatFournisseur.Enabled = False
                MenuApplication.MenuInfolibreEcriture.Enabled = False
                MenuApplication.MenuInfolibreFournisseur.Enabled = False
                MenuApplication.MenuParametrageSociete.Enabled = False
                MenuApplication.MenuPlanFournisseur.Enabled = False
                MenuApplication.MenuPlanSpecifiqueFournisseur.Enabled = False
                MenuApplication.MenuRepertoireEcriture.Enabled = False
                MenuApplication.MenuRepertoireFournisseur.Enabled = False
                MenuApplication.MenuItem10.Enabled = False
                MenuApplication.MenuItem11.Enabled = False
                MenuApplication.MenuItem13.Enabled = False
                MenuApplication.MenuItem14.Enabled = False
                MenuApplication.MenuItem15.Enabled = False
                MenuApplication.MenuItem16.Enabled = False
                MenuApplication.MenuItem17.Enabled = False
                MenuApplication.MenuItem19.Enabled = False
                MenuApplication.MenuItem2.Enabled = False
                MenuApplication.MenuItem20.Enabled = False
                MenuApplication.MenuItem21.Enabled = False
                MenuApplication.MenuItem22.Enabled = False
                MenuApplication.MenuItem23.Enabled = False
                MenuApplication.MenuItem24.Enabled = False
                MenuApplication.MenuItem25.Enabled = False
                MenuApplication.MenuItem26.Enabled = False
                MenuApplication.MenuItem27.Enabled = False
                MenuApplication.MenuItem28.Enabled = False
                MenuApplication.MenuItem29.Enabled = False
                MenuApplication.MenuItem3.Enabled = False
                MenuApplication.MenuItem30.Enabled = False
                MenuApplication.MenuItem31.Enabled = False
                MenuApplication.MenuItem32.Enabled = False
                MenuApplication.MenuItem33.Enabled = False
                MenuApplication.MenuItem37.Enabled = False
                MenuApplication.MenuItem38.Enabled = False
                MenuApplication.MenuItem40.Enabled = False
                MenuApplication.MenuItem41.Enabled = False
                MenuApplication.MenuItem4.Enabled = False
                MenuApplication.TreeView.Enabled = False
                Timer2.Enabled = False
                Timer3.Enabled = False
                Timer1.Enabled = False
                RepAccess = MsgBox("Erreur de Connexion à SQL Server " & Chr(13) & "" & Chr(13) & "Modifiez Le Fichier de Configuration", MsgBoxStyle.YesNo, "Connexion au Serveur SQL")
                If MsgBoxResult.Yes = RepAccess Then
                    MenuApplication.Show()
                    Me.Close()
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
