using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public class NSplineHandle
    {
        public Vector3 point;
        public Vector3 helper1;
        public Vector3 helper2;
    }

    [System.Serializable]
    public sealed class NCurve
    {
        private enum CurveType { Linear = 0, Bezier = 1}
        [SerializeField] private List<NSplineHandle> handles = new List<NSplineHandle>();

        public Vector3 ComputePoint(float _t)
        {
            if(_t == 0) _t = 0.001f;
            float _computedT = _t / Factor;
            int _curveID = (int)_computedT;

            return Vector3.Lerp(handles[_curveID].point, handles[GetNextID(_curveID)].point, _computedT - _curveID);
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