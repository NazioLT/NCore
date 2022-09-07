#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

namespace Nazio_LT.Tools.Core.Internal
{
    public static class CreateUtilty
    {
        public static void CreatePrefab(string _path, bool _asPrefab)
        {
            GameObject _new = PrefabUtility.InstantiatePrefab(Resources.Load(_path)) as GameObject;
            if (!_asPrefab) PrefabUtility.UnpackPrefabInstance(_new, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            Place(_new);
        }

        public static void CreateSample(string _name, params Type[] _types)
        {
            GameObject _new = ObjectFactory.CreateGameObject(_name, _types);
            Place(_new);
        }

        public static void Place(GameObject _obj)
        {
            SceneView _lastView = SceneView.lastActiveSceneView;
            _obj.transform.position = _lastView ? _lastView.pivot : Vector3.zero;

            StageUtility.PlaceGameObjectInCurrentStage(_obj);
            GameObjectUtility.EnsureUniqueNameForSibling(_obj);

            Undo.RegisterCreatedObjectUndo(_obj, $"Create Object : {_obj.name}");
            Selection.activeGameObject = _obj;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
#endif