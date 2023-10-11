namespace Nazio_LT.Tools.Core
{
    /// <summary>A child is a class wich add a parent.</summary>
    public abstract class Child<T>
    {
        public Child(T parent) => Parent = parent;

        public readonly T Parent;
    }
}