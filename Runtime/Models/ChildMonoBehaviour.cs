using UnityEngine;

namespace Nazio_LT.Core
{
    public abstract class ChildMonoBehaviour<ParentType> : MonoBehaviour
    {
        private ParentType core;

        /// <summary>
        /// Initialise l'objet
        /// </summary>
        public virtual void Init(ParentType _core)
        {
            core = _core;
        }

        protected ParentType Core => core;
    }
}