using System;
using RoR2;
using System.Linq;
namespace BetterUI
{
    class ItemSorting
    {
        ConfigManager config;

        public ItemSorting(ConfigManager c)
        {
            config = c;
        }
        public void hook_OnInventoryChanged(On.RoR2.UI.ItemInventoryDisplay.orig_OnInventoryChanged orig, RoR2.UI.ItemInventoryDisplay self)
        {

            orig(self);


            if (self.inventory && self.inventory.itemAcquisitionOrder.Any())
            {
                IOrderedEnumerable<ItemIndex> finalOrder = self.inventory.itemAcquisitionOrder.OrderBy(a => 1);
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
                            finalOrder = finalOrder.ThenBy(item => self.inventory.itemStacks[(int)item]);
                            break;
                        case '3': // Stack Size Descending
                            finalOrder = finalOrder.ThenByDescending(item => self.inventory.itemStacks[(int)item]);
                            break;
                        case '4': // Pickup Order
                            finalOrder = finalOrder.ThenBy(item => self.inventory.itemAcquisitionOrder.IndexOf(item));
                            break;
                        case '5': // Pickup Order Reversed
                            finalOrder = finalOrder.ThenByDescending(item => self.inventory.itemAcquisitionOrder.IndexOf(item));
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

                if (self.itemOrder != null)
                {
                    finalOrder.ToList().CopyTo(self.itemOrder);
                }
            }
        }
    }
}
