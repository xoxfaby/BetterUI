﻿using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RoR2.UI;
using UnityEngine;

namespace BetterUI
{
    class AdvancedIcons
    {
        readonly BetterUI mod;

        Dictionary<string, float> skillCooldowns = new Dictionary<string, float>();
        internal AdvancedIcons(BetterUI mod)
        {
            this.mod = mod;
        }
        internal void hook_SetItemIndex(On.RoR2.UI.ItemIcon.orig_SetItemIndex orig, RoR2.UI.ItemIcon self, ItemIndex itemIndex, int itemCount)
        {
            orig(self, itemIndex, itemCount);

            self.tooltipProvider.bodyToken = ItemCatalog.GetItemDef(itemIndex).descriptionToken;
        }

        internal void Start()
        {
            foreach(var skill in RoR2.Skills.SkillCatalog.allSkillDefs)
            {
                if(skill.baseRechargeInterval>0 && skill.requiredStock > 0)
                {
                    skillCooldowns[skill.skillNameToken] = skill.baseRechargeInterval;
                }
            }
        }

        internal void hook_LoadoutPanelController_Row_AddButton(On.RoR2.UI.LoadoutPanelController.Row.orig_AddButton orig, object self, LoadoutPanelController owner, Sprite icon, string titleToken, string bodyToken, Color tooltipColor, UnityEngine.Events.UnityAction callback, string unlockableName, ViewablesCatalog.Node viewableNode, bool isWIP = false)
        {
            orig(self, owner, icon, titleToken, bodyToken, tooltipColor, callback, unlockableName, viewableNode, isWIP);

            LoadoutPanelController.Row selfRow = (LoadoutPanelController.Row) self;
            UserProfile userProfile = selfRow.userProfile;
            if (mod.config.AdvancedIconsSkillShowProcCoefficient.Value || mod.config.AdvancedIconsSkillShowBaseCooldown.Value)
            {
                if (userProfile != null && userProfile.HasUnlockable(unlockableName))
                {
                    string tooltipBody = Language.GetString(bodyToken);
                    if (mod.config.AdvancedIconsSkillShowBaseCooldown.Value && skillCooldowns.ContainsKey(titleToken))
                    {
                        tooltipBody += $"\n\nCooldown: <style=cIsDamage>{skillCooldowns[titleToken]}</style> seconds";
                    }

                    if (mod.config.AdvancedIconsSkillShowProcCoefficient.Value)
                    {
                        List<ProcCoefficientCatalog.ProcCoefficientInfo> procCoefficientInfos = ProcCoefficientCatalog.GetProcCoefficientInfo(titleToken);

                        if (procCoefficientInfos != null)
                        {
                            foreach (var info in procCoefficientInfos)
                            {
                                tooltipBody += $"\n\n<size=110%>{info.name}:</size>";
                                if (mod.config.AdvancedIconsSkillShowProcCoefficient.Value)
                                {
                                    tooltipBody += $"\n <style=cIsUtility>Proc Coefficient: {info.procCoefficient}</style>";
                                }
                            }

                            
                        }
                    }
                    TooltipProvider tooltipProvider = selfRow.buttons[selfRow.buttons.Count - 1].GetComponent<TooltipProvider>();
                    if (tooltipProvider != null)
                    {
                        tooltipProvider.overrideBodyText = tooltipBody;
                    }
                }
            }
        }
        internal void hook_SkillIcon_Update(On.RoR2.UI.SkillIcon.orig_Update orig, SkillIcon self)
        {
            orig(self);

            if (self.targetSkill != null)
            {
                string tooltipBody = Language.GetString(self.targetSkill.skillDescriptionToken);
                if (mod.config.AdvancedIconsSkillShowBaseCooldown.Value || mod.config.AdvancedIconsSkillShowCalculatedCooldown.Value)
                {
                    tooltipBody += "\n";
                }
                if (mod.config.AdvancedIconsSkillShowBaseCooldown.Value)
                {
                    tooltipBody += $"\nBase Cooldown: <style=cIsDamage>{self.targetSkill.baseRechargeInterval}</style> seconds";
                }
                if (mod.config.AdvancedIconsSkillShowCalculatedCooldown.Value && self.targetSkill.baseRechargeInterval > self.targetSkill.finalRechargeInterval)
                {
                    tooltipBody += $"\nEffective Cooldown: <style=cIsHealing>{self.targetSkill.finalRechargeInterval}</style> seconds";
                }

                if (mod.config.AdvancedIconsSkillShowProcCoefficient.Value || mod.config.AdvancedIconsSkillCalculateSkillProcEffects.Value)
                {
                    List<ProcCoefficientCatalog.ProcCoefficientInfo> procCoefficientInfos = ProcCoefficientCatalog.GetProcCoefficientInfo(self.targetSkill.skillDef.skillNameToken);

                    if (procCoefficientInfos != null)
                    {
                        foreach (var info in procCoefficientInfos)
                        {
                            tooltipBody += $"\n\n<size=110%>{info.name}:</size>";
                            if (mod.config.AdvancedIconsSkillShowProcCoefficient.Value)
                            {
                                tooltipBody += $"\n <style=cIsUtility>Proc Coefficient: {info.procCoefficient}</style>";
                            }
                            if (info.procCoefficient > 0 && mod.config.AdvancedIconsSkillCalculateSkillProcEffects.Value)
                            {
                                foreach (var item in ProcItemsCatalog.GetAllItems())
                                {
                                    int stacks = self.targetSkill.characterBody.inventory.itemStacks[(int)item.Key];
                                    if (stacks > 0)
                                    {
                                        ItemDef itemDef = ItemCatalog.GetItemDef(item.Key);
                                        tooltipBody += "\n  " + Language.GetString(itemDef.nameToken) + ": ";
                                        float luck = self.targetSkill.characterBody.master.luck;
                                        tooltipBody += item.Value.GetOutputString(stacks, luck, info.procCoefficient);
                                    }
                                }
                            }
                        }
                    }
                }

                self.tooltipProvider.overrideBodyText = tooltipBody;
            }

            if (mod.config.AdvancedIconsSkillShowCooldownStacks.Value && self.targetSkill && self.targetSkill.cooldownRemaining > 0)
            {
                SkillIcon.sharedStringBuilder.Clear();
                SkillIcon.sharedStringBuilder.AppendInt(Mathf.CeilToInt(self.targetSkill.cooldownRemaining), 0U, uint.MaxValue);
                self.cooldownText.SetText(SkillIcon.sharedStringBuilder);
                self.cooldownText.gameObject.SetActive(true);
            }
        }

