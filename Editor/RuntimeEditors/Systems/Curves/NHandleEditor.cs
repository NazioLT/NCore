#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nazio_LT.Tools.Core.Internal
{
    [CanEditMultipleObjects, CustomPropertyDrawer(typeof(NHandle))]
    public class NHandleEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var forwardHelper = property.FindPropertyRelative("ForwardHelper");
            var point = property.FindPropertyRelative("Point");
            var backHelper = property.FindPropertyRelative("BackHelper");

            var broken = property.FindPropertyRelative("Broken");

            var baseRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUI.PropertyField(baseRect, property);

            if (property.isExpanded)
            {
                var breakRect = new Rect(position.x, position.y + 20, position.width, EditorGUIUtility.singleLineHeight);
                broken.boolValue = EditorGUI.Toggle(breakRect, broken.displayName, broken.boolValue);

                var newRect = new Rect(position.x, position.y + 40f, position.width, EditorGUIUtility.singleLineHeight);
                string[] labels = new string[] { "Forward Helper", "Point", "Back Helper" };
                SerializedProperty[] props = new SerializedProperty[] { forwardHelper, point, backHelper };

                NEditor.DrawMultiplesGUI(newRect, 20f, props, labels, (i, rect, prop, label) => prop.vector3Value = NEditor.Vector3Field(rect, prop.vector3Value, label));
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 20 + (property.isExpanded ? 80f : 0f);
    }
}
#endif