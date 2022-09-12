#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

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

            var _baseRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            string[] _labels = new string[] { "Forward Helper", "Point", "Back Helper" };
            SerializedProperty[] _props = new SerializedProperty[] { _forwardHelper, _point, _backHelper };

            NEditor.SetMultiplesGUI(_baseRect, 20f, _props, _labels, (_i, _rect, _prop, _label) => _prop.vector3Value = NEditor.Vector3Field(_rect, _prop.vector3Value, _label));


            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 60f;
    }
}
#endif