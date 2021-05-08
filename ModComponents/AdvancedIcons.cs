using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RoR2.UI;
using RoR2.Skills;
using UnityEngine;

namespace BetterUI
{
    static class AdvancedIcons
    {
        static Dictionary<EquipmentIcon,bool> EquipmentIconDirty = new Dictionary<EquipmentIcon, bool>();
        static Dictionary<EquipmentIcon, EquipmentDef> lastEquipment = new Dictionary<EquipmentIcon, EquipmentDef>();

        static Dictionary<SkillIcon,bool> SkillIconDirty = new Dictionary<SkillIcon, bool>();
        static Dictionary<SkillIcon, SkillDef> lastSkill = new Dictionary<SkillIcon, SkillDef>();

        static List<ProcCoefficientCatalog.ProcCoefficientInfo> procCoefficientInfos;
        static Inventory inventory;
        static CharacterBody targetbody;

        static AdvancedIcons()
        {
            BetterUIPlugin.onHUDAwake += onHUDAwake;
        }
        internal static void Hook()
        {
            if (ConfigManager.AdvancedIconsSkillShowProcCoefficient.Value ||
                ConfigManager.AdvancedIconsSkillCalculateSkillProcEffects.Value ||
                ConfigManager.AdvancedIconsSkillShowBaseCooldown.Value ||
                ConfigManager.AdvancedIconsEquipementShowCalculatedCooldown.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.LoadoutPanelController.Row>("AddButton", (LoadoutPanelController_Row_AddButton_Delegate) LoadoutPanelController_Row_AddButton);
                BetterUIPlugin.Hooks.Add<RoR2.UI.SkillIcon>("Update", SkillIcon_Update);
                BetterUIPlugin.Hooks.Add<RoR2.CharacterMaster>("OnInventoryChanged", CharacterMaster_OnInventoryChanged);
            }
            if (ConfigManager.AdvancedIconsItemAdvancedDescriptions.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.ItemIcon, ItemIndex, int>("SetItemIndex", ItemIcon_SetItemIndex);
            }
            if (ConfigManager.AdvancedIconsEquipementAdvancedDescriptions.Value ||
                ConfigManager.AdvancedIconsEquipementShowBaseCooldown.Value ||
                ConfigManager.AdvancedIconsEquipementShowCalculatedCooldown.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.EquipmentIcon>("Update", EquipmentIcon_Update);
            }
        }

        private static void onHUDAwake(HUD self)
        {
            EquipmentIconDirty.Clear();
            lastEquipment.Clear();
            SkillIconDirty.Clear();
            lastSkill.Clear();
        }

        private static void ItemIcon_SetItemIndex(Action<RoR2.UI.ItemIcon, ItemIndex, int> orig, RoR2.UI.ItemIcon self, ItemIndex itemIndex, int itemCount)
        {
            orig(self, itemIndex, itemCount);

            self.tooltipProvider.bodyToken = ItemCatalog.GetItemDef(itemIndex).descriptionToken;
        }


        private static void CharacterMaster_OnInventoryChanged(Action<RoR2.CharacterMaster> orig, CharacterMaster self)
        {
            orig(self);

            if (self.inventory)
            {
                foreach (EquipmentIcon equipment in new List<EquipmentIcon>(EquipmentIconDirty.Keys))
                {
                    if (equipment && equipment.targetInventory == self.inventory)
                    {
                        EquipmentIconDirty[equipment] = true;
                    }
                }
                foreach (SkillIcon skill in new List<SkillIcon>(SkillIconDirty.Keys))
                {
                    if (skill && skill.playerCharacterMasterController && skill.playerCharacterMasterController.master == self)
                    {
                        SkillIconDirty[skill] = true;
                    }
                }
            }
        }

