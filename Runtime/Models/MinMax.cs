namespace Nazio_LT.Core
{
    [System.Serializable]
    public class MinMax
    {
        public MinMax()
        {
            Reset();
        }

        [UnityEngine.SerializeField] private float min;
        [UnityEngine.SerializeField] private float max;

        public void AddValue(float _value)
        {
            if (_value > Max) max = _value;
            if (_value < Min) min = _value;
        }

        public void Reset()
        {
            min = float.MaxValue;
            max = float.MinValue;
        }

        public bool Contain(float _value) => _value > Min && _value < Max;

        public float Min => min;
        public float Max => max;
    }
}