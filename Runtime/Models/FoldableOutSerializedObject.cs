using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public struct FoldableOutSerializedObject<T> where T : ScriptableObject
    {
        public FoldableOutSerializedObject(System.Action _updateCallback)
        {
            scriptableObject = default;
            foldOut = false;
        }

        [SerializeField] private T scriptableObject;
        [SerializeField, HideInInspector] public bool foldOut;

        public T ScriptableObject => scriptableObject;
    }
}