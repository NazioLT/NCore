#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomPropertyDrawer(typeof(FoldableOutSerializedObject<>))]
    public class FoldableOutSerializedObjectPropertyDrawer : NPropertyDrawer
    {
        private SerializedProperty scriptableObject_Prop;

        protected override void DefineProps(SerializedProperty _property)
        {
            scriptableObject_Prop = _property.FindPropertyRelative("scriptableObject");
        }

        protected override void DrawGUI(Rect _position, SerializedProperty _property, GUIContent _label, ref float _propertyHeight, ref Rect _baseRect)
        {
            EditorGUI.PropertyField(_position, scriptableObject_Prop, new GUIContent(_property.displayName));
            _propertyHeight += 20f;
        }
    }
}
#endif