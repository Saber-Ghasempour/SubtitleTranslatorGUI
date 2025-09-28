namespace SubtitleTranslatorGUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            cmbTargetLanguage = new ComboBox();
            txtLog = new TextBox();
            pbFileProgress = new ProgressBar();
            btnStart = new Button();
            btnRemoveFromList = new Button();
            toolStrip1 = new ToolStrip();
            fileMenu = new ToolStripDropDownButton();
            openFileMenu = new ToolStripMenuItem();
            openFolderMenu = new ToolStripMenuItem();
            btnAPIKeyManager = new ToolStripButton();
            dgvFiles = new DataGridView();
            FileName = new DataGridViewTextBoxColumn();
            Type = new DataGridViewTextBoxColumn();
            SubtitleTrack = new DataGridViewComboBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            EditColumn = new DataGridViewButtonColumn();
            label1 = new Label();
            label2 = new Label();
            cmbSourceLanguage = new ComboBox();
            lblVersion = new ToolStripLabel();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFiles).BeginInit();
            SuspendLayout();
            // 
            // cmbTargetLanguage
            // 
            cmbTargetLanguage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmbTargetLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTargetLanguage.FormattingEnabled = true;
            cmbTargetLanguage.Items.AddRange(new object[] { "Persian (fa)", "Arabic (ar)", "French (fr)", "German (de)", "Spanish (es)", "Turkish (tr)", "Russian (ru)", "Chinese (zh)", "Japanese (ja)", "English (en)" });
            cmbTargetLanguage.Location = new Point(933, 265);
            cmbTargetLanguage.Name = "cmbTargetLanguage";
            cmbTargetLanguage.Size = new Size(196, 28);
            cmbTargetLanguage.TabIndex = 8;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Bottom;
            txtLog.Location = new Point(0, 318);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(1241, 435);
            txtLog.TabIndex = 0;
            // 
            // pbFileProgress
            // 
            pbFileProgress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbFileProgress.Location = new Point(12, 298);
            pbFileProgress.Name = "pbFileProgress";
            pbFileProgress.Size = new Size(1217, 14);
            pbFileProgress.TabIndex = 2;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStart.Enabled = false;
            btnStart.Location = new Point(1135, 265);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnRemoveFromList
            // 
            btnRemoveFromList.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRemoveFromList.Location = new Point(12, 265);
            btnRemoveFromList.Name = "btnRemoveFromList";
            btnRemoveFromList.Size = new Size(94, 29);
            btnRemoveFromList.TabIndex = 5;
            btnRemoveFromList.Text = "Remove";
            btnRemoveFromList.UseVisualStyleBackColor = true;
            btnRemoveFromList.Click += btnRemoveFromList_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { fileMenu, btnAPIKeyManager, lblVersion });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1241, 27);
            toolStrip1.TabIndex = 6;
            toolStrip1.Text = "toolStrip1";
            // 
            // fileMenu
            // 
            fileMenu.DisplayStyle = ToolStripItemDisplayStyle.Text;
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { openFileMenu, openFolderMenu });
            fileMenu.Image = (Image)resources.GetObject("fileMenu.Image");
            fileMenu.ImageTransparentColor = Color.Magenta;
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(46, 24);
            fileMenu.Text = "File";
            // 
            // openFileMenu
            // 
            openFileMenu.Name = "openFileMenu";
            openFileMenu.Size = new Size(174, 26);
            openFileMenu.Text = "Open File";
            openFileMenu.Click += openFileMenu_Click;
            // 
            // openFolderMenu
            // 
            openFolderMenu.Name = "openFolderMenu";
            openFolderMenu.Size = new Size(174, 26);
            openFolderMenu.Text = "Open Folder";
            openFolderMenu.Click += openFolderMenu_Click;
            // 
            // btnAPIKeyManager
            // 
            btnAPIKeyManager.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnAPIKeyManager.Image = (Image)resources.GetObject("btnAPIKeyManager.Image");
            btnAPIKeyManager.ImageTransparentColor = Color.Magenta;
            btnAPIKeyManager.Name = "btnAPIKeyManager";
            btnAPIKeyManager.Size = new Size(122, 24);
            btnAPIKeyManager.Text = "APIKey Manager";
            btnAPIKeyManager.Click += btnAPIKeyManager_Click;
            // 
            // dgvFiles
            // 
            dgvFiles.AllowDrop = true;
            dgvFiles.AllowUserToAddRows = false;
            dgvFiles.AllowUserToDeleteRows = false;
            dgvFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFiles.Columns.AddRange(new DataGridViewColumn[] { FileName, Type, SubtitleTrack, Status, EditColumn });
            dgvFiles.Location = new Point(0, 27);
            dgvFiles.Name = "dgvFiles";
            dgvFiles.RowHeadersWidth = 51;
            dgvFiles.Size = new Size(1241, 229);
            dgvFiles.TabIndex = 7;
            dgvFiles.CellContentClick += dgvFiles_CellContentClick;
            dgvFiles.DragDrop += dgvFiles_DragDrop;
            dgvFiles.DragEnter += dgvFiles_DragEnter;
            // 
            // FileName
            // 
            FileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FileName.HeaderText = "File Name";
            FileName.MinimumWidth = 6;
            FileName.Name = "FileName";
            // 
            // Type
            // 
            Type.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Type.HeaderText = "Type";
            Type.MinimumWidth = 6;
            Type.Name = "Type";
            Type.Width = 69;
            // 
            // SubtitleTrack
            // 
            SubtitleTrack.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            SubtitleTrack.HeaderText = "Subtitle Track";
            SubtitleTrack.MinimumWidth = 6;
            SubtitleTrack.Name = "SubtitleTrack";
            SubtitleTrack.Width = 104;
            // 
            // Status
            // 
            Status.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Status.HeaderText = "Status";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.Width = 78;
            // 
            // EditColumn
            // 
            EditColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            EditColumn.HeaderText = "Edit";
            EditColumn.MinimumWidth = 6;
            EditColumn.Name = "EditColumn";
            EditColumn.Text = "Edit";
            EditColumn.UseColumnTextForButtonValue = true;
            EditColumn.Width = 41;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(808, 268);
            label1.Name = "label1";
            label1.Size = new Size(119, 20);
            label1.TabIndex = 9;
            label1.Text = "Target Language";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(465, 268);
            label2.Name = "label2";
            label2.Size = new Size(123, 20);
            label2.TabIndex = 11;
            label2.Text = "Source Language";
            // 
            // cmbSourceLanguage
            // 
            cmbSourceLanguage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmbSourceLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSourceLanguage.FormattingEnabled = true;
            cmbSourceLanguage.Items.AddRange(new object[] { "Persian (fa)", "Arabic (ar)", "French (fr)", "German (de)", "Spanish (es)", "Turkish (tr)", "Russian (ru)", "Chinese (zh)", "Japanese (ja)", "English (en)" });
            cmbSourceLanguage.Location = new Point(590, 265);
            cmbSourceLanguage.Name = "cmbSourceLanguage";
            cmbSourceLanguage.Size = new Size(196, 28);
            cmbSourceLanguage.TabIndex = 10;
            // 
            // lblVersion
            // 
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(18, 24);
            lblVersion.Text = "V";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1241, 753);
            Controls.Add(label2);
            Controls.Add(cmbSourceLanguage);
            Controls.Add(label1);
            Controls.Add(cmbTargetLanguage);
            Controls.Add(dgvFiles);
            Controls.Add(toolStrip1);
            Controls.Add(btnRemoveFromList);
            Controls.Add(btnStart);
            Controls.Add(pbFileProgress);
            Controls.Add(txtLog);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1200, 800);
            Name = "MainForm";
            Text = "Subtitle Translator Using AI (Gemini)";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFiles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLog;
        private ProgressBar pbFileProgress;
        private Button btnStart;
        private TextBox txtFolder;
        private Button btnRemoveFromList;
        private ToolStrip toolStrip1;
        private ToolStripButton btnAPIKeyManager;
        private DataGridView dgvFiles;
        private ComboBox cmbTargetLanguage;
        private ToolStripDropDownButton fileMenu;
        private ToolStripMenuItem openFileMenu;
        private ToolStripMenuItem openFolderMenu;
        private Label label1;
        private Label label2;
        private ComboBox cmbSourceLanguage;
        private DataGridViewTextBoxColumn FileName;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewComboBoxColumn SubtitleTrack;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewButtonColumn EditColumn;
        private ToolStripLabel lblVersion;
    }
}
