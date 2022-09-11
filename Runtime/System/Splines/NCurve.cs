using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public class NHandle
    {
        public Vector3 point;
        public Vector3 forwardHelper;
        public Vector3 backHelper;
    }

    [System.Serializable]
    public sealed class NCurve
    {
        private enum CurveType { Linear = 0, Bezier = 1 }
        [SerializeField] private CurveType type;
        [SerializeField] private List<NHandle> handles = new List<NHandle>();

        public Vector3 ComputePoint(float _t)
        {
            if (_t == 0) _t = 0.001f;
            float _computedT = _t / Factor;
            int _curveID = (int)_computedT;

            return GetPointFunc()(handles[_curveID], handles[GetNextID(_curveID)], _computedT - _curveID);
        }

        private System.Func<NHandle, NHandle, float, Vector3> GetPointFunc()
        {
            switch(type)
            {
                case CurveType.Bezier:
                    return (_h1, _h2, _t) => NMath.BezierLerp(_h1, _h2, _t);
            }

            return (_h1, _h2, _t) => Vector3.Lerp(_h1.point, _h2.point, _t);
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

        private float Factor => 1f / (float)handles.Count;
    }
}