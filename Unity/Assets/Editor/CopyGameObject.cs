using UnityEditor;
using UnityEngine;

namespace BetterUI.EditorTools
{
    public static class CopyGameObject
    {
        [MenuItem("GameObject/Duplicate GameObject", false, 11)]
        static void DuplicateGameObject()
        {
            CloneGameObject(Selection.activeGameObject);
        }

        /// <summary>
        /// Only allow path copying if 1 object is selected.
        /// </summary>
        [MenuItem("GameObject / Duplicate GameObject", true)]
        static bool DuplicateGameObjectValidation() => Selection.gameObjects.Length == 1;

        public static GameObject CloneGameObject(GameObject originalObject, GameObject parent = null)
        {
            var newGameObject = new GameObject(originalObject.name);
            newGameObject.transform.parent = parent?.transform;

            foreach (Transform originalChildObject in originalObject.transform)
            {
                CloneGameObject(originalChildObject.gameObject, newGameObject);
            }

            foreach (var component in originalObject.GetComponents<Component>())
            {
                if (UnityEditorInternal.ComponentUtility.CopyComponent(component))
                {
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(newGameObject);
                }
            }

            return newGameObject;
        }
    }
}
