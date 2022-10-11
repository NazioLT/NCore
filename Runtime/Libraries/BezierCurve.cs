using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static partial class NMath
    {
        /// <summary>
        /// Compute squared Bezier curve point at t value.
        /// </summary>
        public static Vector3 BezierLerp(Vector3 _p1, Vector3 _p2, Vector3 _p3, Vector3 _p4, float _t)
        {
            float _tCube = Mathf.Pow(_t, 3f);
            float _tSquare = _t * _t;

            Vector3 _p1P = (-_tCube + 3f * _tSquare - 3f * _t + 1f) * _p1;
            Vector3 _p2P = (3f * _tCube - 6f * _tSquare + 3f * _t) * _p2;
            Vector3 _p3P = (-3f * _tCube + 3f * _tSquare) * _p3;
            Vector3 _p4P = _tCube * _p4;

            return _p1P + _p2P + _p3P + _p4P;
        }

        public static Vector3 BezierLerp(NHandle _hanlde1, NHandle _handle2, float _t) => BezierLerp(_hanlde1.point, _hanlde1.forwardHelper, _handle2.backHelper, _handle2.point, _t);

        /// <summary>
        /// Compute squared Bezier derivative at t value.
        /// </summary>
        public static Vector3 BezierDerivative(Vector3 _p1, Vector3 _p2, Vector3 _p3, Vector3 _p4, float _t)
        {
            float _tSquare = _t * _t;

            Vector3 _p1P = (-3f * _tSquare + 6f * _t - 3f) * _p1;
            Vector3 _p2P = (9f * _tSquare - 12f * _t + 3f) * _p2;
            Vector3 _p3P = (-9f * _tSquare + 6f * _t) * _p3;
            Vector3 _p4P = 3f * _tSquare * _p4;

            return _p1P + _p2P + _p3P + _p4P;
        }

        public static Vector3 BezierDerivative(NHandle _hanlde1, NHandle _handle2, float _t) => BezierDerivative(_hanlde1.point, _hanlde1.forwardHelper, _handle2.backHelper, _handle2.point, _t);

        /// <summary>
        /// Compute squared Bezier second derivative at t value.
        /// </summary>
        public static Vector3 Bezier2ndDerivative(Vector3 _p1, Vector3 _p2, Vector3 _p3, Vector3 _p4, float _t)
        {
            Vector3 _p1P = (-6f * _t) * _p1;
            Vector3 _p2P = (18f * _t - 12f) * _p2;
            Vector3 _p3P = (-18f * _t + 6f) * _p3;
            Vector3 _p4P = 6f * _t * _p4;

            return _p1P + _p2P + _p3P + _p4P;
        }

        public static Vector3 Bezier2ndDerivative(NHandle _hanlde1, NHandle _handle2, float _t) => Bezier2ndDerivative(_hanlde1.point, _hanlde1.forwardHelper, _handle2.backHelper, _handle2.point, _t);

        /// <summary>
        /// Compute squared Bezier total distance with parameterization.
        /// </summary>
        public static float CumulativeValuesToT(float[] _values, float _curValue)
        {
            int _length = _values.Length;
            float _totalDst = _values[_length - 1];

            _curValue = _curValue % _totalDst;

            for (var i = 0; i < _length; i++)
            {
                if (!_curValue.IsIn(_values[i], _values[i + 1])) continue;

                return Remap(_curValue, _values[i], _values[i + 1], i / (_length - 1f), (i + 1) / (_length - 1f)); 
                // Mathf.Lerp(i / (_length - 1f), (i + 1) / (_length - 1f), Mathf.InverseLerp(_values[i], _values[i + 1], _curValue));
            }

            return 0;
        }
    }
}

