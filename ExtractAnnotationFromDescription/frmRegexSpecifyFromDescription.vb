Public Class frmRegexSpecifyFromDescription
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents lblStoredExpressions As System.Windows.Forms.Label
    Friend WithEvents cboStoredExpressions As System.Windows.Forms.ComboBox
    Friend WithEvents lblRegexEditor As System.Windows.Forms.Label
    Friend WithEvents txtRegexEditor As System.Windows.Forms.TextBox
    Friend WithEvents lblNewNames As System.Windows.Forms.Label
    'Friend WithEvents lvwNewNames As System.Windows.Forms.ListView
    Friend WithEvents lblMatchCount As System.Windows.Forms.Label
    Friend WithEvents cmdUploadAnnotations As System.Windows.Forms.Button
    Friend WithEvents cmdAddExpression As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveExpression As System.Windows.Forms.Button
    Friend WithEvents lblCurrentCollectionInfo As System.Windows.Forms.Label
    Friend WithEvents cmdMatch As System.Windows.Forms.Button
    Friend WithEvents lblNamingAuthority As System.Windows.Forms.Label
    Friend WithEvents cboNamingAuthority As System.Windows.Forms.ComboBox
    Friend WithEvents lvwProteins As System.Windows.Forms.ListView
    Friend WithEvents colProteinName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwNewNames As System.Windows.Forms.ListView
    Friend WithEvents colAnnGroup As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAnnGroupName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colExtAnnotation As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAnnotationTYpe As System.Windows.Forms.ColumnHeader
    Friend WithEvents rdbNameSelect As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDescriptionSelect As System.Windows.Forms.RadioButton
    Friend WithEvents gbxExtractionSource As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblStoredExpressions = New System.Windows.Forms.Label()
        Me.cboStoredExpressions = New System.Windows.Forms.ComboBox()
        Me.lblRegexEditor = New System.Windows.Forms.Label()
        Me.txtRegexEditor = New System.Windows.Forms.TextBox()
        Me.lblNewNames = New System.Windows.Forms.Label()
        Me.lvwNewNames = New System.Windows.Forms.ListView()
        Me.colAnnGroup = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAnnGroupName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colExtAnnotation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAnnotationTYpe = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblMatchCount = New System.Windows.Forms.Label()
        Me.cmdUploadAnnotations = New System.Windows.Forms.Button()
        Me.cmdAddExpression = New System.Windows.Forms.Button()
        Me.cmdRemoveExpression = New System.Windows.Forms.Button()
        Me.lblCurrentCollectionInfo = New System.Windows.Forms.Label()
        Me.cmdMatch = New System.Windows.Forms.Button()
        Me.lblNamingAuthority = New System.Windows.Forms.Label()
        Me.cboNamingAuthority = New System.Windows.Forms.ComboBox()
        Me.lvwProteins = New System.Windows.Forms.ListView()
        Me.colProteinName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.rdbNameSelect = New System.Windows.Forms.RadioButton()
        Me.rdbDescriptionSelect = New System.Windows.Forms.RadioButton()
        Me.gbxExtractionSource = New System.Windows.Forms.GroupBox()
        Me.gbxExtractionSource.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblStoredExpressions
        '
        Me.lblStoredExpressions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStoredExpressions.Location = New System.Drawing.Point(11, 291)
        Me.lblStoredExpressions.Name = "lblStoredExpressions"
        Me.lblStoredExpressions.Size = New System.Drawing.Size(230, 14)
        Me.lblStoredExpressions.TabIndex = 3
        Me.lblStoredExpressions.Text = "Stored and Recent Expressions"
        '
        'cboStoredExpressions
        '
        Me.cboStoredExpressions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboStoredExpressions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStoredExpressions.Location = New System.Drawing.Point(11, 310)
        Me.cboStoredExpressions.Name = "cboStoredExpressions"
        Me.cboStoredExpressions.Size = New System.Drawing.Size(550, 25)
        Me.cboStoredExpressions.TabIndex = 2
        '
        'lblRegexEditor
        '
        Me.lblRegexEditor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegexEditor.Location = New System.Drawing.Point(11, 191)
        Me.lblRegexEditor.Name = "lblRegexEditor"
        Me.lblRegexEditor.Size = New System.Drawing.Size(345, 22)
        Me.lblRegexEditor.TabIndex = 4
        Me.lblRegexEditor.Text = "Current Regular Expression"
        '
        'txtRegexEditor
        '
        Me.txtRegexEditor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRegexEditor.Location = New System.Drawing.Point(11, 211)
        Me.txtRegexEditor.Multiline = True
        Me.txtRegexEditor.Name = "txtRegexEditor"
        Me.txtRegexEditor.Size = New System.Drawing.Size(609, 70)
        Me.txtRegexEditor.TabIndex = 5
        '
        'lblNewNames
        '
        Me.lblNewNames.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewNames.Location = New System.Drawing.Point(11, 344)
        Me.lblNewNames.Name = "lblNewNames"
        Me.lblNewNames.Size = New System.Drawing.Size(175, 20)
        Me.lblNewNames.TabIndex = 6
        Me.lblNewNames.Text = "Extracted Annotations"
        '
        'lvwNewNames
        '
        Me.lvwNewNames.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwNewNames.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colAnnGroup, Me.colAnnGroupName, Me.colExtAnnotation, Me.colAnnotationTYpe})
        Me.lvwNewNames.FullRowSelect = True
        Me.lvwNewNames.GridLines = True
        Me.lvwNewNames.Location = New System.Drawing.Point(11, 364)
        Me.lvwNewNames.Name = "lvwNewNames"
        Me.lvwNewNames.Size = New System.Drawing.Size(797, 240)
        Me.lvwNewNames.TabIndex = 18
        Me.lvwNewNames.UseCompatibleStateImageBehavior = False
        Me.lvwNewNames.View = System.Windows.Forms.View.Details
        '
        'colAnnGroup
        '
        Me.colAnnGroup.Text = "Group ID"
        '
        'colAnnGroupName
        '
        Me.colAnnGroupName.Text = "Group Name"
        Me.colAnnGroupName.Width = 120
        '
        'colExtAnnotation
        '
        Me.colExtAnnotation.Text = "Extracted Annotation"
        Me.colExtAnnotation.Width = 200
        '
        'colAnnotationTYpe
        '
        Me.colAnnotationTYpe.Text = "Annotation Type"
        Me.colAnnotationTYpe.Width = 190
        '
        'lblMatchCount
        '
        Me.lblMatchCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMatchCount.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lblMatchCount.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMatchCount.Location = New System.Drawing.Point(501, 347)
        Me.lblMatchCount.Name = "lblMatchCount"
        Me.lblMatchCount.Size = New System.Drawing.Size(304, 17)
        Me.lblMatchCount.TabIndex = 8
        Me.lblMatchCount.Text = "(Matches 0/0 Descriptions)"
        Me.lblMatchCount.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'cmdUploadAnnotations
        '
        Me.cmdUploadAnnotations.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUploadAnnotations.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdUploadAnnotations.Location = New System.Drawing.Point(587, 631)
        Me.cmdUploadAnnotations.Name = "cmdUploadAnnotations"
        Me.cmdUploadAnnotations.Size = New System.Drawing.Size(221, 27)
        Me.cmdUploadAnnotations.TabIndex = 9
        Me.cmdUploadAnnotations.Text = "Upload Extracted Annotations"
        '
        'cmdAddExpression
        '
        Me.cmdAddExpression.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddExpression.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdAddExpression.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddExpression.Location = New System.Drawing.Point(567, 310)
        Me.cmdAddExpression.Name = "cmdAddExpression"
        Me.cmdAddExpression.Size = New System.Drawing.Size(28, 25)
        Me.cmdAddExpression.TabIndex = 10
        Me.cmdAddExpression.Text = "+"
        '
        'cmdRemoveExpression
        '
        Me.cmdRemoveExpression.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRemoveExpression.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdRemoveExpression.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRemoveExpression.Location = New System.Drawing.Point(595, 310)
        Me.cmdRemoveExpression.Name = "cmdRemoveExpression"
        Me.cmdRemoveExpression.Size = New System.Drawing.Size(28, 25)
        Me.cmdRemoveExpression.TabIndex = 11
        Me.cmdRemoveExpression.Text = "-"
        '
        'lblCurrentCollectionInfo
        '
        Me.lblCurrentCollectionInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCurrentCollectionInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCurrentCollectionInfo.Location = New System.Drawing.Point(0, 167)
        Me.lblCurrentCollectionInfo.Name = "lblCurrentCollectionInfo"
        Me.lblCurrentCollectionInfo.Size = New System.Drawing.Size(825, 19)
        Me.lblCurrentCollectionInfo.TabIndex = 12
        Me.lblCurrentCollectionInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'cmdMatch
        '
        Me.cmdMatch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdMatch.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdMatch.Location = New System.Drawing.Point(631, 308)
        Me.cmdMatch.Name = "cmdMatch"
        Me.cmdMatch.Size = New System.Drawing.Size(177, 27)
        Me.cmdMatch.TabIndex = 13
        Me.cmdMatch.Text = "Test Current Expression"
        '
        'lblNamingAuthority
        '
        Me.lblNamingAuthority.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNamingAuthority.Location = New System.Drawing.Point(11, 611)
        Me.lblNamingAuthority.Name = "lblNamingAuthority"
        Me.lblNamingAuthority.Size = New System.Drawing.Size(363, 22)
        Me.lblNamingAuthority.TabIndex = 16
        Me.lblNamingAuthority.Text = "Annotation Type for Selected Group"
        '
        'cboNamingAuthority
        '
        Me.cboNamingAuthority.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboNamingAuthority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNamingAuthority.Location = New System.Drawing.Point(11, 631)
        Me.cboNamingAuthority.Name = "cboNamingAuthority"
        Me.cboNamingAuthority.Size = New System.Drawing.Size(559, 25)
        Me.cboNamingAuthority.TabIndex = 17
        '
        'lvwProteins
        '
        Me.lvwProteins.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwProteins.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colProteinName, Me.colDescription})
        Me.lvwProteins.FullRowSelect = True
        Me.lvwProteins.GridLines = True
        Me.lvwProteins.Location = New System.Drawing.Point(1, 2)
        Me.lvwProteins.MultiSelect = False
        Me.lvwProteins.Name = "lvwProteins"
        Me.lvwProteins.Size = New System.Drawing.Size(819, 163)
        Me.lvwProteins.TabIndex = 19
        Me.lvwProteins.UseCompatibleStateImageBehavior = False
        Me.lvwProteins.View = System.Windows.Forms.View.Details
        '
        'colProteinName
        '
        Me.colProteinName.Text = "Protein Name"
        Me.colProteinName.Width = 170
        '
        'colDescription
        '
        Me.colDescription.Text = "Description"
        Me.colDescription.Width = 416
        '
        'rdbNameSelect
        '
        Me.rdbNameSelect.Location = New System.Drawing.Point(11, 24)
        Me.rdbNameSelect.Name = "rdbNameSelect"
        Me.rdbNameSelect.Size = New System.Drawing.Size(146, 20)
        Me.rdbNameSelect.TabIndex = 20
        Me.rdbNameSelect.Text = "Protein Name"
        '
        'rdbDescriptionSelect
        '
        Me.rdbDescriptionSelect.Location = New System.Drawing.Point(11, 49)
        Me.rdbDescriptionSelect.Name = "rdbDescriptionSelect"
        Me.rdbDescriptionSelect.Size = New System.Drawing.Size(146, 17)
        Me.rdbDescriptionSelect.TabIndex = 21
        Me.rdbDescriptionSelect.Text = "Description Text"
        '
        'gbxExtractionSource
        '
        Me.gbxExtractionSource.Controls.Add(Me.rdbNameSelect)
        Me.gbxExtractionSource.Controls.Add(Me.rdbDescriptionSelect)
        Me.gbxExtractionSource.Location = New System.Drawing.Point(644, 206)
        Me.gbxExtractionSource.Name = "gbxExtractionSource"
        Me.gbxExtractionSource.Size = New System.Drawing.Size(165, 81)
        Me.gbxExtractionSource.TabIndex = 22
        Me.gbxExtractionSource.TabStop = False
        Me.gbxExtractionSource.Text = "Extract From"
        '
        'frmRegexSpecifyFromDescription
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 17)
        Me.ClientSize = New System.Drawing.Size(822, 667)
        Me.Controls.Add(Me.gbxExtractionSource)
        Me.Controls.Add(Me.lvwProteins)
        Me.Controls.Add(Me.cboNamingAuthority)
        Me.Controls.Add(Me.lblNamingAuthority)
        Me.Controls.Add(Me.cmdMatch)
        Me.Controls.Add(Me.cmdRemoveExpression)
        Me.Controls.Add(Me.cmdAddExpression)
        Me.Controls.Add(Me.cmdUploadAnnotations)
        Me.Controls.Add(Me.lvwNewNames)
        Me.Controls.Add(Me.lblNewNames)
        Me.Controls.Add(Me.txtRegexEditor)
        Me.Controls.Add(Me.lblRegexEditor)
        Me.Controls.Add(Me.lblStoredExpressions)
        Me.Controls.Add(Me.cboStoredExpressions)
        Me.Controls.Add(Me.lblCurrentCollectionInfo)
        Me.Controls.Add(Me.lblMatchCount)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(840, 712)
        Me.Name = "frmRegexSpecifyFromDescription"
        Me.Text = "Extract Annotations From Description"
        Me.gbxExtractionSource.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private m_ExtractionSource As ExtractionSources

    Enum ExtractionSources
        Name
        Description
    End Enum

    Private Sub frmRegexSpecify_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub lblNewNames_Click(sender As System.Object, e As System.EventArgs) Handles lblNewNames.Click

    End Sub

    Private Sub lbxNewNames_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lvwNewNames.SelectedIndexChanged

    End Sub

    Private Sub cmdMatch_Click(sender As System.Object, e As System.EventArgs) Handles cmdMatch.Click

    End Sub

    Private Sub rdbSourceSelect_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdbNameSelect.CheckedChanged, rdbDescriptionSelect.CheckedChanged
		Dim rdb As System.Windows.Forms.RadioButton = CType(sender, System.Windows.Forms.RadioButton)

		If rdb.Checked = True Then
			If rdb.Name = Me.rdbNameSelect.Name Then
				Me.m_ExtractionSource = ExtractionSources.Name
			ElseIf rdb.Name = Me.rdbDescriptionSelect.Name Then
				Me.m_ExtractionSource = ExtractionSources.Description
			End If
		End If

    End Sub
End Class
