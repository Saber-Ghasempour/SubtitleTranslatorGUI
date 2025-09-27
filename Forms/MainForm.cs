using SubtitleTranslatorGUI.Forms;
using SubtitleTranslatorGUI.Models;
using SubtitleTranslatorGUI.Services;
using System.Text;

namespace SubtitleTranslatorGUI
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource _cts;
        private bool _isPaused = false;
        private Task _processingTask;

        public MainForm()
        {
            InitializeComponent();
            ApiKeyManager.LoadApiKeys();
            cmbTargetLanguage.SelectedIndex = 0;
            cmbSourceLanguage.SelectedIndex = 9;
            LoggerService.LogAction = AppendLog;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                SetUIEnabled(false);

                btnStart.Text = "Pause";
                _cts = new CancellationTokenSource();
                _isPaused = false;
                _processingTask = ProcessFilesAsync(_cts.Token);
                await _processingTask;

                SetUIEnabled(true);
            }
            else if (btnStart.Text == "Pause")
            {
                _isPaused = true;
                btnStart.Text = "Resume";
            }
            else if (btnStart.Text == "Resume")
            {
                _isPaused = false;
                btnStart.Text = "Pause";
            }
        }

        private void btnAPIKeyManager_Click(object sender, EventArgs e)
        {
            using var dlg = new ApiKeyManagerForm();
            dlg.ShowDialog();
            ApiKeyManager.LoadApiKeys(); // Reload keys after changes
        }

        private void openFileMenu_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "Subtitle/Video Files|*.srt;*.mkv;*.mp4;*.avi";
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddFilesToList(dialog.FileNames);
            }
        }

        private void openFolderMenu_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            dialog.Description = "Select the folder containing subtitle (*.srt) or video (*.mkv, *.mp4, *.avi) files";
            dialog.UseDescriptionForTitle = true;
            dialog.ShowNewFolderButton = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                AddFilesFromFolder(dialog.SelectedPath);
            }
        }

        private void btnRemoveFromList_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvFiles.SelectedRows)
                dgvFiles.Rows.Remove(row);

            btnStart.Enabled = dgvFiles.Rows.Count > 0;
        }

        private void dgvFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void dgvFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                var allFiles = FileGridService.CollectFilesFromDrop(paths);
                AddFilesToList(allFiles.ToArray());
            }
        }

        private void dgvFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvFiles.Columns["EditColumn"].Index)
                return;

            var row = dgvFiles.Rows[e.RowIndex];
            string originalPath = null, translatedPath = null;
            var fileItem = row.Tag as FileItem;

            if (fileItem.Type == FileType.Subtitle)
            {
                originalPath = fileItem.FilePath;
                var fName = Path.GetFileNameWithoutExtension(originalPath);
                string langCode = SubtitleProcessingService.GetTargetLangCode(cmbTargetLanguage.SelectedItem.ToString());
                translatedPath = Path.Combine(
                    Path.GetDirectoryName(originalPath)!,
                    $"{fName}_{langCode}.srt"
                );
            }
            else if (fileItem.Type == FileType.Video)
            {
                string videoPath = fileItem.FilePath;
                originalPath = GetOriginalSubtitlePath(videoPath);
                translatedPath = Path.Combine(Path.GetDirectoryName(videoPath), Path.GetFileNameWithoutExtension(videoPath) + ".srt");
            }

            if (File.Exists(originalPath) && File.Exists(translatedPath))
            {
                using var dlg = new SubtitleEditForm(originalPath, translatedPath);
                dlg.ShowDialog();
            }
            else
            {
                MessageBox.Show("Original or translated subtitle file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> WaitIfPausedOrCancelled(CancellationToken token)
        {
            while (_isPaused)
            {
                await Task.Delay(1000);
                if (token.IsCancellationRequested)
                    return true;
            }
            if (token.IsCancellationRequested)
                return true;
            return false;
        }

        private static string GetOriginalSubtitlePath(string videoPath)
        {
            return Path.Combine(Path.GetDirectoryName(videoPath), Path.GetFileNameWithoutExtension(videoPath) + "_original.srt");
        }

        public void AppendLog(string message)
        {
            if (txtLog.InvokeRequired)
                txtLog.Invoke(() => txtLog.AppendText(message + Environment.NewLine));
            else
                txtLog.AppendText(message + Environment.NewLine);
        }

        private void AddFilesFromFolder(string folderPath)
        {
            var files = FileGridService.GetSupportedFiles(folderPath);
            AddFilesToList(files);
        }

        private void AddFilesToList(string[] files)
        {
            FileGridService.AddFilesToGrid(dgvFiles, files);
            btnStart.Enabled = dgvFiles.Rows.Count > 0;
        }

        /// <summary>
        ///     Enables or disables all main UI controls during processing.
        /// </summary>
        /// <param name="enabled">True to enable controls, false to disable.</param>
        private void SetUIEnabled(bool enabled)
        {
            btnRemoveFromList.Enabled = enabled;
        }

        private async Task ProcessFilesAsync(CancellationToken token)
        {
            int batchSize = 100;
            pbFileProgress.Style = ProgressBarStyle.Blocks;
            pbFileProgress.Value = 0;
            pbFileProgress.Maximum = dgvFiles.Rows.Count;

            foreach (DataGridViewRow row in dgvFiles.Rows)
            {
                if (await WaitIfPausedOrCancelled(token)) return;

                row.Cells["Status"].Value = FileStatus.Processing.ToString();
                var fileItem = row.Tag as FileItem;

                if (fileItem.Type == FileType.Subtitle)
                {
                    await SubtitleProcessingService.ProcessSrtFile(
                        fileItem.FilePath,
                        batchSize,
                        row,
                        cmbSourceLanguage.SelectedItem.ToString(),
                        cmbTargetLanguage.SelectedItem.ToString(),
                        () => _isPaused,
                        token
                    );
                }
                else if (fileItem.Type == FileType.Video)
                {
                    string videoPath = fileItem.FilePath;
                    var tracks = fileItem.SubtitleTracks;

                    if (tracks.Count == 0 || row.Cells["SubtitleTrack"].Value.ToString() == "No subtitles")
                    {
                        AppendLog($"⚠️ No subtitle tracks found in video: {fileItem.FileName}. Sending audio to create subtitle.");
                        int chunkSeconds = 300;
                        var audioPath = Path.Combine(Path.GetDirectoryName(videoPath), Path.GetFileNameWithoutExtension(videoPath) + ".wav");
                        bool audioOk = FFmpegAudioService.ExtractAudio(videoPath, audioPath);
                        var audioChunks = FFmpegAudioService.SplitAudio(audioPath, chunkSeconds);
                        try { if (File.Exists(audioPath)) File.Delete(audioPath); } catch { /* Ignore */ }
                        var srtParts = new List<string>();
                        AppendLog($"The video splitted into {audioChunks.Count} parts");

                        int blockNumber = 1;
                        for (int i = 0; i < audioChunks.Count; i++)
                        {
                            var srtChunk = await SubtitleTranslator.SpeechToTextAsync(audioChunks[i], cmbTargetLanguage.SelectedItem.ToString(), token);
                            if (srtChunk != null) AppendLog($"✅ Subtitle extracted successfully from part {i + 1}.");
                            var offset = TimeSpan.FromSeconds(i * chunkSeconds);
                            var srtChunkWithOffset = FFmpegAudioService.OffsetSrtTimestamps(srtChunk, offset);
                            var srtChunkRenumbered = FFmpegAudioService.RenumberSrtBlocks(srtChunkWithOffset, blockNumber);
                            srtParts.Add(srtChunkRenumbered);

                            blockNumber += FFmpegAudioService.CountSrtBlocks(srtChunkRenumbered);
                            try { if (File.Exists(audioChunks[i])) File.Delete(audioChunks[i]); } catch { /* Ignore */ }
                        }

                        var finalSrt = string.Join("", srtParts);
                        string finalSrtPath = Path.Combine(Path.GetDirectoryName(videoPath), Path.GetFileNameWithoutExtension(videoPath) + ".srt");
                        File.WriteAllText(finalSrtPath, finalSrt, Encoding.UTF8);
                        AppendLog($"✅ Subtitle extracted successfully from {fileItem.FileName}.");
                    }
                    else
                    {
                        int selectedTrackIndex = tracks.IndexOf(row.Cells["SubtitleTrack"].Value?.ToString());
                        string srtPath = GetOriginalSubtitlePath(videoPath);

                        AppendLog($"Extracting subtitle from {fileItem.FileName} ...");

                        bool ok = await FFmpegSubtitleService.ExtractSubtitle(videoPath, selectedTrackIndex, srtPath);

                        if (ok)
                        {
                            AppendLog($"✅ Subtitle extracted successfully from {fileItem.FileName}.");
                            await SubtitleProcessingService.ProcessSrtFile(
                                srtPath,
                                batchSize,
                                row,
                                cmbSourceLanguage.SelectedItem.ToString(),
                                cmbTargetLanguage.SelectedItem.ToString(),
                                () => _isPaused,
                                token
                            );
                        }
                        else
                        {
                            AppendLog($"❌ There is an error with extracting subtitle from: {fileItem.FileName}.");
                            row.Cells["Status"].Value = FileStatus.SubtitleExtractionFailed.ToString();
                        }
                        pbFileProgress.Value++;
                    }
                }
            }
            btnStart.Text = "Start";
        }
    }
}
