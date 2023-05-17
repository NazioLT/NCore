#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

namespace Nazio_LT.Tools.Core.Internal
{
    public static class CreateUtilty
    {
        public static void CreatePrefab(string path, bool asPrefab)
        {
            GameObject newObj = PrefabUtility.InstantiatePrefab(Resources.Load(path)) as GameObject;
            if (!asPrefab) PrefabUtility.UnpackPrefabInstance(newObj, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            Place(newObj);
        }

        public static void CreateSample(string name, params Type[] types)
        {
            GameObject newObj = ObjectFactory.CreateGameObject(name, types);
            Place(newObj);
        }

        public static void Place(GameObject obj)
        {
            SceneView lastView = SceneView.lastActiveSceneView;
            obj.transform.position = lastView ? lastView.pivot : Vector3.zero;
            obj.transform.SetParent(Selection.activeTransform);

            StageUtility.PlaceGameObjectInCurrentStage(obj);
            GameObjectUtility.EnsureUniqueNameForSibling(obj);

            Undo.RegisterCreatedObjectUndo(obj, $"Create Object : {obj.name}");
            Selection.activeGameObject = obj;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            obj.transform.localScale = Vector3.one;
        }
    }
}
#endif