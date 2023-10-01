Public Class Frm_Position

    Private Sub Frm_Position_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call LirefichierConfig()
        TxtBDCpta.Text = NomBaseCpta
        Txtsql.Text = NomServersql
        TxtUtilisateur.Text = Nom_Utilsql
        TxtPasw.Text = Mot_Passql
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Bool As Boolean
        If Trim(TxtBDCpta.Text) <> "" Then
            Bool = WritePrivateProfileString("CONNECTION", "BASE DE DONNEES COMPTA", Trim(TxtBDCpta.Text), Pouliyou_Fichier)
        End If
        Bool = WritePrivateProfileString("CONNECTION", "UTILISATEUR SQL", Trim(TxtUtilisateur.Text), Pouliyou_Fichier)
        Bool = WritePrivateProfileString("CONNECTION", "MOT DE PASSE SQL", Trim(TxtPasw.Text), Pouliyou_Fichier)
        Bool = WritePrivateProfileString("CONNECTION", "SERVEUR SQL", Trim(Txtsql.Text), Pouliyou_Fichier)


        MsgBox("Modification Terminée!", MsgBoxStyle.Information, "Modification Fichier Ini")

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    

    
End Class