        internal void hook_EquipmentIcon_Update(On.RoR2.UI.EquipmentIcon.orig_Update orig, EquipmentIcon self)
        {
            orig(self);
            if ((mod.config.AdvancedIconsEquipementAdvancedDescriptions.Value || 
                mod.config.AdvancedIconsEquipementShowBaseCooldown.Value || 
                mod.config.AdvancedIconsEquipementShowCalculatedCooldown.Value) && 
                self.currentDisplayData.hasEquipment && self.tooltipProvider)
            {
                EquipmentDef equipmentDef = self.currentDisplayData.equipmentDef;
                string bodyText = Language.GetString(mod.config.AdvancedIconsEquipementAdvancedDescriptions.Value ? equipmentDef.descriptionToken : equipmentDef.pickupToken);
                if(mod.config.AdvancedIconsEquipementShowBaseCooldown.Value || mod.config.AdvancedIconsEquipementShowCalculatedCooldown.Value)
                {
                    bodyText += "\n";
                }
                if (mod.config.AdvancedIconsEquipementShowBaseCooldown.Value)
                {
                    bodyText += "\nBase Cooldown: <style=cIsDamage>" + equipmentDef.cooldown + "</style> seconds";
                }
                if (mod.config.AdvancedIconsEquipementShowCalculatedCooldown.Value)
                {
                    Inventory inventory = null;
                    if (self.targetInventory)
                    {
                        inventory = self.targetInventory;
                    }
                    else if (mod.HUD.targetBodyObject)
                    {
                        CharacterBody targetbody = mod.HUD.targetBodyObject.GetComponent<CharacterBody>();
                        if (targetbody != null)
                        {
                            inventory = targetbody.inventory;
                        }
                    }
                    if (inventory != null)
                    {
                        float reduction = (float)Math.Pow(0.85, inventory.itemStacks[(int)ItemIndex.EquipmentMagazine]);
                        if (inventory.itemStacks[(int)ItemIndex.AutoCastEquipment] > 0)
                        {
                            reduction *= 0.5f * (float)Math.Pow(0.85, inventory.itemStacks[(int)ItemIndex.AutoCastEquipment] - 1);
                        }
                        if (reduction < 1)
                        {
                            bodyText += "\nEffective Cooldown: <style=cIsHealing>" + (equipmentDef.cooldown * reduction).ToString("0.###") + "</style> seconds";
                        }
                    }
                }
                

                self.tooltipProvider.overrideBodyText = bodyText;
            }

            if (mod.config.AdvancedIconsEquipementShowCooldownStacks.Value && self.cooldownText && self.currentDisplayData.cooldownValue > 0)
            {
                self.cooldownText.gameObject.SetActive(true);
            }
        }
    }
}
