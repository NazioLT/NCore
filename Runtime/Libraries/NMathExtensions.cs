using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public static class NMathExtensions
    {
        public static Vector2 Flat(this Vector3 _v, VectorAxis _deletedAxis) => NMath.Flat(_v, VectorAxis.Z);
    }
}
