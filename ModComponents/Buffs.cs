using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using UnityEngine;
using RoR2;
using BetterUI.ModCompatibility;

namespace BetterUI
{
    public static class Buffs
    {
        static IEnumerable<CharacterBody.TimedBuff> timedBuffs;
        static CharacterBody.TimedBuff thisBuff;
        static readonly Dictionary<BuffDef, BuffInfo> buffInfos = new Dictionary<BuffDef, BuffInfo>();

        static Buffs()
        {
            ItemCatalog.availability.CallWhenAvailable(() =>
            {
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixRed, "BUFF_AFFIXRED_NAME", "BUFF_AFFIXRED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixBlue, "BUFF_AFFIXBLUE_NAME", "BUFF_AFFIXBLUE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixWhite, "BUFF_AFFIXWHITE_NAME", "BUFF_AFFIXWHITE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixPoison, "BUFF_AFFIXPOISON_NAME", "BUFF_AFFIXPOISON_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixHaunted, "BUFF_AFFIXHAUNTED_NAME", "BUFF_AFFIXHAUNTED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixLunar, "BUFF_AFFIXLUNAR_NAME", "BUFF_AFFIXLUNAR_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ArmorBoost, "BUFF_ARMORBOOST_NAME", "BUFF_ARMORBOOST_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AttackSpeedOnCrit, "BUFF_ATTACKSPEEDONCRIT_NAME", "BUFF_ATTACKSPEEDONCRIT_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.BugWings, "BUFF_BUGWINGS_NAME", "BUFF_BUGWINGS_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Cloak, "BUFF_CLOAK_NAME", "BUFF_CLOAK_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.CloakSpeed, "BUFF_CLOAKSPEED_NAME", "BUFF_CLOAKSPEED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.AffixHauntedRecipient, "BUFF_AFFIXHAUNTEDRECIPIENT_NAME", "BUFF_AFFIXHAUNTEDRECIPIENT_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ElephantArmorBoost, "BUFF_ELEPHANTARMORBOOST_NAME", "BUFF_ELEPHANTARMORBOOST_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Energized, "BUFF_ENERGIZED_NAME", "BUFF_ENERGIZED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.FullCrit, "BUFF_FULLCRIT_NAME", "BUFF_FULLCRIT_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.HiddenInvincibility, "BUFF_HIDDENINVINCIBILITY_NAME", "BUFF_HIDDENINVINCIBILITY_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Immune, "BUFF_IMMUNE_NAME", "BUFF_IMMUNE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.CrocoRegen, "BUFF_CROCOREGEN_NAME", "BUFF_CROCOREGEN_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.MedkitHeal, "BUFF_MEDKITHEAL_NAME", "BUFF_MEDKITHEAL_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.NoCooldowns, "BUFF_NOCOOLDOWNS_NAME", "BUFF_NOCOOLDOWNS_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.TeslaField, "BUFF_TESLAFIELD_NAME", "BUFF_TESLAFIELD_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.TonicBuff, "BUFF_TONICBUFF_NAME", "BUFF_TONICBUFF_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Warbanner, "BUFF_WARBANNER_NAME", "BUFF_WARBANNER_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.WarCryBuff, "BUFF_WARCRYBUFF_NAME", "BUFF_WARCRYBUFF_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.TeamWarCry, "BUFF_TEAMWARCRY_NAME", "BUFF_TEAMWARCRY_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.WhipBoost, "BUFF_WHIPBOOST_NAME", "BUFF_WHIPBOOST_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LifeSteal, "BUFF_LIFESTEAL_NAME", "BUFF_LIFESTEAL_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.PowerBuff, "BUFF_POWERBUFF_NAME", "BUFF_POWERBUFF_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ElementalRingsReady, "BUFF_ELEMENTALRINGSREADY_NAME", "BUFF_ELEMENTALRINGSREADY_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.BanditSkull, "BUFF_BANDITSKULL_NAME", "BUFF_BANDITSKULL_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LaserTurbineKillCharge, "BUFF_LASERTURBINEKILLCHARGE_NAME", "BUFF_LASERTURBINEKILLCHARGE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.OnFire, "BUFF_ONFIRE_NAME", "BUFF_ONFIRE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Bleeding, "BUFF_BLEEDING_NAME", "BUFF_BLEEDING_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.SuperBleed, "BUFF_SUPERBLEED_NAME", "BUFF_SUPERBLEED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Poisoned, "BUFF_POISONED_NAME", "BUFF_POISONED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Blight, "BUFF_BLIGHT_NAME", "BUFF_BLIGHT_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.MercExpose, "BUFF_MERCEXPOSE_NAME", "BUFF_MERCEXPOSE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.DeathMark, "BUFF_DEATHMARK_NAME", "BUFF_DEATHMARK_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.PulverizeBuildup, "BUFF_PULVERIZEBUILDUP_NAME", "BUFF_PULVERIZEBUILDUP_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Pulverized, "BUFF_PULVERIZED_NAME", "BUFF_PULVERIZED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Cripple, "BUFF_CRIPPLE_NAME", "BUFF_CRIPPLE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Weak, "BUFF_WEAK_NAME", "BUFF_WEAK_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.BeetleJuice, "BUFF_BEETLEJUICE_NAME", "BUFF_BEETLEJUICE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ClayGoo, "BUFF_CLAYGOO_NAME", "BUFF_CLAYGOO_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Slow50, "BUFF_SLOW50_NAME", "BUFF_SLOW50_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Slow60, "BUFF_SLOW60_NAME", "BUFF_SLOW60_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Slow80, "BUFF_SLOW80_NAME", "BUFF_SLOW80_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.NullifyStack, "BUFF_NULLIFYSTACK_NAME", "BUFF_NULLIFYSTACK_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Nullified, "BUFF_NULLIFIED_NAME", "BUFF_NULLIFIED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Entangle, "BUFF_ENTANGLE_NAME", "BUFF_ENTANGLE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Fruiting, "BUFF_FRUITING_NAME", "BUFF_FRUITING_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.HealingDisabled, "BUFF_HEALINGDISABLED_NAME", "BUFF_HEALINGDISABLED_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.ElementalRingsCooldown, "BUFF_ELEMENTALRINGSCOOLDOWN_NAME", "BUFF_ELEMENTALRINGSCOOLDOWN_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.PermanentCurse, "BUFF_PERMANENTCURSE_NAME", "BUFF_PERMANENTCURSE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.Overheat, "BUFF_OVERHEAT_NAME", "BUFF_OVERHEAT_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LunarSecondaryRoot, "BUFF_LUNARSECONDARYROOT_NAME", "BUFF_LUNARSECONDARYROOT_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LunarDetonationCharge, "BUFF_LUNARDETONATIONCHARGE_NAME", "BUFF_LUNARDETONATIONCHARGE_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.EngiShield, "BUFF_ENGISHIELD_NAME", "BUFF_ENGISHIELD_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.LunarShell, "BUFF_LUNARSHELL_NAME", "BUFF_LUNARSHELL_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.VoidFogMild, "BUFF_VOIDFOGMILD_NAME", "BUFF_VOIDFOGMILD_DESC");
                RegisterBuffInfo(RoR2.RoR2Content.Buffs.VoidFogStrong, "BUFF_VOIDFOGSTRONG_NAME", "BUFF_VOIDFOGSTRONG_DESC");

                RegisterBuffInfo(RoR2.JunkContent.Buffs.BodyArmor, "BUFF_BODYARMOR_NAME", "BUFF_BODYARMOR_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.Deafened, "BUFF_DEAFENED_NAME", "BUFF_DEAFENED_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.EngiTeamShield, "BUFF_ENGITEAMSHIELD_NAME", "BUFF_ENGITEAMSHIELD_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.EnrageAncientWisp, "BUFF_ENRAGEANCIENTWISP_NAME", "BUFF_ENRAGEANCIENTWISP_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.GoldEmpowered, "BUFF_GOLDEMPOWERED_NAME", "BUFF_GOLDEMPOWERED_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.LightningShield, "BUFF_LIGHTNINGSHIELD_NAME", "BUFF_LIGHTNINGSHIELD_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.LoaderOvercharged, "BUFF_LOADEROVERCHARGED_NAME", "BUFF_LOADEROVERCHARGED_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.LoaderPylonPowered, "BUFF_LOADERPYLONPOWERED_NAME", "BUFF_LOADERPYLONPOWERED_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.MeatRegenBoost, "BUFF_MEATREGENBOOST_NAME", "BUFF_MEATREGENBOOST_DESC");
                RegisterBuffInfo(RoR2.JunkContent.Buffs.Slow30, "BUFF_SLOW30_NAME", "BUFF_SLOW30_DESC");


                RegisterBuffInfo(RoR2.DLC1Content.Buffs.BearVoidCooldown, "BUFF_BEARVOIDCOOLDOWN_NAME", "BUFF_BEARVOIDCOOLDOWN_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.BearVoidReady, "BUFF_BEARVOIDREADY_NAME", "BUFF_BEARVOIDREADY_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.Blinded, "BUFF_BLINDED_NAME", "BUFF_BLINDED_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.ElementalRingVoidCooldown, "BUFF_ELEMENTALRINGVOIDCOOLDOWN_NAME", "BUFF_ELEMENTALRINGVOIDCOOLDOWN_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.ElementalRingVoidReady, "BUFF_ELEMENTALRINGVOIDREADY_NAME", "BUFF_ELEMENTALRINGVOIDREADY_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.EliteEarth, "BUFF_ELITEEARTH_NAME", "BUFF_ELITEEARTH_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.EliteVoid, "BUFF_ELITEVOID_NAME", "BUFF_ELITEVOID_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.Fracture, "BUFF_FRACTURE_NAME", "BUFF_FRACTURE_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.JailerSlow, "BUFF_JAILERSLOW_NAME", "BUFF_JAILERSLOW_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.JailerTether, "BUFF_JAILERTETHER_NAME", "BUFF_JAILERTETHER_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.KillMoveSpeed, "BUFF_KILLMOVESPEED_NAME", "BUFF_KILLMOVESPEED_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.MushroomVoidActive, "BUFF_MUSHROOMVOIDACTIVE_NAME", "BUFF_MUSHROOMVOIDACTIVE_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.OutOfCombatArmorBuff, "BUFF_OUTOFCOMBATARMORBUFF_NAME", "BUFF_OUTOFCOMBATARMORBUFF_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.PermanentDebuff, "BUFF_PERMANENTDEBUFF_NAME", "BUFF_PERMANENTDEBUFF_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.PrimarySkillShurikenBuff, "BUFF_PRIMARYSKILLSHURIKENBUFF_NAME", "BUFF_PRIMARYSKILLSHURIKENBUFF_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.StrongerBurn, "BUFF_STRONGERBURN_NAME", "BUFF_STRONGERBURN_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.VoidRaidCrabWardWipeFog, "BUFF_VOIDRAIDCRABWARDWIPEFOG_NAME", "BUFF_VOIDRAIDCRABWARDWIPEFOG_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.VoidSurvivorCorruptMode, "BUFF_VOIDSURVIVORCORRUPTMODE_NAME", "BUFF_VOIDSURVIVORCORRUPTMODE_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.ImmuneToDebuffReady, "BUFF_RAINCOATREADY_NAME", "BUFF_RAINCOATREADY_DESC");
                RegisterBuffInfo(RoR2.DLC1Content.Buffs.ImmuneToDebuffCooldown, "BUFF_RAINCOATCOOLDOWN_NAME", "BUFF_RAINCOATCOOLDOWN_DESC");
            });
        }

