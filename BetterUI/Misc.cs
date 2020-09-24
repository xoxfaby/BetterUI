using System;
using System.Collections.Generic;
using System.Text;

using RoR2;

namespace BetterUI
{
    class Misc : BetterUI.ModComponent
    {
        internal Misc(BetterUI mod) : base(mod) { }
        internal override void Hook()
        {
            if (mod.config.MiscHidePickupNotificiationsArtifacts.Value)
            {
                On.RoR2.UI.GenericNotification.SetArtifact += hook_SetArtifact;
            }
            if (mod.config.MiscAdvancedPickupNotificationsEquipements.Value ||
                mod.config.MiscHidePickupNotificiationsEquipements.Value)
            {
                On.RoR2.UI.GenericNotification.SetEquipment += hook_SetEquipment;
            }
            if (mod.config.MiscAdvancedPickupNotificationsItems.Value ||
                mod.config.MiscHidePickupNotificiationsItems.Value)
            {
                On.RoR2.UI.GenericNotification.SetItem += hook_SetItem;
            }

            if (mod.config.MiscShowHidden.Value)
            {
                On.RoR2.UI.ItemInventoryDisplay.ItemIsVisible += hook_ItemIsVisible;
            }
        }
        internal bool hook_ItemIsVisible(On.RoR2.UI.ItemInventoryDisplay.orig_ItemIsVisible orig, ItemIndex itemIndex)
        {
            return true;
        }
        internal void hook_SetArtifact(On.RoR2.UI.GenericNotification.orig_SetArtifact orig, RoR2.UI.GenericNotification self, ArtifactDef artifactDef)
        {
            if (mod.config.MiscHidePickupNotificiationsArtifacts.Value)
            {
                BetterUI.Destroy(self.gameObject);
                return;
            }
            orig(self, artifactDef);

        }
        internal void hook_SetEquipment(On.RoR2.UI.GenericNotification.orig_SetEquipment orig, RoR2.UI.GenericNotification self, EquipmentDef equipmentDef)
        {
            if (mod.config.MiscHidePickupNotificiationsEquipements.Value)
            {
                BetterUI.Destroy(self.gameObject);
                return;
            }
            orig(self, equipmentDef);

            self.descriptionText.token = equipmentDef.descriptionToken;
        }
        internal void hook_SetItem(On.RoR2.UI.GenericNotification.orig_SetItem orig, RoR2.UI.GenericNotification self, ItemDef itemDef)
        {
            if (mod.config.MiscHidePickupNotificiationsItems.Value)
            {
                BetterUI.Destroy(self.gameObject);
                return;
            }
            orig(self, itemDef);

            self.descriptionText.token = itemDef.descriptionToken;
        }
    }
}
