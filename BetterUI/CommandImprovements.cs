using System;
using System.Collections.Generic;
using System.Linq;

using RoR2;
using RoR2.UI;
using UnityEngine;

namespace BetterUI
{
    class CommandImprovements
    {
        private readonly BetterUI mod;

        private PickupPickerPanel currentPanel;
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

        public void hook_PickupPickerPanelAwake(On.RoR2.UI.PickupPickerPanel.orig_Awake orig, PickupPickerPanel self)
        {
            currentPanel = self;
            orig(self);
        }
        public void Update()
        {
            if(currentPanel && currentPanel.gameObject)
            {
                if (mod.config.CommandCloseOnEscape.Value && Input.GetKeyDown("escape") ||
                   mod.config.CommandCloseOnWASD.Value && (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d") ) ||
                   mod.config.CommandCloseOnCustom.Value != "" && Input.GetKeyDown(mod.config.CommandCloseOnCustom.Value))
                {
                    if(Input.GetKeyDown("escape"))
                    {
                        RoR2.Console.instance.SubmitCmd(null, "pause", false);
                    }
                    BetterUI.Destroy(currentPanel.gameObject);
                }
            }

        }
        public void hook_SubmitChoice(On.RoR2.PickupPickerController.orig_SubmitChoice orig, RoR2.PickupPickerController self, int index)
        {
            orig(self, index);
            
            ItemDef itemDef = ItemCatalog.GetItemDef(PickupCatalog.GetPickupDef(self.options[index].pickupIndex).itemIndex);
            if(itemDef.itemIndex != ItemIndex.None)
            {
                lastItem[(int)itemDef.tier] = itemDef.itemIndex;
            }
        }

        public void hook_SetPickupOptions(On.RoR2.UI.PickupPickerPanel.orig_SetPickupOptions orig, RoR2.UI.PickupPickerPanel self, RoR2.PickupPickerController.Option[] options)
        {
            if (options == null || options.Length == 0)
            {
                orig(self, options);
                return;
            }

            String sortOrder;
            switch (self.pickerController.contextString)
            {
                case "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT":
                    if (mod.config.CommandResizeCommandWindow.Value)
                    {
                        self.transform.Find("MainPanel").GetComponent<RectTransform>().sizeDelta = new Vector2(576, 166 + (82 * (float)Math.Ceiling(options.Length / 5f)));
                    }

                    if (mod.config.CommandRemoveBackgroundBlur.Value)
                    {
                        self.transform.GetComponent<LeTai.Asset.TranslucentImage.TranslucentImage>().enabled = false;
                    }
                    if (mod.config.SortingSortItemsCommand.Value)
                    {
                        sortOrder = mod.config.SortingSortOrderCommand.Value;
                        break;
                    }
                    goto default;
                case "SCRAPPER_CONTEXT":
                    if (mod.config.SortingSortItemsScrapper.Value)
                    {
                        sortOrder = mod.config.SortingSortOrderScrapper.Value;
                        break;
                    }
                    goto default;
                default:
                    orig(self, options);
                    return;
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

                PickupPickerController.Option[] sortedOptions = sortedItems.Select(equipemntIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(equipemntIndex), available = availableIndex[(int)equipemntIndex] }).ToArray();
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
            orig(self, optionMap != null ? optionMap[index] : index, button);

            if (mod.config.CommandTooltipsShow.Value || mod.config.CommandCountersShow.Value)
            {
                CharacterMaster master = LocalUserManager.GetFirstLocalUser().cachedMasterController.master;
                PickupDef pickupDef = PickupCatalog.GetPickupDef(self.pickerController.options[optionMap != null ? optionMap[index] : index ].pickupIndex);

                if (pickupDef.itemIndex != ItemIndex.None && mod.config.CommandCountersShow.Value)
                {
                    int count = master.inventory.itemStacks[(int)pickupDef.itemIndex];
                    if (!mod.config.CommandCountersHideOnZero.Value || count > 0)
                    {
                        GameObject textGameObject = new GameObject("StackText");
                        textGameObject.transform.SetParent(button.transform);
                        textGameObject.layer = 5;

                        RectTransform counterRect = textGameObject.AddComponent<RectTransform>();

                        HGTextMeshProUGUI counterText = textGameObject.AddComponent<HGTextMeshProUGUI>();
                        counterText.enableWordWrapping = false;
                        counterText.alignment = mod.config.CommandCountersTextAlignmentOption;
                        counterText.fontSize = mod.config.CommandCountersFontSize.Value;
                        counterText.faceColor = Color.white;
                        counterText.outlineWidth = 0.2f;
                        counterText.text = mod.config.CommandCountersPrefix.Value + count;

                        counterRect.localPosition = Vector3.zero;
                        counterRect.anchorMin = Vector2.zero;
                        counterRect.anchorMax = Vector2.one;
                        counterRect.localScale = Vector3.one;
                        counterRect.sizeDelta = new Vector2(-10, -4);
                        counterRect.anchoredPosition = Vector2.zero;
                    }
                }

                if (mod.config.CommandTooltipsShow.Value)
                {
                    TooltipProvider tooltipProvider = button.gameObject.AddComponent<TooltipProvider>();

                    if (pickupDef.itemIndex != ItemIndex.None)
                    {
                        ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);

                        if (mod.ItemStatsModIntegration)
                        {
                            int count = master.inventory.itemStacks[(int)pickupDef.itemIndex];
                            string bodyText = Language.GetString(itemDef.descriptionToken);
                            if (mod.config.CommandTooltipsItemStatsBeforeAfter.Value && count > 0)
                            {
                                bodyText += String.Format("\n\n<align=left>Before ({0} Stack" + (count > 1 ? "s" : "") + "):", count);
                                String[] descLines = ModCompat.statsFromItemStats(itemDef.itemIndex, count, master).Split(new String[] { "\n", "<br>" }, StringSplitOptions.None);
                                bodyText += String.Join("\n", descLines.Take(descLines.Length - 1).Skip(1));
                                bodyText += String.Format("\n\n<align=left>After ({0} Stacks):", count + 1);
                                descLines = ModCompat.statsFromItemStats(itemDef.itemIndex, count + 1, master).Split(new String[] { "\n", "<br>" }, StringSplitOptions.None);
                                bodyText += String.Join("\n", descLines.Take(descLines.Length - 1).Skip(1));
                            }
                            else
                            {
                                bodyText += ModCompat.statsFromItemStats(itemDef.itemIndex, count + 1, master);
                            }

                            tooltipProvider.overrideBodyText = bodyText;
                        }
                        else
                        {
                            tooltipProvider.bodyToken = itemDef.descriptionToken;
                        }

                        tooltipProvider.titleToken = itemDef.nameToken;
                        tooltipProvider.titleColor = ColorCatalog.GetColor(itemDef.darkColorIndex); ;
                        tooltipProvider.bodyColor = new Color(0.6f, 0.6f, 0.6f, 1f);
                    }
                    else if (pickupDef.equipmentIndex != EquipmentIndex.None)
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
    }
}