        public static void RegisterName(BuffDef buffDef, string nameToken)
        {
            RegisterBuffInfo(buffDef, nameToken: nameToken);
        }

        public static void RegisterDescription(BuffDef buffDef, string descriptionToken)
        {
            RegisterBuffInfo(buffDef, descriptionToken: descriptionToken);
        }

        public static void RegisterBuffInfo(BuffDef buffDef, string nameToken = null, string descriptionToken = null)
        {
            if(buffDef == null)
            {
                UnityEngine.Debug.LogError($"Unable to register BuffInfo for {Language.GetString(nameToken)}");
                return;
            }
            buffInfos.TryGetValue(buffDef, out BuffInfo buffInfo);
            buffInfo.nameToken = buffInfo.nameToken ?? nameToken;
            buffInfo.descriptionToken = buffInfo.descriptionToken ?? descriptionToken;
            RegisterBuffInfo(buffDef, buffInfo);
        }
        public static void RegisterBuffInfo(BuffDef buffDef, BuffInfo buffInfo)
        {
            if (buffDef == null)
            {
                UnityEngine.Debug.LogError($"Unable to register BuffInfo for {Language.GetString("nameToken")}");
                return;
            }
            if (BetterUIPlugin.BetterAPIModIntegration)
            {
                ModCompatibility.BetterAPICompatibility.Buffs.AddInfo(buffDef, buffInfo);
                return;
            }
            buffInfos[buffDef] = buffInfo;
        }
        public static string GetName(BuffDef buffDef)
        {
            if (BetterUIPlugin.BetterAPIModIntegration)
            {
                return ModCompatibility.BetterAPICompatibility.Buffs.GetName(buffDef);
            }
            buffInfos.TryGetValue(buffDef, out BuffInfo buffInfo);
            return string.IsNullOrEmpty(buffInfo.nameToken) ? buffDef.name : RoR2.Language.GetString(buffInfo.nameToken);
        }
        public static string GetDescription(BuffDef buffDef)
        {
            if (BetterUIPlugin.BetterAPIModIntegration)
            {
                return ModCompatibility.BetterAPICompatibility.Buffs.GetDescription(buffDef);
            }
            buffInfos.TryGetValue(buffDef, out BuffInfo buffInfo);
            return string.IsNullOrEmpty(buffInfo.descriptionToken) ? String.Empty : RoR2.Language.GetString(buffInfo.descriptionToken);
        }

