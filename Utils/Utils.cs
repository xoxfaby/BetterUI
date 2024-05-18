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


        [Obsolete("This feature of BetterUI has been removed.")]
        public static float LuckCalc(float chance, float luck)
        {
            return 0;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static int TheREALFindSkillIndexByName(String skillDefName)
        {
            return 0;
        }
    }
}

namespace R2API.Utils
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ManualNetworkRegistrationAttribute : Attribute { }
}