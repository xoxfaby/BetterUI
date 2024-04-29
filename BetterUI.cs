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
    [BepInDependency("com.xoxfaby.BetterAPI", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(GUID, Name, Version)]
    [BepInIncompatibility(MenuUIRemoverGUID)]
    [BepInIncompatibility(nomorebetteruibuttonGUID)]
    [BepInIncompatibility(BUICancelGUID)]
    public class BetterUIPlugin : BetterUnityPlugin.BetterUnityPlugin<BetterUIPlugin>
    {
        public const string MenuUIRemoverGUID = "pseudopulse.MenuUIRemover";
        public const string nomorebetteruibuttonGUID = "com.example.nomorebetteruibutton";
        public const string BUICancelGUID = "com.Dragonyck.BUICancel";
        public readonly string[] Incompatibilities = new [] {
            MenuUIRemoverGUID,
            nomorebetteruibuttonGUID,
            BUICancelGUID
        };
        public const string GUID = "com.xoxfaby.BetterUI";
        public const string Name = "BetterUI";
        public const string Version = "2.7.2";
        internal static BetterUIPlugin instance;
        internal delegate void HUDAwakeEvent(RoR2.UI.HUD self);
        internal static event HUDAwakeEvent onHUDAwake;

        internal static bool BetterAPIModIntegration = false;
        internal static RoR2.UI.HUD hud;
        internal static RoR2.UI.ObjectivePanelController objectivePanelController;

        public static StringBuilder sharedStringBuilder = new StringBuilder();

        public override BaseUnityPlugin typeReference => throw new NotImplementedException();

        protected override void Awake()
        {
            base.Awake();

            if (this.Incompatibilities.Any(x => !this.Info.Incompatibilities.Select(x => x.IncompatibilityGUID).Contains(x)))
            { 
                UnityEngine.Object.Destroy(this);
                UnityEngine.Debug.LogError("It appears BepInEx is not functioning correctly, disabling BetterUI.");
                return;
            }

            instance = this;
            this.gameObject.hideFlags |= UnityEngine.HideFlags.HideAndDontSave;
            this.gameObject.AddComponent<BetterUI.Language.UpdateChecker>();
            BetterUI.Language.LoadLanguages();

            if (ConfigManager.ComponentsItemSorting.Value)
                ItemSorting.Hook();
            if (ConfigManager.ComponentsStatsDisplay.Value)
                StatsDisplay.Hook();
            if (ConfigManager.ComponentsCommandImprovements.Value)
                CommandImprovements.Hook();
            if (ConfigManager.ComponentsDPSMeter.Value)
                DPSMeter.Initialize();
            if (ConfigManager.ComponentsBuffTimers.Value)
                Buffs.Hook();
            if (ConfigManager.ComponentsAdvancedIcons.Value)
                AdvancedIcons.Hook();
            if (ConfigManager.ComponentsItemCounters.Value)
                ItemCounters.Hook();
            if (ConfigManager.ComponentsMisc.Value)
                Misc.Hook();

            BetterUIWindow.Init();
            RoR2.ItemCatalog.availability.CallWhenAvailable(ItemStats.Initialize);
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            BetterAPIModIntegration = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.xoxfaby.BetterAPI");
            BetterUIPlugin.Hooks.Add<RoR2.UI.HUD>("Awake", HUD_Awake);
        }

        internal static void HUD_Awake(Action<RoR2.UI.HUD> orig, RoR2.UI.HUD self)
        {
            orig(self);
            hud = self;
            objectivePanelController = self.GetComponentInChildren<RoR2.UI.ObjectivePanelController>(true);
            if (onHUDAwake != null)
            {
                onHUDAwake.Invoke(hud);
            }
        }
    }
}
