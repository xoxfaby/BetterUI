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
                On.RoR2.UI.ScoreboardStrip.SetMaster += mod.itemCounters.ScoreboardStrip_SetMaster;
                On.RoR2.UI.ScoreboardStrip.Update += mod.itemCounters.ScoreboardStrip_Update;
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
        internal void ScoreboardStrip_SetMaster(On.RoR2.UI.ScoreboardStrip.orig_SetMaster orig, ScoreboardStrip self, CharacterMaster master)
        {
            orig(self, master);

            self.nameLabel.lineSpacing = -20;
            self.nameLabel.overflowMode = TMPro.TextOverflowModes.Truncate;
            self.nameLabel.enableWordWrapping = false;
            self.moneyText.overflowMode = TMPro.TextOverflowModes.Overflow;
        }

        int itemSum;
        int itemScore;
        internal void ScoreboardStrip_Update(On.RoR2.UI.ScoreboardStrip.orig_Update orig, ScoreboardStrip self)
        {
            orig(self);

            if (self.master && self.master.inventory)
            {
                BetterUI.sharedStringBuilder.Clear();
                BetterUI.sharedStringBuilder.Append(Util.GetBestMasterName(self.master));
                BetterUI.sharedStringBuilder.Append("\n<#F8FC97>");
                BetterUI.sharedStringBuilder.Append(self.master.money);
                BetterUI.sharedStringBuilder.Append("</color>");

                self.nameLabel.text = BetterUI.sharedStringBuilder.ToString();
                BetterUI.sharedStringBuilder.Clear();
                BetterUI.sharedStringBuilder.Append("<#FFFFFF>");



                if (mod.config.ItemCountersShowItemSum.Value)
                {
                    itemSum = 0;
                    foreach (var tier in mod.config.ItemCountersItemSumTiers)
                    {
                        itemSum += self.master.inventory.GetTotalItemCountOfTier(tier);
                    }
                    BetterUI.sharedStringBuilder.Append(itemSum);
                    if (mod.config.ItemCountersShowItemScore.Value)
                    {
                        BetterUI.sharedStringBuilder.Append(" | ");
                    }
                }
                if (mod.config.ItemCountersShowItemScore.Value)
                {
                    itemScore = 0;
                    foreach (var item in self.master.inventory.itemAcquisitionOrder)
                    {
                        itemScore += mod.config.ItemCountersItemScores[ItemCatalog.GetItemDef(item).nameToken] * self.master.inventory.GetItemCount(item);
                    }
                    BetterUI.sharedStringBuilder.Append(itemScore);
                }
                
                if (mod.config.ItemCountersShowItemsByTier.Value)
                {
                    BetterUI.sharedStringBuilder.Append("\n");
                    foreach(var tier in mod.config.ItemCountersItemsByTierOrder)
                    {
                        BetterUI.sharedStringBuilder.Append(" <#");
                        BetterUI.sharedStringBuilder.Append(tierColorMap[(int)tier]);
                        BetterUI.sharedStringBuilder.Append(">");
                        BetterUI.sharedStringBuilder.Append(self.master.inventory.GetTotalItemCountOfTier(tier));
                        BetterUI.sharedStringBuilder.Append("</color>");
                    }
                }

                BetterUI.sharedStringBuilder.Append("</color>"); 

                self.moneyText.text = BetterUI.sharedStringBuilder.ToString();
            }
        }


}
}
