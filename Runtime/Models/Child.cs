namespace Nazio_LT.Tools.Core
{
    /// <summary>A child is a class wich add a parent.</summary>
    public abstract class Child<T>
    {
        public Child(T _parent) => parent =_parent;

        public readonly T parent;
    }
}