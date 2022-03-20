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

        static ItemStats()
        {
            RegisterProc(RoR2Content.Items.BleedOnHit, 0.1f, statFormatter: StatFormatter.Chance, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterProc(RoR2Content.Items.StunChanceOnHit, 0.05f, statFormatter: StatFormatter.Chance, stackingFormula: HyperbolicStacking);
            RegisterProc(RoR2Content.Items.StickyBomb, 0.05f, statFormatter: StatFormatter.Chance, stackingFormula: LinearStacking, capFormula: LinearCap);
            RegisterProc(RoR2Content.Items.Missile, 0.1f, statFormatter: StatFormatter.Chance, stackingFormula: NoStacking);
            RegisterProc(RoR2Content.Items.ChainLightning, 0.25f, statFormatter: StatFormatter.Chance, stackingFormula: NoStacking);
            RegisterProc(RoR2Content.Items.Seed, 1f, statFormatter: StatFormatter.HP, stackingFormula: LinearStacking);
            RegisterProc(RoR2Content.Items.HealOnCrit, 8f, statFormatter: StatFormatter.HP, stackingFormula: LinearStacking);
            RegisterProc(RoR2Content.Items.Behemoth, 4f, 2.5f, statFormatter: StatFormatter.Range, stackingFormula: LinearStacking);
            RegisterProc(RoR2Content.Items.BounceNearby, 0.2f, statFormatter: StatFormatter.Chance, stackingFormula: HyperbolicStacking);
            RegisterProc(RoR2Content.Items.GoldOnHit, 0.3f, statFormatter: StatFormatter.Chance, stackingFormula: NoStacking);

            RegisterStat(RoR2Content.Items.AlienHead, "Cooldown Reduction", 0.75f, ExponentialStacking, StatFormatter.Percent);
            RegisterStat(RoR2Content.Items.ArmorPlate, "Armor", 5f, LinearStacking, StatFormatter.Armor);

            var KnurlHealth = RegisterStat(RoR2Content.Items.Knurl, "Bonus Health", 40f, LinearStacking, StatFormatter.HP);
            var KnurlRegen = RegisterStat(RoR2Content.Items.Knurl, "Regen", 1.6f, LinearStacking, StatFormatter.Regen);
            RegisterTag(KnurlRegen, Tags.Healing);

            RegisterModifier(Tags.Healing, RoR2Content.Items.IncreaseHealing, ItemModifier.PercentBonus, 100, styleTag: Styles.Healing);
        }

        public static ItemStat RegisterStat(ItemDef itemDef, ItemStat itemStat)
        {
            if(itemStats.TryGetValue(itemDef, out var stats)){
                stats.Add(itemStat);
            } 
            else
            {
                itemStats[itemDef] = new List<ItemStat>() { itemStat };
            }
            return itemStat;
        }

        public static ItemStat RegisterStat(ItemDef itemDef, string nameToken, float value, StackingFormula stackingFormula, StatFormatter statFormatter)
        {
            return RegisterStat(itemDef, nameToken, value, value, stackingFormula, statFormatter);
        }

        public static ItemStat RegisterStat(ItemDef itemDef, string nameToken, string pluralNameToken, float value, StackingFormula stackingFormula, StatFormatter statFormatter)
        {
            return RegisterStat(itemDef, nameToken, value, value, stackingFormula, statFormatter);
        }

        public static ItemStat RegisterStat(
            ItemDef itemDef,
            string nameToken,
            float value,
            float stackValue,
            StackingFormula stackingFormula,
            StatFormatter statFormatter
        )
        {
            ItemStat itemStat = new ItemStat()
            {
                nameToken = nameToken,
                value = value,
                stackValue = stackValue,
                stackingFormula = stackingFormula,
                statFormatter = statFormatter
            };
            return RegisterStat(itemDef, itemStat);
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

        public static ItemModifier RegisterModifier(
            ItemTag itemTag,
            string nameToken,
            ItemModifier.ModificationFormula modificationFormula,
            ItemDef itemDef,
            float modifier,
            string pluralNameToken = null,
            string styleTag = null,
            string styleClosingTag = "</style>")
        {
            ItemModifier itemModfier = new ItemModifier()
            {
                nameToken = nameToken,
                pluralNameToken = pluralNameToken,
                styleTag = styleTag,
                styleClosingTag = styleClosingTag,
                modificationFormula = modificationFormula,
                itemDef = itemDef,
                modifier = modifier
            };
            return RegisterModifier(itemTag, itemModfier);
        }

        public static ItemModifier RegisterModifier(
            ItemTag itemTag,
            ItemDef itemDef,
            ItemModifier.ModificationFormula modificationFormula,
            float modifier,
            string pluralNameToken = null,
            string styleTag = null,
            string styleClosingTag = "</style>")
        {
            ItemModifier itemModfier = new ItemModifier()
            {
                nameToken = itemDef.nameToken,
                pluralNameToken = pluralNameToken,
                styleTag = styleTag,
                styleClosingTag = styleClosingTag,
                modificationFormula = modificationFormula,
                itemDef = itemDef,
                modifier = modifier
            };
            return RegisterModifier(itemTag, itemModfier);
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
            stringBuilder.Clear();
            stringBuilder.Append("</style>\n");
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
                                    totalValue += itemModifier.modificationFormula(baseValue, itemModifier.modifier, 2);
                                }
                            }
                        }
                    }
                    stringBuilder.Append("\n");
                    if (!String.IsNullOrEmpty(itemStat.styleTag)) stringBuilder.Append(itemStat.styleTag);
                    stringBuilder.Append(Language.GetString(itemStat.nameToken));
                    stringBuilder.Append(": ");
                    if (!String.IsNullOrEmpty(itemStat.styleTag)) stringBuilder.Append(itemStat.styleClosingTag);
                    stringBuilder.Append(totalValue);
                    if (tags != null)
                    {
                        stringBuilder.Append("\n  Base: ");
                        stringBuilder.Append(baseValue);
                        foreach (var itemTag in tags)
                        {
                            if (itemModifiers.TryGetValue(itemTag, out var modifiers))
                            {
                                foreach (var itemModifier in modifiers)
                                {
                                    int modifierStacks = master.inventory.GetItemCount(itemModifier.itemDef);
                                    float modifierValue = itemModifier.modificationLocator != null ? itemModifier.modificationLocator(master) : itemModifier.modifier;
                                    float modifiedValue = itemModifier.modificationFormula(baseValue, modifierValue, modifierStacks);

                                    stringBuilder.Append("\n  ");
                                    if (!String.IsNullOrEmpty(itemModifier.styleTag)) stringBuilder.Append(itemModifier.styleTag);

                                    if (modifierStacks > 1)
                                    {
                                        stringBuilder.Append(modifierStacks);
                                        stringBuilder.Append(" ");
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
                                    if (!String.IsNullOrEmpty(itemModifier.styleTag)) stringBuilder.Append(itemModifier.styleClosingTag);
                                    stringBuilder.Append(modifiedValue);
                                }
                            }
                        }
                    }
                }
            }
        }

        

        public class StatFormatter
        {
            public delegate void Formatter(StringBuilder stringBuilder, float value);
            public Formatter statFormatter;
            public string style;
            public string color;
            public string customFormatTag;
            public string customFormatClosingTag;
            public string prefix;
            public string suffix;
            public bool bold;
            public bool italic;
            public bool underline;
            public bool strikethrough;

            public static StatFormatter Chance = new StatFormatter()
            {
                suffix = "%",
                style = Styles.Damage
            };

            public static StatFormatter Percent = new StatFormatter()
            {
                statFormatter = (StringBuilder stringBuilder, float value) =>
                {
                    stringBuilder.Append("<style=cIsDamage>");
                    stringBuilder.Append(value * 100);
                    stringBuilder.Append("%</style>");
                }
            };

            public static StatFormatter HP = new StatFormatter()
            {
                suffix = " Armor",
                style = Styles.Health
            };

            public static StatFormatter Armor = new StatFormatter()
            {
                suffix = " HP",
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

            public void FormatString(StringBuilder stringBuilder, float value)
            {
                if(statFormatter != null)
                {
                    statFormatter(stringBuilder, value);
                }
                else
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


                    if (!String.IsNullOrEmpty(suffix)) stringBuilder.Append(suffix);
                    if (!String.IsNullOrEmpty(customFormatClosingTag)) stringBuilder.Append(customFormatClosingTag);
                    if (strikethrough) stringBuilder.Append("</s>");
                    if (underline) stringBuilder.Append("</u>");
                    if (italic) stringBuilder.Append("</si>");
                    if (bold) stringBuilder.Append("</b>");
                    if (!String.IsNullOrEmpty(style)) stringBuilder.Append("</style>");
                }
            }
        }



        public delegate float StackingFormula(float value, float extraStackValue, int stacks);

        public static float LinearStacking(float value, float extraStackValue, int stacks)
        {
            return value + extraStackValue * (stacks - 1);
        }
        public static float ExponentialStacking(float value, float extraStackValue, int stacks)
        {
            return (float)(1 - (1 - value) * Math.Pow(1 - extraStackValue, stacks - 1));
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
            return (int)Math.Ceiling((1 - value * procCoefficient) / (extraStackValue * procCoefficient)) + 1;
        }

        public class ItemProcInfo
        {
            public float value;
            public float extraStackValue;
            public StatFormatter statFormatter;
            public StackingFormula stackingFormula;
            public CapFormula capFormula;

            public void GetOutputString(StringBuilder stringBuilder,  int stacks, float luck, float procCoefficient)
            {
                this.statFormatter.FormatString(stringBuilder, this.stackingFormula(this.value, this.extraStackValue, stacks));
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
                statFormatter = statFormatter ?? StatFormatter.Chance,
                stackingFormula = stackingFormula ?? LinearStacking,
                capFormula = capFormula
            });
        }

        public class ItemStat
        {
            public string nameToken;
            public StackingFormula stackingFormula;
            public string styleTag;
            public string styleClosingTag = "</style>";
            public float value;
            public float stackValue;
            public StatFormatter statFormatter;
        }

        public class ItemModifier
        {
            public delegate float ModificationFormula(float value, float modifier, float stacks);
            public static ModificationFormula PercentBonus = (value, modifier, stacks) => value * (modifier / 100) * stacks;

            public delegate float ModificationLocator(CharacterMaster master);
            public static ModificationLocator LuckLocator = (master) => master.luck;

            public string nameToken;
            public string pluralNameToken;
            public string styleTag;
            public string styleClosingTag = "</style>";
            public ModificationFormula modificationFormula;
            public ItemDef itemDef;
            public ModificationLocator modificationLocator;
            public float modifier;
        }


        public class ItemTag
        {
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

        public static class Tags
        {
            public static ItemTag Damage;
            public static ItemTag Healing;
            public static ItemTag Luck;

            static Tags()
            {
                Damage = new ItemTag();
                Healing = new ItemTag();
                Luck = new ItemTag();
            }
        }

    }
}
