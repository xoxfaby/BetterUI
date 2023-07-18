using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoR2;

namespace BetterUI
{
    public static class Language
    {
        internal class UpdateChecker : UnityEngine.MonoBehaviour
        {
            const string apiUrl = "https://weblate.faby.dev/api/components/ror2/betterui/translations/";
            void Start()
            {
                StartCoroutine(CheckForUpdate());
            }

            System.Collections.IEnumerator CheckForUpdate()
            {
                UnityEngine.Networking.UnityWebRequest apiRequest = UnityEngine.Networking.UnityWebRequest.Get(apiUrl);
                yield return apiRequest.SendWebRequest();

                if (apiRequest.isNetworkError)
                {
                    UnityEngine.Debug.LogError(apiRequest.error);
                }
                else
                {
                    JObject result = JObject.Parse(apiRequest.downloadHandler.text);
                    List<(string, string, string)> r = new(result.SelectToken("$.results").Select(
                        s =>(
                        (string) s.SelectToken("$.filename"),
                        (string) s.SelectToken("$.revision"),
                        (string) s.SelectToken("$.file_url")
                        )
                        )
                        );
                    foreach(var s in r)
                    {
                        UnityEngine.Debug.LogError(s.Item1);
                        UnityEngine.Debug.LogError(s.Item2);
                        UnityEngine.Debug.LogError(s.Item3);

                        bool downloadFile = false;
                        string languageName = System.IO.Path.GetFileNameWithoutExtension(s.Item1);
                        string lPath = System.IO.Path.Combine(languagesPath, System.IO.Path.GetFileName(s.Item1));
                        string latestHash = s.Item2.Split(',')[0];
                        try
                        {
                            if(latestHash != CalculateBlobHash(lPath)) downloadFile = true;
                        }
                        catch(FileNotFoundException e)
                        {
                            downloadFile = true;
                        }
                        if (downloadFile)
                        {
                            UnityEngine.Networking.UnityWebRequest languageFileRequest = UnityEngine.Networking.UnityWebRequest.Get(s.Item3);
                            yield return languageFileRequest.SendWebRequest();
                            if (languageFileRequest.isNetworkError)
                            {
                                UnityEngine.Debug.LogError(apiRequest.error);
                            }
                            else
                            {
                                LoadLanguage(languageName, languageFileRequest.downloadHandler.text);
                                File.WriteAllText(lPath, languageFileRequest.downloadHandler.text);
                            }
                        }
                        else
                        {
                            UnityEngine.Debug.LogError("Not downloading " + languageName + " because we have it and it looks right");
                        }
                    }
                }
            }
        }
        static Dictionary<string, List<KeyValuePair<string, string>>> LanguageStrings = new Dictionary<string, List<KeyValuePair<string, string>>>(StringComparer.OrdinalIgnoreCase);
        static string dllPath;
        static string languagesPath;
        static List<string> languageFilePaths;

        static Language()
        {
            RoR2.Language.onCurrentLanguageChanged += Language_onCurrentLanguageChanged;

            // Get the directory of the .dll file
            dllPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Combine the directory of the .dll file with the name of the /Languages/ directory to get the full path
            languagesPath = System.IO.Path.Combine(dllPath, "Languages");

            // If the /Languages/ directory does not exist, use the directory of the .dll file instead
            if (!Directory.Exists(languagesPath))
            {
                languagesPath = dllPath;
            }

            // Get a list of all .json files in the /Languages/ directory or the directory of the .dll file
            languageFilePaths = new List<string>(Directory.GetFiles(languagesPath, "*.lang"));
        }


        static public string GetPluralToken(string token, float seconds)
        {
            switch (RoR2.Language.currentLanguageName)
            {
                case "UA":
                case "RU":
                    if (seconds % 1 != 0)
                    {
                        return token + "_SPECIAL";
                    }
                    int mod10 = (int)seconds % 10;
                    switch (mod10)
                    {
                        case 1:
                            if (seconds != 11)
                            {
                                return token;
                            }
                            break;
                        case 2:
                        case 3:
                        case 4:
                            if (seconds < 12 || seconds > 14)
                            {
                                return token + "_SPECIAL";
                            }
                            break;
                    }
                    return token + "_PLURAL";
                case "zh-CN":
                    return token;
                default:
                    return seconds == 1 ? token : token + "_PLURAL";
            }
        }

        static private void Language_onCurrentLanguageChanged()
        {
            if (LanguageStrings.TryGetValue(RoR2.Language.currentLanguageName, out var strings))
            {
                RoR2.Language.currentLanguage.SetStringsByTokens(strings);
            }
            if (!string.Equals(RoR2.Language.currentLanguageName, "en", StringComparison.OrdinalIgnoreCase)
               && LanguageStrings.TryGetValue("en", out var enStrings))
            {
                RoR2.Language.FindLanguageByName("en")?.SetStringsByTokens(enStrings);
            }
        }

        public static void RegisterLanguageToken(string token, string text, string language = "en")
        {
            if (!LanguageStrings.ContainsKey(language)) LanguageStrings[language] = new List<KeyValuePair<string, string>>();
            LanguageStrings[language].Add(new KeyValuePair<string, string>(token, text));

            if (string.Equals(RoR2.Language.currentLanguageName, language, StringComparison.OrdinalIgnoreCase)
                || string.Equals(language, "en", StringComparison.OrdinalIgnoreCase))
            {
                RoR2.Language.FindLanguageByName(language)?.SetStringByToken(token, text);
            }
        }

        public static void LoadLanguages()
        {
            // Loop through each .json file
            foreach (string jsonFile in languageFilePaths)
            {
                // Read the contents of the .json file
                string json = File.ReadAllText(jsonFile);

                string language = System.IO.Path.GetFileNameWithoutExtension(jsonFile);
                LoadLanguage(language, json);
            }
        }

        public static void LoadLanguage(string languageName ,string jsonText)
        {
            try
            {
                Dictionary<string, string> languageData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);
                foreach (KeyValuePair<string, string> item in languageData)
                {
                    RegisterLanguageToken(item.Key, item.Value, languageName);
                }
            }
            catch (JsonException ex)
            {
                UnityEngine.Debug.LogError("Error deserializing JSON for language: " + languageName + " - " + ex.Message);
            }
        }

        public static string CalculateBlobHash(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                string header = $"blob {fileStream.Length}\0";
                byte[] headerBytes = System.Text.Encoding.ASCII.GetBytes(header);

                using (var sha1 = SHA1.Create())
                {
                    sha1.TransformBlock(headerBytes, 0, headerBytes.Length, headerBytes, 0);

                    byte[] buffer = new byte[8192];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        sha1.TransformBlock(buffer, 0, bytesRead, buffer, 0);
                    }
                    sha1.TransformFinalBlock(buffer, 0, 0);

                    byte[] hashBytes = sha1.Hash;
                    return ByteArrayToHexString(hashBytes);
                }
            }
        }

        private static string ByteArrayToHexString(byte[] bytes)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}