#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.Core.Internal
{
    [CanEditMultipleObjects, CustomPropertyDrawer(typeof(NCurve))]
    public class NCurveEditor : PropertyDrawer
    {
        private bool m_isEditing = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            float propHeight = 0f;

            var baseRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(baseRect, property, new GUIContent(property.displayName), false);
            propHeight += 20f;

            baseRect = new Rect(position.x + 20f, position.y + 20f, position.width - 20f, EditorGUIUtility.singleLineHeight);
            if (property.isExpanded) DisplayOther(ref propHeight, baseRect, property);

            property.FindPropertyRelative("m_inspectorHeight").floatValue = propHeight;
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private void DisplayOther(ref float height, Rect basePosition, SerializedProperty property)
        {
            var handle_Prop = property.FindPropertyRelative("m_handles ");
            var loop_Prop = property.FindPropertyRelative("Loop");
            var type_Prop = property.FindPropertyRelative("Type");
            float baseHeight = 0;

            var baseRect = new Rect(basePosition.x, basePosition.y + baseHeight, basePosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(baseRect, loop_Prop);
            baseHeight += 20f;

            baseRect = new Rect(basePosition.x, basePosition.y + baseHeight, basePosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(baseRect, type_Prop);
            baseHeight += 20f;

            baseRect = new Rect(basePosition.x, basePosition.y + baseHeight, basePosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(baseRect, handle_Prop);
            baseHeight += (handle_Prop.isExpanded ? EditorGUI.GetPropertyHeight(handle_Prop, true) : 20f);

            height += baseHeight;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => property.FindPropertyRelative("inspectorHeight").floatValue;
    }
}
#endif