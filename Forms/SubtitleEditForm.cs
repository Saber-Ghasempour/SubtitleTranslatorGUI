using SubtitleTranslatorGUI.Models;
using SubtitleTranslatorGUI.Utils;
using System.Text;

namespace SubtitleTranslatorGUI.Forms
{
    public partial class SubtitleEditForm : Form
    {
        private readonly string originalPath;
        private readonly string translatedPath;

        public SubtitleEditForm(string originalPath, string translatedPath)
        {
            InitializeComponent();
            this.originalPath = originalPath;
            this.translatedPath = translatedPath;
            LoadSubtitles();
        }

        private void LoadSubtitles()
        {
            var originalBlocks = SubtitleUtils.ReadSrtBlocks(originalPath);
            var translatedBlocks = SubtitleUtils.ReadSrtBlocks(translatedPath);

            dgvEdit.Rows.Clear();
            for (int i = 0; i < originalBlocks.Count; i++)
            {
                var orig = originalBlocks[i];
                var trans = i < translatedBlocks.Count ? string.Join(Environment.NewLine, translatedBlocks[i].TextLines) : "";
                dgvEdit.Rows.Add(orig.Number, orig.TimeCode, string.Join(Environment.NewLine, orig.TextLines), trans);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var blocks = new List<SrtBlock>();
            foreach (DataGridViewRow row in dgvEdit.Rows)
            {
                if (row.IsNewRow) continue;
                var block = new SrtBlock
                {
                    Number = row.Cells[0].Value?.ToString(),
                    TimeCode = row.Cells[1].Value?.ToString(),
                    TextLines = row.Cells[3].Value?.ToString()?.Split([Environment.NewLine], StringSplitOptions.None).ToList() ?? []
                };
                blocks.Add(block);
            }
            var srtText = SubtitleUtils.FormatBlocksToSrtText(blocks);
            File.WriteAllText(translatedPath, srtText, Encoding.UTF8);
            MessageBox.Show("Subtitle saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
