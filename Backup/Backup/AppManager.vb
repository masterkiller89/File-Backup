Public Class AppManager

    <STAThread()> _
    Shared Sub Main()

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)


        If Environment.GetCommandLineArgs.Length > 1 Then
         
            Dim BatchCopy As New FileCopy
            Dim Argument As String
            Dim Index As Integer = 0
            For Each Argument In Environment.GetCommandLineArgs
                Select Case Index
                    Case 0
                    Case 1
                        BatchCopy.FromPath = Environment.GetCommandLineArgs(1).ToString
                    Case 2
                        BatchCopy.ToPath = Environment.GetCommandLineArgs(2).ToString
                    Case 3
                        If Environment.GetCommandLineArgs(3).ToString.ToUpper = "TRUE" Then
                            BatchCopy.MirrorCopy = True
                        Else
                            BatchCopy.MirrorCopy = False
                        End If
                    Case 4
                        If Environment.GetCommandLineArgs(4).ToString.ToUpper = "TRUE" Then
                            BatchCopy.QuietLog = True
                        Else
                            BatchCopy.QuietLog = False
                        End If
                    Case Else
                      
                End Select
                Index += 1
            Next

            BatchCopy.InitialMessage = "*** MODE IS BATCH ***"
            If BatchCopy.MirrorCopy Then
                BatchCopy.InitialMessage = "*** MODE IS BATCH - MIRROR COPY IS SET ***"
            End If
            If BatchCopy.QuietLog Then
                BatchCopy.InitialMessage += vbCrLf & "Logging is quiet - not verbose."
            Else
                BatchCopy.InitialMessage += vbCrLf & "Logging is not quiet - its verbose."
            End If
            BatchCopy.BatchCopy = True
            BatchCopy.StartCopy()

            BatchCopy.WaitForThreads()
        Else
          
            Dim BackupForm As Backup
            BackupForm = New Backup()
            Application.Run(BackupForm)
        End If

    End Sub

End Class
