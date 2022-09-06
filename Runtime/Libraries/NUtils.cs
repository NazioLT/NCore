using System.Collections.Generic;

namespace Nazio_LT.Core
{
    public static partial class NUtils
    {
        public static T First<T>(this List<T> _list) => _list[0];
        public static T Last<T>(this List<T> _list) => _list[_list.Count - 1];
    }
}