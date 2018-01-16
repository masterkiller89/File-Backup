<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Backup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BrowseTo = New System.Windows.Forms.Button
        Me.ToPathTextbox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.BrowseFrom = New System.Windows.Forms.Button
        Me.FromPathTextbox = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.FileStatusTextbox = New System.Windows.Forms.TextBox
        Me.CopyStatusLabel = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.StartCopy = New System.Windows.Forms.Button
        Me.StopCopy = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BrowseTo)
        Me.Panel1.Controls.Add(Me.ToPathTextbox)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.BrowseFrom)
        Me.Panel1.Controls.Add(Me.FromPathTextbox)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.ForeColor = System.Drawing.Color.Blue
        Me.Panel1.Location = New System.Drawing.Point(7, 19)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(422, 58)
        Me.Panel1.TabIndex = 6
        '
        'BrowseTo
        '
        Me.BrowseTo.ForeColor = System.Drawing.Color.Black
        Me.BrowseTo.Location = New System.Drawing.Point(339, 27)
        Me.BrowseTo.Name = "BrowseTo"
        Me.BrowseTo.Size = New System.Drawing.Size(75, 23)
        Me.BrowseTo.TabIndex = 11
        Me.BrowseTo.Text = "Browse"
        Me.BrowseTo.UseVisualStyleBackColor = True
        '
        'ToPathTextbox
        '
        Me.ToPathTextbox.Location = New System.Drawing.Point(84, 28)
        Me.ToPathTextbox.Name = "ToPathTextbox"
        Me.ToPathTextbox.ReadOnly = True
        Me.ToPathTextbox.Size = New System.Drawing.Size(249, 20)
        Me.ToPathTextbox.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(0, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "To Folder:"
        '
        'BrowseFrom
        '
        Me.BrowseFrom.ForeColor = System.Drawing.Color.Black
        Me.BrowseFrom.Location = New System.Drawing.Point(339, 2)
        Me.BrowseFrom.Name = "BrowseFrom"
        Me.BrowseFrom.Size = New System.Drawing.Size(75, 23)
        Me.BrowseFrom.TabIndex = 8
        Me.BrowseFrom.Text = "Browse"
        Me.BrowseFrom.UseVisualStyleBackColor = True
        '
        'FromPathTextbox
        '
        Me.FromPathTextbox.Location = New System.Drawing.Point(84, 4)
        Me.FromPathTextbox.Name = "FromPathTextbox"
        Me.FromPathTextbox.ReadOnly = True
        Me.FromPathTextbox.Size = New System.Drawing.Size(249, 20)
        Me.FromPathTextbox.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(0, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "From Folder:"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.CopyStatusLabel)
        Me.Panel2.Controls.Add(Me.ProgressBar1)
        Me.Panel2.Location = New System.Drawing.Point(7, 97)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(422, 64)
        Me.Panel2.TabIndex = 9
        '
        'FileStatusTextbox
        '
        Me.FileStatusTextbox.BackColor = System.Drawing.SystemColors.Control
        Me.FileStatusTextbox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FileStatusTextbox.Location = New System.Drawing.Point(14, 167)
        Me.FileStatusTextbox.Multiline = True
        Me.FileStatusTextbox.Name = "FileStatusTextbox"
        Me.FileStatusTextbox.ReadOnly = True
        Me.FileStatusTextbox.Size = New System.Drawing.Size(408, 23)
        Me.FileStatusTextbox.TabIndex = 10
        '
        'CopyStatusLabel
        '
        Me.CopyStatusLabel.AutoSize = True
        Me.CopyStatusLabel.Location = New System.Drawing.Point(3, 11)
        Me.CopyStatusLabel.Name = "CopyStatusLabel"
        Me.CopyStatusLabel.Size = New System.Drawing.Size(97, 13)
        Me.CopyStatusLabel.TabIndex = 9
        Me.CopyStatusLabel.Text = "Status: Not Started"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(5, 27)
        Me.ProgressBar1.Maximum = 0
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(409, 23)
        Me.ProgressBar1.TabIndex = 8
        '
        'StartCopy
        '
        Me.StartCopy.Enabled = False
        Me.StartCopy.Location = New System.Drawing.Point(227, 196)
        Me.StartCopy.Name = "StartCopy"
        Me.StartCopy.Size = New System.Drawing.Size(80, 23)
        Me.StartCopy.TabIndex = 10
        Me.StartCopy.Text = "Go"
        Me.StartCopy.UseVisualStyleBackColor = True
        '
        'StopCopy
        '
        Me.StopCopy.Enabled = False
        Me.StopCopy.Location = New System.Drawing.Point(123, 196)
        Me.StopCopy.Name = "StopCopy"
        Me.StopCopy.Size = New System.Drawing.Size(80, 23)
        Me.StopCopy.TabIndex = 11
        Me.StopCopy.Text = "Cancel Copy"
        Me.StopCopy.UseVisualStyleBackColor = True
        '
        'Backup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 229)
        Me.Controls.Add(Me.FileStatusTextbox)
        Me.Controls.Add(Me.StopCopy)
        Me.Controls.Add(Me.StartCopy)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Backup"
        Me.Text = "File Backup"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BrowseTo As System.Windows.Forms.Button
    Friend WithEvents ToPathTextbox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BrowseFrom As System.Windows.Forms.Button
    Friend WithEvents FromPathTextbox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents CopyStatusLabel As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents StartCopy As System.Windows.Forms.Button
    Friend WithEvents StopCopy As System.Windows.Forms.Button
    Friend WithEvents FileStatusTextbox As System.Windows.Forms.TextBox

End Class
