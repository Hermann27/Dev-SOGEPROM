<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RepertoireImport
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RepertoireImport))
        Me.OpenFileFicCpta = New System.Windows.Forms.OpenFileDialog
        Me.FolderRepjournal = New System.Windows.Forms.FolderBrowserDialog
        Me.FolderRepsaving = New System.Windows.Forms.FolderBrowserDialog
        Me.OpenFileGesCom = New System.Windows.Forms.OpenFileDialog
        Me.Button2 = New System.Windows.Forms.Button
        Me.Btsupimp = New System.Windows.Forms.Button
        Me.FolderRepFact = New System.Windows.Forms.FolderBrowserDialog
        Me.FolderRepSave = New System.Windows.Forms.FolderBrowserDialog
        Me.OpenFileAccess = New System.Windows.Forms.OpenFileDialog
        Me.BT_FicJournal = New System.Windows.Forms.Button
        Me.BT_FicRep = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtFilejr = New System.Windows.Forms.TextBox
        Me.Txt_Rep = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Btcreerimp = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Textexport = New System.Windows.Forms.TextBox
        Me.Btexport = New System.Windows.Forms.Button
        Me.Btsupexp = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Btcreerexp = New System.Windows.Forms.Button
        Me.Txt_Sexport = New System.Windows.Forms.TextBox
        Me.Txt_Jexport = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Bt_Exportsave = New System.Windows.Forms.Button
        Me.Bt_export = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'FolderRepjournal
        '
        Me.FolderRepjournal.RootFolder = System.Environment.SpecialFolder.DesktopDirectory
        Me.FolderRepjournal.ShowNewFolderButton = False
        '
        'Button2
        '
        Me.Button2.Image = Global.Integration_des_Ecritures.My.Resources.Resources.image034
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.Location = New System.Drawing.Point(216, 81)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(62, 21)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "&Quitter"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Btsupimp
        '
        Me.Btsupimp.Image = Global.Integration_des_Ecritures.My.Resources.Resources.criticalind_status
        Me.Btsupimp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btsupimp.Location = New System.Drawing.Point(297, 81)
        Me.Btsupimp.Name = "Btsupimp"
        Me.Btsupimp.Size = New System.Drawing.Size(74, 21)
        Me.Btsupimp.TabIndex = 11
        Me.Btsupimp.Text = "&Supprimer"
        Me.Btsupimp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btsupimp.UseVisualStyleBackColor = True
        '
        'BT_FicJournal
        '
        Me.BT_FicJournal.Image = Global.Integration_des_Ecritures.My.Resources.Resources.foldeopen_16
        Me.BT_FicJournal.Location = New System.Drawing.Point(520, 21)
        Me.BT_FicJournal.Name = "BT_FicJournal"
        Me.BT_FicJournal.Size = New System.Drawing.Size(29, 20)
        Me.BT_FicJournal.TabIndex = 19
        Me.BT_FicJournal.UseVisualStyleBackColor = True
        '
        'BT_FicRep
        '
        Me.BT_FicRep.Image = Global.Integration_des_Ecritures.My.Resources.Resources.foldeopen_16
        Me.BT_FicRep.Location = New System.Drawing.Point(520, 52)
        Me.BT_FicRep.Name = "BT_FicRep"
        Me.BT_FicRep.Size = New System.Drawing.Size(29, 20)
        Me.BT_FicRep.TabIndex = 20
        Me.BT_FicRep.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(141, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Repertoire de Journalisation "
        '
        'TxtFilejr
        '
        Me.TxtFilejr.Location = New System.Drawing.Point(217, 22)
        Me.TxtFilejr.Name = "TxtFilejr"
        Me.TxtFilejr.Size = New System.Drawing.Size(301, 20)
        Me.TxtFilejr.TabIndex = 7
        '
        'Txt_Rep
        '
        Me.Txt_Rep.Location = New System.Drawing.Point(216, 52)
        Me.Txt_Rep.Name = "Txt_Rep"
        Me.Txt_Rep.Size = New System.Drawing.Size(301, 20)
        Me.Txt_Rep.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(5, 54)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(135, 13)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Repertoire de Sauvegarde "
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Btcreerimp)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Btsupimp)
        Me.GroupBox1.Controls.Add(Me.Txt_Rep)
        Me.GroupBox1.Controls.Add(Me.TxtFilejr)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.BT_FicRep)
        Me.GroupBox1.Controls.Add(Me.BT_FicJournal)
        Me.GroupBox1.Location = New System.Drawing.Point(-8, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(551, 124)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parametre des Importations"
        '
        'Btcreerimp
        '
        Me.Btcreerimp.Image = Global.Integration_des_Ecritures.My.Resources.Resources.save_162
        Me.Btcreerimp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btcreerimp.Location = New System.Drawing.Point(399, 81)
        Me.Btcreerimp.Name = "Btcreerimp"
        Me.Btcreerimp.Size = New System.Drawing.Size(67, 21)
        Me.Btcreerimp.TabIndex = 31
        Me.Btcreerimp.Text = "&Créer"
        Me.Btcreerimp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btcreerimp.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Textexport)
        Me.GroupBox2.Controls.Add(Me.Btexport)
        Me.GroupBox2.Controls.Add(Me.Btsupexp)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Btcreerexp)
        Me.GroupBox2.Controls.Add(Me.Txt_Sexport)
        Me.GroupBox2.Controls.Add(Me.Txt_Jexport)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Bt_Exportsave)
        Me.GroupBox2.Controls.Add(Me.Bt_export)
        Me.GroupBox2.Location = New System.Drawing.Point(0, 135)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(551, 147)
        Me.GroupBox2.TabIndex = 22
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Parametre des Exportations"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(-4, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Repertoire d'export "
        '
        'Textexport
        '
        Me.Textexport.Location = New System.Drawing.Point(207, 84)
        Me.Textexport.Name = "Textexport"
        Me.Textexport.Size = New System.Drawing.Size(301, 20)
        Me.Textexport.TabIndex = 32
        '
        'Btexport
        '
        Me.Btexport.Image = Global.Integration_des_Ecritures.My.Resources.Resources.foldeopen_16
        Me.Btexport.Location = New System.Drawing.Point(511, 84)
        Me.Btexport.Name = "Btexport"
        Me.Btexport.Size = New System.Drawing.Size(29, 20)
        Me.Btexport.TabIndex = 33
        Me.Btexport.UseVisualStyleBackColor = True
        '
        'Btsupexp
        '
        Me.Btsupexp.Image = Global.Integration_des_Ecritures.My.Resources.Resources.criticalind_status
        Me.Btsupexp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btsupexp.Location = New System.Drawing.Point(303, 116)
        Me.Btsupexp.Name = "Btsupexp"
        Me.Btsupexp.Size = New System.Drawing.Size(74, 21)
        Me.Btsupexp.TabIndex = 31
        Me.Btsupexp.Text = "&Supprimer"
        Me.Btsupexp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btsupexp.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Image = Global.Integration_des_Ecritures.My.Resources.Resources.image034
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.Location = New System.Drawing.Point(209, 116)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(62, 21)
        Me.Button3.TabIndex = 12
        Me.Button3.Text = "&Quitter"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-3, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Repertoire Archive"
        '
        'Btcreerexp
        '
        Me.Btcreerexp.Image = Global.Integration_des_Ecritures.My.Resources.Resources.save_162
        Me.Btcreerexp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btcreerexp.Location = New System.Drawing.Point(407, 116)
        Me.Btcreerexp.Name = "Btcreerexp"
        Me.Btcreerexp.Size = New System.Drawing.Size(62, 21)
        Me.Btcreerexp.TabIndex = 11
        Me.Btcreerexp.Text = "&Créer"
        Me.Btcreerexp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btcreerexp.UseVisualStyleBackColor = True
        '
        'Txt_Sexport
        '
        Me.Txt_Sexport.Location = New System.Drawing.Point(208, 53)
        Me.Txt_Sexport.Name = "Txt_Sexport"
        Me.Txt_Sexport.Size = New System.Drawing.Size(301, 20)
        Me.Txt_Sexport.TabIndex = 8
        '
        'Txt_Jexport
        '
        Me.Txt_Jexport.Location = New System.Drawing.Point(209, 23)
        Me.Txt_Jexport.Name = "Txt_Jexport"
        Me.Txt_Jexport.Size = New System.Drawing.Size(301, 20)
        Me.Txt_Jexport.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-3, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Repertoire de Journalisation "
        '
        'Bt_Exportsave
        '
        Me.Bt_Exportsave.Image = Global.Integration_des_Ecritures.My.Resources.Resources.foldeopen_16
        Me.Bt_Exportsave.Location = New System.Drawing.Point(512, 53)
        Me.Bt_Exportsave.Name = "Bt_Exportsave"
        Me.Bt_Exportsave.Size = New System.Drawing.Size(29, 20)
        Me.Bt_Exportsave.TabIndex = 20
        Me.Bt_Exportsave.UseVisualStyleBackColor = True
        '
        'Bt_export
        '
        Me.Bt_export.Image = Global.Integration_des_Ecritures.My.Resources.Resources.foldeopen_16
        Me.Bt_export.Location = New System.Drawing.Point(512, 22)
        Me.Bt_export.Name = "Bt_export"
        Me.Bt_export.Size = New System.Drawing.Size(29, 20)
        Me.Bt_export.TabIndex = 19
        Me.Bt_export.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(486, 116)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(53, 20)
        Me.Button1.TabIndex = 35
        Me.Button1.Text = "Modifier"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(488, 81)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(53, 20)
        Me.Button4.TabIndex = 36
        Me.Button4.Text = "Modifier"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'RepertoireImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 284)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "RepertoireImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Parametre des Repertoires de Sauvegarde et de Journalisation"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Btsupimp As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents OpenFileFicCpta As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderRepjournal As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents FolderRepsaving As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileGesCom As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderRepFact As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents FolderRepSave As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileAccess As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BT_FicJournal As System.Windows.Forms.Button
    Friend WithEvents BT_FicRep As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtFilejr As System.Windows.Forms.TextBox
    Friend WithEvents Txt_Rep As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Btcreerexp As System.Windows.Forms.Button
    Friend WithEvents Txt_Sexport As System.Windows.Forms.TextBox
    Friend WithEvents Txt_Jexport As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Bt_Exportsave As System.Windows.Forms.Button
    Friend WithEvents Bt_export As System.Windows.Forms.Button
    Friend WithEvents Btcreerimp As System.Windows.Forms.Button
    Friend WithEvents Btsupexp As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Textexport As System.Windows.Forms.TextBox
    Friend WithEvents Btexport As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
End Class
