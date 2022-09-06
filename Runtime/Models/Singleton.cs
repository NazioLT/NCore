using UnityEngine;

namespace Nazio_LT.Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T instance { private set; get; }

        [SerializeField] private bool dontDestroyOnLoad = true;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = (T)this;
            if (dontDestroyOnLoad && transform.parent == null) DontDestroyOnLoad(this.gameObject);
        }

        public static void DestroyInstance()
        {
            Destroy(instance);
            instance = null;
        }
    }
}