using SubtitleTranslatorGUI.Models;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SubtitleTranslatorGUI.Services
{
    /// <summary>
    ///     A service for managing file operations in a DataGridView, including populating the grid with subtitle and video files,
    /// </summary>
    internal static class FileGridService
    {
        private static readonly string[] SubtitleExtensions = { ".srt" };
        private static readonly string[] VideoExtensions = { ".mkv", ".mp4", ".avi" };

        /// <summary>
        ///     Populates the given DataGridView with subtitle and video files from the specified folder path.
        /// </summary>
        /// <param name="dgvFiles">
        ///     A DataGridView control to be populated with file information.
        /// </param>
        /// <param name="folderPath">
        ///     The path to the folder from which to retrieve files.
        /// </param>
        public static void PopulateFileGrid(DataGridView dgvFiles, string folderPath)
        {
            dgvFiles.Rows.Clear();
            var files = GetSupportedFiles(folderPath);
            AddFilesToGrid(dgvFiles, files);
        }

        /// <summary>
        ///     Gets all supported subtitle and video files from the specified folder path, including subdirectories.
        /// </summary>
        /// <param name="folderPath">
        ///     A string representing the path to the folder.
        /// </param>
        /// <returns>
        ///     A string array containing the paths of all supported subtitle and video files found in the folder and its subdirectories.
        /// </returns>
        public static string[] GetSupportedFiles(string folderPath)
        {
            var subtitleFiles = Directory.GetFiles(folderPath, "*.srt", SearchOption.AllDirectories);
            var videoFiles = VideoExtensions
                .SelectMany(ext => Directory.GetFiles(folderPath, $"*{ext}", SearchOption.AllDirectories))
                .ToArray();
            return subtitleFiles.Concat(videoFiles).ToArray();
        }

        /// <summary>
        ///     A method to add files to a DataGridView, avoiding duplicates and handling subtitle and video files differently.
        /// </summary>
        /// <param name="dgvFiles">
        ///     A DataGridView control to which files will be added.
        /// </param>
        /// <param name="files">
        ///     A collection of file paths to be added to the DataGridView.
        /// </param>
        public static void AddFilesToGrid(DataGridView dgvFiles, IEnumerable<string> files)
        {
            var existingFiles = new HashSet<string>(
                dgvFiles.Rows
                    .OfType<DataGridViewRow>()
                    .Select(r => r.Cells["FileName"].Value?.ToString() ?? string.Empty),
                System.StringComparer.OrdinalIgnoreCase
            );

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (existingFiles.Contains(fileName))
                    continue;

                string ext = Path.GetExtension(file).ToLowerInvariant();
                if (SubtitleExtensions.Contains(ext))
                {
                    int idx = dgvFiles.Rows.Add();
                    dgvFiles.Rows[idx].Cells["FileName"].Value = fileName;
                    dgvFiles.Rows[idx].Cells["Type"].Value = FileType.Subtitle;
                    dgvFiles.Rows[idx].Cells["SubtitleTrack"].Value = "";
                    dgvFiles.Rows[idx].Cells["Status"].Value = FileStatus.Queued;
                    dgvFiles.Rows[idx].Tag = new FileItem
                    {
                        FileName = fileName,
                        FilePath = file,
                        Type = FileType.Subtitle,
                        Status = FileStatus.Queued
                    };
                }
                else if (VideoExtensions.Contains(ext))
                {
                    var tracks = FFmpegSubtitleService.GetSubtitleTracks(file);
                    int idx = dgvFiles.Rows.Add();
                    dgvFiles.Rows[idx].Cells["FileName"].Value = fileName;
                    dgvFiles.Rows[idx].Cells["Type"].Value = FileType.Video;
                    var comboCell = (DataGridViewComboBoxCell)dgvFiles.Rows[idx].Cells["SubtitleTrack"];
                    comboCell.Items.AddRange(tracks.Count > 0 ? tracks.ToArray() : new[] { "No subtitles" });
                    comboCell.Value = tracks.Count > 0 ? tracks[0] : "No subtitles";
                    dgvFiles.Rows[idx].Cells["Status"].Value = FileStatus.Queued;
                    dgvFiles.Rows[idx].Tag = new FileItem
                    {
                        FileName = fileName,
                        FilePath = file,
                        Type = FileType.Video,
                        SubtitleTracks = tracks,
                        SelectedTrack = tracks.Count > 0 ? tracks[0] : "No subtitles",
                        Status = FileStatus.Queued
                    };
                }
            }
        }

        /// <summary>
        ///     A method to collect supported files from an array of paths, handling both files and directories.
        /// </summary>
        /// <param name="paths">
        ///     A string array containing file and/or directory paths.
        /// </param>
        /// <returns>
        ///     A list of strings representing the paths of all supported files collected from the provided paths.
        /// </returns>
        public static List<string> CollectFilesFromDrop(string[] paths)
        {
            var allFiles = new List<string>();
            foreach (var path in paths)
            {
                if (Directory.Exists(path))
                {
                    allFiles.AddRange(GetSupportedFiles(path));
                }
                else if (File.Exists(path))
                {
                    allFiles.Add(path);
                }
            }
            return allFiles;
        }
    }
}
