Option Strict Off

Imports System.IO

Public Class CRC32

    ' This is v2 of the VB CRC32 algorithm provided by Paul
    ' (wpsjr1@succeed.net) - much quicker than the nasty
    ' original version I posted.  Excellent work!

    Private ReadOnly crc32Table() As Integer
    Private Const BUFFER_SIZE As Integer = 1024

    Public Function GetCrc32(stream As Stream) As Integer

        Dim crc32Result = &HFFFFFFFF

        Dim buffer(BUFFER_SIZE) As Byte
        Dim readSize As Integer = BUFFER_SIZE

        Dim count As Integer = stream.Read(buffer, 0, readSize)
        Do While (count > 0)
            For i = 0 To count - 1
                Dim iLookup = (crc32Result And &HFF) Xor buffer(i)
                crc32Result = ((crc32Result And &HFFFFFF00) \ &H100) And &HFFFFFF   ' nasty shr 8 with vb :/
                crc32Result = crc32Result Xor crc32Table(iLookup)
            Next i
            count = stream.Read(buffer, 0, readSize)
        Loop

        Return Not (crc32Result)

    End Function

    Public Sub New()

        ' This is the official polynomial used by CRC32 in PKZip.
        ' Often the polynomial is shown reversed (04C11DB7).
        Dim dwPolynomial = &HEDB88320

        ReDim crc32Table(256)

        For i = 0 To 255
            Dim dwCrc = i
            ' ReSharper disable once RedundantAssignment
            For j = 8 To 1 Step -1
                If (dwCrc And 1) Then
                    dwCrc = ((dwCrc And &HFFFFFFFE) \ 2&) And &H7FFFFFFF
                    dwCrc = dwCrc Xor dwPolynomial
                Else
                    dwCrc = ((dwCrc And &HFFFFFFFE) \ 2&) And &H7FFFFFFF
                End If
            Next j
            crc32Table(i) = dwCrc
        Next i
    End Sub

End Class
