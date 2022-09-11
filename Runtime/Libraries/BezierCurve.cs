using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static partial class NMath
    {
        public static Vector3 BezierLerp(Vector3 _p1, Vector3 _p2, Vector3 _p3, Vector3 _p4, float _t)
        {
            Vector3 _p1_2 = Vector3.Lerp(_p1, _p2, _t);
            Vector3 _p2_3 = Vector3.Lerp(_p2, _p3, _t);
            Vector3 _p3_4 = Vector3.Lerp(_p3, _p4, _t);

            Vector3 _p1_2_3 = Vector3.Lerp(_p1_2, _p2_3, _t);
            Vector3 _p2_3_4 = Vector3.Lerp(_p2_3, _p3_4, _t);

            return Vector3.Lerp(_p1_2_3, _p2_3_4, _t);
        }
    }
}