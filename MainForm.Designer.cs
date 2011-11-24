namespace TvTzRenameTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TestOutput = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.folderPath = new System.Windows.Forms.Button();
            this.FolderTextBox = new System.Windows.Forms.TextBox();
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.LoadFolder = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileTypeBox = new System.Windows.Forms.ComboBox();
            this.outputListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutCreditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DoRename = new System.Windows.Forms.Button();
            this.checkBoxTVrageLookup = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxKeepScene = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.BackgroundOutput = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundProcessFiles = new System.ComponentModel.BackgroundWorker();
            this.checkBoxCopy = new System.Windows.Forms.CheckBox();
            this.removeEpInfo = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.generateMoveCB1 = new System.Windows.Forms.CheckBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.TestSR = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestOutput
            // 
            this.TestOutput.Location = new System.Drawing.Point(343, 513);
            this.TestOutput.Name = "TestOutput";
            this.TestOutput.Size = new System.Drawing.Size(95, 23);
            this.TestOutput.TabIndex = 0;
            this.TestOutput.Text = "Test";
            this.TestOutput.UseVisualStyleBackColor = true;
            this.TestOutput.Click += new System.EventHandler(this.TestOutput_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Folder";
            // 
            // folderPath
            // 
            this.folderPath.Location = new System.Drawing.Point(87, 60);
            this.folderPath.Name = "folderPath";
            this.folderPath.Size = new System.Drawing.Size(100, 22);
            this.folderPath.TabIndex = 12;
            this.folderPath.Text = "Browse";
            this.folderPath.UseVisualStyleBackColor = true;
            this.folderPath.Click += new System.EventHandler(this.folderPath_Click);
            // 
            // FolderTextBox
            // 
            this.FolderTextBox.Location = new System.Drawing.Point(87, 31);
            this.FolderTextBox.Name = "FolderTextBox";
            this.FolderTextBox.Size = new System.Drawing.Size(258, 20);
            this.FolderTextBox.TabIndex = 13;
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.Location = new System.Drawing.Point(12, 98);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(429, 316);
            this.fileListBox.TabIndex = 0;
            // 
            // LoadFolder
            // 
            this.LoadFolder.Location = new System.Drawing.Point(193, 60);
            this.LoadFolder.Name = "LoadFolder";
            this.LoadFolder.Size = new System.Drawing.Size(100, 22);
            this.LoadFolder.TabIndex = 15;
            this.LoadFolder.Text = "Load";
            this.LoadFolder.UseVisualStyleBackColor = true;
            this.LoadFolder.Click += new System.EventHandler(this.LoadFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 576);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(894, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // fileTypeBox
            // 
            this.fileTypeBox.FormattingEnabled = true;
            this.fileTypeBox.Location = new System.Drawing.Point(351, 31);
            this.fileTypeBox.Name = "fileTypeBox";
            this.fileTypeBox.Size = new System.Drawing.Size(90, 21);
            this.fileTypeBox.TabIndex = 17;
            // 
            // outputListBox
            // 
            this.outputListBox.FormattingEnabled = true;
            this.outputListBox.Location = new System.Drawing.Point(447, 98);
            this.outputListBox.Name = "outputListBox";
            this.outputListBox.Size = new System.Drawing.Size(430, 316);
            this.outputListBox.TabIndex = 18;
            this.outputListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.outputListBox_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Input";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(447, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Output";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(894, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.aboutCreditsToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // aboutCreditsToolStripMenuItem
            // 
            this.aboutCreditsToolStripMenuItem.Name = "aboutCreditsToolStripMenuItem";
            this.aboutCreditsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.aboutCreditsToolStripMenuItem.Text = "About / Credits";
            this.aboutCreditsToolStripMenuItem.Click += new System.EventHandler(this.aboutCreditsToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DoRename
            // 
            this.DoRename.Location = new System.Drawing.Point(342, 536);
            this.DoRename.Name = "DoRename";
            this.DoRename.Size = new System.Drawing.Size(200, 23);
            this.DoRename.TabIndex = 22;
            this.DoRename.Text = "Engage !";
            this.DoRename.UseVisualStyleBackColor = true;
            this.DoRename.Click += new System.EventHandler(this.DoRename_Click);
            // 
            // checkBoxTVrageLookup
            // 
            this.checkBoxTVrageLookup.AutoSize = true;
            this.checkBoxTVrageLookup.Location = new System.Drawing.Point(12, 491);
            this.checkBoxTVrageLookup.Name = "checkBoxTVrageLookup";
            this.checkBoxTVrageLookup.Size = new System.Drawing.Size(190, 17);
            this.checkBoxTVrageLookup.TabIndex = 23;
            this.checkBoxTVrageLookup.Text = "Lookup episode name(s) on tvrage";
            this.checkBoxTVrageLookup.UseVisualStyleBackColor = true;
            this.checkBoxTVrageLookup.CheckedChanged += new System.EventHandler(this.checkBoxTVrageLookup_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 464);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Rename Options:";
            // 
            // checkBoxKeepScene
            // 
            this.checkBoxKeepScene.AutoSize = true;
            this.checkBoxKeepScene.Checked = true;
            this.checkBoxKeepScene.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKeepScene.Location = new System.Drawing.Point(12, 517);
            this.checkBoxKeepScene.Name = "checkBoxKeepScene";
            this.checkBoxKeepScene.Size = new System.Drawing.Size(103, 17);
            this.checkBoxKeepScene.TabIndex = 25;
            this.checkBoxKeepScene.Text = "Keep scene info";
            this.checkBoxKeepScene.UseVisualStyleBackColor = true;
            this.checkBoxKeepScene.CheckedChanged += new System.EventHandler(this.checkBoxKeepScene_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(-9, 565);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(907, 10);
            this.progressBar.TabIndex = 26;
            // 
            // BackgroundOutput
            // 
            this.BackgroundOutput.WorkerReportsProgress = true;
            this.BackgroundOutput.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundOutput_DoWork);
            this.BackgroundOutput.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundOutput_ProgressChanged);
            this.BackgroundOutput.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundOutput_RunWorkerComplete);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.editToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Enabled = false;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // backgroundProcessFiles
            // 
            this.backgroundProcessFiles.WorkerReportsProgress = true;
            this.backgroundProcessFiles.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundProcessFiles_DoWork);
            this.backgroundProcessFiles.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundProcessFiles_ProgressChanged);
            this.backgroundProcessFiles.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundProcessFiles_RunWorkerComplete);
            // 
            // checkBoxCopy
            // 
            this.checkBoxCopy.AutoSize = true;
            this.checkBoxCopy.Location = new System.Drawing.Point(200, 540);
            this.checkBoxCopy.Name = "checkBoxCopy";
            this.checkBoxCopy.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxCopy.Size = new System.Drawing.Size(137, 17);
            this.checkBoxCopy.TabIndex = 27;
            this.checkBoxCopy.Text = "Copy instead of rename";
            this.checkBoxCopy.UseVisualStyleBackColor = true;
            // 
            // removeEpInfo
            // 
            this.removeEpInfo.AutoSize = true;
            this.removeEpInfo.Location = new System.Drawing.Point(12, 540);
            this.removeEpInfo.Name = "removeEpInfo";
            this.removeEpInfo.Size = new System.Drawing.Size(139, 17);
            this.removeEpInfo.TabIndex = 28;
            this.removeEpInfo.Text = "Remove all episode info";
            this.removeEpInfo.UseVisualStyleBackColor = true;
            this.removeEpInfo.CheckedChanged += new System.EventHandler(this.removeEpInfo_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 426);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Search and Replace:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(197, 464);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "File options:";
            // 
            // generateMoveCB1
            // 
            this.generateMoveCB1.AutoSize = true;
            this.generateMoveCB1.Enabled = false;
            this.generateMoveCB1.Location = new System.Drawing.Point(197, 517);
            this.generateMoveCB1.Name = "generateMoveCB1";
            this.generateMoveCB1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.generateMoveCB1.Size = new System.Drawing.Size(140, 17);
            this.generateMoveCB1.TabIndex = 31;
            this.generateMoveCB1.Text = "Generate move batches";
            this.generateMoveCB1.UseVisualStyleBackColor = true;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(447, 448);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(430, 20);
            this.textBoxSearch.TabIndex = 32;
            this.textBoxSearch.Text = "Search";
            this.textBoxSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxReplace
            // 
            this.textBoxReplace.Location = new System.Drawing.Point(447, 478);
            this.textBoxReplace.Name = "textBoxReplace";
            this.textBoxReplace.Size = new System.Drawing.Size(430, 20);
            this.textBoxReplace.TabIndex = 33;
            this.textBoxReplace.Text = "Replace";
            this.textBoxReplace.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TestSR
            // 
            this.TestSR.Location = new System.Drawing.Point(447, 513);
            this.TestSR.Name = "TestSR";
            this.TestSR.Size = new System.Drawing.Size(95, 23);
            this.TestSR.TabIndex = 34;
            this.TestSR.Text = "Test";
            this.TestSR.UseVisualStyleBackColor = true;
            this.TestSR.Click += new System.EventHandler(this.TestSR_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 598);
            this.Controls.Add(this.textBoxReplace);
            this.Controls.Add(this.TestSR);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.generateMoveCB1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.removeEpInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxKeepScene);
            this.Controls.Add(this.checkBoxCopy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxTVrageLookup);
            this.Controls.Add(this.DoRename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.outputListBox);
            this.Controls.Add(this.fileTypeBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.LoadFolder);
            this.Controls.Add(this.fileListBox);
            this.Controls.Add(this.FolderTextBox);
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TestOutput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "TvTz Rename Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion

        private System.Windows.Forms.Button TestOutput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button folderPath;
        private System.Windows.Forms.TextBox FolderTextBox;
        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Button LoadFolder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox fileTypeBox;
        private System.Windows.Forms.ListBox outputListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button DoRename;
        private System.Windows.Forms.CheckBox checkBoxTVrageLookup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxKeepScene;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker BackgroundOutput;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundProcessFiles;
        private System.Windows.Forms.CheckBox checkBoxCopy;
        private System.Windows.Forms.CheckBox removeEpInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox generateMoveCB1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.TextBox textBoxReplace;
        private System.Windows.Forms.Button TestSR;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutCreditsToolStripMenuItem;
    }
}

