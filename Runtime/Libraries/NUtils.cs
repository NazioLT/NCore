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
        public static T First<T>(List<T> _list) => _list[0];

        /// <summary> Return the last element of a List.</summary>
        public static T Last<T>(List<T> _list) => _list[_list.Count - 1];

        /// <summary> Return a random element of the List. </summary>
        public static T GetRandomElementOf<T>(List<T> _list) => _list[Random.Range(0, _list.Count)];

        /// <summary> Return a random element of the List, then remove it. </summary>
        public static T PickRandomElementOf<T>(List<T> _list)
        {
            int _index = Random.Range(0, _list.Count);
            T _element = _list[_index];
            _list.RemoveAt(_index);
            return _element;
        }

        #endregion

        /// <summary> Return if value is between higher or equals than min and lower than max.</summary>
        public static bool IsIn(this float _value, float _min, float _maxExclusive) => _value >= _min && _value < _maxExclusive;

        /// <summary> Set the alpha of a color.</summary>
        public static Color SetAlpha(Color _value, float _alpha)
        {
            _value.a = _alpha;
            return _value;
        }

        /// <summary> Merge array 1 and 2.</summary>
        public static T[] Merge<T>(T[] _array1, T[] _array2)
        {
            T[] _result = new T[_array1.Length + _array2.Length];
            _array1.CopyTo(_result, 0);
            _array2.CopyTo(_result, _array1.Length);

            return _result;
        }

        /// <summary>Double for : for i { for j }</summary>
        public static void DoubleFor(int _iLenght, int _jLeght, Action<int, int> _callbackAction)
        {
            for (int i = 0; i < _iLenght; i++)
            {
                for (int j = 0; j < _jLeght; j++)
                {
                    _callbackAction(i, j);
                }
            }
        }
    }
}