using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    [Obsolete]
    public static class ProcItemsCatalog
    {
        [Obsolete]
        private static StringBuilder formatterStringBuilder = new StringBuilder();


        public static string ChanceFormatter(float value, float procCoefficient, float luck, bool canCap, int cap)
        {
            formatterStringBuilder.Clear();
            formatterStringBuilder.Append("<style=cIsDamage>");
            formatterStringBuilder.Append(Math.Min(100, 100 * Utils.LuckCalc(value * procCoefficient, luck)).ToString("0.##"));
            formatterStringBuilder.Append("%</style>");
            return formatterStringBuilder.ToString();
        }

        public static string HPFormatter(float value, float procCoefficient, float luck, bool canCap, int cap)
        {
            formatterStringBuilder.Clear();
            formatterStringBuilder.Append("<style=cIsHealing>");
            formatterStringBuilder.Append(value * procCoefficient);
            formatterStringBuilder.Append(" HP</style>");
            return formatterStringBuilder.ToString();
        }

        public static string RangeFormatter(float value, float procCoefficient, float luck, bool canCap, int cap)
        {
            formatterStringBuilder.Clear();
            formatterStringBuilder.Append("<style=cIsDamage>");
            formatterStringBuilder.Append(value * procCoefficient);
            formatterStringBuilder.Append("m </style>");
            return formatterStringBuilder.ToString();
        }

        public delegate string EffectFormatter(float value, float procCoefficient, float luck, bool canCap, int cap);
        public class FormatterTranslater
        {
            EffectFormatter effectFormatter;
            internal FormatterTranslater(EffectFormatter effectFormatter)
            {
                this.effectFormatter = effectFormatter;
            }

            public void Formatter(StringBuilder stringBuilder, float value, CharacterMaster master)
            {
                stringBuilder.Append(effectFormatter(value, 1, 1, false, 1));
            }
        }


        [Obsolete]
        public delegate float StackingFormula(float value, float extraStackValue, int stacks);
        [Obsolete]
        public static StackingFormula LinearStacking = ItemStats.LinearStacking;
        [Obsolete]
        public static StackingFormula ExponentialStacking = ItemStats.ExponentialStacking;
        [Obsolete]
        public static StackingFormula HyperbolicStacking = ItemStats.HyperbolicStacking;
        [Obsolete]
        public static StackingFormula NoStacking = ItemStats.NoStacking;

        [Obsolete]
        public delegate int CapFormula(float value, float extraStackValue, float procCoefficient);
        [Obsolete]
        public static CapFormula LinearCap = ItemStats.LinearCap;

        [Obsolete]
        public class EffectInfo
        {
            public float value;
            public float extraStackValue;
            public EffectFormatter effectFormatter;
            public StackingFormula stackingFormula;
            public CapFormula capFormula;

            public ItemStats.ItemProcInfo ToItemProcInfo()
            {
                var itemProcInfo = new ItemStats.ItemProcInfo()
                {
                    value = value,
                    extraStackValue = extraStackValue,
                    statFormatter = new ItemStats.StatFormatter()
                    {
                        statFormatter = (new FormatterTranslater(effectFormatter)).Formatter
                    },
                    stackingFormula = new ItemStats.StackingFormula(stackingFormula),
                    capFormula = new ItemStats.CapFormula(capFormula)
                };
                return itemProcInfo;
            }
        }

        [Obsolete("Removed, please use BetterUI.ItemStats.RegisterEffect", true)]
        public static void AddEffect(ItemIndex itemIndex, EffectInfo effectInfo)
        {
            if (itemIndex <= ItemIndex.None)
            {
                UnityEngine.Debug.LogError("ERROR: AddEffect was passed ItemIndex.None or below, this likely means you tried to register your effect before the item catalog was ready. Please use ItemCatalog.availability.CallWhenAvailable or pass an ItemDef directly.");
                return;
            }
            AddEffect(ItemCatalog.GetItemDef(itemIndex), effectInfo);
        }

        [Obsolete("Removed, please use BetterUI.ItemStats.RegisterEffect", true)]
        public static void AddEffect(ItemIndex itemIndex, float value, float? extraStackValue = null, EffectFormatter effectFormatter = null, StackingFormula stackingFormula = null, CapFormula capFormula = null)
        {
            if (itemIndex <= ItemIndex.None)
            {
                UnityEngine.Debug.LogError("ERROR: AddEffect was passed ItemIndex.None or below, this likely means you tried to register your effect before the item catalog was ready. Please use ItemCatalog.availability.CallWhenAvailable or pass an ItemDef directly.");
                return;
            }
            AddEffect(ItemCatalog.GetItemDef(itemIndex), value, extraStackValue, effectFormatter, stackingFormula, capFormula);
        }

        [Obsolete("Removed, please use BetterUI.ItemStats.RegisterEffect", true)]
        public static void AddEffect(ItemDef itemDef, EffectInfo effectInfo)
        {
            ItemStats.RegisterProc(itemDef, effectInfo.ToItemProcInfo());
        }

        [Obsolete("Removed, please use BetterUI.ItemStats.RegisterEffect", true)]
        public static void AddEffect(ItemDef itemDef, float value, float? extraStackValue = null, EffectFormatter effectFormatter = null, StackingFormula stackingFormula = null, CapFormula capFormula = null)
        {
            AddEffect(itemDef, new EffectInfo() { 
                value = value, 
                extraStackValue = extraStackValue ?? value, 
                effectFormatter = effectFormatter ?? ChanceFormatter, 
                stackingFormula = stackingFormula ?? LinearStacking, 
                capFormula = capFormula 
            });
        }
    }
}