        internal static void Hook()
        {
            if (ConfigManager.BuffTimers.Value || ConfigManager.BuffTooltips.Value)
            {
                BetterUIPlugin.Hooks.Add<RoR2.UI.BuffIcon>("Awake", BuffIcon_Awake);
                BetterUIPlugin.Hooks.Add<RoR2.UI.BuffIcon>("UpdateIcon", BuffIcon_UpdateIcon);
            }
        }
        internal static void BuffIcon_Awake(Action<RoR2.UI.BuffIcon> orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            if (self.transform.parent.name == "BuffDisplayRoot")
            {
                if (ConfigManager.BuffTooltips.Value)
                {
                    UnityEngine.UI.GraphicRaycaster raycaster = self.transform.parent.GetComponent<UnityEngine.UI.GraphicRaycaster>();
                    if (raycaster == null)
                    {
                        self.transform.parent.gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                    }
                    self.gameObject.AddComponent<RoR2.UI.TooltipProvider>();
                }
                if (ConfigManager.BuffTimers.Value)
                {
                    GameObject TimerText = new GameObject("TimerText");
                    RectTransform timerRect = TimerText.AddComponent<RectTransform>();
                    RoR2.UI.HGTextMeshProUGUI timerTextMesh = TimerText.AddComponent<RoR2.UI.HGTextMeshProUGUI>();
                    TimerText.transform.SetParent(self.transform);

                    timerTextMesh.enableWordWrapping = false;
                    timerTextMesh.alignment = ConfigManager.BuffTimersTextAlignmentOption;
                    timerTextMesh.fontSize = ConfigManager.BuffTimersFontSize.Value;
                    timerTextMesh.faceColor = Color.white;
                    timerTextMesh.text = "";

                    timerRect.localPosition = Vector3.zero;
                    timerRect.anchorMin = new Vector2(1, 0);
                    timerRect.anchorMax = new Vector2(1, 0);
                    timerRect.localScale = Vector3.one;
                    timerRect.sizeDelta = new Vector2(48, 48);
                    timerRect.anchoredPosition = new Vector2(-24, 24);
                }
            }
        }

