namespace Nazio_LT.Tools.Core
{
    /// <summary>
    /// The MinMax is a class wich contains a minimum and a maximum values. These values can be modified by adding other values if they are not included in the MinMax range.
    /// </summary>
    [System.Serializable]
    public struct MinMax
    {
        public MinMax(float _min, float _max)
        {
            min = _min;
            max = _max;
        }

        [UnityEngine.SerializeField] private float min;
        [UnityEngine.SerializeField] private float max;

        /// <summary>Change the max or min if value isn’t in the MinMax range.</summary>
        public void AddValue(float _value)
        {
            if (_value > Max) max = _value;
            if (_value < Min) min = _value;
        }

        /// <summary>Reset the MinMax.</summary>
        public void Reset()
        {
            min = float.MaxValue;
            max = float.MinValue;
        }

        /// <summary>Return if the value is in the MinMax range.</summary>
        public bool Contain(float _value) => _value > Min && _value < Max;

        /// <summary>Minimum value.</summary>
        public float Min => min;
        /// <summary>Minimum value.</summary>
        public float Max => max;
    }
}