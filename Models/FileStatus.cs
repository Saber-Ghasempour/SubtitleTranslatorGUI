using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTranslatorGUI.Models
{
    internal enum FileStatus
    {
        Queued,
        Processing,
        Done,
        SubtitleExtractionFailed
    }
}
