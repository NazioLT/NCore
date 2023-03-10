using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public abstract class ChildMonoBehaviour<T> : MonoBehaviour
    {
        public T parent { private set; get; }

        /// <summary>Initialize the object.</summary>
        public virtual void Init(T _core)
        {
            parent = _core;
        }
    }
}