#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomPropertyDrawer(typeof(NMusic))]
    public class NMusicEditor : PropertyDrawer
    {
        // private SerializedProperty clip_Prop, playlist_Prop;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var clip_Prop = property.FindPropertyRelative("clip");
            var playlist_Prop = property.FindPropertyRelative("playlist");

            var enumRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.ObjectField(enumRect, clip_Prop, new GUIContent(clip_Prop.displayName));
            enumRect = new Rect(position.x, position.y +20f, position.width, EditorGUIUtility.singleLineHeight);
            playlist_Prop.intValue = EditorGUI.Popup(enumRect, playlist_Prop.displayName, playlist_Prop.intValue, MusicManager.playlistArray);

            // EditorGUILayout.ObjectField(clip_Prop);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 40f;
    }
}
#endif