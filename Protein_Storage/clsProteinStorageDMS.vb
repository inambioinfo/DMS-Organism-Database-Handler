
Imports System.Collections.Generic

Public Class clsProteinStorageDMS
    Implements IProteinStorage
    
    Private m_Proteins As Dictionary(Of String, IProteinStorageEntry)             'Protein_Name, clsProteinStorageEntry
    Private m_UniqueProteinIDList As Dictionary(Of Integer, SortedSet(Of String))        'Protein_ID, Protein_Name
    Private m_ProteinNames As SortedSet(Of String)

    Private m_ResidueCount As Integer
    Private m_EncryptionFlag As Boolean
    Private m_PassPhrase As String

    Public Sub New(fastaFileName As String)
        FileName = fastaFileName
    End Sub

    Public Sub AddProtein(proteinEntry As IProteinStorageEntry) Implements IProteinStorage.AddProtein
        Dim proteinName As String
        Dim ProteinEntryID As Integer = proteinEntry.Protein_ID
        Dim ProteinEntryName As String = proteinEntry.Reference

        If m_Proteins Is Nothing Then
            m_Proteins = New Dictionary(Of String, IProteinStorageEntry)
        End If

        If m_UniqueProteinIDList Is Nothing Then
            m_UniqueProteinIDList = New Dictionary(Of Integer, SortedSet(Of String))
        End If

        If m_ProteinNames Is Nothing Then
            m_ProteinNames = New SortedSet(Of String)
        End If

        Dim nameList As SortedSet(Of String) = Nothing

        If Not m_UniqueProteinIDList.TryGetValue(ProteinEntryID, nameList) Then
            nameList = New SortedSet(Of String)
            nameList.Add(ProteinEntryName)
            m_UniqueProteinIDList.Add(ProteinEntryID, nameList)
            m_ResidueCount += proteinEntry.Sequence.Length
        Else

            For Each proteinName In nameList
                Dim existingEntry = m_Proteins.Item(proteinName)

                If Not proteinEntry.Reference.Equals(existingEntry.Reference) Then
                    existingEntry.AddXRef(ProteinEntryName)
                    proteinEntry.AddXRef(existingEntry.Reference)
                End If
            Next

            nameList.Add(ProteinEntryName)
            m_UniqueProteinIDList(ProteinEntryID) = nameList
        End If

        If Not m_ProteinNames.Contains(ProteinEntryName) Then
            m_Proteins.Add(ProteinEntryName, proteinEntry)
            m_ProteinNames.Add(ProteinEntryName)
        End If

    End Sub

    Protected Function SortedProteinNameList() As SortedSet(Of String) Implements IProteinStorage.GetSortedProteinNames
        Return m_ProteinNames
    End Function

    Protected Property FileName As String Implements IProteinStorage.FileName

    Protected Function GetProtein(Reference As String) As Protein_Storage.IProteinStorageEntry Implements IProteinStorage.GetProtein
        If m_Proteins.ContainsKey(Reference) Then
            Return m_Proteins.Item(Reference)
        Else
            Return Nothing
        End If
    End Function

    Protected Sub ClearProteins() Implements IProteinStorage.ClearProteinEntries

        m_ResidueCount = 0

        Try
            If Not m_Proteins Is Nothing Then
                m_Proteins.Clear()
            End If
        Catch ex As Exception
            ' Ignore errors here
        End Try

        Try
            If Not m_ProteinNames Is Nothing Then
                m_ProteinNames.Clear()
            End If
        Catch ex As Exception
            ' Ignore errors here
        End Try

        Try
            If Not m_UniqueProteinIDList Is Nothing Then
                m_UniqueProteinIDList.Clear()
            End If
        Catch ex As Exception
            ' Ignore errors here
        End Try

    End Sub

    Protected ReadOnly Property TotalResidueCount As Integer Implements IProteinStorage.TotalResidueCount
        Get
            Return m_ResidueCount
        End Get
    End Property

    Protected ReadOnly Property ProteinCount As Integer Implements IProteinStorage.ProteinCount
        Get
            Return m_Proteins.Count
        End Get
    End Property

    Protected Property EncryptSequences As Boolean Implements IProteinStorage.EncryptSequences
        Get
            Return m_EncryptionFlag
        End Get
        Set
            m_EncryptionFlag = Value
        End Set
    End Property

    Protected Property PassPhrase As String Implements IProteinStorage.PassPhrase
        Get
            If EncryptSequences Then
                Return m_PassPhrase
            Else
                Return Nothing
            End If
        End Get
        Set
            m_PassPhrase = Value
        End Set
    End Property

    Protected Function m_GetEnumerator() As Dictionary(Of String, IProteinStorageEntry).Enumerator Implements IProteinStorage.GetEnumerator
        Return m_Proteins.GetEnumerator
    End Function

    Public Overrides Function ToString() As String
        Return FileName & ": " & m_ProteinNames.Count() & " proteins"
    End Function
End Class

