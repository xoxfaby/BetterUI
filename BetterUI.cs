using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using BepInEx;
using MonoMod.RuntimeDetour;
using System.Reflection;

namespace BetterUI
{
    [BepInDependency("dev.ontrigger.itemstats", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "2.0.4.1")]
    public class BetterUIPlugin : BaseUnityPlugin
    {
        public static BetterUIPlugin instance;

        internal delegate void HUDAwakeEvent(RoR2.UI.HUD self);
        internal delegate void BetterUIEvent(BetterUIPlugin self);

        internal static event BetterUIEvent onStart;
        internal static event BetterUIEvent onEnable;
        internal static event BetterUIEvent onDisable;
        internal static event BetterUIEvent onUpdate;
        internal static event HUDAwakeEvent onHUDAwake;

        internal ConfigManager config;
        internal bool ItemStatsModIntegration;
        internal static RoR2.UI.HUD HUD;

        public static StringBuilder sharedStringBuilder = new StringBuilder();

        BetterUIPlugin()
        {
            BetterUIPlugin.instance = this;
        }

        public void Awake()
        {
            BepInExPatcher.DoPatching();
            config = new ConfigManager();
            if (config.ComponentsItemSorting.Value)
                ItemSorting.Hook();
            if (config.ComponentsStatsDisplay.Value)
                StatsDisplay.Hook();
            if (config.ComponentsCommandImprovements.Value)
                CommandImprovements.Hook();
            if (config.ComponentsDPSMeter.Value)
                DPSMeter.Hook();
            if (config.ComponentsBuffTimers.Value)
                BuffTimers.Hook();
            if (config.ComponentsAdvancedIcons.Value)
                AdvancedIcons.Hook();
            if (config.ComponentsItemCounters.Value)
                ItemCounters.Hook();
            if (config.ComponentsMisc.Value)
                Misc.Hook();
        }

        public void Start()
        {
            if (onStart != null)
            {
                onStart.Invoke(this);
            }
        }
        public void Update()
        {
            if (onUpdate != null)
            {
                onUpdate.Invoke(this);
            }
        }

        public void OnEnable()
        {

            ItemStatsModIntegration = config.AdvancedIconsItemItemStatsIntegration.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("dev.ontrigger.itemstats");
            HookManager.Add<RoR2.UI.HUD>("Awake", HUD_Awake);

            if (onEnable != null)
            {
                onEnable.Invoke(this);
            }
        }

        public void OnDisable()
        {
            if (onDisable != null)
            {
                onDisable.Invoke(this);
            }
        }

        internal static void HUD_Awake(Action<RoR2.UI.HUD> orig, RoR2.UI.HUD self)
        {
            orig(self);
            HUD = self;
            if (onHUDAwake != null)
            {
                onHUDAwake.Invoke(self);
            }
        }
    }
}