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
            AddSkill("CROCO_PRIMARY_NAME", "Slash", 1);
            AddSkill("CROCO_SECONDARY_NAME", "Spit", 1);
            AddSkill("CROCO_SECONDARY_ALT_NAME", "Bite", 1);
            AddSkill("CROCO_UTILITY_NAME", "Leap", 1);
            AddToSkill("CROCO_UTILITY_NAME", "Pool", 0.1f);
            AddSkill("CROCO_UTILITY_ALT1_NAME", "Leap", 1);
            AddSkill("CROCO_SPECIAL_NAME", "Bounce", 1);

            // Artificer
            AddSkill("MAGE_PRIMARY_FIRE_NAME", "Bolt", 1);
            AddSkill("MAGE_PRIMARY_LIGHTNING_NAME", "Bolt", 1);
            AddSkill("MAGE_SECONDARY_LIGHTNING_NAME", "Explosion", 1);
            AddToSkill("MAGE_SECONDARY_LIGHTNING_NAME", "Sparks", 0.3f);
            AddSkill("MAGE_SECONDARY_ICE_NAME", "Spear", 1);
            AddSkill("MAGE_UTILITY_ICE_NAME", "Wall", 1);
            AddSkill("MAGE_SPECIAL_FIRE_NAME", "Flame Thrower", 1);
            AddSkill("MAGE_SPECIAL_LIGHTNING_NAME", "Surge", 1);

            // Captain
            AddSkill("CAPTAIN_PRIMARY_NAME", "Shotgun", 0.75f);
            AddSkill("CAPTAIN_SECONDARY_NAME", "Power Tazer", 1);
            AddSkill("CAPTAIN_UTILITY_NAME", "Airstrike", 1);
            AddSkill("CAPTAIN_SPECIAL_NAME", "Impact", 0);

            // Commando
            AddSkill("COMMANDO_PRIMARY_NAME", "Bullet", 1);
            AddSkill("COMMANDO_SECONDARY_NAME", "Phase Round", 1);
            AddSkill("COMMANDO_SECONDARY_ALT1_NAME", "Shotgun", 0.5f);
            AddSkill("COMMANDO_SPECIAL_NAME", "Bullet", 1);
            AddSkill("COMMANDO_SPECIAL_ALT1_NAME", "Grenade", 1);

            // Engineer
            AddSkill("ENGI_PRIMARY_NAME", "Grenade", 1);
            AddSkill("ENGI_SECONDARY_NAME", "Mine", 1);
            AddSkill("ENGI_SPIDERMINE_NAME", "Mine", 1);
            AddSkill("ENGI_SKILL_HARPOON_NAME", "Harpoon", 1);
            AddSkill("ENGI_SPECIAL_NAME", "Turret Shot", 1);
            AddSkill("ENGI_SPECIAL_ALT1_NAME", "Laser", 0.6f);

            // Huntress
            AddSkill("HUNTRESS_PRIMARY_NAME", "Arrow", 1);
            AddSkill("HUNTRESS_PRIMARY_ALT_NAME", "Arrow", 0.7f);
            AddSkill("HUNTRESS_SECONDARY_NAME", "Glaive", 0.8f);
            AddSkill("HUNTRESS_SPECIAL_NAME", "Rain", 0.2f);
            AddSkill("HUNTRESS_SPECIAL_ALT1_NAME", "Ballista", 1);

            // Loader
            AddSkill("LOADER_PRIMARY_NAME", "Fist", 1);
            AddSkill("LOADER_YANKHOOK_NAME", "Fist", 1);
            AddSkill("LOADER_UTILITY_NAME", "Fist", 1);
            AddSkill("LOADER_UTILITY_ALT1_NAME", "Fist", 1);
            AddSkill("LOADER_SPECIAL_NAME", "Fist", 0.5f);

            // Mercenary
            AddSkill("MERC_PRIMARY_NAME", "Sword", 1);
            AddSkill("MERC_SECONDARY_NAME", "Slice", 1);
            AddSkill("MERC_SECONDARY_ALT1_NAME", "Slice", 1);
            AddSkill("MERC_UTILITY_NAME", "Dash", 1);
            AddSkill("MERC_SPECIAL_NAME", "Hit", 1);
            AddSkill("MERC_SPECIAL_ALT1_NAME", "Blade", 1);

            // MUL-T
            AddSkill("TOOLBOT_PRIMARY_NAME", "Nail", 0.6f);
            AddSkill("TOOLBOT_PRIMARY_ALT1_NAME", "Rebar", 1);
            AddSkill("TOOLBOT_PRIMARY_ALT2_NAME", "Rocket", 1);
            AddSkill("TOOLBOT_PRIMARY_ALT3_NAME", "Saw", 1);
            AddSkill("TOOLBOT_SECONDARY_NAME", "Canister", 1);
            AddToSkill("TOOLBOT_SECONDARY_NAME", "Bomblet", 0.3f);
            AddSkill("TOOLBOT_UTILITY_NAME", "Charge", 1);
            AddToSkill("TOOLBOT_UTILITY_NAME", "Ram", 0);

            // REX
            AddSkill("TREEBOT_PRIMARY_NAME", "Syringes", 0.5f);
            AddSkill("TREEBOT_SECONDARY_NAME", "Mortar", 1);
            AddSkill("TREEBOT_SECONDARY_ALT1_NAME", "Hit", 0.5f);
            AddSkill("TREEBOT_UTILITY_ALT1_NAME", "Boom", 0.5f);
            AddSkill("TREEBOT_SPECIAL_NAME", "Projectile", 1);
            AddToSkill("TREEBOT_SPECIAL_NAME", "Roots", 0);

            // Items
            AddSkill("SKILL_LUNAR_PRIMARY_REPLACEMENT_NAME", "Shard", 0.1f);
            AddToSkill("SKILL_LUNAR_PRIMARY_REPLACEMENT_NAME", "Explosion", 1);

            
        }
    }
}
