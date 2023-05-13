namespace Nazio_LT.Tools.Core
{
    /// <summary>
    /// The MinMax is a class wich contains a minimum and a maximum values. These values can be modified by adding other values if they are not included in the MinMax range.
    /// </summary>
    [System.Serializable]
    public struct MinMax
    {
        public MinMax(float min, float max)
        {
            this.m_min = min;
            this.m_max = max;
        }

        [UnityEngine.SerializeField] private float m_min;
        [UnityEngine.SerializeField] private float m_max;

        /// <summary>Change the max or min if value isn’t in the MinMax range.</summary>
        public void AddValue(float value)
        {
            if (value > Max) m_max = value;
            if (value < Min) m_min = value;
        }

        /// <summary>Reset the MinMax.</summary>
        public void Reset()
        {
            m_min = float.MaxValue;
            m_max = float.MinValue;
        }

        /// <summary>Return if the value is in the MinMax range.</summary>
        public bool Contain(float value) => value > Min && value < Max;

        /// <summary>Minimum value.</summary>
        public float Min => m_min;
        /// <summary>Minimum value.</summary>
        public float Max => m_max;
    }
}