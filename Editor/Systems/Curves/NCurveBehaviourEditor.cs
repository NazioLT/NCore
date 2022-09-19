#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Nazio_LT.Tools.Core.Internal
{
    [CustomEditor(typeof(NCurveBehaviour)), CanEditMultipleObjects]
    public class NCurveBehaviourEditor : Editor
    {
        private NCurve curve;

        private SerializedProperty loop_Prop;

        private void OnEnable()
        {
            curve = (target as NCurveBehaviour).curve;

            loop_Prop = serializedObject.FindProperty("loop");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.EndVertical();
            serializedObject.Update();
        }

        private void OnSceneGUI()
        {
            if (!Target.editing) return;

            for (int i = 0; i < curve.handles.Count; i++)
            {
                DisplayHandle(i);
                if (i < curve.handles.Count - 1) DrawCurvePart(i, i + 1);
            }

            // if(loop_Prop.boolValue) DrawCurvePart(handles.Count -1, 0);
        }

        private void DisplayHandle(int i)
        {
            //Handles
            curve.handles[i].point = Handles.DoPositionHandle(curve.handles[i].point, Quaternion.identity);
            curve.handles[i].forwardHelper = Handles.DoPositionHandle(curve.handles[i].forwardHelper, Quaternion.identity);
            curve.handles[i].backHelper = Handles.DoPositionHandle(curve.handles[i].backHelper, Quaternion.identity);

            //Lines
            Handles.DrawLine(curve.handles[i].point, curve.handles[i].forwardHelper, 0.2f);
            Handles.DrawLine(curve.handles[i].point, curve.handles[i].backHelper, 0.2f);
        }

        private void DrawCurvePart(int _startI, int _endI)
        {
            Handles.DrawBezier(curve.handles[_startI].point, curve.handles[_endI].point, curve.handles[_startI].forwardHelper, curve.handles[_endI].backHelper, Color.blue, new Texture2D(10,10), 2f);
        }

        public NCurveBehaviour Target => target as NCurveBehaviour;
    }
}
#endif