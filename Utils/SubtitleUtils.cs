using SubtitleTranslatorGUI.Models;
using System.Text;

namespace SubtitleTranslatorGUI.Utils
{
    /// <summary>
    ///     A utility class for reading and formatting SRT subtitle files.
    /// </summary>
    internal static class SubtitleUtils
    {
        /// <summary>
        ///     Reads SRT blocks from a file.
        /// </summary>
        /// <param name="path">
        ///     Path to the SRT file.
        /// </param>
        /// <returns>
        ///     List of SrtBlock objects representing the subtitle blocks.
        /// </returns>
        public static List<SrtBlock> ReadSrtBlocks(string path)
        {
            var text = File.ReadAllText(path, Encoding.UTF8);
            var lines = text.Replace("\r\n", "\n").Split('\n').ToList();
            var blocks = new List<SrtBlock>();
            int i = 0;
            while (i < lines.Count)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) { i++; continue; }
                var block = new SrtBlock
                {
                    Number = lines[i++].Trim()
                };

                if (i >= lines.Count) break;
                block.TimeCode = lines[i++].Trim();
                var txtLines = new List<string>();
                while (i < lines.Count && !string.IsNullOrWhiteSpace(lines[i]))
                {
                    txtLines.Add(lines[i++]);
                }
                block.TextLines = txtLines;
                blocks.Add(block);
                while (i < lines.Count && string.IsNullOrWhiteSpace(lines[i])) i++;
            }
            return blocks;
        }

        /// <summary>
        ///     Formats a list of SrtBlock objects back into SRT text format.
        /// </summary>
        /// <param name="blocks">
        ///     List of SrtBlock objects to format.
        /// </param>
        /// <returns>
        ///     A string representing the formatted SRT text.
        /// </returns>
        public static string FormatBlocksToSrtText(List<SrtBlock> blocks)
        {
            var sb = new StringBuilder();
            foreach (var block in blocks)
            {
                sb.AppendLine(block.Number);
                sb.AppendLine(block.TimeCode);
                foreach (var l in block.TextLines) sb.AppendLine(l);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
