using System;
using System.Linq;

using RoR2;
using R2API.Utils;
using BepInEx;
using UnityEngine;


namespace BetterUI
{
    [BepInDependency("dev.ontrigger.itemstats", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "1.4.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class BetterUI : BaseUnityPlugin
    {
        internal ConfigManager config;
        internal ItemSorting itemSorting;
        internal StatsDisplay statsDisplay;
        internal CommandImprovements commandImprovements;
        internal DPSMeter DPSMeter;
        internal bool ItemStatsModIntegration;
        internal RoR2.UI.HUD HUD;
        public void Awake()
        {
            BepInExPatcher.DoPatching();

            config = new ConfigManager(this);
            itemSorting = new ItemSorting(this);
            statsDisplay = new StatsDisplay(this);
            commandImprovements = new CommandImprovements(this);
            DPSMeter = new DPSMeter(this);
        }

        public void Update()
        {
            commandImprovements.Update();
            DPSMeter.Update();
            statsDisplay.Update();
        }
        public void OnEnable()
        {
            if (config.MiscAdvancedDescriptions.Value)
            {
                On.RoR2.UI.ItemIcon.SetItemIndex += hook_SetItemIndex;
                On.RoR2.UI.EquipmentIcon.Update += hook_EquipmentIconUpdate;
            }
            if (config.MiscAdvancedPickupNotifications.Value)
            {
                On.RoR2.UI.GenericNotification.SetItem += hook_SetItem;
                On.RoR2.UI.GenericNotification.SetEquipment += hook_SetEquipment;
            }

            ItemStatsModIntegration = config.MiscItemStatsIntegration.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("dev.ontrigger.itemstats");
 
            if (config.MiscShowHidden.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible += hook_ItemIsVisible;
            }


            if (config.CommandResizeCommandWindow.Value ||
                config.SortingSortItemsScrapper.Value || 
                config.SortingSortItemsScrapper.Value)
            {
                On.RoR2.UI.PickupPickerPanel.SetPickupOptions += commandImprovements.hook_SetPickupOptions;
            }

            if (config.CommandCloseOnEscape.Value ||
                config.CommandCloseOnEscape.Value ||
                config.CommandCloseOnCustom.Value != "")
            {
                On.RoR2.UI.PickupPickerPanel.Awake += commandImprovements.hook_PickupPickerPanelAwake;
            }

            if (config.CommandTooltipsShow.Value ||
                config.CommandCountersShow.Value)
            {
                On.RoR2.UI.PickupPickerPanel.OnCreateButton += commandImprovements.hook_OnCreateButton;
            }

            if (config.DPSMeterWindowShow.Value ||
                config.StatsDisplayStatString.Value.Contains("$dps"))
            {
                On.RoR2.GlobalEventManager.ClientDamageNotified += DPSMeter.hook_ClientDamageNotified;
            }

            if (config.DPSMeterWindowShow.Value)
            {
                On.RoR2.UI.HUD.Awake += DPSMeter.hook_Awake;
            }

            if (config.StatsDisplayEnable.Value)
            {
                RoR2.Run.onRunStartGlobal += statsDisplay.hook_runStartGlobal;
                On.RoR2.UI.HUD.Awake += statsDisplay.hook_Awake;
            }

            if (config.SortingSortItemsInventory.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged += itemSorting.hook_OnInventoryChanged;
            }
            if (config.SortingSortItemsCommand.Value ||
                config.SortingSortItemsScrapper.Value)
            {
                On.RoR2.PickupPickerController.SubmitChoice += commandImprovements.hook_SubmitChoice;
            }
            if(config.BuffTimers.Value || config.BuffTooltips.Value)
            {
                On.RoR2.UI.BuffIcon.Awake += hook_BuffIconAwake;
                On.RoR2.UI.BuffIcon.UpdateIcon += hook_BuffIconUpdateIcon;
                On.RoR2.UI.HUD.Awake += hook_HUDAwake;
            }
        }

        private void OnDisable()
        {
            if (config.MiscAdvancedDescriptions.Value)
            {
                On.RoR2.UI.ItemIcon.SetItemIndex -= hook_SetItemIndex;
                On.RoR2.UI.EquipmentIcon.Update -= hook_EquipmentIconUpdate;
            }
            if (config.MiscAdvancedPickupNotifications.Value)
            {
                On.RoR2.UI.GenericNotification.SetItem -= hook_SetItem;
                On.RoR2.UI.GenericNotification.SetEquipment -= hook_SetEquipment;
            }

            ItemStatsModIntegration = config.MiscItemStatsIntegration.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("dev.ontrigger.itemstats");

            if (config.MiscShowHidden.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible -= hook_ItemIsVisible;
            }


            if (config.CommandResizeCommandWindow.Value ||
                config.SortingSortItemsScrapper.Value ||
                config.SortingSortItemsScrapper.Value)
            {
                On.RoR2.UI.PickupPickerPanel.SetPickupOptions -= commandImprovements.hook_SetPickupOptions;
            }

            if (config.CommandCloseOnEscape.Value ||
                config.CommandCloseOnEscape.Value ||
                config.CommandCloseOnCustom.Value != "")
            {
                On.RoR2.UI.PickupPickerPanel.Awake -= commandImprovements.hook_PickupPickerPanelAwake;
            }

            if (config.CommandTooltipsShow.Value ||
                config.CommandCountersShow.Value)
            {
                On.RoR2.UI.PickupPickerPanel.OnCreateButton -= commandImprovements.hook_OnCreateButton;
            }

            if (config.DPSMeterWindowShow.Value ||
                config.StatsDisplayStatString.Value.Contains("$dps"))
            {
                On.RoR2.GlobalEventManager.ClientDamageNotified -= DPSMeter.hook_ClientDamageNotified;
            }

            if (config.DPSMeterWindowShow.Value)
            {
                On.RoR2.UI.HUD.Awake -= DPSMeter.hook_Awake;
            }

            if (config.StatsDisplayEnable.Value)
            {
                RoR2.Run.onRunStartGlobal -= statsDisplay.hook_runStartGlobal;
                On.RoR2.UI.HUD.Awake -= statsDisplay.hook_Awake;
            }

            if (config.SortingSortItemsInventory.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged -= itemSorting.hook_OnInventoryChanged;
            }
            if (config.SortingSortItemsCommand.Value ||
                config.SortingSortItemsScrapper.Value)
            {
                On.RoR2.PickupPickerController.SubmitChoice -= commandImprovements.hook_SubmitChoice;
            }
            if (config.BuffTimers.Value || config.BuffTooltips.Value)
            {
                On.RoR2.UI.BuffIcon.Awake -= hook_BuffIconAwake;
                On.RoR2.UI.BuffIcon.UpdateIcon -= hook_BuffIconUpdateIcon;
                On.RoR2.UI.HUD.Awake -= hook_HUDAwake;
            }
        }
        
        private void hook_HUDAwake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);
            HUD = self;
        }
        private void hook_BuffIconAwake(On.RoR2.UI.BuffIcon.orig_Awake orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            if (self.transform.parent.name == "BuffDisplayRoot")
            {
                if (config.BuffTooltips.Value)
                {
                    UnityEngine.UI.GraphicRaycaster raycaster = self.transform.parent.GetComponent<UnityEngine.UI.GraphicRaycaster>();
                    if (raycaster == null)
                    {
                        self.transform.parent.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                    }
                    self.gameObject.AddComponent<RoR2.UI.TooltipProvider>();
                }
                if (config.BuffTimers.Value)
                {
                    GameObject TimerText = new GameObject("TimerText");
                    RectTransform timerRect = TimerText.AddComponent<RectTransform>();
                    RoR2.UI.HGTextMeshProUGUI timerTextMesh = TimerText.AddComponent<RoR2.UI.HGTextMeshProUGUI>();
                    TimerText.transform.SetParent(self.transform);

                    timerTextMesh.enableWordWrapping = false;
                    timerTextMesh.alignment = config.BuffTimersTextAlignmentOption;
                    timerTextMesh.fontSize = config.BuffTimersFontSize.Value;
                    timerTextMesh.faceColor = Color.white;
                    timerTextMesh.text = "";

                    timerRect.localPosition = Vector3.zero;
                    timerRect.anchorMin = new Vector2(1,0);
                    timerRect.anchorMax = new Vector2(1,0);
                    timerRect.localScale = Vector3.one;
                    timerRect.sizeDelta = new Vector2(48,48);
                    timerRect.anchoredPosition = new Vector2(-24,24);
                }
            }
        }


        private void hook_BuffIconUpdateIcon(On.RoR2.UI.BuffIcon.orig_UpdateIcon orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            BuffDef buffDef = BuffCatalog.GetBuffDef(self.buffIndex);
            if (buffDef != null && self.transform.parent.name == "BuffDisplayRoot")
            {
                if (config.BuffTooltips.Value)
                {
                    RoR2.UI.TooltipProvider tooltipProvider = self.GetComponent<RoR2.UI.TooltipProvider>();
                    tooltipProvider.overrideTitleText = buffDef.name;
                    tooltipProvider.titleColor = buffDef.buffColor;
                }
                if (config.BuffTimers.Value)
                {
                    Transform timerText = self.transform.Find("TimerText");
                    if (timerText != null)
                    {
                        if (HUD != null)
                        {
                            CharacterBody characterBody = HUD.targetBodyObject ? HUD.targetBodyObject.GetComponent<CharacterBody>() : null;
                            if (characterBody != null)
                            {
                                var ThisBuff = characterBody.timedBuffs.Where(b => b.buffIndex == self.buffIndex);
                                if (ThisBuff.Any())
                                {
                                    var buff = ThisBuff.OrderByDescending(b => b.timer).First();
                                    if (buff != null)
                                    {
                                        timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = buff.timer < 10 && config.BuffTimersDecimal.Value ? buff.timer.ToString("N1") : buff.timer.ToString("N0");
                                        return;
                                    }
                                }
                            }
                        }
                        timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = "";
                    }
                }
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

        private void hook_SetEquipment(On.RoR2.UI.GenericNotification.orig_SetEquipment orig, RoR2.UI.GenericNotification self, EquipmentDef equipmentDef)
        {
            orig(self, equipmentDef);

            self.descriptionText.token = equipmentDef.descriptionToken;
        }

        private void hook_SetItemIndex(On.RoR2.UI.ItemIcon.orig_SetItemIndex orig, RoR2.UI.ItemIcon self, ItemIndex itemIndex, int itemCount)
        {
            orig(self, itemIndex, itemCount);

            self.tooltipProvider.bodyToken = ItemCatalog.GetItemDef(itemIndex).descriptionToken;
        }

        private void hook_EquipmentIconUpdate(On.RoR2.UI.EquipmentIcon.orig_Update orig, RoR2.UI.EquipmentIcon self)
        {
            orig(self);
            if (self.currentDisplayData.hasEquipment && self.tooltipProvider)
            {
                EquipmentDef equipmentDef = self.currentDisplayData.equipmentDef;
                self.tooltipProvider.bodyToken = equipmentDef.descriptionToken;
            }
         }
    }
}
