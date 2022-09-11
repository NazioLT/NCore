#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace Nazio_LT.Tools.Core.Internal
{
    public static class NEditor
    {
        #region Globals

        public static void SetMultiplesGUI(Rect _baseRect, float _sizeY, SerializedProperty[] _props, string[] _labels, Action<int, Rect, SerializedProperty, string> _callback)
        {
            for (int i = 0; i < _props.Length; i++)
            {
                var _rect = new Rect(_baseRect.x, _baseRect.y + i * _sizeY, _baseRect.width, EditorGUIUtility.singleLineHeight);
                _callback(i, _rect, _props[i], _labels[i]);
            }
        }

        #endregion

        #region NewFields

        public static Vector3 Vector3Field(Rect _position, Vector3 _value, string _label)
        {
            var _labelRect = new Rect(_position.x, _position.y, _position.width / 5f, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(_labelRect, _label);
            var _pointRect = new Rect(_position.x + _position.width / 5f, _position.y, _position.width * (4f / 5f), EditorGUIUtility.singleLineHeight);
            return EditorGUI.Vector3Field(_pointRect, GUIContent.none, _value);
        }

        #endregion
    }
}
#endif