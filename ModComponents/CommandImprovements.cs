using System;
using System.Collections.Generic;
using System.Linq;

using RoR2;
using RoR2.UI;
using UnityEngine;

namespace BetterUI
{
    class CommandImprovements : BetterUI.ModComponent
    {
        public CommandImprovements(BetterUI mod) : base(mod) { }

        internal override void Hook()
        {
            if (mod.config.CommandResizeCommandWindow.Value ||
                mod.config.SortingSortItemsScrapper.Value ||
                mod.config.SortingSortItemsScrapper.Value)
            {
                On.RoR2.UI.PickupPickerPanel.SetPickupOptions += SetPickupOptions;
            }
            if (mod.config.CommandCloseOnEscape.Value ||
                mod.config.CommandCloseOnWASD.Value ||
                mod.config.CommandCloseOnCustom.Value != "")
            {
                On.RoR2.UI.PickupPickerPanel.Awake += mod.commandImprovements.PickupPickerPanelAwake;
            }

            if (mod.config.CommandTooltipsShow.Value ||
                mod.config.CommandCountersShow.Value)
            {
                On.RoR2.UI.PickupPickerPanel.OnCreateButton += mod.commandImprovements.OnCreateButton;
            }
        }

        internal override void Start()
        {
            var maxOptions = Math.Max(ItemCatalog.itemCount, EquipmentCatalog.equipmentCount);
            optionMap = new int[maxOptions];
            availableIndex = new bool[maxOptions];
            sortedOptions = new PickupPickerController.Option[maxOptions];
        }

        private PickupPickerPanel currentPanel;
        private int[] optionMap;
        private bool[] availableIndex;
        PickupPickerController.Option[] sortedOptions;
        private String sortOrder;

        internal ItemIndex[] lastItem = new ItemIndex[] {
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
        };
        public void PickupPickerPanelAwake(On.RoR2.UI.PickupPickerPanel.orig_Awake orig, PickupPickerPanel self)
        {
            currentPanel = self;
            orig(self);
        }
        internal override void Update()
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
        public void SubmitChoice(On.RoR2.PickupPickerController.orig_SubmitChoice orig, RoR2.PickupPickerController self, int index)
        {
            orig(self, index);
            
            ItemDef itemDef = ItemCatalog.GetItemDef(PickupCatalog.GetPickupDef(self.options[index].pickupIndex).itemIndex);
            if(itemDef.itemIndex != ItemIndex.None)
            {
                lastItem[(int)itemDef.tier] = itemDef.itemIndex;
            }
        }

        public void SetPickupOptions(On.RoR2.UI.PickupPickerPanel.orig_SetPickupOptions orig, RoR2.UI.PickupPickerPanel self, RoR2.PickupPickerController.Option[] options)
        {
            if (options == null || options.Length == 0)
            {
                orig(self, options);
                return;
            }


            self.buttonAllocator.AllocateElements(0);
            optionMap[0] = -1;
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
                foreach (RoR2.PickupPickerController.Option option in options)
                {
                    availableIndex[(int)PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex] = option.available;
                }

                List<EquipmentIndex> sortedItems = mod.itemSorting.sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex).ToList(), sortOrder);

                sortedItems.Select(equipemntIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(equipemntIndex), available = availableIndex[(int)equipemntIndex] }).ToArray().CopyTo(sortedOptions, 0); ;
                sortedOptions.Select(option => Array.IndexOf(options, option)).ToArray().CopyTo(optionMap,0);
                options = sortedOptions.Take(options.Length).ToArray();
            }
            else if (PickupCatalog.GetPickupDef(options[0].pickupIndex).itemIndex != ItemIndex.None)
            {
                bool[] availableIndex = new bool[ItemCatalog.itemCount];
                foreach (RoR2.PickupPickerController.Option option in options)
                {
                    availableIndex[(int)PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex] = option.available;
                }

                List<ItemIndex> sortedItems = mod.itemSorting.sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex).ToList(), inventory, sortOrder);

                sortedItems.Select(itemIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(itemIndex), available = availableIndex[(int)itemIndex] }).ToArray().CopyTo(sortedOptions, 0);
                sortedOptions.Select(option => Array.IndexOf(options, option)).ToArray().CopyTo(optionMap, 0);
                options = sortedOptions.Take(options.Length).ToArray();
            }

            orig(self, options);
        }

        public void OnCreateButton(On.RoR2.UI.PickupPickerPanel.orig_OnCreateButton orig, RoR2.UI.PickupPickerPanel self, int index, MPButton button)
        {
            orig(self, optionMap[0] >= 0 ? optionMap[index] : index, button);

            if (mod.config.CommandTooltipsShow.Value || mod.config.CommandCountersShow.Value)
            {
                CharacterMaster master = LocalUserManager.GetFirstLocalUser().cachedMasterController.master;
                PickupDef pickupDef = PickupCatalog.GetPickupDef(self.pickerController.options[optionMap[0] >= 0 ? optionMap[index] : index ].pickupIndex);

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
                            if (self.pickerController.contextString == "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT" && mod.config.CommandTooltipsItemStatsBeforeAfter.Value && count > 0 )
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
                                if(self.pickerController.contextString == "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT")
                                {
                                    count += 1;
                                }
                                bodyText += ModCompat.statsFromItemStats(itemDef.itemIndex, count, master);
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
