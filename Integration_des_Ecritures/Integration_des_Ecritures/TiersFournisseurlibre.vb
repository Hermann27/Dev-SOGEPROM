Imports System.Data.OleDb
Public Class TiersFournisseurlibre
    Public Num_Count As Integer
    Public OleSocieteAdaptater As OleDbDataAdapter
    Public OleSocieteDataset As DataSet
    Public OledatableSociete As DataTable

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Frm_Periode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Connected() = True Then
            Initialiser()
        End If

    End Sub
    Private Sub UpdateParametre()
        Dim OleUpdatAdaptater As OleDbDataAdapter
        Dim OleUpdatDataset As DataSet
        Dim OleDatable As DataTable
        Dim UpdateSociete As String
        Dim OleCommandUpdate As OleDbCommand
        Try
            If Trim(DateDebut.Text) <> "" And Trim(Periode.Text) <> "" And Trim(DateFin.Text) <> "" Then
                OleUpdatAdaptater = New OleDbDataAdapter("select * From COTIERS where Libelle='" & Trim(Periode.Text) & "' Or Colonne='" & Trim(DateDebut.Text) & "'", OleConnenection)
                OleUpdatDataset = New DataSet
                OleUpdatAdaptater.Fill(OleUpdatDataset)
                OleDatable = OleUpdatDataset.Tables(0)
                If OleDatable.Rows.Count <> 0 Then
                    UpdateSociete = "Update COTIERS SET Type='" & Trim(DateFin.Text) & "' where Libelle='" & Trim(Periode.Text) & "' Or Colonne='" & Trim(DateDebut.Text) & "'"
                    OleCommandUpdate = New OleDbCommand(UpdateSociete)
                    OleCommandUpdate.Connection = OleConnenection
                    OleCommandUpdate.ExecuteNonQuery()
                    MsgBox("Modification Effectuée avec Succès!", MsgBoxStyle.Information, "Modification Information libre")
                    Initialiser()
                Else
                    MsgBox("Aucune Modification Effectuée!", MsgBoxStyle.Information, "Modification Information libre")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Creationperiode()
        Dim OleAdaptaterEnreg As OleDbDataAdapter
        Dim OleEnregDataset As DataSet
        Dim OledatableEnreg As DataTable
        Dim OleCommandEnreg As OleDbCommand
        Dim Insert As Boolean = False
        Dim Insertion As String
        Try
            If Trim(DateDebut.Text) <> "" And Trim(Periode.Text) <> "" And Trim(DateFin.Text) <> "" Then
                OleAdaptaterEnreg = New OleDbDataAdapter("select * From COTIERS where Libelle='" & Trim(Periode.Text) & "' Or Colonne='" & Trim(DateDebut.Text) & "'", OleConnenection)
                OleEnregDataset = New DataSet
                OleAdaptaterEnreg.Fill(OleEnregDataset)
                OledatableEnreg = OleEnregDataset.Tables(0)
                If OledatableEnreg.Rows.Count <> 0 Then
                    MsgBox("Impossible! Information Existante", MsgBoxStyle.Information, "Creation Information Libre")
                Else
                    Insertion = "Insert Into COTIERS (Colonne,Libelle,Libre,Type) VALUES ('" & Join(Split(Join(Split(Join(Split(Join(Split(Trim(DateDebut.Text), " "), "_"), "-"), ""), "/"), ""), "\"), "") & "','" & Trim(Periode.Text) & "','1','" & Trim(DateFin.Text) & "')"
                    OleCommandEnreg = New OleDbCommand(Insertion)
                    OleCommandEnreg.Connection = OleConnenection
                    OleCommandEnreg.ExecuteNonQuery()
                    MsgBox("Insertion Reussie", MsgBoxStyle.Information, "Creation Information Libre")
                    Raffraichir()
                    Initialiser()
                End If
            Else
                MsgBox("Information Inexistant", MsgBoxStyle.Information, "Creation Information Libre")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub supprimeperiode()
        Dim OleAdaptaterDelete As OleDbDataAdapter
        Dim OleDeleteDataset As DataSet
        Dim OledatableDelete As DataTable
        Dim OleCommandDelete As OleDbCommand
        Dim DelFile As String
        Try
            If Trim(DateDebut.Text) <> "" And Trim(Periode.Text) <> "" And Trim(DateFin.Text) <> "" Then
                OleAdaptaterDelete = New OleDbDataAdapter("select * From COTIERS where Libelle='" & Trim(Periode.Text) & "' Or Colonne='" & Trim(DateDebut.Text) & "'", OleConnenection)
                OleDeleteDataset = New DataSet
                OleAdaptaterDelete.Fill(OleDeleteDataset)
                OledatableDelete = OleDeleteDataset.Tables(0)
                If OledatableDelete.Rows.Count <> 0 Then
                    DelFile = "Delete From COTIERS where Libelle='" & Trim(Periode.Text) & "' Or Colonne='" & Trim(DateDebut.Text) & "'"
                    OleCommandDelete = New OleDbCommand(DelFile)
                    OleCommandDelete.Connection = OleConnenection
                    OleCommandDelete.ExecuteNonQuery()
                    MsgBox("Suppression Reussie", MsgBoxStyle.Information, "Suppression Information libre")
                    Raffraichir()
                    Initialiser()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BT_Creer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Creer.Click
        Try
            Creationperiode()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BT_Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Update.Click
        Try
            UpdateParametre()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BT_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Del.Click
        Try
            supprimeperiode()

        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub AfficheParametreNext()
        Try
            If Num_Count <> OledatableSociete.Rows.Count - 1 Then
                Num_Count = Num_Count + 1
            Else
                Num_Count = OledatableSociete.Rows.Count - 1
            End If
            If OledatableSociete.Rows.Count <> 0 Then
                Raffraichir()
                Call Affiche()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AfficheParametrePrevious()
        Try
            If Num_Count > 0 Then
                Num_Count = Num_Count - 1
            Else
                Num_Count = 0
            End If
            If OledatableSociete.Rows.Count <> 0 Then
                Raffraichir()
                Call Affiche()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Initialiser()
        Try
            OleSocieteAdaptater = New OleDbDataAdapter("select * From COTIERS where Libre='1' ", OleConnenection)
            OleSocieteDataset = New DataSet
            OleSocieteAdaptater.Fill(OleSocieteDataset)
            OledatableSociete = OleSocieteDataset.Tables(0)
            If OledatableSociete.Rows.Count <> 0 Then
                Num_Count = 0
                Call Affiche()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Affiche()
        Try
            Periode.Text = OledatableSociete.Rows(Num_Count).Item("Libelle")
            DateDebut.Text = OledatableSociete.Rows(Num_Count).Item("Colonne")
            DateFin.Text = OledatableSociete.Rows(Num_Count).Item("Type")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BT_Suivant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Suivant.Click
        Try
            AfficheParametreNext()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Raffraichir()
        Periode.Text = ""
        DateDebut.Text = ""
        DateFin.Text = ""
    End Sub

    Private Sub BT_Prec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Prec.Click
        Try
            AfficheParametrePrevious()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Periode.Text = ""
        DateDebut.Text = ""
    End Sub
End Class