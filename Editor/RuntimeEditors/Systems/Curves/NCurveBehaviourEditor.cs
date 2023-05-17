#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomEditor(typeof(NCurveBehaviour)), CanEditMultipleObjects]
    public class NCurveBehaviourEditor : Editor
    {
        private SerializedProperty m_editing_Prop, m_meshToDeform_Prop, m_meshType_Prop, m_curve_Prop, m_material_Prop,
                                m_meshPlacementSettings_Prop;

        private NCurve m_curve;

        private void OnEnable()
        {
            m_curve = (target as NCurveBehaviour).Curve;

            m_editing_Prop = serializedObject.FindProperty("editing");
            m_meshType_Prop = serializedObject.FindProperty("meshType");
            m_curve_Prop = serializedObject.FindProperty("curve");
            m_meshToDeform_Prop = serializedObject.FindProperty("meshToDeform");
            m_material_Prop = serializedObject.FindProperty("material");
            m_meshPlacementSettings_Prop = serializedObject.FindProperty("meshPlacementSettings");
        }

        public override void OnInspectorGUI()
        {
            GUIStyle editButtonStyle = new GUIStyle(GUI.skin.button);
            Texture2D buttTexture = new Texture2D(1, 1);
            buttTexture.SetPixel(0, 0, Color.red);
            buttTexture.Apply();
            editButtonStyle.normal.background = m_editing_Prop.boolValue ? buttTexture : GUI.skin.button.normal.background;
            if (GUILayout.Button(m_editing_Prop.boolValue ? "Stop Editing Curve" : "Edit Curve", editButtonStyle)) m_editing_Prop.boolValue = !m_editing_Prop.boolValue;

            EditorGUILayout.Space();

            NEditor.DrawMultipleLayoutProperty(new SerializedProperty[] { m_meshToDeform_Prop, m_material_Prop, m_meshType_Prop });

            if (m_meshType_Prop.intValue == 1) EditorGUILayout.PropertyField(m_meshPlacementSettings_Prop);

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Mesh")) Target.Gen();
            if (GUILayout.Button("Delete Meshs")) Target.DeleteMeshes();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(m_curve_Prop);

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        #region Scene

        private void OnSceneGUI()
        {
            if (m_curve == null)
            {
                Debug.LogError("Curve doesn't exist");
                return;
            }

            if (Target.Editing) m_curve.Update();

            for (int i = 0; i < m_curve.Handles.Count; i++)
            {
                if (Target.Editing) DisplayHandle(i);
                if (i < m_curve.Handles.Count - 1) DrawCurvePart(i, i + 1);
            }

            if (m_curve.Loop) DrawCurvePart(m_curve.Handles.Count - 1, 0);
        }

        private void DisplayHandle(int i)
        {
            NHandle handle = m_curve.Handles[i];
            Handles.color = Color.white;

            //Point
            Vector3 point = Handles.DoPositionHandle(handle.Point, Quaternion.identity);

            if (point != handle.Point) handle.MoveCentralPoint(point);

            if ((int)m_curve.Type == 0) return;

            //Handles
            DisplayControlPoint(ref handle.ForwardHelper, handle.Point, true);
            DisplayControlPoint(ref handle.BackHelper, handle.Point, handle.Broken);

            handle.UpdateHandle(true);
        }

        private void DisplayControlPoint(ref Vector3 handlePoint, Vector3 basePoint, bool positionHandle)
        {
            Handles.DrawLine(basePoint, handlePoint, 0.2f);

            if (positionHandle)
            {
                handlePoint = Handles.DoPositionHandle(handlePoint, Quaternion.identity);
                return;
            }

            Handles.Button(handlePoint, Quaternion.identity, 0.2f, 0.2f, Handles.CubeHandleCap);
        }

        private void DrawCurvePart(int startI, int endI)
        {
            Handles.color = Color.blue;
            switch ((int)m_curve.Type)
            {
                case 0://Line Type
                    Handles.DrawLine(m_curve.Handles[startI].Point, m_curve.Handles[endI].Point, 2f);
                    break;
                case 1://Bezier Type
                    Handles.DrawBezier(m_curve.Handles[startI].Point, m_curve.Handles[endI].Point, m_curve.Handles[startI].ForwardHelper, m_curve.Handles[endI].BackHelper, Color.blue, new Texture2D(10, 10), 2f);
                    break;
            }
        }

        #endregion

        public NCurveBehaviour Target => target as NCurveBehaviour;
    }
}
#endif