Imports Protein_Importer
Imports TableManipulationBase

Public Class clsCollectionStatePickerHandler
    Private m_forceReload As Boolean = False
    Private m_ListViewData As DataTable
    Private ReadOnly m_GetTables As IGetSQLData
    Private ReadOnly m_SPAccess As IAddUpdateEntries

    Public Sub New(PSConnectionString As String)
        Me.m_GetTables = New clsDBTask(PSConnectionString)
        Me.m_SPAccess = New clsAddUpdateEntries(PSConnectionString)
        Me.m_forceReload = True
    End Sub

    WriteOnly Property ForceIDTableReload As Boolean
        Set
            Me.m_forceReload = Value
        End Set
    End Property

    Sub ChangeSelectedCollectionStates(newStateID As Integer, selectedCollectionIDList As ArrayList)

        Dim ID As Integer

        For Each ID In selectedCollectionIDList
            Me.m_SPAccess.UpdateProteinCollectionState(ID, newStateID)
        Next
    End Sub

    Private Sub SetupPickerListView(lvw As ListView, dt As DataTable, filterCriteria As String)

        Dim tmpCreated As Date
        Dim tmpMod As Date


        filterCriteria = filterCriteria.Trim(" "c)

        Dim criteriaCollection() As String = filterCriteria.Split(" "c)
        Dim collectionRows() As DataRow
        Dim cRow As DataRow

        Dim filterString As String = String.Empty
        Dim filterElement As String

        If criteriaCollection.Length > 0 And filterCriteria.Length > 0 Then
            For Each filterElement In criteriaCollection
                filterString += "[Name] LIKE '%" & filterElement & "%' OR [State] LIKE '%" & filterElement & "%' OR "
            Next
            'Trim off final " OR "
            filterString = Left(filterString, filterString.Length - 4)
        Else
            filterString = ""
        End If

        collectionRows = dt.Select(filterString)
        Dim item As ListViewItem

        lvw.BeginUpdate()
        For Each cRow In collectionRows
            tmpCreated = CType(cRow.Item("Created"), Date)
            tmpMod = CType(cRow.Item("Modified"), Date)
            item = New ListViewItem
            item.Text = cRow.Item("Name").ToString
            item.Tag = cRow.Item("ID")
            item.SubItems.Add(Format(tmpCreated, "yyyy-MM-dd"))
            item.SubItems.Add(Format(tmpMod, "yyyy-MM-dd"))
            item.SubItems.Add(cRow.Item("State").ToString)
            lvw.Items.Add(item)
        Next
        lvw.EndUpdate()
    End Sub

    Sub FillListView(ListViewToFill As ListView)
        Me.FillFilteredListView(ListViewToFill, "")
    End Sub

    Sub FillFilteredListView(ListViewToFill As ListView, FilterString As String)
        ListViewToFill.Items.Clear()
        If Me.m_forceReload Then
            Me.m_ListViewData = Me.GetCollectionTable()
            Me.m_forceReload = False
        End If
        SetupPickerListView(ListViewToFill, Me.m_ListViewData, FilterString)
    End Sub

    Function GetCollectionTable() As DataTable
        Dim SQL = "SELECT * FROM V_Collection_State_Picker ORDER BY [Name]"
        Dim cTable As DataTable = Me.m_GetTables.GetTable(SQL)
        Return cTable
    End Function

    Function GetStates() As DataTable
        Dim SQL As String = "SELECT State, Collection_State_ID as ID " & "FROM T_Protein_Collection_States ORDER BY Collection_State_ID"
        Dim sTable As DataTable = Me.m_GetTables.GetTable(SQL)

        Return sTable
    End Function
End Class
