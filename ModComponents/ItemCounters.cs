using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

using RoR2;
using RoR2.UI;
using BepInEx.Configuration;

namespace BetterUI
{
    class ItemCounters : BetterUI.ModComponent
    {
        internal ItemCounters(BetterUI mod) : base(mod) { }

        public static readonly Dictionary<ItemIndex, int> ItemScores = new Dictionary<ItemIndex, int>();
        
        static ItemCounters()
        {
            //TODO: Add individual item scores.
        }
        
        string[] tierColorMap = new string[]
        {
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.Tier1Item),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.Tier2Item),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.Tier3Item),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.LunarItem),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.BossItem),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.Error),
        };

        internal override void Hook()
        {
            if (mod.config.ItemCountersShowItemCounters.Value)
            {
                On.RoR2.UI.ScoreboardStrip.SetMaster += mod.itemCounters.hook_ScoreboardStrip_SetMaster;
                On.RoR2.UI.ScoreboardStrip.Update += mod.itemCounters.hook_ScoreboardStrip_Update;
            }
        }

        internal override void Start()
        {
            bool first = true;
            foreach (var itemIndex in RoR2.ItemCatalog.allItems)
            {
                ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
                int itemValue;
                if (!ItemScores.TryGetValue(itemIndex, out itemValue))
                { 
                    itemValue = mod.config.ItemCountersTierScores[(int)itemDef.tier];
                }
                if (first)
                {
                    var itemScore = mod.config.ConfigFileItemCounters.Bind<int>("ItemScores", itemDef.nameToken, itemValue, $"Score of each item for the ItemScore.\n{Language.GetString(itemDef.nameToken)}");
                    mod.config.ItemScoreConfig.Add(itemScore);
                    first = false;
                }
                else
                {
                    var itemScore = mod.config.ConfigFileItemCounters.Bind<int>("ItemScores", itemDef.nameToken, itemValue, Language.GetString(itemDef.nameToken));
                    mod.config.ItemScoreConfig.Add(itemScore);
                }
            }

            mod.config.ItemCountersItemScores = mod.config.ItemScoreConfig.ToDictionary(e => e.Definition.Key, e => e.Value);

        }
        internal void hook_ScoreboardStrip_SetMaster(On.RoR2.UI.ScoreboardStrip.orig_SetMaster orig, ScoreboardStrip self, CharacterMaster master)
        {
            orig(self, master);

            self.moneyText.overflowMode = TMPro.TextOverflowModes.Overflow;
        }

        internal void hook_ScoreboardStrip_Update(On.RoR2.UI.ScoreboardStrip.orig_Update orig, ScoreboardStrip self)
        {
            orig(self);

            if (self.master)
            {
                string nameLabel = $"{Util.GetBestMasterName(self.master)}\n<#F8FC97>${self.master.money}</color>";
                string moneyLabel = "<#FFFFFF>";

                string itemSum = "";
                string itemScore = "";


                if (mod.config.ItemCountersShowItemSum.Value)
                {
                    itemSum = mod.config.ItemCountersItemSumTiers.Aggregate(0, (s, t) => s + self.master.inventory.GetTotalItemCountOfTier(t)).ToString();
                }
                if (mod.config.ItemCountersShowItemScore.Value)
                {
                    itemScore = self.master.inventory.itemAcquisitionOrder.Aggregate(0, (s, i) => s + mod.config.ItemCountersItemScores[ItemCatalog.GetItemDef(i).nameToken] * self.master.inventory.itemStacks[(int)i]).ToString();
                }
                moneyLabel += String.Join(" | ", new[] { itemScore, itemSum }.Where(s => !string.IsNullOrEmpty(s)));

                if (mod.config.ItemCountersShowItemsByTier.Value)
                {
                    moneyLabel += "\n" + string.Join(" ", mod.config.ItemCountersItemsByTierOrder.Select(tier => $"<#{tierColorMap[(int)tier]}>{self.master.inventory.GetTotalItemCountOfTier(tier)}</color>"));
                }

                moneyLabel += "</color>";

                self.nameLabel.text = nameLabel;
                self.moneyText.text = moneyLabel;
            }
        }


}
}
