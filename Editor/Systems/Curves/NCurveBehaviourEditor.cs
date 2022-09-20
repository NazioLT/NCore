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
            for (int i = 0; i < curve.handles.Count; i++)
            {
                if (Target.editing) DisplayHandle(i);
                if (i < curve.handles.Count - 1) DrawCurvePart(i, i + 1);
            }

            if (curve.loop) DrawCurvePart(curve.handles.Count - 1, 0);

            curve.Update();
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