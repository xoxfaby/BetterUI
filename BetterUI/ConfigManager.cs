using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            configAdvancedDescriptions = mod.Config.Bind<bool>("BetterUI", "AdvancedDescriptions", true,
                "Show advanced descriptions when hovering over an item or picking it up.");

            configShowHidden = mod.Config.Bind<bool>("BetterUI", "ShowHidden", false,
                "Show hidden items in the item inventory");

            configSortItems = mod.Config.Bind<bool>("BetterUI", "SortItems", true,
                "Sort items");

            configTierOrder = mod.Config.Bind<String>("Sorting", "TierOrder", "012345",
                "Tiers in ascending order, left to right \n0 = White, 1 = Green, 2 = Red, 3 = Lunar, 4 = Boss, 5 = NoTier");

            configSortOrder = mod.Config.Bind<String>("Sorting", "SortOrder", "S134",
                "What to sort the items by, most important to least important.\n" +
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
                "P = Sprint Related Last\n\n" +
                "Since RoR2Modman seems to have issues with the config file you can find the full list in the README on Thunderstore.");
        }
    }
}
