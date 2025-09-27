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

        private static readonly SemaphoreSlim SwitchLock = new(1, 1);
        private static volatile bool IsSwitching = false;

        private static readonly string ApiKeysFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SubtitleTranslator", "api_keys.json");

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
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

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
        ///     Tries to switch to the next API key, ensuring only one switch happens at a time.
        /// </summary>
        /// <returns>
        ///     Returns true if switched to the next key, false if all keys are exhausted.
        /// </returns>
        public static async Task<bool> SwitchToNextApiKeyAsync()
        {
            if (IsSwitching)
                return false; // Someone else is already switching, just wait for them.

            await SwitchLock.WaitAsync();
            try
            {
                IsSwitching = true;
                if (CurrentKeyIndex < ApiKeys.Count - 1)
                {
                    CurrentKeyIndex++;
                    LoggerService.Log($"🔄 Switching to API key #{CurrentKeyIndex + 1}");
                    await Task.Delay(3000); // Hold lock for a few seconds
                    IsSwitching = false;
                    return true;
                }
                else
                {
                    LoggerService.Log("❌ All API keys exhausted. Stopping...");
                    await Task.Delay(3000);
                    IsSwitching = false;
                    return false;
                }
            }
            finally
            {
                SwitchLock.Release();
            }
        }

        /// <summary>
        ///     Waits for any ongoing switch to complete.
        /// </summary>
        public static async Task WaitForSwitchAsync()
        {
            while (IsSwitching)
                await Task.Delay(100);
        }

        /// <summary>
        ///     Resets the API key index to zero.
        /// </summary>
        public static void ResetKeyIndex() => CurrentKeyIndex = 0;
    }
}
