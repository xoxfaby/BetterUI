using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BetterUI
{
    public class LanguageLoader
    {
        public void LoadLanguages()
        {
            // Get the directory of the .dll file
            string dllPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Combine the directory of the .dll file with the name of the /Languages/ directory to get the full path
            string languagesPath = Path.Combine(dllPath, "Languages");

            // If the /Languages/ directory does not exist, use the directory of the .dll file instead
            if (!Directory.Exists(languagesPath))
            {
                languagesPath = dllPath;
            }

            // Get a list of all .json files in the /Languages/ directory or the directory of the .dll file
            string[] jsonFiles = Directory.GetFiles(languagesPath, "*.json");

            // Loop through each .json file
            foreach (string jsonFile in jsonFiles)
            {
                // Read the contents of the .json file
                string json = File.ReadAllText(jsonFile);

                // Deserialize the JSON into a dictionary
                Dictionary<string, string> languageData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                // Get the name of the .json file without the .json extension
                string language = Path.GetFileNameWithoutExtension(jsonFile);

                // Loop through each item in the dictionary
                foreach (KeyValuePair<string, string> item in languageData)
                {
                    // Call BetterUI.Utils.RegisterLanguageToken with the appropriate values
                    BetterUI.Utils.RegisterLanguageToken(item.Key, item.Value, language);
                }
            }
        }
    }
}
