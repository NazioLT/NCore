using System;
using System.Reflection;

namespace Nazio_LT.Tools.Core
{
    public static class NReflection
    {
        public static Action GetMethodByReflection<T>(T _target, string _methodName)
        {
            //Appel par reflexion
            System.Type _type = _target.GetType();

            MethodInfo _methodInfos = _type.GetMethod(_methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            return ((Action)Delegate.CreateDelegate(typeof(Action), _target, _methodInfos));
        }
    }
}