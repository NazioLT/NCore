#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomPropertyDrawer(typeof(SecondOrderDynamicsData))]
    public class SecondOrderDynamicsDataPropertyDrawer : NPropertyDrawer
    {
        private SerializedProperty m_frequency_Prop, m_damping_Prop, m_impulse_Prop;
        private AnimationCurve m_inputCurve, m_systemCurve;
        private MinMax m_systemCurveMinMax;

        private const int KEYCOUNT = 50;

        protected override void DefineProps(SerializedProperty property)
        {
            m_frequency_Prop = property.FindPropertyRelative("frequency");
            m_damping_Prop = property.FindPropertyRelative("damping");
            m_impulse_Prop = property.FindPropertyRelative("impulse");
        }

        protected override void DrawGUI(Rect position, SerializedProperty property, GUIContent label, ref float propertyHeight, ref Rect baseRect)
        {
            NEditor.DrawMultipleGUIClassic(position, 20f, new SerializedProperty[] { m_frequency_Prop, m_damping_Prop, m_impulse_Prop });
            NEditor.AdaptGUILine(ref position, ref propertyHeight, 3);

            if (EditorGUI.EndChangeCheck())
            {
                UpdateCurves();
            }

            Rect curveRect = new Rect(position.x, position.y, position.width, 8 * NEditor.SINGLE_LINE);
            Rect curveBounds = new Rect(0, m_systemCurveMinMax.Min, 1f, m_systemCurveMinMax.Max);

            EditorGUIUtility.DrawCurveSwatch(curveRect, m_inputCurve, null, Color.white, new Color(0, 0, 0, 0), curveBounds);
            EditorGUIUtility.DrawCurveSwatch(curveRect, m_systemCurve, null, Color.cyan, new Color(0, 0, 0, 0), curveBounds);

            NEditor.AdaptGUILine(ref position, ref propertyHeight, 8);
        }

        private void UpdateCurves()
        {
            Keyframe[] inputCurveKeyframes = new Keyframe[]{
                new Keyframe(0f,0f),
                new Keyframe(0.099f, 0f),
                new Keyframe(0.1f,1f),
                new Keyframe(1f,1f)
            };

            m_inputCurve = new AnimationCurve(inputCurveKeyframes);

            m_systemCurveMinMax = new MinMax();
            SecondOrderDynamics<float> system = new SecondOrderDynamics<float>(m_frequency_Prop.floatValue, m_damping_Prop.floatValue, m_impulse_Prop.floatValue, 0);

            Keyframe[] systemCurveKeyframes = new Keyframe[KEYCOUNT];
            float step = 1f / (float)KEYCOUNT;
            float t = 0;
            for (int i = 0; i < KEYCOUNT; i++)
            {
                t += step;
                float _y = system.Update(step, m_inputCurve.Evaluate(t));

                systemCurveKeyframes[i] = new Keyframe(t, _y);
                m_systemCurveMinMax.AddValue(_y);
            }

            m_systemCurve = new AnimationCurve(systemCurveKeyframes);
        }
    }
}
#endif