using UnityEditor;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomEditor(typeof(NSceneGroup)), CanEditMultipleObjects]
    public class NSceneGroupEditor : Editor
    {
        private SerializedProperty _key, _mainScene, _additivesScenes, _scenePlayList;

        private void OnEnable()
        {
            _key = serializedObject.FindProperty("key");
            _mainScene = serializedObject.FindProperty("mainScene");
            _additivesScenes = serializedObject.FindProperty("additivesScenes");
            _scenePlayList = serializedObject.FindProperty("scenePlayList");
        }

        public override void OnInspectorGUI()
        {
            NEditor.DrawMultipleLayoutProperty(new SerializedProperty[] { _key, _mainScene, _additivesScenes });
            _scenePlayList.intValue = EditorGUILayout.Popup(_scenePlayList.displayName, _scenePlayList.intValue, MusicManager.playlistArray);
        }
    }
}