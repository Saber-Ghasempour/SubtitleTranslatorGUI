using SubtitleTranslatorGUI.Services;
using System.Diagnostics;

namespace SubtitleTranslatorGUI
{
    public partial class ApiKeyManagerForm : Form
    {
        public ApiKeyManagerForm()
        {
            InitializeComponent();
            LoadKeys();
        }

        void LoadKeys()
        {
            lbKeys.Items.Clear();
            foreach (var key in ApiKeyManager.ApiKeys)
                lbKeys.Items.Add(key);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newKey = txtKey.Text.Trim();
            if (!string.IsNullOrEmpty(newKey) && !ApiKeyManager.ApiKeys.Contains(newKey))
            {
                ApiKeyManager.ApiKeys.Add(newKey);
                ApiKeyManager.SaveApiKeys();
                LoadKeys();
                txtKey.Clear();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbKeys.SelectedItem is string key)
            {
                ApiKeyManager.ApiKeys.Remove(key);
                ApiKeyManager.SaveApiKeys();
                LoadKeys();
            }
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            if (txtKey.Text.Length > 0)
                btnAdd.Enabled = true;
            else
                btnAdd.Enabled = false;
        }

        private void lbKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbKeys.SelectedIndex >= 0)
                btnRemove.Enabled = true;
            else
                btnRemove.Enabled = false;
        }

        private void linkGetApiKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://aistudio.google.com/u/0/apikey",
                UseShellExecute = true
            });
        }
    }
}
