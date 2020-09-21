using System;
using RoR2;
using R2API.Utils;
using BepInEx;
using UnityEngine;

using System.Collections.Generic;
using System.Reflection;

namespace DebugTools
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.xoxfaby.DebugTools", "DebugTools", "1.0.0")]
    public class DebugTools : BaseUnityPlugin
    {
        public void Awake()
        {


            On.RoR2.Networking.GameNetworkManager.OnClientConnect += (self, user, t) => { };
            RoR2.Run.onRunStartGlobal += (run) => {
                foreach (var skillDef in RoR2.Skills.SkillCatalog.allSkillDefs)
                {
                    print("Skill:");
                    print(RoR2.Skills.SkillCatalog.GetSkillName(skillDef.skillIndex));
                    print(Language.GetString(skillDef.skillNameToken));
                    print(skillDef.skillNameToken);
                }
            };
        }
    }
    static class Extentions
    {
        public static List<Variance> DetailedCompare<T>(this T val1, T val2)
        {
            List<Variance> variances = new List<Variance>();
            PropertyInfo[] fi = val1.GetType().GetProperties();
            foreach (var f in fi)
            {
                Variance v = new Variance();
                v.Prop = f.Name;
                v.valA = f.GetValue(val1);
                v.valB = f.GetValue(val2);
                if (!Equals(v.valA, v.valB))
                    variances.Add(v);

            }
            return variances;
        }


    }
    class Variance
    {
        public string Prop { get; set; }
        public object valA { get; set; }
        public object valB { get; set; }
    }

}
