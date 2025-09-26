using SubtitleTranslatorGUI.Models;
using SubtitleTranslatorGUI.Utils;
using System.Text;

namespace SubtitleTranslatorGUI.Services
{
    /// <summary>
    ///     A service class for processing subtitle files, including reading, translating in batches, and saving the output.
    /// </summary>
    internal static class SubtitleProcessingService
    {
        /// <summary>
        ///     Processes a subtitle file by reading its content, translating it in batches, and saving the translated output.
        /// </summary>
        /// <param name="srtPath">
        ///     Path to the subtitle file to be processed.
        /// </param>
        /// <param name="batchSize">
        ///     A number indicating how many subtitle blocks to process in each batch.
        /// </param>
        /// <param name="row">
        ///     A DataGridViewRow object to update the status of the processing.
        /// </param>
        /// <param name="token">
        ///     A CancellationToken to handle cancellation requests.
        /// </param>
        /// <param name="sourceLang">
        ///     A string representing the source language code.
        /// </param>
        /// <param name="targetLang">
        ///     A string representing the target language code.
        /// </param>
        /// <returns>
        ///     Returns true if the processing completes successfully, otherwise false if cancelled.
        /// </returns>
        public static async Task<bool> ProcessSrtFile(
           string srtPath,
           int batchSize,
           DataGridViewRow row,
           string sourceLang,
           string targetLang,
           Action<int> reportProgress,
           Func<bool> isPaused,
           CancellationToken token)
        {
            var blocks = SubtitleUtils.ReadSrtBlocks(srtPath);
            var fName = Path.GetFileNameWithoutExtension(srtPath);
            string langCode = GetTargetLangCode(targetLang);
            string outputPath = GetOutputSubtitlePath(srtPath, fName, langCode);

            if (File.Exists(outputPath))
                File.Delete(outputPath);

            LoggerService.Log($"Translating subtitle file: {Path.GetFileName(srtPath)} ...");

            int totalBatches = (int)Math.Ceiling(blocks.Count / (double)batchSize);
            for (int b = 0; b < totalBatches; b++)
            {
                if (token.IsCancellationRequested)
                    return false;

                while (isPaused())
                {
                    await Task.Delay(1000, token);
                    if (token.IsCancellationRequested)
                        return false;
                }

                reportProgress?.Invoke((int)((b + 1) * 100.0 / totalBatches));
                int start = b * batchSize;
                var batch = blocks.Skip(start).Take(batchSize).ToList();
                string batchSrt = SubtitleUtils.FormatBlocksToSrtText(batch);

                LoggerService.Log($"Translating {b + 1} of {totalBatches} ...");

                var translated = await SubtitleTranslator.TranslateBatchSrt(
                    batchSrt,
                    sourceLang,
                    targetLang
                );

                File.AppendAllText(outputPath,
                    !string.IsNullOrEmpty(translated) ? translated + Environment.NewLine : batchSrt + Environment.NewLine,
                    Encoding.UTF8);

                LoggerService.Log(!string.IsNullOrEmpty(translated)
                    ? $"✅ Batch {b + 1} translated and saved into file."
                    : $"⚠️ Batch {b + 1} was not translated, original text applied.");
            }

            row.Cells["Status"].Value = FileStatus.Done;
            LoggerService.Log($"🎉 Translation completed successfully for {Path.GetFileName(srtPath)}.");
            return true;
        }

        /// <summary>
        ///     Gets the language code from a target language string formatted as "Language Name (code)".
        /// </summary>
        /// <param name="targetLang">
        ///     A string representing the target language in the format "Language Name (code)".
        /// </param>
        /// <returns>
        ///     Returns the extracted language code, or "fa" if the format is incorrect.
        /// </returns>
        public static string GetTargetLangCode(string targetLang)
        {
            int startIdx = targetLang.LastIndexOf('(');
            int endIdx = targetLang.LastIndexOf(')');
            if (startIdx != -1 && endIdx != -1 && endIdx > startIdx)
                return targetLang.Substring(startIdx + 1, endIdx - startIdx - 1);
            return "fa";
        }

        /// <summary>
        ///     Generates the output subtitle file path based on the original subtitle path, file name, and language code.
        /// </summary>
        /// <param name="srtPath">
        ///     A string representing the original subtitle file path.
        /// </param>
        /// <param name="fName">
        ///     A string representing the file name without extension.
        /// </param>
        /// <param name="langCode">
        ///     A string representing the target language code.
        /// </param>
        /// <returns>
        ///     Returns the constructed output subtitle file path.
        /// </returns>
        private static string GetOutputSubtitlePath(string srtPath, string fName, string langCode)
        {
            return Path.Combine(
                Path.GetDirectoryName(srtPath)!,
                fName.EndsWith("_original")
                    ? string.Concat(fName.AsSpan(0, fName.Length - 9), ".srt")
                    : $"{fName}_{langCode}.srt"
            );
        }
    }
}
