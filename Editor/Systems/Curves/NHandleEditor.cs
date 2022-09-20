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
            var _forwardHelper = property.FindPropertyRelative("forwardHelper");
            var _point = property.FindPropertyRelative("point");
            var _backHelper = property.FindPropertyRelative("backHelper");

            var _break = property.FindPropertyRelative("broken");

            var _baseRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorGUI.PropertyField(_baseRect, property);

            if (property.isExpanded)
            {
                var _breakRect = new Rect(position.x, position.y + 20, position.width, EditorGUIUtility.singleLineHeight);
                _break.boolValue = EditorGUI.Toggle(_breakRect, _break.displayName, _break.boolValue);

                var _newRect = new Rect(position.x, position.y + 40f, position.width, EditorGUIUtility.singleLineHeight);
                string[] _labels = new string[] { "Forward Helper", "Point", "Back Helper" };
                SerializedProperty[] _props = new SerializedProperty[] { _forwardHelper, _point, _backHelper };

                NEditor.SetMultiplesGUI(_newRect, 20f, _props, _labels, (_i, _rect, _prop, _label) => _prop.vector3Value = NEditor.Vector3Field(_rect, _prop.vector3Value, _label));
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 20 + (property.isExpanded ? 80f : 0f);
    }
}
#endif