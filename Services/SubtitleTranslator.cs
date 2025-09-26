using System.Text;
using System.Text.Json;

namespace SubtitleTranslatorGUI.Services
{
    /// <summary>
    ///     A service class for translating subtitle files using Google Gemini API.
    /// </summary>
    internal static class SubtitleTranslator
    {
        /// <summary>
        ///     Translates the given SRT content from source language to target language using Google Gemini API.
        /// </summary>
        /// <param name="srtContent">
        ///     A string containing the entire content of an SRT subtitle file.
        /// </param>
        /// <param name="sourceLang">
        ///     Source language code (e.g., "en" for English).
        /// </param>
        /// <param name="targetLang">
        ///     Target language code (e.g., "es" for Spanish).
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation. The task result contains the translated SRT content as a string.
        /// </returns>
        public static async Task<string> TranslateBatchSrt(string srtContent, string sourceLang, string targetLang)
        {
            int retryCount = 0;

            while (retryCount < ApiKeyManager.ApiKeys.Count)
            {
                string apiKey = ApiKeyManager.GetCurrentApiKey();

                try
                {
                    using var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("X-goog-api-key", apiKey);

                    string prompt =
                        $@"You are given a subtitle file in SRT format (multiple blocks).
                        Translate ONLY the subtitle texts from {sourceLang} to {targetLang}.
                        DO NOT change or remove the subtitle numbers.
                        DO NOT change or remove the timestamps.
                        Keep the exact SRT structure (number → timestamp → text).
                        Return ONLY the translated subtitle file in valid SRT format (no commentary).

                        Here is the SRT content:
                        {srtContent}
                    ";

                    var requestBody = new
                    {
                        contents = new[]
                        {
                            new {
                                parts = new[] {
                                    new { text = prompt }
                                }
                            }
                        }
                    };

                    var json = JsonSerializer.Serialize(requestBody);
                    var response = await httpClient.PostAsync(
                        "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent",
                        new StringContent(json, Encoding.UTF8, "application/json")
                    );

                    var responseText = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests) // 429
                    {
                        LoggerService.Log($"⚠️ API key #{retryCount + 1} hit rate limit (429).");
                        retryCount++;
                        if (!ApiKeyManager.SwitchToNextApiKey())
                            return null;
                        continue;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        LoggerService.Log($"❌ API error ({(int)response.StatusCode}): {response.ReasonPhrase}. Waiting 30 seconds before retry...");
                        await Task.Delay(TimeSpan.FromSeconds(30));
                        continue;
                    }

                    using var doc = JsonDocument.Parse(responseText);
                    var generated = doc.RootElement
                                       .GetProperty("candidates")[0]
                                       .GetProperty("content")
                                       .GetProperty("parts")[0]
                                       .GetProperty("text")
                                       .GetString();

                    if (!string.IsNullOrEmpty(generated))
                    {
                        generated = generated.Trim();
                        if (generated.StartsWith("```srt"))
                            generated = generated.Substring(6).TrimStart();
                        if (generated.StartsWith("```"))
                            generated = generated.Substring(3).TrimStart();
                        if (generated.EndsWith("```"))
                            generated = generated.Substring(0, generated.Length - 3).TrimEnd();
                        generated += Environment.NewLine;
                        return generated;
                    }

                    return generated;
                }
                catch (Exception ex)
                {
                    LoggerService.LogError(new Exception($"❌ Error during TranslateBatchSrt: {ex.Message}. Waiting 30 seconds before retry..."), "SubtitleTranslator");
                    await Task.Delay(TimeSpan.FromSeconds(30));
                }
            }

            return null;
        }
    }
}
