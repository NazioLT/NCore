using System.Collections.Generic;

namespace Nazio_LT.Tools.Core
{
    public static partial class NUtils
    {
        #region Lists

        /// <summary> Return the first element of a List.</summary>
        public static T GetFirst<T>(this List<T> list) => list[0];

        /// <summary> Return the last element of a List.</summary>
        public static T GetLast<T>(this List<T> list) => list[list.Count - 1];

        /// <summary> Return a random element of the List. </summary>
        public static T GetRandomElement<T>(this List<T> list) => GetRandomElement(list);

        /// <summary> Return a random element of the List, then remove it. </summary>
        public static T PickRandomElement<T>(this List<T> list) => PickRandomElementOf(list);

        #endregion
    }
}