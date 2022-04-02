using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BetterUI
{
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
                    if (instance != null) Destroy(instance);
                    var prefab = obj.Result;
                    instance = Instantiate(prefab);
                    instance.hideFlags |= HideFlags.DontSave;
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
    }
    [ExecuteAlways]
    internal class BetterUIAssetLoader : MonoBehaviour
    {
        public Image image;
        public string spriteAddress;

        void Start()
        {
            Addressables.LoadAssetAsync<Sprite>(spriteAddress).Completed += SpriteLoaded;
        }

        void OnValidate()
        {
            Addressables.LoadAssetAsync<Sprite>(spriteAddress).Completed += SpriteLoaded;
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
}
