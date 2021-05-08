using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    public static class ProcItemsCatalog
    {
        public static readonly Dictionary<ItemDef, EffectInfo> items = new Dictionary<ItemDef, EffectInfo>();
        private static StringBuilder formatterStringBuilder = new StringBuilder();

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

        public static void AddEffect(ItemIndex itemIndex, EffectInfo effectInfo)
        {
            if (itemIndex <= ItemIndex.None)
            {
                UnityEngine.Debug.LogError("ERROR: AddEffect was passed ItemIndex.None or below, this likely means you tried to register your effect before the item catalog was ready. Please use ItemCatalog.availability.CallWhenAvailable or pass an ItemDef directly.");
                return;
            }
            AddEffect(ItemCatalog.GetItemDef(itemIndex), effectInfo);
        }
        public static void AddEffect(ItemIndex itemIndex, float value, float? extraStackValue = null, EffectFormatter effectFormatter = null, StackingFormula stackingFormula = null, CapFormula capFormula = null)
        {
            if (itemIndex <= ItemIndex.None)
            {
                UnityEngine.Debug.LogError("ERROR: AddEffect was passed ItemIndex.None or below, this likely means you tried to register your effect before the item catalog was ready. Please use ItemCatalog.availability.CallWhenAvailable or pass an ItemDef directly.");
                return;
            }
            AddEffect(ItemCatalog.GetItemDef(itemIndex), value, extraStackValue, effectFormatter, stackingFormula, capFormula);
        }
        public static void AddEffect(ItemDef itemDef, EffectInfo effectInfo)
        {
            if ((effectInfo.value > 1 || effectInfo.extraStackValue > 1) && effectInfo.effectFormatter == ChanceFormatter)
            {
                UnityEngine.Debug.LogError("Warning: EffectInfo.value for chance is expected to be 0 to 1 based. After BetterUI v1.7.0 it will no longer be converted.");
                effectInfo.value = effectInfo.value * 0.01f;
                effectInfo.extraStackValue = effectInfo.extraStackValue * 0.01f;
            }
            items[itemDef] = effectInfo;
        }
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

        static ProcItemsCatalog()
        {
            AddEffect(RoR2.RoR2Content.Items.BleedOnHit, 0.1f, effectFormatter: ChanceFormatter, stackingFormula: LinearStacking, capFormula: LinearCap);
            AddEffect(RoR2.RoR2Content.Items.StunChanceOnHit, 0.05f, effectFormatter: ChanceFormatter, stackingFormula: HyperbolicStacking );
            AddEffect(RoR2.RoR2Content.Items.StickyBomb, 0.05f, effectFormatter: ChanceFormatter, stackingFormula: LinearStacking, capFormula: LinearCap);
            AddEffect(RoR2.RoR2Content.Items.Missile,  0.1f, effectFormatter: ChanceFormatter, stackingFormula: NoStacking);
            AddEffect(RoR2.RoR2Content.Items.ChainLightning,  0.25f, effectFormatter: ChanceFormatter, stackingFormula: NoStacking);
            AddEffect(RoR2.RoR2Content.Items.Seed, 1, effectFormatter: HPFormatter, stackingFormula: LinearStacking);
            AddEffect(RoR2.RoR2Content.Items.HealOnCrit, 8, effectFormatter: HPFormatter, stackingFormula: LinearStacking);
            AddEffect(RoR2.RoR2Content.Items.Behemoth, 4, 2.5f, effectFormatter: RangeFormatter, stackingFormula: LinearStacking);
            AddEffect(RoR2.RoR2Content.Items.BounceNearby, 0.2f, effectFormatter: ChanceFormatter, stackingFormula: HyperbolicStacking);
            AddEffect(RoR2.RoR2Content.Items.GoldOnHit, 0.3f, effectFormatter: ChanceFormatter, stackingFormula: NoStacking);
        }
    }
}
