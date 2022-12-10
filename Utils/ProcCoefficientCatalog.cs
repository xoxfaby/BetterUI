using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BetterUI
{
    public static class ProcCoefficientCatalog
    {
        public struct ProcCoefficientInfo
        {
            public string name;
            public float procCoefficient;
        }

        private static readonly Dictionary<string, List<ProcCoefficientInfo>> skills = new Dictionary<string, List<ProcCoefficientInfo>>();
        public static void AddSkill(string skillDef, List<ProcCoefficientInfo> procCoefficientInfoList)
        {
            skills[skillDef] = procCoefficientInfoList;
        }
        public static void AddSkill(string skillDef, ProcCoefficientInfo procCoefficientInfo)
        {
            skills[skillDef] = new List<ProcCoefficientInfo>() { procCoefficientInfo };
        }
        public static void AddSkill(string skillDef, string name, float procCoefficient)
        {
            AddSkill(skillDef, new ProcCoefficientInfo() { name = name, procCoefficient = procCoefficient });
        }
        public static void AddToSkill(string skillDef, ProcCoefficientInfo procCoefficientInfo)
        {
            if (skills.ContainsKey(skillDef))
            {
                skills[skillDef].Add(procCoefficientInfo);
            }
            else
            {
                AddSkill(skillDef,procCoefficientInfo);
            }  
        }
        public static void AddToSkill(string skillDef, string name, float procCoefficient)
        {
            AddToSkill(skillDef, new ProcCoefficientInfo() { name = name, procCoefficient = procCoefficient });
        }
        public static List<ProcCoefficientInfo> GetProcCoefficientInfo(string skillDef)
        {
            return skills.ContainsKey(skillDef) ? skills[skillDef] : null;
        }

        static ProcCoefficientCatalog()
        {
            // Acrid
            AddSkill("CrocoSlash", "SKILL_SLASH_NAME", 1);
            AddSkill("CrocoSpit", "SKILL_SPIT_NAME", 1);
            AddSkill("CrocoBite", "SKILL_BITE_NAME", 1);
            AddSkill("CrocoLeap", "SKILL_LEAP_NAME", 1);
            AddToSkill("CrocoLeap", "SKILL_POOL_NAME", 0.1f);
            AddSkill("CrocoChainableLeap", "SKILL_LEAP_NAME", 1);
            AddSkill("CrocoDisease", "SKILL_BOUNCE_NAME", 1);

            // Artificer
            AddSkill("MageBodyFireFirebolt", "SKILL_BOLT_NAME", 1);
            AddSkill("MageBodyFireLightningBolt", "SKILL_BOLT_NAME", 1);
            AddSkill("MageBodyNovaBomb", "SKILL_EXPLOSION_NAME", 1);
            AddToSkill("MageBodyNovaBomb", "SKILL_SPARKS_NAME", 0.3f);
            AddSkill("MageBodyIceBomb", "SKILL_SPEAR_NAME", 1);
            AddSkill("MageBodyWall", "SKILL_WALL_NAME", 1);
            AddSkill("MageBodyFlamethrower", "SKILL_FLAMETHROWER_NAME", 1);
            AddSkill("MageBodyFlyUp", "SKILL_SURGE_NAME", 1);

            // Bandit
            AddSkill("FireShotgun2", "SKILL_BURST_NAME", 0.5f);
            AddSkill("Bandit2Blast", "SKILL_BLAST_NAME", 1);
            AddSkill("SlashBlade", "SKILL_SLASH_NAME", 1);
            AddSkill("Bandit2SerratedShivs", "SKILL_BLADE_NAME", 1);
            AddSkill("ResetRevolver", "SKILL_SHOT_NAME", 1);
            AddSkill("SkullRevolver", "SKILL_SHOT_NAME", 1);
            AddSkill("ThrowSmokebomb", "SKILL_SMOKEBOMB_NAME", 1);

            // Captain
            AddSkill("CaptainShotgun", "SKILL_SHOTGUN_NAME", 0.75f);
            AddSkill("CaptainTazer", "SKILL_POWERTAZER_NAME", 1);
            AddSkill("CallAirstrike", "SKILL_AIRSTRIKE_NAME", 1);
            AddSkill("CallAirstrikeAlt", "SKILL_AIRSTRIKE_NAME", 1);
            AddSkill("PrepSupplyDrop", "SKILL_IMPACT_NAME", 0);

            // Commando
            AddSkill("CommandoBodyFirePistol", "SKILL_BULLET_NAME", 1);
            AddSkill("CommandoBodyFireFMJ", "SKILL_PHASEROUND_NAME", 1);
            AddSkill("CommandoBodyFireShotgunBlast", "SKILL_SHOTGUN_NAME", 0.5f);
            AddSkill("CommandoBodyBarrage", "SKILL_BULLET_NAME", 1);
            AddSkill("ThrowGrenade", "SKILL_GRENADE_NAME", 1);

            // Engineer
            AddSkill("EngiBodyFireGrenade", "SKILL_GRENADE_NAME", 1);
            AddSkill("EngiBodyPlaceMine", "SKILL_MINE_NAME", 1);
            AddSkill("EngiBodyPlaceSpiderMine", "SKILL_MINE_NAME", 1);
            AddSkill("EngiHarpoons", "SKILL_HARPOON_NAME", 1);
            AddSkill("EngiBodyPlaceTurret", "SKILL_TURRETSHOT_NAME", 1);
            AddSkill("EngiBodyPlaceWalkerTurret", "SKILL_LASER_NAME", 0.6f);

            // Huntress
            AddSkill("HuntressBodyFireSeekingArrow", "SKILL_ARROW_NAME", 1);
            AddSkill("FireFlurrySeekingArrow", "SKILL_ARROW_NAME", 0.7f);
            AddSkill("HuntressBodyGlaive", "SKILL_GLAIVE_NAME", 0.8f);
            AddSkill("HuntressBodyArrowRain", "SKILL_RAIN_NAME", 0.2f);
            AddSkill("AimArrowSnipe", "SKILL_BALLISTA_NAME", 1);

            // Loader
            AddSkill("SwingFist", "SKILL_FIST_NAME", 1);
            AddSkill("FireYankHook", "SKILL_FIST_NAME", 1);
            AddSkill("BigPunch", "SKILL_FIST_NAME", 1);
            AddSkill("ChargeZapFist", "SKILL_FIST_NAME", 1);
            AddSkill("ThrowPylon", "SKILL_ZAP_NAME", 0.5f);
            AddSkill("GroundSlam", "SKILL_SLAM_NAME", 1);

            // Mercenary
            AddSkill("MercGroundLight2", "SKILL_SWORD_NAME", 1);
            AddSkill("MercBodyWhirlwind", "SKILL_SLICE_NAME", 1);
            AddSkill("MercBodyUppercut", "SKILL_SLICE_NAME", 1);
            AddSkill("MercBodyAssaulter", "SKILL_DASH_NAME", 1);
            AddSkill("MercBodyFocusedAssault", "SKILL_DASH_NAME", 1);
            AddSkill("MercBodyEvis", "SKILL_HIT_NAME", 1);
            AddSkill("MercBodyEvisProjectile", "SKILL_BLADE_NAME", 1);

            // MUL-T
            AddSkill("ToolbotBodyFireNailgun", "SKILL_NAIL_NAME", 0.6f);
            AddSkill("ToolbotBodyFireSpear", "SKILL_REBAR_NAME", 1);
            AddSkill("ToolbotBodyFireGrenadeLauncher", "SKILL_ROCKET_NAME", 1);
            AddSkill("ToolbotBodyFireBuzzsaw", "SKILL_SAW_NAME", 1);
            AddSkill("ToolbotBodyStunDrone", "SKILL_CANISTER_NAME", 1);
            AddToSkill("ToolbotBodyStunDrone", "SKILL_BOMBLET_NAME", 0.3f);
            AddSkill("ToolbotBodyToolbotDash", "SKILL_CHARGE_NAME", 1);
            AddToSkill("ToolbotBodyToolbotDash", "SKILL_RAM_NAME", 0);

            // REX
            AddSkill("TreebotBodyFireSyringe", "SKILL_SYRINGES_NAME", 0.5f);
            AddSkill("TreebotBodyAimMortar2", "SKILL_MORTAR_NAME", 1);
            AddSkill("TreebotBodyAimMortarRain", "SKILL_HIT_NAME", 0.5f);
            AddSkill("TreebotBodySonicBoom", "SKILL_BOOM_NAME", 0);
            AddSkill("TreebotBodyPlantSonicBoom", "SKILL_BOOM_NAME", 0.5f);
            AddSkill("TreebotBodyFireFlower2", "SKILL_PROJECTILE_NAME", 1);
            AddToSkill("TreebotBodyFireFlower2", "SKILL_ROOTS_NAME", 0);
            AddSkill("TreebotBodyFireFruitSeed", "SKILL_PROJECTILE_NAME", 1);

            // Railgunner
            AddSkill("RailgunnerBodyFirePistol", "SKILL_PROJECTILE_NAME", 1f);
            AddSkill("RailgunnerBodyScopeHeavy", "SKILL_PROJECTILE_NAME", 1f);
            AddSkill("RailgunnerBodyFireSnipeHeavy", "SKILL_PROJECTILE_NAME", 1f);
            AddSkill("RailgunnerBodyScopeLight", "SKILL_PROJECTILE_NAME", 1f);
            AddSkill("RailgunnerBodyFireSnipeLight", "SKILL_PROJECTILE_NAME", 1f);
            AddSkill("RailgunnerBodyFireMineBlinding", "SKILL_MINE_NAME", 0f);
            AddSkill("RailgunnerBodyFireMineConcussive", "SKILL_MINE_NAME", 0f);
            AddSkill("RailgunnerBodyChargeSnipeCryo", "SKILL_PROJECTILE_NAME", 1.5f);
            AddSkill("RailgunnerBodyFireSnipeCryo", "SKILL_PROJECTILE_NAME", 1.5f);
            AddSkill("RailgunnerBodyChargeSnipeSuper", "SKILL_PROJECTILE_NAME", 3f);
            AddSkill("RailgunnerBodyFireSnipeSuper", "SKILL_PROJECTILE_NAME", 3f);

            // Void Fiend
            AddSkill("FireHandBeam", "SKILL_BEAM_NAME", 1f);
            AddSkill("FireCorruptBeam", "SKILL_BEAM_NAME", 0.625f);
            AddSkill("ChargeMegaBlaster", "SKILL_FLOOD_NAME", 1);
            AddSkill("FireCorruptDisk", "SKILL_FLOOD_NAME", 1);
            AddSkill("CrushCorruption", "SKILL_CRUSH_NAME", 0f);
            AddSkill("CrushHealth", "SKILL_CRUSH_NAME", 0f);

            // Items
            AddSkill("LunarPrimaryReplacement", "SKILL_SHARD_NAME", 0.1f);
            AddToSkill("LunarPrimaryReplacement", "SKILL_EXPLOSION_NAME", 1);
            AddSkill("LunarSecondaryReplacement", "SKILL_MAELSTROM_NAME", 0.2f);
            AddToSkill("LunarSecondaryReplacement", "SKILL_EXPLOSION_NAME", 1);
            AddSkill("LunarDetonatorSpecialReplacement", "SKILL_DETONATION_NAME", 1);
        }
    }
}