        internal static void BuffIcon_UpdateIcon(Action<RoR2.UI.BuffIcon> orig, RoR2.UI.BuffIcon self)
        {
            orig(self);
            if (self.buffDef && self.transform.parent.name == "BuffDisplayRoot")
            {
                if (ConfigManager.BuffTooltips.Value)
                {
                    RoR2.UI.TooltipProvider tooltipProvider = self.GetComponent<RoR2.UI.TooltipProvider>();
                    tooltipProvider.titleToken = GetName(self.buffDef);
                    tooltipProvider.bodyToken = GetDescription(self.buffDef);
                    tooltipProvider.titleColor = self.buffDef.buffColor;
                }
                if (ConfigManager.BuffTimers.Value)
                {
                    Transform timerText = self.transform.Find("TimerText");
                    if (timerText != null)
                    {
                        if (BetterUIPlugin.hud != null)
                        {
                            CharacterBody characterBody = BetterUIPlugin.hud.targetBodyObject ? BetterUIPlugin.hud.targetBodyObject.GetComponent<CharacterBody>() : null;
                            if (characterBody != null && characterBody.timedBuffs.Count > 0)
                            {
                                timedBuffs = characterBody.timedBuffs.Where(b => b.buffIndex == self.buffDef.buffIndex);
                                if(timedBuffs.Any())
                                {
                                    thisBuff = timedBuffs.OrderByDescending(b => b.timer).First();
                                    timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = thisBuff.timer < 10 && ConfigManager.BuffTimersDecimal.Value ? thisBuff.timer.ToString("N1") : thisBuff.timer.ToString("N0");
                                    return;
                                }
                            }
                        }
                        timerText.GetComponent<RoR2.UI.HGTextMeshProUGUI>().text = "";
                    }
                }
            }
        }
        public struct BuffInfo
        {
            public string nameToken;
            public string descriptionToken;
        }
    }
}
