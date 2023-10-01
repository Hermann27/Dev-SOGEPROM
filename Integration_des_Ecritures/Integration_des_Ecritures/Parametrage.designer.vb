<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Parametrage
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Parametrage))
        Me.FileSearched = New System.Windows.Forms.OpenFileDialog
        Me.FolderRepListeFile = New System.Windows.Forms.OpenFileDialog
        Me.BT_Save = New System.Windows.Forms.Button
        Me.BT_Quit = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.BT_Delete = New System.Windows.Forms.Button
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.DataListeSchema = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BT_ADD = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BT_DelRow = New System.Windows.Forms.Button
        Me.DataListeIntegrer = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Societe = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Fichier = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Rep = New System.Windows.Forms.DataGridViewButtonColumn
        Me.Base = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Utilisateur = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Passe = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Plan = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Plan2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Server = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.User = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Pasword = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Societe1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Base1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Fichier1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Utilisateur1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Passe1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Plan1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Plan21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Server1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.User1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Pasword1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Supprimer = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataListeSchema, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataListeIntegrer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BT_Save
        '
        Me.BT_Save.Image = Global.Integration_des_Ecritures.My.Resources.Resources.save_16
        Me.BT_Save.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_Save.Location = New System.Drawing.Point(668, 11)
        Me.BT_Save.Name = "BT_Save"
        Me.BT_Save.Size = New System.Drawing.Size(79, 23)
        Me.BT_Save.TabIndex = 1
        Me.BT_Save.Text = "&Enregistrer"
        Me.BT_Save.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_Save.UseVisualStyleBackColor = True
        '
        'BT_Quit
        '
        Me.BT_Quit.Image = Global.Integration_des_Ecritures.My.Resources.Resources.image034
        Me.BT_Quit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_Quit.Location = New System.Drawing.Point(451, 11)
        Me.BT_Quit.Name = "BT_Quit"
        Me.BT_Quit.Size = New System.Drawing.Size(79, 23)
        Me.BT_Quit.TabIndex = 2
        Me.BT_Quit.Text = "&Quitter"
        Me.BT_Quit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_Quit.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox4.Location = New System.Drawing.Point(3, -2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(400, 9)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        '
        'BT_Delete
        '
        Me.BT_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BT_Delete.Image = Global.Integration_des_Ecritures.My.Resources.Resources.criticalind_status
        Me.BT_Delete.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_Delete.Location = New System.Drawing.Point(561, 12)
        Me.BT_Delete.Name = "BT_Delete"
        Me.BT_Delete.Size = New System.Drawing.Size(75, 22)
        Me.BT_Delete.TabIndex = 4
        Me.BT_Delete.Text = "Supprimer"
        Me.BT_Delete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_Delete.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataListeSchema)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Panel2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataListeIntegrer)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer2.Size = New System.Drawing.Size(1026, 545)
        Me.SplitContainer2.SplitterDistance = 260
        Me.SplitContainer2.TabIndex = 0
        '
        'DataListeSchema
        '
        Me.DataListeSchema.AllowUserToAddRows = False
        Me.DataListeSchema.AllowUserToDeleteRows = False
        Me.DataListeSchema.AllowUserToOrderColumns = True
        Me.DataListeSchema.AllowUserToResizeRows = False
        Me.DataListeSchema.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataListeSchema.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataListeSchema.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Societe, Me.Fichier, Me.Rep, Me.Base, Me.Utilisateur, Me.Passe, Me.Plan, Me.Plan2, Me.Server, Me.User, Me.Pasword})
        Me.DataListeSchema.Cursor = System.Windows.Forms.Cursors.Default
        Me.DataListeSchema.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataListeSchema.Location = New System.Drawing.Point(0, 30)
        Me.DataListeSchema.MultiSelect = False
        Me.DataListeSchema.Name = "DataListeSchema"
        Me.DataListeSchema.RowHeadersVisible = False
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.DataListeSchema.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataListeSchema.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.DataListeSchema.Size = New System.Drawing.Size(1026, 230)
        Me.DataListeSchema.TabIndex = 44
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1026, 30)
        Me.Panel2.TabIndex = 43
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(966, 30)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BT_ADD)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox2.Location = New System.Drawing.Point(966, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(29, 30)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'BT_ADD
        '
        Me.BT_ADD.Image = Global.Integration_des_Ecritures.My.Resources.Resources.add_16
        Me.BT_ADD.Location = New System.Drawing.Point(2, 7)
        Me.BT_ADD.Name = "BT_ADD"
        Me.BT_ADD.Size = New System.Drawing.Size(22, 20)
        Me.BT_ADD.TabIndex = 3
        Me.BT_ADD.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BT_DelRow)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox1.Location = New System.Drawing.Point(995, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(31, 30)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'BT_DelRow
        '
        Me.BT_DelRow.Image = Global.Integration_des_Ecritures.My.Resources.Resources.delete_16
        Me.BT_DelRow.Location = New System.Drawing.Point(3, 7)
        Me.BT_DelRow.Name = "BT_DelRow"
        Me.BT_DelRow.Size = New System.Drawing.Size(23, 20)
        Me.BT_DelRow.TabIndex = 2
        Me.BT_DelRow.UseVisualStyleBackColor = True
        '
        'DataListeIntegrer
        '
        Me.DataListeIntegrer.AllowUserToAddRows = False
        Me.DataListeIntegrer.AllowUserToDeleteRows = False
        Me.DataListeIntegrer.AllowUserToOrderColumns = True
        Me.DataListeIntegrer.AllowUserToResizeRows = False
        Me.DataListeIntegrer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataListeIntegrer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataListeIntegrer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Societe1, Me.Base1, Me.Fichier1, Me.Utilisateur1, Me.Passe1, Me.Plan1, Me.Plan21, Me.Server1, Me.User1, Me.Pasword1, Me.Supprimer})
        Me.DataListeIntegrer.Cursor = System.Windows.Forms.Cursors.Default
        Me.DataListeIntegrer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataListeIntegrer.Location = New System.Drawing.Point(0, 20)
        Me.DataListeIntegrer.MultiSelect = False
        Me.DataListeIntegrer.Name = "DataListeIntegrer"
        Me.DataListeIntegrer.RowHeadersVisible = False
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.DataListeIntegrer.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataListeIntegrer.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.DataListeIntegrer.Size = New System.Drawing.Size(1026, 261)
        Me.DataListeIntegrer.TabIndex = 45
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1026, 20)
        Me.Panel1.TabIndex = 9
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BT_Delete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BT_Quit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BT_Save)
        Me.SplitContainer1.Size = New System.Drawing.Size(1026, 586)
        Me.SplitContainer1.SplitterDistance = 545
        Me.SplitContainer1.TabIndex = 0
        '
        'Societe
        '
        Me.Societe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Format = "N0"
        Me.Societe.DefaultCellStyle = DataGridViewCellStyle1
        Me.Societe.FillWeight = 69.7006!
        Me.Societe.HeaderText = "Societe GPS"
        Me.Societe.Name = "Societe"
        Me.Societe.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Fichier
        '
        Me.Fichier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Fichier.FillWeight = 69.7006!
        Me.Fichier.HeaderText = "Fichier Comptable"
        Me.Fichier.Name = "Fichier"
        Me.Fichier.ReadOnly = True
        '
        'Rep
        '
        Me.Rep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Rep.HeaderText = "Rep"
        Me.Rep.Name = "Rep"
        Me.Rep.Width = 30
        '
        'Base
        '
        Me.Base.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Base.FillWeight = 69.7006!
        Me.Base.HeaderText = "Base Comptable"
        Me.Base.Name = "Base"
        Me.Base.ReadOnly = True
        '
        'Utilisateur
        '
        Me.Utilisateur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Utilisateur.FillWeight = 69.7006!
        Me.Utilisateur.HeaderText = "Utilisateur"
        Me.Utilisateur.Name = "Utilisateur"
        Me.Utilisateur.Width = 90
        '
        'Passe
        '
        Me.Passe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Passe.FillWeight = 69.7006!
        Me.Passe.HeaderText = "Mot de Passe"
        Me.Passe.Name = "Passe"
        Me.Passe.Width = 110
        '
        'Plan
        '
        Me.Plan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Plan.HeaderText = "Plan Analytique"
        Me.Plan.Name = "Plan"
        Me.Plan.Width = 120
        '
        'Plan2
        '
        Me.Plan2.HeaderText = "Plan Analytique 2"
        Me.Plan2.Name = "Plan2"
        '
        'Server
        '
        Me.Server.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Server.HeaderText = "Server SQL"
        Me.Server.Name = "Server"
        '
        'User
        '
        Me.User.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.User.HeaderText = "Utilisateur"
        Me.User.Name = "User"
        Me.User.Width = 60
        '
        'Pasword
        '
        Me.Pasword.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Pasword.HeaderText = "Mot de Passe"
        Me.Pasword.Name = "Pasword"
        '
        'Societe1
        '
        Me.Societe1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.Format = "N0"
        Me.Societe1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Societe1.HeaderText = "Societe GPS"
        Me.Societe1.Name = "Societe1"
        Me.Societe1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Base1
        '
        Me.Base1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Base1.HeaderText = "Base Comptable"
        Me.Base1.Name = "Base1"
        Me.Base1.ReadOnly = True
        '
        'Fichier1
        '
        Me.Fichier1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Fichier1.HeaderText = "Fichier Comptable"
        Me.Fichier1.Name = "Fichier1"
        Me.Fichier1.ReadOnly = True
        '
        'Utilisateur1
        '
        Me.Utilisateur1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Utilisateur1.HeaderText = "Utilisateur"
        Me.Utilisateur1.Name = "Utilisateur1"
        '
        'Passe1
        '
        Me.Passe1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Passe1.HeaderText = "Mot de Passe"
        Me.Passe1.Name = "Passe1"
        Me.Passe1.Width = 110
        '
        'Plan1
        '
        Me.Plan1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Plan1.HeaderText = "Plan Analytique"
        Me.Plan1.Name = "Plan1"
        '
        'Plan21
        '
        Me.Plan21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Plan21.HeaderText = "Plan Analytique 2"
        Me.Plan21.Name = "Plan21"
        Me.Plan21.Width = 95
        '
        'Server1
        '
        Me.Server1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Server1.HeaderText = "Server SQL"
        Me.Server1.Name = "Server1"
        Me.Server1.ReadOnly = True
        Me.Server1.Width = 80
        '
        'User1
        '
        Me.User1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.User1.HeaderText = "Utilisateur"
        Me.User1.Name = "User1"
        Me.User1.ReadOnly = True
        Me.User1.Width = 60
        '
        'Pasword1
        '
        Me.Pasword1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Pasword1.HeaderText = "Mot de Passe"
        Me.Pasword1.Name = "Pasword1"
        Me.Pasword1.ReadOnly = True
        '
        'Supprimer
        '
        Me.Supprimer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Supprimer.HeaderText = "Supprimer"
        Me.Supprimer.Name = "Supprimer"
        Me.Supprimer.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Supprimer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Supprimer.Width = 60
        '
        'Parametrage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(1026, 586)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Parametrage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Parametrage des Sociétés Comptables"
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataListeSchema, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataListeIntegrer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FileSearched As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FolderRepListeFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BT_Save As System.Windows.Forms.Button
    Friend WithEvents BT_Quit As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents BT_Delete As System.Windows.Forms.Button
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataListeSchema As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BT_ADD As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BT_DelRow As System.Windows.Forms.Button
    Friend WithEvents DataListeIntegrer As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Societe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fichier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rep As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Base As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Utilisateur As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Passe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Plan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Plan2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Server As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents User As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pasword As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Societe1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Base1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fichier1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Utilisateur1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Passe1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Plan1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Plan21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Server1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents User1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pasword1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supprimer As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
