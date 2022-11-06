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

            RegisterStat(RoR2Content.Items.AlienHead, "Cooldown Reduction", 0.25f, NegativeExponentialStacking, StatFormatter.Percent, itemTag:ItemTag.SkillCooldown);
            RegisterModifier(ItemTag.SkillCooldown, RoR2Content.Items.AlienHead, ItemModifier.ExponentialBonus, 0.25f);
            RegisterStat(RoR2Content.Items.ArmorPlate, "Armor", 5f, LinearStacking, StatFormatter.Armor);
            RegisterStat(RoR2Content.Items.ArmorReductionOnHit, "Duration", 8, LinearStacking , StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.AttackSpeedOnCrit, "Maximum Attack Speed", 0.36f, 0.24f , LinearStacking, StatFormatter.Percent);
            RegisterStat(RoR2Content.Items.AutoCastEquipment, "Cooldown Reduction", 0.5f, 0.15f, NegativeExponentialStacking, StatFormatter.Percent, ItemTag.EquipmentCooldown );
            RegisterModifier(ItemTag.EquipmentCooldown, RoR2Content.Items.AutoCastEquipment, ItemModifier.ExponentialBonus, 0.5f, 0.15f);
            RegisterStat(RoR2Content.Items.Bandolier, "Drop Chance", 0.18f, 0.10f, itemTag: ItemTag.Luck);
            RegisterStat(RoR2Content.Items.BarrierOnKill, "Barrier", 15, statFormatter: StatFormatter.HP );
            RegisterStat(RoR2Content.Items.BarrierOnOverHeal, "Healing Converted", 0.5f);
            RegisterStat(RoR2Content.Items.Bear, "Tougher Times", 0.15f, HyperbolicStacking);
            RegisterStat(RoR2Content.Items.BeetleGland, "Beetle Guards", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.Behemoth, "Explosion Radius", 4, 2.5f , statFormatter: StatFormatter.Range );
            RegisterProc(RoR2Content.Items.Behemoth, 4f, 2.5f, statFormatter: StatFormatter.Range, stackingFormula: LinearStacking);
            RegisterStat(RoR2Content.Items.BleedOnHit, "Bleed Chance", 0.1f, LinearStacking, StatFormatter.Chance, ItemTag.Luck);
            RegisterProc(RoR2Content.Items.BleedOnHit, 0.1f, capFormula: LinearCap);
            RegisterStat(RoR2Content.Items.BleedOnHitAndExplode, "Explosion Base Damage", 4);
            RegisterStat(RoR2Content.Items.BleedOnHitAndExplode, "Explosion Max HP Damage", 0.15f);
            RegisterStat(RoR2Content.Items.BonusGoldPackOnKill, "Drop Chance", 0.04f, itemTag: ItemTag.Luck);
            RegisterStat(RoR2Content.Items.BossDamageBonus, "Damage", 0.2f);
            RegisterStat(RoR2Content.Items.BounceNearby, "Chance", 0.25f, HyperbolicStacking, itemTag: ItemTag.Luck);
            RegisterStat(RoR2Content.Items.BounceNearby, "Targets", 10, 5 , statFormatter: StatFormatter.Charges);
            RegisterProc(RoR2Content.Items.BounceNearby, 0.2f, stackingFormula: HyperbolicStacking);
            RegisterStat(RoR2Content.Items.CaptainDefenseMatrix, "Projectiles Shot", 1, statFormatter: StatFormatter.Charges );
            RegisterStat(RoR2Content.Items.ChainLightning, "Targets", 3, 2, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.ChainLightning, "Radius", 20, 2, statFormatter: StatFormatter.Range);
            RegisterProc(RoR2Content.Items.ChainLightning, 0.25f, stackingFormula: NoStacking);
            RegisterStat(RoR2Content.Items.Clover, "Luck", 1, statFormatter: StatFormatter.Charges, itemTag: ItemTag.LuckStat);
            RegisterStat(RoR2Content.Items.CritGlasses, "Chance", 0.10f);
            RegisterStat(RoR2Content.Items.Crowbar, "Damage", 0.75f);
            RegisterStat(RoR2Content.Items.Dagger, "Damage", 1.5f);
            RegisterStat(RoR2Content.Items.DeathMark, "Debuff Duration", 7, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.EnergizedOnEquipmentUse, "Duration", 8, 4 , statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.EquipmentMagazine, "Equipment Charges", 1f, LinearStacking, StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.EquipmentMagazine, "Cooldown Reduction", 0.15f, 0.15f, NegativeExponentialStacking, StatFormatter.Percent, ItemTag.EquipmentCooldown);
            RegisterModifier(ItemTag.EquipmentCooldown, RoR2Content.Items.EquipmentMagazine, ItemModifier.ExponentialBonus, 0.15f);
            RegisterStat(RoR2Content.Items.ExecuteLowHealthElite, "Threshold", 0.13f , HyperbolicStacking );
            RegisterStat(RoR2Content.Items.ExplodeOnDeath, "Damage", 3.5f, 2.8f, LinearStacking );
            RegisterStat(RoR2Content.Items.ExplodeOnDeath, "Radius", 12, 2.4f, statFormatter:StatFormatter.Range );
            RegisterStat(RoR2Content.Items.ExtraLife, "Uses", 1, statFormatter:StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.FallBoots, "Cooldown", 10, 0.5f, ExponentialStacking, statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.Feather, "Extra Jumps", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.FireRing, "Damage", 3 );
            RegisterStat(RoR2Content.Items.FireballsOnHit, "Damage", 3 );
            RegisterStat(RoR2Content.Items.Firework, "Fireworks", 8, 4 , statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.FlatHealth, "Health", 25, statFormatter:StatFormatter.HP );
            RegisterStat(RoR2Content.Items.FocusConvergence, "Charge Speed", 0.30f);
            RegisterStat(RoR2Content.Items.FocusConvergence, "Teleporter Zone", 2, 3, FocusedConvergenceStacking);
            RegisterStat(RoR2Content.Items.GhostOnKill, "Ghost Duration", 30 , statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.GoldOnHit, "Gold Gained", 2, statFormatter:StatFormatter.Charges );
            RegisterStat(RoR2Content.Items.GoldOnHit, "Gold Lost", 1 );
            RegisterProc(RoR2Content.Items.GoldOnHit, 0.3f, stackingFormula: NoStacking);
            RegisterStat(RoR2Content.Items.HeadHunter, "Duration", 8, 5, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.HealOnCrit, "Heal", 8, 4, statFormatter: StatFormatter.HP);
            RegisterProc(RoR2Content.Items.HealOnCrit, 8f, statFormatter: StatFormatter.HP, stackingFormula: LinearStacking);
            RegisterStat(RoR2Content.Items.HealWhileSafe, "Regen", 3, statFormatter:StatFormatter.Regen );
            RegisterStat(RoR2Content.Items.Hoof, "Movement Speed", 0.14f, itemTag: ItemTag.MovementSpeed);
            RegisterStat(RoR2Content.Items.IceRing, "Slow Duration", 3, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.IceRing, "Damage", 2.5f );
            RegisterStat(RoR2Content.Items.Icicle, "Maximum Radius", 18, 12, statFormatter:StatFormatter.Range);
            RegisterStat(RoR2Content.Items.IgniteOnKill, "Radius", 12, 4, statFormatter: StatFormatter.Range);
            RegisterStat(RoR2Content.Items.IgniteOnKill, "Damage", 1.5f, 0.75f );
            RegisterStat(RoR2Content.Items.IncreaseHealing, "Healing", 1 );
            RegisterModifier(ItemTag.Healing, RoR2Content.Items.IncreaseHealing, ItemModifier.PercentBonus, 100);
            RegisterStat(RoR2Content.Items.Infusion, "Maximum Health", 100, statFormatter:StatFormatter.HP, itemTag: ItemTag.MaxHealth);
            RegisterStat(RoR2Content.Items.Infusion, "Health Per Kill", 1, statFormatter:StatFormatter.HP );
            RegisterStat(RoR2Content.Items.JumpBoost, "Boost", 10, statFormatter:StatFormatter.Range );
            RegisterStat(RoR2Content.Items.KillEliteFrenzy, "Duration", 4,statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.Knurl, "Health", 40f, LinearStacking, StatFormatter.HP);
            RegisterStat(RoR2Content.Items.Knurl, "Regen", 1.6f, LinearStacking, StatFormatter.Regen, ItemTag.Healing);
            RegisterStat(RoR2Content.Items.LaserTurbine, "Damage", 3 );
            RegisterStat(RoR2Content.Items.LaserTurbine, "Explosion", 10 );
            RegisterStat(RoR2Content.Items.LightningStrikeOnHit, "Damage", 5 );
            RegisterStat(RoR2Content.Items.LunarBadLuck, "Luck", -1, statFormatter:StatFormatter.Charges, itemTag: ItemTag.LuckStat);
            RegisterStat(RoR2Content.Items.LunarDagger, "Damage", 2, ExponentialStacking);
            RegisterModifier(ItemTag.Damage, RoR2Content.Items.LunarDagger, ItemModifier.PositiveExponentialBonus, 2);
            RegisterStat(RoR2Content.Items.LunarDagger, "Health", 0.5f, ExponentialStacking);
            RegisterStat(RoR2Content.Items.LunarPrimaryReplacement, "Charges", 12, statFormatter:StatFormatter.Charges );
            RegisterStat(RoR2Content.Items.LunarPrimaryReplacement, "Reload", 2, statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.LunarSecondaryReplacement, "Root Duration", 3, statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.LunarSecondaryReplacement, "Cooldown", 5, statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.LunarSpecialReplacement, "Ruin Duration", 10, statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.LunarSpecialReplacement, "Cooldown", 8, statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.LunarUtilityReplacement, "Heal", 0.182f);
            RegisterStat(RoR2Content.Items.LunarUtilityReplacement, "Duration", 3, statFormatter: StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.Medkit, "Heal", 0.05f);
            RegisterStat(RoR2Content.Items.Missile, "Damage",3 );
            RegisterProc(RoR2Content.Items.Missile, 0.1f, stackingFormula: NoStacking);
            RegisterStat(RoR2Content.Items.MonstersOnShrineUse, "Enemy Difficulty", 1f );
            RegisterStat(RoR2Content.Items.Mushroom, "Health per Second", 0.045f, 0.0225f);
            RegisterStat(RoR2Content.Items.Mushroom, "Radius", 3, 1.5f, statFormatter:StatFormatter.Range );
            RegisterStat(RoR2Content.Items.NearbyDamageBonus, "Damage", 0.2f);
            RegisterStat(RoR2Content.Items.NovaOnHeal, "Damage", 1 );
            RegisterStat(RoR2Content.Items.NovaOnLowHealth, "Recharge Speed", 30, DivideByBonusStacks, statFormatter:StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.ParentEgg, "Heath", 15, statFormatter:StatFormatter.HP );
            RegisterStat(RoR2Content.Items.Pearl, "Health", 0.1f );
            RegisterModifier(ItemTag.MaxHealth, RoR2Content.Items.Pearl, ItemModifier.PercentBonus, 10);
            RegisterStat(RoR2Content.Items.PersonalShield, "Shield", 0.08f );
            RegisterStat(RoR2Content.Items.Phasing, "Cooldown", 30, 0.5f, ExponentialStacking, statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.Plant, "Radius", 5, statFormatter:StatFormatter.Range );
            RegisterStat(RoR2Content.Items.RandomDamageZone, "Range", 16, 1.5f, ExponentialStacking, statFormatter:StatFormatter.Range);
            RegisterStat(RoR2Content.Items.RepeatHeal, "Healing", 1, itemTag:ItemTag.Healing );
            RegisterStat(RoR2Content.Items.RepeatHeal, "Maximum", 0.1f, 0.5f, ExponentialStacking, itemTag:ItemTag.Healing );
            RegisterModifier(ItemTag.Healing, RoR2Content.Items.RepeatHeal, ItemModifier.PercentBonus, 100);
            RegisterStat(RoR2Content.Items.RoboBallBuddy, "Damage", 1f, itemTag:ItemTag.Allies);
            RegisterStat(RoR2Content.Items.SecondarySkillMagazine, "Charges", 1, statFormatter:StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.Seed, "Heal", 1, statFormatter:StatFormatter.HP, itemTag:ItemTag.Healing );
            RegisterProc(RoR2Content.Items.Seed, 1f, statFormatter: StatFormatter.HP, stackingFormula: LinearStacking);
            RegisterStat(RoR2Content.Items.ShieldOnly, "Maximum Health", 0.5f, 0.25f);
            RegisterModifier(ItemTag.MaxHealth, RoR2Content.Items.ShieldOnly, ItemModifier.PercentBonus, 50, 25);
            RegisterStat(RoR2Content.Items.ShinyPearl, "Health", 0.1f);
            RegisterModifier(ItemTag.MaxHealth, RoR2Content.Items.ShinyPearl, ItemModifier.PercentBonus, 10);
            RegisterStat(RoR2Content.Items.ShinyPearl, "Regen", 0.1f, statFormatter:StatFormatter.Regen, itemTag:ItemTag.Healing);
            RegisterStat(RoR2Content.Items.ShinyPearl, "Movement Speed", 0.1f, itemTag: ItemTag.MovementSpeed);
            RegisterStat(RoR2Content.Items.ShinyPearl, "Damage", 0.1f);
            RegisterModifier(ItemTag.Damage, RoR2Content.Items.ShinyPearl, ItemModifier.PercentBonus, 10);
            RegisterStat(RoR2Content.Items.ShinyPearl, "Attack Speed", 0.1f);
            RegisterStat(RoR2Content.Items.ShinyPearl, "Crit Chance", 0.1f);
            RegisterStat(RoR2Content.Items.ShinyPearl, "Armor", 0.1f);
            RegisterStat(RoR2Content.Items.ShockNearby, "Targets", 3, 2 ,statFormatter:StatFormatter.Charges );
            RegisterStat(RoR2Content.Items.SiphonOnLowHealth, "Tethered Enemies", 1, statFormatter: StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.SlowOnHit, "Slow Duration", 2, 2, statFormatter:StatFormatter.Seconds);
            RegisterStat(RoR2Content.Items.SprintArmor, "Armor", 30, statFormatter:StatFormatter.Armor );
            RegisterStat(RoR2Content.Items.SprintBonus, "Sprint Speed", 0.25f );
            RegisterStat(RoR2Content.Items.SprintOutOfCombat, "Movement Speed", 0.3f, itemTag: ItemTag.MovementSpeed);
            RegisterStat(RoR2Content.Items.SprintWisp, "Damage", 3f, itemTag:ItemTag.Damage );
            RegisterStat(RoR2Content.Items.Squid, "Attack Speed", 1f );
            RegisterStat(RoR2Content.Items.StickyBomb, "Chance", 0.05f, itemTag:ItemTag.Luck );
            RegisterProc(RoR2Content.Items.StickyBomb, 0.05f, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterStat(RoR2Content.Items.StunChanceOnHit, "Chance", 0.05f, HyperbolicStacking , itemTag: ItemTag.Luck);
            RegisterProc(RoR2Content.Items.StunChanceOnHit, 0.05f, stackingFormula: HyperbolicStacking);
            RegisterStat(RoR2Content.Items.Syringe, "Attack Speed", 0.15f );
            RegisterStat(RoR2Content.Items.TPHealingNova, "Healing Nova", 1, statFormatter:StatFormatter.Charges );
            RegisterStat(RoR2Content.Items.Talisman, "Cooldown Reduction", 4, 2 ,statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.Thorns, "Targets", 5, 2, statFormatter:StatFormatter.Charges);
            RegisterStat(RoR2Content.Items.Thorns, "Radius", 25, 10, statFormatter:StatFormatter.Range);
            RegisterStat(RoR2Content.Items.TitanGoldDuringTP, "Damage", 1, 0.5f );
            RegisterStat(RoR2Content.Items.TitanGoldDuringTP, "Health", 1);
            RegisterModifier(ItemTag.TitanDamage, RoR2Content.Items.TitanGoldDuringTP, ItemModifier.FlatBonus, 0.5f, modificationChecker: ItemModifier.TeamItemChecker, modificationCounter:ItemModifier.TeamItemCounter);
            RegisterModifier(ItemTag.TitanHealth, RoR2Content.Items.TitanGoldDuringTP, ItemModifier.FlatBonus, 1f, modificationChecker: ItemModifier.TeamItemChecker, modificationCounter: ItemModifier.TeamItemCounter);
            RegisterStat(RoR2Content.Items.Tooth, "Health per Orb", 0.02f, itemTag:ItemTag.Healing );
            RegisterStat(RoR2Content.Items.UtilitySkillMagazine, "Charges", 2,statFormatter:StatFormatter.Charges );
            RegisterStat(RoR2Content.Items.WarCryOnMultiKill, "Frenzy Duration", 6, 4, statFormatter:StatFormatter.Seconds );
            RegisterStat(RoR2Content.Items.WardOnLevel, "Radius", 16, 8 , statFormatter:StatFormatter.Range );

            RegisterStat(DLC1Content.Items.AttackSpeedAndMoveSpeed, "Attack Speed", 0.075f);
            RegisterStat(DLC1Content.Items.AttackSpeedAndMoveSpeed, "Movement Speed", 0.07f, itemTag: ItemTag.MovementSpeed);
            RegisterStat(DLC1Content.Items.BearVoid, "Recharge Time ", 15, 0.9f, ExponentialStacking, StatFormatter.Seconds );
            RegisterStat(DLC1Content.Items.BleedOnHitVoid, "Chance", 0.1f, itemTag: ItemTag.Luck);
            RegisterProc(DLC1Content.Items.BleedOnHitVoid, 0.1f, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterStat(DLC1Content.Items.ChainLightningVoid, "Hits", 3, statFormatter:StatFormatter.Charges );
            RegisterProc(DLC1Content.Items.ChainLightningVoid, 0.25f, stackingFormula: NoStacking );
            RegisterStat(DLC1Content.Items.CloverVoid, "Items upgraded", 3, statFormatter:StatFormatter.Charges );
            RegisterStat(DLC1Content.Items.CritDamage, "Damage", 1f );
            RegisterStat(DLC1Content.Items.CritGlassesVoid, "Chance", 0.005f, itemTag: ItemTag.Luck);
            RegisterProc(DLC1Content.Items.CritGlassesVoid, 0.005f, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterStat(DLC1Content.Items.DroneWeapons, "Attack Speed", 0.5f);
            RegisterStat(DLC1Content.Items.ElementalRingVoid, "Damage", 1f );
            RegisterStat(DLC1Content.Items.EquipmentMagazineVoid, "Charges", 1f, statFormatter: StatFormatter.Charges );
            RegisterStat(DLC1Content.Items.ExplodeOnDeathVoid, "Damage", 2.6f, 1.56f, LinearStacking);
            RegisterStat(DLC1Content.Items.ExplodeOnDeathVoid, "Radius", 12, 2.4f, statFormatter: StatFormatter.Range);
            RegisterStat(DLC1Content.Items.ExtraLifeVoid, "Uses", 1f, statFormatter:StatFormatter.Charges );
            RegisterStat(DLC1Content.Items.FragileDamageBonus, "Damage", 0.2f );
            RegisterModifier(ItemTag.Damage, DLC1Content.Items.FragileDamageBonus, ItemModifier.PercentBonus, 20);
//            RegisterStat(DLC1Content.Items.FreeChest, "Shipping Request Form";
            RegisterStat(DLC1Content.Items.GoldOnHurt, "Base Gold", 3, statFormatter:StatFormatter.Gold );
            RegisterStat(DLC1Content.Items.HalfAttackSpeedHalfCooldowns, "Skill Cooldowns", 0.5f, ExponentialStacking, itemTag: ItemTag.SkillCooldown);
            RegisterModifier(ItemTag.SkillCooldown, DLC1Content.Items.HalfAttackSpeedHalfCooldowns, ItemModifier.ExponentialBonus, 0.5f);
            RegisterStat(DLC1Content.Items.HalfAttackSpeedHalfCooldowns, "Attack Speed", 1, DivideByBonusStacks);
            RegisterStat(DLC1Content.Items.HalfSpeedDoubleHealth, "Max Health", 1 );
            RegisterModifier(ItemTag.MaxHealth, DLC1Content.Items.HalfSpeedDoubleHealth, ItemModifier.PercentBonus, 100);
            RegisterStat(DLC1Content.Items.HalfSpeedDoubleHealth, "Movement Speed", 0.5f, HyperbolicStacking);
            RegisterModifier(ItemTag.MovementSpeed, DLC1Content.Items.HalfSpeedDoubleHealth, ItemModifier.ExponentialBonus, 0.5f);
            RegisterStat(DLC1Content.Items.HealingPotion, "Uses", 1f, statFormatter:StatFormatter.Charges );
            RegisterStat(DLC1Content.Items.ImmuneToDebuff, "Max Health", 100, statFormatter:StatFormatter.HP, itemTag:ItemTag.MaxHealth);
            RegisterStat(DLC1Content.Items.LunarSun, "Charge time", 3f, DivideByStacks, statFormatter: StatFormatter.Seconds );
            RegisterStat(DLC1Content.Items.LunarSun, "Bombs", 3f, 1f, statFormatter:StatFormatter.Charges );
            RegisterStat(DLC1Content.Items.MinorConstructOnKill, "Max Constructs", 4, statFormatter:StatFormatter.Charges );
            RegisterStat(DLC1Content.Items.MissileVoid, "Damage", 0.4f );
            RegisterStat(DLC1Content.Items.MoreMissile, "Damage", 0f, 0.5f );
            RegisterStat(DLC1Content.Items.MoveSpeedOnKill, "Duration", 1, 0.5f, statFormatter:StatFormatter.Seconds);
            RegisterStat(DLC1Content.Items.MushroomVoid, "HP per second", 0.02f );
            RegisterStat(DLC1Content.Items.OutOfCombatArmor, "Armor", 100, statFormatter:StatFormatter.Armor );
            RegisterStat(DLC1Content.Items.PermanentDebuffOnHit, "Armor Reduction", 2, statFormatter:StatFormatter.Armor );
            RegisterStat(DLC1Content.Items.PrimarySkillShuriken, "Damage", 4, 1);
            RegisterStat(DLC1Content.Items.PrimarySkillShuriken, "Shuriken", 3, 1, statFormatter:StatFormatter.Charges);
            RegisterStat(DLC1Content.Items.RandomEquipmentTrigger, "Effects", 1, statFormatter:StatFormatter.Charges );
            RegisterStat(DLC1Content.Items.RandomlyLunar, "Chance", 0.05f, itemTag: ItemTag.Luck);
            RegisterStat(DLC1Content.Items.RegeneratingScrap, "Uses", 1, statFormatter:StatFormatter.Charges );
            RegisterStat(DLC1Content.Items.SlowOnHitVoid, "Chance", 0.05f, itemTag: ItemTag.Luck );
            RegisterStat(DLC1Content.Items.SlowOnHitVoid, "Duration", 1, statFormatter:StatFormatter.Seconds );
            RegisterProc(DLC1Content.Items.SlowOnHitVoid, 0.05f, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterStat(DLC1Content.Items.StrengthenBurn, "Damage", 3f );
            //RegisterStat(DLC1Content.Items.TreasureCacheVoid, "Encrusted Key", , , );
            RegisterStat(DLC1Content.Items.VoidMegaCrabItem, "Cooldown", 60, 0.5f, ExponentialStacking, statFormatter:StatFormatter.Seconds);
            RegisterStat(DLC1Content.Items.VoidMegaCrabItem, "Max allies", 1, statFormatter:StatFormatter.Charges);


            RegisterModifier(ItemTag.Luck, "Luck", ItemModifier.LuckBonus, ItemModifier.LuckLocator, ItemModifier.LuckChecker, ItemModifier.LuckCounter, "Luck");
            RegisterModifier(ItemTag.Allies, "Ally", ItemModifier.AlliesBonus, ItemModifier.AlliesLocator, ItemModifier.AlliesChecker, ItemModifier.AlliesCounter, "Allies");
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
                statFormatter = statFormatter ?? StatFormatter.Chance
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
                suffix = "%",
                style = Styles.Damage,
                statFormatter = (sb, value, master) => {
                    sb.Append(Math.Min(100, 100 * Utils.LuckCalc(value, master.luck)).ToString("0.##"));
                }
            };

            public static StatFormatter Chance = new StatFormatter()
            {
                suffix = "%",
                style = Styles.Damage,
                statFormatter = (sb, value, master) => { sb.AppendFormat("{0:0.##}", value * 100); }
            };

            public static StatFormatter Gold = new StatFormatter()
            {
                prefix = "$",
                style = Styles.Damage,
            };

            public static StatFormatter Charges = new StatFormatter()
            {
            };

            public static StatFormatter Percent = new StatFormatter()
            {
                suffix = "%",
                style = Styles.Damage,
                statFormatter = (sb, value, master) => { sb.AppendFormat("{0:0.##}", value * 100); }
            };

            public static StatFormatter HP = new StatFormatter()
            {
                suffix = " HP",
                style = Styles.Health
            }; 
            
            public static StatFormatter Seconds = new StatFormatter()
            {
                suffix = "s",
                style = Styles.Damage
            };

            public static StatFormatter Armor = new StatFormatter()
            {
                suffix = " Armor",
                style = Styles.Stack
            };

            public static StatFormatter Regen = new StatFormatter()
            {
                suffix = " HP/s",
                style = Styles.Healing
            };

            public static StatFormatter Range = new StatFormatter()
            {
                suffix = "m",
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
                if (!String.IsNullOrEmpty(prefix)) stringBuilder.Append(prefix);

                if (statFormatter != null)
                {
                    statFormatter(stringBuilder, value, master);
                }
                else
                {
                    stringBuilder.AppendFormat("{0:0.##}", value);
                }

                if (!String.IsNullOrEmpty(suffix)) stringBuilder.Append(suffix);
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
                    stringBuilder.Append(" <style=cStack>(");
                    stringBuilder.Append(capFormula(value, extraStackValue, procCoefficient));
                    stringBuilder.Append(" stacks to cap)</style>");
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
