#if UNITY_EDITOR
using UnityEngine;

namespace Nazio_LT.Tools.Core.Internal
{
    [ExecuteAlways]
    public abstract class ObjectTemplate : MonoBehaviour
    {
        private void Awake()
        {
            BeforeDestroy();
            DestroyImmediate(this);
        }

        protected abstract void BeforeDestroy();
    }
}
#endif