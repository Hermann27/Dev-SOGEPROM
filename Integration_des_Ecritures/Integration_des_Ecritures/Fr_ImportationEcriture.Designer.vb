<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fr_ImportationEcriture
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Fr_ImportationEcriture))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtFormat = New System.Windows.Forms.TextBox
        Me.GroupBox13 = New System.Windows.Forms.GroupBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtChemin = New System.Windows.Forms.TextBox
        Me.GroupBox12 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.GroupBox11 = New System.Windows.Forms.GroupBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.DataListeIntegrer = New System.Windows.Forms.DataGridView
        Me.NomFormat = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FichierExport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Dossier = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Valider = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Chemin = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CheminExport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.Datagridaffiche = New System.Windows.Forms.DataGridView
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.BT_integrer = New System.Windows.Forms.Button
        Me.BT_Quitter = New System.Windows.Forms.Button
        Me.BT_Apercue = New System.Windows.Forms.Button
        Me.FileSearched = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileXml = New System.Windows.Forms.SaveFileDialog
        Me.FileSearchedXml = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        CType(Me.DataListeIntegrer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.Datagridaffiche, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(845, 576)
        Me.SplitContainer1.SplitterDistance = 223
        Me.SplitContainer1.TabIndex = 11
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.DataListeIntegrer)
        Me.SplitContainer3.Size = New System.Drawing.Size(845, 223)
        Me.SplitContainer3.SplitterDistance = 324
        Me.SplitContainer3.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TxtFormat)
        Me.GroupBox1.Controls.Add(Me.GroupBox13)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TxtChemin)
        Me.GroupBox1.Controls.Add(Me.GroupBox12)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.GroupBox9)
        Me.GroupBox1.Controls.Add(Me.GroupBox10)
        Me.GroupBox1.Controls.Add(Me.GroupBox11)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(324, 223)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Fichier en Cours"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(37, 198)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(0, 13)
        Me.Label5.TabIndex = 42
        '
        'TxtFormat
        '
        Me.TxtFormat.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFormat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFormat.Cursor = System.Windows.Forms.Cursors.Default
        Me.TxtFormat.Enabled = False
        Me.TxtFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormat.Location = New System.Drawing.Point(88, 53)
        Me.TxtFormat.Name = "TxtFormat"
        Me.TxtFormat.Size = New System.Drawing.Size(228, 15)
        Me.TxtFormat.TabIndex = 1
        Me.TxtFormat.WordWrap = False
        '
        'GroupBox13
        '
        Me.GroupBox13.Location = New System.Drawing.Point(88, 44)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(230, 27)
        Me.GroupBox13.TabIndex = 41
        Me.GroupBox13.TabStop = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(87, 159)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(112, 20)
        Me.DateTimePicker1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-2, 163)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Date de Saisie"
        '
        'TxtChemin
        '
        Me.TxtChemin.BackColor = System.Drawing.SystemColors.Window
        Me.TxtChemin.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtChemin.Cursor = System.Windows.Forms.Cursors.Default
        Me.TxtChemin.Enabled = False
        Me.TxtChemin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChemin.Location = New System.Drawing.Point(88, 24)
        Me.TxtChemin.Name = "TxtChemin"
        Me.TxtChemin.Size = New System.Drawing.Size(228, 15)
        Me.TxtChemin.TabIndex = 0
        Me.TxtChemin.WordWrap = False
        '
        'GroupBox12
        '
        Me.GroupBox12.Location = New System.Drawing.Point(88, 15)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(230, 27)
        Me.GroupBox12.TabIndex = 36
        Me.GroupBox12.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(229, 135)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(0, 18)
        Me.Label8.TabIndex = 34
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(89, 80)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(115, 13)
        Me.TextBox1.TabIndex = 28
        Me.TextBox1.WordWrap = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(-2, 135)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Solde"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(-2, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Montant Credit"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-2, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Montant Debit"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(-2, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Format d'import"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(-1, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Fichier à importer"
        '
        'GroupBox9
        '
        Me.GroupBox9.Location = New System.Drawing.Point(89, 71)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(116, 25)
        Me.GroupBox9.TabIndex = 31
        Me.GroupBox9.TabStop = False
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.TextBox2)
        Me.GroupBox10.Location = New System.Drawing.Point(87, 98)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(116, 25)
        Me.GroupBox10.TabIndex = 32
        Me.GroupBox10.TabStop = False
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox2.Enabled = False
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(0, 9)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(115, 13)
        Me.TextBox2.TabIndex = 29
        Me.TextBox2.WordWrap = False
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.TextBox3)
        Me.GroupBox11.Location = New System.Drawing.Point(87, 125)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(116, 25)
        Me.GroupBox11.TabIndex = 33
        Me.GroupBox11.TabStop = False
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(0, 9)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(115, 13)
        Me.TextBox3.TabIndex = 29
        Me.TextBox3.WordWrap = False
        '
        'DataListeIntegrer
        '
        Me.DataListeIntegrer.AllowUserToAddRows = False
        Me.DataListeIntegrer.AllowUserToDeleteRows = False
        Me.DataListeIntegrer.AllowUserToOrderColumns = True
        Me.DataListeIntegrer.AllowUserToResizeRows = False
        Me.DataListeIntegrer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataListeIntegrer.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DataListeIntegrer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataListeIntegrer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NomFormat, Me.FichierExport, Me.Dossier, Me.Valider, Me.Chemin, Me.CheminExport})
        Me.DataListeIntegrer.Cursor = System.Windows.Forms.Cursors.Default
        Me.DataListeIntegrer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataListeIntegrer.Location = New System.Drawing.Point(0, 0)
        Me.DataListeIntegrer.MultiSelect = False
        Me.DataListeIntegrer.Name = "DataListeIntegrer"
        Me.DataListeIntegrer.RowHeadersVisible = False
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.DataListeIntegrer.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataListeIntegrer.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight
        Me.DataListeIntegrer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataListeIntegrer.Size = New System.Drawing.Size(517, 223)
        Me.DataListeIntegrer.TabIndex = 8
        '
        'NomFormat
        '
        Me.NomFormat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.NullValue = Nothing
        Me.NomFormat.DefaultCellStyle = DataGridViewCellStyle1
        Me.NomFormat.HeaderText = "Nom du Fichier Format"
        Me.NomFormat.Name = "NomFormat"
        Me.NomFormat.ReadOnly = True
        Me.NomFormat.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NomFormat.Width = 150
        '
        'FichierExport
        '
        Me.FichierExport.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FichierExport.DefaultCellStyle = DataGridViewCellStyle2
        Me.FichierExport.HeaderText = "Nom du Fichier à Importer"
        Me.FichierExport.Name = "FichierExport"
        Me.FichierExport.ReadOnly = True
        Me.FichierExport.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FichierExport.Width = 230
        '
        'Dossier
        '
        Me.Dossier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Dossier.HeaderText = "Dossier"
        Me.Dossier.Name = "Dossier"
        '
        'Valider
        '
        Me.Valider.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Valider.HeaderText = "Valider"
        Me.Valider.Name = "Valider"
        Me.Valider.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Valider.ToolTipText = "Valider les Fichiers à exporter"
        Me.Valider.Width = 50
        '
        'Chemin
        '
        Me.Chemin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.Format = "N0"
        Me.Chemin.DefaultCellStyle = DataGridViewCellStyle3
        Me.Chemin.FillWeight = 40.0!
        Me.Chemin.HeaderText = "Chemin d'acces du Fichier Format"
        Me.Chemin.Name = "Chemin"
        Me.Chemin.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Chemin.Visible = False
        '
        'CheminExport
        '
        Me.CheminExport.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CheminExport.HeaderText = "Chemin d'acces du Fichier Export"
        Me.CheminExport.Name = "CheminExport"
        Me.CheminExport.ReadOnly = True
        Me.CheminExport.Visible = False
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.Datagridaffiche)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ProgressBar1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox6)
        Me.SplitContainer2.Panel2.Controls.Add(Me.BT_integrer)
        Me.SplitContainer2.Panel2.Controls.Add(Me.BT_Quitter)
        Me.SplitContainer2.Panel2.Controls.Add(Me.BT_Apercue)
        Me.SplitContainer2.Size = New System.Drawing.Size(845, 349)
        Me.SplitContainer2.SplitterDistance = 301
        Me.SplitContainer2.TabIndex = 0
        '
        'Datagridaffiche
        '
        Me.Datagridaffiche.AllowUserToAddRows = False
        Me.Datagridaffiche.AllowUserToDeleteRows = False
        Me.Datagridaffiche.AllowUserToOrderColumns = True
        Me.Datagridaffiche.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Datagridaffiche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Datagridaffiche.Location = New System.Drawing.Point(0, 0)
        Me.Datagridaffiche.Name = "Datagridaffiche"
        Me.Datagridaffiche.RowHeadersVisible = False
        Me.Datagridaffiche.Size = New System.Drawing.Size(845, 282)
        Me.Datagridaffiche.TabIndex = 3
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 282)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(845, 19)
        Me.ProgressBar1.Step = 0
        Me.ProgressBar1.TabIndex = 3
        '
        'BT_integrer
        '
        Me.BT_integrer.Image = Global.Integration_des_Ecritures.My.Resources.Resources.applications_161
        Me.BT_integrer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_integrer.Location = New System.Drawing.Point(529, 15)
        Me.BT_integrer.Name = "BT_integrer"
        Me.BT_integrer.Size = New System.Drawing.Size(65, 23)
        Me.BT_integrer.TabIndex = 5
        Me.BT_integrer.Text = "&Integrer"
        Me.BT_integrer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_integrer.UseVisualStyleBackColor = True
        '
        'BT_Quitter
        '
        Me.BT_Quitter.Image = Global.Integration_des_Ecritures.My.Resources.Resources.image034
        Me.BT_Quitter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_Quitter.Location = New System.Drawing.Point(702, 15)
        Me.BT_Quitter.Name = "BT_Quitter"
        Me.BT_Quitter.Size = New System.Drawing.Size(68, 23)
        Me.BT_Quitter.TabIndex = 2
        Me.BT_Quitter.Text = "&Quitter"
        Me.BT_Quitter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_Quitter.UseVisualStyleBackColor = True
        '
        'BT_Apercue
        '
        Me.BT_Apercue.Image = Global.Integration_des_Ecritures.My.Resources.Resources.arrowforward_161
        Me.BT_Apercue.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BT_Apercue.Location = New System.Drawing.Point(371, 15)
        Me.BT_Apercue.Name = "BT_Apercue"
        Me.BT_Apercue.Size = New System.Drawing.Size(65, 23)
        Me.BT_Apercue.TabIndex = 0
        Me.BT_Apercue.Text = "&Aperçu"
        Me.BT_Apercue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BT_Apercue.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox6.Location = New System.Drawing.Point(-1, 1)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(102, 9)
        Me.GroupBox6.TabIndex = 6
        Me.GroupBox6.TabStop = False
        '
        'Fr_ImportationEcriture
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(845, 576)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Fr_ImportationEcriture"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Integration des Ecritures Comptables"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        CType(Me.DataListeIntegrer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.Datagridaffiche, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Datagridaffiche As System.Windows.Forms.DataGridView
    Friend WithEvents FileSearched As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BT_Quitter As System.Windows.Forms.Button
    Friend WithEvents BT_Apercue As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents SaveFileXml As System.Windows.Forms.SaveFileDialog
    Friend WithEvents FileSearchedXml As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BT_integrer As System.Windows.Forms.Button
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtFormat As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtChemin As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents DataListeIntegrer As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents NomFormat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FichierExport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dossier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Valider As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Chemin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CheminExport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox

End Class
