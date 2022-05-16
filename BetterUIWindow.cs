using System;
using System.Collections.Generic;
using System.Text;

using RoR2;
using RoR2.UI;
using RoR2.UI.MainMenu;
using RoR2.UI.SkinControllers;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Reflection;
using System.Collections;

namespace BetterUI
{
    internal static class BetterUIWindow
    {
        static AssetBundle bundle;
        static GameObject modPanelPrefab;
        static GameObject modButtonPrefab;
        static GameObject betterUIWindowPrefab;
        internal static Transform menuParent;

        static BetterUIWindow()
        {
            bundle = AssetBundle.LoadFromMemory(Properties.Resources.betteruiassets);
            modPanelPrefab = bundle.LoadAsset<GameObject>($"Assets/ModPanel.prefab");
            modButtonPrefab = bundle.LoadAsset<GameObject>($"Assets/ModButton.prefab");
            betterUIWindowPrefab = bundle.LoadAsset<GameObject>($"Assets/BetterUIWindow.prefab");
        }

        internal static void Init()
        {
            BetterUIPlugin.Hooks.Add<BaseMainMenuScreen>(nameof(BaseMainMenuScreen.Awake), BaseMainMenuScreen_Awake);

            BetterUI.Utils.RegisterLanguageToken("BETTERUI_TITLE", "BetterUI");
            BetterUI.Utils.RegisterLanguageToken("BETTERUI_TITLE_HOVER", "Open the BetterUI window");
            BetterUI.Utils.RegisterLanguageToken("BETTERUI_DONATE", "Donate to BetterUI");
            BetterUI.Utils.RegisterLanguageToken("BETTERUI_DISCORD", "Join our Discord");
            BetterUI.Utils.RegisterLanguageToken("BETTERUI_BUGREPORT", "Report a bug");
            BetterUI.Utils.RegisterLanguageToken("BETTERUI_CLOSE_WINDOW", "Close Window");
            BetterUI.Utils.RegisterLanguageToken("BETTERUI_MADEBY", "Made by XoXFaby");
            BetterUI.Utils.RegisterLanguageToken("BETTERUI_WINDOW_LABEL", "Thanks for using BetterUI! If you enjoy the mod, please consider donating to enable me to keep working on it as much as possible!\n\nIn the future, this window will allow you to edit the settings for BetterUI!");

            ViewablesCatalog.Node node = new ViewablesCatalog.Node("BetterUI", true, null);
            var buttonNode = new ViewablesCatalog.Node("Donate", false, node);
            buttonNode.shouldShowUnviewed = (UserProfile userProfile) => !userProfile.HasViewedViewable(buttonNode.fullName);

            ViewablesCatalog.AddNodeToRoot(node);
        }


        static void BaseMainMenuScreen_Awake(Action<BaseMainMenuScreen> orig, BaseMainMenuScreen self)
        {
            menuParent = self.transform;
            var transform = self.transform.Find("SafeZone/GenericMenuButtonPanel");
            var DescriptionGameObject = self.transform.Find("SafeZone/GenericMenuButtonPanel/JuicePanel/DescriptionPanel, Naked/ContentSizeFitter/DescriptionText");
            if (transform != null || DescriptionGameObject != null)
            {
                var DescriptionController = DescriptionGameObject.GetComponent<LanguageTextMeshController>();
                var modPanel = GameObject.Instantiate(modPanelPrefab, transform);
                foreach(var hgButton in modPanel.GetComponentsInChildren<HGButton>())
                {
                    hgButton.hoverLanguageTextMeshController = DescriptionController;
                }
            }
            orig(self);
        }
    }

    public class BetterUIEventFunctions : MonoBehaviour
    {
        static Dictionary<GameObject, GameObject> spawnedGameObjects = new Dictionary<GameObject, GameObject>();
        public void createWindow(GameObject prefab)
        {
            if (BetterUIWindow.menuParent != null)
            {
                var exists = spawnedGameObjects.TryGetValue(prefab, out var instance);
                if (!exists || instance == null)
                {
                    spawnedGameObjects[prefab] = GameObject.Instantiate(prefab, BetterUIWindow.menuParent);
                }
            }
        }
    }

    [ExecuteAlways]
    internal class PrefabLoader : MonoBehaviour
    {
        public string prefabAddress;
        private string loadedPrefab;
        private Boolean loading = false;
        GameObject instance;
        void Start()
        {
            LoadPrefab();
        }

        void OnValidate()
        {
            LoadPrefab();
        }

        void LoadPrefab()
        {
            if (!string.IsNullOrEmpty(prefabAddress) && !loading)
            {
                loading = true;
                Addressables.LoadAssetAsync<GameObject>(prefabAddress).Completed += PrefabLoaded;
            }
        }

        private void PrefabLoaded(AsyncOperationHandle<GameObject> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    if (loadedPrefab == prefabAddress) break;
                    if (instance != null) DestroyImmediate(instance);
                    var prefab = obj.Result;
                    instance = Instantiate(prefab);
                    SetRecursiveFlags(instance.transform);
                    instance.transform.SetParent(this.gameObject.transform, false);
                    loadedPrefab = prefabAddress;
                    loading = false;
                    break;
                case AsyncOperationStatus.Failed:
                    if (instance != null) DestroyImmediate(instance);
                    Debug.LogError("Prefab load failed.");
                    loading = false;
                    break;
                default:
                    // case AsyncOperationStatus.None:
                    break;
            }
        }

        static void SetRecursiveFlags(Transform transform)
        {
            transform.gameObject.hideFlags |= HideFlags.DontSave;
            foreach(Transform child in transform)
            {
                SetRecursiveFlags(child);
            }
        }
    }

    [ExecuteAlways]
    internal class AddressableAssetLoader : MonoBehaviour
    {
        public Component component;
        public string fieldName;
        public string assetAddress;

        private static readonly MethodInfo LoadAssetAsyncInfo = typeof(Addressables).GetMethod(nameof(Addressables.LoadAssetAsync), new[] { typeof(string) });

        void LoadAsset(bool dontSave = false)
        {
            var typ = component.GetType();
            var field = typ.GetField(fieldName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
            PropertyInfo property = null;
            if (field == null)
            {
                property = typ.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
                if (property == null) return;
            }
            var meth = LoadAssetAsyncInfo.MakeGenericMethod(field?.FieldType ?? property.PropertyType);
            var awaiter = meth.Invoke(null, new object[] { assetAddress });
            var wait = awaiter.GetType().GetMethod("WaitForCompletion", BindingFlags.Instance | BindingFlags.Public);
            var asset = wait.Invoke(awaiter, null);
            var assetObject = (UnityEngine.Object)asset;
            if (assetObject != null)
            {
                if (dontSave)
                {
                    assetObject.hideFlags |= HideFlags.DontSave;
                }
                field?.SetValue(component, asset);
                property?.SetValue(component, asset);
            }
        }
        IEnumerator WaitAndLoadAsset()
        {
            yield return new WaitUntil(() => Addressables.InternalIdTransformFunc != null);
            LoadAsset(true);
        }

        void Start()
        {
            LoadAsset();
        }

        void OnValidate()
        {
            if(gameObject.activeInHierarchy) StartCoroutine(WaitAndLoadAsset());
        }

    }
}
