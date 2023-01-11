#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace Nazio_LT.Tools.Core.Internal
{
    public static class NEditor
    {
        #region Globals

        public static void DrawMultiplesGUI(Rect _baseRect, float _sizeY, SerializedProperty[] _props, string[] _labels, Action<int, Rect, SerializedProperty, string> _callback)
        {
            for (int i = 0; i < _props.Length; i++)
            {
                var _rect = new Rect(_baseRect.x, _baseRect.y + i * _sizeY, _baseRect.width, EditorGUIUtility.singleLineHeight);
                _callback(i, _rect, _props[i], _labels[i]);
            }
        }

        public static void DrawMultipleLayoutProperty(SerializedProperty[] _props)
        {
            foreach (var _prop in _props)
            {
                EditorGUILayout.PropertyField(_prop);
            }
        }

        public static void DrawMultipleGUIClassic(Rect _baseRect, float _sizeY, SerializedProperty[] _props) => DrawMultiplesGUI(_baseRect, _sizeY, _props, GetPropLabels(_props), (_i, _rect, _prop, _label) => EditorGUI.PropertyField(_rect, _prop));

        #endregion

        #region NewFields

        public static Vector3 Vector3Field(Rect _position, Vector3 _value, string _label)
        {
            var _labelRect = new Rect(_position.x, _position.y, _position.width / 5f, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_labelRect, _label);
            var _pointRect = new Rect(_position.x + _position.width / 5f, _position.y, _position.width * (4f / 5f), EditorGUIUtility.singleLineHeight);
            return EditorGUI.Vector3Field(_pointRect, GUIContent.none, _value);
        }

        /// <summary>Draw bool property, and then display properties only if bool property is true.</summary>
        public static void DrawDisplayIf(SerializedProperty _conditionProperty, SerializedProperty[] _otherProperties, ref Rect _baseRect, ref float _propertyHeight, bool _spaceAtTheEnd)
        {
            _conditionProperty.boolValue = EditorGUI.Toggle(_baseRect, _conditionProperty.displayName, _conditionProperty.boolValue);
            AdaptGUI(ref _baseRect, ref _propertyHeight, 20f);

            DisplayIf(() => _conditionProperty.boolValue, _otherProperties, ref _baseRect, ref _propertyHeight, _spaceAtTheEnd);
        }

        /// <summary>Display properties only if condition is true.</summary>
        public static void DisplayIf(Func<bool> _condition, SerializedProperty[] _otherProperties, ref Rect _baseRect, ref float _propertyHeight, bool _spaceAtTheEnd)
        {
            if (!_condition()) return;

            foreach (var _prop in _otherProperties)
            {
                EditorGUI.PropertyField(_baseRect, _prop);
                AdaptGUI(ref _baseRect, ref _propertyHeight, 20f);
            }
            
            if(_spaceAtTheEnd) AdaptGUI(ref _baseRect, ref _propertyHeight, 20f);
        }

        /// <summary>Recalculate rect size and property total height with property height.</summary>
        public static void AdaptGUI(ref Rect _baseRect, ref float _propertyHeight, float _rectHeight)
        {
            _propertyHeight += _rectHeight;
            _baseRect.y += _rectHeight;
        }

        #endregion

        public static string[] GetPropLabels(SerializedProperty[] _props)
        {
            string[] _result = new string[_props.Length];

            for (var i = 0; i < _props.Length; i++) _result[i] = _props[i].displayName;

            return _result;
        }
    }
}
#endif