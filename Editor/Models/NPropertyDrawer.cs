#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nazio_LT.Tools.Core.Internal
{
    public abstract class NPropertyDrawer : PropertyDrawer
    {
        private float propertyHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DefineProps(property);

            var _baseRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            propertyHeight = 0f;

            DrawGUI(position, property, label, ref propertyHeight, ref _baseRect);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        protected abstract void DefineProps(SerializedProperty _property);
        protected abstract void DrawGUI(Rect _position, SerializedProperty _property, GUIContent _label, ref float _propertyHeight, ref Rect _baseRect);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => propertyHeight;
    }
}
#endif