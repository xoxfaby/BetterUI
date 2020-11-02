using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using RoR2;
using R2API.Utils;
using BepInEx;
using UnityEngine;


namespace BetterUI
{
    [BepInDependency("dev.ontrigger.itemstats", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "1.6.15.2")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class BetterUI : BaseUnityPlugin
    {
        internal ConfigManager config;
        internal ItemSorting itemSorting;
        internal StatsDisplay statsDisplay;
        internal CommandImprovements commandImprovements;
        internal DPSMeter DPSMeter;
        internal BuffTimers buffTimers;
        internal AdvancedIcons advancedIcons;
        internal ItemCounters itemCounters;
        internal Misc misc;
        internal bool ItemStatsModIntegration;
        internal RoR2.UI.HUD HUD;
        internal List<ModComponent> modComponents = new List<ModComponent>();

        public static StringBuilder sharedStringBuilder = new StringBuilder(); 
        public void Awake()
        {
            BepInExPatcher.DoPatching();

            itemSorting = new ItemSorting(this);
            statsDisplay = new StatsDisplay(this);
            commandImprovements = new CommandImprovements(this);
            DPSMeter = new DPSMeter(this);
            buffTimers = new BuffTimers(this);
            advancedIcons = new AdvancedIcons(this);
            itemCounters = new ItemCounters(this);
            misc = new Misc(this);


        }

        public void Start()
        {
            foreach (ModComponent modComponent in modComponents)
            {
                modComponent.Start();
            }
        }
        public void Update()
        {
            foreach(ModComponent modComponent in modComponents)
            {
                modComponent.Update();
            }
        }
        public void OnEnable()
        {
            config = new ConfigManager(this);
            if (config.ComponentsItemSorting.Value)
                this.AddComponent(itemSorting);
            if (config.ComponentsStatsDisplay.Value)
                this.AddComponent(statsDisplay);
            if (config.ComponentsCommandImprovements.Value)
                this.AddComponent(commandImprovements);
            if (config.ComponentsDPSMeter.Value)
                this.AddComponent(DPSMeter);
            if (config.ComponentsBuffTimers.Value)
                this.AddComponent(buffTimers);
            if (config.ComponentsAdvancedIcons.Value)
                this.AddComponent(advancedIcons);
            if (config.ComponentsItemCounters.Value)
               this.AddComponent(itemCounters);
            if (config.ComponentsMisc.Value)
               this.AddComponent(misc);

            ItemStatsModIntegration = config.AdvancedIconsItemItemStatsIntegration.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("dev.ontrigger.itemstats");
            On.RoR2.UI.HUD.Awake += HUD_Awake;

            foreach (ModComponent modComponent in modComponents)
            {
                modComponent.Hook();
            }
        }

        public void OnDisable()
        {
            On.RoR2.UI.HUD.Awake -= HUD_Awake;
            foreach (ModComponent modComponent in modComponents)
            {
                modComponent.Unhook();
            }
        }
        internal void HUD_Awake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);
            HUD = self;
            foreach (ModComponent modComponent in modComponents)
            {
                modComponent.HUD_Awake();
            }
        }
        public void AddComponent(ModComponent modComponent)
        {
            modComponents.Add(modComponent);
        }
        public abstract class ModComponent
        {
            protected BetterUI mod;
            public ModComponent(BetterUI mod)
            {
                this.mod = mod;
            }
            internal virtual void Start() { }
            internal virtual void Update() { }
            internal virtual void Hook() { }
            internal virtual void Unhook() { }
            internal virtual void HUD_Awake() { }
        }
    }
}
