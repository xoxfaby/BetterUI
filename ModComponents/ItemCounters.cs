﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

using RoR2;
using RoR2.UI;
using BepInEx.Configuration;

namespace BetterUI
{
    public static class ItemCounters
    {
        static string[] tierColorMap = new string[]
        {
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.Tier1Item),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.Tier2Item),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.Tier3Item),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.LunarItem),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.BossItem),
            ColorCatalog.GetColorHexString(ColorCatalog.ColorIndex.Error),
        };

        static ItemCounters()
        {
            Language.onCurrentLanguageChanged += GenerateItemScores;
            ItemCatalog.availability.CallWhenAvailable(GenerateItemScores);
        }
        internal static void Hook()
        {
            if (ConfigManager.ItemCountersShowItemCounters.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.ScoreboardStrip, CharacterMaster>("SetMaster", ScoreboardStrip_SetMaster);
                BetterUIPlugin.Hooks.Add<RoR2.UI.ScoreboardStrip>("Update", ScoreboardStrip_Update);
            }
        }

        private static void GenerateItemScores()
        {
            char[] bad_characters = new char[] { '\n', '\t', '\\', '"', '\'', '[', ']' };
            bool first = true;
            foreach (var itemIndex in RoR2.ItemCatalog.allItems)
            {
                ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
                String safe_name = String.Join("", itemDef.name.Split(bad_characters));
                if (String.IsNullOrWhiteSpace(safe_name))
                {
                    UnityEngine.Debug.LogError($"BetterUI: Unable to generate ItemScore config option for item {itemDef.name}: nameToken is empty! ItemScores may be unreliable.");
                    continue;
                }
                int itemValue = 0;
                ConfigManager.ItemCountersTierScores.TryGetValue(itemDef.tier, out itemValue);
                ConfigEntry<int> itemScore;
                if (first)
                {
                    itemScore = ConfigManager.ConfigFileItemCounters.Bind<int>("ItemScores", safe_name, itemValue, $"Score of each item for the ItemScore.\n{Language.GetString(itemDef.nameToken)}");
                    first = false;
                }
                else
                {
                    itemScore = ConfigManager.ConfigFileItemCounters.Bind<int>("ItemScores", safe_name, itemValue, Language.GetString(itemDef.nameToken));
                }

                ConfigManager.ItemCountersItemScores[itemDef] = itemScore.Value;
            }
        }
        internal static void ScoreboardStrip_SetMaster(Action<RoR2.UI.ScoreboardStrip, CharacterMaster> orig, ScoreboardStrip self, CharacterMaster master)
        {
            orig(self, master);

            self.nameLabel.lineSpacing = -20;
            self.nameLabel.overflowMode = TMPro.TextOverflowModes.Truncate;
            self.nameLabel.enableWordWrapping = false;
            self.moneyText.overflowMode = TMPro.TextOverflowModes.Overflow;
        }

        static int itemSum;
        static int itemScore;
        internal static void ScoreboardStrip_Update(Action<RoR2.UI.ScoreboardStrip> orig, ScoreboardStrip self)
        {
            orig(self);

            if (self.master && self.master.inventory)
            {
                BetterUIPlugin.sharedStringBuilder.Clear();
                BetterUIPlugin.sharedStringBuilder.Append(Util.GetBestMasterName(self.master));
                BetterUIPlugin.sharedStringBuilder.Append("\n<#F8FC97>");
                BetterUIPlugin.sharedStringBuilder.Append(self.master.money);
                BetterUIPlugin.sharedStringBuilder.Append("</color>");

                self.nameLabel.text = BetterUIPlugin.sharedStringBuilder.ToString();
                BetterUIPlugin.sharedStringBuilder.Clear();
                BetterUIPlugin.sharedStringBuilder.Append("<#FFFFFF>");



                if (ConfigManager.ItemCountersShowItemSum.Value)
                {
                    itemSum = 0;
                    foreach (var tier in ConfigManager.ItemCountersItemSumTiers)
                    {
                        itemSum += self.master.inventory.GetTotalItemCountOfTier(tier);
                    }
                    BetterUIPlugin.sharedStringBuilder.Append(itemSum);
                    if (ConfigManager.ItemCountersShowItemScore.Value)
                    {
                        BetterUIPlugin.sharedStringBuilder.Append(" | ");
                    }
                }
                if (ConfigManager.ItemCountersShowItemScore.Value)
                {
                    itemScore = 0;
                    foreach (var item in self.master.inventory.itemAcquisitionOrder)
                    {
                        itemScore += GetItemScore(ItemCatalog.GetItemDef(item)) * self.master.inventory.GetItemCount(item);
                    }
                    BetterUIPlugin.sharedStringBuilder.Append(itemScore);
                }

                if (ConfigManager.ItemCountersShowItemsByTier.Value)
                {
                    BetterUIPlugin.sharedStringBuilder.Append("\n");
                    foreach (var tier in ConfigManager.ItemCountersItemsByTierOrder)
                    {
                        BetterUIPlugin.sharedStringBuilder.Append(" <#");
                        BetterUIPlugin.sharedStringBuilder.Append(tierColorMap[(int)tier]);
                        BetterUIPlugin.sharedStringBuilder.Append(">");
                        BetterUIPlugin.sharedStringBuilder.Append(self.master.inventory.GetTotalItemCountOfTier(tier));
                        BetterUIPlugin.sharedStringBuilder.Append("</color>");
                    }
                }

                BetterUIPlugin.sharedStringBuilder.Append("</color>");

                self.moneyText.text = BetterUIPlugin.sharedStringBuilder.ToString();
            }
        }

        public static int GetItemScore(ItemDef item) {
            int value;
            return ConfigManager.ItemCountersItemScores.TryGetValue(item, out value) ? value : 0;
        }
    }
}
