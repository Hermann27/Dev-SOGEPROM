<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Ecriturelibre
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Ecriturelibre))
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Txtintragroupe = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.BT_Prec = New System.Windows.Forms.Button
        Me.BT_Suivant = New System.Windows.Forms.Button
        Me.DateFin = New System.Windows.Forms.ComboBox
        Me.DateDebut = New System.Windows.Forms.TextBox
        Me.Periode = New System.Windows.Forms.TextBox
        Me.CheckInfo = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.BT_Creer = New System.Windows.Forms.Button
        Me.BT_Update = New System.Windows.Forms.Button
        Me.BT_Del = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(2, 81)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Type de données"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(2, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Nom Sage"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Intitulé"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Txtintragroupe)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.BT_Prec)
        Me.GroupBox1.Controls.Add(Me.BT_Suivant)
        Me.GroupBox1.Controls.Add(Me.DateFin)
        Me.GroupBox1.Controls.Add(Me.DateDebut)
        Me.GroupBox1.Controls.Add(Me.Periode)
        Me.GroupBox1.Controls.Add(Me.CheckInfo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(2, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(343, 161)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Informations Libres"
        '
        'Txtintragroupe
        '
        Me.Txtintragroupe.Location = New System.Drawing.Point(91, 105)
        Me.Txtintragroupe.Name = "Txtintragroupe"
        Me.Txtintragroupe.Size = New System.Drawing.Size(219, 20)
        Me.Txtintragroupe.TabIndex = 33
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 26)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Intra-groupe" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Valeur à Vide"
        '
        'Button2
        '
        Me.Button2.Image = Global.Integration_des_Ecritures.My.Resources.Resources.image019
        Me.Button2.Location = New System.Drawing.Point(280, 138)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(31, 21)
        Me.Button2.TabIndex = 31
        Me.Button2.UseVisualStyleBackColor = True
        '
        'BT_Prec
        '
        Me.BT_Prec.Image = Global.Integration_des_Ecritures.My.Resources.Resources.arrowback_16
        Me.BT_Prec.Location = New System.Drawing.Point(206, 138)
        Me.BT_Prec.Name = "BT_Prec"
        Me.BT_Prec.Size = New System.Drawing.Size(31, 21)
        Me.BT_Prec.TabIndex = 30
        Me.BT_Prec.UseVisualStyleBackColor = True
        '
        'BT_Suivant
        '
        Me.BT_Suivant.Image = Global.Integration_des_Ecritures.My.Resources.Resources.arrowforward_16
        Me.BT_Suivant.Location = New System.Drawing.Point(243, 138)
        Me.BT_Suivant.Name = "BT_Suivant"
        Me.BT_Suivant.Size = New System.Drawing.Size(31, 21)
        Me.BT_Suivant.TabIndex = 29
        Me.BT_Suivant.UseVisualStyleBackColor = True
        '
        'DateFin
        '
        Me.DateFin.FormattingEnabled = True
        Me.DateFin.Items.AddRange(New Object() {"Chaine", "Numerique", "Date"})
        Me.DateFin.Location = New System.Drawing.Point(93, 78)
        Me.DateFin.Name = "DateFin"
        Me.DateFin.Size = New System.Drawing.Size(75, 21)
        Me.DateFin.TabIndex = 28
        '
        'DateDebut
        '
        Me.DateDebut.Location = New System.Drawing.Point(93, 52)
        Me.DateDebut.Name = "DateDebut"
        Me.DateDebut.Size = New System.Drawing.Size(217, 20)
        Me.DateDebut.TabIndex = 27
        '
        'Periode
        '
        Me.Periode.Location = New System.Drawing.Point(93, 18)
        Me.Periode.Name = "Periode"
        Me.Periode.Size = New System.Drawing.Size(217, 20)
        Me.Periode.TabIndex = 26
        '
        'CheckInfo
        '
        Me.CheckInfo.AutoSize = True
        Me.CheckInfo.Checked = True
        Me.CheckInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckInfo.Enabled = False
        Me.CheckInfo.Location = New System.Drawing.Point(107, 138)
        Me.CheckInfo.Name = "CheckInfo"
        Me.CheckInfo.Size = New System.Drawing.Size(70, 17)
        Me.CheckInfo.TabIndex = 25
        Me.CheckInfo.Text = "Info Libre"
        Me.CheckInfo.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(14, 165)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(62, 21)
        Me.Button1.TabIndex = 25
        Me.Button1.Text = "&Quitter"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BT_Creer
        '
        Me.BT_Creer.Location = New System.Drawing.Point(251, 165)
        Me.BT_Creer.Name = "BT_Creer"
        Me.BT_Creer.Size = New System.Drawing.Size(62, 21)
        Me.BT_Creer.TabIndex = 26
        Me.BT_Creer.Text = "&Creer"
        Me.BT_Creer.UseVisualStyleBackColor = True
        '
        'BT_Update
        '
        Me.BT_Update.Location = New System.Drawing.Point(177, 165)
        Me.BT_Update.Name = "BT_Update"
        Me.BT_Update.Size = New System.Drawing.Size(62, 21)
        Me.BT_Update.TabIndex = 27
        Me.BT_Update.Text = "&Modifier"
        Me.BT_Update.UseVisualStyleBackColor = True
        '
        'BT_Del
        '
        Me.BT_Del.Location = New System.Drawing.Point(101, 165)
        Me.BT_Del.Name = "BT_Del"
        Me.BT_Del.Size = New System.Drawing.Size(62, 21)
        Me.BT_Del.TabIndex = 28
        Me.BT_Del.Text = "&Supprimer"
        Me.BT_Del.UseVisualStyleBackColor = True
        '
        'Ecriturelibre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(347, 188)
        Me.Controls.Add(Me.BT_Del)
        Me.Controls.Add(Me.BT_Update)
        Me.Controls.Add(Me.BT_Creer)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Ecriturelibre"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gestion Informations libres <Ecritures>"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents BT_Creer As System.Windows.Forms.Button
    Friend WithEvents BT_Update As System.Windows.Forms.Button
    Friend WithEvents BT_Del As System.Windows.Forms.Button
    Friend WithEvents DateFin As System.Windows.Forms.ComboBox
    Friend WithEvents DateDebut As System.Windows.Forms.TextBox
    Friend WithEvents Periode As System.Windows.Forms.TextBox
    Friend WithEvents CheckInfo As System.Windows.Forms.CheckBox
    Friend WithEvents BT_Prec As System.Windows.Forms.Button
    Friend WithEvents BT_Suivant As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Txtintragroupe As System.Windows.Forms.TextBox
End Class