        internal delegate void LoadoutPanelController_Row_AddButton_Delegate(Action<RoR2.UI.LoadoutPanelController.Row, LoadoutPanelController, Sprite, string, string, Color, UnityEngine.Events.UnityAction, string, ViewablesCatalog.Node, bool> orig,
            RoR2.UI.LoadoutPanelController.Row self, LoadoutPanelController owner, Sprite icon, string titleToken, string bodyToken, Color tooltipColor, UnityEngine.Events.UnityAction callback, string unlockableName, ViewablesCatalog.Node viewableNode, bool isWIP = false);
        internal static void LoadoutPanelController_Row_AddButton(Action<RoR2.UI.LoadoutPanelController.Row, LoadoutPanelController, Sprite, string, string, Color, UnityEngine.Events.UnityAction, string, ViewablesCatalog.Node, bool> orig,
            RoR2.UI.LoadoutPanelController.Row self, LoadoutPanelController owner, Sprite icon, string titleToken, string bodyToken, Color tooltipColor, UnityEngine.Events.UnityAction callback, string unlockableName, ViewablesCatalog.Node viewableNode, bool isWIP = false)
        {
            orig(self, owner, icon, titleToken, bodyToken, tooltipColor, callback, unlockableName, viewableNode, isWIP);

            LoadoutPanelController.Row selfRow = (LoadoutPanelController.Row) self;
            UserProfile userProfile = selfRow.userProfile;
            if (ConfigManager.AdvancedIconsSkillShowProcCoefficient.Value || ConfigManager.AdvancedIconsSkillShowBaseCooldown.Value)
            {
                if (userProfile != null && userProfile.HasUnlockable(unlockableName))
                {
                    BetterUIPlugin.sharedStringBuilder.Clear();
                    BetterUIPlugin.sharedStringBuilder.Append(Language.GetString(bodyToken));
                    if (ConfigManager.AdvancedIconsSkillShowBaseCooldown.Value)
                    {
                        var skillDef = RoR2.Skills.SkillCatalog.GetSkillDef(Utils.TheREALFindSkillIndexByName(titleToken));
                        if (skillDef)
                        {
                            BetterUIPlugin.sharedStringBuilder.Append("\n\nCooldown: <style=cIsDamage>");
                            BetterUIPlugin.sharedStringBuilder.Append(skillDef.baseRechargeInterval);
                            BetterUIPlugin.sharedStringBuilder.Append("</style> second");
                            if(skillDef.baseRechargeInterval != 1) BetterUIPlugin.sharedStringBuilder.Append("s");
                        }
                    }

                    if (ConfigManager.AdvancedIconsSkillShowProcCoefficient.Value)
                    {
                        List<ProcCoefficientCatalog.ProcCoefficientInfo> procCoefficientInfos = ProcCoefficientCatalog.GetProcCoefficientInfo(titleToken);

                        if (procCoefficientInfos != null)
                        {
                            foreach (var info in procCoefficientInfos)
                            {
                                BetterUIPlugin.sharedStringBuilder.Append("\n\n<size=110%>");
                                BetterUIPlugin.sharedStringBuilder.Append(info.name);
                                BetterUIPlugin.sharedStringBuilder.Append(":</size>");
                                if (ConfigManager.AdvancedIconsSkillShowProcCoefficient.Value)
                                {
                                    BetterUIPlugin.sharedStringBuilder.Append("\n <style=cIsUtility>Proc Coefficient: ");
                                    BetterUIPlugin.sharedStringBuilder.Append(info.procCoefficient);
                                    BetterUIPlugin.sharedStringBuilder.Append("</style>");
                                }
                            }

                            
                        }
                    }
                    TooltipProvider tooltipProvider = selfRow.buttons[selfRow.buttons.Count - 1].GetComponent<TooltipProvider>();
                    if (tooltipProvider != null)
                    {
                        tooltipProvider.overrideBodyText = BetterUIPlugin.sharedStringBuilder.ToString();
                    }
                }
            }
        }
        internal static void SkillIcon_Update(Action<RoR2.UI.SkillIcon> orig, SkillIcon self)
        {
            orig(self);

            if (!SkillIconDirty.ContainsKey(self))
            {
                SkillIconDirty.Add(self, true);
                lastSkill.Add(self, null);
            }
            if (self.targetSkill && (self.targetSkill.skillDef != lastSkill[self] || SkillIconDirty[self]))
            {
                lastSkill[self] = self.targetSkill.skillDef ;
                SkillIconDirty[self] = false;
                BetterUIPlugin.sharedStringBuilder.Clear();
                BetterUIPlugin.sharedStringBuilder.Append(Language.GetString(self.targetSkill.skillDescriptionToken));
                if (ConfigManager.AdvancedIconsSkillShowBaseCooldown.Value || ConfigManager.AdvancedIconsSkillShowCalculatedCooldown.Value)
                {
                    BetterUIPlugin.sharedStringBuilder.Append("\n");
                }
                if (ConfigManager.AdvancedIconsSkillShowBaseCooldown.Value)
                {
                    BetterUIPlugin.sharedStringBuilder.Append("\nBase Cooldown: <style=cIsDamage>");
                    BetterUIPlugin.sharedStringBuilder.Append(self.targetSkill.baseRechargeInterval);
                    BetterUIPlugin.sharedStringBuilder.Append("</style> second");
                    if (self.targetSkill.baseRechargeInterval != 1) BetterUIPlugin.sharedStringBuilder.Append("s");
                }
                if (ConfigManager.AdvancedIconsSkillShowCalculatedCooldown.Value && self.targetSkill.baseRechargeInterval > self.targetSkill.finalRechargeInterval)
                {
                    BetterUIPlugin.sharedStringBuilder.Append("\nEffective Cooldown: <style=cIsHealing>");
                    BetterUIPlugin.sharedStringBuilder.Append(self.targetSkill.finalRechargeInterval);
                    BetterUIPlugin.sharedStringBuilder.Append("</style> second");
                    if (self.targetSkill.baseRechargeInterval != 1) BetterUIPlugin.sharedStringBuilder.Append("s");
                }

                if (ConfigManager.AdvancedIconsSkillShowProcCoefficient.Value || ConfigManager.AdvancedIconsSkillCalculateSkillProcEffects.Value)
                {
                    procCoefficientInfos = ProcCoefficientCatalog.GetProcCoefficientInfo(self.targetSkill.skillDef.skillNameToken);

                    if (procCoefficientInfos != null)
                    {
                        foreach (var info in procCoefficientInfos)
                        {
                            BetterUIPlugin.sharedStringBuilder.Append("\n\n<size=110%>");
                            BetterUIPlugin.sharedStringBuilder.Append(info.name);
                            BetterUIPlugin.sharedStringBuilder.Append("</size>");
                            if (ConfigManager.AdvancedIconsSkillShowProcCoefficient.Value)
                            {
                                BetterUIPlugin.sharedStringBuilder.Append("\n <style=cIsUtility>Proc Coefficient: ");
                                BetterUIPlugin.sharedStringBuilder.Append(info.procCoefficient);
                                BetterUIPlugin.sharedStringBuilder.Append("</style>");
                            }
                            if (info.procCoefficient > 0 && ConfigManager.AdvancedIconsSkillCalculateSkillProcEffects.Value)
                            {
                                foreach (var item in ProcItemsCatalog.items)
                                {
                                    int stacks = self.targetSkill.characterBody.inventory.GetItemCount(item.Key);
                                    if (stacks > 0)
                                    {
                                        BetterUIPlugin.sharedStringBuilder.Append("\n  ");
                                        BetterUIPlugin.sharedStringBuilder.Append(Language.GetString(item.Key.nameToken));
                                        BetterUIPlugin.sharedStringBuilder.Append(": ");
                                        BetterUIPlugin.sharedStringBuilder.Append(item.Value.GetOutputString(stacks, self.targetSkill.characterBody.master.luck, info.procCoefficient));
                                    }
                                }
                            }
                        }
                    }
                }

                self.tooltipProvider.overrideBodyText = BetterUIPlugin.sharedStringBuilder.ToString();
            }

            if (ConfigManager.AdvancedIconsSkillShowCooldownStacks.Value && self.targetSkill && self.targetSkill.cooldownRemaining > 0)
            {
                BetterUIPlugin.sharedStringBuilder.Clear();
                BetterUIPlugin.sharedStringBuilder.AppendInt(Mathf.CeilToInt(self.targetSkill.cooldownRemaining), 0U, uint.MaxValue);
                self.cooldownText.SetText(BetterUIPlugin.sharedStringBuilder);
                self.cooldownText.gameObject.SetActive(true);
            }
        }

