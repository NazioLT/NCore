#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.Core.Internal
{
    [CanEditMultipleObjects, CustomPropertyDrawer(typeof(NCurve))]
    public class NCurveEditor : PropertyDrawer
    {
        private bool isEditing = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            float _propHeight = 0f;

            var _baseRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(_baseRect, property, new GUIContent(property.displayName), false);
            _propHeight += 20f;

            _baseRect = new Rect(position.x + 20f, position.y + 20f, position.width - 20f, EditorGUIUtility.singleLineHeight);
            if (property.isExpanded) DisplayOther(ref _propHeight, _baseRect, property);

            property.FindPropertyRelative("inspectorHeight").floatValue = _propHeight;
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private void DisplayOther(ref float _height, Rect _basePosition, SerializedProperty _property)
        {
            var _handle_Prop = _property.FindPropertyRelative("handles");
            var _loop_Prop = _property.FindPropertyRelative("loop");
            var _type_Prop = _property.FindPropertyRelative("type");
            var _isEditing_Prop = _property.FindPropertyRelative("isEditing");
            float _baseHeight = 0;

            var _baseRect = new Rect(_basePosition.x, _basePosition.y + _baseHeight, _basePosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(_baseRect, _loop_Prop);
            _baseHeight += 20f;

            _baseRect = new Rect(_basePosition.x, _basePosition.y + _baseHeight, _basePosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(_baseRect, _type_Prop);
            _baseHeight += 20f;

            _baseRect = new Rect(_basePosition.x, _basePosition.y + _baseHeight, _basePosition.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(_baseRect, _handle_Prop);
            _baseHeight += (_handle_Prop.isExpanded ? EditorGUI.GetPropertyHeight(_handle_Prop, true) : 20f);

            _height += _baseHeight;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => property.FindPropertyRelative("inspectorHeight").floatValue;
    }
}
#endif