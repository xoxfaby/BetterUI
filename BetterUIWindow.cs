using System;
using System.Collections.Generic;
using System.Text;

using RoR2.UI;
using RoR2.UI.MainMenu;
using RoR2.UI.SkinControllers;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Reflection;

namespace BetterUI
{
    internal static class BetterUIWindow
    {
        static AssetBundle bundle;
        static GameObject modPanelPrefab;
        static GameObject modButtonPrefab;
        static GameObject betterUIWindowPrefab;
        internal static Transform safeZone;

        static BetterUIWindow()
        {
            bundle = AssetBundle.LoadFromMemory(Properties.Resources.betteruiassets);
            modPanelPrefab = bundle.LoadAsset<GameObject>($"Assets/ModPanel.prefab");
            modButtonPrefab = bundle.LoadAsset<GameObject>($"Assets/ModButton.prefab");
            betterUIWindowPrefab = bundle.LoadAsset<GameObject>($"Assets/BetterUIWindow.prefab");
        }

        internal static void Initialize()
        {
            BetterUIPlugin.Hooks.Add<BaseMainMenuScreen>(nameof(BaseMainMenuScreen.Awake), BaseMainMenuScreen_Awake);

            BetterUI.Utils.RegisterLanguageToken("TITLE_BETTERUI", "BetterUI");
            BetterUI.Utils.RegisterLanguageToken("DESCRIPTION_BETTERUI", "Open the BetterUI window");
        }


        static void BaseMainMenuScreen_Awake(Action<BaseMainMenuScreen> orig, BaseMainMenuScreen self)
        {
            safeZone = self.transform.Find("SafeZone");
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
            if (BetterUIWindow.safeZone != null)
            {
                var exists = spawnedGameObjects.TryGetValue(prefab, out var instance);
                if (!exists || instance == null)
                {
                    spawnedGameObjects[prefab] = GameObject.Instantiate(prefab, BetterUIWindow.safeZone);
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
            if (!loading)
            {
                loading = true;
                Addressables.LoadAssetAsync<GameObject>(prefabAddress).Completed += PrefabLoaded;
            }
        }

        void OnValidate()
        {
            if (!loading)
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

        void LoadAsset()
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
            field?.SetValue(component, asset);
            property?.SetValue(component, asset);
        }

        void Awake()
        {
            LoadAsset();
        }

        void OnValidate()
        {
            LoadAsset();
        }
    }

    [ExecuteAlways]
    internal class HGButtonLoader : MonoBehaviour
    {
        public ButtonSkinController buttonSkinController;
        public string SkinDataAddress;
        void Awake()
        {
            buttonSkinController = this.GetComponent<ButtonSkinController>();
            buttonSkinController.enabled = false;
            Addressables.LoadAssetAsync<UISkinData>(SkinDataAddress).Completed += SkinDataLoaded;
        }

        void OnValidate()
        {
            buttonSkinController = this.GetComponent<ButtonSkinController>();
            buttonSkinController.enabled = false;
            Addressables.LoadAssetAsync<UISkinData>(SkinDataAddress).Completed += SkinDataLoaded;
        }

        private void SkinDataLoaded(AsyncOperationHandle<UISkinData> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    buttonSkinController.skinData = obj.Result;
                    buttonSkinController.enabled = true;
                    break;
                case AsyncOperationStatus.Failed:
                    Debug.LogError("Sprite load failed.");
                    break;
                default:
                    // case AsyncOperationStatus.None:
                    break;
            }
        }
    }
}
