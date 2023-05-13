using System.Collections.Generic;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Nazio_LT.Tools.Core
{
    public static partial class NUtils
    {
        #region Lists

        /// <summary> Return the first element of a List.</summary>
        public static T First<T>(List<T> list) => list[0];

        /// <summary> Return the last element of a List.</summary>
        public static T Last<T>(List<T> list) => list[list.Count - 1];

        /// <summary> Return a random element of the List. </summary>
        public static T GetRandomElementOf<T>(List<T> list) => list[Random.Range(0, list.Count)];

        /// <summary> Return a random element of the List, then remove it. </summary>
        public static T PickRandomElementOf<T>(List<T> list)
        {
            int index = Random.Range(0, list.Count);
            T element = list[index];
            list.RemoveAt(index);
            return element;
        }

        #endregion

        /// <summary> Return if value is between higher or equals than min and lower than max.</summary>
        public static bool IsIn(this float value, float min, float maxExclusive) => value >= min && value < maxExclusive;

        /// <summary> Set the alpha of a color.</summary>
        public static Color SetAlpha(Color value, float alpha)
        {
            value.a = alpha;
            return value;
        }

        /// <summary> Merge array 1 and 2.</summary>
        public static T[] Merge<T>(T[] array1, T[] array2)
        {
            T[] _result = new T[array1.Length + array2.Length];
            array1.CopyTo(_result, 0);
            array2.CopyTo(_result, array1.Length);

            return _result;
        }

        /// <summary>Double for : for i { for j }</summary>
        public static void DoubleFor(int iLength, int jLength, Action<int, int> callbackAction)
        {
            for (int i = 0; i < iLength; i++)
            {
                for (int j = 0; j < jLength; j++)
                {
                    callbackAction(i, j);
                }
            }
        }
    }
}