Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Timers
Imports ExtractAnnotationFromDescription
Imports Protein_Importer
Imports Protein_Uploader
Imports ValidateFastaFile

Public Class frmCollectionEditor
    Inherits Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.CheckTransferButtonsEnabledStatus()

        'Add any initialization after the InitializeComponent() call

        m_CachedFileDescriptions = New Dictionary(Of String, KeyValuePair(Of String, String))

        ReadSettings()

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents pnlProgBar As Panel
    Friend WithEvents gbxSourceCollection As GroupBox
    Friend WithEvents cboAnnotationTypePicker As ComboBox
    Friend WithEvents lblAnnotationTypeFilter As Label
    Friend WithEvents pbxLiveSearchCancel As PictureBox
    Friend WithEvents lblSearchCount As Label
    Friend WithEvents cmdLoadProteins As Button
    Friend WithEvents cmdLoadFile As Button
    Friend WithEvents txtLiveSearch As TextBox
    Friend WithEvents cboCollectionPicker As ComboBox
    Friend WithEvents cboOrganismFilter As ComboBox
    Friend WithEvents lblOrganismFilter As Label
    Friend WithEvents lblCollectionPicker As Label
    Friend WithEvents pbxLiveSearchBkg As PictureBox
    Friend WithEvents lvwSource As ListView
    Friend WithEvents lblSourceMembers As Label
    Friend WithEvents colSrcName As ColumnHeader
    Friend WithEvents colSrcDesc As ColumnHeader
    Friend WithEvents cmdDestAdd As UIControls.ImageButton
    Friend WithEvents cmdDestRemove As UIControls.ImageButton
    Friend WithEvents cmdDestAddAll As UIControls.ImageButton
    Friend WithEvents cmdDestRemoveAll As UIControls.ImageButton
    Friend WithEvents gbxDestinationCollection As GroupBox
    Friend WithEvents lblCurrProteinCount As Label
    Friend WithEvents lvwDestination As ListView
    Friend WithEvents colName As ColumnHeader
    Friend WithEvents pnlProgBarLower As Panel
    Friend WithEvents pgbMain As ProgressBar
    Friend WithEvents pnlSource As Panel
    Friend WithEvents SourceDestSplit As Splitter
    Friend WithEvents pnlDest As Panel
    Friend WithEvents pnlProgBarUpper As Panel
    Friend WithEvents lblCurrentTask As Label
    Friend WithEvents mnuMainGUI As MainMenu
    Friend WithEvents mnuFile As MenuItem
    Friend WithEvents mnuHelp As MenuItem
    Friend WithEvents mnuTools As MenuItem
    Friend WithEvents mnuAdmin As MenuItem
    Friend WithEvents mnuFileExit As MenuItem
    Friend WithEvents mnuToolsNucToProt As MenuItem
    Friend WithEvents mnuToolsConvert As MenuItem
    Friend WithEvents mnuToolsConvertF2A As MenuItem
    Friend WithEvents mnuToolsConvertA2F As MenuItem
    Friend WithEvents mnuToolsFCheckup As MenuItem
    Friend WithEvents mnuToolsOptions As MenuItem
    Friend WithEvents mnuToolsCollectionEdit As MenuItem
    Friend WithEvents mnuToolsCompareDBs As MenuItem
    Friend WithEvents mnuToolsExtractFromFile As MenuItem
    Friend WithEvents mnuHelpAbout As MenuItem
    Friend WithEvents mnuAdminNameHashRefresh As MenuItem
    Friend WithEvents mnuToolsSep1 As MenuItem
    Friend WithEvents mnuAdminBatchUploadFiles As MenuItem
    Friend WithEvents mnuAdminUpdateSHA As MenuItem
    Friend WithEvents mnuAdminUpdateCollectionsArchive As MenuItem
    Friend WithEvents mnuAdminUpdateZeroedMasses As MenuItem
    Friend WithEvents mnuAdminTestingInterface As MenuItem
    Friend WithEvents mnuAdminFixArchivePaths As MenuItem
    Friend WithEvents mnuAdminAddSortingIndexes As MenuItem
    Friend WithEvents lblBatchProgress As Label
    Friend WithEvents cmdExportToFile As Button
    Friend WithEvents lblTargetServer As Label
    Friend WithEvents cmdSaveDestCollection As Button


    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCollectionEditor))
        Me.pnlProgBar = New Panel()
        Me.pnlProgBarUpper = New Panel()
        Me.lblBatchProgress = New Label()
        Me.lblCurrentTask = New Label()
        Me.pnlProgBarLower = New Panel()
        Me.pgbMain = New ProgressBar()
        Me.pnlSource = New Panel()
        Me.lblTargetServer = New Label()
        Me.cmdDestAdd = New UIControls.ImageButton()
        Me.cmdDestRemove = New UIControls.ImageButton()
        Me.cmdDestAddAll = New UIControls.ImageButton()
        Me.cmdDestRemoveAll = New UIControls.ImageButton()
        Me.gbxSourceCollection = New GroupBox()
        Me.cboAnnotationTypePicker = New ComboBox()
        Me.lblAnnotationTypeFilter = New Label()
        Me.pbxLiveSearchCancel = New PictureBox()
        Me.lblSearchCount = New Label()
        Me.cmdLoadProteins = New Button()
        Me.cmdLoadFile = New Button()
        Me.txtLiveSearch = New TextBox()
        Me.cboCollectionPicker = New ComboBox()
        Me.cboOrganismFilter = New ComboBox()
        Me.lblOrganismFilter = New Label()
        Me.lblCollectionPicker = New Label()
        Me.pbxLiveSearchBkg = New PictureBox()
        Me.lvwSource = New ListView()
        Me.colSrcName = CType(New ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSrcDesc = CType(New ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblSourceMembers = New Label()
        Me.SourceDestSplit = New Splitter()
        Me.pnlDest = New Panel()
        Me.gbxDestinationCollection = New GroupBox()
        Me.cmdSaveDestCollection = New Button()
        Me.cmdExportToFile = New Button()
        Me.lblCurrProteinCount = New Label()
        Me.lvwDestination = New ListView()
        Me.colName = CType(New ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.mnuMainGUI = New MainMenu(Me.components)
        Me.mnuFile = New MenuItem()
        Me.mnuFileExit = New MenuItem()
        Me.mnuTools = New MenuItem()
        Me.mnuToolsCollectionEdit = New MenuItem()
        Me.mnuToolsNucToProt = New MenuItem()
        Me.mnuToolsConvert = New MenuItem()
        Me.mnuToolsConvertF2A = New MenuItem()
        Me.mnuToolsConvertA2F = New MenuItem()
        Me.mnuToolsFCheckup = New MenuItem()
        Me.mnuToolsCompareDBs = New MenuItem()
        Me.mnuToolsExtractFromFile = New MenuItem()
        Me.mnuToolsSep1 = New MenuItem()
        Me.mnuToolsOptions = New MenuItem()
        Me.mnuAdmin = New MenuItem()
        Me.mnuAdminBatchUploadFiles = New MenuItem()
        Me.mnuAdminNameHashRefresh = New MenuItem()
        Me.mnuAdminUpdateSHA = New MenuItem()
        Me.mnuAdminUpdateCollectionsArchive = New MenuItem()
        Me.mnuAdminUpdateZeroedMasses = New MenuItem()
        Me.mnuAdminTestingInterface = New MenuItem()
        Me.mnuAdminFixArchivePaths = New MenuItem()
        Me.mnuAdminAddSortingIndexes = New MenuItem()
        Me.mnuHelp = New MenuItem()
        Me.mnuHelpAbout = New MenuItem()
        Me.pnlProgBar.SuspendLayout()
        Me.pnlProgBarUpper.SuspendLayout()
        Me.pnlProgBarLower.SuspendLayout()
        Me.pnlSource.SuspendLayout()
        Me.gbxSourceCollection.SuspendLayout()
        CType(Me.pbxLiveSearchCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxLiveSearchBkg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDest.SuspendLayout()
        Me.gbxDestinationCollection.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlProgBar
        '
        Me.pnlProgBar.Controls.Add(Me.pnlProgBarUpper)
        Me.pnlProgBar.Controls.Add(Me.pnlProgBarLower)
        Me.pnlProgBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlProgBar.Location = New System.Drawing.Point(0, 432)
        Me.pnlProgBar.Name = "pnlProgBar"
        Me.pnlProgBar.Size = New System.Drawing.Size(1130, 92)
        Me.pnlProgBar.TabIndex = 0
        Me.pnlProgBar.Visible = False
        '
        'pnlProgBarUpper
        '
        Me.pnlProgBarUpper.Controls.Add(Me.lblBatchProgress)
        Me.pnlProgBarUpper.Controls.Add(Me.lblCurrentTask)
        Me.pnlProgBarUpper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProgBarUpper.Location = New System.Drawing.Point(0, 0)
        Me.pnlProgBarUpper.Name = "pnlProgBarUpper"
        Me.pnlProgBarUpper.Padding = New Padding(6)
        Me.pnlProgBarUpper.Size = New System.Drawing.Size(1130, 51)
        Me.pnlProgBarUpper.TabIndex = 2
        '
        'lblBatchProgress
        '
        Me.lblBatchProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBatchProgress.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchProgress.Location = New System.Drawing.Point(6, 23)
        Me.lblBatchProgress.Name = "lblBatchProgress"
        Me.lblBatchProgress.Size = New System.Drawing.Size(1118, 22)
        Me.lblBatchProgress.TabIndex = 16
        '
        'lblCurrentTask
        '
        Me.lblCurrentTask.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCurrentTask.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentTask.Location = New System.Drawing.Point(6, 6)
        Me.lblCurrentTask.Name = "lblCurrentTask"
        Me.lblCurrentTask.Size = New System.Drawing.Size(1118, 17)
        Me.lblCurrentTask.TabIndex = 0
        Me.lblCurrentTask.Text = "Reading Source File..."
        Me.lblCurrentTask.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.lblCurrentTask.Visible = False
        '
        'pnlProgBarLower
        '
        Me.pnlProgBarLower.Controls.Add(Me.pgbMain)
        Me.pnlProgBarLower.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlProgBarLower.Location = New System.Drawing.Point(0, 51)
        Me.pnlProgBarLower.Name = "pnlProgBarLower"
        Me.pnlProgBarLower.Padding = New Padding(6)
        Me.pnlProgBarLower.Size = New System.Drawing.Size(1130, 41)
        Me.pnlProgBarLower.TabIndex = 0
        '
        'pgbMain
        '
        Me.pgbMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgbMain.Location = New System.Drawing.Point(6, 6)
        Me.pgbMain.Name = "pgbMain"
        Me.pgbMain.Size = New System.Drawing.Size(1118, 29)
        Me.pgbMain.TabIndex = 14
        Me.pgbMain.Visible = False
        '
        'pnlSource
        '
        Me.pnlSource.Controls.Add(Me.lblTargetServer)
        Me.pnlSource.Controls.Add(Me.cmdDestAdd)
        Me.pnlSource.Controls.Add(Me.cmdDestRemove)
        Me.pnlSource.Controls.Add(Me.cmdDestAddAll)
        Me.pnlSource.Controls.Add(Me.cmdDestRemoveAll)
        Me.pnlSource.Controls.Add(Me.gbxSourceCollection)
        Me.pnlSource.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSource.Location = New System.Drawing.Point(0, 0)
        Me.pnlSource.Name = "pnlSource"
        Me.pnlSource.Padding = New Padding(8, 8, 8, 10)
        Me.pnlSource.Size = New System.Drawing.Size(762, 432)
        Me.pnlSource.TabIndex = 0
        '
        'lblTargetServer
        '
        Me.lblTargetServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTargetServer.Location = New System.Drawing.Point(13, 388)
        Me.lblTargetServer.Name = "lblTargetServer"
        Me.lblTargetServer.Size = New System.Drawing.Size(300, 19)
        Me.lblTargetServer.TabIndex = 21
        Me.lblTargetServer.Text = "Target server: "
        '
        'cmdDestAdd
        '
        Me.cmdDestAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDestAdd.Enabled = False
        Me.cmdDestAdd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdDestAdd.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDestAdd.ForeColor = System.Drawing.SystemColors.Highlight
        Me.cmdDestAdd.Location = New System.Drawing.Point(688, 186)
        Me.cmdDestAdd.Name = "cmdDestAdd"
        Me.cmdDestAdd.Size = New System.Drawing.Size(54, 38)
        Me.cmdDestAdd.TabIndex = 5
        Me.cmdDestAdd.ThemedImage = CType(resources.GetObject("cmdDestAdd.ThemedImage"), System.Drawing.Bitmap)
        '
        'cmdDestRemove
        '
        Me.cmdDestRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDestRemove.Enabled = False
        Me.cmdDestRemove.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdDestRemove.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDestRemove.ForeColor = System.Drawing.SystemColors.Highlight
        Me.cmdDestRemove.Location = New System.Drawing.Point(688, 245)
        Me.cmdDestRemove.Name = "cmdDestRemove"
        Me.cmdDestRemove.Size = New System.Drawing.Size(54, 40)
        Me.cmdDestRemove.TabIndex = 6
        Me.cmdDestRemove.ThemedImage = CType(resources.GetObject("cmdDestRemove.ThemedImage"), System.Drawing.Bitmap)
        '
        'cmdDestAddAll
        '
        Me.cmdDestAddAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDestAddAll.Enabled = False
        Me.cmdDestAddAll.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdDestAddAll.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDestAddAll.ForeColor = System.Drawing.SystemColors.Highlight
        Me.cmdDestAddAll.Location = New System.Drawing.Point(688, 125)
        Me.cmdDestAddAll.Name = "cmdDestAddAll"
        Me.cmdDestAddAll.Size = New System.Drawing.Size(54, 40)
        Me.cmdDestAddAll.TabIndex = 3
        Me.cmdDestAddAll.ThemedImage = CType(resources.GetObject("cmdDestAddAll.ThemedImage"), System.Drawing.Bitmap)
        '
        'cmdDestRemoveAll
        '
        Me.cmdDestRemoveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDestRemoveAll.Enabled = False
        Me.cmdDestRemoveAll.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdDestRemoveAll.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDestRemoveAll.ForeColor = System.Drawing.SystemColors.Highlight
        Me.cmdDestRemoveAll.Location = New System.Drawing.Point(688, 306)
        Me.cmdDestRemoveAll.Name = "cmdDestRemoveAll"
        Me.cmdDestRemoveAll.Size = New System.Drawing.Size(54, 39)
        Me.cmdDestRemoveAll.TabIndex = 4
        Me.cmdDestRemoveAll.ThemedImage = CType(resources.GetObject("cmdDestRemoveAll.ThemedImage"), System.Drawing.Bitmap)
        '
        'gbxSourceCollection
        '
        Me.gbxSourceCollection.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbxSourceCollection.Controls.Add(Me.cboAnnotationTypePicker)
        Me.gbxSourceCollection.Controls.Add(Me.lblAnnotationTypeFilter)
        Me.gbxSourceCollection.Controls.Add(Me.pbxLiveSearchCancel)
        Me.gbxSourceCollection.Controls.Add(Me.lblSearchCount)
        Me.gbxSourceCollection.Controls.Add(Me.cmdLoadProteins)
        Me.gbxSourceCollection.Controls.Add(Me.cmdLoadFile)
        Me.gbxSourceCollection.Controls.Add(Me.txtLiveSearch)
        Me.gbxSourceCollection.Controls.Add(Me.cboCollectionPicker)
        Me.gbxSourceCollection.Controls.Add(Me.cboOrganismFilter)
        Me.gbxSourceCollection.Controls.Add(Me.lblOrganismFilter)
        Me.gbxSourceCollection.Controls.Add(Me.lblCollectionPicker)
        Me.gbxSourceCollection.Controls.Add(Me.pbxLiveSearchBkg)
        Me.gbxSourceCollection.Controls.Add(Me.lvwSource)
        Me.gbxSourceCollection.Controls.Add(Me.lblSourceMembers)
        Me.gbxSourceCollection.Location = New System.Drawing.Point(11, 10)
        Me.gbxSourceCollection.Name = "gbxSourceCollection"
        Me.gbxSourceCollection.Size = New System.Drawing.Size(661, 360)
        Me.gbxSourceCollection.TabIndex = 1
        Me.gbxSourceCollection.TabStop = False
        Me.gbxSourceCollection.Text = "Source Collection"
        '
        'cboAnnotationTypePicker
        '
        Me.cboAnnotationTypePicker.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboAnnotationTypePicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAnnotationTypePicker.Location = New System.Drawing.Point(336, 56)
        Me.cboAnnotationTypePicker.Name = "cboAnnotationTypePicker"
        Me.cboAnnotationTypePicker.Size = New System.Drawing.Size(302, 25)
        Me.cboAnnotationTypePicker.TabIndex = 17
        '
        'lblAnnotationTypeFilter
        '
        Me.lblAnnotationTypeFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAnnotationTypeFilter.Location = New System.Drawing.Point(336, 38)
        Me.lblAnnotationTypeFilter.Name = "lblAnnotationTypeFilter"
        Me.lblAnnotationTypeFilter.Size = New System.Drawing.Size(297, 20)
        Me.lblAnnotationTypeFilter.TabIndex = 18
        Me.lblAnnotationTypeFilter.Text = "Naming Authority Filter"
        '
        'pbxLiveSearchCancel
        '
        Me.pbxLiveSearchCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pbxLiveSearchCancel.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.pbxLiveSearchCancel.Image = CType(resources.GetObject("pbxLiveSearchCancel.Image"), System.Drawing.Image)
        Me.pbxLiveSearchCancel.Location = New System.Drawing.Point(272, 324)
        Me.pbxLiveSearchCancel.Name = "pbxLiveSearchCancel"
        Me.pbxLiveSearchCancel.Size = New System.Drawing.Size(22, 20)
        Me.pbxLiveSearchCancel.TabIndex = 16
        Me.pbxLiveSearchCancel.TabStop = False
        '
        'lblSearchCount
        '
        Me.lblSearchCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearchCount.Location = New System.Drawing.Point(314, 329)
        Me.lblSearchCount.Name = "lblSearchCount"
        Me.lblSearchCount.Size = New System.Drawing.Size(123, 19)
        Me.lblSearchCount.TabIndex = 15
        Me.lblSearchCount.Text = "30000/30000"
        Me.lblSearchCount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cmdLoadProteins
        '
        Me.cmdLoadProteins.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdLoadProteins.Location = New System.Drawing.Point(493, 103)
        Me.cmdLoadProteins.Name = "cmdLoadProteins"
        Me.cmdLoadProteins.Size = New System.Drawing.Size(143, 29)
        Me.cmdLoadProteins.TabIndex = 14
        Me.cmdLoadProteins.Text = "Load Proteins"
        '
        'cmdLoadFile
        '
        Me.cmdLoadFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdLoadFile.Location = New System.Drawing.Point(442, 320)
        Me.cmdLoadFile.Name = "cmdLoadFile"
        Me.cmdLoadFile.Size = New System.Drawing.Size(196, 29)
        Me.cmdLoadFile.TabIndex = 10
        Me.cmdLoadFile.Text = "&Import New Collection..."
        '
        'txtLiveSearch
        '
        Me.txtLiveSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtLiveSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLiveSearch.ForeColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtLiveSearch.Location = New System.Drawing.Point(53, 326)
        Me.txtLiveSearch.Name = "txtLiveSearch"
        Me.txtLiveSearch.Size = New System.Drawing.Size(150, 17)
        Me.txtLiveSearch.TabIndex = 8
        Me.txtLiveSearch.Text = "Search"
        '
        'cboCollectionPicker
        '
        Me.cboCollectionPicker.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboCollectionPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCollectionPicker.Location = New System.Drawing.Point(20, 103)
        Me.cboCollectionPicker.Name = "cboCollectionPicker"
        Me.cboCollectionPicker.Size = New System.Drawing.Size(459, 25)
        Me.cboCollectionPicker.TabIndex = 1
        '
        'cboOrganismFilter
        '
        Me.cboOrganismFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOrganismFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrganismFilter.Location = New System.Drawing.Point(20, 56)
        Me.cboOrganismFilter.Name = "cboOrganismFilter"
        Me.cboOrganismFilter.Size = New System.Drawing.Size(302, 25)
        Me.cboOrganismFilter.TabIndex = 0
        '
        'lblOrganismFilter
        '
        Me.lblOrganismFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOrganismFilter.Location = New System.Drawing.Point(20, 38)
        Me.lblOrganismFilter.Name = "lblOrganismFilter"
        Me.lblOrganismFilter.Size = New System.Drawing.Size(296, 20)
        Me.lblOrganismFilter.TabIndex = 3
        Me.lblOrganismFilter.Text = "Organism Selector"
        '
        'lblCollectionPicker
        '
        Me.lblCollectionPicker.Location = New System.Drawing.Point(20, 82)
        Me.lblCollectionPicker.Name = "lblCollectionPicker"
        Me.lblCollectionPicker.Size = New System.Drawing.Size(140, 19)
        Me.lblCollectionPicker.TabIndex = 4
        Me.lblCollectionPicker.Text = "Protein Collection"
        '
        'pbxLiveSearchBkg
        '
        Me.pbxLiveSearchBkg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pbxLiveSearchBkg.Image = CType(resources.GetObject("pbxLiveSearchBkg.Image"), System.Drawing.Image)
        Me.pbxLiveSearchBkg.Location = New System.Drawing.Point(22, 319)
        Me.pbxLiveSearchBkg.Name = "pbxLiveSearchBkg"
        Me.pbxLiveSearchBkg.Size = New System.Drawing.Size(280, 32)
        Me.pbxLiveSearchBkg.TabIndex = 9
        Me.pbxLiveSearchBkg.TabStop = False
        '
        'lvwSource
        '
        Me.lvwSource.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwSource.Columns.AddRange(New ColumnHeader() {Me.colSrcName, Me.colSrcDesc})
        Me.lvwSource.FullRowSelect = True
        Me.lvwSource.GridLines = True
        Me.lvwSource.Location = New System.Drawing.Point(20, 158)
        Me.lvwSource.Name = "lvwSource"
        Me.lvwSource.Size = New System.Drawing.Size(618, 152)
        Me.lvwSource.TabIndex = 2
        Me.lvwSource.UseCompatibleStateImageBehavior = False
        Me.lvwSource.View = System.Windows.Forms.View.Details
        '
        'colSrcName
        '
        Me.colSrcName.Text = "Name"
        Me.colSrcName.Width = 117
        '
        'colSrcDesc
        '
        Me.colSrcDesc.Text = "Description"
        Me.colSrcDesc.Width = 320
        '
        'lblSourceMembers
        '
        Me.lblSourceMembers.Location = New System.Drawing.Point(20, 135)
        Me.lblSourceMembers.Name = "lblSourceMembers"
        Me.lblSourceMembers.Size = New System.Drawing.Size(179, 20)
        Me.lblSourceMembers.TabIndex = 5
        Me.lblSourceMembers.Text = "Collection Members"
        '
        'SourceDestSplit
        '
        Me.SourceDestSplit.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.SourceDestSplit.Location = New System.Drawing.Point(762, 0)
        Me.SourceDestSplit.MinExtra = 265
        Me.SourceDestSplit.MinSize = 450
        Me.SourceDestSplit.Name = "SourceDestSplit"
        Me.SourceDestSplit.Size = New System.Drawing.Size(4, 432)
        Me.SourceDestSplit.TabIndex = 2
        Me.SourceDestSplit.TabStop = False
        '
        'pnlDest
        '
        Me.pnlDest.Controls.Add(Me.gbxDestinationCollection)
        Me.pnlDest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDest.Location = New System.Drawing.Point(766, 0)
        Me.pnlDest.Name = "pnlDest"
        Me.pnlDest.Padding = New Padding(8, 8, 8, 10)
        Me.pnlDest.Size = New System.Drawing.Size(364, 432)
        Me.pnlDest.TabIndex = 1
        '
        'gbxDestinationCollection
        '
        Me.gbxDestinationCollection.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbxDestinationCollection.Controls.Add(Me.cmdSaveDestCollection)
        Me.gbxDestinationCollection.Controls.Add(Me.cmdExportToFile)
        Me.gbxDestinationCollection.Controls.Add(Me.lblCurrProteinCount)
        Me.gbxDestinationCollection.Controls.Add(Me.lvwDestination)
        Me.gbxDestinationCollection.Location = New System.Drawing.Point(11, 10)
        Me.gbxDestinationCollection.Name = "gbxDestinationCollection"
        Me.gbxDestinationCollection.Size = New System.Drawing.Size(342, 410)
        Me.gbxDestinationCollection.TabIndex = 2
        Me.gbxDestinationCollection.TabStop = False
        Me.gbxDestinationCollection.Text = "Destination Collection"
        '
        'cmdSaveDestCollection
        '
        Me.cmdSaveDestCollection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSaveDestCollection.Location = New System.Drawing.Point(167, 368)
        Me.cmdSaveDestCollection.Name = "cmdSaveDestCollection"
        Me.cmdSaveDestCollection.Size = New System.Drawing.Size(159, 29)
        Me.cmdSaveDestCollection.TabIndex = 4
        Me.cmdSaveDestCollection.Text = "&Upload Collection..."
        '
        'cmdExportToFile
        '
        Me.cmdExportToFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdExportToFile.Enabled = False
        Me.cmdExportToFile.Location = New System.Drawing.Point(20, 368)
        Me.cmdExportToFile.Name = "cmdExportToFile"
        Me.cmdExportToFile.Size = New System.Drawing.Size(142, 29)
        Me.cmdExportToFile.TabIndex = 3
        Me.cmdExportToFile.Text = "Export to File..."
        '
        'lblCurrProteinCount
        '
        Me.lblCurrProteinCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrProteinCount.Location = New System.Drawing.Point(20, 23)
        Me.lblCurrProteinCount.Name = "lblCurrProteinCount"
        Me.lblCurrProteinCount.Size = New System.Drawing.Size(229, 18)
        Me.lblCurrProteinCount.TabIndex = 2
        Me.lblCurrProteinCount.Text = "Protein Count: 0"
        '
        'lvwDestination
        '
        Me.lvwDestination.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwDestination.Columns.AddRange(New ColumnHeader() {Me.colName})
        Me.lvwDestination.FullRowSelect = True
        Me.lvwDestination.GridLines = True
        Me.lvwDestination.Location = New System.Drawing.Point(20, 64)
        Me.lvwDestination.Name = "lvwDestination"
        Me.lvwDestination.Size = New System.Drawing.Size(302, 296)
        Me.lvwDestination.TabIndex = 0
        Me.lvwDestination.UseCompatibleStateImageBehavior = False
        Me.lvwDestination.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 228
        '
        'mnuMainGUI
        '
        Me.mnuMainGUI.MenuItems.AddRange(New MenuItem() {Me.mnuFile, Me.mnuTools, Me.mnuAdmin, Me.mnuHelp})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New MenuItem() {Me.mnuFileExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Index = 0
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuTools
        '
        Me.mnuTools.Index = 1
        Me.mnuTools.MenuItems.AddRange(New MenuItem() {Me.mnuToolsCollectionEdit, Me.mnuToolsNucToProt, Me.mnuToolsConvert, Me.mnuToolsFCheckup, Me.mnuToolsCompareDBs, Me.mnuToolsExtractFromFile, Me.mnuToolsSep1, Me.mnuToolsOptions})
        Me.mnuTools.Text = "&Tools"
        '
        'mnuToolsCollectionEdit
        '
        Me.mnuToolsCollectionEdit.Index = 0
        Me.mnuToolsCollectionEdit.Text = "&Edit Collection States..."
        '
        'mnuToolsNucToProt
        '
        Me.mnuToolsNucToProt.Index = 1
        Me.mnuToolsNucToProt.Text = "Translate Nucleotides to Proteins..."
        Me.mnuToolsNucToProt.Visible = False
        '
        'mnuToolsConvert
        '
        Me.mnuToolsConvert.Index = 2
        Me.mnuToolsConvert.MenuItems.AddRange(New MenuItem() {Me.mnuToolsConvertF2A, Me.mnuToolsConvertA2F})
        Me.mnuToolsConvert.Text = "Convert Database Format..."
        Me.mnuToolsConvert.Visible = False
        '
        'mnuToolsConvertF2A
        '
        Me.mnuToolsConvertF2A.Index = 0
        Me.mnuToolsConvertF2A.Text = "FASTA to Access..."
        '
        'mnuToolsConvertA2F
        '
        Me.mnuToolsConvertA2F.Index = 1
        Me.mnuToolsConvertA2F.Text = "Access to FASTA..."
        '
        'mnuToolsFCheckup
        '
        Me.mnuToolsFCheckup.Index = 3
        Me.mnuToolsFCheckup.Text = "Perform FASTA File Checkup..."
        Me.mnuToolsFCheckup.Visible = False
        '
        'mnuToolsCompareDBs
        '
        Me.mnuToolsCompareDBs.Index = 4
        Me.mnuToolsCompareDBs.Text = "Compare Databases..."
        Me.mnuToolsCompareDBs.Visible = False
        '
        'mnuToolsExtractFromFile
        '
        Me.mnuToolsExtractFromFile.Enabled = False
        Me.mnuToolsExtractFromFile.Index = 5
        Me.mnuToolsExtractFromFile.Text = "Extract Annotations from Text File..."
        '
        'mnuToolsSep1
        '
        Me.mnuToolsSep1.Index = 6
        Me.mnuToolsSep1.Text = "-"
        '
        'mnuToolsOptions
        '
        Me.mnuToolsOptions.Enabled = False
        Me.mnuToolsOptions.Index = 7
        Me.mnuToolsOptions.Text = "Options..."
        '
        'mnuAdmin
        '
        Me.mnuAdmin.Index = 2
        Me.mnuAdmin.MenuItems.AddRange(New MenuItem() {Me.mnuAdminBatchUploadFiles, Me.mnuAdminNameHashRefresh, Me.mnuAdminUpdateSHA, Me.mnuAdminUpdateCollectionsArchive, Me.mnuAdminUpdateZeroedMasses, Me.mnuAdminTestingInterface, Me.mnuAdminFixArchivePaths, Me.mnuAdminAddSortingIndexes})
        Me.mnuAdmin.Text = "Admin"
        '
        'mnuAdminBatchUploadFiles
        '
        Me.mnuAdminBatchUploadFiles.Index = 0
        Me.mnuAdminBatchUploadFiles.Text = "Batch Upload FASTA Files Using DMS..."
        Me.mnuAdminBatchUploadFiles.Visible = False
        '
        'mnuAdminNameHashRefresh
        '
        Me.mnuAdminNameHashRefresh.Index = 1
        Me.mnuAdminNameHashRefresh.Text = "Refresh Protein Name Hashes"
        '
        'mnuAdminUpdateSHA
        '
        Me.mnuAdminUpdateSHA.Enabled = False
        Me.mnuAdminUpdateSHA.Index = 2
        Me.mnuAdminUpdateSHA.Text = "Update File Authentication Hashes"
        Me.mnuAdminUpdateSHA.Visible = False
        '
        'mnuAdminUpdateCollectionsArchive
        '
        Me.mnuAdminUpdateCollectionsArchive.Enabled = False
        Me.mnuAdminUpdateCollectionsArchive.Index = 3
        Me.mnuAdminUpdateCollectionsArchive.Text = "Update Collections Archive"
        Me.mnuAdminUpdateCollectionsArchive.Visible = False
        '
        'mnuAdminUpdateZeroedMasses
        '
        Me.mnuAdminUpdateZeroedMasses.Index = 4
        Me.mnuAdminUpdateZeroedMasses.Text = "Update Zeroed Masses"
        Me.mnuAdminUpdateZeroedMasses.Visible = False
        '
        'mnuAdminTestingInterface
        '
        Me.mnuAdminTestingInterface.Index = 5
        Me.mnuAdminTestingInterface.Text = "Testing Interface Window..."
        '
        'mnuAdminFixArchivePaths
        '
        Me.mnuAdminFixArchivePaths.Enabled = False
        Me.mnuAdminFixArchivePaths.Index = 6
        Me.mnuAdminFixArchivePaths.Text = "Fix Archive Path Names"
        Me.mnuAdminFixArchivePaths.Visible = False
        '
        'mnuAdminAddSortingIndexes
        '
        Me.mnuAdminAddSortingIndexes.Index = 7
        Me.mnuAdminAddSortingIndexes.Text = "Add Sorting Indexes"
        Me.mnuAdminAddSortingIndexes.Visible = False
        '
        'mnuHelp
        '
        Me.mnuHelp.Index = 3
        Me.mnuHelp.MenuItems.AddRange(New MenuItem() {Me.mnuHelpAbout})
        Me.mnuHelp.Text = "&Help"
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Index = 0
        Me.mnuHelpAbout.Text = "&About Protein Collection Editor"
        '
        'frmCollectionEditor
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 17)
        Me.ClientSize = New System.Drawing.Size(1130, 524)
        Me.Controls.Add(Me.pnlDest)
        Me.Controls.Add(Me.SourceDestSplit)
        Me.Controls.Add(Me.pnlSource)
        Me.Controls.Add(Me.pnlProgBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mnuMainGUI
        Me.MinimumSize = New System.Drawing.Size(1148, 550)
        Me.Name = "frmCollectionEditor"
        Me.Text = "Protein Collection Editor"
        Me.pnlProgBar.ResumeLayout(False)
        Me.pnlProgBarUpper.ResumeLayout(False)
        Me.pnlProgBarLower.ResumeLayout(False)
        Me.pnlSource.ResumeLayout(False)
        Me.gbxSourceCollection.ResumeLayout(False)
        Me.gbxSourceCollection.PerformLayout()
        CType(Me.pbxLiveSearchCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxLiveSearchBkg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDest.ResumeLayout(False)
        Me.gbxDestinationCollection.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    <System.STAThread()> Public Shared Sub Main()
        System.Windows.Forms.Application.EnableVisualStyles()
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Application.Run(New frmCollectionEditor)
    End Sub


#End Region

    Private Const PROGRAM_DATE As String = "October 3, 2017"

    Private m_Organisms As DataTable
    Private m_ProteinCollections As DataTable
    Private m_ProteinCollectionNames As DataTable
    Private m_AnnotationTypes As DataTable
    Private m_CollectionMembers As DataTable
    Private m_SelectedOrganismID As Integer
    Private m_SelectedAnnotationTypeID As Integer
    Private m_SelectedFilePath As String
    Private m_SelectedCollectionID As Integer
    Private m_LastBatchULDirectoryPath As String
    Private m_PSConnectionString As String = "Data Source=proteinseqs;Initial Catalog=Protein_Sequences;Integrated Security=SSPI;"

    Private m_LastSelectedOrganism As String = ""
    Private m_LastSelectedAnnotationType As String = ""
    Private m_LastValueForAllowAsterisks As Boolean = False
    Private m_LastValueForAllowDash As Boolean = False
    Private m_LastValueForMaxProteinNameLength As Integer = clsValidateFastaFile.DEFAULT_MAXIMUM_PROTEIN_NAME_LENGTH

    Private WithEvents m_ImportHandler As IImportProteins
    Private WithEvents m_UploadHandler As IUploadProteins
    Private WithEvents m_SourceListViewHandler As DataListViewHandler
    Private WithEvents m_fileBatcher As clsBatchUploadFromFileList

    Private m_LocalFileLoaded As Boolean

    Private m_SearchActive As Boolean = False

    Private m_BatchLoadTotalCount As Integer
    Private m_BatchLoadCurrentCount As Integer

    ''' <summary>
    ''' Keys are fasta file names, values are lists of errors
    ''' </summary>
    Private m_FileErrorList As Dictionary(Of String, List(Of ICustomValidation.udtErrorInfoExtended))

    ''' <summary>
    ''' Keys are fasta file names, values are dictionaries of error messages, tracking the count of each error
    ''' </summary>
    Private m_SummarizedFileErrorList As Dictionary(Of String, Dictionary(Of String, Integer))

    ''' <summary>
    ''' Keys are fasta file names, values are lists of warnings
    ''' </summary>
    Private m_FileWarningList As Dictionary(Of String, List(Of ICustomValidation.udtErrorInfoExtended))

    ''' <summary>
    ''' Keys are fasta file names, values are dictionaries of warning messages, tracking the count of each warning
    ''' </summary>
    Private m_SummarizedFileWarningList As Dictionary(Of String, Dictionary(Of String, Integer))

    ''' <summary>
    ''' Keys are FASTA file paths
    ''' Values are upload info
    ''' </summary>
    Private m_ValidUploadsList As Dictionary(Of String, IUploadProteins.UploadInfo)

    Private WithEvents m_Syncer As clsSyncFASTAFileArchive

    Private ReadOnly m_EncryptSequences As Boolean = False

    Friend WithEvents SearchTimer As New Timer(2000)
    Friend WithEvents MemberLoadTimer As New Timer(2000)

    ''' <summary>
    ''' Tracks the description and source that the user has entered for each FASTA file
    ''' Key: fasta file name
    ''' Value: KeyValuePair of Description and Source
    ''' </summary>
    ''' <remarks>Useful in case validation fails and the uploader needs to try again to upload a FASTA file</remarks>
    Private ReadOnly m_CachedFileDescriptions As Dictionary(Of String, KeyValuePair(Of String, String))

    Private Sub frmCollectionEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Get initial info - organism list, full collections list

        ' Data Source=proteinseqs;Initial Catalog=Protein_Sequences
        Dim connectionString = My.Settings.ProteinSeqsDBConnectStr

        If Not String.IsNullOrWhiteSpace(connectionString) Then
            m_PSConnectionString = connectionString
        End If

        UpdateServerNameLabel()

        m_ImportHandler = New clsImportHandler(m_PSConnectionString)
        'mnuToolsFBatchUpload.Enabled = False

        lblBatchProgress.Text = "Fetching Organism and Collection Lists..."

        cboCollectionPicker.Enabled = False
        cboOrganismFilter.Enabled = False

        TriggerCollectionTableUpdate(False)

        m_SourceListViewHandler = New DataListViewHandler(lvwSource)

        cmdLoadProteins.Enabled = False
        txtLiveSearch.Visible = False
        pbxLiveSearchBkg.Visible = False
        pbxLiveSearchCancel.Visible = False
        lblSearchCount.Text = ""
        cmdExportToFile.Enabled = False
        cmdSaveDestCollection.Enabled = False

        AddHandler cboAnnotationTypePicker.SelectedIndexChanged, AddressOf cboAnnotationTypePicker_SelectedIndexChanged
        lblBatchProgress.Text = ""

        CheckTransferButtonsEnabledStatus()

        'Setup collections for selected organism

        'Use 2-3 second delay after collection change before refreshing member list


    End Sub

    Private Sub CheckTransferButtonsEnabledStatus()
        If lvwSource.Items.Count > 0 Then
            cmdDestAdd.Enabled = True
            cmdDestAddAll.Enabled = True
        Else
            cmdDestAdd.Enabled = False
            cmdDestAddAll.Enabled = False
        End If

        If lvwDestination.Items.Count > 0 Then
            cmdDestRemove.Enabled = True
            cmdDestRemoveAll.Enabled = True
        Else
            cmdDestRemove.Enabled = False
            cmdDestRemoveAll.Enabled = False
        End If
    End Sub

    Private Sub RefreshCollectionList()

        If m_SelectedOrganismID <> -1 And m_SelectedCollectionID <> -1 Then
            RemoveHandler cboAnnotationTypePicker.SelectedIndexChanged, AddressOf cboAnnotationTypePicker_SelectedIndexChanged
            RemoveHandler cboCollectionPicker.SelectedIndexChanged, AddressOf cboCollectionPicker_SelectedIndexChanged
            cboOrganismFilter.SelectedItem = m_SelectedOrganismID
            cboOrganismList_SelectedIndexChanged(Me, Nothing)

            cboCollectionPicker.SelectedItem = m_SelectedCollectionID
            cboAnnotationTypePicker.SelectedItem = m_SelectedAnnotationTypeID
            cboCollectionPicker.Select()
            AddHandler cboCollectionPicker.SelectedIndexChanged, AddressOf cboCollectionPicker_SelectedIndexChanged
            AddHandler cboAnnotationTypePicker.SelectedIndexChanged, AddressOf cboAnnotationTypePicker_SelectedIndexChanged
        End If

    End Sub

    Private Sub TriggerCollectionTableUpdate(RefreshTable As Boolean)
        If RefreshTable Then
            m_ImportHandler.TriggerProteinCollectionTableUpdate()
        End If
        'CollectionLoadThread = New System.Threading.Thread(AddressOf m_ImportHandler.TriggerProteinCollectionsLoad)
        'CollectionLoadThread.Start()
        If m_SelectedOrganismID > 0 Then
            m_ImportHandler.TriggerProteinCollectionsLoad(m_SelectedOrganismID)
        Else
            m_ImportHandler.TriggerProteinCollectionsLoad()
        End If
    End Sub

    Private Sub BindOrganismListToControl(organismList As DataTable)

        cboOrganismFilter.BeginUpdate()
        With cboOrganismFilter
            .DataSource = organismList
            .DisplayMember = "Display_Name"
            .ValueMember = "ID"
        End With
        cboOrganismFilter.EndUpdate()

    End Sub

    Private Sub BindAnnotationTypeListToControl(annotationTypeList As DataTable)
        cboAnnotationTypePicker.BeginUpdate()

        With cboAnnotationTypePicker
            .DisplayMember = "Display_Name"
            '.DisplayMember = "name"
            .ValueMember = "ID"
            .DataSource = annotationTypeList
            .Refresh()

        End With
        cboAnnotationTypePicker.EndUpdate()
    End Sub

    Private Sub BindCollectionListToControl(collectionList As DataView)

        cboCollectionPicker.BeginUpdate()
        If collectionList.Count = 0 Then
            With cboCollectionPicker
                .DataSource = Nothing
                .Items.Add(" -- No Collections for this Organism -- ")
                .SelectedIndex = 0
                .Enabled = False
            End With
            cmdLoadProteins.Enabled = False
            txtLiveSearch.Visible = False
            pbxLiveSearchBkg.Visible = False
            pbxLiveSearchCancel.Visible = False
        Else
            With cboCollectionPicker
                .Enabled = True
                .DataSource = collectionList
                .DisplayMember = "Display"
                .ValueMember = "Protein_Collection_ID"
            End With
            cmdLoadProteins.Enabled = True
        End If
        cboCollectionPicker.EndUpdate()

    End Sub

    Private Sub BatchLoadController()
        Dim resultReturn As DialogResult

        m_ProteinCollectionNames = m_ImportHandler.LoadProteinCollectionNames

        If Not m_FileErrorList Is Nothing Then
            m_FileErrorList.Clear()
        End If

        If Not m_FileWarningList Is Nothing Then
            m_FileWarningList.Clear()
        End If

        If Not m_ValidUploadsList Is Nothing Then
            m_ValidUploadsList.Clear()
        End If

        If Not m_SummarizedFileErrorList Is Nothing Then
            m_SummarizedFileErrorList.Clear()
        End If

        If Not m_SummarizedFileWarningList Is Nothing Then
            m_SummarizedFileWarningList.Clear()
        End If

        Dim frmBatchUpload As New frmBatchAddNewCollection(
            m_Organisms,
            m_AnnotationTypes,
            m_ProteinCollectionNames,
            m_PSConnectionString,
            m_LastBatchULDirectoryPath,
            m_CachedFileDescriptions)

        Dim tmpSelectedFileList As List(Of IUploadProteins.UploadInfo)

        lblBatchProgress.Text = ""

        If Not m_LastSelectedOrganism Is Nothing AndAlso m_LastSelectedOrganism.Length > 0 Then
            frmBatchUpload.SelectedOrganismName = m_LastSelectedOrganism
        End If

        If Not m_LastSelectedAnnotationType Is Nothing AndAlso m_LastSelectedAnnotationType.Length > 0 Then
            frmBatchUpload.SelectedAnnotationType = m_LastSelectedAnnotationType
        End If

        frmBatchUpload.ValidationAllowAsterisks = m_LastValueForAllowAsterisks
        frmBatchUpload.ValidationAllowDash = m_LastValueForAllowDash
        frmBatchUpload.ValidationMaxProteinNameLength = m_LastValueForMaxProteinNameLength

        ' Show the window
        resultReturn = frmBatchUpload.ShowDialog

        ' Save the selected organism and annotation type
        m_LastSelectedOrganism = frmBatchUpload.SelectedOrganismName
        m_LastSelectedAnnotationType = frmBatchUpload.SelectedAnnotationType
        m_LastValueForAllowAsterisks = frmBatchUpload.ValidationAllowAsterisks
        m_LastValueForAllowDash = frmBatchUpload.ValidationAllowDash
        m_LastValueForMaxProteinNameLength = frmBatchUpload.ValidationMaxProteinNameLength

        m_LastBatchULDirectoryPath = frmBatchUpload.CurrentDirectory

        Try
            ' Save these settings to the registry
            If Not String.IsNullOrEmpty(m_LastSelectedOrganism) Then
                SaveSetting("ProteinCollectionEditor", "UserOptions", "LastSelectedOrganism", m_LastSelectedOrganism)
            End If

            If Not String.IsNullOrEmpty(m_LastSelectedAnnotationType) Then
                SaveSetting("ProteinCollectionEditor", "UserOptions", "LastSelectedAnnotationType", m_LastSelectedAnnotationType)
            End If

            If Not String.IsNullOrEmpty(m_LastBatchULDirectoryPath) Then
                SaveSetting("ProteinCollectionEditor", "UserOptions", "LastBatchULDirectoryPath", m_LastBatchULDirectoryPath)
            End If

        Catch ex As Exception
            ' Ignore errors here
        End Try


        If resultReturn <> DialogResult.OK Then Return

        gbxSourceCollection.Enabled = False
        gbxDestinationCollection.Enabled = False
        cmdDestAdd.Enabled = False
        cmdDestAddAll.Enabled = False
        cmdDestRemove.Enabled = False
        cmdDestRemoveAll.Enabled = False

        tmpSelectedFileList = frmBatchUpload.FileList

        m_BatchLoadTotalCount = tmpSelectedFileList.Count

        If m_EncryptSequences Then
            m_UploadHandler = New clsPSUploadHandler(m_PSConnectionString)
        Else
            m_UploadHandler = New clsPSUploadHandler(m_PSConnectionString)
        End If
        m_UploadHandler.InitialSetup()

        pnlProgBar.Visible = True

        Try
            m_UploadHandler.SetValidationOptions(IUploadProteins.eValidationOptionConstants.AllowAllSymbolsInProteinNames, frmBatchUpload.ValidationAllowAllSymbolsInProteinNames)
            m_UploadHandler.SetValidationOptions(IUploadProteins.eValidationOptionConstants.AllowAsterisksInResidues, frmBatchUpload.ValidationAllowAsterisks)
            m_UploadHandler.SetValidationOptions(IUploadProteins.eValidationOptionConstants.AllowDashInResidues, frmBatchUpload.ValidationAllowDash)

            m_UploadHandler.MaximumProteinNameLength = frmBatchUpload.ValidationMaxProteinNameLength

            m_UploadHandler.BatchUpload(tmpSelectedFileList)

        Catch ex As Exception
            MessageBox.Show("Error uploading collection: " & ex.Message, "Error")
        End Try

        pnlProgBar.Visible = False

        ' Display any errors that occurred
        Dim errorDisplay As New frmValidationReport
        errorDisplay.FileErrorList = m_FileErrorList
        errorDisplay.FileWarningList = m_FileWarningList
        errorDisplay.FileValidList = m_ValidUploadsList
        errorDisplay.ErrorSummaryList = m_SummarizedFileErrorList
        errorDisplay.WarningSummaryList = m_SummarizedFileWarningList
        errorDisplay.OrganismList = m_Organisms
        errorDisplay.ShowDialog()

        lblBatchProgress.Text = "Updating Protein Collections List..."
        Application.DoEvents()

        TriggerCollectionTableUpdate(True)

        RefreshCollectionList()
        m_UploadHandler.ResetErrorList()

        lblBatchProgress.Text = ""
        gbxSourceCollection.Enabled = True
        gbxDestinationCollection.Enabled = True
        cmdDestAdd.Enabled = True
        cmdDestAddAll.Enabled = True
        cmdDestRemove.Enabled = True
        cmdDestRemoveAll.Enabled = True

        m_BatchLoadCurrentCount = 0
    End Sub

    Private Sub ReadSettings()
        Try
            m_LastSelectedOrganism = GetSetting("ProteinCollectionEditor", "UserOptions", "LastSelectedOrganism", "")
            m_LastSelectedAnnotationType = GetSetting("ProteinCollectionEditor", "UserOptions", "LastSelectedAnnotationType", "")
            m_LastBatchULDirectoryPath = GetSetting("ProteinCollectionEditor", "UserOptions", "LastBatchULDirectoryPath", "")
        Catch ex As Exception
            ' Ignore errors here
        End Try
    End Sub

    Private Sub ShowAboutBox()
        '    Dim AboutBox As New frmAboutBox

        '    AboutBox.Location = m_MainProcess.myAppSettings.AboutBoxLocation
        '    AboutBox.ShowDialog()

        Dim strMessage As String

        strMessage = "This is version " & Application.ProductVersion & ", " & PROGRAM_DATE

        MessageBox.Show(strMessage, "About Protein Collection Editor", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub UpdateServerNameLabel()

        Try
            If String.IsNullOrWhiteSpace(m_PSConnectionString) Then
                lblTargetServer.Text = "ERROR determining target server: m_PSConnectionString is empty"
                Return
            End If

            Dim reExtractServerName = New Regex("Data Source\s*=\s*([^\s;]+)", RegexOptions.IgnoreCase)
            Dim reMatch = reExtractServerName.Match(m_PSConnectionString)

            If reMatch.Success Then
                lblTargetServer.Text = "Target server: " & reMatch.Groups(1).Value
            Else
                lblTargetServer.Text = "Target server: UNKNOWN -- name not found in " & m_PSConnectionString
            End If

        Catch ex As Exception
            lblTargetServer.Text = "ERROR determining target server: " + ex.Message
        End Try

    End Sub

#Region " Combobox handlers "

    Private Sub cboOrganismList_SelectedIndexChanged(sender As Object, e As EventArgs)
        If CInt(cboOrganismFilter.SelectedValue) <> 0 Then
            m_ProteinCollections.DefaultView.RowFilter = "[Organism_ID] = " & cboOrganismFilter.SelectedValue.ToString
        Else
            m_ProteinCollections.DefaultView.RowFilter = ""
        End If

        m_SelectedOrganismID = CInt(cboOrganismFilter.SelectedValue)

        BindCollectionListToControl(m_ProteinCollections.DefaultView)

        If lvwSource.Items.Count = 0 Then
            cboCollectionPicker_SelectedIndexChanged(Me, Nothing)

        End If
    End Sub

    Private Sub cboCollectionPicker_SelectedIndexChanged(sender As Object, e As EventArgs)
        lvwSource.Items.Clear()
        m_ImportHandler.ClearProteinCollection()
        m_SelectedCollectionID = CInt(cboCollectionPicker.SelectedValue)

        If m_SelectedCollectionID > 0 Then
            Dim foundRows() As DataRow = m_ProteinCollections.Select("[Protein_Collection_ID] = " & m_SelectedCollectionID.ToString)
            m_SelectedAnnotationTypeID = CInt(foundRows(0).Item("Authority_ID"))
            'm_AnnotationTypes = m_ImportHandler.LoadAnnotationTypes(m_SelectedCollectionID)
            'm_AnnotationTypes = m_ImportHandler.LoadAnnotationTypes()
            cmdLoadProteins.Enabled = True
        Else
            m_AnnotationTypes = m_ImportHandler.LoadAnnotationTypes
            cmdLoadProteins.Enabled = False
        End If
        BindAnnotationTypeListToControl(m_AnnotationTypes)
    End Sub

    Private Sub cboAnnotationTypePicker_SelectedIndexChanged(sender As Object, e As EventArgs)
        If lvwSource.Items.Count > 0 Then
            lvwSource.Items.Clear()
            m_ImportHandler.ClearProteinCollection()
        End If

        If cboAnnotationTypePicker.SelectedValue.GetType Is Type.GetType("System.Int32") Then
            m_SelectedAnnotationTypeID = CInt(cboAnnotationTypePicker.SelectedValue)
        Else
            'm_SelectedAuthorityID = 0
        End If

        If m_SelectedCollectionID > 0 Then
            Dim foundRows() As DataRow = m_ProteinCollections.Select("[Protein_Collection_ID] = " & m_SelectedCollectionID.ToString)
            m_SelectedAnnotationTypeID = CInt(foundRows(0).Item("Authority_ID"))
            'ElseIf m_SelectedAuthorityID = -2 Then
            '    'Bring up addition dialog
            '    Dim AuthAdd As New clsAddNamingAuthority(m_PSConnectionString)
            '    tmpAuthID = AuthAdd.AddNamingAuthority
            '    m_Authorities = m_ImportHandler.LoadAuthorities()
            '    BindAuthorityListToControl(m_Authorities)
            '    m_SelectedAuthorityID = tmpAuthID
            '    cboAuthorityPicker.SelectedValue = tmpAuthID
        End If
    End Sub
#End Region

#Region " Action Button Handlers "

    Private Sub cmdLoadProteins_Click(sender As Object, e As EventArgs) Handles cmdLoadProteins.Click
        ImportStartHandler("Retrieving Protein Entries..")
        Dim foundRows() As DataRow =
            m_ProteinCollections.Select("Protein_Collection_ID = " & cboCollectionPicker.SelectedValue.ToString)
        ImportProgressHandler(0.5)
        m_SelectedFilePath = foundRows(0).Item("FileName").ToString
        MemberLoadTimerHandler(Me, Nothing)
        ImportProgressHandler(1.0)
        txtLiveSearch.Visible = True
        pbxLiveSearchBkg.Visible = True
        ImportEndHandler()

    End Sub

    Private Sub cmdLoadFile_Click(sender As Object, e As EventArgs) Handles cmdLoadFile.Click

        BatchLoadController()

    End Sub

    Private Sub cmdSaveDestCollection_Click(sender As Object, e As EventArgs) Handles cmdSaveDestCollection.Click
        Dim resultReturn As DialogResult

        Dim frmAddCollection As New frmAddNewCollection
        Dim tmpOrganismID As Integer
        Dim tmpAnnotationTypeID As Integer
        Dim tmpSelectedProteinList As List(Of String)

        If lvwDestination.Items.Count > 0 Then

            With frmAddCollection
                .CollectionName = Path.GetFileNameWithoutExtension(m_SelectedFilePath)
                .IsLocalFile = m_LocalFileLoaded
                .AnnotationTypes = m_AnnotationTypes
                .OrganismList = m_Organisms
                .OrganismID = m_SelectedOrganismID
                .AnnotationTypeID = m_SelectedAnnotationTypeID
            End With

            resultReturn = frmAddCollection.ShowDialog

            If resultReturn = DialogResult.OK Then
                cboCollectionPicker.Enabled = True
                cboOrganismFilter.Enabled = True

                tmpOrganismID = frmAddCollection.OrganismID
                tmpAnnotationTypeID = frmAddCollection.AnnotationTypeID

                tmpSelectedProteinList = ScanDestinationCollectionWindow(lvwDestination)

                If m_UploadHandler Is Nothing Then
                    m_UploadHandler = New clsPSUploadHandler(m_PSConnectionString)
                    m_UploadHandler.InitialSetup()
                End If

                m_UploadHandler.UploadCollection(m_ImportHandler.CollectionMembers,
                    tmpSelectedProteinList, frmAddCollection.CollectionName,
                    frmAddCollection.CollectionDescription,
                    frmAddCollection.CollectionSource,
                    IAddUpdateEntries.CollectionTypes.prot_original_source,
                    tmpOrganismID, tmpAnnotationTypeID)

                RefreshCollectionList()

                ClearFromDestinationCollectionWindow(lvwDestination, True)

                cboOrganismFilter.Enabled = True
                cboCollectionPicker.Enabled = True
                cboOrganismFilter.SelectedValue = tmpOrganismID
            End If

        End If
        m_UploadHandler = Nothing
    End Sub

    'Private Sub cmdExportToFile_Click(sender As System.Object, e As System.EventArgs) Handles cmdExportToFile.Click

    '    Dim SaveDialog As New SaveFileDialog
    '    Dim fileType As Protein_Importer.IImportProteins.ProteinImportFileTypes
    '    Dim SelectedSavePath As String
    '    Dim tmpSelectedProteinList As ArrayList
    '    Dim tmpProteinCollection As Protein_Storage.IProteinStorage
    '    Dim tmpProteinReference As String

    '    With SaveDialog
    '        .Title = "Save Protein Database File"
    '        .DereferenceLinks = True
    '        .Filter = "FASTA Files (*.fasta)|*.fasta|Microsoft Access Databases (*.mdb)|*.mdb|All Files (*.*)|*.*"
    '        .FilterIndex = 1
    '        .RestoreDirectory = True
    '        .OverwritePrompt = True
    '        '.FileName = m_ImportHandler.CollectionMembers.FileName
    '    End With

    '    If SaveDialog.ShowDialog = DialogResult.OK Then
    '        SelectedSavePath = SaveDialog.FileName
    '    Else
    '        Exit Sub
    '    End If

    '    If System.IO.Path.GetExtension(m_SelectedFilePath) = ".fasta" Then
    '        fileType = Protein_Importer.IImportProteins.ProteinImportFileTypes.FASTA
    '    ElseIf System.IO.Path.GetExtension(m_SelectedFilePath) = ".mdb" Then
    '        fileType = Protein_Importer.IImportProteins.ProteinImportFileTypes.Access
    '    End If

    '    If fileType = Protein_Importer.IImportProteins.ProteinImportFileTypes.FASTA Then
    '        m_ProteinExporter = New Protein_Exporter.clsExportProteinsFASTA
    '    Else
    '        Exit Sub
    '    End If

    '    tmpProteinCollection = New Protein_Storage.clsProteinStorage(SelectedSavePath)

    '    tmpSelectedProteinList = ScanDestinationCollectionWindow(lvwDestination)

    '    For Each tmpProteinReference In tmpSelectedProteinList
    '        tmpProteinCollection.AddProtein(
    '            m_ImportHandler.CollectionMembers.GetProtein(tmpProteinReference))
    '    Next


    '    m_ProteinExporter.Export(
    '        m_ImportHandler.CollectionMembers,
    '        SelectedSavePath)


    'End Sub

#End Region

#Region " LiveSearch Handlers "

    Private Sub txtLiveSearch_TextChanged(sender As Object, e As EventArgs) Handles txtLiveSearch.TextChanged

        If txtLiveSearch.Text.Length > 0 And txtLiveSearch.ForeColor.ToString <> "Color [InactiveCaption]" Then
            SearchTimer.Start()
        ElseIf txtLiveSearch.Text = "" And m_SearchActive = False Then
            pbxLiveSearchCancel_Click(Me, Nothing)
        Else
            m_SearchActive = False
            SearchTimer.Stop()
            'txtLiveSearch.Text = "Search"
            'txtLiveSearch.ForeColor = System.Drawing.SystemColors.InactiveCaption
        End If

    End Sub

    Private Sub txtLiveSearch_Click(sender As Object, e As EventArgs) Handles txtLiveSearch.Click
        If m_SearchActive Then
        Else
            RemoveHandler txtLiveSearch.TextChanged, AddressOf txtLiveSearch_TextChanged
            txtLiveSearch.Text = Nothing
            txtLiveSearch.ForeColor = SystemColors.ControlText
            m_SearchActive = True
            pbxLiveSearchCancel.Visible = True
            AddHandler txtLiveSearch.TextChanged, AddressOf txtLiveSearch_TextChanged
            'Debug.WriteLine("inactive.click")
        End If
    End Sub

    Private Sub txtLiveSearch_Leave(sender As Object, e As EventArgs) Handles txtLiveSearch.Leave
        If txtLiveSearch.Text.Length = 0 Then
            txtLiveSearch.ForeColor = SystemColors.InactiveCaption
            txtLiveSearch.Text = "Search"
            m_SearchActive = False
            SearchTimer.Stop()
            m_SourceListViewHandler.Load(m_CollectionMembers)
        End If
    End Sub

    Private Sub pbxLiveSearchCancel_Click(sender As Object, e As EventArgs) Handles pbxLiveSearchCancel.Click
        txtLiveSearch.Text = ""
        txtLiveSearch_Leave(Me, Nothing)
        lvwSource.Focus()
        pbxLiveSearchCancel.Visible = False
    End Sub

    Friend Sub SearchTimerHandler(
        sender As Object,
        e As ElapsedEventArgs) Handles SearchTimer.Elapsed

        If m_SearchActive = True Then
            'Debug.WriteLine("Searchtimer.active.kick")

            m_SourceListViewHandler.Load(m_CollectionMembers, txtLiveSearch.Text)
            m_SearchActive = False
            SearchTimer.Stop()
        Else
            'Debug.WriteLine("Searchtimer.inactive.kick")

        End If
    End Sub


#End Region

#Region " ListView Event Handlers "


    Private Sub lvwSource_DoubleClick(
        sender As Object,
        e As EventArgs) Handles lvwSource.DoubleClick

        ScanSourceCollectionWindow(lvwSource, lvwDestination, False)

    End Sub

    'Doubleclick to remove selected member from the destination collection
    Private Sub lvwDestination_DoubleClick(
        sender As Object,
        e As EventArgs) Handles lvwDestination.DoubleClick

        ClearFromDestinationCollectionWindow(lvwDestination, False)

    End Sub

    Friend Sub MemberLoadTimerHandler(
        sender As Object,
        e As ElapsedEventArgs) Handles MemberLoadTimer.Elapsed

        m_SelectedCollectionID = CInt(cboCollectionPicker.SelectedValue)
        m_SelectedAnnotationTypeID = CInt(cboAnnotationTypePicker.SelectedValue)

        m_CollectionMembers = m_ImportHandler.LoadCollectionMembersByID(m_SelectedCollectionID, m_SelectedAnnotationTypeID)
        m_LocalFileLoaded = False

        'm_SelectedAuthorityID = m_ImportHandler.

        m_SourceListViewHandler.Load(m_CollectionMembers)
        lvwSource.Focus()
        lvwSource.Enabled = True


        'MemberLoadTimer.Stop()

    End Sub

    'Update the selected collection
    'Private Sub lvwSource_SelectedIndexChanged(
    '    sender As System.Object,
    '    e As System.EventArgs) Handles lvwSource.SelectedIndexChanged

    'End Sub

    'Private Sub lvwDestination_SelectedIndexChanged(
    '    sender As System.Object,
    '    e As System.EventArgs) Handles lvwDestination.SelectedIndexChanged

    'End Sub

    'DoubleClick to Move the selected member to the destination collection


    Private Sub cmdDestAddAll_Click(sender As Object, e As EventArgs) Handles cmdDestAddAll.Click
        ScanSourceCollectionWindow(lvwSource, lvwDestination, True)
        CheckTransferButtonsEnabledStatus()
    End Sub

    Private Sub cmdDestAdd_Click(sender As Object, e As EventArgs) Handles cmdDestAdd.Click
        ScanSourceCollectionWindow(lvwSource, lvwDestination, False)
        CheckTransferButtonsEnabledStatus()
    End Sub

    Private Sub cmdDestRemove_Click(sender As Object, e As EventArgs) Handles cmdDestRemove.Click
        ClearFromDestinationCollectionWindow(lvwDestination, False)
        CheckTransferButtonsEnabledStatus()
    End Sub

    Private Sub cmdDestRemoveAll_Click(sender As Object, e As EventArgs) Handles cmdDestRemoveAll.Click
        ClearFromDestinationCollectionWindow(lvwDestination, True)
        CheckTransferButtonsEnabledStatus()
    End Sub

    Private Sub ScanSourceCollectionWindow(
        lvwSrc As ListView, lvwDest As ListView, SelectAll As Boolean)

        Dim entry As ListViewItem


        If SelectAll Then
            For Each entry In lvwSrc.Items
                'Need to figure out some way to check for duplicates (maybe just at upload time)
                lvwDest.Items.Add(entry.Text)
            Next
        Else
            For Each entry In lvwSrc.SelectedItems
                'Need to figure out some way to check for duplicates (maybe just at upload time)
                lvwDest.Items.Add(entry.Text)
            Next
        End If

        lblCurrProteinCount.Text = "Protein Count: " & lvwDest.Items.Count
        cmdExportToFile.Enabled = True
        cmdSaveDestCollection.Enabled = True

    End Sub

    Private Function ScanDestinationCollectionWindow(lvwDest As ListView) As List(Of String)
        Dim selectedList As New List(Of String)
        Dim li As ListViewItem

        For Each li In lvwDest.Items
            selectedList.Add(li.Text)
        Next

        Return selectedList

    End Function

    Private Sub ClearFromDestinationCollectionWindow(lvwDest As ListView, SelectAll As Boolean)
        Dim entry As ListViewItem

        If SelectAll Then
            lvwDest.Items.Clear()
            cmdSaveDestCollection.Enabled = False
            cmdExportToFile.Enabled = False
        Else
            For Each entry In lvwDest.SelectedItems
                entry.Remove()
            Next
        End If

        lblCurrProteinCount.Text = "Protein Count: " & lvwDest.Items.Count

    End Sub

#End Region

#Region " Menu Option Handlers"

    Private Sub mnuFileExit_Click(sender As Object, e As EventArgs) Handles mnuFileExit.Click
        Application.Exit()
    End Sub

    Private Sub mnuToolsFBatchUpload_Click(sender As Object, e As EventArgs)
        'Steal this to use with file-directed loading
        m_fileBatcher = New clsBatchUploadFromFileList(m_PSConnectionString)
        m_fileBatcher.UploadBatch()
    End Sub

    Private Sub mnuToolsCollectionEdit_Click(sender As Object, e As EventArgs) Handles mnuToolsCollectionEdit.Click
        Dim cse As New frmCollectionStateEditor(m_PSConnectionString)
        Dim r As DialogResult = cse.ShowDialog

    End Sub

    Private Sub mnuToolsExtractFromFile_Click(sender As Object, e As EventArgs) Handles mnuToolsExtractFromFile.Click
        Dim f As New frmExtractFromFlatfile(m_ImportHandler.Authorities, m_PSConnectionString)
        f.ShowDialog()
    End Sub

    Private Sub mnuToolsUpdateArchives_Click(sender As Object, e As EventArgs)
        Dim f As FolderBrowserDialog = New FolderBrowserDialog
        Dim r As DialogResult
        Dim outputPath As String

        If m_Syncer Is Nothing Then
            m_Syncer = New clsSyncFASTAFileArchive(m_PSConnectionString)
        End If



        With f
            .RootFolder = Environment.SpecialFolder.MyComputer
            .ShowNewFolderButton = True

            r = .ShowDialog()
        End With

        If r = DialogResult.OK Then
            outputPath = f.SelectedPath

            Dim errorCode As Integer
            errorCode = m_Syncer.SyncCollectionsAndArchiveTables(outputPath)
        End If

    End Sub

#End Region

#Region " Progress Event Handlers "
    Private Sub ImportStartHandler(taskTitle As String) Handles _
        m_ImportHandler.LoadStart,
        m_SourceListViewHandler.LoadStart,
        m_UploadHandler.LoadStart,
        m_fileBatcher.LoadStart,
        m_Syncer.SyncStart

        pnlProgBar.Visible = True
        pgbMain.Visible = True
        pgbMain.Value = 0
        lblCurrentTask.Text = taskTitle
        lblCurrentTask.Visible = True
        Application.DoEvents()

    End Sub

    Private Sub ImportProgressHandler(fractionDone As Double) Handles _
        m_ImportHandler.LoadProgress,
        m_SourceListViewHandler.LoadProgress,
        m_UploadHandler.LoadProgress,
        m_fileBatcher.ProgressUpdate

        pgbMain.Value = CInt(fractionDone * 100)
        Application.DoEvents()
    End Sub

    Private Sub SyncProgressHandler(statusmsg As String, fractionDone As Double) Handles m_Syncer.SyncProgress
        lblBatchProgress.Text = statusmsg
        If fractionDone > 1.0 Then
            fractionDone = 1.0
        End If
        pgbMain.Value = CInt(fractionDone * 100)
        Application.DoEvents()
    End Sub

    Private Sub ImportEndHandler() Handles _
        m_ImportHandler.LoadEnd,
        m_SourceListViewHandler.LoadEnd,
        m_UploadHandler.LoadEnd, m_fileBatcher.LoadEnd,
        m_Syncer.SyncComplete

        lblCurrentTask.Text = "Complete: " & lblCurrentTask.Text
        Invalidate()
        gbxDestinationCollection.Invalidate()
        gbxSourceCollection.Invalidate()
        Application.DoEvents()
    End Sub

    Private Sub CollectionLoadHandler(CollectionTable As DataTable) Handles m_ImportHandler.CollectionLoadComplete
        m_ProteinCollections = CollectionTable
        If m_Organisms Is Nothing Then
            m_Organisms = m_ImportHandler.LoadOrganisms
        End If
        If m_AnnotationTypes Is Nothing Then
            m_AnnotationTypes = m_ImportHandler.LoadAnnotationTypes
        End If
        BindOrganismListToControl(m_Organisms)
        BindAnnotationTypeListToControl(m_AnnotationTypes)
        m_ProteinCollections.DefaultView.RowFilter = ""
        BindCollectionListToControl(m_ProteinCollections.DefaultView)
        cboCollectionPicker.Enabled = True
        cboOrganismFilter.Enabled = True
        lblBatchProgress.Text = ""
        'mnuToolsFBatchUpload.Enabled = True

        AddHandler cboOrganismFilter.SelectedIndexChanged, AddressOf cboOrganismList_SelectedIndexChanged
        AddHandler cboCollectionPicker.SelectedIndexChanged, AddressOf cboCollectionPicker_SelectedIndexChanged

    End Sub

    Private Sub BatchImportProgressHandler(Status As String) Handles m_UploadHandler.BatchProgress, m_fileBatcher.TaskChange
        m_BatchLoadCurrentCount += 1
        lblBatchProgress.Text = Status & " (File " & m_BatchLoadCurrentCount.ToString & " of " & m_BatchLoadTotalCount & ")"
        Application.DoEvents()
    End Sub

    Private Sub FilteredLoadCountHandler(FilteredCount As Integer, TotalCount As Integer) Handles m_SourceListViewHandler.NumberLoadedStatus
        lblSearchCount.Text = FilteredCount.ToString & " / " & TotalCount.ToString
    End Sub

    Private Sub ValidFASTAUploadHandler(
        FASTAFilePath As String,
        UploadInfo As IUploadProteins.UploadInfo) Handles m_UploadHandler.ValidFASTAFileLoaded

        If m_ValidUploadsList Is Nothing Then
            m_ValidUploadsList = New Dictionary(Of String, IUploadProteins.UploadInfo)
        End If

        m_ValidUploadsList.Add(FASTAFilePath, UploadInfo)

    End Sub

    Private Sub InvalidFASTAFileHandler(FASTAFilePath As String, errorCollection As List(Of ICustomValidation.udtErrorInfoExtended)) Handles m_UploadHandler.InvalidFASTAFile

        If m_FileErrorList Is Nothing Then
            m_FileErrorList = New Dictionary(Of String, List(Of ICustomValidation.udtErrorInfoExtended))
        End If

        m_FileErrorList.Add(Path.GetFileName(FASTAFilePath), errorCollection)

        If m_SummarizedFileErrorList Is Nothing Then
            m_SummarizedFileErrorList = New Dictionary(Of String, Dictionary(Of String, Integer))
        End If

        m_SummarizedFileErrorList.Add(Path.GetFileName(FASTAFilePath), SummarizeErrors(errorCollection))

    End Sub

    Private Sub FASTAFileWarningsHandler(FASTAFilePath As String, warningCollection As List(Of ICustomValidation.udtErrorInfoExtended)) Handles m_UploadHandler.FASTAFileWarnings

        If m_FileWarningList Is Nothing Then
            m_FileWarningList = New Dictionary(Of String, List(Of ICustomValidation.udtErrorInfoExtended))
        End If

        m_FileWarningList.Add(Path.GetFileName(FASTAFilePath), warningCollection)

        If m_SummarizedFileWarningList Is Nothing Then
            m_SummarizedFileWarningList = New Dictionary(Of String, Dictionary(Of String, Integer))
        End If

        m_SummarizedFileWarningList.Add(Path.GetFileName(FASTAFilePath), SummarizeErrors(warningCollection))

    End Sub

    Private Function SummarizeErrors(ByRef errorCollection As List(Of ICustomValidation.udtErrorInfoExtended)) As Dictionary(Of String, Integer)

        ' Keys are error messages, values are the number of times the error was reported
        Dim errorSummary As New Dictionary(Of String, Integer)

        If Not errorCollection Is Nothing AndAlso errorCollection.Count > 0 Then
            For Each errorEntry In errorCollection
                Dim message = errorEntry.MessageText
                Dim currentCount As Integer

                If errorSummary.TryGetValue(message, currentCount) Then
                    errorSummary.Item(message) = currentCount + 1
                Else
                    errorSummary.Add(message, 1)
                End If
            Next
        End If

        Return errorSummary

    End Function

    Private Sub ValidationProgressHandler(taskTitle As String, fractionDone As Double) Handles m_UploadHandler.ValidationProgress
        'Handles m_ImportHandler.ValidationProgress, m_UploadHandler.ValidationProgress
        If Not taskTitle Is Nothing Then
            lblCurrentTask.Text = taskTitle
        End If
        pgbMain.Value = CInt(fractionDone * 100)
        Application.DoEvents()
    End Sub

    Private Sub NormalizedFASTAFileGenerationHandler(newFilePath As String) Handles m_UploadHandler.WroteLineEndNormalizedFASTA



    End Sub

#End Region

    Private Sub mnuAdminUpdateZeroedMasses_Click(sender As Object, e As EventArgs) Handles mnuAdminUpdateZeroedMasses.Click
        If m_Syncer Is Nothing Then
            m_Syncer = New clsSyncFASTAFileArchive(m_PSConnectionString)
        End If

        m_Syncer.CorrectMasses()
    End Sub

    Private Sub mnuAdminNameHashRefresh_Click(sender As Object, e As EventArgs) Handles mnuAdminNameHashRefresh.Click
        If m_Syncer Is Nothing Then
            m_Syncer = New clsSyncFASTAFileArchive(m_PSConnectionString)
        End If

        m_Syncer.RefreshNameHashes()
    End Sub

    Private Sub mnuToolsNucToProt_Click(sender As Object, e As EventArgs) Handles mnuToolsNucToProt.Click

    End Sub

    Private Sub mnuToolsConvert_Click(sender As Object, e As EventArgs) Handles mnuToolsConvert.Click

    End Sub

    Private Sub mnuToolsConvertF2A_Click(sender As Object, e As EventArgs) Handles mnuToolsConvertF2A.Click

    End Sub

    Private Sub mnuToolsConvertA2F_Click(sender As Object, e As EventArgs) Handles mnuToolsConvertA2F.Click

    End Sub

    Private Sub mnuToolsFCheckup_Click(sender As Object, e As EventArgs) Handles mnuToolsFCheckup.Click

    End Sub

    Private Sub MenuItem5_Click(sender As Object, e As EventArgs) Handles mnuAdminTestingInterface.Click
        Dim frmTesting As New frmTestingInterface
        frmTesting.Show()
    End Sub

    Private Sub MenuItem6_Click(sender As Object, e As EventArgs) Handles mnuAdminFixArchivePaths.Click
        If m_Syncer Is Nothing Then
            m_Syncer = New clsSyncFASTAFileArchive(m_PSConnectionString)
        End If

        m_Syncer.FixArchivedFilePaths()

    End Sub

    Private Sub MenuItem8_Click(sender As Object, e As EventArgs) Handles mnuAdminAddSortingIndexes.Click
        If m_Syncer Is Nothing Then
            m_Syncer = New clsSyncFASTAFileArchive(m_PSConnectionString)
        End If
        m_Syncer.AddSortingIndices()
    End Sub

    Private Sub mnuHelpAbout_Click(sender As Object, e As EventArgs) Handles mnuHelpAbout.Click
        ShowAboutBox()
    End Sub

    Private Sub mnuToolsOptions_Click(sender As Object, e As EventArgs) Handles mnuToolsOptions.Click

    End Sub

    'Private Sub mnuAdminUpdateZeroedMasses_Click(sender As System.Object, e As System.EventArgs) Handles mnuAdminUpdateZeroedMasses.Click

    'End Sub


End Class
