using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    public static class ItemStats
    {
        [Obsolete("This feature of BetterUI has been removed.")]
        public static readonly Dictionary<ItemDef, ItemProcInfo> itemProcInfos = new Dictionary<ItemDef, ItemProcInfo>();



        [Obsolete("This feature of BetterUI has been removed.")]
        public static List<ItemStat> GetItemStats(ItemDef itemDef)
        {
            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static List<ItemTag> GetItemTags(ItemStat itemStat)
        {
            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static List<ItemModifier> GetItemModifers(ItemTag itemTag)
        {
            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static ItemStat RegisterStat(ItemDef itemDef, string nameToken, float value, StackingFormula stackingFormula = null, StatFormatter statFormatter = null, ItemTag itemTag = null)
        {
            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
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
            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static ItemStat RegisterStat(
            ItemDef itemDef,
            string nameToken,
            float value,
            float stackValue,
            int stackLimit,
            StackingFormula stackingFormula = null,
            StatFormatter statFormatter = null,
            ItemTag itemTag = null
        )
        {
            return null;
        }

        public static ItemStat RegisterStat(ItemDef itemDef, ItemStat itemStat, ItemTag itemTag)
        {
            return null;
        }


        [Obsolete("This feature of BetterUI has been removed.")]
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

            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static ItemModifier RegisterModifier(
            ItemTag itemTag,
            string nameToken,
            ItemModifier.ModificationFormula modificationFormula,
            ItemModifier.ModificationLocator modificationLocator,
            ItemModifier.ModificationChecker modificationChecker,
            ItemModifier.ModificationCounter modificationCounter,
            string pluralNameToken = null)
        {
            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static ItemModifier RegisterModifier(ItemTag itemTag, ItemModifier itemModfier)
        {
            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static ItemTag RegisterTag(ItemStat itemStat, ItemTag itemTag)
        {
            return null;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static void GetItemStats(StringBuilder stringBuilder, ItemDef itemDef, int stacks, CharacterMaster master)
        {
            
        }



        [Obsolete("This feature of BetterUI has been removed.")]
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

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter LuckChance = new StatFormatter()
            {
                
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Chance = new StatFormatter()
            {
                
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Gold = new StatFormatter()
            {
                
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Charges = new StatFormatter()
            {
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Percent = new StatFormatter()
            {
                
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter HP = new StatFormatter()
            {
                
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Seconds = new StatFormatter()
            {
                
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Armor = new StatFormatter()
            {
                
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Regen = new StatFormatter()
            {

            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Range = new StatFormatter()
            {
                
            };

            [Obsolete("This feature of BetterUI has been removed.")]
            public static StatFormatter Damage = new StatFormatter()
            {

            };


            [Obsolete("This feature of BetterUI has been removed.")]
            public void FormatString(StringBuilder stringBuilder, float value, CharacterMaster master)
            {
                
            }

        }



        [Obsolete("This feature of BetterUI has been removed.")]
        public delegate float StackingFormula(float value, float extraStackValue, int stacks);

        [Obsolete("This feature of BetterUI has been removed.")]
        public static float LinearStacking(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static float ExponentialStackingPlusOne(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static float ExponentialStacking(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static float NegativeExponentialStacking(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        [Obsolete("Deprecated: Use DivideByStacksPlusOne instead.")]
        public static float DivideByBonusStacks(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static float DivideByStacksPlusOne(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static float DivideByStacks(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static float FocusedConvergenceStacking(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        public static float HyperbolicStacking(float value, float extraStackValue, int stacks)
        {
            return 0;
        }
        public static float NoStacking(float value, float extraStackValue, int stacks)
        {
            return 0;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public delegate int CapFormula(float value, float extraStackValue, float procCoefficient);

        [Obsolete("This feature of BetterUI has been removed.")]
        public static int LinearCap(float value, float extraStackValue, float procCoefficient)
        {
            return 0;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public class ItemProcInfo
        {
            public float value;
            public float extraStackValue;
            public StatFormatter statFormatter;
            public StackingFormula stackingFormula;
            public CapFormula capFormula;

            [Obsolete("This feature of BetterUI has been removed.")]
            public void GetOutputString(StringBuilder stringBuilder, int stacks, CharacterMaster master, float procCoefficient)
            {

            }
            [Obsolete("This feature of BetterUI has been removed.")]
            public float GetValue(int stacks)
            {
                return 0;
            }

            [Obsolete("This feature of BetterUI has been removed.")]
            public float GetCap(float procCoefficient)
            {
                return 0;
            }
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static void RegisterProc(ItemIndex itemIndex, ItemProcInfo itemProcInfo)
        {
            
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public static void RegisterProc(ItemIndex itemIndex, float value, float? extraStackValue = null, StatFormatter statFormatter = null, StackingFormula stackingFormula = null, CapFormula capFormula = null)
        {
            }


        [Obsolete("This feature of BetterUI has been removed.")]
        public static void RegisterProc(ItemDef itemDef, ItemProcInfo itemProcInfo)
        {

        }
        [Obsolete("This feature of BetterUI has been removed.")]
        public static void RegisterProc(ItemDef itemDef, float value, float? extraStackValue = null, StatFormatter statFormatter = null, StackingFormula stackingFormula = null, CapFormula capFormula = null)
        {
            
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public class ItemStat
        {
            public string nameToken;
            public StackingFormula stackingFormula;
            public float value;
            public float stackValue;
            public int stackLimit = int.MaxValue;
            public StatFormatter statFormatter;
        }

        [Obsolete("This feature of BetterUI has been removed.")]
        public class ItemModifier
        {
            public delegate float ModificationFormula(float value, float modifier, float stacks, float stackModifier);
            public static ModificationFormula ExponentialBonus = (value, modifier, stackModifier, stacks) => (1 - value) - (1 - value) * (1 - modifier) * (float)Math.Pow(1 - stackModifier, stacks - 1);
            public static ModificationFormula PositiveExponentialBonus = (value, modifier, stackModifier, stacks) => value * modifier * (float)Math.Pow(1 - stackModifier, stacks - 1);
            public static ModificationFormula PercentBonus = (value, modifier, stackModifier, stacks) => value * ((modifier + (stackModifier * (stacks - 1))) / 100);
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

            [Obsolete("This feature of BetterUI has been removed.")]
            public bool GetModificationActive(CharacterMaster master)
            {
                return modificationChecker(master, this);
            }

            [Obsolete("This feature of BetterUI has been removed.")]
            public float GetModificationCount(CharacterMaster master)
            {
                return modificationCounter(master, this);
            }
            [Obsolete("This feature of BetterUI has been removed.")]
            public float GetModifiedValue(float value, CharacterMaster master, float count)
            {
                if (modificationLocator != null)
                {
                    return modificationFormula(value, modificationLocator(master), stackModifier, count);
                }
                else
                {
                    return modificationFormula(value, modifier, stackModifier, count);
                }
            }
        }


        [Obsolete("This feature of BetterUI has been removed.")]
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

        [Obsolete("This feature of BetterUI has been removed.")]
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
