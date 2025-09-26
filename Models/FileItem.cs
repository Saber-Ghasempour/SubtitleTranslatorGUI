using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTranslatorGUI.Models
{
    internal class FileItem
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public FileType Type { get; set; }
        public List<string> SubtitleTracks { get; set; } // For video files
        public string SelectedTrack { get; set; } // For video files
        public FileStatus Status { get; set; }
    }
}
