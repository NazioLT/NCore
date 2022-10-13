using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static partial class NMath
    {
        #region Constants

        /// <summary>Golden ratio.</summary>
        public const float GOLDEN_NUMB = 1.618033988749894f;

        /// <summary>Euler's number.</summary>
        public const float E = 2.71828182846f;

        /// <summary> Value wich is near to zero and positive.</summary>
        public const float EPSILON = 0.03f;

        /// <summary> Square value of Epsilon.</summary>
        public const float SQUARE_EPSILON = EPSILON * EPSILON;

        #endregion

        /// <summary>Return if the value is near to zero. (His absolute value is lower than epsilon)</summary>
        public static bool AverageZero(float _value) => Mathf.Abs(_value) < EPSILON;

        #region Vector Bounding

        public static bool InBoundsInclusive(Vector2Int _value, Vector2Int _bounds) => InBoundsInclusive(_value, Vector2Int.zero, _bounds);
        public static bool InBoundsInclusive(Vector2Int _value, Vector2Int _minBounds, Vector2Int _maxBounds) => InBoundsInclusive((Vector2)_value, (Vector2)_minBounds, (Vector2)_maxBounds);

        public static bool InBoundsInclusive(Vector3Int _value, Vector3Int _bounds) => InBoundsInclusive(_value, Vector3Int.zero, _bounds);
        public static bool InBoundsInclusive(Vector3Int _value, Vector3Int _minBounds, Vector3Int _maxBounds) => InBoundsInclusive((Vector3)_value, (Vector3)_minBounds, (Vector3)_maxBounds);

        public static bool InBoundsInclusive(Vector2 _value, Vector2 _bounds) => InBoundsInclusive(_value, Vector2.zero, _bounds);
        public static bool InBoundsInclusive(Vector2 _value, Vector2 _minBounds, Vector2 _maxBounds) => InBoundsInclusive((Vector3)_value, (Vector3)_minBounds, (Vector3)_maxBounds);

        public static bool InBoundsInclusive(Vector3 _value, Vector3 _bounds) => InBoundsInclusive(_value, Vector3.zero, _bounds);
        public static bool InBoundsInclusive(Vector3 _value, Vector3 _minBounds, Vector3 _maxBounds)
        {
            if (!CheckVectorAxis(_value, _minBounds, _maxBounds, VectorAxis.X)) return false;

            if (!CheckVectorAxis(_value, _minBounds, _maxBounds, VectorAxis.Y)) return false;

            if (!CheckVectorAxis(_value, _minBounds, _maxBounds, VectorAxis.Z)) return false;

            return true;
        }

        private static bool CheckVectorAxis(Vector3 _value, Vector3 _minBounds, Vector3 _maxBounds, VectorAxis _axis)
        {
            if (!VectorAxisInBounds(_value, _minBounds, _maxBounds, _axis))
            {
                Debug.LogErrorFormat($"The X value of {_value} isn't in bounds ({_minBounds}:{_maxBounds})");
                return false;
            }

            return true;
        }

        public enum VectorAxis { X, Y, Z }

        public static bool VectorAxisInBounds(Vector2 _value, Vector2 _minBounds, Vector2 _maxBounds, VectorAxis _axis) => VectorAxisInBounds((Vector3)_value, (Vector3)_minBounds, (Vector3)_maxBounds, _axis);

        public static bool VectorAxisInBounds(Vector3 _value, Vector3 _minBounds, Vector3 _maxBounds, VectorAxis _axis)
        {
            switch (_axis)
            {
                case VectorAxis.X:
                    return (_value.x >= _minBounds.x && _value.x <= _maxBounds.x);

                case VectorAxis.Y:
                    return (_value.y >= _minBounds.y && _value.y <= _maxBounds.y);
            }

            return (_value.z >= _minBounds.z && _value.z <= _maxBounds.z);
        }

        #endregion

        #region Angles

        /// <summary> Calculate the angle of a 2D vector in degree.</summary>
        public static float VectorAngleDEG(float _x, float _y) => Mathf.Rad2Deg * VectorAngleRAD(_x, _y);

        /// <summary> Calculate the angle of a 2D vector in radian.</summary>
        public static float VectorAngleRAD(float _x, float _y) => Mathf.Atan(_y / _x);

        #endregion

        public static Vector2 Flat(Vector3 _v) => new Vector2(_v.x, _v.z);

        public static float Remap(float _value, float _inputMin, float _inputMax, float _outputMin, float _outputMax) => Mathf.Lerp(_outputMin, _outputMax, Mathf.InverseLerp(_inputMin, _inputMax, _value));
    }
}