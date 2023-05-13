#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nazio_LT.Tools.Core.Internal
{
    public abstract class NPropertyDrawer : PropertyDrawer
    {
        private float m_propertyHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DefineProps(property);

            var baseRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            m_propertyHeight = 0f;

            DrawGUI(position, property, label, ref m_propertyHeight, ref baseRect);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        protected abstract void DefineProps(SerializedProperty property);
        protected abstract void DrawGUI(Rect position, SerializedProperty property, GUIContent label, ref float propertyHeight, ref Rect baseRect);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => m_propertyHeight;
    }
}
#endif