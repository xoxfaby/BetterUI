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