using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace SubtitleTranslatorGUI.Services
{
    internal static class FFmpegAudioService
    {
        public static bool ExtractAudio(string videoPath, string outputAudioPath)
        {
            var args = $"-i \"{videoPath}\" -vn -acodec pcm_s16le -ar 16000 -ac 1 \"{outputAudioPath}\"";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();
            return true;
        }

        public static List<string> SplitAudio(string inputAudioPath, int chunkSeconds = 300)
        {
            var outputFiles = new List<string>();
            var ffmpegPath = "ffmpeg";
            var duration = GetAudioDuration(inputAudioPath);
            int totalChunks = (int)Math.Ceiling(duration / (double)chunkSeconds);

            for (int i = 0; i < totalChunks; i++)
            {
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputAudioPath)!,
                    $"{Path.GetFileNameWithoutExtension(inputAudioPath)}_chunk{i + 1}.wav"
                );
                var args = $"-y -i \"{inputAudioPath}\" -ss {i * chunkSeconds} -t {chunkSeconds} \"{outputPath}\"";
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ffmpegPath,
                        Arguments = args,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                process.WaitForExit();
                outputFiles.Add(outputPath);
            }
            return outputFiles;
        }

        private static double GetAudioDuration(string audioPath)
        {
            var ffprobePath = "ffprobe";
            var args = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{audioPath}\"";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffprobePath,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (double.TryParse(output.Trim(), out double duration))
                return duration;

            throw new InvalidOperationException("Could not determine audio duration.");
        }

        public static string OffsetSrtTimestamps(string srtContent, TimeSpan offset)
        {
            var lines = srtContent.Split(["\r\n", "\n"], StringSplitOptions.None);
            var sb = new StringBuilder();

            var timestampRegexFull = new Regex(@"(\d{2}:\d{2}:\d{2},\d{3}) --> (\d{2}:\d{2}:\d{2},\d{3})");
            var timestampRegexShort = new Regex(@"(\d{2}:\d{2},\d{3}) --> (\d{2}:\d{2},\d{3})");

            foreach (var line in lines)
            {
                var matchFull = timestampRegexFull.Match(line);
                var matchShort = timestampRegexShort.Match(line);

                if (matchFull.Success)
                {
                    var start = TimeSpan.ParseExact(matchFull.Groups[1].Value, @"hh\:mm\:ss\,fff", null);
                    var end = TimeSpan.ParseExact(matchFull.Groups[2].Value, @"hh\:mm\:ss\,fff", null);
                    var newStart = start + offset;
                    var newEnd = end + offset;
                    sb.AppendLine($"{newStart:hh\\:mm\\:ss\\,fff} --> {newEnd:hh\\:mm\\:ss\\,fff}");
                }
                else if (matchShort.Success)
                {
                    var startStr = "00:" + matchShort.Groups[1].Value;
                    var endStr = "00:" + matchShort.Groups[2].Value;
                    var start = TimeSpan.ParseExact(startStr, @"hh\:mm\:ss\,fff", null);
                    var end = TimeSpan.ParseExact(endStr, @"hh\:mm\:ss\,fff", null);
                    var newStart = start + offset;
                    var newEnd = end + offset;
                    sb.AppendLine($"{newStart:hh\\:mm\\:ss\\,fff} --> {newEnd:hh\\:mm\\:ss\\,fff}");
                }
                else
                {
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        public static string RenumberSrtBlocks(string srtContent, int startNumber)
        {
            var lines = srtContent.Split(["\r\n", "\n"], StringSplitOptions.None);
            var sb = new StringBuilder();
            int currentNumber = startNumber;
            int lineIndex = 0;

            while (lineIndex < lines.Length)
            {
                if (int.TryParse(lines[lineIndex], out _))
                {
                    sb.AppendLine(currentNumber.ToString());
                    currentNumber++;
                    lineIndex++;
                    while (lineIndex < lines.Length && !string.IsNullOrWhiteSpace(lines[lineIndex]))
                    {
                        sb.AppendLine(lines[lineIndex]);
                        lineIndex++;
                    }
                }
                else
                {
                    sb.AppendLine(lines[lineIndex]);
                    lineIndex++;
                }
            }
            return sb.ToString();
        }

        public static int CountSrtBlocks(string srtContent)
        {
            var lines = srtContent.Split(["\r\n", "\n"], StringSplitOptions.None);
            int count = 0;
            foreach (var line in lines)
            {
                if (int.TryParse(line, out _))
                    count++;
            }
            return count;
        }
    }
}
