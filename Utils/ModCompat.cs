using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    namespace ModCompatibility
    {
        internal static class BetterAPICompatibility
        {
            internal static class Buffs
            {
                [Obsolete("This feature of BetterUI has been removed.")]
                public static void AddName(BuffDef buffDef, string nameToken)
                {
                }
                [Obsolete("This feature of BetterUI has been removed.")]
                public static void AddDescription(BuffDef buffDef, string descriptionToken)
                {
                }

                [Obsolete("This feature of BetterUI has been removed.")]
                public static void AddInfo(BuffDef buffDef, string nameToken = null, string descriptionToken = null)
                {
                }
                [Obsolete("This feature of BetterUI has been removed.")]
                public static void AddInfo(BuffDef buffDef, BetterUI.Buffs.BuffInfo buffInfo)
                {
                    
                }

                [Obsolete("This feature of BetterUI has been removed.")]
                public static string GetName(BuffDef buffDef)
                {
                    return "";
                }
                [Obsolete("This feature of BetterUI has been removed.")]
                public static string GetDescription(BuffDef buffDef)
                {
                    return "";
                }
            }
        }
    }
}