Option Strict On
Imports Protein_Exporter

Module modMain

    Const m_DebugLevel As Integer = 4
    Const FASTA_GEN_TIMEOUT_INTERVAL_MINUTES As Integer = 70
    Const DEFAULT_COLLECTION_OPTIONS As String = "seq_direction=forward,filetype=fasta"

    Private WithEvents m_FastaTools As ExportProteinCollectionsIFC.IGetFASTAFromDMS

    Private m_FastaToolsCnStr As String = "Data Source=proteinseqs;Initial Catalog=Protein_Sequences;Integrated Security=SSPI;"
    Private m_message As String
    Private m_FastaFileName As String
    Private WithEvents m_FastaTimer As System.Timers.Timer
    Private m_FastaGenTimeOut As Boolean
    Private m_GenerationComplete As Boolean = False
    Private m_GenerationStarted As Boolean = False

    Private m_FastaGenStartTime As DateTime = System.DateTime.Now

    Private mProteinCollectionList As String
    Private mCreationOpts As String
    Private mLegacyFasta As String
    Private mDestFolder As String
    Private mLogProteinFileDetails As Boolean

#Region "Event handlers"
    Private Sub m_FastaTools_FileGenerationStarted(ByVal taskMsg As String) Handles m_FastaTools.FileGenerationStarted

        m_GenerationStarted = True

    End Sub

    Private Sub m_FastaTools_FileGenerationCompleted(ByVal FullOutputPath As String) Handles m_FastaTools.FileGenerationCompleted

        m_FastaFileName = System.IO.Path.GetFileName(FullOutputPath)  'Get the name of the fasta file that was generated
        m_GenerationComplete = True     'Set the completion flag

    End Sub

    Private Sub m_FastaTools_FileGenerationProgress(ByVal statusMsg As String, ByVal fractionDone As Double) Handles m_FastaTools.FileGenerationProgress
        Const MINIMUM_LOG_INTERVAL_SEC As Integer = 15
        Static dtLastLogTime As DateTime
        Static dblFractionDoneSaved As Double = -1

        If m_DebugLevel >= 3 Then
            ' Limit the logging to once every MINIMUM_LOG_INTERVAL_SEC seconds
            If System.DateTime.Now.Subtract(dtLastLogTime).TotalSeconds >= MINIMUM_LOG_INTERVAL_SEC OrElse _
               fractionDone - dblFractionDoneSaved >= 0.25 Then
                dtLastLogTime = System.DateTime.Now
                dblFractionDoneSaved = fractionDone
                Console.WriteLine("Generating Fasta file, " & (fractionDone * 100).ToString("0.0") & "% complete, " & statusMsg)
            End If
        End If
    End Sub

    Private Sub m_FastaTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles m_FastaTimer.Elapsed

        If System.DateTime.Now.Subtract(m_FastaGenStartTime).TotalMinutes >= FASTA_GEN_TIMEOUT_INTERVAL_MINUTES Then
            m_FastaGenTimeOut = True      'Set the timeout flag so an error will be reported
            m_GenerationComplete = True     'Set the completion flag so the fasta generation wait loop will exit
        End If

    End Sub

