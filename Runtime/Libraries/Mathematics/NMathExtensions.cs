using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static class NMathExtensions
    {
        public static Vector2 Flat(this Vector3 value, VectorAxis deletedAxis = VectorAxis.Z) => NMath.Flat(value, deletedAxis);
        public static float Remap(this float value, float inputMin, float inputMax, float outputMin, float outputMax) => NMath.Remap(value, inputMin, inputMax, outputMin, outputMax);
        public static bool AverageZero(this float value) => NMath.AverageZero(value);
    }
}
