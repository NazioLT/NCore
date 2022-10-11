using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public abstract class ChildMonoBehaviour<ParentType> : MonoBehaviour
    {
        private ParentType parent;

        /// <summary>
        /// Initialise l'objet
        /// </summary>
        public virtual void Init(ParentType _core)
        {
            parent = _core;
        }

        protected ParentType Parent => parent;
    }
}