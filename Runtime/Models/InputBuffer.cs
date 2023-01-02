using UnityEngine;

namespace Nazio_LT.Tools.Core
{
    /// <summary>Save inputs.</summary>
    public struct InputBuffer<T>
    {
        private T value;
        private float lastPressedTime;
        private bool canConsumme, consummed;

        private const float TIME_TO_SAVE = 0.2f;

        public void Reset()
        {
            canConsumme = false;
            consummed = true;
        }

        public void Input(T _value, bool _performed)
        {
            value = _value;

            if (!_performed)
            {
                lastPressedTime = Time.time;
                canConsumme = false;
                return;
            }

            canConsumme = true;
            consummed = false;
        }

        public void ExecuteIfInputIsAvailable(System.Func<T, bool> _callback)
        {
            if (!available) return;

            if (_callback(value)) consummed = true;
        }

        private bool available => !consummed && (canConsumme || (Time.time - lastPressedTime) < TIME_TO_SAVE);
    }
}