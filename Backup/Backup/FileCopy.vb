' Author: Taner Riffat
' Date Written: June 2008

Imports System.IO

Public Class FileCopy


    Public Event CopyStatus(ByVal sender As Object, ByVal e As BackupEventArgs)
    Public Event CountStatus(ByVal sender As Object, ByVal e As BackupEventArgs)
    Public Event MirrorStatus(ByVal sender As Object, ByVal e As BackupEventArgs)

    Public Event CopyCompleted(ByVal sender As Object, ByVal e As BackupEventArgs)
    Public Event CountCompleted(ByVal sender As Object, ByVal e As BackupEventArgs)
    Public Event MirrorCompleted(ByVal sender As Object, ByVal e As BackupEventArgs)

    Public Event BackupCompleted(ByVal sender As Object, ByVal e As BackupEventArgs)
    Public Event MirrorStarted()

    Public Event LogFileCreated(ByVal sender As Object, ByVal LogFileName As String)

    Public CopyThread As System.Threading.Thread
    Public CountThread As System.Threading.Thread
    Public MirrorThread As System.Threading.Thread

    Private _fromPath As String
    Private _toPath As String
    Private _mirrorCopy As Boolean
    Private _initMsg As String
    Private _quietLog As Boolean
    Private _batchCopy As Boolean

    Private _filePath As String
    Private _fileSize As String
    Private _copiedFolders As Long
    Private _copiedFiles As Long
    Private _countedFolders As Long
    Private _countedFiles As Long
    Private _mirroredFolders As Long
    Private _mirroredFiles As Long
    Private _stopNow As Boolean
    Private _lastFolder As String
    Private _lastTypeIsFile As Boolean

#Region "Properties"
    Public Property FromPath() As String

        Get
            Return _fromPath
        End Get
        Set(ByVal value As String)
            _fromPath = value
        End Set

    End Property
    Public Property ToPath() As String

        Get
            Return _toPath
        End Get
        Set(ByVal value As String)
            _toPath = value
        End Set

    End Property
    Public Property MirrorCopy() As Boolean

        Get
            Return _mirrorCopy
        End Get
        Set(ByVal value As Boolean)
            _mirrorCopy = value
        End Set

    End Property
    Public Property QuietLog() As Boolean

        Get
            Return _quietLog
        End Get
        Set(ByVal value As Boolean)
            _quietLog = value
        End Set

    End Property
    Public Property BatchCopy() As Boolean

        Get
            Return _batchCopy
        End Get
        Set(ByVal value As Boolean)
            _batchCopy = value
        End Set

    End Property
    Public Property InitialMessage() As String

        Get
            Return _initMsg
        End Get
        Set(ByVal value As String)
            _initMsg = value
        End Set

    End Property
