#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomEditor(typeof(NCurveBehaviour)), CanEditMultipleObjects]
    public class NCurveBehaviourEditor : Editor
    {
        private SerializedProperty editing_Prop, meshToDeform_Prop, meshType_Prop, curve_Prop, material_Prop, 
                                meshPlacementSettings_Prop;

        private NCurve curve;

        private void OnEnable()
        {
            curve = (target as NCurveBehaviour).Curve;

            editing_Prop = serializedObject.FindProperty("editing");
            meshType_Prop = serializedObject.FindProperty("meshType");
            curve_Prop = serializedObject.FindProperty("curve");
            meshToDeform_Prop = serializedObject.FindProperty("meshToDeform");
            material_Prop = serializedObject.FindProperty("material");
            meshPlacementSettings_Prop = serializedObject.FindProperty("meshPlacementSettings");
        }

        public override void OnInspectorGUI()
        {
            GUIStyle _editButtonStyle = new GUIStyle(GUI.skin.button);
            Texture2D _buttTexture = new Texture2D(1, 1);
            _buttTexture.SetPixel(0, 0, Color.red);
            _buttTexture.Apply();
            _editButtonStyle.normal.background = editing_Prop.boolValue ? _buttTexture : GUI.skin.button.normal.background;
            if (GUILayout.Button(editing_Prop.boolValue ? "Stop Editing Curve" : "Edit Curve", _editButtonStyle)) editing_Prop.boolValue = !editing_Prop.boolValue;

            EditorGUILayout.Space();

            NEditor.DrawMultipleLayoutProperty(new SerializedProperty[] { meshToDeform_Prop, material_Prop, meshType_Prop });

            if(meshType_Prop.intValue == 1) EditorGUILayout.PropertyField(meshPlacementSettings_Prop);

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Mesh")) Target.Gen();
            if (GUILayout.Button("Delete Meshs")) Target.DeleteMeshes();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(curve_Prop);

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        #region Scene

        private void OnSceneGUI()
        {
            if(Target.editing) curve.Update();

            for (int i = 0; i < curve.handles.Count; i++)
            {
                if (Target.editing) DisplayHandle(i);
                if (i < curve.handles.Count - 1) DrawCurvePart(i, i + 1);
            }

            if (curve.loop) DrawCurvePart(curve.handles.Count - 1, 0);
        }

        private void DisplayHandle(int i)
        {
            NHandle _handle = curve.handles[i];
            Handles.color = Color.white;

            //Point
            Vector3 _point = Handles.DoPositionHandle(_handle.point, Quaternion.identity);

            if (_point != _handle.point) _handle.MoveCentralPoint(_point);

            if ((int)curve.type == 0) return;

            //Handles
            DisplayControlPoint(ref _handle.forwardHelper, _handle.point, true);
            DisplayControlPoint(ref _handle.backHelper, _handle.point, _handle.broken);

            _handle.UpdateHandle(true);
        }

        private void DisplayControlPoint(ref Vector3 _handlePoint, Vector3 _basePoint, bool _positionHandle)
        {
            Handles.DrawLine(_basePoint, _handlePoint, 0.2f);

            if (_positionHandle)
            {
                _handlePoint = Handles.DoPositionHandle(_handlePoint, Quaternion.identity);
                return;
            }

            Handles.Button(_handlePoint, Quaternion.identity, 0.2f, 0.2f, Handles.CubeHandleCap);
        }

        private void DrawCurvePart(int _startI, int _endI)
        {
            Handles.color = Color.blue;
            switch ((int)curve.type)
            {
                case 0://Line Type
                    Handles.DrawLine(curve.handles[_startI].point, curve.handles[_endI].point, 2f);
                    break;
                case 1://Bezier Type
                    Handles.DrawBezier(curve.handles[_startI].point, curve.handles[_endI].point, curve.handles[_startI].forwardHelper, curve.handles[_endI].backHelper, Color.blue, new Texture2D(10, 10), 2f);
                    break;
            }
        }

        #endregion

        public NCurveBehaviour Target => target as NCurveBehaviour;
    }
}
#endif