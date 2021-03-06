﻿using System;
using RoR2;
using BepInEx;
using System.Linq;
using System.Collections.Generic;
namespace BetterUI
{
    class ItemSorting : BetterUI.ModComponent
    {
        public ItemSorting(BetterUI mod) : base(mod) { }

        internal override void Hook()
        {
            if (mod.config.SortingSortItemsInventory.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged += mod.itemSorting.ItemInventoryDisplay_OnInventoryChanged;
            }
            if (mod.config.SortingSortItemsCommand.Value && mod.config.SortingSortOrderCommand.Value.Contains("C"))
            {
                On.RoR2.PickupPickerController.SubmitChoice += mod.commandImprovements.PickupPickerController_SubmitChoice;
            }
        }

        public List<EquipmentIndex> sortItems(List<EquipmentIndex> equipmentList, String sortOrder)
        { 
            IOrderedEnumerable<EquipmentIndex> finalOrder = equipmentList.OrderBy(a => 1);
            foreach (char c in sortOrder.ToCharArray())
            {
                switch (c)
                {
                    case '6': // Alphabetical
                        finalOrder = finalOrder.ThenBy(equipment => Language.GetString(EquipmentCatalog.GetEquipmentDef(equipment).nameToken));
                        break;
                    case '7': // Alphabetical Reversed
                        finalOrder = finalOrder.ThenByDescending(equipment => Language.GetString(EquipmentCatalog.GetEquipmentDef(equipment).nameToken));
                        break;
                    case '8': // Random"
                        Random random = new Random();
                        finalOrder = finalOrder.ThenBy(equipment => random.Next());
                        break;
                }
            }
            return equipmentList;
        }

        public List<ItemIndex> sortItems(List<ItemIndex> itemList, Inventory inventory, String sortOrder)
        {

            IOrderedEnumerable<ItemIndex> finalOrder = itemList.OrderBy(a => 1);
            foreach (char c in sortOrder.ToCharArray())
            {
                switch (c)
                {
                    case '0': // Tier Ascending
                        finalOrder = finalOrder.ThenBy(item => mod.config.SortingTierOrder[(int)ItemCatalog.GetItemDef(item).tier]);
                        break;
                    case '1': // Tier Descending
                        finalOrder = finalOrder.ThenByDescending(item => mod.config.SortingTierOrder[(int)ItemCatalog.GetItemDef(item).tier]);
                        break;
                    case '2': // Stack Size Ascending
                        finalOrder = finalOrder.ThenBy(item => inventory.itemStacks[(int)item]);
                        break;
                    case '3': // Stack Size Descending
                        finalOrder = finalOrder.ThenByDescending(item => inventory.itemStacks[(int)item]);
                        break;
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
                    case 'C': // Special Command Centered
                        ItemDef firstItemDef = ItemCatalog.GetItemDef(finalOrder.First());
                        if (firstItemDef != null && mod.commandImprovements.lastItem[(int)firstItemDef.tier] != ItemIndex.None && finalOrder.Contains(mod.commandImprovements.lastItem[(int)firstItemDef.tier])) 
                        {
                            int roundUp = (int)Math.Ceiling((double)finalOrder.Count() / 5) * 5;
                            int offset;
                            if (roundUp == 5) 
                            {
                                offset = finalOrder.Count() / 2;
                            } else if (roundUp % 10 == 0)
                            {
                                offset = (roundUp / 2) - 3 ;
                            }
                            else
                            {
                                offset = roundUp / 2;
                            }
                            List<ItemIndex> finalOrderList = finalOrder.ToList();
                            finalOrderList.Remove(mod.commandImprovements.lastItem[(int)firstItemDef.tier]);
                            finalOrderList.Insert(offset, mod.commandImprovements.lastItem[(int)firstItemDef.tier]);
                            finalOrder = finalOrderList.OrderBy(a => 1);
                        }
                        break;

                    case 'i': // ItemIndex Ascending
                        finalOrder = finalOrder.ThenBy(itemIndex => itemIndex);
                        break;
                    case 'I': // ItemIndex Ascending
                        finalOrder = finalOrder.ThenByDescending(itemIndex => itemIndex);
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
            return finalOrder.ToList();
        }
        public void ItemInventoryDisplay_OnInventoryChanged(On.RoR2.UI.ItemInventoryDisplay.orig_OnInventoryChanged orig, RoR2.UI.ItemInventoryDisplay self)
        {

            orig(self);

            if (self.itemOrder != null && self.inventory && self.inventory.itemAcquisitionOrder.Any())
            {
                sortItems(self.inventory.itemAcquisitionOrder, self.inventory, mod.config.SortingSortOrder.Value).ToList().CopyTo(self.itemOrder);
            }
        }
    }
}
