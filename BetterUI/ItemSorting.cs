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

        public ItemSorting(ConfigManager c)
        {
            config = c;
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
            
            bool[] availableIndex = new bool[ItemCatalog.itemCount];
            foreach (RoR2.PickupPickerController.Option option in options)
            {
                availableIndex[(int) PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex] = option.available;
            }

            List<ItemIndex> sortedItems = sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex).ToList(), inventory, sortOrder);

            RoR2.PickupPickerController.Option[] sortedOptions = sortedItems.Select(itemIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(itemIndex), available = availableIndex[(int)itemIndex] }).ToArray();

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
