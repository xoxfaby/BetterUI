using System;
using RoR2;
using BepInEx;
using System.Linq;
using System.Collections.Generic;
namespace BetterUI
{
    class ItemSorting
    {
        private ConfigManager config;
        private ItemIndex[] lastItem = new ItemIndex[] {
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
        };

        public ItemSorting(ConfigManager c)
        {
            config = c;
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
                        finalOrder = finalOrder.ThenBy(item => config.tierOrder[(int)ItemCatalog.GetItemDef(item).tier]);
                        break;
                    case '1': // Tier Descending
                        finalOrder = finalOrder.ThenByDescending(item => config.tierOrder[(int)ItemCatalog.GetItemDef(item).tier]);
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
                        if (firstItemDef != null && lastItem[(int)firstItemDef.tier] != ItemIndex.None && finalOrder.Contains(lastItem[(int)firstItemDef.tier])) 
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
                            finalOrderList.Remove(lastItem[(int)firstItemDef.tier]);
                            finalOrderList.Insert(offset, lastItem[(int)firstItemDef.tier]);
                            finalOrder = finalOrderList.OrderBy(a => 1);
                        }
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
        public void hook_OnInventoryChanged(On.RoR2.UI.ItemInventoryDisplay.orig_OnInventoryChanged orig, RoR2.UI.ItemInventoryDisplay self)
        {

            orig(self);

            if (self.itemOrder != null && self.inventory && self.inventory.itemAcquisitionOrder.Any())
            {
                sortItems(self.inventory.itemAcquisitionOrder, self.inventory, config.sortOrder.Value).ToList().CopyTo(self.itemOrder);
            }
        }

        public void hook_SubmitChoice(On.RoR2.PickupPickerController.orig_SubmitChoice orig, RoR2.PickupPickerController self, int index)
        {
            orig(self, index);
            if(PickupCatalog.GetPickupDef(self.options[0].pickupIndex).itemIndex != ItemIndex.None)
            {
                ItemDef itemDef = ItemCatalog.GetItemDef(PickupCatalog.GetPickupDef(self.options[index].pickupIndex).itemIndex);
                lastItem[(int)itemDef.tier] = itemDef.itemIndex;
            }
        }

        public void hook_SetPickupOptions(On.RoR2.UI.PickupPickerPanel.orig_SetPickupOptions orig, RoR2.UI.PickupPickerPanel self, RoR2.PickupPickerController.Option[] options)
        {
            if (self.pickerController.contextString == "SCRAPPER_CONTEXT" && !config.sortItemsScrapper.Value || 
                self.pickerController.contextString == "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT" && !config.sortItemsCommand.Value)
            {
                orig(self, options);
                return;
            }

            String sortOrder;
            switch (self.pickerController.contextString)
            {
                case "SCRAPPER_CONTEXT":
                    sortOrder = config.sortOrderScrapper.Value;
                    break;
                case "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT":
                    sortOrder = config.sortOrderCommand.Value;
                    break;
                default:
                    sortOrder = config.sortOrder.Value;
                    break;
            }
                  
            Inventory inventory = LocalUserManager.GetFirstLocalUser().cachedMasterController.master.inventory;
            RoR2.PickupPickerController.Option[] sortedOptions;
            if (PickupCatalog.GetPickupDef(options[0].pickupIndex).equipmentIndex != EquipmentIndex.None)
            {
                bool[] availableIndex = new bool[EquipmentCatalog.equipmentCount];
                foreach (RoR2.PickupPickerController.Option option in options)
                {
                    availableIndex[(int)PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex] = option.available;
                }

                List<EquipmentIndex> sortedItems = sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex).ToList(), sortOrder);

                sortedOptions = sortedItems.Select(equipmentIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(equipmentIndex), available = availableIndex[(int)equipmentIndex] }).ToArray();

            }
            else if(PickupCatalog.GetPickupDef(options[0].pickupIndex).itemIndex != ItemIndex.None)
            {
                bool[] availableIndex = new bool[ItemCatalog.itemCount];
                foreach (RoR2.PickupPickerController.Option option in options)
                {
                    availableIndex[(int)PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex] = option.available;
                }

                List<ItemIndex> sortedItems = sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex).ToList(), inventory, sortOrder);

                sortedOptions = sortedItems.Select(itemIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(itemIndex), available = availableIndex[(int)itemIndex] }).ToArray();

            }
            else
            {
                orig(self, options);
                return;
            }


            int[] optionMap = sortedOptions.Select(option => Array.IndexOf(options,option)).ToArray();


            //This feels dirty but it's the only way I can think to do it rn

            void hook_OnCreateButton(On.RoR2.UI.PickupPickerPanel.orig_OnCreateButton orig2, RoR2.UI.PickupPickerPanel self2, int index, RoR2.UI.MPButton button)
            {
                orig2(self2, optionMap[index], button);
            }

            On.RoR2.UI.PickupPickerPanel.OnCreateButton += hook_OnCreateButton;

            orig(self, sortedOptions);

            On.RoR2.UI.PickupPickerPanel.OnCreateButton -= hook_OnCreateButton;
        }
    }
}
