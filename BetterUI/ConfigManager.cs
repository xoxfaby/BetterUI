using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using UnityEngine;
using BepInEx;

namespace BetterUI
{
    class ConfigManager
    {
        private static BepInEx.Configuration.ConfigEntry<bool> configAdvancedDescriptions;
        public bool AdvancedDescriptions { get => configAdvancedDescriptions.Value; }

        private static BepInEx.Configuration.ConfigEntry<bool> configShowHidden;
        public bool showHidden { get => configShowHidden.Value; }

        private static BepInEx.Configuration.ConfigEntry<bool> configSortItems;
        public bool sortItems { get => configSortItems.Value; }

        private static BepInEx.Configuration.ConfigEntry<bool> configShowStatsDisplay;
        public bool showStatsDisplay { get => configShowStatsDisplay.Value; }


        private static BepInEx.Configuration.ConfigEntry<bool> configScoreboardOnly;
        public bool scoreboardOnly { get => configScoreboardOnly.Value; }

        private static BepInEx.Configuration.ConfigEntry<Vector2> configWindowAnchorMin;
        public Vector2 windowAnchorMin { get => configWindowAnchorMin.Value; }

        private static BepInEx.Configuration.ConfigEntry<Vector2> configWindowAnchorMax;
        public Vector2 windowAnchorMax { get => configWindowAnchorMax.Value; }

        private static BepInEx.Configuration.ConfigEntry<Vector2> configWindowPosition;
        public Vector2 windowPosition { get => configWindowPosition.Value; }

        private static BepInEx.Configuration.ConfigEntry<Vector2> configWindowSize;
        public Vector2 windowSize { get => configWindowSize.Value; }

        private static BepInEx.Configuration.ConfigEntry<float> configStatsFontSize;
        public float statsFontSize { get => configStatsFontSize.Value; }

        private static BepInEx.Configuration.ConfigEntry<Color> configStatsFontColor;
        public Color statsFontColor { get => configStatsFontColor.Value; }

        private static BepInEx.Configuration.ConfigEntry<Color> configStatsFontOutlineColor;
        public Color statsFontOutlineColor { get => configStatsFontOutlineColor.Value; }

        private static BepInEx.Configuration.ConfigEntry<String> configStatString;
        public String statString { get => configStatString.Value; }


        private static BepInEx.Configuration.ConfigEntry<String> configTierOrder;
        public int[] tierOrder { get => new int[]
            {
            configTierOrder.Value.IndexOf('0'),
            configTierOrder.Value.IndexOf('1'),
            configTierOrder.Value.IndexOf('2'),
            configTierOrder.Value.IndexOf('3'),
            configTierOrder.Value.IndexOf('4'),
            configTierOrder.Value.IndexOf('5'),
            };
        }


        private static BepInEx.Configuration.ConfigEntry<String> configSortOrder;
        public string sortOrder { get => configSortOrder.Value; }




        public ConfigManager(BetterUI mod)
        {
            configAdvancedDescriptions = mod.Config.Bind<bool>("BetterUI", "AdvancedDescriptions", true, "Show advanced descriptions when hovering over an item or picking it up.");

            configShowHidden = mod.Config.Bind<bool>("BetterUI", "ShowHidden", false, "Show hidden items in the item inventory");

            configSortItems = mod.Config.Bind<bool>("BetterUI", "SortItems", true, "Sort items");

            configShowStatsDisplay = mod.Config.Bind<bool>("BetterUI", "ShowStatsDisplay", true, "Show Stats Display");


            configScoreboardOnly = mod.Config.Bind<bool>("StatsDisplay", "ShowOnlyScoreboard", false, "Only show the stats display when the scoreboard is open");

            configWindowAnchorMin = mod.Config.Bind<Vector2>("StatsDisplay", "WindowAnchorMin", new Vector2(1, 0.5f), 
                "Minimum position to anchor from. x & y\n" +
                "X: 0 is left of screen, 1 is right of screen\n" +
                "Y: 0 is top of scree, 1 is bottom of screen\n" +
                "default of (1, 0.5) puts the anchor in the middle of the right side of the screen.\n");

            configWindowAnchorMax = mod.Config.Bind<Vector2>("StatsDisplay", "WindowAnchorMax", new Vector2(1, 0.5f), "Maximum position to anchor from, see above.");

            configWindowPosition = mod.Config.Bind<Vector2>("StatsDisplay", "WindowPosition", new Vector2(-260, 200), "Position of the StatsDisplay window relative to anchor");

            configWindowSize = mod.Config.Bind<Vector2>("StatsDisplay", "WindowSize", new Vector2(250,600), "Size of the StatsDisplay window");

            configStatsFontSize = mod.Config.Bind<float>("StatsDisplay", "StatsDisplayFontSize", 22f, "Size of the StatsDisplay text");

            configStatsFontColor = mod.Config.Bind<Color>("StatsDisplay", "StatsFontColor", Color.white, "Color of the StatsDisplay text");

            configStatsFontOutlineColor = mod.Config.Bind<Color>("StatsDisplay", "StatsFontOutlineColor", new Color(0,0,0,1), "Color of the outline for the stats display");


            configStatString = mod.Config.Bind<String>("StatsDisplay", "StatString", 
                "Stats\n"+
                "Level: $level\n" + 
                "Experience: $exp\n" +
                "Luck: $luck\n" +
                "Base Damage $dmg\n" + 
                "Crit Chance $crit%\n" +
                "Attack Speed: $atkspd\n" +
                "Health: $hp/$maxhp\n" +
                "Shield: $shield/$maxshield\n" +
                "Barrier: $barrier/$maxbarrier\n" + 
                "Armor: $armor\n" + 
                "Regen: $regen\n" + 
                "MoveSpeed: $movespeed\n" + 
                "Jumps: $jumps/$maxjumps\n" +
                "Kills: $multikill | $killcount",
                "Valid Parameters:\n" +
                "$exp $level $luck\n" +
                "$dmg $crit $atkspd\n" +
                "$hp $maxhp $shield $maxshield $barrier $maxbarrier\n" +
                "$armor $regen\n" +
                "$movespeed $jumps $maxjumps\n" +
                "$killcount $multikill\n");


        configTierOrder = mod.Config.Bind<String>("Sorting", "TierOrder", "012345", "Tiers in ascending order, left to right \n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");

            configSortOrder = mod.Config.Bind<String>("Sorting", "SortOrder", "S134",
                "What to sort the items by, most important to least important.\n" +
                "Find the full details and an example in the Readme on Thunderstore/Github\n" +
                "0 = Tier Ascending\n" +
                "1 = Tier Descending\n" +
                "2 = Stack Size Ascending\n" +
                "3 = Stack Size Descending\n" +
                "4 = Pickup Order\n" +
                "5 = Pickup Order Reversed\n" +
                "6 = Alphabetical\n" +
                "7 = Alphabetical Reversed\n" +
                "8 = Random\n\n" +
                "Tag Based:\n" +
                "s = Scrap First\n" +
                "S = Scrap Last\n" +
                "d = Damage First\n" +
                "D = Damage Last\n" +
                "h = Healing First\n" +
                "H = Healing Last\n" +
                "u = Utility First\n" +
                "U = Utility Last\n" +
                "o = On Kill Effect First\n" +
                "O = On Kill Effect Last\n" +
                "e = Equipment Related First\n" +
                "E = Equipment Related Last\n" +
                "p = Sprint Related First\n" +
                "P = Sprint Related Last");
        }
    }
}