#End Region

    Public Sub Main()
        Dim objParseCommandLine As New clsParseCommandLine
        Dim blnProceed As Boolean

        Try
            blnProceed = False

            mProteinCollectionList = String.Empty
            mCreationOpts = String.Empty
            mLegacyFasta = String.Empty
            mDestFolder = String.Empty
            mLogProteinFileDetails = False

            If objParseCommandLine.ParseCommandLine Then
                If SetOptionsUsingCommandLineParameters(objParseCommandLine) Then blnProceed = True
            End If

            If Not blnProceed OrElse _
               objParseCommandLine.NeedToShowHelp OrElse _
               objParseCommandLine.ParameterCount + objParseCommandLine.NonSwitchParameterCount = 0 OrElse _
               (mProteinCollectionList.Length = 0 AndAlso mLegacyFasta.Length = 0) Then
                ShowProgramHelp()
            Else

                ' To hard-code defaults, enter them here
                'mProteinCollectionList = "Shewanella_2006-07-11"
                'mCreationOpts = "seq_direction=forward,filetype=fasta"
                'mLegacyFasta = "na"
                'mDestFolder = "C:\DMS_Temp_Org"
                'mLogProteinFileDetails = True

                If mLegacyFasta.Length = 0 Then
                    mLegacyFasta = "na"
                End If

                If mCreationOpts.Length = 0 Then
                    mCreationOpts = DEFAULT_COLLECTION_OPTIONS
                End If

                If mDestFolder.Length = 0 Then
                    mDestFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                End If

                TestExport(mProteinCollectionList, mCreationOpts, mLegacyFasta, mDestFolder, mLogProteinFileDetails)

            End If

        Catch ex As Exception
            Console.WriteLine("Error occurred in modMain->Main: " & ControlChars.NewLine & ex.Message)
        End Try



    End Sub

    Private Function GetHumanReadableTimeInterval(ByVal dtInterval As System.TimeSpan) As String

        If dtInterval.TotalDays >= 1 Then
            ' Report Days
            Return dtInterval.TotalDays.ToString("0.00") & " days"
        ElseIf dtInterval.TotalHours >= 1 Then
            ' Report hours
            Return dtInterval.TotalHours.ToString("0.00") & " hours"
        ElseIf dtInterval.TotalMinutes >= 1 Then
            ' Report minutes
            Return dtInterval.TotalMinutes.ToString("0.00") & " minutes"
        Else
            ' Report seconds
            Return dtInterval.TotalSeconds.ToString("0.0") & " seconds"
        End If

    End Function

    Private Sub LogProteinFileDetails(ByVal CollectionList As String, _
                                      ByVal CreationOpts As String, _
                                      ByVal LegacyFasta As String, _
                                      ByVal HashString As String, _
                                      ByVal DestFolder As String, _
                                      ByVal FastaFileName As String, _
                                      ByVal LogFolderPath As String)


        ' Appends a new entry to the log file
        Dim swOutFile As System.IO.StreamWriter

        Dim strLogFileName As String
        Dim strLogFilePath As String
        Dim blnWriteHeader As Boolean = False

        Try
            ' Create a new log file each day
            strLogFileName = "FastaFileMakerLog_" & System.DateTime.Now.ToString("yyyy-MM-dd") & ".txt"

            If Not LogFolderPath Is Nothing AndAlso LogFolderPath.Length > 0 Then
                strLogFilePath = System.IO.Path.Combine(LogFolderPath, strLogFileName)
            Else
                strLogFilePath = String.Copy(strLogFileName)
            End If

            If Not System.IO.File.Exists(strLogFilePath) Then
                blnWriteHeader = True
            End If

            swOutFile = New System.IO.StreamWriter(New System.IO.FileStream(strLogFilePath, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Read))

            If blnWriteHeader Then
                swOutFile.WriteLine("Date" & ControlChars.Tab & _
                                    "Time" & ControlChars.Tab & _
                                    "Protein_Collection_List" & ControlChars.Tab & _
                                    "Creation_Options" & ControlChars.Tab & _
                                    "Legacy_Fasta_Name" & ControlChars.Tab & _
                                    "Hash_String" & ControlChars.Tab & _
                                    "Fasta_File_Name" & ControlChars.Tab & _
                                    "Fasta_File_Last_Modified" & ControlChars.Tab & _
                                    "Fasta_File_Created" & ControlChars.Tab & _
                                    "Fasta_File_Size_Bytes" & ControlChars.Tab & _
                                    "Fasta_File_Age_vs_PresentTime")
            End If

            Dim fiFastaFile As System.IO.FileInfo
            If Not DestFolder Is Nothing AndAlso DestFolder.Length > 0 Then
                fiFastaFile = New System.IO.FileInfo(System.IO.Path.Combine(DestFolder, FastaFileName))
            Else
                fiFastaFile = New System.IO.FileInfo(FastaFileName)
            End If


            swOutFile.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd") & ControlChars.Tab & _
                                System.DateTime.Now.ToString("hh:mm:ss tt") & ControlChars.Tab & _
                                CollectionList & ControlChars.Tab & _
                                CreationOpts & ControlChars.Tab & _
                                LegacyFasta & ControlChars.Tab & _
                                HashString & ControlChars.Tab & _
                                FastaFileName & ControlChars.Tab & _
                                fiFastaFile.LastWriteTime.ToString() & ControlChars.Tab & _
                                fiFastaFile.CreationTime.ToString() & ControlChars.Tab & _
                                fiFastaFile.Length & ControlChars.Tab & _
                                GetHumanReadableTimeInterval(System.DateTime.Now.Subtract(fiFastaFile.LastWriteTime)))

            If Not swOutFile Is Nothing Then
                swOutFile.Close()
            End If

        Catch
            ' Ignore errors here
        End Try

    End Sub

    Private Function SetOptionsUsingCommandLineParameters(ByVal objParseCommandLine As clsParseCommandLine) As Boolean
        ' Returns True if no problems; otherwise, returns false

        Dim strValue As String = String.Empty
        Dim strValidParameters() As String = New String() {"P", "C", "L", "O", "D"}

        Try
            ' Make sure no invalid parameters are present
            If objParseCommandLine.InvalidParametersPresent(strValidParameters) Then
                Return False
            Else
                With objParseCommandLine
                    ' Query objParseCommandLine to see if various parameters are present
                    If .RetrieveValueForParameter("P", strValue) Then mProteinCollectionList = strValue
                    If .RetrieveValueForParameter("C", strValue) Then mCreationOpts = strValue
                    If .RetrieveValueForParameter("L", strValue) Then mLegacyFasta = strValue
                    If .RetrieveValueForParameter("O", strValue) Then mDestFolder = strValue

                    If .RetrieveValueForParameter("D", strValue) Then mLogProteinFileDetails = True

                    If mProteinCollectionList.Length > 0 Then
                        mLegacyFasta = String.Empty
                    ElseIf mLegacyFasta.Length = 0 Then
                        ' Neither /P nor /L were used

                        If .NonSwitchParameterCount > 0 Then
                            ' User specified a non-switch parameter
                            ' Assume it is a protein collection list
                            mProteinCollectionList = .RetrieveNonSwitchParameter(0)
                            If mProteinCollectionList.ToLower.EndsWith(".fasta") OrElse _
                               mProteinCollectionList.ToLower.EndsWith(".fasta""") Then
                                ' User specified a .fasta file
                                mLegacyFasta = String.Copy(mProteinCollectionList)
                                mProteinCollectionList = String.Empty
                            End If
                        End If
                    End If

                End With

                Return True
            End If

        Catch ex As Exception
            Console.WriteLine("Error parsing the command line parameters: " & ControlChars.NewLine & ex.Message)
        End Try

    End Function

    Private Sub ShowProgramHelp()

        Try

            Console.WriteLine("This program can export protein collection(s) from the DMS Protein_Sequences database to create a .Fasta file. ")
            Console.WriteLine("Alternatively, you can specify a legacy .Fasta file name to retrieve")
            Console.WriteLine()
            Console.WriteLine("Program syntax:")
            Console.WriteLine("  " & System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location) & _
                                        " /P:ProteinCollectionList [/C:ProteinCollectionCreationOptions] [/O:OutputFolder] [/D]")
            Console.WriteLine("   or   ")
            Console.WriteLine("  " & System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location) & _
                                        " /L:LegacyFastaFileName [/O:OutputFolder] [/D]")
            Console.WriteLine()
            Console.WriteLine("To export one or more protein collections, specify the protein collection names as a comma separated list after the /P switch.")
            Console.WriteLine("When exporting protein collections, use optional switch /C to change the protein collection export options.  The default is: " & DEFAULT_COLLECTION_OPTIONS)
            Console.WriteLine()
            Console.WriteLine("To export a legacy fasta file, use /L, for example /L:FileName.fasta")
            Console.WriteLine()
            Console.WriteLine("Optionally use /O to specify the output folder.")
            Console.WriteLine("Optionally use /D to log the details of the protein collections, options, and resultant file to a log file.")
            Console.WriteLine()

            Console.WriteLine("Program written by Matthew Monroe for the Department of Energy (PNNL, Richland, WA) in 2009")
            Console.WriteLine()

            Console.WriteLine("E-mail: matthew.monroe@pnl.gov or matt@alchemistmatt.com")
            Console.WriteLine("Website: http://ncrr.pnl.gov/ or http://www.sysbio.org/resources/staff/")
            Console.WriteLine()

            Console.WriteLine("Licensed under the Apache License, Version 2.0; you may not use this file except in compliance with the License.  " & _
                              "You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0")
            Console.WriteLine()

            Console.WriteLine("Notice: This computer software was prepared by Battelle Memorial Institute, " & _
                              "hereinafter the Contractor, under Contract No. DE-AC05-76RL0 1830 with the " & _
                              "Department of Energy (DOE).  All rights in the computer software are reserved " & _
                              "by DOE on behalf of the United States Government and the Contractor as " & _
                              "provided in the Contract.  NEITHER THE GOVERNMENT NOR THE CONTRACTOR MAKES ANY " & _
                              "WARRANTY, EXPRESS OR IMPLIED, OR ASSUMES ANY LIABILITY FOR THE USE OF THIS " & _
                              "SOFTWARE.  This notice including this sentence must appear on any copies of " & _
                              "this computer software.")

            ' Delay for 750 msec in case the user double clicked this file from within Windows Explorer (or started the program via a shortcut)
            System.Threading.Thread.Sleep(750)

        Catch ex As Exception
            Console.WriteLine("Error displaying the program syntax: " & ex.Message)
        End Try

    End Sub

    Public Function TestExport(ByVal CollectionList As String, _
                               ByVal CreationOpts As String, _
                               ByVal LegacyFasta As String, _
                               ByVal DestFolder As String, _
                               ByVal blnLogProteinFileDetails As Boolean) As Boolean

        Dim HashString As String

        'Instantiate fasta tool if not already done
        If m_FastaTools Is Nothing Then
            If m_FastaToolsCnStr = "" Then
                m_message = "Protein database connection string not specified"
                Console.WriteLine("clsAnalysisResources.CreateFastaFile(), " & m_message)
                Return False
            End If
            m_FastaTools = New clsGetFASTAFromDMS(m_FastaToolsCnStr)

        End If

        m_FastaTimer = New System.Timers.Timer
        m_FastaTimer.Interval = 5000
        m_FastaTimer.AutoReset = True

        ' Note that m_FastaTools does not spawn a new thread
        '   Since it does not spawn a new thread, the while loop after this Try block won't actually get reached while m_FastaTools.ExportFASTAFile is running
        '   Furthermore, even if m_FastaTimer_Elapsed sets m_FastaGenTimeOut to True, this won't do any good since m_FastaTools.ExportFASTAFile will still be running
        m_FastaGenTimeOut = False
        m_FastaGenStartTime = System.DateTime.Now
        Try
            m_FastaTimer.Start()
            HashString = m_FastaTools.ExportFASTAFile(CollectionList, CreationOpts, LegacyFasta, DestFolder)
        Catch Ex As Exception
            Console.WriteLine("clsAnalysisResources.CreateFastaFile(), Exception generating OrgDb file: " & Ex.Message & _
            "; " & GetExceptionStackTrace(Ex))
            Return False
        End Try

        'Wait for fasta creation to finish
        While Not m_GenerationComplete
            System.Threading.Thread.Sleep(2000)
            If System.DateTime.Now.Subtract(m_FastaGenStartTime).TotalMinutes >= FASTA_GEN_TIMEOUT_INTERVAL_MINUTES Then
                m_FastaGenTimeOut = True
                Exit While
            End If
        End While

        m_FastaTimer.Stop()
        If m_FastaGenTimeOut Then
            'Fasta generator hung - report error and exit
            m_message = "Timeout error while generating OrdDb file (" & FASTA_GEN_TIMEOUT_INTERVAL_MINUTES.ToString & " minutes have elapsed)"
            Console.WriteLine("clsAnalysisResources.CreateFastaFile(), " & m_message)

            Return False
        End If

        If blnLogProteinFileDetails Then
            LogProteinFileDetails(CollectionList, CreationOpts, LegacyFasta, HashString, DestFolder, m_FastaFileName, "")
        End If

        Return True

    End Function

    ''' <summary>
    ''' Parses the .StackTrace text of the given expression to return a compact description of the current stack
    ''' </summary>
    ''' <param name="objException"></param>
    ''' <returns>String similar to "Stack trace: clsCodeTest.Test->clsCodeTest.TestException->clsCodeTest.InnerTestException in clsCodeTest.vb:line 86"</returns>
    ''' <remarks></remarks>
    Public Function GetExceptionStackTrace(ByVal objException As System.Exception) As String
        Const REGEX_FUNCTION_NAME As String = "at ([^(]+)\("
        Const REGEX_FILE_NAME As String = "in .+\\(.+)"

        Dim trTextReader As System.IO.StringReader
        Dim intIndex As Integer

        Dim intFunctionCount As Integer = 0
        Dim strFunctions() As String

        Dim strCurrentFunction As String
        Dim strFinalFile As String = String.Empty

        Dim strLine As String = String.Empty
        Dim strStackTrace As String = String.Empty

        Dim reFunctionName As New System.Text.RegularExpressions.Regex(REGEX_FUNCTION_NAME, System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim reFileName As New System.Text.RegularExpressions.Regex(REGEX_FILE_NAME, System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim objMatch As System.Text.RegularExpressions.Match

        ' Process each line in objException.StackTrace
        ' Populate strFunctions() with the function name of each line
        trTextReader = New System.IO.StringReader(objException.StackTrace)

        intFunctionCount = 0
        ReDim strFunctions(9)

        Do While trTextReader.Peek >= 0
            strLine = trTextReader.ReadLine

            If Not strLine Is Nothing AndAlso strLine.Length > 0 Then
                strCurrentFunction = String.Empty

                objMatch = reFunctionName.Match(strLine)
                If objMatch.Success AndAlso objMatch.Groups.Count > 1 Then
                    strCurrentFunction = objMatch.Groups(1).Value
                Else
                    ' Look for the word " in "
                    intIndex = strLine.ToLower.IndexOf(" in ")
                    If intIndex = 0 Then
                        ' " in" not found; look for the first space after startIndex 4
                        intIndex = strLine.IndexOf(" ", 4)
                    End If
                    If intIndex = 0 Then
                        ' Space not found; use the entire string
                        intIndex = strLine.Length - 1
                    End If

                    If intIndex > 0 Then
                        strCurrentFunction = strLine.Substring(0, intIndex)
                    End If

                End If

                If Not strCurrentFunction Is Nothing AndAlso strCurrentFunction.Length > 0 Then
                    If intFunctionCount >= strFunctions.Length Then
                        ' Reserve more space in strFunctions()
                        ReDim Preserve strFunctions(strFunctions.Length * 2 - 1)
                    End If

                    strFunctions(intFunctionCount) = strCurrentFunction
                    intFunctionCount += 1
                End If

                If strFinalFile.Length = 0 Then
                    ' Also extract the file name where the Exception occurred
                    objMatch = reFileName.Match(strLine)
                    If objMatch.Success AndAlso objMatch.Groups.Count > 1 Then
                        strFinalFile = objMatch.Groups(1).Value
                    End If
                End If

            End If
        Loop

        strStackTrace = String.Empty
        For intIndex = intFunctionCount - 1 To 0 Step -1
            If Not strFunctions(intIndex) Is Nothing Then
                If strStackTrace.Length = 0 Then
                    strStackTrace = "Stack trace: " & strFunctions(intIndex)
                Else
                    strStackTrace &= "->" & strFunctions(intIndex)
                End If
            End If
        Next intIndex

        If Not strStackTrace Is Nothing AndAlso strFinalFile.Length > 0 Then
            strStackTrace &= " in " & strFinalFile
        End If

        Return strStackTrace

    End Function
End Module
