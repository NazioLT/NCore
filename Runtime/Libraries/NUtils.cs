using System.Collections.Generic;

namespace Nazio_LT.Tools.Core
{
    public static partial class NUtils
    {
        public static bool IsIn(this float _value, float _min, float _max) => _value >= _min && _value < _max;

        public static T First<T>(this List<T> _list) => _list[0];
        public static T Last<T>(this List<T> _list) => _list[_list.Count - 1];
    }
}