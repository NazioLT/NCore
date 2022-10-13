using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static partial class NUtils
    {
        /// <summary> Return if value is between higher or equals than min and lower than max.</summary>
        public static bool IsIn(this float _value, float _min, float _maxExclusive) => _value >= _min && _value < _maxExclusive;

        /// <summary> Set the alpha of a color.</summary>
        public static Color SetAlpha(Color _value, float _alpha)
        {
            _value.a = _alpha;
            return _value;
        }

        /// <summary> Return the first element of a List.</summary>
        public static T First<T>(this List<T> _list) => _list[0];

        /// <summary> Return the last element of a List.</summary>
        public static T Last<T>(this List<T> _list) => _list[_list.Count - 1];
    }
}