#End Region
    Public Sub StartCopy()

        If Not ValidCopyPaths() Then Exit Sub

        CopyThread = New System.Threading.Thread(AddressOf Copy)
        CopyThread.IsBackground = True
        CopyThread.Name = "Copy"
        CopyThread.Start()

        If Not BatchCopy Then
            CountThread = New System.Threading.Thread(AddressOf Count)
            CountThread.IsBackground = True
            CountThread.Name = "Count"
            CountThread.Start()
        End If

    End Sub
    Private Sub StartMirror()

        MirrorThread = New System.Threading.Thread(AddressOf Mirror)
        MirrorThread.IsBackground = True
        MirrorThread.Name = "Mirror"
        MirrorThread.Start()

    End Sub
    Private Sub Copy()

        Dim dir As New DirectoryInfo(_fromPath)
        Dim FSinfo As FileSystemInfo() = dir.GetFileSystemInfos

        If Not CopyFiles(FSinfo) Then
            Exit Sub
        End If


        RaiseEvent CopyCompleted(Me, New BackupEventArgs(_filePath, _fileSize, _copiedFiles, _copiedFolders))
        Threading.Thread.Sleep(1)

        If MirrorCopy Then
            StartMirror()
        Else

            If Not CountThread Is Nothing Then
                If CountThread.IsAlive Then CountThread.Join()
            End If

            RaiseEvent BackupCompleted(Me, New BackupEventArgs("", 0, _copiedFiles, _copiedFolders))
            Threading.Thread.Sleep(1)
            End If

    End Sub
    Private Function CopyFiles(ByVal FSInfo As FileSystemInfo()) As Boolean

        If _stopNow Then
            Return True
        End If

        If FSInfo Is Nothing Then

            Return False
        End If

        Dim i As FileSystemInfo
        For Each i In FSInfo

            Try
                If TypeOf i Is DirectoryInfo Then

                    Dim dInfo As DirectoryInfo = CType(i, DirectoryInfo)

                    _copiedFolders = _copiedFolders + 1

                    CopyFiles(dInfo.GetFileSystemInfos())

                ElseIf TypeOf i Is FileInfo Then
                    _filePath = i.FullName

                    Dim CopyPath As String = _toPath & Mid(_filePath, Len(_fromPath) + 1, Len(_filePath) - Len(_fromPath) - Len(i.Name))

                    If Not Directory.Exists(CopyPath) Then
                        Directory.CreateDirectory(CopyPath)
                    End If

                    Dim ToFile As String = _toPath & Mid(_filePath, Len(_fromPath) + 1)

                    Dim OkayToCopy As Boolean = True
                    If File.Exists(ToFile) Then
                        If File.GetLastWriteTime(_filePath) = File.GetLastWriteTime(ToFile) Then
                            OkayToCopy = False
                        End If
                    End If

                    If OkayToCopy Then

                        Dim fi As New FileInfo(_filePath)
                        _fileSize = Decimal.Round(CDec(fi.Length / 1048576), 2)

                        RaiseEvent CopyStatus(Me, New BackupEventArgs(_filePath, _fileSize, _copiedFiles, _copiedFolders))
                        Threading.Thread.Sleep(1)

                        File.Copy(_filePath, ToFile, True)


                    End If

                    _copiedFiles += 1

                End If
            Catch ex As Exception

            End Try

        Next i

        Return True

    End Function
    Private Sub Count()
        Try
            Dim dir As New DirectoryInfo(_fromPath)

            Dim FSinfo As FileSystemInfo() = dir.GetFileSystemInfos

            If Not CountFiles(FSinfo) Then


                Exit Sub
            End If

            RaiseEvent CountCompleted(Me, New BackupEventArgs(_filePath, _fileSize, _countedFiles, _countedFolders))
            Threading.Thread.Sleep(1)
        Catch ex As Exception
         
        End Try
    End Sub
    Private Function CountFiles(ByVal FSInfo As FileSystemInfo()) As Boolean

        If _stopNow Then
            Return True
        End If

        If FSInfo Is Nothing Then

            Return False
        End If

        Dim i As FileSystemInfo
        For Each i In FSInfo

            Try
                If TypeOf i Is DirectoryInfo Then
                    _countedFolders += 1

                    Dim dInfo As DirectoryInfo = CType(i, DirectoryInfo)

                    CountFiles(dInfo.GetFileSystemInfos())

                ElseIf TypeOf i Is FileInfo Then
                    _countedFiles += 1

                    RaiseEvent CountStatus(Me, New BackupEventArgs("", 0, _countedFiles, _countedFolders))
                    Threading.Thread.Sleep(1)

                End If
            Catch ex As Exception

            End Try

        Next i

        Return True

    End Function
    Private Sub Mirror()

        RaiseEvent MirrorStarted()

        If Not BatchCopy Then MirrorCount()

        MirrorDeleteFolders(_toPath)

        RaiseEvent MirrorCompleted(Me, New BackupEventArgs("", 0, _mirroredFiles, _mirroredFolders))
        Threading.Thread.Sleep(1)

        RaiseEvent BackupCompleted(Me, New BackupEventArgs("", 0, _mirroredFiles, _mirroredFolders))
        Threading.Thread.Sleep(1)

    End Sub
    Private Function MirrorCount() As Boolean

        Try
            Dim dir As New DirectoryInfo(_toPath)
            Dim FSinfo As FileSystemInfo() = dir.GetFileSystemInfos

            If Not CountFiles(FSinfo) Then

              
                Exit Function
            End If

            RaiseEvent CountCompleted(Me, New BackupEventArgs(_filePath, _fileSize, _countedFiles, _countedFolders))
            Threading.Thread.Sleep(1)
        Catch ex As Exception
       
        End Try

    End Function
    Private Sub MirrorDeleteFolders(ByVal StartFolder As String)

        If _stopNow Then
            Exit Sub
        End If

        Dim Folder As DirectoryInfo = New DirectoryInfo(StartFolder)
        Dim SubFolders() As DirectoryInfo = Folder.GetDirectories()
        Dim Files() As FileInfo = Folder.GetFiles()

        Try
            For Each SubFolder As DirectoryInfo In SubFolders

                MirrorDeleteFolders(SubFolder.FullName)

            Next
            For Each FileItem As FileInfo In Files

                Dim CurrentFile As String = _fromPath & Mid(FileItem.FullName, Len(_toPath) + 1, Len(FileItem.FullName) - Len(_toPath))

                If Not File.Exists(CurrentFile) Then
                    FileItem.Attributes = FileAttributes.Normal
                    FileItem.Delete()


                End If

                RaiseEvent MirrorStatus(Me, New BackupEventArgs(_filePath, 0, _mirroredFiles, _mirroredFolders))

                _mirroredFiles += 1

            Next

            If Folder.FullName <> _toPath Then

                Dim CurrentFolder As String = _fromPath & Mid(Folder.FullName, Len(_toPath) + 1, Len(Folder.FullName) - Len(_toPath))

                If Not Directory.Exists(CurrentFolder) Then
                    Folder.Attributes = FileAttributes.Normal
                    Folder.Delete()


                End If

            End If

            _mirroredFolders += 1

        Catch ex As Exception
           
        End Try

    End Sub
    Public Sub DeleteFolders(ByVal StartFolder As String)

        Dim Folder As DirectoryInfo = New DirectoryInfo(StartFolder)
        Dim SubFolders() As DirectoryInfo = Folder.GetDirectories()
        Dim Files() As FileInfo = Folder.GetFiles()

        Try
            For Each SubFolder As DirectoryInfo In SubFolders
                DeleteFolders(SubFolder.FullName)
            Next

            For Each File As FileInfo In Files
                File.Delete()
            Next

            Folder.Delete()

        Catch ex As Exception

        End Try

    End Sub
    Public Sub StopThreads()

        _stopNow = True

    End Sub
    Private Function ValidCopyPaths() As Boolean

        If Not CheckPath(FromPath) Then
            Return False
        End If

        If Not CheckPath(ToPath) Then
            If Not CreatePath(ToPath) Then
                Return False
            End If
        End If

        Return (True)

    End Function
    Private Function CheckPath(ByVal Path As String) As Boolean

        Try
            Directory.GetDirectories(Path)
        Catch ex As System.IO.DirectoryNotFoundException

            Return False
        Catch ex As Exception
        
            Return False
        End Try

        Return True

    End Function
    Private Function CreatePath(ByVal Path As String) As Boolean

        Try
            Directory.CreateDirectory(Path)
        Catch ex As Exception
          
            Return False
        End Try

        Return True

    End Function
    Public Sub WaitForThreads()

        Dim ThreadsRunning As Boolean = False

        Do While True

            ThreadsRunning = False

            If Not CopyThread Is Nothing Then
                If CopyThread.IsAlive Then
                    ThreadsRunning = True
                End If
            End If
            If Not CountThread Is Nothing Then
                If CountThread.IsAlive Then
                    ThreadsRunning = True
                End If
            End If
            If Not MirrorThread Is Nothing Then
                If MirrorThread.IsAlive Then
                    ThreadsRunning = True
                End If
            End If

            If Not ThreadsRunning Then Exit Do

            Threading.Thread.Sleep(5000)
        Loop



    End Sub

End Class
Public Class BackupEventArgs
    Inherits EventArgs

    Private _folderCount As Long
    Public Property FolderCount() As Long
        Get
            Return _folderCount
        End Get
        Set(ByVal value As Long)
            _folderCount = value
        End Set
    End Property

    Private _fileCount As Long
    Public Property FileCount() As Long
        Get
            Return _fileCount
        End Get
        Set(ByVal value As Long)
            _fileCount = value
        End Set
    End Property
    Private _filePath As String
    Public Property FilePath() As String
        Get
            Return _filePath
        End Get
        Set(ByVal value As String)
            _filePath = value
        End Set
    End Property

    Private _filesize As String
    Public Property FileSize() As Long
        Get
            Return _filesize
        End Get
        Set(ByVal value As Long)
            _filesize = value
        End Set
    End Property

    Public Sub New(ByVal FilePathV As String, ByVal FileSizeV As Long, ByVal FileCountV As Long, ByVal FolderCountV As Long)
        FilePath = FilePathV
        FileSize = FileSizeV
        FileCount = FileCountV
        FolderCount = FolderCountV
    End Sub
    Public Sub New()

    End Sub
End Class