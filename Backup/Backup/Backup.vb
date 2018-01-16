

Imports System.Threading
Imports System.IO
Imports System.Diagnostics.Process
Imports System.Windows

Public Class Backup

    Dim WithEvents CopyFiles As FileCopy

    Public Delegate Sub CopyHandler(ByVal FilePath As String, ByVal FileSize As Long, ByVal FileCount As Long)
    Public Delegate Sub CountHandler(ByVal FileCount As Long, ByVal FolderCount As Long)
    Public Delegate Sub MirrorHandler(ByVal FilePath As String, ByVal FileCount As Long, ByVal FolderCount As Long)
    Public Delegate Sub MirrorStartedHandler()
    Public Delegate Sub BackupHandler()
    Public Delegate Sub WorkingHandler()


    Private _totalFiles As Long = 0
    Private _totalFolders As Long = 0
    Private _copiedFiles As Long = 0
    Private _rootDir As String = ""
    Private _logFile As String
    Private _stopped As Boolean = False
    Private _copyStatus As String = "Status: Copying and Counting. "
    Private _deletedCount As Long = 0
    Private Sub BrowseFrom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseFrom.Click

        FolderBrowserDialog1.ShowDialog()
        FromPathTextbox.Text = FolderBrowserDialog1.SelectedPath

        If FromPathTextbox.Text <> "" And ToPathTextbox.Text <> "" Then
            StartCopy.Enabled = True
        End If

    End Sub
    Private Sub BrowseTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseTo.Click

        FolderBrowserDialog1.ShowDialog()
        ToPathTextbox.Text = FolderBrowserDialog1.SelectedPath

        If FromPathTextbox.Text <> "" And ToPathTextbox.Text <> "" Then
            StartCopy.Enabled = True
        End If

    End Sub
    Private Sub StartCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartCopy.Click

        _stopped = False


        CopyFiles = New FileCopy

        CopyFiles.FromPath = FromPathTextbox.Text
        CopyFiles.ToPath = ToPathTextbox.Text
        CopyFiles.InitialMessage = "*** MODE IS WINFORMS ***"
        If CopyFiles.MirrorCopy Then
            CopyFiles.InitialMessage = "*** MODE IS WINFORMS - MIRROR COPY IS SET ***"
        End If
       
        CopyFiles.StartCopy()

        StartCopy.Enabled = False
        Panel1.Enabled = False
        StopCopy.Enabled = True


    End Sub
    Private Sub StopCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopCopy.Click

        CopyFiles.StopThreads()
        CopyFiles = Nothing

        ProgressBar1.Maximum = 0
        ProgressBar1.Value = 0

        FileStatusTextbox.Text = ""
        CopyStatusLabel.Text = "Status: Not Started"

        StartCopy.Enabled = True
        Panel1.Enabled = True
        StopCopy.Enabled = False

        _copyStatus = "Status: Copying and Counting. "
        _deletedCount = 0

        
        _stopped = True

    End Sub
    Private Sub CopyStatus(ByVal FilePath As String, ByVal FileSize As Long, ByVal FileCount As Long)

      
        If _stopped Then Exit Sub

        FileStatusTextbox.Text = "Copying: " & FilePath & ". Filesize is " & FileSize.ToString & " megabytes."
       
        If ProgressBar1.Maximum <> 0 Then ProgressBar1.Value = _totalFiles - (_totalFiles - FileCount)

    End Sub
    Private Sub CopyCompleted(ByVal FilePath As String, ByVal FileSize As Long, ByVal FileCount As Long)

        CopyStatusLabel.Text = "Status: Copy Finsihed. Copied " + _totalFiles.ToString + " files in " + _totalFolders.ToString + " folders."
        FileStatusTextbox.Text = "Copy completed successfully."
        ProgressBar1.Value = ProgressBar1.Maximum

    End Sub
    Private Sub CountStatus(ByVal FileCount As Long, ByVal FolderCount As Long)

        If _stopped Then Exit Sub

        CopyStatusLabel.Text = _copyStatus & "So far there are " + FileCount.ToString + " files in " + FolderCount.ToString + " folders."

    End Sub
    Private Sub CountCompleted(ByVal FileCount As Long, ByVal FolderCount As Long)


        _copyStatus = "Status: Copying. "
        CopyStatusLabel.Text = _copyStatus & "There are " + FileCount.ToString + " files in " + FolderCount.ToString + " folders."

        _totalFiles = FileCount
        _totalFolders = FolderCount

        ProgressBar1.Maximum = _totalFiles
        ProgressBar1.Value = 0

    End Sub
  
 

    Private Sub BackupCompleted()

        CopyStatusLabel.Text = "Backup copy completed."

        CopyFiles.StopThreads()
        CopyFiles = Nothing


        StartCopy.Enabled = True
        Panel1.Enabled = True
        StopCopy.Enabled = False

    End Sub
    Private Sub CopyFiles_CopyStatus(ByVal sender As Object, ByVal e As BackupEventArgs) Handles CopyFiles.CopyStatus

        Me.BeginInvoke(New CopyHandler(AddressOf CopyStatus), New Object() {e.FilePath, e.FileSize, e.FileCount})

    End Sub
    Private Sub CopyFiles_CopyCompleted(ByVal sender As Object, ByVal e As BackupEventArgs) Handles CopyFiles.CopyCompleted
 
        Me.BeginInvoke(New CopyHandler(AddressOf CopyCompleted), New Object() {e.FilePath, e.FileSize, e.FileCount})

    End Sub

    Private Sub CopyFiles_CountStatus(ByVal sender As Object, ByVal e As BackupEventArgs) Handles CopyFiles.CountStatus

        Me.BeginInvoke(New CountHandler(AddressOf CountStatus), New Object() {e.FileCount, e.FolderCount})

    End Sub
    Private Sub CopyFiles_CountCompleted(ByVal sender As Object, ByVal e As BackupEventArgs) Handles CopyFiles.CountCompleted

        Me.BeginInvoke(New CountHandler(AddressOf CountCompleted), New Object() {e.FileCount, e.FolderCount})

    End Sub

    Private Sub CopyFiles_BackupCompleted(ByVal sender As Object, ByVal e As BackupEventArgs) Handles CopyFiles.BackupCompleted

        Me.BeginInvoke(New BackupHandler(AddressOf BackupCompleted))
    End Sub
   


    Private Sub Backup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.CenterToScreen()
    End Sub

End Class