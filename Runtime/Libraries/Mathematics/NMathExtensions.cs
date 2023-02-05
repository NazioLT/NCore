using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static class NMathExtensions
    {
        public static Vector2 Flat(this Vector3 _value, VectorAxis _deletedAxis = VectorAxis.Z) => NMath.Flat(_value, _deletedAxis);
        public static float Remap(this float _value, float _inputMin, float _inputMax, float _outputMin, float _outputMax) => NMath.Remap(_value, _inputMin, _inputMax, _outputMin, _outputMax);
        public static bool AverageZero(this float _value) => NMath.AverageZero(_value);
    }
}
