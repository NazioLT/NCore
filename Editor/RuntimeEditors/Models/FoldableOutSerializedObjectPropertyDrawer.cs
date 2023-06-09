#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomPropertyDrawer(typeof(FoldableOutSerializedObject<>))]
    public class FoldableOutSerializedObjectPropertyDrawer : NPropertyDrawer
    {
        private SerializedProperty m_scriptableObject_Prop;

        protected override void DefineProps(SerializedProperty property)
        {
            m_scriptableObject_Prop = property.FindPropertyRelative("m_scriptableObject");
        }

        protected override void DrawGUI(Rect position, SerializedProperty property, GUIContent label, ref float propertyHeight, ref Rect baseRect)
        {
            EditorGUI.PropertyField(position, m_scriptableObject_Prop, new GUIContent(property.displayName));
            propertyHeight += 20f;
        }
    }
}
#endif