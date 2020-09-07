using System;
using System.Linq;

using RoR2;
using BepInEx;

using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace BetterUI
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "1.0.1")]


    public class BetterUI : BaseUnityPlugin
    {
        private ConfigManager config;

        public void Awake()
        {
            config = new ConfigManager(this);
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

            if (config.sortItems)
            { 
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged += hook_OnInventoryChanged;
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

            if (config.sortItems)
            {
                On.RoR2.UI.ItemInventoryDisplay.OnInventoryChanged -= hook_OnInventoryChanged;
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
        private void hook_OnInventoryChanged(On.RoR2.UI.ItemInventoryDisplay.orig_OnInventoryChanged orig, RoR2.UI.ItemInventoryDisplay self)
        {

            orig(self);


            if (self.inventory && self.inventory.itemAcquisitionOrder.Any())
            {
                IOrderedEnumerable<ItemIndex> finalOrder = self.inventory.itemAcquisitionOrder.OrderBy(a => 1);
                foreach (char c in config.sortOrder.ToCharArray())
                {
                    switch (c)
                    {
                        case '0': // Tier Ascending
                            finalOrder = finalOrder.ThenBy(item => config.tierOrder[(int)ItemCatalog.GetItemDef(item).tier]);
                            break;
                        case '1': // Tier Descending
                            finalOrder = finalOrder.ThenByDescending(item => config.tierOrder[(int)ItemCatalog.GetItemDef(item).tier]);
                            break;
                        case '2': // Stack Size Ascending
                            finalOrder = finalOrder.ThenBy(item => self.inventory.itemStacks[(int)item]);
                            break;
                        case '3': // Stack Size Descending
                            finalOrder = finalOrder.ThenByDescending(item => self.inventory.itemStacks[(int)item]);
                            break;
                        case '4': // Pickup Order
                            finalOrder = finalOrder.ThenBy(item => self.inventory.itemAcquisitionOrder.IndexOf(item));
                            break;
                        case '5': // Pickup Order Reversed
                            finalOrder = finalOrder.ThenByDescending(item => self.inventory.itemAcquisitionOrder.IndexOf(item));
                            break;
                        case '6': // Alphabetical
                            finalOrder = finalOrder.ThenBy(item => Language.GetString(ItemCatalog.GetItemDef(item).nameToken));
                            break;
                        case '7': // Alphabetical Reversed
                            finalOrder = finalOrder.ThenByDescending(item => Language.GetString(ItemCatalog.GetItemDef(item).nameToken));
                            break;
                        case '8': // Random"
                            Random random = new Random();
                            finalOrder = finalOrder.ThenBy(item => random.Next());
                            break;
                        case 's': // Scrap First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Scrap));
                            break;
                        case 'S': // Scrap Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.Scrap));
                            break;
                        case 'd': // Damage First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Damage));
                            break;
                        case 'D': // Damage Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.Damage));
                            break;
                        case 'h': // Healing First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Healing));
                            break;
                        case 'H': // Healing Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.Healing));
                            break;
                        case 'u': // Utility First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.Utility));
                            break;
                        case 'U': // Utility Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.Utility));
                            break;
                        case 'o': // OnKillEffect First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.OnKillEffect));
                            break;
                        case 'O': // OnKillEffect Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.OnKillEffect));
                            break;
                        case 'e': // EquipmentRelated First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.EquipmentRelated));
                            break;
                        case 'E': // EquipmentRelated Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.EquipmentRelated));
                            break;
                        case 'p': // SprintRelated First
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).ContainsTag(ItemTag.SprintRelated));
                            break;
                        case 'P': // SprintRelated Last
                            finalOrder = finalOrder.ThenByDescending(item => ItemCatalog.GetItemDef(item).DoesNotContainTag(ItemTag.SprintRelated));
                            break;
                    }
                }

                if (self.itemOrder != null)
                {
                    finalOrder.ToList().CopyTo(self.itemOrder);
                }
            }
        }
    }
}
