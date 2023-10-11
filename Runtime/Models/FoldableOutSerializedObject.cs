using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    [System.Serializable]
    public struct FoldableOutSerializedObject<T> where T : ScriptableObject
    {
        public FoldableOutSerializedObject(System.Action updateCallback)
        {
            m_scriptableObject = default;
            FoldOut = false;
        }

        [SerializeField] private T m_scriptableObject;
        [SerializeField, HideInInspector] public bool FoldOut;

        public T ScriptableObject => m_scriptableObject;
    }
}