using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    public static class ItemStats
    {

        internal static Dictionary<ItemDef, List<ItemStat>> itemStats = new Dictionary<ItemDef, List<ItemStat>>();
        internal static Dictionary<ItemStat, List<ItemTag>> itemTags = new Dictionary<ItemStat, List<ItemTag>>();
        internal static Dictionary<ItemTag, List<ItemModifier>> itemModifiers = new Dictionary<ItemTag, List<ItemModifier>>();
        public static readonly Dictionary<ItemDef, ItemProcInfo> itemProcInfos = new Dictionary<ItemDef, ItemProcInfo>();

        
        public static void Initialize()
        {
            RegisterStat(RoR2Content.Items.AlienHead, "BETTERUI_COOLDOWNREDUCTION", 0.25f, NegativeExponentialStacking, StatFormatter.Percent, itemTag: ItemTag.SkillCooldown);
            RegisterModifier(ItemTag.SkillCooldown, RoR2Content.Items.AlienHead, ItemModifier.ExponentialBonus, 0.25f);
            RegisterStat(RoR2Content.Items.ArmorPlate, "BETTERUI_ARMOR", 5f, LinearStacking, StatFormatter.Armor);
            RegisterStat(RoR2Content.Items.ArmorReductionOnHit, "BETTERUI_DURATION", 8, LinearStacking, StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.AttackSpeedOnCrit, "BETTERUI_MAXIMUMATTACKSPEED", 0.36f, 0.24f, LinearStacking, StatFormatter.Percent);
            RegisterStat(RoR2Content.Items.AutoCastEquipment, "BETTERUI_COOLDOWNREDUCTION", 0.5f, 0.15f, NegativeExponentialStacking, StatFormatter.Percent, ItemTag.EquipmentCooldown);
            RegisterModifier(ItemTag.EquipmentCooldown, RoR2Content.Items.AutoCastEquipment, ItemModifier.ExponentialBonus, 0.5f, 0.15f);
            RegisterStat(RoR2Content.Items.Bandolier, "BETTERUI_DROPCHANCE", 0.18f, 0.10f, itemTag: ItemTag.Luck);
            RegisterStat(RoR2Content.Items.BarrierOnKill, "BETTERUI_BARRIER", 15, statFormatter: StatFormatter.HP);
            RegisterStat(RoR2Content.Items.BarrierOnOverHeal, "BETTERUI_HEALINGCONVERTED", 0.5f);
            RegisterStat(RoR2Content.Items.Bear, "BETTERUI_TOUGHERTIMES", 0.15f, HyperbolicStacking);
            RegisterStat(RoR2Content.Items.BeetleGland, "BETTERUI_BEETLEGUARDS", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.Behemoth, "BETTERUI_EXPLOSIONRADIUS", 4, 2.5f, statFormatter: StatFormatter.Range);
            RegisterProc(RoR2Content.Items.Behemoth, 4f, 2.5f, statFormatter: StatFormatter.Range, stackingFormula: LinearStacking);
            RegisterStat(RoR2Content.Items.BleedOnHit, "BETTERUI_BLEEDCHANCE", 0.1f, LinearStacking, StatFormatter.Chance, ItemTag.Luck);
            RegisterProc(RoR2Content.Items.BleedOnHit, 0.1f, capFormula: LinearCap);
            RegisterStat(RoR2Content.Items.BleedOnHitAndExplode, "BETTERUI_EXPLOSIONBASEDAMAGE", 4);
            RegisterStat(RoR2Content.Items.BleedOnHitAndExplode, "BETTERUI_EXPLOSIONMAXHPDAMAGE", 0.15f);
            RegisterStat(RoR2Content.Items.BonusGoldPackOnKill, "BETTERUI_DROPCHANCE", 0.04f, itemTag: ItemTag.Luck);
            RegisterStat(RoR2Content.Items.BossDamageBonus, "BETTERUI_DAMAGE", 0.2f);
            RegisterStat(RoR2Content.Items.BounceNearby, "BETTERUI_CHANCE", 0.25f, HyperbolicStacking, itemTag: ItemTag.Luck);
            RegisterStat(RoR2Content.Items.BounceNearby, "BETTERUI_TARGETS", 10, 5, statFormatter: StatFormatter.Charges);
            RegisterProc(RoR2Content.Items.BounceNearby, 0.2f, stackingFormula: HyperbolicStacking);
            RegisterStat(RoR2Content.Items.CaptainDefenseMatrix, "BETTERUI_PROJECTILESSHOT", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.ChainLightning, "BETTERUI_TARGETS", 3, 2, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.ChainLightning, "BETTERUI_RADIUS", 20, 2, statFormatter: StatFormatter.Range);
            RegisterProc(RoR2Content.Items.ChainLightning, 0.25f, stackingFormula: NoStacking);
            RegisterStat(RoR2Content.Items.Clover, "BETTERUI_LUCK", 1, statFormatter: StatFormatter.Charges, itemTag: ItemTag.LuckStat);
            RegisterStat(RoR2Content.Items.CritGlasses, "BETTERUI_CHANCE", 0.10f);
            RegisterStat(RoR2Content.Items.Crowbar, "BETTERUI_DAMAGE", 0.75f);
            RegisterStat(RoR2Content.Items.Dagger, "BETTERUI_DAMAGE", 1.5f);
            RegisterStat(RoR2Content.Items.DeathMark, "BETTERUI_DEBUFFDURATION", 7, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.EnergizedOnEquipmentUse, "BETTERUI_DURATION", 8, 4, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.EquipmentMagazine, "BETTERUI_EQUIPMENTCHARGES", 1f, LinearStacking, StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.EquipmentMagazine, "BETTERUI_COOLDOWNREDUCTION", 0.15f, 0.15f, NegativeExponentialStacking, StatFormatter.Percent, ItemTag.EquipmentCooldown);
            RegisterModifier(ItemTag.EquipmentCooldown, RoR2Content.Items.EquipmentMagazine, ItemModifier.ExponentialBonus, 0.15f);
            RegisterStat(RoR2Content.Items.ExecuteLowHealthElite, "BETTERUI_THRESHOLD", 0.13f, HyperbolicStacking);
            RegisterStat(RoR2Content.Items.ExplodeOnDeath, "BETTERUI_DAMAGE", 3.5f, 2.8f, LinearStacking);
            RegisterStat(RoR2Content.Items.ExplodeOnDeath, "BETTERUI_RADIUS", 12, 2.4f, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.ExtraLife, "BETTERUI_USES", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.FallBoots, "BETTERUI_COOLDOWN", 10, 0.5f, ExponentialStacking, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.Feather, "BETTERUI_EXTRAJUMPS", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.FireRing, "BETTERUI_DAMAGE", 3);
            RegisterStat(RoR2Content.Items.FireballsOnHit, "BETTERUI_DAMAGE", 3);
            RegisterStat(RoR2Content.Items.Firework, "BETTERUI_FIREWORKS", 8, 4, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.FlatHealth, "BETTERUI_HEALTH", 25, statFormatter: StatFormatter.HP);
            RegisterStat(RoR2Content.Items.FocusConvergence, "BETTERUI_CHARGESPEED", 0.30f);
            RegisterStat(RoR2Content.Items.FocusConvergence, "BETTERUI_TELEPORTERZONE", 2, 3, FocusedConvergenceStacking);
            RegisterStat(RoR2Content.Items.GhostOnKill, "BETTERUI_GHOSTDURATION", 30, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.GoldOnHit, "BETTERUI_GOLDGAINED", 2, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.GoldOnHit, "BETTERUI_GOLDLOST", 1);
            RegisterProc(RoR2Content.Items.GoldOnHit, 0.3f, stackingFormula: NoStacking);
            RegisterStat(RoR2Content.Items.HeadHunter, "BETTERUI_DURATION", 8, 5, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.HealOnCrit, "BETTERUI_HEAL", 8, 4, statFormatter: StatFormatter.HP);
            RegisterProc(RoR2Content.Items.HealOnCrit, 8f, statFormatter: StatFormatter.HP, stackingFormula: LinearStacking);
            RegisterStat(RoR2Content.Items.HealWhileSafe, "BETTERUI_REGEN", 3, statFormatter: StatFormatter.Regen);
            RegisterStat(RoR2Content.Items.Hoof, "BETTERUI_MOVEMENTSPEED", 0.14f, itemTag: ItemTag.MovementSpeed);
            RegisterStat(RoR2Content.Items.IceRing, "BETTERUI_SLOWDURATION", 3, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.IceRing, "BETTERUI_DAMAGE", 2.5f);
            RegisterStat(RoR2Content.Items.Icicle, "BETTERUI_MAXIMUMRADIUS", 18, 12, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.IgniteOnKill, "BETTERUI_RADIUS", 12, 4, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.IgniteOnKill, "BETTERUI_DAMAGE", 1.5f, 0.75f);
            RegisterStat(RoR2Content.Items.IncreaseHealing, "BETTERUI_HEALING", 1);
            RegisterModifier(ItemTag.Healing, RoR2Content.Items.IncreaseHealing, ItemModifier.PercentBonus, 100);
            RegisterStat(RoR2Content.Items.Infusion, "BETTERUI_MAXIMUMHEALTH", 100, statFormatter: StatFormatter.HP, itemTag: ItemTag.MaxHealth);
            RegisterStat(RoR2Content.Items.Infusion, "BETTERUI_HEALTHPERKILL", 1, statFormatter: StatFormatter.HP);
            RegisterStat(RoR2Content.Items.JumpBoost, "BETTERUI_BOOST", 10, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.KillEliteFrenzy, "BETTERUI_DURATION", 4, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.Knurl, "BETTERUI_HEALTH", 40f, LinearStacking, StatFormatter.HP);
            RegisterStat(RoR2Content.Items.Knurl, "BETTERUI_REGEN", 1.6f, LinearStacking, StatFormatter.Regen, ItemTag.Healing);
            RegisterStat(RoR2Content.Items.LaserTurbine, "BETTERUI_DAMAGE", 3);
            RegisterStat(RoR2Content.Items.LaserTurbine, "BETTERUI_EXPLOSION", 10);
            RegisterStat(RoR2Content.Items.LightningStrikeOnHit, "BETTERUI_DAMAGE", 5);
            RegisterStat(RoR2Content.Items.LunarBadLuck, "BETTERUI_LUCK", -1, statFormatter: StatFormatter.Charges, itemTag: ItemTag.LuckStat);
            RegisterStat(RoR2Content.Items.LunarDagger, "BETTERUI_DAMAGE", 2, ExponentialStacking);
            RegisterModifier(ItemTag.Damage, RoR2Content.Items.LunarDagger, ItemModifier.PositiveExponentialBonus, 2);
            RegisterStat(RoR2Content.Items.LunarDagger, "BETTERUI_HEALTH", 0.5f, ExponentialStacking);
            RegisterStat(RoR2Content.Items.LunarPrimaryReplacement, "BETTERUI_CHARGES", 12, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.LunarPrimaryReplacement, "BETTERUI_RELOAD", 2, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.LunarSecondaryReplacement, "BETTERUI_ROOTDURATION", 3, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.LunarSecondaryReplacement, "BETTERUI_COOLDOWN", 5, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.LunarSpecialReplacement, "BETTERUI_RUINDURATION", 10, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.LunarSpecialReplacement, "BETTERUI_COOLDOWN", 8, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.LunarUtilityReplacement, "BETTERUI_HEAL", 0.182f);
            RegisterStat(RoR2Content.Items.LunarUtilityReplacement, "BETTERUI_DURATION", 3, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.Medkit, "BETTERUI_HEAL", 0.05f);
            RegisterStat(RoR2Content.Items.Missile, "BETTERUI_DAMAGE", 3);
            RegisterProc(RoR2Content.Items.Missile, 0.1f, stackingFormula: NoStacking);
            RegisterStat(RoR2Content.Items.MonstersOnShrineUse, "BETTERUI_ENEMYDIFFICULTY", 1f);
            RegisterStat(RoR2Content.Items.Mushroom, "BETTERUI_HEALTHPERSECOND", 0.045f, 0.0225f);
            RegisterStat(RoR2Content.Items.Mushroom, "BETTERUI_RADIUS", 3, 1.5f, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.NearbyDamageBonus, "BETTERUI_DAMAGE", 0.2f);
            RegisterStat(RoR2Content.Items.NovaOnHeal, "BETTERUI_DAMAGE", 1);
            RegisterStat(RoR2Content.Items.NovaOnLowHealth, "BETTERUI_RECHARGESPEED", 30, DivideByBonusStacks, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.ParentEgg, "BETTERUI_HEATH", 15, statFormatter: StatFormatter.HP);
            RegisterStat(RoR2Content.Items.Pearl, "BETTERUI_HEALTH", 0.1f);
            RegisterModifier(ItemTag.MaxHealth, RoR2Content.Items.Pearl, ItemModifier.PercentBonus, 10);
            RegisterStat(RoR2Content.Items.PersonalShield, "BETTERUI_SHIELD", 0.08f);
            RegisterStat(RoR2Content.Items.Phasing, "BETTERUI_COOLDOWN", 30, 0.5f, ExponentialStacking, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.Plant, "BETTERUI_RADIUS", 5, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.RandomDamageZone, "BETTERUI_RANGE", 16, 1.5f, ExponentialStacking, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.RepeatHeal, "BETTERUI_HEALING", 1, itemTag: ItemTag.Healing);
            RegisterStat(RoR2Content.Items.RepeatHeal, "BETTERUI_MAXIMUM", 0.1f, 0.5f, ExponentialStacking, itemTag: ItemTag.Healing);
            RegisterModifier(ItemTag.Healing, RoR2Content.Items.RepeatHeal, ItemModifier.PercentBonus, 100);
            RegisterStat(RoR2Content.Items.RoboBallBuddy, "BETTERUI_DAMAGE", 1f, itemTag: ItemTag.Allies);
            RegisterStat(RoR2Content.Items.SecondarySkillMagazine, "BETTERUI_CHARGES", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.Seed, "BETTERUI_HEAL", 1, statFormatter: StatFormatter.HP, itemTag: ItemTag.Healing);
            RegisterProc(RoR2Content.Items.Seed, 1f, statFormatter: StatFormatter.HP, stackingFormula: LinearStacking);
            RegisterStat(RoR2Content.Items.ShieldOnly, "BETTERUI_MAXIMUMHEALTH", 0.5f, 0.25f);
            RegisterModifier(ItemTag.MaxHealth, RoR2Content.Items.ShieldOnly, ItemModifier.PercentBonus, 50, 25);
            RegisterStat(RoR2Content.Items.ShinyPearl, "BETTERUI_HEALTH", 0.1f);
            RegisterModifier(ItemTag.MaxHealth, RoR2Content.Items.ShinyPearl, ItemModifier.PercentBonus, 10);
            RegisterStat(RoR2Content.Items.ShinyPearl, "BETTERUI_REGEN", 0.1f, statFormatter: StatFormatter.Regen, itemTag: ItemTag.Healing);
            RegisterStat(RoR2Content.Items.ShinyPearl, "BETTERUI_MOVEMENTSPEED", 0.1f, itemTag: ItemTag.MovementSpeed);
            RegisterStat(RoR2Content.Items.ShinyPearl, "BETTERUI_DAMAGE", 0.1f);
            RegisterModifier(ItemTag.Damage, RoR2Content.Items.ShinyPearl, ItemModifier.PercentBonus, 10);
            RegisterStat(RoR2Content.Items.ShinyPearl, "BETTERUI_ATTACKSPEED", 0.1f);
            RegisterStat(RoR2Content.Items.ShinyPearl, "BETTERUI_CRITCHANCE", 0.1f);
            RegisterStat(RoR2Content.Items.ShinyPearl, "BETTERUI_ARMOR", 0.1f);
            RegisterStat(RoR2Content.Items.ShockNearby, "BETTERUI_TARGETS", 3, 2, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.SiphonOnLowHealth, "BETTERUI_TETHEREDENEMIES", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.SlowOnHit, "BETTERUI_SLOWDURATION", 2, 2, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.SprintArmor, "BETTERUI_ARMOR", 30, statFormatter: StatFormatter.Armor);
            RegisterStat(RoR2Content.Items.SprintBonus, "BETTERUI_SPRINTSPEED", 0.25f);
            RegisterStat(RoR2Content.Items.SprintOutOfCombat, "BETTERUI_MOVEMENTSPEED", 0.3f, itemTag: ItemTag.MovementSpeed);
            RegisterStat(RoR2Content.Items.SprintWisp, "BETTERUI_DAMAGE", 3f, itemTag: ItemTag.Damage);
            RegisterStat(RoR2Content.Items.Squid, "BETTERUI_ATTACKSPEED", 1f);
            RegisterStat(RoR2Content.Items.StickyBomb, "BETTERUI_CHANCE", 0.05f, itemTag: ItemTag.Luck);
            RegisterProc(RoR2Content.Items.StickyBomb, 0.05f, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterStat(RoR2Content.Items.StunChanceOnHit, "BETTERUI_CHANCE", 0.05f, HyperbolicStacking, itemTag: ItemTag.Luck);
            RegisterProc(RoR2Content.Items.StunChanceOnHit, 0.05f, stackingFormula: HyperbolicStacking);
            RegisterStat(RoR2Content.Items.Syringe, "BETTERUI_ATTACKSPEED", 0.15f);
            RegisterStat(RoR2Content.Items.TPHealingNova, "BETTERUI_HEALINGNOVA", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.Talisman, "BETTERUI_COOLDOWNREDUCTION", 4, 2, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.Thorns, "BETTERUI_TARGETS", 5, 2, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.Thorns, "BETTERUI_RADIUS", 25, 10, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.TitanGoldDuringTP, "BETTERUI_DAMAGE", 1, 0.5f);
            RegisterStat(RoR2Content.Items.TitanGoldDuringTP, "BETTERUI_HEALTH", 1);
            RegisterModifier(ItemTag.TitanDamage, RoR2Content.Items.TitanGoldDuringTP, ItemModifier.FlatBonus, 0.5f, modificationChecker: ItemModifier.TeamItemChecker, modificationCounter: ItemModifier.TeamItemCounter);
            RegisterModifier(ItemTag.TitanHealth, RoR2Content.Items.TitanGoldDuringTP, ItemModifier.FlatBonus, 1f, modificationChecker: ItemModifier.TeamItemChecker, modificationCounter: ItemModifier.TeamItemCounter);
            RegisterStat(RoR2Content.Items.Tooth, "BETTERUI_HEALTHPERORB", 0.02f, itemTag: ItemTag.Healing);
            RegisterStat(RoR2Content.Items.UtilitySkillMagazine, "BETTERUI_CHARGES", 2, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.WarCryOnMultiKill, "BETTERUI_FRENZYDURATION", 6, 4, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.WardOnLevel, "BETTERUI_RADIUS", 16, 8, statFormatter: StatFormatter.Range);

            RegisterStat(DLC1Content.Items.AttackSpeedAndMoveSpeed, "BETTERUI_ATTACKSPEED", 0.075f);
            RegisterStat(DLC1Content.Items.AttackSpeedAndMoveSpeed, "BETTERUI_MOVEMENTSPEED", 0.07f, itemTag: ItemTag.MovementSpeed);
            RegisterStat(DLC1Content.Items.BearVoid, "BETTERUI_RECHARGETIME", 15, 0.9f, ExponentialStacking, StatFormatter.Seconds);
            RegisterStat(DLC1Content.Items.BleedOnHitVoid, "BETTERUI_CHANCE", 0.1f, itemTag: ItemTag.Luck);
            RegisterProc(DLC1Content.Items.BleedOnHitVoid, 0.1f, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterStat(DLC1Content.Items.ChainLightningVoid, "BETTERUI_HITS", 3, statFormatter: StatFormatter.Charges);
            RegisterProc(DLC1Content.Items.ChainLightningVoid, 0.25f, stackingFormula: NoStacking);
            RegisterStat(DLC1Content.Items.CloverVoid, "BETTERUI_ITEMSUPGRADED", 3, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.CritDamage, "BETTERUI_DAMAGE", 1f);
            RegisterStat(DLC1Content.Items.CritGlassesVoid, "BETTERUI_CHANCE", 0.005f, itemTag: ItemTag.Luck);
            RegisterProc(DLC1Content.Items.CritGlassesVoid, 0.005f, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterStat(DLC1Content.Items.DroneWeapons, "BETTERUI_ATTACKSPEED", 0.5f);
            RegisterStat(DLC1Content.Items.ElementalRingVoid, "BETTERUI_DAMAGE", 1f);
            RegisterStat(DLC1Content.Items.EquipmentMagazineVoid, "BETTERUI_CHARGES", 1f, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.ExplodeOnDeathVoid, "BETTERUI_DAMAGE", 2.6f, 1.56f, LinearStacking);
            RegisterStat(DLC1Content.Items.ExplodeOnDeathVoid, "BETTERUI_RADIUS", 12, 2.4f, statFormatter: StatFormatter.Range);
            RegisterStat(DLC1Content.Items.ExtraLifeVoid, "BETTERUI_USES", 1f, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.FragileDamageBonus, "BETTERUI_DAMAGE", 0.2f);
            RegisterModifier(ItemTag.Damage, DLC1Content.Items.FragileDamageBonus, ItemModifier.PercentBonus, 20);
            //            RegisterStat(DLC1Content.Items.FreeChest, "BETTERUI_SHIPPINGREQUESTFORM";
            RegisterStat(DLC1Content.Items.GoldOnHurt, "BETTERUI_BASEGOLD", 3, statFormatter: StatFormatter.Gold);
            RegisterStat(DLC1Content.Items.HalfAttackSpeedHalfCooldowns, "BETTERUI_SKILLCOOLDOWNS", 0.5f, ExponentialStacking, itemTag: ItemTag.SkillCooldown);
            RegisterModifier(ItemTag.SkillCooldown, DLC1Content.Items.HalfAttackSpeedHalfCooldowns, ItemModifier.ExponentialBonus, 0.5f);
            RegisterStat(DLC1Content.Items.HalfAttackSpeedHalfCooldowns, "BETTERUI_ATTACKSPEED", 1, DivideByBonusStacks);
            RegisterStat(DLC1Content.Items.HalfSpeedDoubleHealth, "BETTERUI_MAXHEALTH", 1);
            RegisterModifier(ItemTag.MaxHealth, DLC1Content.Items.HalfSpeedDoubleHealth, ItemModifier.PercentBonus, 100);
            RegisterStat(DLC1Content.Items.HalfSpeedDoubleHealth, "BETTERUI_MOVEMENTSPEED", 0.5f, HyperbolicStacking);
            RegisterModifier(ItemTag.MovementSpeed, DLC1Content.Items.HalfSpeedDoubleHealth, ItemModifier.ExponentialBonus, 0.5f);
            RegisterStat(DLC1Content.Items.HealingPotion, "BETTERUI_USES", 1f, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.ImmuneToDebuff, "BETTERUI_MAXHEALTH", 100, statFormatter: StatFormatter.HP, itemTag: ItemTag.MaxHealth);
            RegisterStat(DLC1Content.Items.LunarSun, "BETTERUI_CHARGETIME", 3f, DivideByStacks, statFormatter: StatFormatter.Seconds);
            RegisterStat(DLC1Content.Items.LunarSun, "BETTERUI_BOMBS", 3f, 1f, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.MinorConstructOnKill, "BETTERUI_MAXCONSTRUCTS", 4, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.MissileVoid, "BETTERUI_DAMAGE", 0.4f);
            RegisterStat(DLC1Content.Items.MoreMissile, "BETTERUI_DAMAGE", 0f, 0.5f);
            RegisterStat(DLC1Content.Items.MoveSpeedOnKill, "BETTERUI_DURATION", 1, 0.5f, statFormatter: StatFormatter.Seconds);
            RegisterStat(DLC1Content.Items.MushroomVoid, "BETTERUI_HPPERSECOND", 0.02f);
            RegisterStat(DLC1Content.Items.OutOfCombatArmor, "BETTERUI_ARMOR", 100, statFormatter: StatFormatter.Armor);
            RegisterStat(DLC1Content.Items.PermanentDebuffOnHit, "BETTERUI_ARMORREDUCTION", 2, statFormatter: StatFormatter.Armor);
            RegisterStat(DLC1Content.Items.PrimarySkillShuriken, "BETTERUI_DAMAGE", 4, 1);
            RegisterStat(DLC1Content.Items.PrimarySkillShuriken, "BETTERUI_SHURIKEN", 3, 1, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.RandomEquipmentTrigger, "BETTERUI_EFFECTS", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.RandomlyLunar, "BETTERUI_CHANCE", 0.05f, itemTag: ItemTag.Luck);
            RegisterStat(DLC1Content.Items.RegeneratingScrap, "BETTERUI_USES", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.SlowOnHitVoid, "BETTERUI_CHANCE", 0.05f, itemTag: ItemTag.Luck);
            RegisterStat(DLC1Content.Items.SlowOnHitVoid, "BETTERUI_DURATION", 1, statFormatter: StatFormatter.Seconds);
            RegisterProc(DLC1Content.Items.SlowOnHitVoid, 0.05f, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterStat(DLC1Content.Items.StrengthenBurn, "BETTERUI_DAMAGE", 3f);
            //RegisterStat(DLC1Content.Items.TreasureCacheVoid, "BETTERUI_ENCRUSTEDKEY", , , );
            RegisterStat(DLC1Content.Items.VoidMegaCrabItem, "BETTERUI_COOLDOWN", 60, 0.5f, ExponentialStacking, statFormatter: StatFormatter.Seconds);
            RegisterStat(DLC1Content.Items.VoidMegaCrabItem, "BETTERUI_MAXALLIES", 1, statFormatter: StatFormatter.Charges);


            RegisterModifier(ItemTag.Luck, "BETTERUI_LUCK", ItemModifier.LuckBonus, ItemModifier.LuckLocator, ItemModifier.LuckChecker, ItemModifier.LuckCounter, "BETTERUI_LUCK");
            RegisterModifier(ItemTag.Allies, "BETTERUI_ALLY", ItemModifier.AlliesBonus, ItemModifier.AlliesLocator, ItemModifier.AlliesChecker, ItemModifier.AlliesCounter, "BETTERUI_ALLIES");



        }

        public static List<ItemStat> GetItemStats(ItemDef itemDef)
        {
            if (itemStats.TryGetValue(itemDef, out var stats)) return stats;
            return null;
        }

        public static List<ItemTag> GetItemTags(ItemStat itemStat)
        {
            if (itemTags.TryGetValue(itemStat, out var tags)) return tags;
            return null;
        }

        public static List<ItemModifier> GetItemModifers(ItemTag itemTag)
        {
            if (itemModifiers.TryGetValue(itemTag, out var modifiers)) return modifiers;
            return null;
        }

        public static ItemStat RegisterStat(ItemDef itemDef, string nameToken, float value, StackingFormula stackingFormula = null, StatFormatter statFormatter = null, ItemTag itemTag = null)
        {
            return RegisterStat(itemDef, nameToken, value, value, stackingFormula, statFormatter, itemTag);
        }

        public static ItemStat RegisterStat(
            ItemDef itemDef,
            string nameToken,
            float value,
            float stackValue,
            StackingFormula stackingFormula = null,
            StatFormatter statFormatter = null,
            ItemTag itemTag = null
        )
        {
            ItemStat itemStat = new ItemStat()
            {
                nameToken = nameToken,
                value = value,
                stackValue = stackValue,
                stackingFormula = stackingFormula ?? LinearStacking,
                statFormatter = statFormatter ?? StatFormatter.Percent
            };
            return RegisterStat(itemDef, itemStat, itemTag);
        }

        public static ItemStat RegisterStat(ItemDef itemDef, ItemStat itemStat, ItemTag itemTag)
        {
            if (itemStats.TryGetValue(itemDef, out var stats))
            {
                stats.Add(itemStat);
            }
            else
            {
                itemStats[itemDef] = new List<ItemStat>() { itemStat };
            }
            if (itemTag != null) RegisterTag(itemStat, itemTag);
            return itemStat;
        }


        public static ItemModifier RegisterModifier(
            ItemTag itemTag,
            ItemDef itemDef,
            ItemModifier.ModificationFormula modificationFormula,
            float modifier,
            float? stackModifier = null,
            string nameToken = null,
            string pluralNameToken = null,
            ItemModifier.ModificationLocator modificationLocator = null,
            ItemModifier.ModificationChecker modificationChecker = null,
            ItemModifier.ModificationCounter modificationCounter = null
        )
        {
            ItemModifier itemModfier = new ItemModifier()
            {
                nameToken = nameToken ?? itemDef.nameToken,
                pluralNameToken = pluralNameToken,
                modificationFormula = modificationFormula,
                itemDef = itemDef,
                modifier = modifier,
                stackModifier = stackModifier ?? modifier,
                modificationLocator = modificationLocator,
                modificationChecker = modificationChecker ?? ItemModifier.ItemChecker,
                modificationCounter = modificationCounter ?? ItemModifier.ItemCounter
            };
            return RegisterModifier(itemTag, itemModfier);
        }

        public static ItemModifier RegisterModifier(
            ItemTag itemTag,
            string nameToken,
            ItemModifier.ModificationFormula modificationFormula,
            ItemModifier.ModificationLocator modificationLocator,
            ItemModifier.ModificationChecker modificationChecker,
            ItemModifier.ModificationCounter modificationCounter,
            string pluralNameToken = null)
        {
            ItemModifier itemModfier = new ItemModifier()
            {
                nameToken = nameToken,
                pluralNameToken = pluralNameToken,
                modificationFormula = modificationFormula,
                modificationLocator = modificationLocator,
                modificationChecker = modificationChecker,
                modificationCounter = modificationCounter
            };
            return RegisterModifier(itemTag, itemModfier);
        }

        public static ItemModifier RegisterModifier(ItemTag itemTag, ItemModifier itemModfier)
        {
            if (itemModifiers.TryGetValue(itemTag, out var modifers))
            {
                modifers.Add(itemModfier);
            }
            else
            {
                itemModifiers[itemTag] = new List<ItemModifier>() { itemModfier };
            }
            return itemModfier;
        }

        public static ItemTag RegisterTag(ItemStat itemStat, ItemTag itemTag)
        {
            if (itemTags.TryGetValue(itemStat, out var tags))
            {
                tags.Add(itemTag);
            }
            else
            {
                itemTags[itemStat] = new List<ItemTag>() { itemTag };
            }
            return itemTag;
        }

        public static void GetItemStats(StringBuilder stringBuilder, ItemDef itemDef, int stacks, CharacterMaster master)
        {
            if (itemStats.TryGetValue(itemDef, out var stats))
            {
                foreach (var itemStat in stats)
                {
                    float baseValue = itemStat.stackingFormula(itemStat.value, itemStat.stackValue, stacks);
                    float totalValue = baseValue;
                    if (itemTags.TryGetValue(itemStat, out var tags))
                    {
                        foreach (var itemTag in tags)
                        {
                            if (itemModifiers.TryGetValue(itemTag, out var modifiers))
                            {
                                foreach (var itemModifier in modifiers)
                                {
                                    if (itemModifier.itemDef == itemDef) continue;
                                    if (itemModifier.GetModificationActive(master))
                                    {
                                        float count = itemModifier.GetModificationCount(master);
                                        float modifiedValue = itemModifier.GetModifiedValue(baseValue, master, count);
                                        totalValue += modifiedValue;
                                    }
                                }
                            }
                        }
                    }

                    stringBuilder.Append("\n");
                    stringBuilder.Append(Language.GetString(itemStat.nameToken));
                    stringBuilder.Append(": ");
                    itemStat.statFormatter.FormatString(stringBuilder, totalValue, master);
                    bool first = true;
                    if (tags != null)
                    {
                        foreach (var itemTag in tags)
                        {
                            if (itemModifiers.TryGetValue(itemTag, out var modifiers))
                            {
                                foreach (var itemModifier in modifiers)
                                {
                                    if (itemModifier.GetModificationActive(master))
                                    {
                                        if (itemModifier.itemDef == itemDef) continue;
                                        if (first)
                                        {
                                            var itemCount = master.inventory.GetItemCount(itemDef);
                                            stringBuilder.Append("\n  ");
                                            stringBuilder.Append(itemCount);
                                            stringBuilder.Append(" ");
                                            stringBuilder.Append(Language.GetString(itemDef.nameToken));
                                            if (itemCount > 1)
                                            {
                                                stringBuilder.Append("s");
                                            }
                                            stringBuilder.Append(": ");
                                            itemStat.statFormatter.FormatString(stringBuilder, baseValue, master);
                                            first = false;
                                        }
                                        float count = itemModifier.GetModificationCount(master);
                                        float modifiedValue = itemModifier.GetModifiedValue(baseValue, master, count);
                                        totalValue += modifiedValue;
                                        stringBuilder.Append("\n  ");
                                        stringBuilder.Append(count);
                                        stringBuilder.Append(" ");
                                        if (Math.Abs(count) != 1)
                                        {
                                            if (!String.IsNullOrEmpty(itemModifier.pluralNameToken))
                                            {
                                                stringBuilder.Append(Language.GetString(itemModifier.pluralNameToken));
                                            }
                                            else
                                            {
                                                stringBuilder.Append(Language.GetString(itemModifier.nameToken));
                                                stringBuilder.Append("s");
                                            }
                                        }
                                        else
                                        {
                                            stringBuilder.Append(Language.GetString(itemModifier.nameToken));
                                        }
                                        stringBuilder.Append(": ");
                                        itemStat.statFormatter.FormatString(stringBuilder, modifiedValue, master);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }



        public class StatFormatter
        {
            public delegate void Formatter(StringBuilder stringBuilder, float value, CharacterMaster master);
            public Formatter statFormatter;
            public string style;
            public string color;
            public string customFormatTag;
            public string customFormatClosingTag;
            public string prefix;
            public string suffix;
            public string pluralSuffix;
            public string format = "{0:0.##}";
            public bool bold;
            public bool italic;
            public bool underline;
            public bool strikethrough;

            public static StatFormatter LuckChance = new StatFormatter()
            {
                suffix = "BETTERUI_LUCKCHANCE_SUFFIX",
                style = Styles.Damage,
                statFormatter = (sb, value, master) => {
                    sb.Append(Math.Min(100, 100 * Utils.LuckCalc(value, master.luck)).ToString("0.##"));
                }
            };

            public static StatFormatter Chance = new StatFormatter()
            {
                suffix = "BETTERUI_CHANCE_SUFFIX",
                style = Styles.Damage,
                statFormatter = (sb, value, master) => { sb.AppendFormat("{0:0.##}", value * 100); }
            };

            public static StatFormatter Gold = new StatFormatter()
            {
                prefix = "BETTERUI_GOLD_PREFIX",
                style = Styles.Damage,
            };

            public static StatFormatter Charges = new StatFormatter()
            {
            };

            public static StatFormatter Percent = new StatFormatter()
            {
                suffix = "BETTERUI_PERCENT_SUFFIX",
                style = Styles.Damage,
                statFormatter = (sb, value, master) => { sb.AppendFormat("{0:0.##}", value * 100); }
            };

            public static StatFormatter HP = new StatFormatter()
            {
                suffix = "BETTERUI_HP_SUFFIX",
                style = Styles.Health
            };

            public static StatFormatter Seconds = new StatFormatter()
            {
                suffix = "BETTERUI_SECONDS_SUFFIX",
                style = Styles.Damage
            };

            public static StatFormatter Armor = new StatFormatter()
            {
                suffix = "BETTERUI_ARMOR_SUFFIX",
                style = Styles.Stack
            };

            public static StatFormatter Regen = new StatFormatter()
            {
                suffix = "BETTERUI_REGEN_SUFFIX",
                style = Styles.Healing
            };

            public static StatFormatter Range = new StatFormatter()
            {
                suffix = "BETTERUI_RANGE_SUFFIX",
                style = Styles.Damage
            };

            public void FormatString(StringBuilder stringBuilder, float value, CharacterMaster master)
            {
                if (!String.IsNullOrEmpty(style))
                {
                    stringBuilder.Append("<style=");
                    stringBuilder.Append(style);
                    stringBuilder.Append(">");
                }
                if (bold) stringBuilder.Append("<b>");
                if (italic) stringBuilder.Append("<i>");
                if (underline) stringBuilder.Append("<u>");
                if (strikethrough) stringBuilder.Append("<s>");
                if (!String.IsNullOrEmpty(color))
                {
                    stringBuilder.Append("<color=#");
                    stringBuilder.Append(color);
                    stringBuilder.Append(">");
                }
                if (!String.IsNullOrEmpty(customFormatTag)) stringBuilder.Append(customFormatTag);
                if (!String.IsNullOrEmpty(prefix)) stringBuilder.Append(Language.GetString(prefix));

                if (statFormatter != null)
                {
                    statFormatter(stringBuilder, value, master);
                }
                else
                {
                    stringBuilder.AppendFormat("{0:0.##}", value);
                }

                if (!String.IsNullOrEmpty(suffix)) stringBuilder.Append(Language.GetString(suffix));
                if (!String.IsNullOrEmpty(customFormatClosingTag)) stringBuilder.Append(customFormatClosingTag);
                if (strikethrough) stringBuilder.Append("</s>");
                if (underline) stringBuilder.Append("</u>");
                if (italic) stringBuilder.Append("</si>");
                if (bold) stringBuilder.Append("</b>");
                if (!String.IsNullOrEmpty(style)) stringBuilder.Append("</style>");
            }

        }



        public delegate float StackingFormula(float value, float extraStackValue, int stacks);

        public static float LinearStacking(float value, float extraStackValue, int stacks)
        {
            return value + extraStackValue * (stacks - 1);
        }
        public static float ExponentialStacking(float value, float extraStackValue, int stacks)
        {
            return value * (float)Math.Pow(extraStackValue, stacks - 1);
        }
        public static float NegativeExponentialStacking(float value, float extraStackValue, int stacks)
        {
            return (1 - (1 - value) * (float)Math.Pow(1 - extraStackValue, stacks - 1));
        }
        public static float DivideByBonusStacks(float value, float extraStackValue, int stacks)
        {
            return value / (stacks + 1);
        }
        public static float DivideByStacks(float value, float extraStackValue, int stacks)
        {
            return value / stacks;
        }
        public static float FocusedConvergenceStacking(float value, float extraStackValue, int stacks)
        {
            return 1 / (value * Math.Min(stacks, extraStackValue));
        }
        public static float HyperbolicStacking(float value, float extraStackValue, int stacks)
        {
            return (float)(1 - 1 / (1 + (value + extraStackValue * (stacks - 1))));
        }
        public static float NoStacking(float value, float extraStackValue, int stacks)
        {
            return value;
        }

        public delegate int CapFormula(float value, float extraStackValue, float procCoefficient);
        public static int LinearCap(float value, float extraStackValue, float procCoefficient)
        {
            return (int)UnityEngine.Mathf.Round((1f - value * procCoefficient) / (extraStackValue * procCoefficient)) + 1;
        }

        public class ItemProcInfo
        {
            public float value;
            public float extraStackValue;
            public StatFormatter statFormatter;
            public StackingFormula stackingFormula;
            public CapFormula capFormula;

            public void GetOutputString(StringBuilder stringBuilder, int stacks, CharacterMaster master, float procCoefficient)
            {
                this.statFormatter.FormatString(stringBuilder, procCoefficient * this.stackingFormula(this.value, this.extraStackValue, stacks), master);
                if(capFormula != null)
                {
                    var stacksToCap = capFormula(value, extraStackValue, procCoefficient);
                    stringBuilder.AppendFormat(Language.GetString(stacksToCap > 1 ? "BETTERUI_PROCSTACKS_PLURAL" : "BETTERUI_PROCSTACKS_SINGULAR"), stacksToCap);
                }
            }
            public float GetValue(int stacks)
            {
                return this.stackingFormula(value, extraStackValue, stacks);
            }

            public float GetCap(float procCoefficient)
            {
                return this.capFormula == null ? -1 : this.capFormula(value, extraStackValue, procCoefficient);
            }
        }

        public static void RegisterProc(ItemIndex itemIndex, ItemProcInfo itemProcInfo)
        {
            if (itemIndex <= ItemIndex.None)
            {
                UnityEngine.Debug.LogError("ERROR: AddEffect was passed ItemIndex.None or below, this likely means you tried to register your effect before the item catalog was ready. Please use ItemCatalog.availability.CallWhenAvailable or pass an ItemDef directly.");
                return;
            }
            RegisterProc(ItemCatalog.GetItemDef(itemIndex), itemProcInfo);
        }
        public static void RegisterProc(ItemIndex itemIndex, float value, float? extraStackValue = null, StatFormatter statFormatter = null, StackingFormula stackingFormula = null, CapFormula capFormula = null)
        {
            if (itemIndex <= ItemIndex.None)
            {
                UnityEngine.Debug.LogError("ERROR: AddEffect was passed ItemIndex.None or below, this likely means you tried to register your effect before the item catalog was ready. Please use ItemCatalog.availability.CallWhenAvailable or pass an ItemDef directly.");
                return;
            }
            RegisterProc(ItemCatalog.GetItemDef(itemIndex), value, extraStackValue, statFormatter, stackingFormula, capFormula);
        }
        public static void RegisterProc(ItemDef itemDef, ItemProcInfo itemProcInfo)
        {
            itemProcInfos[itemDef] = itemProcInfo;
        }
        public static void RegisterProc(ItemDef itemDef, float value, float? extraStackValue = null, StatFormatter statFormatter = null, StackingFormula stackingFormula = null, CapFormula capFormula = null)
        {
            RegisterProc(itemDef, new ItemProcInfo()
            {
                value = value,
                extraStackValue = extraStackValue ?? value,
                statFormatter = statFormatter ?? StatFormatter.LuckChance,
                stackingFormula = stackingFormula ?? LinearStacking,
                capFormula = capFormula
            });
        }

        public class ItemStat
        {
            public string nameToken;
            public StackingFormula stackingFormula;
            public float value;
            public float stackValue;
            public StatFormatter statFormatter;
        }

        public class ItemModifier
        {
            public delegate float ModificationFormula(float value, float modifier, float stacks, float stackModifier);
            public static ModificationFormula ExponentialBonus = (value, modifier, stackModifier, stacks) => (1 - value) - (1 - value) * (1 - modifier) * (float) Math.Pow(1 - stackModifier, stacks - 1);
            public static ModificationFormula PositiveExponentialBonus = (value, modifier, stackModifier, stacks) => value * modifier * (float) Math.Pow(1 - stackModifier, stacks - 1);
            public static ModificationFormula PercentBonus = (value, modifier, stackModifier, stacks) => value * ((modifier + (stackModifier * (stacks-1))) / 100);
            public static ModificationFormula LuckBonus = (value, modifier, stackModifier, stacks) =>
            {
                return Math.Min(1, Utils.LuckCalc(value, modifier)) - value;
            };
            public static ModificationFormula AlliesBonus = (value, modifier, stackModifier, stacks) =>
            {
                return value * modifier;
            };
            public static ModificationFormula FlatBonus = (value, modifier, stackModifier, stacks) =>
            {
                return value + modifier + stackModifier * stacks;
            };

            public delegate float ModificationLocator(CharacterMaster master);
            public static ModificationLocator LuckLocator = (master) => master.luck;
            public static ModificationLocator AlliesLocator = (master) => Math.Max(TeamComponent.GetTeamMembers(master.GetBody().teamComponent.teamIndex).Count - 1, 0);

            public delegate bool ModificationChecker(CharacterMaster master, ItemModifier itemModifier);
            public static ModificationChecker ItemChecker = (master, itemModifier) => master.inventory.GetItemCount(itemModifier.itemDef) > 0;
            public static ModificationChecker TeamItemChecker = (master, itemModifier) => Util.GetItemCountForTeam(master.GetBody().teamComponent.teamIndex, itemModifier.itemDef.itemIndex, true, true) > master.inventory.GetItemCount(itemModifier.itemDef);
            public static ModificationChecker LuckChecker = (master, itemModifier) => master.luck != 0;
            public static ModificationChecker AlliesChecker = (master, itemModifier) => Math.Max(TeamComponent.GetTeamMembers(master.GetBody().teamComponent.teamIndex).Count - 1, 0) > 1;

            public delegate float ModificationCounter(CharacterMaster master, ItemModifier itemModifier);
            public static ModificationCounter ItemCounter = (master, itemModifier) => master.inventory.GetItemCount(itemModifier.itemDef);
            public static ModificationCounter TeamItemCounter = (master, itemModifier) => Util.GetItemCountForTeam(master.GetBody().teamComponent.teamIndex, itemModifier.itemDef.itemIndex, true, true) - master.inventory.GetItemCount(itemModifier.itemDef); 
            public static ModificationCounter LuckCounter = (master, itemModifier) => master.luck;
            public static ModificationCounter AlliesCounter = (master, itemModifier) => Math.Max(TeamComponent.GetTeamMembers(master.GetBody().teamComponent.teamIndex).Count - 1, 0);

            public string nameToken;
            public string pluralNameToken;
            public ModificationFormula modificationFormula;
            public ItemDef itemDef;
            public ModificationLocator modificationLocator;
            public ModificationChecker modificationChecker;
            public ModificationCounter modificationCounter;
            public float modifier;
            public float stackModifier;

            public bool GetModificationActive(CharacterMaster master)
            {
                return modificationChecker(master, this); 
            }

            public float GetModificationCount(CharacterMaster master)
            {
                return modificationCounter(master, this);
            }
            public float GetModifiedValue(float value, CharacterMaster master, float count)
            {
                if(modificationLocator != null)
                {
                    return modificationFormula(value, modificationLocator(master), stackModifier, count);
                }
                else
                {
                    return modificationFormula(value, modifier, stackModifier, count);
                }
            }
        }


        public class ItemTag
        {
            public static ItemTag Damage;
            public static ItemTag Healing;
            public static ItemTag Luck;
            public static ItemTag LuckStat;
            public static ItemTag EquipmentCooldown;
            public static ItemTag SkillCooldown;
            public static ItemTag Allies;
            public static ItemTag MaxHealth;
            public static ItemTag TitanDamage;
            public static ItemTag TitanHealth;
            public static ItemTag MovementSpeed;
            static ItemTag()
            {
                Damage = new ItemTag();
                Healing = new ItemTag();
                Luck = new ItemTag();
                LuckStat = new ItemTag();
                EquipmentCooldown = new ItemTag();
                Allies = new ItemTag();
                MaxHealth = new ItemTag();
                TitanDamage = new ItemTag();
                TitanHealth = new ItemTag();
                SkillCooldown = new ItemTag();
                MovementSpeed = new ItemTag();
            }
        }

        public static class Styles
        {
            public const string Damage = "cIsDamage";
            public const string Healing = "cIsHealing";
            public const string Mono = "cMono";
            public const string Stack = "cStack";
            public const string Health = "cIsHealth";
            public const string Void = "cIsVoid";
            public const string Death = "cDeath";
            public const string UserSetting = "cUserSetting";
            public const string Artifact = "cArtifact";
            public const string Sub = "cSub";
            public const string Event = "cEvent";
            public const string WorldEvent = "cWorldEvent";
            public const string KeywordName = "cKeywordName";
            public const string Shrine = "cShrine";
        }
    }
}
