using System;
using System.Collections.Generic;
using System.Text;

using RoR2.Skills;
using R2API.Utils;

[assembly: ManualNetworkRegistration]
namespace BetterUI
{
    static class Utils
    {
        static Dictionary<string, List<KeyValuePair<string, string>>> LanguageStrings = new Dictionary<string, List<KeyValuePair<string, string>>>();
        static Utils()
        {
            RoR2.Language.onCurrentLanguageChanged += Language_onCurrentLanguageChanged;
        }

        static public string SecondsToString(float seconds)
        {
            switch (RoR2.Language.currentLanguageName)
            {
                case "UA":
                case "RU":
                    if (seconds % 1 != 0)
                    {
                        return "BETTERUI_SECONDS_SPECIAL";
                    }
                    int mod10 = (int)seconds % 10;
                    switch (mod10)
                    {
                        case 1:
                            if (seconds != 11)
                            {
                                return "BETTERUI_SECOND";
                            }
                            break;
                        case 2:
                        case 3:
                        case 4:
                            if (seconds < 12 || seconds > 14)
                            {
                                return "BETTERUI_SECONDS_SPECIAL";
                            }
                            break;
                    }
                    return "BETTERUI_SECONDS";
                case "zh-CN":
                    return "BETTERUI_SECOND";
                default:
                    return seconds == 1 ? "BETTERUI_SECOND" : "BETTERUI_SECONDS";
            }
        }

        static private Boolean LanguagesLoaded = false;

        static private void Language_onCurrentLanguageChanged()
        {
            LanguagesLoaded = true;
            if (LanguageStrings.TryGetValue(RoR2.Language.currentLanguageName, out var strings)){
                RoR2.Language.currentLanguage.SetStringsByTokens(strings);
            }
            if(RoR2.Language.currentLanguageName != "en" 
               && LanguageStrings.TryGetValue("en", out var enStrings))
            {
                RoR2.Language.GetOrCreateLanguage("en").SetStringsByTokens(enStrings);
            }
        }

        public static void RegisterLanguageToken(string token, string text, string language = "en")
        {
            if(!LanguageStrings.ContainsKey(language)) LanguageStrings[language] = new List<KeyValuePair<string, string>>();
            LanguageStrings[language].Add(new KeyValuePair<string, string>(token, text));

            if (!LanguagesLoaded) return;

            if(RoR2.Language.currentLanguageName == language || language == "en")
            {
                RoR2.Language.GetOrCreateLanguage(language).SetStringByToken(token, text);
            }
        }

        public static float LuckCalc(float chance, float luck)
        {
            if (luck == 0)
            {
                return chance;
            }
            else if (luck < 0)
            {
                return (float) ((int) chance + Math.Pow(chance % 1, Math.Abs(luck) + 1));
            }
            else
            {
                return (float) ((int)chance + (1 - Math.Pow(1 - (chance % 1), Math.Abs(luck) + 1)));
            }
        }

        public static int TheREALFindSkillIndexByName(String skillDefName)
        {
            for (int i = SkillCatalog._allSkillDefs.Length - 1; i >= 0; i--)
            {
                if (SkillCatalog._allSkillNames[i] == skillDefName)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

namespace R2API.Utils
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ManualNetworkRegistrationAttribute : Attribute { }
}