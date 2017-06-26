Option Strict On

Imports System.Text
Imports Protein_Exporter.ExportProteinCollectionsIFC

Public Class clsGetFASTAFromDMSScrambled
    Inherits clsGetFASTAFromDMSForward

    Private m_RndNumGen As Random
    Private m_RandomSeed As Integer

    Public Sub New(
        ProteinStorageConnectionString As String,
        DatabaseFormatType As IGetFASTAFromDMS.DatabaseFormatTypes)

        MyBase.New(ProteinStorageConnectionString, DatabaseFormatType)
        'Me.m_Naming_Suffix = "_scrambled_seed_" + RandomSeed.ToString
        'Me.m_RndNumGen = New Random(RandomSeed)
    End Sub

    Overrides Function SequenceExtender(originalSequence As String, collectionCount As Integer) As String

        Dim sb As New StringBuilder(originalSequence.Length)
        Dim sequence As String = originalSequence

        'Dim origArray() As Char = originalSequence.ToCharArray
        'Dim res As Char

        Dim index As Integer
        Dim counter As Integer

        'Dim al As New ArrayList(originalSequence.Length)

        'For counter = 1 To originalSequence.Length
        '    al.Add(counter)
        'Next

        'While al.Count > 0
        '    index = DirectCast(al.Item(Me.m_RndNumGen.Next(0, al.Count - 1)), Int32)
        '    Debug.WriteLine(
        '        "count = " + al.Count.ToString +
        '        "; maxRnd = " + (al.Count - 1).ToString +
        '        "; index = " + index.ToString +
        '        "; al.item = " + al.Item(index).ToString +
        '        "; residue = " + origArray(DirectCast(al.Item(index), Int32)))
        '    sb.Append(origArray(DirectCast(al.Item(index), Int32)))

        '    al.RemoveAt(index)
        'End While

        If Me.m_RndNumGen Is Nothing Then
            Me.m_RndNumGen = New Random(collectionCount)
            Me.m_Naming_Suffix = "_scrambled_seed_" + collectionCount.ToString
        End If

        counter = sequence.Length

        While counter > 0
            Debug.Assert(counter = sequence.Length)
            index = Me.m_RndNumGen.Next(counter)
            sb.Append(sequence.Substring(index, 1))

            If index > 0 Then
                If index < sequence.Length - 1 Then
                    sequence = sequence.Substring(0, index) & sequence.Substring(index + 1)
                Else
                    sequence = sequence.Substring(0, index)
                End If
            Else
                sequence = sequence.Substring(index + 1)
            End If
            counter -= 1

        End While
        sequence = sb.ToString
        Return sb.ToString

    End Function


    Overrides Function ReferenceExtender(originalReference As String) As String
        Return "Scrambled_" + originalReference
    End Function
End Class
