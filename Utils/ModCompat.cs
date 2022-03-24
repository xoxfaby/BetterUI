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
                public static void AddName(BuffDef buffDef, string nameToken)
                {
                    BetterAPI.Buffs.AddName(buffDef, nameToken);
                }
                public static void AddDescription(BuffDef buffDef, string descriptionToken)
                {
                    BetterAPI.Buffs.AddDescription(buffDef, descriptionToken);
                }

                public static void AddInfo(BuffDef buffDef, string nameToken = null, string descriptionToken = null)
                {
                    BetterAPI.Buffs.AddInfo(buffDef, nameToken, descriptionToken);
                }
                public static void AddInfo(BuffDef buffDef, BetterUI.Buffs.BuffInfo buffInfo)
                {
                    BetterAPI.Buffs.BuffInfo BetterAPIBuffInfo = new BetterAPI.Buffs.BuffInfo
                    {
                        nameToken = buffInfo.nameToken,
                        descriptionToken = buffInfo.descriptionToken,
                    };
                    BetterAPI.Buffs.AddInfo(buffDef, BetterAPIBuffInfo);
                }

                public static string GetName(BuffDef buffDef)
                {
                    return BetterAPI.Buffs.GetName(buffDef);
                }
                public static string GetDescription(BuffDef buffDef)
                {
                    return BetterAPI.Buffs.GetDescription(buffDef);
                }
            }
        }
    }
}