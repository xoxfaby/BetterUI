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
    public class BetterUIPlugin : BetterUnityPlugin.BetterUnityPlugin<BetterUIPlugin>
    {
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


            instance = this;
            this.gameObject.hideFlags |= UnityEngine.HideFlags.HideAndDontSave;
            this.gameObject.AddComponent<BetterUI.Language.UpdateChecker>();
            BetterUI.Language.LoadLanguages();


            BetterUIWindow.Init();
        }
    }
}
