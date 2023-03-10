using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    /// <summary>
    /// The singleton is a design pattern, whose objective is to restrict the instantiation of a class to a single object. The global access to it will be provided.
    /// </summary>
    /// <typeparam name="T">Type of the object inheriting the singleton</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T instance { protected set; get; }

        [SerializeField] protected bool dontDestroyOnLoad = true;

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

        protected static void ExecuteIfInstance(System.Action _callback)
        {
            if (!instance) throw new System.Exception("No Music Manager instance.");

            _callback();
        }
    }
}