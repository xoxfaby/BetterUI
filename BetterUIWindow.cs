using System;
using System.Collections.Generic;
using System.Text;

using RoR2.UI;
using RoR2.UI.MainMenu;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BetterUI
{
    internal static class BetterUIWindow
    {
        static AssetBundle bundle;
        static GameObject prefab;

        static BetterUIWindow()
        {
            bundle = AssetBundle.LoadFromMemory(Properties.Resources.betteruiassets);
            prefab = bundle.LoadAsset<GameObject>($"Assets/ModPanel.prefab");
        }

        internal static void Initialize()
        {
            BetterUIPlugin.Hooks.Add<BaseMainMenuScreen>(nameof(BaseMainMenuScreen.Awake), BaseMainMenuScreen_Awake);
        }

        static void BaseMainMenuScreen_Awake(Action<BaseMainMenuScreen> orig, BaseMainMenuScreen self)
        {
            var transform = self.transform.Find("SafeZone/GenericMenuButtonPanel");
            var DescriptionGameObject = self.transform.Find("SafeZone/GenericMenuButtonPanel/JuicePanel/DescriptionPanel, Naked/ContentSizeFitter/DescriptionText");
            var DescriptionController = DescriptionGameObject.GetComponent<LanguageTextMeshController>();
            if (transform != null)
            {
                var hgButtons = prefab.GetComponentsInChildren<HGButton>();
                foreach(var hgButton in hgButtons)
                {
                    hgButton.hoverLanguageTextMeshController = DescriptionController;
                }
                UnityEngine.Debug.Log("Boop: Panel Attached");
                GameObject.Instantiate(prefab, transform);
                
            }
            else
            {
                UnityEngine.Debug.Log("Boop: Panel Not Attached");
            }
            orig(self);
        }
    }


    [ExecuteAlways]
    internal class PrefabLoader : MonoBehaviour
    {
        public string prefabAddress;
        GameObject instance;
        void Start()
        {
            Addressables.LoadAssetAsync<GameObject>(prefabAddress).Completed += PrefabLoaded;
        }

        void OnValidate()
        {
            Addressables.LoadAssetAsync<GameObject>(prefabAddress).Completed += PrefabLoaded;
        }

        private void PrefabLoaded(AsyncOperationHandle<GameObject> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    if (instance != null) DestroyImmediate(instance);
                    var prefab = obj.Result;
                    instance = Instantiate(prefab);
                    SetRecursiveFlags(instance.transform);
                    instance.transform.SetParent(this.gameObject.transform, false);
                    break;
                case AsyncOperationStatus.Failed:
                    Debug.LogError("Prefab load failed.");
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
    internal class SpriteLoader : MonoBehaviour
    {
        public Image image;
        public string spriteAddress;

        void LoadAsset()
        {
            image = this.GetComponent<Image>();
            Addressables.LoadAssetAsync<Sprite>(spriteAddress).Completed += SpriteLoaded;
        }

        void Start()
        {
            LoadAsset();
        }

        void OnValidate()
        {
            LoadAsset();
        }

        private void SpriteLoaded(AsyncOperationHandle<Sprite> obj)
            {
                switch (obj.Status)
                {
                    case AsyncOperationStatus.Succeeded:
                        image.sprite = obj.Result;
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

    [ExecuteAlways]
    internal class HGButtonLoader : MonoBehaviour
    {
        public HGButton hgButton;
        public string imageOnHoverAddress;

        void Start()
        {
            hgButton = this.GetComponent<HGButton>();
            Addressables.LoadAssetAsync<Image>(imageOnHoverAddress).Completed += ImageLoaded;
        }

        void OnValidate()
        {
            hgButton = this.GetComponent<HGButton>();
            Addressables.LoadAssetAsync<Image>(imageOnHoverAddress).Completed += ImageLoaded;
        }

        private void ImageLoaded(AsyncOperationHandle<Image> obj)
        {
            switch (obj.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    hgButton.imageOnHover = obj.Result;
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
