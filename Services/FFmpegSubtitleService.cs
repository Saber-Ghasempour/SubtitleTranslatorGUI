using System.ComponentModel;
using System.Diagnostics;

namespace SubtitleTranslatorGUI.Services
{
    /// <summary>
    ///     A service class for handling subtitle extraction using FFmpeg/FFprobe.
    /// </summary>
    internal class FFmpegSubtitleService
    {
        /// <summary>
        ///     Gets a list of subtitle tracks from the specified video file using FFprobe.
        /// </summary>
        /// <param name="videoPath">
        ///     A string representing the path to the video file.
        /// </param>
        /// <returns>
        ///     A list of strings, each representing a subtitle track with its index, codec, language, and title (if available).
        /// </returns>
        public static List<string> GetSubtitleTracks(string videoPath)
        {
            var tracks = new List<string>();
            var psi = new ProcessStartInfo
            {
                FileName = "ffprobe",
                Arguments = $"-v error -select_streams s -show_entries stream=index,codec_name,language:stream_tags=title -of json \"{videoPath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            try
            {
                using var proc = Process.Start(psi);
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                using var doc = System.Text.Json.JsonDocument.Parse(output);
                if (doc.RootElement.TryGetProperty("streams", out var streams))
                {
                    foreach (var stream in streams.EnumerateArray())
                    {
                        string index = stream.GetProperty("index").ToString();
                        string codec = stream.TryGetProperty("codec_name", out var c) ? c.ToString() : "";
                        string lang = stream.TryGetProperty("tags", out var tags) && tags.TryGetProperty("language", out var l) ? l.ToString() : "";
                        string title = stream.TryGetProperty("tags", out var tags2) && tags2.TryGetProperty("title", out var t) ? t.ToString() : "";
                        string display = $"Track {index}: {codec}";
                        if (!string.IsNullOrEmpty(lang)) display += $" [{lang}]";
                        if (!string.IsNullOrEmpty(title)) display += $" - {title}";
                        tracks.Add(display);
                    }
                }
            }
            catch (Win32Exception)
            {
                MessageBox.Show(
                    "FFprobe is not installed or not found in PATH.\nPlease download and install FFmpeg/FFprobe from: https://ffmpeg.org/download.html",
                    "FFprobe Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            return tracks;
        }

        /// <summary>
        ///     Extracts the specified subtitle track from the video file and saves it as an SRT file using FFmpeg.
        /// </summary>
        /// <param name="videoPath">
        ///     A string representing the path to the video file.
        /// </param>
        /// <param name="trackIndex">
        ///     Integer index of the subtitle track to extract.
        /// </param>
        /// <param name="outputSrt">
        ///     A string representing the path where the extracted SRT file will be saved.
        /// </param>
        /// <returns>
        ///     Returns true if the extraction is successful and the SRT file is created; otherwise, false.
        /// </returns>
        public static async Task<bool> ExtractSubtitle(string videoPath, int trackIndex, string outputSrt)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-y -i \"{videoPath}\" -map 0:s:{trackIndex} \"{outputSrt}\"",
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi);
            string errorOutput = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();
            proc.Dispose();

            Debug.WriteLine(errorOutput);

            return File.Exists(outputSrt) && new FileInfo(outputSrt).Length > 0;
        }
    }
}
