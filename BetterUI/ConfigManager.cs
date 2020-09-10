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
        //BetterUI
        public BepInEx.Configuration.ConfigEntry<bool> showStatsDisplay;
        public BepInEx.Configuration.ConfigEntry<bool> sortItemsInventory;
        public BepInEx.Configuration.ConfigEntry<bool> sortItemsScrapper;
        public BepInEx.Configuration.ConfigEntry<bool> sortItemsCommand;
        public BepInEx.Configuration.ConfigEntry<bool> advancedDescriptions;
        public BepInEx.Configuration.ConfigEntry<bool> showHidden;


        //Sorting
        public BepInEx.Configuration.ConfigEntry<String> tierOrderString;
        public BepInEx.Configuration.ConfigEntry<String> sortOrder;
        public BepInEx.Configuration.ConfigEntry<String> sortOrderScrapper;
        public BepInEx.Configuration.ConfigEntry<String> sortOrderCommand;
        public int[] tierOrder;

        //StatsDisplay
        public BepInEx.Configuration.ConfigEntry<bool> scoreboardOnly;
        public BepInEx.Configuration.ConfigEntry<Vector2> windowAnchorMin;
        public BepInEx.Configuration.ConfigEntry<Vector2> windowAnchorMax;
        public BepInEx.Configuration.ConfigEntry<Vector2> windowPosition;
        public BepInEx.Configuration.ConfigEntry<Vector2> windowSize;
        public BepInEx.Configuration.ConfigEntry<float> statsFontSize;
        public BepInEx.Configuration.ConfigEntry<Color> statsFontColor;
        public BepInEx.Configuration.ConfigEntry<Color> statsFontOutlineColor;
        public BepInEx.Configuration.ConfigEntry<String> statString;


        public ConfigManager(BetterUI mod)
        {
            //BetterUI
            showStatsDisplay = mod.Config.Bind<bool>("BetterUI", "ShowStatsDisplay", true, "Show Stats Display");
            sortItemsInventory = mod.Config.Bind<bool>("BetterUI", "SortItemsInventory", true, "Sort items in the inventory and scoreboard");
            sortItemsScrapper = mod.Config.Bind<bool>("BetterUI", "SortItemsScrapper", true, "Sort items in the scrapper window");
            sortItemsCommand = mod.Config.Bind<bool>("BetterUI", "SortItemsCommand", true, "Sort items in the command window");
            advancedDescriptions = mod.Config.Bind<bool>("BetterUI", "AdvancedDescriptions", true, "Show advanced descriptions when hovering over an item or picking it up.");
            showHidden = mod.Config.Bind<bool>("BetterUI", "ShowHidden", false, "Show hidden items in the item inventory");

            //Sorting
            tierOrderString = mod.Config.Bind<String>("Sorting", "TierOrder", "012345", "Tiers in ascending order, left to right \n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");


            tierOrder = new int[]
            {
                tierOrderString.Value.IndexOf('0'),
                tierOrderString.Value.IndexOf('1'),
                tierOrderString.Value.IndexOf('2'),
                tierOrderString.Value.IndexOf('3'),
                tierOrderString.Value.IndexOf('4'),
                tierOrderString.Value.IndexOf('5'),
            };

            sortOrder = mod.Config.Bind<String>("Sorting", "SortOrder", "S134",
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
                    "8 = Random\n" +
                    "i = ItemIndex\n" +
                    "I = ItemIndex Descending\n" +
                    "\n" +
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

            sortOrderScrapper = mod.Config.Bind<String>("Sorting", "SortOrderScrapper", "S134",
            "Sort order for the scrapper window");

            sortOrderCommand = mod.Config.Bind<String>("Sorting", "SortOrderCommand", "36C",
            "Sort order for the command window\n" +
            "The command window has a special sort option \"C\" which will place the last selected item in the middle.\n" +
            "Note: This option must be the last one in the SortOrderCommand option");



            //StatsDisplay

            scoreboardOnly = mod.Config.Bind<bool>("StatsDisplay", "ShowOnlyScoreboard", false, "Only show the stats display when the scoreboard is open");

            windowAnchorMin = mod.Config.Bind<Vector2>("StatsDisplay", "WindowAnchorMin", new Vector2(1, 0.5f), 
                "Minimum position to anchor from. x & y\n" +
                "X: 0 is left of screen, 1 is right of screen\n" +
                "Y: 0 is top of scree, 1 is bottom of screen\n" +
                "default of (1, 0.5) puts the anchor in the middle of the right side of the screen.\n");

            windowAnchorMax = mod.Config.Bind<Vector2>("StatsDisplay", "WindowAnchorMax", new Vector2(1, 0.5f), "Maximum position to anchor from, see above.");
            windowPosition = mod.Config.Bind<Vector2>("StatsDisplay", "WindowPosition", new Vector2(-210, 100), "Position of the StatsDisplay window relative to anchor");
            windowSize = mod.Config.Bind<Vector2>("StatsDisplay", "WindowSize", new Vector2(200,600), "Size of the StatsDisplay window");
            statsFontSize = mod.Config.Bind<float>("StatsDisplay", "StatsDisplayFontSize", 22f, "Size of the StatsDisplay text");
            statsFontColor = mod.Config.Bind<Color>("StatsDisplay", "StatsFontColor", Color.white, "Color of the StatsDisplay text");
            statsFontOutlineColor = mod.Config.Bind<Color>("StatsDisplay", "StatsFontOutlineColor", new Color(0,0,0,1), "Color of the outline for the stats display");

            statString = mod.Config.Bind<String>("StatsDisplay", "StatString", 
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

        }
    }
}
