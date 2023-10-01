Imports System.Data.OleDb
Imports System.IO
Public Class Frm_Clientspecifique
    Private Sub AfficheSchemasConso()
        Try
            Dim i As Integer
            Dim OleAdaptaterschema As OleDbDataAdapter
            Dim OleSchemaDataset As DataSet
            Dim OledatableSchema As DataTable
            DataListeIntegrer.Rows.Clear()

            OleAdaptaterschema = New OleDbDataAdapter("select * from CLIENTS", OleConnenection)
            OleSchemaDataset = New DataSet
            OleAdaptaterschema.Fill(OleSchemaDataset)
            OledatableSchema = OleSchemaDataset.Tables(0)
            DataListeIntegrer.RowCount = OledatableSchema.Rows.Count
            For i = 0 To OledatableSchema.Rows.Count - 1
                DataListeIntegrer.Rows(i).Cells("Catego1").Value = OledatableSchema.Rows(i).Item("Plant")
                DataListeIntegrer.Rows(i).Cells("Intitu1").Value = OledatableSchema.Rows(i).Item("Collectif")
                DataListeIntegrer.Rows(i).Cells("Caption1").Value = OledatableSchema.Rows(i).Item("Caption")
            Next i
        Catch ex As Exception
            MsgBox("Message Systeme: " & ex.Message, MsgBoxStyle.Information, "Plan Auxiliaire")
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
                OleAdaptaterDelete = New OleDbDataAdapter("select * from CLIENTS where Plant='" & DataListeIntegrer.Rows(i).Cells("Catego1").Value & "'", OleConnenection)
                OleDeleteDataset = New DataSet
                OleAdaptaterDelete.Fill(OleDeleteDataset)
                OledatableDelete = OleDeleteDataset.Tables(0)
                If OledatableDelete.Rows.Count <> 0 Then
                    DelFile = "Delete From CLIENTS where Plant='" & DataListeIntegrer.Rows(i).Cells("Catego1").Value & "'"
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
                    OleAdaptaterEnreg = New OleDbDataAdapter("select * From CLIENTS WHERE Plant='" & DataListeSchema.Rows(n).Cells("Catego").Value & "'", OleConnenection)
                    OleEnregDataset = New DataSet
                    OleAdaptaterEnreg.Fill(OleEnregDataset)
                    OledatableEnreg = OleEnregDataset.Tables(0)
                    If OledatableEnreg.Rows.Count <> 0 Then
                        MsgBox("Ce Plan Auxiliaire Existe déja!", MsgBoxStyle.Information, "Creation Plan Auxiliaire Comptable")
                    Else
                        If Trim(DataListeSchema.Rows(n).Cells("Catego").Value) <> "" Then
                            Insertion = "Insert Into CLIENTS (Plant,Collectif,Caption) VALUES ('" & DataListeSchema.Rows(n).Cells("Catego").Value & "','" & DataListeSchema.Rows(n).Cells("Intitu").Value & "','" & DataListeSchema.Rows(n).Cells("Caption").Value & "')"
                            OleCommandEnreg = New OleDbCommand(Insertion)
                            OleCommandEnreg.Connection = OleConnenection
                            OleCommandEnreg.ExecuteNonQuery()
                            Insert = True
                        End If
                    End If
                Next n
                If Insert = True Then
                    MsgBox("Insertion Reussie", MsgBoxStyle.Information, "Parametrage des Plan Auxiliaire")
                    DataListeSchema.Rows.Clear()
                End If
            End If
        Catch ex As Exception
            MsgBox("Message Systeme :" & ex.Message, MsgBoxStyle.Information, "Plan Auxiliaire")
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

    Private Sub Frm_Clientspecifique_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim boll As Boolean
        boll = Connected()
        AfficheSchemasConso()
        Me.WindowState = FormWindowState.Maximized
        GroupBox4.Width = Me.Width
    End Sub

    Private Sub BT_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BT_Delete.Click
        Delete_DataListeSch()

    End Sub
End Class