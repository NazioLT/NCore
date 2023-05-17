using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public sealed class NHandle
    {
        public Vector3 Point;
        public Vector3 ForwardHelper;
        public Vector3 BackHelper;

        public bool Broken;

        public void UpdateHandle(bool forward)
        {
            if (forward)
            {
                UpdateHandle(ref ForwardHelper, ref BackHelper);
                return;
            }

            UpdateHandle(ref BackHelper, ref ForwardHelper);
        }
        private void UpdateHandle(ref Vector3 controlPoint, ref Vector3 otherPoint)
        {
            if (Broken) return;

            Vector3 controlDelta = controlPoint - Point;
            otherPoint = Point - controlDelta;
        }

        public void MoveCentralPoint(Vector3 newPos)
        {
            Vector3 deltaForward = ForwardHelper - Point;
            Vector3 deltaBackward = BackHelper - Point;

            Point = newPos;
            ForwardHelper = Point + deltaForward;
            BackHelper = Point + deltaBackward;
        }
    }

    [System.Serializable]
    public sealed class NCurve
    {
        public enum CurveType { Linear = 0, Bezier = 1 }

        [SerializeField] public CurveType Type;
        [SerializeField] public bool Loop;
        [SerializeField] private List<NHandle> m_handles = new List<NHandle>();
        [SerializeField] private float m_inspectorHeight;
        public float[] SimplifiedCurveDst;
        private float m_curveLength;

        private const int PARAMETERIZATION_PRECISION = 100;

        public Vector3 ComputePoint(float t, bool loop)
        {
            if (t == 0f || (t == 1f && loop)) t = 0.001f;
            float computedT = t / m_factor;
            int curveID = (int)computedT;

            return GetPointFunc()(m_handles[curveID], m_handles[GetNextID(curveID)], computedT - curveID);
        }

        public Vector3 ComputePoint(float t) => ComputePoint(t, Loop);

        public Vector3 ComputePointDistance(float distance, bool loop)
        {
            float t = distance == CurveLength ? 1f : DistanceToT(distance);
            return ComputePoint(t, loop);
        }

        public Vector3 ComputePointDistance(float distance) => ComputePointDistance(distance, Loop);

        public float DistanceToT(float distance) => NMath.CumulativeValuesToT(SimplifiedCurveDst, distance);

        public Vector3 ComputePointUniform(float t, bool loop) => ComputePointDistance(t * CurveLength, loop);
        public Vector3 ComputePointUniform(float t) => ComputePointDistance(t * CurveLength, Loop);

        public void DirectionUniform(float t, out Vector3 forward, out Vector3 up, out Vector3 right) => Direction(DistanceToT(t * CurveLength), out forward, out up, out right);

        public void Direction(float t, out Vector3 forward, out Vector3 up, out Vector3 right)
        {
            forward = Forward(t).normalized;
            up = Up(t).normalized;
            right = Vector3.Cross(forward, up).normalized;
        }

        public void Update()
        {
            SimplifyCurve();
        }

        public Vector3 Forward(float t)
        {
            t = t - (int)t;
            float computedT = t / m_factor;
            int curveID = (int)computedT;

            return NMath.BezierDerivative(m_handles[curveID], m_handles[GetNextID(curveID)], computedT - curveID);
        }

        public Vector3 Up(float t) => Vector3.up;

        private void SimplifyCurve()
        {
            if (m_handles.Count == 0) return;

            SimplifiedCurveDst = new float[m_parameterization + 1];

            float factor = 1f / (float)m_parameterization;
            Vector3 previousPoint = ComputePoint(0f, Loop);
            SimplifiedCurveDst[0] = 0;

            for (var i = 1; i < m_parameterization + 1; i++)
            {
                Vector3 newPoint = ComputePoint(i * factor, Loop);
                float relativeDst = Vector3.Distance(previousPoint, newPoint);
                SimplifiedCurveDst[i] = SimplifiedCurveDst[i - 1] + relativeDst;

                //Debug.Log(simplifiedCurveDst[i] + " : " + i + ": " + i * _factor);

                previousPoint = newPoint;
            }

            m_curveLength = SimplifiedCurveDst[m_parameterization];
        }

        private System.Func<NHandle, NHandle, float, Vector3> GetPointFunc()
        {
            switch (Type)
            {
                case CurveType.Bezier:
                    return (_h1, _h2, _t) => NMath.BezierLerp(_h1, _h2, _t);
            }

            return (_h1, _h2, _t) => Vector3.Lerp(_h1.Point, _h2.Point, _t);
        }

        private System.Func<NHandle, NHandle, float, Vector3> GetPointDerivative()
        {
            switch (Type)
            {
                case CurveType.Bezier:
                    return (_h1, _h2, _t) => NMath.BezierDerivative(_h1, _h2, _t);
            }

            return (_h1, _h2, _t) => (_h1.Point - _h1.Point).normalized;
        }

        private int GetNextID(int value)
        {
            if (value == m_handles.Count - 1) return 0;

            return value + 1;
        }

        private int GetPreID(int value)
        {
            if (value == 0) return m_handles.Count - 1;

            return value + 1;
        }

        public List<NHandle> Handles => m_handles;
        public float CurveLength => m_curveLength;
        private float m_factor => 1f / (float)(Loop ? m_handles.Count : m_handles.Count - 1);
        private int m_parameterization => PARAMETERIZATION_PRECISION * (Loop ? m_handles.Count : m_handles.Count - 1);
    }
}