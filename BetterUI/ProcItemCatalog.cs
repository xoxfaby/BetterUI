using System;
using System.Collections.Generic;
using System.Text;
using RoR2;

namespace BetterUI
{
    public static class ProcItemsCatalog
    {
        public enum ProcEffect
        {
            Chance,
            HP,
            Range,
        }
        public enum Stacking
        {
            None,
            Linear,
            Hyperbolic,
        }
        public struct EffectInfo
        {
            public ProcEffect effectType;
            public float value;
            public Stacking stacking;
            public float stackAmount;
        }
        private static readonly Dictionary<ItemIndex, EffectInfo> items = new Dictionary<ItemIndex, EffectInfo>();
        public static void AddEffect(ItemIndex itemIndex, EffectInfo effectInfo)
        {
            items[itemIndex] = effectInfo;
        }
        public static void AddEffect(ItemIndex itemIndex, ProcEffect procEffect, float value, Stacking stacking = Stacking.Linear)
        {
            AddEffect(itemIndex, procEffect, value, value, stacking);
        }
        public static void AddEffect(ItemIndex itemIndex, ProcEffect procEffect, float value, float stackAmount, Stacking stacking = Stacking.Linear )
        {
            items[itemIndex] = new EffectInfo() { effectType = procEffect, value = value, stacking = stacking, stackAmount = stackAmount };
        }
        public static Dictionary<ItemIndex, EffectInfo> GetAllItems()
        {
            return new Dictionary<ItemIndex, EffectInfo>(items);
        }
        static ProcItemsCatalog()
        {
            AddEffect(ItemIndex.BleedOnHit, ProcEffect.Chance, 15);
            AddEffect(ItemIndex.StunChanceOnHit, ProcEffect.Chance, 5, Stacking.Hyperbolic);
            AddEffect(ItemIndex.StickyBomb, ProcEffect.Chance, 5);
            AddEffect(ItemIndex.Missile, ProcEffect.Chance, 10, Stacking.None);
            AddEffect(ItemIndex.ChainLightning, ProcEffect.Chance, 25, Stacking.None);
            AddEffect(ItemIndex.Seed, ProcEffect.HP, 1);
            AddEffect(ItemIndex.HealOnCrit, ProcEffect.HP, 8);
            AddEffect(ItemIndex.Behemoth, ProcEffect.Range, 4, 2.5f);
            AddEffect(ItemIndex.BounceNearby, ProcEffect.Chance, 20, Stacking.Hyperbolic);
            AddEffect(ItemIndex.GoldOnHit, ProcEffect.Chance, 30, Stacking.None);
        }
    }
}
