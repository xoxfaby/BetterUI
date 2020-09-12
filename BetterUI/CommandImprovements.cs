using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using RoR2;
using RoR2.UI;
using BepInEx;
using UnityEngine;

namespace BetterUI
{
    class CommandImprovements
    {
        private readonly BetterUI mod;

        private int[] optionMap;

        internal ItemIndex[] lastItem = new ItemIndex[] {
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
        };
        public CommandImprovements(BetterUI m)
        {
            mod = m;
        }

        public void hook_SubmitChoice(On.RoR2.PickupPickerController.orig_SubmitChoice orig, RoR2.PickupPickerController self, int index)
        {
            orig(self, optionMap[index]);
            if (PickupCatalog.GetPickupDef(self.options[0].pickupIndex).itemIndex != ItemIndex.None)
            {
                ItemDef itemDef = ItemCatalog.GetItemDef(PickupCatalog.GetPickupDef(self.options[optionMap[index]].pickupIndex).itemIndex);
                lastItem[(int)itemDef.tier] = itemDef.itemIndex;
            }
        }

        public void hook_SetPickupOptions(On.RoR2.UI.PickupPickerPanel.orig_SetPickupOptions orig, RoR2.UI.PickupPickerPanel self, RoR2.PickupPickerController.Option[] options)
        {
            if (self.pickerController.contextString == "SCRAPPER_CONTEXT" && !mod.config.sortItemsScrapper.Value ||
                self.pickerController.contextString == "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT" && !mod.config.sortItemsCommand.Value)
            {
                orig(self, options);
                return;
            }

            String sortOrder;
            switch (self.pickerController.contextString)
            {
                case "SCRAPPER_CONTEXT":
                    sortOrder = mod.config.sortOrderScrapper.Value;
                    break;
                case "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT":
                    sortOrder = mod.config.sortOrderCommand.Value;
                    break;
                default:
                    sortOrder = mod.config.sortOrder.Value;
                    break;
            }


            Inventory inventory = LocalUserManager.GetFirstLocalUser().cachedMasterController.master.inventory;

            if (PickupCatalog.GetPickupDef(options[0].pickupIndex).equipmentIndex != EquipmentIndex.None)
            {
                bool[] availableIndex = new bool[EquipmentCatalog.equipmentCount];
                foreach (RoR2.PickupPickerController.Option option in options)
                {
                    availableIndex[(int)PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex] = option.available;
                }

                List<EquipmentIndex> sortedItems = mod.itemSorting.sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex).ToList(), sortOrder);

                PickupPickerController.Option[] sortedOptions = sortedItems.Select(itemIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(itemIndex), available = availableIndex[(int)itemIndex] }).ToArray();
                optionMap = sortedOptions.Select(option => Array.IndexOf(options, option)).ToArray();
                options = sortedOptions;
            }
            else if (PickupCatalog.GetPickupDef(options[0].pickupIndex).itemIndex != ItemIndex.None)
            {
                bool[] availableIndex = new bool[ItemCatalog.itemCount];
                foreach (RoR2.PickupPickerController.Option option in options)
                {
                    availableIndex[(int)PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex] = option.available;
                }

                List<ItemIndex> sortedItems = mod.itemSorting.sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex).ToList(), inventory, sortOrder);

                PickupPickerController.Option[] sortedOptions = sortedItems.Select(itemIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(itemIndex), available = availableIndex[(int)itemIndex] }).ToArray();
                optionMap = sortedOptions.Select(option => Array.IndexOf(options, option)).ToArray();
                options = sortedOptions;
            }

            orig(self, options);
        }

        public void hook_OnCreateButton(On.RoR2.UI.PickupPickerPanel.orig_OnCreateButton orig, RoR2.UI.PickupPickerPanel self, int index, MPButton button)
        {
            orig(self, index, button);


            GameObject textGameObject = new GameObject("StackText");
            textGameObject.transform.SetParent(button.transform);
            textGameObject.layer = 5;


            textGameObject.AddComponent<CanvasRenderer>();

            RectTransform counterRect = textGameObject.AddComponent<RectTransform>();
            HGTextMeshProUGUI counterText = textGameObject.AddComponent<HGTextMeshProUGUI>();
            TooltipProvider tooltipProvider = textGameObject.AddComponent<TooltipProvider>();

            counterRect.localPosition = Vector3.zero;
            counterRect.anchorMin = Vector2.zero;
            counterRect.anchorMax = Vector2.one;
            counterRect.localScale = Vector3.one;
            counterRect.sizeDelta = new Vector2(-10, -4);
            counterRect.anchoredPosition = Vector2.zero;

            counterText.enableWordWrapping = false;
            counterText.alignment = mod.config.counterTextAlignmentOption;
            counterText.fontSize = mod.config.counterFontSize.Value;
            counterText.faceColor = Color.white;
            counterText.outlineWidth = 0.2f;



            PickupDef pickupDef;
            if (optionMap != null)
            {
                pickupDef = PickupCatalog.GetPickupDef(self.pickerController.options[optionMap[index]].pickupIndex);
            }
            else
            {
                pickupDef = PickupCatalog.GetPickupDef(self.pickerController.options[index].pickupIndex);
            }


            if (pickupDef.itemIndex != ItemIndex.None)
            {
                Inventory inventory = LocalUserManager.GetFirstLocalUser().cachedMasterController.master.inventory;
                ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);

                counterText.text = mod.config.counterPrefix.Value + inventory.itemStacks[(int) itemDef.itemIndex];



                tooltipProvider.titleToken = itemDef.nameToken;
                tooltipProvider.bodyToken = itemDef.descriptionToken;
                tooltipProvider.titleColor = ColorCatalog.GetColor(itemDef.darkColorIndex); ;
                tooltipProvider.bodyColor = new Color(0.6f, 0.6f, 0.6f, 1f);

            }
            else if(pickupDef.equipmentIndex != EquipmentIndex.None)    
            {
                EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(pickupDef.equipmentIndex);

                tooltipProvider.titleToken = equipmentDef.nameToken;
                tooltipProvider.bodyToken = equipmentDef.descriptionToken;
                tooltipProvider.titleColor = ColorCatalog.GetColor(equipmentDef.colorIndex);
                tooltipProvider.bodyColor = Color.gray;
            }
        }
    }
}
