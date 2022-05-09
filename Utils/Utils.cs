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

        static private void Language_onCurrentLanguageChanged()
        {
            if(LanguageStrings.TryGetValue(RoR2.Language.currentLanguageName, out var strings)){
                RoR2.Language.currentLanguage.SetStringsByTokens(strings);
            }
        }

        public static void RegisterLanguageToken(string token, string text, string language = "en")
        {
            if(!LanguageStrings.ContainsKey(language)) LanguageStrings[language] = new List<KeyValuePair<string, string>>();
            LanguageStrings[language].Add(new KeyValuePair<string, string>(token, text));

            if(RoR2.Language.currentLanguageName == language)
            {
                RoR2.Language.currentLanguage.SetStringByToken(token, text);
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