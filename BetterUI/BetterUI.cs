using System;
using System.Linq;

using RoR2;
using R2API.Utils;
using BepInEx;


namespace BetterUI
{
    [BepInDependency("dev.ontrigger.itemstats", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "1.3.2")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class BetterUI : BaseUnityPlugin
    {
        internal ConfigManager config;
        internal ItemSorting itemSorting;
        internal StatsDisplay statsDisplay;
        internal CommandImprovements commandImprovements;
        internal DPSMeter DPSMeter;
        internal bool ItemStatsModIntegration; 
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
                On.RoR2.GlobalEventManager.ClientDamageNotified += DPSMeter.hook_HandleDamageDealt;
            }

            if (config.DPSMeterWindowShow.Value)
            {
                On.RoR2.UI.HUD.Awake += DPSMeter.hook_Awake;
            }

            if (config.StatsDisplayEnable.Value)
            {
                RoR2.Run.onRunStartGlobal += statsDisplay.hook_runStartGlobal;
                On.RoR2.UI.HUD.OnEnable += statsDisplay.hook_Awake;
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
                On.RoR2.GlobalEventManager.ClientDamageNotified -= DPSMeter.hook_HandleDamageDealt;
            }

            if (config.DPSMeterWindowShow.Value)
            {
                On.RoR2.UI.HUD.Awake -= DPSMeter.hook_Awake;
            }

            if (config.StatsDisplayEnable.Value)
            {
                RoR2.Run.onRunStartGlobal -= statsDisplay.hook_runStartGlobal;
                On.RoR2.UI.HUD.OnEnable -= statsDisplay.hook_Awake;
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

        //private void hook_SetDisplayData(On.RoR2.UI.EquipmentIcon.orig_SetDisplayData orig, RoR2.UI.EquipmentIcon self, ValueType newDisplayData)
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
