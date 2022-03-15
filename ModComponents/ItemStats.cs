using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    public static class ItemStats
    {
        internal static StringBuilder stringBuilder = new StringBuilder();
        internal static Dictionary<ItemTag, List<ItemModifier>> itemModifiers = new Dictionary<ItemTag, List<ItemModifier>>();
        internal static Dictionary<ItemStat, List<ItemTag>> itemTags = new Dictionary<ItemStat, List<ItemTag>>();
        internal static Dictionary<ItemDef, List<ItemStat>> itemStats = new Dictionary<ItemDef, List<ItemStat>>();

        static ItemStats()
        {
            var healing = new ItemTag();
            RegisterItemStat(RoR2Content.Items.Knurl, new ItemStat()
            {
                nameToken = "Bonus Health",
                value = 40,
                stackValue = 40,
                stackingFormula = ProcItemsCatalog.LinearStacking
            });

            var stat = new ItemStat()
            {
                nameToken = "Regen",
                value = 1.6f,
                stackValue = 1.6f,
                stackingFormula = ProcItemsCatalog.LinearStacking
            };
            RegisterItemStat(RoR2Content.Items.Knurl, stat);
            RegisterItemTag(stat, healing);
            RegisterItemModifier(healing, new ItemModifier()
            {
                itemDef = RoR2Content.Items.IncreaseHealing,
                value = 2,
                nameToken = RoR2Content.Items.IncreaseHealing.nameToken,
                modifier = (x, y) => x * y
            });
        }

        public static void RegisterItemStat(ItemDef itemDef, ItemStat itemStat)
        {
            if(itemStats.TryGetValue(itemDef, out var stats)){
                stats.Add(itemStat);
            } 
            else
            {
                itemStats[itemDef] = new List<ItemStat>() { itemStat };
            }
        }

        public static void RegisterItemTag(ItemStat itemStat, ItemTag itemTag)
        {
            if (itemTags.TryGetValue(itemStat, out var tags))
            {
                tags.Add(itemTag);
            }
            else
            {
                itemTags[itemStat] = new List<ItemTag>() { itemTag };
            }
        }

        public static void RegisterItemModifier(ItemTag itemTag, ItemModifier itemModfier)
        {
            if (itemModifiers.TryGetValue(itemTag, out var modifers))
            {
                modifers.Add(itemModfier);
            }
            else
            {
                itemModifiers[itemTag] = new List<ItemModifier>() { itemModfier };
            }
        }

        public static String GetItemStats(ItemDef itemDef, int stacks)
        {
            stringBuilder.Clear();
            if(itemStats.TryGetValue(itemDef, out var stats))
            {
                stringBuilder.Append("\n");
                foreach (var itemStat in stats)
                {
                    float value = itemStat.stackingFormula(itemStat.value, itemStat.stackValue, stacks);
                    stringBuilder.Append("\n");
                    stringBuilder.Append(Language.GetString(itemStat.nameToken));
                    stringBuilder.Append(":\n  Base: ");
                    stringBuilder.Append(value);
                    if (itemTags.TryGetValue(itemStat, out var tags))
                    {
                        foreach (var itemTag in tags)
                        {
                            if (itemModifiers.TryGetValue(itemTag, out var modifiers))
                            {
                                foreach (var itemModifier in modifiers)
                                {
                                    float modifierValue = itemModifier.modifier(itemModifier.value, 2);
                                    value += modifierValue;
                                    stringBuilder.Append("\n  ");
                                    stringBuilder.Append(Language.GetString(itemModifier.nameToken));
                                    stringBuilder.Append(": ");
                                    stringBuilder.Append(modifierValue);
                                }
                                stringBuilder.Append("\n  Total: ");
                                stringBuilder.Append(value);
                            }
                        }
                    }

                }
            }

            return stringBuilder.ToString();
        }

        public class ItemStat
        {
            public string nameToken;
            public ProcItemsCatalog.StackingFormula stackingFormula;
            public float value;
            public float stackValue;
        }

        public class ItemModifier
        {
            public delegate float Modifier(float value, float stacks);
            public Modifier modifier;
            public ItemDef itemDef;
            public string nameToken;
            public ItemTag tag;
            public float value;
        }

        [Flags]
        enum ItemTagFlag : Int64
        {
            None = 0
        }

        public class ItemTag
        {
            ItemTagFlag itemTagFlag;
        }

    }
}
