using System;
using System.Collections.Generic;
using System.Linq;

using RoR2;
using RoR2.UI;
using UnityEngine;

namespace BetterUI
{
    static class CommandImprovements
    {
        static CommandImprovements()
        {
            BetterUIPlugin.onStart += onStart;
            BetterUIPlugin.onUpdate += onUpdate;
        }
        internal static void Hook() 
        { 
            if (ConfigManager.CommandResizeCommandWindow.Value ||
                ConfigManager.SortingSortItemsCommand.Value ||
                ConfigManager.SortingSortItemsScrapper.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.PickupPickerPanel, RoR2.PickupPickerController.Option[]>("SetPickupOptions", PickupPickerPanel_SetPickupOptions);
            }
            if (ConfigManager.CommandCloseOnEscape.Value ||
                ConfigManager.CommandCloseOnWASD.Value ||
                ConfigManager.CommandCloseOnCustom.Value != "")
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.PickupPickerPanel>("Awake", PickupPickerPanel_Awake);
            }

            if (ConfigManager.CommandTooltipsShow.Value ||
                ConfigManager.CommandCountersShow.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.PickupPickerPanel, int, MPButton>("OnCreateButton", PickupPickerPanel_OnCreateButton);
            }
        }
        private static void onStart()
        {
            ItemCatalog.availability.CallWhenAvailable(Init);
            EquipmentCatalog.availability.CallWhenAvailable(Init);
        }

        private static void onUpdate()
        {
            if (currentPanel && currentPanel.gameObject)
            {
                if (ConfigManager.CommandCloseOnEscape.Value && Input.GetKeyDown("escape") ||
                   ConfigManager.CommandCloseOnWASD.Value && (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")) ||
                   ConfigManager.CommandCloseOnCustom.Value != "" && Input.GetKeyDown(ConfigManager.CommandCloseOnCustom.Value))
                {
                    if (Input.GetKeyDown("escape"))
                    {
                        RoR2.Console.instance.SubmitCmd(null, "pause", false);
                    }
                    UnityEngine.Object.Destroy(currentPanel.gameObject);
                }
            }
        }


        private static void Init()
        {   
            if(ItemCatalog.availability.available && EquipmentCatalog.availability.available)
            {
                var maxOptions = ItemCatalog.itemCount + EquipmentCatalog.equipmentCount;
                optionMap = new int[maxOptions];
                optionMap[0] = -1;
                availableIndex = new bool[maxOptions];
                sortedOptions = new PickupPickerController.Option[maxOptions];
            }
        }

        private static PickupPickerPanel currentPanel;
        private static int[] optionMap;
        private static bool[] availableIndex;
        private static PickupPickerController.Option[] sortedOptions;
        private static String sortOrder;

        internal static ItemIndex[] lastItem = new ItemIndex[] {
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
            ItemIndex.None,
        };
        public static void PickupPickerPanel_Awake(Action<RoR2.UI.PickupPickerPanel> orig, PickupPickerPanel self)
        {
            currentPanel = self;
            orig(self);
        }
        public static void PickupPickerController_SubmitChoice(Action<RoR2.PickupPickerController, int> orig, RoR2.PickupPickerController self, int index)
        {
            orig(self, index);
            
            ItemDef itemDef = ItemCatalog.GetItemDef(PickupCatalog.GetPickupDef(self.options[index].pickupIndex).itemIndex);
            if(itemDef.itemIndex != ItemIndex.None)
            {
                lastItem[(int)itemDef.tier] = itemDef.itemIndex;
            }
        }

        public static void PickupPickerPanel_SetPickupOptions(Action<RoR2.UI.PickupPickerPanel, RoR2.PickupPickerController.Option[]> orig, RoR2.UI.PickupPickerPanel self, RoR2.PickupPickerController.Option[] options)
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
                    if (ConfigManager.CommandResizeCommandWindow.Value)
                    {
                        var mainPanelRectTransform = self.transform.Find("MainPanel").GetComponent<RectTransform>();
                        var mainContainerRectTransform = self.transform.parent.GetComponent<RectTransform>();
                        int columns = (int)Math.Ceiling(Math.Sqrt(options.Length));;
                        int defaultPanelHeight = 166 + (82 * (int)Math.Ceiling(options.Length / (float)columns));

                        if (defaultPanelHeight > mainContainerRectTransform.rect.height - 120)
                        {
                            columns = (int)Math.Ceiling(options.Length / Math.Floor((mainContainerRectTransform.rect.height - 286) / 82) );
                        }
                            
                        self.gridlayoutGroup.constraintCount = self.maxColumnCount = columns;
                        mainPanelRectTransform.sizeDelta = new Vector2(166 + 82 * columns, 166 + (82 * (float)Math.Ceiling(options.Length / (float)columns)));
                    }

                    if (ConfigManager.CommandRemoveBackgroundBlur.Value)
                    {
                        self.transform.GetComponent<LeTai.Asset.TranslucentImage.TranslucentImage>().enabled = false;
                    }
                    if (ConfigManager.SortingSortItemsCommand.Value)
                    {
                        sortOrder = ConfigManager.SortingSortOrderCommand.Value;
                        break;
                    }
                    goto default;
                case "SCRAPPER_CONTEXT":
                    if (ConfigManager.SortingSortItemsScrapper.Value)
                    {
                        sortOrder = ConfigManager.SortingSortOrderScrapper.Value;
                        break;
                    }
                    goto default;
                default:
                    orig(self, options);
                    return;
            }


