using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using UnityEngine;
using RoR2;
using BetterUI.ModCompatibility;

namespace BetterUI
{
    [Obsolete("This feature of BetterUI has been removed.")]
    public static class Buffs
    {

        [Obsolete("This feature of BetterUI has been removed.")]
        public static void RegisterName(BuffDef buffDef, string nameToken)
        {
            
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static void RegisterDescription(BuffDef buffDef, string descriptionToken)
        {
            
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static void RegisterBuffInfo(BuffDef buffDef, string nameToken = null, string descriptionToken = null)
        {

        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static void RegisterBuffInfo(BuffDef buffDef, BuffInfo buffInfo)
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

        [Obsolete("This feature of BetterUI has been removed.")]
        public struct BuffInfo
        {
            public string nameToken;
            public string descriptionToken;
        }
    }
}
