using System;
using System.Linq;

using RoR2;
using BepInEx;


namespace BetterUI
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "1.1.6")]


    public class BetterUI : BaseUnityPlugin
    {
        private ConfigManager config;
        private ItemSorting itemSorting;
        private StatsDisplay statsDisplay;

        public void Awake()
        {
            config = new ConfigManager(this);
            itemSorting = new ItemSorting(config);
            statsDisplay = new StatsDisplay(config);
        }
        public void OnEnable()
        {

            if (config.showHidden.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible += hook_ItemIsVisible;
            }

            if (config.advancedDescriptions.Value)
            {
                On.RoR2.UI.GenericNotification.SetItem += hook_SetItem;
                On.RoR2.UI.ItemIcon.SetItemIndex += hook_SetItemIndex;
                On.RoR2.UI.GenericNotification.SetEquipment += hook_SetEquipment;
                On.RoR2.UI.EquipmentIcon.Update += hook_EquipmentIconUpdate;
            }

            if (config.showStatsDisplay.Value)
            {
                On.RoR2.UI.HUD.OnEnable += statsDisplay.hook_OnEnable;
                On.RoR2.UI.HUD.OnDisable += statsDisplay.hook_OnDisable;
            }

            if (config.sortItemsInventory.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged += itemSorting.hook_OnInventoryChanged;
            }
            if (config.sortItemsCommand.Value || config.sortItemsScrapper.Value)
            {
                if(config.sortItemsCommand.Value && config.sortOrderCommand.Value.IndexOf('C') >= 0)
                {
                    On.RoR2.PickupPickerController.SubmitChoice += itemSorting.hook_SubmitChoice;
                }
                On.RoR2.UI.PickupPickerPanel.SetPickupOptions += itemSorting.hook_SetPickupOptions;
            }

        }

        private void OnDisable()
        {
            if (config.showHidden.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible -= hook_ItemIsVisible;
            }

            if (config.advancedDescriptions.Value)
            {
                On.RoR2.UI.GenericNotification.SetItem -= hook_SetItem;
                On.RoR2.UI.ItemIcon.SetItemIndex -= hook_SetItemIndex;
                On.RoR2.UI.GenericNotification.SetEquipment -= hook_SetEquipment;
                On.RoR2.UI.EquipmentIcon.Update -= hook_EquipmentIconUpdate;
            }
            if (config.showStatsDisplay.Value)
            {
                On.RoR2.UI.HUD.OnEnable -= statsDisplay.hook_OnEnable;
                On.RoR2.UI.HUD.OnDisable -= statsDisplay.hook_OnDisable;
            }
            if (config.sortItemsInventory.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged -= itemSorting.hook_OnInventoryChanged;
            }
            if (config.sortItemsCommand.Value || config.sortItemsScrapper.Value)
            {
                if (config.sortItemsCommand.Value && config.sortOrderCommand.Value.Contains('C'))
                {
                    On.RoR2.PickupPickerController.SubmitChoice -= itemSorting.hook_SubmitChoice;
                }
                On.RoR2.UI.PickupPickerPanel.SetPickupOptions -= itemSorting.hook_SetPickupOptions;
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
