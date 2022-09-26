using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public sealed class NHandle
    {
        public Vector3 point;
        public Vector3 forwardHelper;
        public Vector3 backHelper;

        public bool broken;

        public void UpdateHandle(bool _forward)
        {
            if (_forward)
            {
                UpdateHandle(ref forwardHelper, ref backHelper);
                return;
            }

            UpdateHandle(ref backHelper, ref forwardHelper);
        }
        private void UpdateHandle(ref Vector3 _controlPoint, ref Vector3 _otherPoint)
        {
            if (broken) return;

            Vector3 _controlDelta = _controlPoint - point;
            _otherPoint = point - _controlDelta;
        }

        public void MoveCentralPoint(Vector3 _newPos)
        {
            Vector3 _deltaForward = forwardHelper - point;
            Vector3 _deltaBackward = backHelper - point;

            point = _newPos;
            forwardHelper = point + _deltaForward;
            backHelper = point + _deltaBackward;
        }
    }

    [System.Serializable]
    public sealed class NCurve
    {
        public enum CurveType { Linear = 0, Bezier = 1 }
        [SerializeField] public CurveType type;
        [SerializeField] public bool loop;
        [SerializeField] public List<NHandle> handles = new List<NHandle>();
        [SerializeField] private float inspectorHeight;
        public float[] simplifiedCurveDst;
        public float curveLength { private set; get; }

        private const int PARAMETERIZATION_PRECISION = 40;

        public Vector3 ComputePoint(float _t, bool _loop)
        {
            if (_t == 0f || (_t == 1f && _loop)) _t = 0.001f;
            float _computedT = _t / Factor;
            int _curveID = (int)_computedT;

            return GetPointFunc()(handles[_curveID], handles[GetNextID(_curveID)], _computedT - _curveID);
        }

        public Vector3 ComputePointDistance(float _distance) => ComputePoint(DistanceToT(_distance), true);

        public float DistanceToT(float _distance) => NMath.CumulativeValuesToT(simplifiedCurveDst, _distance);

        public Vector3 ComputePointUniform(float _t) => ComputePointDistance(_t * curveLength);

        public void Update()
        {
            SimplifyCurve();
        }

        public void DirectionUniform(float _t, out Vector3 _forward, out Vector3 _up, out Vector3 _right) => Direction(DistanceToT(_t * curveLength), out _forward, out _up, out _right);

        public void Direction(float _t, out Vector3 _forward, out Vector3 _up, out Vector3 _right)
        {
            _forward = Forward(_t).normalized;
            _up = Up(_t).normalized;
            _right = Vector3.Cross(_forward, _up).normalized;
        }

        public Vector3 Forward(float _t)
        {
            _t = _t - (int)_t;
            float _computedT = _t / Factor;
            int _curveID = (int)_computedT;

            return NMath.BezierDerivative(handles[_curveID], handles[GetNextID(_curveID)], _computedT - _curveID);
        }

        public Vector3 Up(float _t) => Vector3.up;

        private void SimplifyCurve()
        {
            simplifiedCurveDst = new float[Parameterization + 1];

            float _factor = 1f / (float)Parameterization;
            Vector3 _previousPoint = ComputePoint(0f, true);
            simplifiedCurveDst[0] = 0;

            for (var i = 1; i < Parameterization + 1; i++)
            {
                Vector3 _newPoint = ComputePoint(i * _factor, true);
                float _relativeDst = Vector3.Distance(_previousPoint, _newPoint);
                simplifiedCurveDst[i] = simplifiedCurveDst[i - 1] + _relativeDst;

                _previousPoint = _newPoint;
            }

            curveLength = simplifiedCurveDst[Parameterization];
        }

        private System.Func<NHandle, NHandle, float, Vector3> GetPointFunc()
        {
            switch (type)
            {
                case CurveType.Bezier:
                    return (_h1, _h2, _t) => NMath.BezierLerp(_h1, _h2, _t);
            }

            return (_h1, _h2, _t) => Vector3.Lerp(_h1.point, _h2.point, _t);
        }

        private System.Func<NHandle, NHandle, float, Vector3> GetPointDerivative()
        {
            switch (type)
            {
                case CurveType.Bezier:
                    return (_h1, _h2, _t) => NMath.BezierDerivative(_h1, _h2, _t);
            }

            return (_h1, _h2, _t) => (_h1.point - _h1.point).normalized;
        }

        private int GetNextID(int _value)
        {
            if (_value == handles.Count - 1) return 0;

            return _value + 1;
        }

        private int GetPreID(int _value)
        {
            if (_value == 0) return handles.Count - 1;

            return _value + 1;
        }

        private float Factor => 1f / (float)(loop ? handles.Count : handles.Count - 1);
        private int Parameterization => PARAMETERIZATION_PRECISION * (loop ? handles.Count : handles.Count - 1);
        public List<NHandle> Handles => handles;
    }
}