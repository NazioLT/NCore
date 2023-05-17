#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;

namespace Nazio_LT.Tools.Core.Internal
{
    public static class NEditor
    {
        #region Constants

        public const float SINGLE_LINE = 20f;

        #endregion

        public static void DrawHeader(string label, ref Rect baseRect, ref float propertyHeight)
        {
            EditorGUI.LabelField(baseRect, label, EditorStyles.boldLabel);
            NEditor.AdaptGUI(ref baseRect, ref propertyHeight, 20f);
        }

        #region Globals

        public static void DrawMultiplesGUI(Rect baseRect, float sizeY, SerializedProperty[] props, string[] labels, Action<int, Rect, SerializedProperty, string> callback)
        {
            for (int i = 0; i < props.Length; i++)
            {
                var rect = new Rect(baseRect.x, baseRect.y + i * sizeY, baseRect.width, EditorGUIUtility.singleLineHeight);
                callback(i, rect, props[i], labels[i]);
            }
        }

        public static void DrawMultipleLayoutProperty(SerializedProperty[] props)
        {
            foreach (var prop in props)
            {
                EditorGUILayout.PropertyField(prop);
            }
        }

        public static void DrawMultipleGUIClassic(Rect baseRect, float sizeY, SerializedProperty[] props) => DrawMultiplesGUI(baseRect, sizeY, props, GetPropLabels(props), (i, rect, prop, label) => EditorGUI.PropertyField(rect, prop));

        #endregion

        #region NewFields

        public static Vector3 Vector3Field(Rect position, Vector3 value, string label)
        {
            var labelRect = new Rect(position.x, position.y, position.width / 5f, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, label);
            var pointRect = new Rect(position.x + position.width / 5f, position.y, position.width * (4f / 5f), EditorGUIUtility.singleLineHeight);
            return EditorGUI.Vector3Field(pointRect, GUIContent.none, value);
        }

        /// <summary>Draw bool property, and then display properties only if bool property is true.</summary>
        public static void DrawDisplayIf(SerializedProperty conditionProperty, SerializedProperty[] otherProperties, ref Rect baseRect, ref float propertyHeight, bool spaceAtTheEnd)
        {
            conditionProperty.boolValue = EditorGUI.Toggle(baseRect, conditionProperty.displayName, conditionProperty.boolValue);
            AdaptGUI(ref baseRect, ref propertyHeight, 20f);

            DisplayIf(() => conditionProperty.boolValue, otherProperties, ref baseRect, ref propertyHeight, spaceAtTheEnd);
        }

        /// <summary>Display properties only if condition is true.</summary>
        public static void DisplayIf(Func<bool> condition, SerializedProperty[] otherProperties, ref Rect baseRect, ref float propertyHeight, bool spaceAtTheEnd)
        {
            if (!condition()) return;

            foreach (var prop in otherProperties)
            {
                EditorGUI.PropertyField(baseRect, prop);
                AdaptGUI(ref baseRect, ref propertyHeight, 20f);
            }

            if (spaceAtTheEnd) AdaptGUI(ref baseRect, ref propertyHeight, 20f);
        }

        /// <summary>Recalculate rect size and property total height with property height.</summary>
        public static void AdaptGUI(ref Rect baseRect, ref float propertyHeight, float rectHeight)
        {
            propertyHeight += rectHeight;
            baseRect.y += rectHeight;
        }

        /// <summary>Recalculate rect size and property total height with property height.</summary>
        public static void AdaptGUILine(ref Rect baseRect, ref float propertyHeight, int lineCount = 1) => AdaptGUI(ref baseRect, ref propertyHeight, SINGLE_LINE * lineCount);

        #endregion

        public static string[] GetPropLabels(SerializedProperty[] props)
        {
            string[] _result = new string[props.Length];

            for (var i = 0; i < props.Length; i++) _result[i] = props[i].displayName;

            return _result;
        }

        /// <summary>Will draw a scriptable object fold out settings.</summary>
        public static void DrawScriptableObjectSettingsEditor(UnityEngine.Object settings, System.Action onSettingUpdated, ref bool foldOut, ref Editor settingsEditor)
        {
            if (settings == null) return;

            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settings);

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (!foldOut) return;

                Editor.CreateCachedEditor(settings, null, ref settingsEditor);
                settingsEditor.OnInspectorGUI();

                if (!check.changed) return;

                if (onSettingUpdated != null) onSettingUpdated();
            }
        }
    }
}
#endif