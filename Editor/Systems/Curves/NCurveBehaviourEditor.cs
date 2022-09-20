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

            if (curve.loop) DrawCurvePart(curve.handles.Count - 1, 0);
        }

        private void DisplayHandle(int i)
        {
            Handles.color = Color.white;
            //Point
            curve.handles[i].point = Handles.DoPositionHandle(curve.handles[i].point, Quaternion.identity);

            if((int)curve.type == 0) return;

            //Handles
            curve.handles[i].forwardHelper = Handles.DoPositionHandle(curve.handles[i].forwardHelper, Quaternion.identity);
            curve.handles[i].backHelper = Handles.DoPositionHandle(curve.handles[i].backHelper, Quaternion.identity);

            curve.handles[i].UpdateHandle(true);

            //Lines
            Handles.DrawLine(curve.handles[i].point, curve.handles[i].forwardHelper, 0.2f);
            Handles.DrawLine(curve.handles[i].point, curve.handles[i].backHelper, 0.2f);
        }

        private void DrawCurvePart(int _startI, int _endI)
        {
            Handles.color = Color.blue;
            switch ((int)curve.type)
            {
                case 0:
                    Handles.DrawLine(curve.handles[_startI].point, curve.handles[_endI].point, 2f);
                    break;
                case 1:
                     Handles.DrawBezier(curve.handles[_startI].point, curve.handles[_endI].point, curve.handles[_startI].forwardHelper, curve.handles[_endI].backHelper, Color.blue, new Texture2D(10, 10), 2f);
                    break;
            }     
        }

        public NCurveBehaviour Target => target as NCurveBehaviour;
    }
}
#endif