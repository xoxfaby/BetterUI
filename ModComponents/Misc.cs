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
            if (mod.config.MiscShowPickupDescription.Value)
            {
                On.RoR2.UI.ContextManager.Awake += hook_ContextManager_Awake;
                On.RoR2.GenericPickupController.GetContextString += hook_GenericPickupController_GetContextString;
            }
        }

        internal void hook_ContextManager_Awake(On.RoR2.UI.ContextManager.orig_Awake orig, RoR2.UI.ContextManager self)
        {
            var Description = self.gameObject.transform.Find("ContextDisplay/Description");
            var textMesh = Description.gameObject.GetComponent<RoR2.UI.HGTextMeshProUGUI>();
            var rectTransform = Description.gameObject.GetComponent<UnityEngine.RectTransform>();
            rectTransform.sizeDelta = new UnityEngine.Vector2( rectTransform.sizeDelta.x ,-0.7f);
            rectTransform.anchoredPosition = new UnityEngine.Vector2( rectTransform.anchoredPosition.x ,-1);
            textMesh.fontSizeMin = textMesh.fontSize;
            textMesh.alignment = TMPro.TextAlignmentOptions.TopLeft;
            orig(self);
        }
        internal string hook_GenericPickupController_GetContextString(On.RoR2.GenericPickupController.orig_GetContextString orig, GenericPickupController self, Interactor activator)
        {
            PickupDef pickupDef = PickupCatalog.GetPickupDef(self.pickupIndex);
            string pickupText = string.Format(Language.GetString(((pickupDef != null) ? pickupDef.interactContextToken : null) ?? string.Empty), Language.GetString(pickupDef.nameToken));
            if (pickupDef.itemIndex != ItemIndex.None)
            {
                ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
                pickupText += $"\n\n{Language.GetString( mod.config.MiscPickupDescriptionAdvanced.Value ? itemDef.descriptionToken : itemDef.pickupToken)}";
            }
            else if (pickupDef.equipmentIndex != EquipmentIndex.None)
            {
                EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(pickupDef.equipmentIndex);
                pickupText += $"\n\n{Language.GetString(mod.config.MiscPickupDescriptionAdvanced.Value ? equipmentDef.descriptionToken : equipmentDef.pickupToken)}";
            }
            return pickupText;
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
