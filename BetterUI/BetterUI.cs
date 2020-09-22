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
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "1.5.3")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class BetterUI : BaseUnityPlugin
    {
        internal ConfigManager config;
        internal ItemSorting itemSorting;
        internal StatsDisplay statsDisplay;
        internal CommandImprovements commandImprovements;
        internal DPSMeter DPSMeter;
        internal BuffTimers buffTimers;
        internal AdvancedIcons advancedIcons;
        internal bool ItemStatsModIntegration;
        internal RoR2.UI.HUD HUD;
        public void Awake()
        {
            BepInExPatcher.DoPatching();

            itemSorting = new ItemSorting(this);
            statsDisplay = new StatsDisplay(this);
            commandImprovements = new CommandImprovements(this);
            DPSMeter = new DPSMeter(this);
            advancedIcons = new AdvancedIcons(this);
            buffTimers = new BuffTimers(this);
        }

        public void Update()
        {
            commandImprovements.Update();
            DPSMeter.Update();
            statsDisplay.Update();
        }
        public void OnEnable()
        {
            config = new ConfigManager(this);

            ItemStatsModIntegration = config.AdvancedIconsItemItemStatsIntegration.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("dev.ontrigger.itemstats");

            Hooks.Hook(this);
        }

        public void OnDisable()
        {
            Hooks.Unhook(this);
        }

        internal void hook_HUDAwake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);
            HUD = self;
        }
        internal bool hook_ItemIsVisible(On.RoR2.UI.ItemInventoryDisplay.orig_ItemIsVisible orig, ItemIndex itemIndex)
        {
            return true;
        }
        internal void hook_SetArtifact(On.RoR2.UI.GenericNotification.orig_SetArtifact orig, RoR2.UI.GenericNotification self, ArtifactDef artifactDef)
        {
            if (config.MiscHidePickupNotificiationsArtifacts.Value)
            {
                BetterUI.Destroy(self.gameObject);
                return;
            }
            orig(self, artifactDef);

        }
        internal void hook_SetEquipment(On.RoR2.UI.GenericNotification.orig_SetEquipment orig, RoR2.UI.GenericNotification self, EquipmentDef equipmentDef)
        {
            if (config.MiscHidePickupNotificiationsEquipements.Value)
            {
                BetterUI.Destroy(self.gameObject);
                return;
            }
            orig(self, equipmentDef);

            self.descriptionText.token = equipmentDef.descriptionToken;
        }
        internal void hook_SetItem(On.RoR2.UI.GenericNotification.orig_SetItem orig, RoR2.UI.GenericNotification self, ItemDef itemDef)
        {
            if (config.MiscHidePickupNotificiationsItems.Value)
            {
                BetterUI.Destroy(self.gameObject);
                return;
            }
            orig(self, itemDef);

            self.descriptionText.token = itemDef.descriptionToken;
        }
    }
}
