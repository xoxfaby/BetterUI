using System;
using System.Collections.Generic;
using System.Text;

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
            //Acrid
            AddSkill("CrocoSlash", "Slash", 1);
            AddSkill("CrocoSpit", "Spit", 1);
            AddSkill("CrocoBite", "Bite", 1);
            AddSkill("CrocoLeap", "Leap", 1);
            AddToSkill("CrocoLeap", "Pool", 0.1f);
            AddSkill("CrocoChainableLeap", "Leap", 1);
            AddSkill("CrocoDisease", "Bounce", 1);

            //Artificier
            AddSkill("MageBodyFireFirebolt", "Bolt", 1);
            AddSkill("MageBodyFireLightningBolt", "Bolt", 1);
            AddSkill("MageBodyNovaBomb", "Explosion", 1);
            AddToSkill("MageBodyNovaBomb", "Sparks", 0.3f);
            AddSkill("MageBodyIceBomb", "Spear", 1);
            AddSkill("MageBodyWall", "Wall", 1);
            AddSkill("MageBodyFlamethrower", "Flame Thrower", 1);
            AddSkill("MageBodyFlyUp", "Surge", 1);

            //Captain
            AddSkill("CaptainShotgun", "Shotgun", 0.75f);
            AddSkill("CaptainTazer", "Power Tazer", 1);
            AddSkill("PrepAirstrike", "Airstrike", 1);
            AddSkill("PrepSupplyDrop", "Impact", 0);

            //Commando
            AddSkill("CommandoBodyFirePistol", "Bullet", 1);
            AddSkill("CommandoBodyFireFMJ", "Phase Round", 1);
            AddSkill("CommandoBodyFireShotgunBlast", "Shotgun", 0.5f);
            AddSkill("CommandoBodyBarrage", "Bullet", 1);
            AddSkill("ThrowGrenade", "Grenade", 1);

            //Engineer
            AddSkill("EngiBodyFireGrenade", "Grenade", 1);
            AddSkill("EngiBodyPlaceMine", "Mine", 1);
            AddSkill("EngiBodyPlaceSpiderMine", "Mine", 1);
            AddSkill("EngiHarpoons", "Harpoon", 1);
            AddSkill("EngiBodyPlaceTurret", "Turret Shot", 1);
            AddSkill("EngiBodyPlaceWalkerTurret", "Laser", 0.6f);

            //Huntress
            AddSkill("HuntressBodyFireSeekingArrow", "Arrow", 1);
            AddSkill("FireFlurrySeekingArrow", "Arrow", 0.7f);
            AddSkill("HuntressBodyGlaive", "Glaive", 0.8f);
            AddSkill("HuntressBodyArrowRain", "Rain", 1);
            AddSkill("AimArrowSnipe", "Ballista", 1);

            //Loader
            AddSkill("SwingFist", "Fist", 1);
            AddSkill("FireYankHook", "Fist", 1);
            AddSkill("ChargeFist", "Fist", 1);
            AddSkill("ChargeZapFist", "Fist", 1);
            AddSkill("ThrowPylon", "Fist", 0.5f);

            //Mercenary
            AddSkill("MercGroundLight2", "Sword", 1);
            AddSkill("MercBodyWhirlwind", "Slice", 1);
            AddSkill("MercBodyUppercut", "Slice", 1);
            AddSkill("MercBodyAssaulter", "Dash", 1);
            AddSkill("MercBodyEvis", "Hit", 1);
            AddSkill("MercBodyEvisProjectile", "Blade", 1);

            //MUL-T
            AddSkill("ToolbotBodyFireNailgun", "Nail", 0.6f);
            AddSkill("ToolbotBodyFireSpear", "Rebar", 1);
            AddSkill("ToolbotBodyFireGrenadeLauncher", "Rocket", 1);
            AddSkill("ToolbotBodyFireBuzzsaw", "Saw", 1);
            AddSkill("ToolbotBodyStunDrone", "Canister", 1);
            AddToSkill("ToolbotBodyStunDrone", "Bomblet", 0.3f);
            AddSkill("ToolbotBodyToolbotDash", "Charge", 1);
            AddToSkill("ToolbotBodyToolbotDash", "Ram", 0);

            //REX
            AddSkill("TreebotBodyFireSyringe", "Syringes", 0.5f);
            AddSkill("TreebotBodyAimMortar2", "Mortar", 1);
            AddSkill("TreebotBodyAimMortarRain", "Hit", 0.5f);
            AddSkill("TreebotBodySonicBoom", "Boom", 0);
            AddSkill("TreebotBodyPlantSonicBoom", "Boom", 0.5f);
            AddSkill("TreebotBodyFireFlower2", "Projectile", 1);
            AddToSkill("TreebotBodyFireFlower2", "Roots", 0);

            //Items
            AddSkill("LunarPrimaryReplacement", "Shard", 0.1f);
            AddToSkill("LunarPrimaryReplacement", "Explosion", 1);

        }
    }
}
