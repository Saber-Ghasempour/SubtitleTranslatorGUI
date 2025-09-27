namespace SubtitleTranslatorGUI
{
    partial class ApiKeyManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApiKeyManagerForm));
            lbKeys = new ListBox();
            btnRemove = new Button();
            btnAdd = new Button();
            txtKey = new TextBox();
            gbAdd = new GroupBox();
            linkGetApiKey = new LinkLabel();
            gbAdd.SuspendLayout();
            SuspendLayout();
            // 
            // lbKeys
            // 
            lbKeys.FormattingEnabled = true;
            lbKeys.Location = new Point(12, 12);
            lbKeys.Name = "lbKeys";
            lbKeys.Size = new Size(776, 324);
            lbKeys.TabIndex = 0;
            lbKeys.SelectedIndexChanged += lbKeys_SelectedIndexChanged;
            // 
            // btnRemove
            // 
            btnRemove.Enabled = false;
            btnRemove.Location = new Point(694, 342);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(94, 29);
            btnRemove.TabIndex = 1;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnAdd
            // 
            btnAdd.Enabled = false;
            btnAdd.Location = new Point(676, 28);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtKey
            // 
            txtKey.Location = new Point(6, 29);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(664, 27);
            txtKey.TabIndex = 3;
            txtKey.TextChanged += txtKey_TextChanged;
            // 
            // gbAdd
            // 
            gbAdd.Controls.Add(btnAdd);
            gbAdd.Controls.Add(txtKey);
            gbAdd.Location = new Point(12, 377);
            gbAdd.Name = "gbAdd";
            gbAdd.Size = new Size(776, 67);
            gbAdd.TabIndex = 4;
            gbAdd.TabStop = false;
            gbAdd.Text = "Add Key";
            // 
            // linkGetApiKey
            // 
            linkGetApiKey.AutoSize = true;
            linkGetApiKey.Location = new Point(12, 346);
            linkGetApiKey.Name = "linkGetApiKey";
            linkGetApiKey.Size = new Size(265, 20);
            linkGetApiKey.TabIndex = 5;
            linkGetApiKey.TabStop = true;
            linkGetApiKey.Text = "Get the API Key from Google AI Studio";
            linkGetApiKey.LinkClicked += linkGetApiKey_LinkClicked;
            // 
            // ApiKeyManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 454);
            Controls.Add(linkGetApiKey);
            Controls.Add(gbAdd);
            Controls.Add(btnRemove);
            Controls.Add(lbKeys);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ApiKeyManagerForm";
            Text = "Api Key Manager Form";
            gbAdd.ResumeLayout(false);
            gbAdd.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbKeys;
        private Button btnRemove;
        private Button btnAdd;
        private TextBox txtKey;
        private GroupBox gbAdd;
        private LinkLabel linkGetApiKey;
    }
}