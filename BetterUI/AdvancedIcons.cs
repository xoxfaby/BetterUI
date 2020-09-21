using System;
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
        internal AdvancedIcons(BetterUI mod)
        {
            this.mod = mod;
        }
        internal void hook_SetItemIndex(On.RoR2.UI.ItemIcon.orig_SetItemIndex orig, RoR2.UI.ItemIcon self, ItemIndex itemIndex, int itemCount)
        {
            orig(self, itemIndex, itemCount);

            self.tooltipProvider.bodyToken = ItemCatalog.GetItemDef(itemIndex).descriptionToken;
        }
        internal void hook_SkillIcon_Update(On.RoR2.UI.SkillIcon.orig_Update orig, SkillIcon self)
        {
            orig(self);
            
            if(mod.config.AdvancedIconsSkillShowProcCoefficient.Value || mod.config.AdvancedIconsSkillCalculateSkillProcEffects.Value)
            {
                List<ProcCoefficientCatalog.ProcCoefficientInfo> procCoefficientInfos = self.targetSkill ? ProcCoefficientCatalog.GetProcCoefficientInfo(self.targetSkill.skillDef.skillNameToken) : null;

                if (procCoefficientInfos != null)
                {
                    string tooltipBody = Language.GetString(self.targetSkill.skillDescriptionToken) + "";
                    foreach (var info in procCoefficientInfos)
                    {
                        if (mod.config.AdvancedIconsSkillShowProcCoefficient.Value)
                        {
                            tooltipBody += "\n\n<size=110%>" + info.name + ":</size>\n<style=cIsUtility>Proc Coefficient: " + info.procCoefficient + "</style>";
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
                                    switch (item.Value.effectType)
                                    {
                                        case ProcItemsCatalog.ProcEffect.Chance:
                                            if (item.Value.stacking == ProcItemsCatalog.Stacking.Linear)
                                            {
                                                tooltipBody += "<style=cIsDamage>" + Math.Min(100, Utils.LuckCalc((item.Value.value + item.Value.stackAmount * (stacks - 1)) * info.procCoefficient, self.targetSkill.characterBody.master.luck)).ToString("0.##");
                                                tooltipBody += "%</style> <style=cStack>(" + Math.Ceiling(1 / (item.Value.value * info.procCoefficient * 0.01)) + " stacks to cap)</style>";
                                            }
                                            else if (item.Value.stacking == ProcItemsCatalog.Stacking.Hyperbolic)
                                            {
                                                tooltipBody += "<style=cIsDamage>" + Math.Min(100, Utils.LuckCalc((float)(1 - 1 / (1 + item.Value.value * 0.01 * stacks)) * info.procCoefficient * 100, self.targetSkill.characterBody.master.luck)).ToString("0.##") + "%</style>";
                                            }
                                            else if (item.Value.stacking == ProcItemsCatalog.Stacking.None)
                                            {
                                                tooltipBody += "<style=cIsDamage>" + Math.Min(100, Utils.LuckCalc(item.Value.value * info.procCoefficient, self.targetSkill.characterBody.master.luck)).ToString("0.##") + "%</style>";
                                            }
                                            break;
                                        case ProcItemsCatalog.ProcEffect.HP:
                                            tooltipBody += "<style=cIsHealing>" + (item.Value.value + item.Value.stackAmount * (stacks - 1)) * info.procCoefficient + " HP</style>";
                                            break;
                                        case ProcItemsCatalog.ProcEffect.Range:
                                            tooltipBody += "<style=cIsDamage>" + (item.Value.value + item.Value.stackAmount * (stacks - 1)) * info.procCoefficient + "m</style>";
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    self.tooltipProvider.overrideBodyText = tooltipBody;
                }
                else
                {
                    self.tooltipProvider.overrideBodyText = null;
                }
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
