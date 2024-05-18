using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

using RoR2;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace BetterUI
{
    public static class StatsDisplay
    {

        public delegate string DisplayCallback(CharacterBody characterBody);

        internal static void Hook() { }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static void AddStatsDisplay(string token, Func<CharacterBody, string> displayCallback)
        {

        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static void AddStatsDisplay(string token, DisplayCallback displayCallback)
        {
            
        }



        
    }
}
