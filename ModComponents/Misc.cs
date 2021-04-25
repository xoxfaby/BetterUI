using System;
using System.Collections.Generic;
using System.Text;

using RoR2;

namespace BetterUI
{
    static class Misc
    {
        internal static void Hook()
        {
            if (BetterUIPlugin.instance.config.MiscHidePickupNotificiationsArtifacts.Value)
            {
                HookManager.Add<RoR2.UI.GenericNotification, ArtifactDef>( "SetArtifact", GenericNotification_SetArtifact );
            }
            if (BetterUIPlugin.instance.config.MiscAdvancedPickupNotificationsEquipements.Value ||
                BetterUIPlugin.instance.config.MiscHidePickupNotificiationsEquipements.Value)
            {
                HookManager.Add<RoR2.UI.GenericNotification, EquipmentDef>( "SetEquipment", GenericNotification_SetEquipment );
            }
            if (BetterUIPlugin.instance.config.MiscAdvancedPickupNotificationsItems.Value ||
                BetterUIPlugin.instance.config.MiscHidePickupNotificiationsItems.Value)
            {
                HookManager.Add<RoR2.UI.GenericNotification, ItemDef>( "SetItem", GenericNotification_SetItem );
            }

            if (BetterUIPlugin.instance.config.MiscShowHidden.Value)
            {
                HookManager.Add<RoR2.UI.ItemInventoryDisplay>( "ItemIsVisible", (ItemInventoryDisplay_ItemIsVisible_Delegate) ItemInventoryDisplay_ItemIsVisible);
            }
            if (BetterUIPlugin.instance.config.MiscShowPickupDescription.Value)
            {
                HookManager.Add<RoR2.UI.ContextManager>( "Awake", ContextManager_Awake );
                HookManager.Add<RoR2.GenericPickupController>( "GetContextString", (GenericPickupController_GetContextString_Delegate) GenericPickupController_GetContextString );
            }
        }

        internal static void ContextManager_Awake(Action<RoR2.UI.ContextManager> orig, RoR2.UI.ContextManager self)
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
        internal delegate string GenericPickupController_GetContextString_Delegate(Func<RoR2.GenericPickupController, Interactor, string> orig, GenericPickupController self, Interactor activator);
        internal static string GenericPickupController_GetContextString(Func<RoR2.GenericPickupController, Interactor, string> orig, GenericPickupController self, Interactor activator)
        {
            PickupDef pickupDef = PickupCatalog.GetPickupDef(self.pickupIndex);
            string pickupText = string.Format(Language.GetString(((pickupDef != null) ? pickupDef.interactContextToken : null) ?? string.Empty), Language.GetString(pickupDef.nameToken));
            if (pickupDef.itemIndex != ItemIndex.None)
            {
                ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
                pickupText += $"\n\n{Language.GetString( BetterUIPlugin.instance.config.MiscPickupDescriptionAdvanced.Value ? itemDef.descriptionToken : itemDef.pickupToken)}";
            }
            else if (pickupDef.equipmentIndex != EquipmentIndex.None)
            {
                EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(pickupDef.equipmentIndex);
                pickupText += $"\n\n{Language.GetString(BetterUIPlugin.instance.config.MiscPickupDescriptionAdvanced.Value ? equipmentDef.descriptionToken : equipmentDef.pickupToken)}";
            }
            return pickupText;
        }
        internal delegate bool ItemInventoryDisplay_ItemIsVisible_Delegate(Func<ItemIndex, bool> orig, ItemIndex itemIndex);
        internal static bool ItemInventoryDisplay_ItemIsVisible(Func<ItemIndex, bool> orig, ItemIndex itemIndex)
        {
            return true;
        }
        internal static void GenericNotification_SetArtifact(Action<RoR2.UI.GenericNotification, ArtifactDef> orig, RoR2.UI.GenericNotification self, ArtifactDef artifactDef)
        {
            if (BetterUIPlugin.instance.config.MiscHidePickupNotificiationsArtifacts.Value)
            {
                UnityEngine.Object.Destroy(self.gameObject);
                return;
            }
            orig(self, artifactDef);

        }
        internal static void GenericNotification_SetEquipment(Action<RoR2.UI.GenericNotification, EquipmentDef> orig, RoR2.UI.GenericNotification self, EquipmentDef equipmentDef)
        {
            if (BetterUIPlugin.instance.config.MiscHidePickupNotificiationsEquipements.Value)
            {
                UnityEngine.Object.Destroy(self.gameObject);
                return;
            }
            orig(self, equipmentDef);

            self.descriptionText.token = equipmentDef.descriptionToken;
        }
        internal static void GenericNotification_SetItem(Action<RoR2.UI.GenericNotification, ItemDef> orig, RoR2.UI.GenericNotification self, ItemDef itemDef)
        {
            if (BetterUIPlugin.instance.config.MiscHidePickupNotificiationsItems.Value)
            {
                UnityEngine.Object.Destroy(self.gameObject);
                return;
            }
            orig(self, itemDef);

            self.descriptionText.token = itemDef.descriptionToken;
        }
    }
}
