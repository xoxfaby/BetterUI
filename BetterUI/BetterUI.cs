using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

using RoR2;
using RoR2.UI;
using BepInEx;


namespace BetterUI
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "1.0.0")]


    public class BetterUI : BaseUnityPlugin
    {

        public static BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

        private ConfigManager config;
        public void Awake()
        {
            config = new ConfigManager(this);

            if (config.showHidden)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible += hook_ItemIsVisible;
            }

            if (config.AdvancedDescriptions)
            {
                On.RoR2.UI.GenericNotification.SetItem += hook_SetItem;
                On.RoR2.UI.ItemIcon.SetItemIndex += hook_SetItemIndex;
            }

            if (config.sortItems)
            { 
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged += hook_OnInventoryChanged;
            }
        }

        private void OnDestroy()
        {
            if (config.showHidden)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible -= hook_ItemIsVisible;
            }

            if (config.AdvancedDescriptions)
            {
                On.RoR2.UI.GenericNotification.SetItem -= hook_SetItem;
                On.RoR2.UI.ItemIcon.SetItemIndex -= hook_SetItemIndex;
            }

            if (config.sortItems)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged -= hook_OnInventoryChanged;
            }
        }


        private bool hook_ItemIsVisible(On.RoR2.UI.ItemInventoryDisplay.orig_ItemIsVisible orig, ItemIndex itemIndex)
        {
            return true;
        }

        private void hook_SetItem(On.RoR2.UI.GenericNotification.orig_SetItem orig, RoR2.UI.GenericNotification self, ItemDef itemDef)
        {
            orig(self, itemDef);

            self.descriptionText.token = itemDef.descriptionToken;
        }

        private void hook_SetItemIndex(On.RoR2.UI.ItemIcon.orig_SetItemIndex orig, RoR2.UI.ItemIcon self, ItemIndex itemIndex, int itemCount)
        {
            orig(self, itemIndex, itemCount);

            self.tooltipProvider.bodyToken = ItemCatalog.GetItemDef(itemIndex).descriptionToken;
        }
        private void hook_OnInventoryChanged(On.RoR2.UI.ItemInventoryDisplay.orig_OnInventoryChanged orig, RoR2.UI.ItemInventoryDisplay self)
        {

            orig(self);

            Inventory inventory = typeof(ItemInventoryDisplay).GetField("inventory", flags).GetValue(self) as Inventory;
            if (inventory)
            {
                if (!inventory.itemAcquisitionOrder.Any())
                {
                    System.Console.WriteLine("no any");
                    return;
                }
                IOrderedEnumerable<ItemIndex> finalOrder = inventory.itemAcquisitionOrder.OrderBy(a => 1);
                foreach (char c in config.sortOrder.ToCharArray())
                {
                    switch (c)
                    {
                        case '0': // Tier Ascending
                            finalOrder = finalOrder.ThenBy(item => config.tierOrder[(int)ItemCatalog.GetItemDef(item).tier]);
                            break;
                        case '1': // Tier Descending
                            finalOrder = finalOrder.ThenByDescending(item => config.tierOrder[(int)ItemCatalog.GetItemDef(item).tier]);
                            break;
                        case '2': // Stack Size Ascending
                        {
                            int[] itemStacks = (int[])typeof(ItemInventoryDisplay).GetField("itemStacks", flags).GetValue(self);
                            finalOrder = finalOrder.ThenBy(item => itemStacks[(int)item]);
                            break;
                        }
                        case '3': // Stack Size Descending
                            {
                                int[] itemStacks = (int[])typeof(ItemInventoryDisplay).GetField("itemStacks", flags).GetValue(self);
                            finalOrder = finalOrder.ThenByDescending(item => itemStacks[(int)item]);
                            break;
                        }
                        case '4': // Pickup Order
                            finalOrder = finalOrder.ThenBy(item => inventory.itemAcquisitionOrder.IndexOf(item));
                            break;
                        case '5': // Pickup Order Reversed
                            finalOrder = finalOrder.ThenByDescending(item => inventory.itemAcquisitionOrder.IndexOf(item));
                            break;
                        case '6': // Alphabetical
                            finalOrder = finalOrder.ThenBy(item => Language.GetString(ItemCatalog.GetItemDef(item).nameToken));
                            break;
                        case '7': // Alphabetical Reversed
                            finalOrder = finalOrder.ThenByDescending(item => Language.GetString(ItemCatalog.GetItemDef(item).nameToken));
                            break;
                        case '8': // Random"
                            Random random = new Random();
                            finalOrder = finalOrder.ThenBy(item => random.Next());
                            break;
                        case 's': // Scrap First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Scrap));
                            break;
                        case 'S': // Scrap Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.Scrap));
                            break;
                        case 'd': // Damage First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Damage));
                            break;
                        case 'D': // Damage Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.Damage));
                            break;
                        case 'h': // Healing First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Healing));
                            break;
                        case 'H': // Healing Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.Healing));
                            break;
                        case 'u': // Utility First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Utility));
                            break;
                        case 'U': // Utility Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.Utility));
                            break;
                        case 'o': // OnKillEffect First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.OnKillEffect));
                            break;
                        case 'O': // OnKillEffect Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.OnKillEffect));
                            break;
                        case 'e': // EquipmentRelated First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.EquipmentRelated));
                            break;
                        case 'E': // EquipmentRelated Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.EquipmentRelated));
                            break;
                        case 'p': // SprintRelated First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.SprintRelated));
                            break;
                        case 'P': // SprintRelated Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.SprintRelated));
                            break;
                    }
                }


                ItemIndex[] itemOrder = (ItemIndex[])typeof(ItemInventoryDisplay).GetField("itemOrder", flags).GetValue(self);
                if (itemOrder != null)
                {
                    finalOrder.ToList().CopyTo(itemOrder);
                }
            }
        }
    }
}
