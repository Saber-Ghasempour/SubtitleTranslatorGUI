namespace SubtitleTranslatorGUI.Models
{
    /// <summary>
    ///     Model representing a single subtitle block in an SRT file.
    /// </summary>
    internal class SrtBlock
    {
        public string Number = "";
        public string TimeCode = "";
        public List<string> TextLines = [];
    }
}
