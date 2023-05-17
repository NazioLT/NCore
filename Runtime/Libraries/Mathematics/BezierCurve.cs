using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static partial class NMath
    {
        #region Core

        /// <summary>Compute squared Bezier curve point at t value.</summary>
        public static Vector3 BezierLerp(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            float tCube = Mathf.Pow(t, 3f);
            float tSquare = t * t;

            Vector3 p1P = (-tCube + 3f * tSquare - 3f * t + 1f) * p1;
            Vector3 p2P = (3f * tCube - 6f * tSquare + 3f * t) * p2;
            Vector3 p3P = (-3f * tCube + 3f * tSquare) * p3;
            Vector3 p4P = tCube * p4;

            return p1P + p2P + p3P + p4P;
        }

        /// <summary>Compute squared Bezier derivative at t value.</summary>
        public static Vector3 BezierDerivative(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            float _tSquare = t * t;

            Vector3 p1P = (-3f * _tSquare + 6f * t - 3f) * p1;
            Vector3 p2P = (9f * _tSquare - 12f * t + 3f) * p2;
            Vector3 p3P = (-9f * _tSquare + 6f * t) * p3;
            Vector3 p4P = 3f * _tSquare * p4;

            return p1P + p2P + p3P + p4P;
        }

        /// <summary>Compute squared Bezier second derivative at t value.</summary>
        public static Vector3 Bezier2ndDerivative(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
        {
            Vector3 p1P = (-6f * t) * p1;
            Vector3 p2P = (18f * t - 12f) * p2;
            Vector3 p3P = (-18f * t + 6f) * p3;
            Vector3 p4P = 6f * t * p4;

            return p1P + p2P + p3P + p4P;
        }

        /// <summary>Compute squared Bezier total distance with parameterization.</summary>
        public static float CumulativeValuesToT(float[] values, float curValue)
        {
            int length = values.Length;
            float totalDst = values[length - 1];

            curValue = curValue % totalDst;

            for (var i = 0; i < length; i++)
            {
                if (!curValue.IsIn(values[i], values[i + 1])) continue;

                return Remap(curValue, values[i], values[i + 1], i / (length - 1f), (i + 1) / (length - 1f));
                // Mathf.Lerp(i / (_length - 1f), (i + 1) / (_length - 1f), Mathf.InverseLerp(_values[i], _values[i + 1], _curValue));
            }

            return 0;
        }

        #endregion

        #region Variants

        /// <inheritdoc cref="BezierLerp(Vector3, Vector3, Vector3, Vector3, float)"/>
        public static Vector3 BezierLerp(NHandle hanlde1, NHandle handle2, float t) => BezierLerp(hanlde1.Point, hanlde1.ForwardHelper, handle2.BackHelper, handle2.Point, t);

        /// <inheritdoc cref="BezierDerivative(Vector3, Vector3, Vector3, Vector3, float)"/>
        public static Vector3 BezierDerivative(NHandle hanlde1, NHandle handle2, float t) => BezierDerivative(hanlde1.Point, hanlde1.ForwardHelper, handle2.BackHelper, handle2.Point, t);

        /// <inheritdoc cref="Bezier2ndDerivative(Vector3, Vector3, Vector3, Vector3, float)"/>
        public static Vector3 Bezier2ndDerivative(NHandle hanlde1, NHandle handle2, float t) => Bezier2ndDerivative(hanlde1.Point, hanlde1.ForwardHelper, handle2.BackHelper, handle2.Point, t);

        #endregion
    }
}