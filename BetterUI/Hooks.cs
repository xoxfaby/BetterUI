namespace BetterUI
{
    class Hooks
    {
        public static void Hook(BetterUI mod)
        {
            On.RoR2.UI.HUD.Awake += mod.hook_HUDAwake;

            if (mod.config.AdvancedIconsSkillShowProcCoefficient.Value ||
                mod.config.AdvancedIconsSkillCalculateSkillProcEffects.Value)
            {
                On.RoR2.UI.SkillIcon.Update += mod.advancedIcons.hook_SkillIcon_Update;
            }
            if (mod.config.AdvancedIconsItemAdvancedDescriptions.Value)
            {
                On.RoR2.UI.ItemIcon.SetItemIndex += mod.advancedIcons.hook_SetItemIndex;
            }
            if (mod.config.AdvancedIconsEquipementAdvancedDescriptions.Value ||
                mod.config.AdvancedIconsEquipementShowBaseCooldown.Value ||
                mod.config.AdvancedIconsEquipementShowCalculatedCooldown.Value)
            { 
                On.RoR2.UI.EquipmentIcon.Update += mod.advancedIcons.hook_EquipmentIcon_Update;
            }

            if (mod.config.MiscHidePickupNotificiationsArtifacts.Value)
            {
                On.RoR2.UI.GenericNotification.SetArtifact += mod.hook_SetArtifact;
            }
            if (mod.config.MiscAdvancedPickupNotificationsEquipements.Value ||
                mod.config.MiscHidePickupNotificiationsEquipements.Value)
            {
                On.RoR2.UI.GenericNotification.SetEquipment += mod.hook_SetEquipment;
            }
            if (mod.config.MiscAdvancedPickupNotificationsItems.Value || 
                mod.config.MiscHidePickupNotificiationsItems.Value)
            {
                On.RoR2.UI.GenericNotification.SetItem += mod.hook_SetItem;
            }

            if (mod.config.MiscShowHidden.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible += mod.hook_ItemIsVisible;
            }

            if (mod.config.CommandResizeCommandWindow.Value ||
                mod.config.SortingSortItemsScrapper.Value ||
                mod.config.SortingSortItemsScrapper.Value)
            {
                On.RoR2.UI.PickupPickerPanel.SetPickupOptions += mod.commandImprovements.hook_SetPickupOptions;
            }

            if (mod.config.CommandCloseOnEscape.Value ||
                mod.config.CommandCloseOnWASD.Value ||
                mod.config.CommandCloseOnCustom.Value != "")
            {
                On.RoR2.UI.PickupPickerPanel.Awake += mod.commandImprovements.hook_PickupPickerPanelAwake;
            }

            if (mod.config.CommandTooltipsShow.Value ||
                mod.config.CommandCountersShow.Value)
            {
                On.RoR2.UI.PickupPickerPanel.OnCreateButton += mod.commandImprovements.hook_OnCreateButton;
            }

            if (mod.config.DPSMeterWindowShow.Value ||
                mod.config.StatsDisplayStatString.Value.Contains("$dps"))
            {
                On.RoR2.GlobalEventManager.ClientDamageNotified += mod.DPSMeter.hook_ClientDamageNotified;
            }

            if (mod.config.DPSMeterWindowShow.Value)
            {
                On.RoR2.UI.HUD.Awake += mod.DPSMeter.hook_Awake;
            }

            if (mod.config.StatsDisplayEnable.Value)
            {
                RoR2.Run.onRunStartGlobal += mod.statsDisplay.hook_runStartGlobal;
                On.RoR2.UI.HUD.Awake += mod.statsDisplay.hook_Awake;
            }

            if (mod.config.SortingSortItemsInventory.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged += mod.itemSorting.hook_OnInventoryChanged;
            }
            if (mod.config.SortingSortItemsCommand.Value && mod.config.SortingSortOrderCommand.Value.Contains("C"))
            {
                On.RoR2.PickupPickerController.SubmitChoice += mod.commandImprovements.hook_SubmitChoice;
            }
            if (mod.config.BuffTimers.Value || mod.config.BuffTooltips.Value)
            {
                On.RoR2.UI.BuffIcon.Awake += mod.buffTimers.hook_BuffIconAwake;
                On.RoR2.UI.BuffIcon.UpdateIcon += mod.buffTimers.hook_BuffIconUpdateIcon;
                On.RoR2.UI.HUD.Awake += mod.hook_HUDAwake;
            }
        }
        public static void Unhook(BetterUI mod)
        {
            On.RoR2.UI.HUD.Awake -= mod.hook_HUDAwake;

            if (mod.config.AdvancedIconsSkillShowProcCoefficient.Value ||
                mod.config.AdvancedIconsSkillCalculateSkillProcEffects.Value)
            {
                On.RoR2.UI.SkillIcon.Update -= mod.advancedIcons.hook_SkillIcon_Update;
            }
            if (mod.config.AdvancedIconsItemAdvancedDescriptions.Value)
            {
                On.RoR2.UI.ItemIcon.SetItemIndex -= mod.advancedIcons.hook_SetItemIndex;
            }
            if (mod.config.AdvancedIconsEquipementAdvancedDescriptions.Value ||
                mod.config.AdvancedIconsEquipementShowBaseCooldown.Value ||
                mod.config.AdvancedIconsEquipementShowCalculatedCooldown.Value)
            {
                On.RoR2.UI.EquipmentIcon.Update -= mod.advancedIcons.hook_EquipmentIcon_Update;
            }

            if (mod.config.MiscHidePickupNotificiationsArtifacts.Value)
            {
                On.RoR2.UI.GenericNotification.SetArtifact -= mod.hook_SetArtifact;
            }
            if (mod.config.MiscAdvancedPickupNotificationsEquipements.Value ||
                mod.config.MiscHidePickupNotificiationsEquipements.Value)
            {
                On.RoR2.UI.GenericNotification.SetEquipment -= mod.hook_SetEquipment;
            }
            if (mod.config.MiscAdvancedPickupNotificationsItems.Value ||
                mod.config.MiscHidePickupNotificiationsItems.Value)
            {
                On.RoR2.UI.GenericNotification.SetItem -= mod.hook_SetItem;
            }

            if (mod.config.MiscShowHidden.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible -= mod.hook_ItemIsVisible;
            }

            if (mod.config.CommandResizeCommandWindow.Value ||
                mod.config.SortingSortItemsScrapper.Value ||
                mod.config.SortingSortItemsCommand.Value)
            {
                On.RoR2.UI.PickupPickerPanel.SetPickupOptions -= mod.commandImprovements.hook_SetPickupOptions;
            }

            if (mod.config.CommandCloseOnEscape.Value ||
                mod.config.CommandCloseOnWASD.Value ||
                mod.config.CommandCloseOnCustom.Value != "")
            {
                On.RoR2.UI.PickupPickerPanel.Awake -= mod.commandImprovements.hook_PickupPickerPanelAwake;
            }

            if (mod.config.CommandTooltipsShow.Value ||
                mod.config.CommandCountersShow.Value ||
                mod.config.SortingSortItemsScrapper.Value ||
                mod.config.SortingSortItemsCommand.Value)
            {
                On.RoR2.UI.PickupPickerPanel.OnCreateButton -= mod.commandImprovements.hook_OnCreateButton;
            }

            if (mod.config.DPSMeterWindowShow.Value ||
                mod.config.StatsDisplayStatString.Value.Contains("$dps"))
            {
                On.RoR2.GlobalEventManager.ClientDamageNotified -= mod.DPSMeter.hook_ClientDamageNotified;
            }

            if (mod.config.DPSMeterWindowShow.Value)
            {
                On.RoR2.UI.HUD.Awake -= mod.DPSMeter.hook_Awake;
            }

            if (mod.config.StatsDisplayEnable.Value)
            {
                RoR2.Run.onRunStartGlobal -= mod.statsDisplay.hook_runStartGlobal;
                On.RoR2.UI.HUD.Awake -= mod.statsDisplay.hook_Awake;
            }

            if (mod.config.SortingSortItemsInventory.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged -= mod.itemSorting.hook_OnInventoryChanged;
            }
            if (mod.config.SortingSortItemsCommand.Value && mod.config.SortingSortOrderCommand.Value.Contains("C"))
            {
                On.RoR2.PickupPickerController.SubmitChoice -= mod.commandImprovements.hook_SubmitChoice;
            }
            if (mod.config.BuffTimers.Value || mod.config.BuffTooltips.Value)
            {
                On.RoR2.UI.BuffIcon.Awake -= mod.buffTimers.hook_BuffIconAwake;
                On.RoR2.UI.BuffIcon.UpdateIcon -= mod.buffTimers.hook_BuffIconUpdateIcon;
            }
        }
    }
}