            Inventory inventory = LocalUserManager.GetFirstLocalUser().cachedMasterController.master.inventory;

            if (options.All(option => PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex != EquipmentIndex.None))
            {
                foreach (RoR2.PickupPickerController.Option option in options)
                {
                    availableIndex[(int)PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex] = option.available;
                }

                List<EquipmentIndex> sortedItems = ItemSorting.sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).equipmentIndex).ToList(), sortOrder);

                sortedItems.Select(equipemntIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(equipemntIndex), available = availableIndex[(int)equipemntIndex] }).ToArray().CopyTo(sortedOptions, 0); ;
                sortedOptions.Select(option => Array.IndexOf(options, option)).ToArray().CopyTo(optionMap,0);
                options = sortedOptions.Take(options.Length).ToArray();
            }
            else if (options.All(option => PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex != ItemIndex.None))
            {
                foreach (RoR2.PickupPickerController.Option option in options)
                {
                    availableIndex[(int)PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex] = option.available;
                }

                List<ItemIndex> sortedItems = ItemSorting.sortItems(options.Select(option => PickupCatalog.GetPickupDef(option.pickupIndex).itemIndex).ToList(), inventory, sortOrder);

                sortedItems.Select(itemIndex => new RoR2.PickupPickerController.Option { pickupIndex = PickupCatalog.FindPickupIndex(itemIndex), available = availableIndex[(int)itemIndex] }).ToArray().CopyTo(sortedOptions, 0);
                sortedOptions.Select(option => Array.IndexOf(options, option)).ToArray().CopyTo(optionMap, 0);
                options = sortedOptions.Take(options.Length).ToArray();
            }

            orig(self, options);
        }

        public static void PickupPickerPanel_OnCreateButton(Action<RoR2.UI.PickupPickerPanel, int, MPButton> orig, RoR2.UI.PickupPickerPanel self, int index, MPButton button)
        {
            orig(self, optionMap[0] >= 0 ? optionMap[index] : index, button);

            if (ConfigManager.CommandTooltipsShow.Value || ConfigManager.CommandCountersShow.Value)
            {
                CharacterMaster master = LocalUserManager.GetFirstLocalUser().cachedMasterController.master;
                PickupDef pickupDef = PickupCatalog.GetPickupDef(self.pickerController.options[optionMap[0] >= 0 ? optionMap[index] : index ].pickupIndex);

                if (pickupDef.itemIndex != ItemIndex.None && ConfigManager.CommandCountersShow.Value)
                {
                    int count = master.inventory.itemStacks[(int)pickupDef.itemIndex];
                    if (!ConfigManager.CommandCountersHideOnZero.Value || count > 0)
                    {
                        GameObject textGameObject = new GameObject("StackText");
                        textGameObject.transform.SetParent(button.transform);
                        textGameObject.layer = 5;

                        RectTransform counterRect = textGameObject.AddComponent<RectTransform>();

                        HGTextMeshProUGUI counterText = textGameObject.AddComponent<HGTextMeshProUGUI>();
                        counterText.enableWordWrapping = false;
                        counterText.alignment = ConfigManager.CommandCountersTextAlignmentOption;
                        counterText.fontSize = ConfigManager.CommandCountersFontSize.Value;
                        counterText.faceColor = Color.white;
                        counterText.outlineWidth = 0.2f;
                        counterText.text = ConfigManager.CommandCountersPrefix.Value + count;

                        counterRect.localPosition = Vector3.zero;
                        counterRect.anchorMin = Vector2.zero;
                        counterRect.anchorMax = Vector2.one;
                        counterRect.localScale = Vector3.one;
                        counterRect.sizeDelta = new Vector2(-10, -4);
                        counterRect.anchoredPosition = Vector2.zero;
                    }
                }

                if (ConfigManager.CommandTooltipsShow.Value)
                {
                    TooltipProvider tooltipProvider = button.gameObject.AddComponent<TooltipProvider>();

                    if (pickupDef.itemIndex != ItemIndex.None)
                    {
                        ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);

                        if (BetterUIPlugin.ItemStatsModIntegration)
                        {
                            int count = master.inventory.itemStacks[(int)pickupDef.itemIndex];
                            string bodyText = Language.GetString(itemDef.descriptionToken);
                            if (self.pickerController.contextString == "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT" && ConfigManager.CommandTooltipsItemStatsBeforeAfter.Value && count > 0 )
                            {
                                bodyText += String.Format("\n\n<align=left>Before ({0} Stack" + (count > 1 ? "s" : "") + "):", count);
                                String[] descLines = ItemStats.GetItemStats(itemDef, count).Split(new String[] { "\n", "<br>" }, StringSplitOptions.None);
                                bodyText += String.Join("\n", descLines.Take(descLines.Length - 1).Skip(1));
                                bodyText += String.Format("\n\n<align=left>After ({0} Stacks):", count + 1);
                                descLines = ItemStats.GetItemStats(itemDef, count + 1).Split(new String[] { "\n", "<br>" }, StringSplitOptions.None);
                                bodyText += String.Join("\n", descLines.Take(descLines.Length - 1).Skip(1));
                            }
                            else
                            {
                                if(self.pickerController.contextString == "ARTIFACT_COMMAND_CUBE_INTERACTION_PROMPT")
                                {
                                    count += 1;
                                }
                                bodyText += ItemStats.GetItemStats(itemDef, count);
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
