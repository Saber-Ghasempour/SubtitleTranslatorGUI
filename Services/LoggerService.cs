namespace SubtitleTranslatorGUI.Services
{
    /// <summary>
    /// A service to handle logging
    /// </summary>
    internal static class LoggerService
    {
        public static Action<string> LogAction { get; set; }

        /// <summary>
        /// Log a string into text view
        /// </summary>
        /// <param name="message">The string of log</param>
        public static void Log(string message)
        {
            LogAction?.Invoke(message);
        }

        /// <summary>
        /// Log an exception into text view
        /// </summary>
        /// <param name="ex">The exception object</param>
        /// <param name="context">Context name</param>
        public static void LogError(Exception ex, string context = "")
        {
            var msg = $"❌ Error{(string.IsNullOrWhiteSpace(context) ? "" : $" in {context}")}: {ex.Message}";
            LogAction?.Invoke(msg);
        }
    }
}
