namespace Nazio_LT.Tools.Core
{
    public abstract class Child<T>
    {
        private T parent;

        /// <summary>Initialise l'objet</summary>
        public virtual void Init(T _parent) => parent = _parent;

        public T Parent => parent;
    }
}