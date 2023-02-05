using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public enum VectorAxis { X, Y, Z }

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

        #region Core

        /// <summary>Return if the value is near to zero.</summary>
        public static bool AverageZero(float _value) => Mathf.Abs(_value) < EPSILON;

        public static Vector3 GetPositionAround(int i, int _count, float _radius, Vector3 _center)
        {
            float _angleI = 360f;
            _angleI /= _count;

            float _currentAngle = _angleI * i;
            return new Vector3(_radius * Mathf.Sin(_currentAngle * Mathf.PI / 180), 0, _radius * Mathf.Cos(_currentAngle * Mathf.PI / 180)) + _center;
        }

        /// <summary> Calculate the angle of a 2D vector in degree.</summary>
        public static float VectorAngleDEG(float _x, float _y) => Mathf.Rad2Deg * VectorAngleRAD(_x, _y);

        /// <summary> Calculate the angle of a 2D vector in radian.</summary>
        public static float VectorAngleRAD(float _x, float _y) => Mathf.Atan(_y / _x);

        /// <summary>Remap</summary>
        public static float Remap(float _value, float _inputMin, float _inputMax, float _outputMin, float _outputMax) => Mathf.Lerp(_outputMin, _outputMax, Mathf.InverseLerp(_inputMin, _inputMax, _value));

        public static Vector2 Flat(Vector3 _v, VectorAxis _deletedAxis)
        {
            if (_deletedAxis == VectorAxis.X) return new Vector2(_v.z, _v.y);
            if (_deletedAxis == VectorAxis.Y) return new Vector2(_v.x, _v.z);
            return new Vector2(_v.x, _v.y);
        }

        #endregion
    }
}