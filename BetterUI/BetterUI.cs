using System;
using System.Linq;

using RoR2;
using BepInEx;


namespace BetterUI
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "1.0.1")]


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

            if (config.showHidden)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible += hook_ItemIsVisible;
            }

            if (config.AdvancedDescriptions)
            {
                On.RoR2.UI.GenericNotification.SetItem += hook_SetItem;
                On.RoR2.UI.ItemIcon.SetItemIndex += hook_SetItemIndex;
            }

            if (config.showStatsDisplay)
            {
                On.RoR2.UI.HUD.OnEnable += statsDisplay.hook_OnEnable;
                On.RoR2.UI.HUD.OnDisable += statsDisplay.hook_OnDisable;
            }

            if (config.sortItems)
            { 
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged += itemSorting.hook_OnInventoryChanged;
            }

        }

        private void OnDisable()
        {
            if (config.showHidden)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible -= hook_ItemIsVisible;
            }

            if (config.AdvancedDescriptions)
            {
                On.RoR2.UI.GenericNotification.SetItem -= hook_SetItem;
                On.RoR2.UI.ItemIcon.SetItemIndex -= hook_SetItemIndex;
            }

            if (config.showStatsDisplay)
            {
                On.RoR2.UI.HUD.OnEnable -= statsDisplay.hook_OnEnable;
                On.RoR2.UI.HUD.OnDisable -= statsDisplay.hook_OnDisable;
            }

            if (config.sortItems)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged -= itemSorting.hook_OnInventoryChanged;
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

        private void hook_SetItemIndex(On.RoR2.UI.ItemIcon.orig_SetItemIndex orig, RoR2.UI.ItemIcon self, ItemIndex itemIndex, int itemCount)
        {
            orig(self, itemIndex, itemCount);

            self.tooltipProvider.bodyToken = ItemCatalog.GetItemDef(itemIndex).descriptionToken;
        }
    }
}
