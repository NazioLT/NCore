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
        public static bool AverageZero(float value) => Mathf.Abs(value) < EPSILON;

        public static Vector3 GetPositionAround(int i, int count, float radius, Vector3 center)
        {
            float angleI = 360f;
            angleI /= count;

            float currentAngle = angleI * i;
            return new Vector3(radius * Mathf.Sin(currentAngle * Mathf.PI / 180), 0, radius * Mathf.Cos(currentAngle * Mathf.PI / 180)) + center;
        }

        /// <summary> Calculate the angle of a 2D vector in degree.</summary>
        public static float VectorAngleDEG(float x, float y) => Mathf.Rad2Deg * VectorAngleRAD(x, y);

        /// <summary> Calculate the angle of a 2D vector in radian.</summary>
        public static float VectorAngleRAD(float x, float y) => Mathf.Atan(y / x);

        /// <summary>Remap</summary>
        public static float Remap(float value, float inputMin, float inputMax, float outputMin, float outputMax) => Mathf.Lerp(outputMin, outputMax, Mathf.InverseLerp(inputMin, inputMax, value));

        public static Vector2 Flat(Vector3 v, VectorAxis deletedAxis)
        {
            if (deletedAxis == VectorAxis.X) return new Vector2(v.z, v.y);
            if (deletedAxis == VectorAxis.Y) return new Vector2(v.x, v.z);
            return new Vector2(v.x, v.y);
        }

        #endregion
    }
}