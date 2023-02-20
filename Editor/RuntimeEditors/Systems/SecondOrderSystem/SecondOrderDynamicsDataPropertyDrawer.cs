#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomPropertyDrawer(typeof(SecondOrderDynamicsData))]
    public class SecondOrderDynamicsDataPropertyDrawer : NPropertyDrawer
    {
        private SerializedProperty frequency_Prop, damping_Prop, impulse_Prop;
        private AnimationCurve inputCurve, systemCurve;
        private MinMax systemCurveMinMax;

        private const int KEYCOUNT = 200;

        protected override void DefineProps(SerializedProperty _property)
        {
            frequency_Prop = _property.FindPropertyRelative("frequency");
            damping_Prop = _property.FindPropertyRelative("damping");
            impulse_Prop = _property.FindPropertyRelative("impulse");
        }

        protected override void DrawGUI(Rect _position, SerializedProperty _property, GUIContent _label, ref float _propertyHeight, ref Rect _baseRect)
        {
            NEditor.DrawMultipleGUIClassic(_position, 20f, new SerializedProperty[] { frequency_Prop, damping_Prop, impulse_Prop });
            NEditor.AdaptGUILine(ref _position, ref _propertyHeight, 3);

            if (EditorGUI.EndChangeCheck())
            {
                UpdateCurves();
            }

            Rect _curveRect = new Rect(_position.x, _position.y, _position.width, 8 * NEditor.SINGLE_LINE);
            Rect _curveBounds = new Rect(0, systemCurveMinMax.Min, 1f, systemCurveMinMax.Max);

            EditorGUIUtility.DrawCurveSwatch(_curveRect, inputCurve, null, Color.white, new Color(0, 0, 0, 0), _curveBounds);
            EditorGUIUtility.DrawCurveSwatch(_curveRect, systemCurve, null, Color.cyan, new Color(0, 0, 0, 0), _curveBounds);

            NEditor.AdaptGUILine(ref _position, ref _propertyHeight, 8);
        }

        private void UpdateCurves()
        {
            Keyframe[] _inputCurveKeyframes = new Keyframe[]{
                new Keyframe(0f,0f),
                new Keyframe(0.099f, 0f),
                new Keyframe(0.1f,1f),
                new Keyframe(1f,1f)
            };

            inputCurve = new AnimationCurve(_inputCurveKeyframes);

            systemCurveMinMax = new MinMax();
            SecondOrderDynamics<float> _system = new SecondOrderDynamics<float>(frequency_Prop.floatValue, damping_Prop.floatValue, impulse_Prop.floatValue, 0);

            Keyframe[] _systemCurveKeyframes = new Keyframe[KEYCOUNT];
            float _step = 1f / (float)KEYCOUNT;
            float _t = 0;
            for (int i = 0; i < KEYCOUNT; i++)
            {
                _t += _step;
                float _y = _system.Update(_step, inputCurve.Evaluate(_t));

                _systemCurveKeyframes[i] = new Keyframe(_t, _y);
                systemCurveMinMax.AddValue(_y);
            }

            systemCurve = new AnimationCurve(_systemCurveKeyframes);
        }
    }
}
#endif