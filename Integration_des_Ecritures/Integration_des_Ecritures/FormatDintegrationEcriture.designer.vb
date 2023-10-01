<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormatDintegrationEcriture
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormatDintegrationEcriture))
        Me.SaveFileXml = New System.Windows.Forms.SaveFileDialog
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.DataDispo = New System.Windows.Forms.DataGridView
        Me.ColDispo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LibelleDispo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Positio = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Longueur = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BT_New = New System.Windows.Forms.Button
        Me.BT_DelForm = New System.Windows.Forms.Button
        Me.BT_SaveFormat = New System.Windows.Forms.Button
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.BT_Down = New System.Windows.Forms.Button
        Me.BT_DelDispo = New System.Windows.Forms.Button
        Me.BT_UP = New System.Windows.Forms.Button
        Me.BT_DelSelt = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataSelect = New System.Windows.Forms.DataGridView
        Me.Colonne = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Positions = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Info = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Defaut = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.ValeurDefaut = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Libelle = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.NumUpDown = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.Txt_Chemin = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Cmb_Format = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Cmb_Del = New System.Windows.Forms.ComboBox
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DataDispo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.NumUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(783, 355)
        Me.Panel1.TabIndex = 47
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(783, 355)
        Me.SplitContainer1.SplitterDistance = 191
        Me.SplitContainer1.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DataDispo)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(191, 355)
        Me.GroupBox3.TabIndex = 47
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Colonnes Disponibles"
        '
        'DataDispo
        '
        Me.DataDispo.AllowUserToAddRows = False
        Me.DataDispo.AllowUserToDeleteRows = False
        Me.DataDispo.AllowUserToResizeRows = False
        Me.DataDispo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataDispo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColDispo, Me.LibelleDispo, Me.Positio, Me.Longueur})
        Me.DataDispo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DataDispo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataDispo.Location = New System.Drawing.Point(3, 16)
        Me.DataDispo.MultiSelect = False
        Me.DataDispo.Name = "DataDispo"
        Me.DataDispo.RowHeadersVisible = False
        Me.DataDispo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataDispo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataDispo.Size = New System.Drawing.Size(185, 336)
        Me.DataDispo.TabIndex = 10
        '
        'ColDispo
        '
        Me.ColDispo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColDispo.HeaderText = "Colonnes Disponibles"
        Me.ColDispo.Name = "ColDispo"
        Me.ColDispo.ReadOnly = True
        '
        'LibelleDispo
        '
        Me.LibelleDispo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.LibelleDispo.HeaderText = "LibelleDispo"
        Me.LibelleDispo.Name = "LibelleDispo"
        Me.LibelleDispo.ReadOnly = True
        Me.LibelleDispo.Visible = False
        '
        'Positio
        '
        DataGridViewCellStyle1.NullValue = "0"
        Me.Positio.DefaultCellStyle = DataGridViewCellStyle1
        Me.Positio.HeaderText = "Position"
        Me.Positio.Name = "Positio"
        Me.Positio.Visible = False
        '
        'Longueur
        '
        DataGridViewCellStyle2.NullValue = "0"
        Me.Longueur.DefaultCellStyle = DataGridViewCellStyle2
        Me.Longueur.HeaderText = "Longueur"
        Me.Longueur.Name = "Longueur"
        Me.Longueur.Visible = False
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer2.Size = New System.Drawing.Size(588, 355)
        Me.SplitContainer2.SplitterDistance = 89
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BT_New)
        Me.GroupBox1.Controls.Add(Me.BT_DelForm)
        Me.GroupBox1.Controls.Add(Me.BT_SaveFormat)
        Me.GroupBox1.Controls.Add(Me.GroupBox8)
        Me.GroupBox1.Controls.Add(Me.BT_Down)
        Me.GroupBox1.Controls.Add(Me.BT_DelDispo)
        Me.GroupBox1.Controls.Add(Me.BT_UP)
        Me.GroupBox1.Controls.Add(Me.BT_DelSelt)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(89, 355)
        Me.GroupBox1.TabIndex = 45
        Me.GroupBox1.TabStop = False
        '
        'BT_New
        '
        Me.BT_New.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_New.Image = Global.Integration_des_Ecritures.My.Resources.Resources.image019
        Me.BT_New.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_New.Location = New System.Drawing.Point(1, 206)
        Me.BT_New.Name = "BT_New"
        Me.BT_New.Size = New System.Drawing.Size(78, 24)
        Me.BT_New.TabIndex = 48
        Me.BT_New.Text = "&Nouveau"
        Me.BT_New.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_New.UseVisualStyleBackColor = True
        '
        'BT_DelForm
        '
        Me.BT_DelForm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_DelForm.Image = Global.Integration_des_Ecritures.My.Resources.Resources.delete_161
        Me.BT_DelForm.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_DelForm.Location = New System.Drawing.Point(0, 285)
        Me.BT_DelForm.Name = "BT_DelForm"
        Me.BT_DelForm.Size = New System.Drawing.Size(81, 23)
        Me.BT_DelForm.TabIndex = 2
        Me.BT_DelForm.Text = "&Supprimer"
        Me.BT_DelForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_DelForm.UseVisualStyleBackColor = True
        '
        'BT_SaveFormat
        '
        Me.BT_SaveFormat.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BT_SaveFormat.Image = Global.Integration_des_Ecritures.My.Resources.Resources.save_16
        Me.BT_SaveFormat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_SaveFormat.Location = New System.Drawing.Point(-1, 246)
        Me.BT_SaveFormat.Name = "BT_SaveFormat"
        Me.BT_SaveFormat.Size = New System.Drawing.Size(81, 23)
        Me.BT_SaveFormat.TabIndex = 0
        Me.BT_SaveFormat.Text = "&Ok"
        Me.BT_SaveFormat.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox8.Location = New System.Drawing.Point(3, 314)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(83, 38)
        Me.GroupBox8.TabIndex = 47
        Me.GroupBox8.TabStop = False
        '
        'BT_Down
        '
        Me.BT_Down.Image = Global.Integration_des_Ecritures.My.Resources.Resources.arrowdown_16
        Me.BT_Down.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_Down.Location = New System.Drawing.Point(-1, 165)
        Me.BT_Down.Name = "BT_Down"
        Me.BT_Down.Size = New System.Drawing.Size(80, 24)
        Me.BT_Down.TabIndex = 45
        Me.BT_Down.Text = "&Descendre"
        Me.BT_Down.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_Down.UseVisualStyleBackColor = True
        '
        'BT_DelDispo
        '
        Me.BT_DelDispo.Image = Global.Integration_des_Ecritures.My.Resources.Resources.arrowforward_16
        Me.BT_DelDispo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_DelDispo.Location = New System.Drawing.Point(-1, 50)
        Me.BT_DelDispo.Name = "BT_DelDispo"
        Me.BT_DelDispo.Size = New System.Drawing.Size(80, 23)
        Me.BT_DelDispo.TabIndex = 42
        Me.BT_DelDispo.Text = "&Ajouter"
        Me.BT_DelDispo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_DelDispo.UseVisualStyleBackColor = True
        '
        'BT_UP
        '
        Me.BT_UP.Image = Global.Integration_des_Ecritures.My.Resources.Resources.arrowup_16
        Me.BT_UP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_UP.Location = New System.Drawing.Point(0, 123)
        Me.BT_UP.Name = "BT_UP"
        Me.BT_UP.Size = New System.Drawing.Size(79, 23)
        Me.BT_UP.TabIndex = 44
        Me.BT_UP.Text = "&Monter"
        Me.BT_UP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_UP.UseVisualStyleBackColor = True
        '
        'BT_DelSelt
        '
        Me.BT_DelSelt.Image = Global.Integration_des_Ecritures.My.Resources.Resources.arrowback_16
        Me.BT_DelSelt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_DelSelt.Location = New System.Drawing.Point(-1, 86)
        Me.BT_DelSelt.Name = "BT_DelSelt"
        Me.BT_DelSelt.Size = New System.Drawing.Size(80, 22)
        Me.BT_DelSelt.TabIndex = 43
        Me.BT_DelSelt.Text = "&Supprimer"
        Me.BT_DelSelt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_DelSelt.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DataSelect)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 57)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(495, 298)
        Me.GroupBox2.TabIndex = 48
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Personnalisation de la Liste"
        '
        'DataSelect
        '
        Me.DataSelect.AllowUserToAddRows = False
        Me.DataSelect.AllowUserToDeleteRows = False
        Me.DataSelect.AllowUserToResizeRows = False
        Me.DataSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataSelect.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Colonne, Me.Positions, Me.Info, Me.Defaut, Me.ValeurDefaut, Me.Libelle})
        Me.DataSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.DataSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataSelect.EnableHeadersVisualStyles = False
        Me.DataSelect.Location = New System.Drawing.Point(3, 16)
        Me.DataSelect.MultiSelect = False
        Me.DataSelect.Name = "DataSelect"
        Me.DataSelect.RowHeadersVisible = False
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.DataSelect.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataSelect.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.DataSelect.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataSelect.Size = New System.Drawing.Size(489, 279)
        Me.DataSelect.TabIndex = 6
        '
        'Colonne
        '
        Me.Colonne.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.Colonne.DefaultCellStyle = DataGridViewCellStyle3
        Me.Colonne.HeaderText = "Colonnes Selectionnées"
        Me.Colonne.Name = "Colonne"
        Me.Colonne.ReadOnly = True
        '
        'Positions
        '
        Me.Positions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = "0"
        Me.Positions.DefaultCellStyle = DataGridViewCellStyle4
        Me.Positions.FillWeight = 40.0!
        Me.Positions.HeaderText = "Position"
        Me.Positions.Name = "Positions"
        Me.Positions.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Positions.Width = 60
        '
        'Info
        '
        Me.Info.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Info.HeaderText = "Info Libre"
        Me.Info.Name = "Info"
        Me.Info.ReadOnly = True
        Me.Info.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Info.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Info.Width = 90
        '
        'Defaut
        '
        Me.Defaut.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Defaut.HeaderText = "Defaut"
        Me.Defaut.Name = "Defaut"
        Me.Defaut.Width = 50
        '
        'ValeurDefaut
        '
        Me.ValeurDefaut.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ValeurDefaut.HeaderText = "Valeur par Defaut"
        Me.ValeurDefaut.Name = "ValeurDefaut"
        Me.ValeurDefaut.ReadOnly = True
        Me.ValeurDefaut.Width = 130
        '
        'Libelle
        '
        Me.Libelle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Libelle.HeaderText = "Libelle"
        Me.Libelle.Name = "Libelle"
        Me.Libelle.ReadOnly = True
        Me.Libelle.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.NumUpDown)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Txt_Chemin)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.GroupBox4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(495, 57)
        Me.Panel2.TabIndex = 0
        '
        'NumUpDown
        '
        Me.NumUpDown.Location = New System.Drawing.Point(94, 33)
        Me.NumUpDown.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumUpDown.Name = "NumUpDown"
        Me.NumUpDown.Size = New System.Drawing.Size(64, 20)
        Me.NumUpDown.TabIndex = 18
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(2, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Decalage entête"
        '
        'Txt_Chemin
        '
        Me.Txt_Chemin.BackColor = System.Drawing.SystemColors.Window
        Me.Txt_Chemin.Location = New System.Drawing.Point(250, 7)
        Me.Txt_Chemin.Name = "Txt_Chemin"
        Me.Txt_Chemin.ReadOnly = True
        Me.Txt_Chemin.Size = New System.Drawing.Size(245, 20)
        Me.Txt_Chemin.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Nom du Format"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Cmb_Format)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.Cmb_Del)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(495, 57)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        '
        'Cmb_Format
        '
        Me.Cmb_Format.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Cmb_Format.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Cmb_Format.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Cmb_Format.FormattingEnabled = True
        Me.Cmb_Format.Location = New System.Drawing.Point(94, 7)
        Me.Cmb_Format.Name = "Cmb_Format"
        Me.Cmb_Format.Size = New System.Drawing.Size(155, 21)
        Me.Cmb_Format.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(255, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Délimiteur de Champs"
        '
        'Cmb_Del
        '
        Me.Cmb_Del.FormattingEnabled = True
        Me.Cmb_Del.Items.AddRange(New Object() {"Tabulation", "Point Virgule"})
        Me.Cmb_Del.Location = New System.Drawing.Point(370, 33)
        Me.Cmb_Del.Name = "Cmb_Del"
        Me.Cmb_Del.Size = New System.Drawing.Size(100, 21)
        Me.Cmb_Del.TabIndex = 1
        Me.Cmb_Del.Text = "Tabulation"
        '
        'FormatDintegrationEcriture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(783, 355)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormatDintegrationEcriture"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Parametrage des Formats d'integration  <Ecritures Comptables>"
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.DataDispo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DataSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.NumUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SaveFileXml As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DataDispo As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BT_DelForm As System.Windows.Forms.Button
    Friend WithEvents BT_SaveFormat As System.Windows.Forms.Button
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents BT_Down As System.Windows.Forms.Button
    Friend WithEvents BT_DelDispo As System.Windows.Forms.Button
    Friend WithEvents BT_UP As System.Windows.Forms.Button
    Friend WithEvents BT_DelSelt As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DataSelect As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Txt_Chemin As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ColDispo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LibelleDispo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Positio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Longueur As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BT_New As System.Windows.Forms.Button
    Friend WithEvents NumUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Colonne As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Positions As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Info As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Defaut As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ValeurDefaut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Libelle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cmb_Del As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Cmb_Format As System.Windows.Forms.ComboBox
End Class
