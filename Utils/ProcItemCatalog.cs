﻿using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    public static class ProcItemsCatalog
    {
        private static StringBuilder formatterStringBuilder = new StringBuilder();
        [Obsolete("Deprecated: ProcEffect is deprecated and will be removed in BetterUI version 1.7.0. Use EffectFormatter instead.")]
        public enum ProcEffect
        {
            Chance,
            HP,
            Range,
        }
        [Obsolete("Deprecated: Stacking is deprecated and will be removed in BetterUI version 1.7.0. Use StackingForumla and CapFormula instead.")]
        public enum Stacking
        {
            None,
            Linear,
            Hyperbolic,
        }


        public delegate string EffectFormatter(float value, float procCoefficient, float luck, bool canCap, int cap);

        public static string ChanceFormatter(float value, float procCoefficient, float luck, bool canCap, int cap)
        {
            formatterStringBuilder.Clear();
            formatterStringBuilder.Append("<style=cIsDamage>");
            formatterStringBuilder.Append(Math.Min(100, 100 * Utils.LuckCalc(value * procCoefficient, luck)).ToString("0.##"));
            formatterStringBuilder.Append("%</style>");
            if (canCap) {
                formatterStringBuilder.Append(" <style=cStack>(");
                formatterStringBuilder.Append(cap);
                formatterStringBuilder.Append(" stacks to cap)</style>");
            }
            return formatterStringBuilder.ToString();
        }

        public static string HPFormatter(float value, float procCoefficient, float luck, bool canCap, int cap)
        {
            formatterStringBuilder.Clear();
            formatterStringBuilder.Append("<style=cIsHealing>");
            formatterStringBuilder.Append(value * procCoefficient);
            formatterStringBuilder.Append(" HP</style>");
            if (canCap)
            {
                formatterStringBuilder.Append(" <style=cStack>(");
                formatterStringBuilder.Append(cap);
                formatterStringBuilder.Append(" stacks to cap)</style>");
            }
            return formatterStringBuilder.ToString();
        }

        public static string RangeFormatter(float value, float procCoefficient, float luck, bool canCap, int cap)
        {
            formatterStringBuilder.Clear();
            formatterStringBuilder.Append("<style=cIsDamage>");
            formatterStringBuilder.Append(value * procCoefficient);
            formatterStringBuilder.Append("m </style>");
            if (canCap)
            {
                formatterStringBuilder.Append(" <style=cStack>(");
                formatterStringBuilder.Append(cap);
                formatterStringBuilder.Append(" stacks to cap)</style>");
            }
            return formatterStringBuilder.ToString();
        }

        public delegate float StackingFormula(float value, float extraStackValue, int stacks);

        public delegate int CapFormula(float value, float extraStackValue, float procCoefficient);
        public static float LinearStacking(float value, float extraStackValue, int stacks)
        {
            return value + extraStackValue * (stacks - 1);
        }
        public static float ExponentialStacking(float value, float extraStackValue, int stacks)
        {
            return (float) (1 - (1 - value) * Math.Pow(1 - extraStackValue, stacks - 1 ));
        }
        public static float HyperbolicStacking(float value, float extraStackValue, int stacks)
        {
            return (float)(1 - 1 / (1 +( value + extraStackValue * (stacks-1))));
        }
        public static float NoStacking(float value, float extraStackValue, int stacks)
        {
            return value;
        }
        public static int LinearCap(float value, float extraStackValue, float procCoefficient)
        {
            return (int) Math.Ceiling((1 - value*procCoefficient) / (extraStackValue * procCoefficient)) + 1;
        }
        public class EffectInfo
        {
            public float value;
            public float extraStackValue;
            public EffectFormatter effectFormatter;
            public StackingFormula stackingFormula;
            public CapFormula capFormula;

            public string GetOutputString(int stacks, float luck, float procCoefficient)
            {
                bool canCap = capFormula != null;
                return this.effectFormatter(this.stackingFormula(this.value, this.extraStackValue, stacks), procCoefficient, luck, canCap, canCap ? this.capFormula(this.value, this.extraStackValue, procCoefficient) : 1);
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
        private static readonly Dictionary<ItemIndex, EffectInfo> items = new Dictionary<ItemIndex, EffectInfo>();
        public static void AddEffect(ItemIndex itemIndex, EffectInfo effectInfo)
        {
            if((effectInfo.value > 1 || effectInfo.extraStackValue > 1) && effectInfo.effectFormatter == ChanceFormatter)
            {
                BetterUI.print("Warning: EffectInfo.value for chance is expected to be 0 to 1 based. After BetterUI v1.7.0 it will no longer be converted.");
                effectInfo.value = effectInfo.value * 0.01f;
                effectInfo.extraStackValue = effectInfo.extraStackValue * 0.01f;
            }
            items[itemIndex] = effectInfo;
        }
        public static void AddEffect(ItemIndex itemIndex, float value, float? extraStackValue = null, EffectFormatter effectFormatter = null, StackingFormula stackingFormula = null, CapFormula capFormula = null)
        {
            AddEffect(itemIndex, new EffectInfo() { 
                value = value, 
                extraStackValue = extraStackValue ?? value, 
                effectFormatter = effectFormatter ?? ChanceFormatter, 
                stackingFormula = stackingFormula ?? LinearStacking, 
                capFormula = capFormula 
            });
        }
        [Obsolete("Deprecated: This overload uses deprecated types and will be removed in BetterUI version 1.7.0")]
        public static void AddEffect(ItemIndex itemIndex, ProcEffect procEffect, float value, Stacking stacking = Stacking.Linear)
        {
            AddEffect(itemIndex, procEffect, value, value, stacking);
        }

        [Obsolete("Deprecated: This overload uses deprecated types and will be removed in BetterUI version 1.7.0")]
        public static void AddEffect(ItemIndex itemIndex, ProcEffect procEffect, float value, float extraStackValue, Stacking stacking = Stacking.Linear)
        {
            StackingFormula stackingFormula;
            CapFormula capFormula = null;
            if (stacking == Stacking.Linear)
            {
                stackingFormula = LinearStacking;
                if (procEffect == ProcEffect.Chance)
                {
                    capFormula = LinearCap;
                }
            } 
            else if (stacking == Stacking.Hyperbolic)
            {
                stackingFormula = HyperbolicStacking;
            }
            else
            {
                stackingFormula = NoStacking;
            }
            EffectFormatter effectFormatter;

            if (procEffect == ProcEffect.Chance)
            {
                effectFormatter = ChanceFormatter;
            } 
            else if (procEffect == ProcEffect.HP)
            {
                effectFormatter = HPFormatter;
            }
            else
            {
                effectFormatter = RangeFormatter;
            }
            AddEffect(itemIndex, value, extraStackValue, effectFormatter, stackingFormula, capFormula);
        }
        public static Dictionary<ItemIndex, EffectInfo> GetAllItems()
        {
            return items;
        }

        static ProcItemsCatalog()
        {
            AddEffect(ItemIndex.BleedOnHit, 0.15f, effectFormatter: ChanceFormatter, stackingFormula: LinearStacking, capFormula: LinearCap);
            AddEffect(ItemIndex.StunChanceOnHit, 0.05f, effectFormatter: ChanceFormatter, stackingFormula: HyperbolicStacking );
            AddEffect(ItemIndex.StickyBomb, 0.05f, effectFormatter: ChanceFormatter, stackingFormula: LinearStacking, capFormula: LinearCap);
            AddEffect(ItemIndex.Missile,  0.1f, effectFormatter: ChanceFormatter, stackingFormula: NoStacking);
            AddEffect(ItemIndex.ChainLightning,  0.25f, effectFormatter: ChanceFormatter, stackingFormula: NoStacking);
            AddEffect(ItemIndex.Seed, 1, effectFormatter: HPFormatter, stackingFormula: LinearStacking);
            AddEffect(ItemIndex.HealOnCrit, 8, effectFormatter: HPFormatter, stackingFormula: LinearStacking);
            AddEffect(ItemIndex.Behemoth, 4, 2.5f, effectFormatter: RangeFormatter, stackingFormula: LinearStacking);
            AddEffect(ItemIndex.BounceNearby, 0.2f, effectFormatter: ChanceFormatter, stackingFormula: HyperbolicStacking);
            AddEffect(ItemIndex.GoldOnHit, 0.3f, effectFormatter: ChanceFormatter, stackingFormula: NoStacking);
        }
    }
}
