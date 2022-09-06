using UnityEngine;

namespace Nazio_LT.Core
{
    public static partial class NMath
    {
        public static Vector2 Vector2PerAxis(bool _firstX, float _value1, float _value2) => new Vector2(_firstX ? _value1 : _value2, _firstX ? _value2 : _value1);

        #region Angles

        /// <summary>
        /// Calulate the angle of a 2D vector in degree.
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <returns></returns>
        public static float VectorAngleDEG(float _x, float _y) => Mathf.Rad2Deg * VectorAngleRAD(_x, _y);

        /// <summary>
        /// Calulate the angle of a 2D vector in radian.
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <returns></returns>
        public static float VectorAngleRAD(float _x, float _y) => Mathf.Atan(_y / _x);

        #endregion

        #region Curves

        #endregion
    }
}