        internal static void EquipmentIcon_Update(Action<RoR2.UI.EquipmentIcon> orig, EquipmentIcon self)
        {
            orig(self);

            if (!EquipmentIconDirty.ContainsKey(self))
            {
                EquipmentIconDirty.Add(self,true);
                lastEquipment.Add(self,null);
            }
            if ((ConfigManager.AdvancedIconsEquipementAdvancedDescriptions.Value || 
                ConfigManager.AdvancedIconsEquipementShowBaseCooldown.Value || 
                ConfigManager.AdvancedIconsEquipementShowCalculatedCooldown.Value) && 
                (self.currentDisplayData.equipmentDef != lastEquipment[self] || EquipmentIconDirty[self]) &&
                self.currentDisplayData.hasEquipment && self.tooltipProvider)
            {
                lastEquipment[self] = self.currentDisplayData.equipmentDef;
                EquipmentIconDirty[self] = false;
                BetterUIPlugin.sharedStringBuilder.Clear();
                BetterUIPlugin.sharedStringBuilder.Append(Language.GetString(ConfigManager.AdvancedIconsEquipementAdvancedDescriptions.Value ? self.currentDisplayData.equipmentDef.descriptionToken : self.currentDisplayData.equipmentDef.pickupToken));
                if(ConfigManager.AdvancedIconsEquipementShowBaseCooldown.Value || ConfigManager.AdvancedIconsEquipementShowCalculatedCooldown.Value)
                {
                    BetterUIPlugin.sharedStringBuilder.Append("\n");
                }
                if (ConfigManager.AdvancedIconsEquipementShowBaseCooldown.Value)
                {
                    BetterUIPlugin.sharedStringBuilder.Append("\nBase Cooldown: <style=cIsDamage>");
                    BetterUIPlugin.sharedStringBuilder.Append(self.currentDisplayData.equipmentDef.cooldown);
                    BetterUIPlugin.sharedStringBuilder.Append("</style> second");
                    if (self.currentDisplayData.equipmentDef.cooldown != 1) BetterUIPlugin.sharedStringBuilder.Append("s");
                }
                if (ConfigManager.AdvancedIconsEquipementShowCalculatedCooldown.Value)
                {
                    inventory = self.targetInventory;
                    if (!inventory && BetterUIPlugin.HUD.targetBodyObject)
                    {
                        targetbody = BetterUIPlugin.HUD.targetBodyObject.GetComponent<CharacterBody>();
                        if (targetbody)
                        {
                            inventory = targetbody.inventory;
                        }
                    }
                    if (inventory)
                    {
                        float reduction = (float)Math.Pow(0.85, inventory.GetItemCount(RoR2.RoR2Content.Items.EquipmentMagazine));
                        if (inventory.GetItemCount(RoR2.RoR2Content.Items.AutoCastEquipment.itemIndex) > 0)
                        {
                            reduction *= 0.5f * (float)Math.Pow(0.85, inventory.GetItemCount(RoR2.RoR2Content.Items.AutoCastEquipment.itemIndex) - 1);
                        }
                        if (reduction < 1)
                        {
                            BetterUIPlugin.sharedStringBuilder.Append("\nEffective Cooldown: <style=cIsHealing>");
                            BetterUIPlugin.sharedStringBuilder.Append((self.currentDisplayData.equipmentDef.cooldown * reduction).ToString("0.###"));
                            BetterUIPlugin.sharedStringBuilder.Append("</style> second");
                            if (self.currentDisplayData.equipmentDef.cooldown != 1) BetterUIPlugin.sharedStringBuilder.Append("s");
                        }
                    }
                }
                

                self.tooltipProvider.overrideBodyText = BetterUIPlugin.sharedStringBuilder.ToString();
            }

            if (ConfigManager.AdvancedIconsEquipementShowCooldownStacks.Value && self.cooldownText && self.currentDisplayData.cooldownValue > 0)
            {
                self.cooldownText.gameObject.SetActive(true);
            }
        }
    }
}
