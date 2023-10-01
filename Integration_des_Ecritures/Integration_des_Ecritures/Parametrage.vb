Imports System.Data.OleDb
Imports System.IO
Public Class Parametrage
    Private Sub AfficheSchemasConso()
        Try
            Dim i As Integer
            Dim OleAdaptaterschema As OleDbDataAdapter
            Dim OleSchemaDataset As DataSet
            Dim OledatableSchema As DataTable
            DataListeIntegrer.Rows.Clear()

            OleAdaptaterschema = New OleDbDataAdapter("select * from SOCIETE", OleConnenection)
            OleSchemaDataset = New DataSet
            OleAdaptaterschema.Fill(OleSchemaDataset)
            OledatableSchema = OleSchemaDataset.Tables(0)
            DataListeIntegrer.RowCount = OledatableSchema.Rows.Count
            For i = 0 To OledatableSchema.Rows.Count - 1
                DataListeIntegrer.Rows(i).Cells("Societe1").Value = OledatableSchema.Rows(i).Item("SOCGPS")
                DataListeIntegrer.Rows(i).Cells("Base1").Value = OledatableSchema.Rows(i).Item("SOCSAGE")
                DataListeIntegrer.Rows(i).Cells("Fichier1").Value = OledatableSchema.Rows(i).Item("FICHIERCPTA")
                DataListeIntegrer.Rows(i).Cells("Utilisateur1").Value = OledatableSchema.Rows(i).Item("UTILISATEUR")
                DataListeIntegrer.Rows(i).Cells("Passe1").Value = OledatableSchema.Rows(i).Item("MOTPASSE")
                DataListeIntegrer.Rows(i).Cells("Plan1").Value = OledatableSchema.Rows(i).Item("PlanAna")
                DataListeIntegrer.Rows(i).Cells("Plan21").Value = OledatableSchema.Rows(i).Item("PlanAna2")
                DataListeIntegrer.Rows(i).Cells("Server1").Value = OledatableSchema.Rows(i).Item("Server")
                DataListeIntegrer.Rows(i).Cells("User1").Value = OledatableSchema.Rows(i).Item("ServerUser")
                DataListeIntegrer.Rows(i).Cells("Pasword1").Value = OledatableSchema.Rows(i).Item("ServerPasse")
            Next i
        Catch ex As Exception
            MsgBox("Message Systeme: " & ex.Message, MsgBoxStyle.Information, "Societes Comptables")
        End Try
    End Sub
    Private Sub Delete_DataListeSch()
        Dim i As Integer
        Dim OleAdaptaterDelete As OleDbDataAdapter
        Dim OleDeleteDataset As DataSet
        Dim OledatableDelete As DataTable
        Dim OleCommandDelete As OleDbCommand
        Dim DelFile As String
        For i = 0 To DataListeIntegrer.RowCount - 1
            If DataListeIntegrer.Rows(i).Cells("Supprimer").Value = True Then
                OleAdaptaterDelete = New OleDbDataAdapter("select * from SOCIETE where SOCGPS='" & DataListeIntegrer.Rows(i).Cells("Societe1").Value & "'", OleConnenection)
                OleDeleteDataset = New DataSet
                OleAdaptaterDelete.Fill(OleDeleteDataset)
                OledatableDelete = OleDeleteDataset.Tables(0)
                If OledatableDelete.Rows.Count <> 0 Then
                    DelFile = "Delete From SOCIETE where SOCGPS='" & DataListeIntegrer.Rows(i).Cells("Societe1").Value & "'"
                    OleCommandDelete = New OleDbCommand(DelFile)
                    OleCommandDelete.Connection = OleConnenection
                    OleCommandDelete.ExecuteNonQuery()
                End If
            End If
        Next i
        AfficheSchemasConso()
    End Sub
    Private Sub EnregistrerLeSchema()
        Try
            Dim n As Integer
            Dim OleAdaptaterEnreg As OleDbDataAdapter
            Dim OleEnregDataset As DataSet
            Dim OledatableEnreg As DataTable
            Dim OleCommandEnreg As OleDbCommand
            Dim Insert As Boolean = False
            Dim Insertion As String
            If DataListeSchema.RowCount >= 1 Then
                For n = 0 To DataListeSchema.RowCount - 1
                    OleAdaptaterEnreg = New OleDbDataAdapter("select * From SOCIETE WHERE SOCGPS='" & DataListeSchema.Rows(n).Cells("Societe").Value & "'", OleConnenection)
                    OleEnregDataset = New DataSet
                    OleAdaptaterEnreg.Fill(OleEnregDataset)
                    OledatableEnreg = OleEnregDataset.Tables(0)
                    If OledatableEnreg.Rows.Count <> 0 Then
                        MsgBox("Cette Societé Existe déja!", MsgBoxStyle.Information, "Creation Parametres")
                    Else
                        If Trim(DataListeSchema.Rows(n).Cells("Base").Value) <> "" Then
                            Insertion = "Insert Into SOCIETE (SOCGPS,SOCSAGE,FICHIERCPTA,UTILISATEUR,MOTPASSE,PlanAna,PlanAna2,Server,ServerUser,ServerPasse) VALUES ('" & DataListeSchema.Rows(n).Cells("Societe").Value & "','" & DataListeSchema.Rows(n).Cells("base").Value & "','" & DataListeSchema.Rows(n).Cells("Fichier").Value & "','" & DataListeSchema.Rows(n).Cells("Utilisateur").Value & "','" & DataListeSchema.Rows(n).Cells("Passe").Value & "','" & DataListeSchema.Rows(n).Cells("Plan").Value & "','" & DataListeSchema.Rows(n).Cells("Plan2").Value & "','" & DataListeSchema.Rows(n).Cells("Server").Value & "','" & DataListeSchema.Rows(n).Cells("User").Value & "','" & DataListeSchema.Rows(n).Cells("Pasword").Value & "')"
                            OleCommandEnreg = New OleDbCommand(Insertion)
                            OleCommandEnreg.Connection = OleConnenection
                            OleCommandEnreg.ExecuteNonQuery()
                            Insert = True
                        End If
                    End If
                Next n
                If Insert = True Then
                    MsgBox("Insertion Reussie", MsgBoxStyle.Information, "Parametrage des Societes Comptables")
                    DataListeSchema.Rows.Clear()
                End If
            End If
        Catch ex As Exception
            MsgBox("Message Systeme :" & ex.Message, MsgBoxStyle.Information, "Societes Comptables")
        End Try
    End Sub
    Private Sub BT_Quit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Quit.Click
        Me.Close()
    End Sub
    Private Sub BT_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Save.Click
        EnregistrerLeSchema()
        AfficheSchemasConso()
    End Sub

    Private Sub BT_DelRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_DelRow.Click
        Dim first As Integer
        Dim last As Integer
        first = DataListeSchema.Rows.GetFirstRow(DataGridViewElementStates.Displayed)
        last = DataListeSchema.Rows.GetLastRow(DataGridViewElementStates.Displayed)
        If last >= 0 Then
            If last - first >= 0 Then
                DataListeSchema.Rows.RemoveAt(DataListeSchema.CurrentRow.Index)
            End If
        End If
    End Sub
    Private Sub BT_ADD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_ADD.Click
        DataListeSchema.Rows.Add()
    End Sub

    Private Sub Parametrage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim boll As Boolean
        boll = Connected()
        AfficheSchemasConso()
        Me.WindowState = FormWindowState.Maximized
        GroupBox4.Width = Me.Width
    End Sub

    Private Sub BT_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Delete.Click
        Delete_DataListeSch()

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeIntegrer.CellContentClick

    End Sub

    Private Sub DataListeSchema_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeSchema.CellClick
        Dim NameFormat() As String
        Try
            If e.RowIndex >= 0 Then
                If DataListeSchema.Columns(e.ColumnIndex).Name = "Rep" Then
                    FolderRepListeFile.Filter = "Fichier mae (*.mae)|*.mae"
                    FolderRepListeFile.Title = "Repertoire des Fichiers Comptables"
                    If FolderRepListeFile.ShowDialog = Windows.Forms.DialogResult.OK Then
                        NameFormat = Split(Trim(FolderRepListeFile.FileName), "\")
                        DataListeSchema.Rows(e.RowIndex).Cells("Fichier").Value = FolderRepListeFile.FileName
                        DataListeSchema.Rows(e.RowIndex).Cells("Base").Value = NameFormat(UBound(NameFormat))
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataListeSchema_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataListeSchema.CellContentClick

    End Sub
End Class