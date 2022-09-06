using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T instance { protected set; get; }

        [SerializeField] private bool dontDestroyOnLoad = true;

        protected virtual void Awake() => TryMakeThisTheInstance();

        protected void TryMakeThisTheInstance()
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