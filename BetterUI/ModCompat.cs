using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    class ModCompat
    {
        public static string statsFromItemStats(ItemIndex itemIndex, int count, CharacterMaster master)
        {
            return ItemStats.ItemStatsMod.GetStatsForItem(itemIndex, count, new ItemStats.StatContext(master));
        }
    }
}
