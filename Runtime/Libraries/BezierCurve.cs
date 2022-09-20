using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static partial class NMath
    {
        //Classic Method
        // public static Vector3 BezierLerp(Vector3 _p1, Vector3 _p2, Vector3 _p3, Vector3 _p4, float _t)
        // {
        //     Vector3 _p1_2 = Vector3.Lerp(_p1, _p2, _t);
        //     Vector3 _p2_3 = Vector3.Lerp(_p2, _p3, _t);
        //     Vector3 _p3_4 = Vector3.Lerp(_p3, _p4, _t);

        //     Vector3 _p1_2_3 = Vector3.Lerp(_p1_2, _p2_3, _t);
        //     Vector3 _p2_3_4 = Vector3.Lerp(_p2_3, _p3_4, _t);

        //     return Vector3.Lerp(_p1_2_3, _p2_3_4, _t);
        // }

        public static Vector3 BezierLerp(Vector3 _p1, Vector3 _p2, Vector3 _p3, Vector3 _p4, float _t)
        {
            float _tCube = Mathf.Pow(_t,3f);
            float _tSquare = _t * _t;

            Vector3 _p1P = (-_tCube + 3 * _tSquare - 3 * _t + 1) * _p1;
            Vector3 _p2P = (3 * _tCube - 6 * _tSquare + 3 * _t) * _p2;
            Vector3 _p3P = (-3 * _tCube + 3 * _tSquare) * _p3;
            Vector3 _p4P = _tCube * _p4;

            return _p1P + _p2P + _p3P + _p4P;
        }

        public static Vector3 BezierLerp(NHandle _hanlde1, NHandle _handle2, float _t) => BezierLerp(_hanlde1.point, _hanlde1.forwardHelper, _handle2.backHelper, _handle2.point, _t);
    }
}