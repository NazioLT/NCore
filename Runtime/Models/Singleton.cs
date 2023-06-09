using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    /// <summary>The singleton is a design pattern, whose objective is to restrict the instantiation of a class to a single object. The global access to it will be provided.</summary>
    /// <typeparam name="T">Type of the object inheriting the singleton</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T s_instance = null;

        [SerializeField] protected bool dontDestroyOnLoad = true;

        public static void DestroyInstance()
        {
            Destroy(s_instance);
            s_instance = null;
        }

        protected virtual void Awake() => TryMakeThisTheInstance();

        protected void TryMakeThisTheInstance()
        {
            if (s_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = (T)this;
            if (dontDestroyOnLoad && transform.parent == null) DontDestroyOnLoad(gameObject);
        }
        
        protected void SetInstance(T instance) => s_instance = instance;

        protected static void ExecuteIfInstance(System.Action callback)
        {
            if (!s_instance) throw new System.Exception($"No instance.");

            callback();
        }

        public static T Instance => s_instance;
    }
}
