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
    [BepInPlugin("com.xoxfaby.BetterUI", "BetterUI", "2.1.1.1")]
    public class BetterUIPlugin : BetterAPI.BetterUnityPlugin<BetterUIPlugin>
    {

        internal delegate void HUDAwakeEvent(RoR2.UI.HUD self);
        internal static event HUDAwakeEvent onHUDAwake;

        internal static bool ItemStatsModIntegration;
        internal static RoR2.UI.HUD HUD;

        public static StringBuilder sharedStringBuilder = new StringBuilder();

        public override BaseUnityPlugin typeReference => throw new NotImplementedException();

        protected override void Awake()
        {
            base.Awake();
            BepInExPatcher.DoPatching();
            if (ConfigManager.ComponentsItemSorting.Value)
                ItemSorting.Hook();
            if (ConfigManager.ComponentsStatsDisplay.Value)
                StatsDisplay.Hook();
            if (ConfigManager.ComponentsCommandImprovements.Value)
                CommandImprovements.Hook();
            if (ConfigManager.ComponentsDPSMeter.Value)
                DPSMeter.Hook();
            if (ConfigManager.ComponentsBuffTimers.Value)
                BuffTimers.Hook();
            if (ConfigManager.ComponentsAdvancedIcons.Value)
                AdvancedIcons.Hook();
            if (ConfigManager.ComponentsItemCounters.Value)
                ItemCounters.Hook();
            if (ConfigManager.ComponentsMisc.Value)
                Misc.Hook();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            ItemStatsModIntegration = ConfigManager.AdvancedIconsItemItemStatsIntegration.Value && BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("dev.ontrigger.itemstats");
            BetterUIPlugin.Hooks.Add<RoR2.UI.HUD>("Awake", HUD_Awake);
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