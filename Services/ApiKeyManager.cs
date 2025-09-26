using System.Text;
using System.Text.Json;

namespace SubtitleTranslatorGUI.Services
{
    /// <summary>
    ///     Manages API keys for the application, including loading and saving.
    /// </summary>
    internal static class ApiKeyManager
    {
        public static List<string> ApiKeys { get; private set; } = [];
        public static int CurrentKeyIndex { get; private set; } = 0;

        private static string ApiKeysFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SubtitleTranslator", "api_keys.json");

        /// <summary>
        ///     Loads API keys from a JSON file in the application data directory.
        /// </summary>
        public static void LoadApiKeys()
        {
            if (File.Exists(ApiKeysFile))
            {
                var json = File.ReadAllText(ApiKeysFile, Encoding.UTF8);
                ApiKeys = JsonSerializer.Deserialize<List<string>>(json) ?? [];
            }
            else
            {
                ApiKeys = [];
            }
            CurrentKeyIndex = 0;
        }

        /// <summary>
        ///     Saves the current list of API keys to a JSON file in the application data directory.
        /// </summary>
        public static void SaveApiKeys()
        {
            var json = JsonSerializer.Serialize(ApiKeys);
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appData, "SubtitleTranslator");
            string ApiKeysFile = Path.Combine(folder, "api_keys.json");
            File.WriteAllText(ApiKeysFile, json, Encoding.UTF8);
        }

        /// <summary>
        ///     Gets the current API key based on the CurrentKeyIndex.
        /// </summary>
        /// <returns>
        ///     API key as a string, or null if no keys are available.
        /// </returns>
        public static string GetCurrentApiKey()
        {
            if (ApiKeys.Count == 0) return null;
            return ApiKeys[CurrentKeyIndex];
        }

        /// <summary>
        ///     Switches to the next API key in the list.
        /// </summary>
        /// <returns>
        ///     Returns true if switched to the next key, false if all keys are exhausted.
        /// </returns>
        public static bool SwitchToNextApiKey()
        {
            if (CurrentKeyIndex < ApiKeys.Count - 1)
            {
                CurrentKeyIndex++;
                LoggerService.Log($"🔄 Switching to API key #{CurrentKeyIndex + 1}");
                return true;
            }
            else
            {
                LoggerService.Log("❌ All API keys exhausted. Stopping...");
                return false;
            }
        }

        /// <summary>
        ///     Resets the API key index to zero.
        /// </summary>
        public static void ResetKeyIndex() => CurrentKeyIndex = 0;
    }
}
