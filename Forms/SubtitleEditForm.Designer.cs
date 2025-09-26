namespace SubtitleTranslatorGUI.Forms
{
    partial class SubtitleEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubtitleEditForm));
            dgvEdit = new DataGridView();
            Number = new DataGridViewTextBoxColumn();
            TimeCode = new DataGridViewTextBoxColumn();
            OriginalText = new DataGridViewTextBoxColumn();
            TranslatedText = new DataGridViewTextBoxColumn();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEdit).BeginInit();
            SuspendLayout();
            // 
            // dgvEdit
            // 
            dgvEdit.AllowUserToAddRows = false;
            dgvEdit.AllowUserToDeleteRows = false;
            dgvEdit.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEdit.Columns.AddRange(new DataGridViewColumn[] { Number, TimeCode, OriginalText, TranslatedText });
            dgvEdit.Location = new Point(12, 12);
            dgvEdit.Name = "dgvEdit";
            dgvEdit.RowHeadersWidth = 51;
            dgvEdit.Size = new Size(984, 579);
            dgvEdit.TabIndex = 0;
            // 
            // Number
            // 
            Number.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Number.HeaderText = "Number";
            Number.MinimumWidth = 6;
            Number.Name = "Number";
            Number.ReadOnly = true;
            Number.Width = 92;
            // 
            // TimeCode
            // 
            TimeCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            TimeCode.HeaderText = "Time Code";
            TimeCode.MinimumWidth = 6;
            TimeCode.Name = "TimeCode";
            TimeCode.ReadOnly = true;
            TimeCode.Width = 110;
            // 
            // OriginalText
            // 
            OriginalText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            OriginalText.HeaderText = "Original Text";
            OriginalText.MinimumWidth = 6;
            OriginalText.Name = "OriginalText";
            OriginalText.ReadOnly = true;
            // 
            // TranslatedText
            // 
            TranslatedText.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            TranslatedText.HeaderText = "Traslated Text";
            TranslatedText.MinimumWidth = 6;
            TranslatedText.Name = "TranslatedText";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(902, 597);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // SubtitleEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 634);
            Controls.Add(btnSave);
            Controls.Add(dgvEdit);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SubtitleEditForm";
            Text = "Subtitle Edit Form";
            ((System.ComponentModel.ISupportInitialize)dgvEdit).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvEdit;
        private Button btnSave;
        private DataGridViewTextBoxColumn Number;
        private DataGridViewTextBoxColumn TimeCode;
        private DataGridViewTextBoxColumn OriginalText;
        private DataGridViewTextBoxColumn TranslatedText;
    }
}