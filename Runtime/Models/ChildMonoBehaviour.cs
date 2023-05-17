using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public abstract class ChildMonoBehaviour<T> : MonoBehaviour
    {
        private T m_parent;

        /// <summary>Initialize the object.</summary>
        public virtual void Init(T parent)
        {
            m_parent = parent;
        }

        public T Parent => m_parent;
    }
}