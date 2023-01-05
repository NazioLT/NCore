using UnityEngine;
using System;

namespace Nazio_LT.Tools.Core
{
    /// <summary>Save inputs.</summary>
    public class InputBuffer<T>
    {
        public InputBuffer(Func<T, bool> _linkedAction)
        {
            linkedAction = _linkedAction;
        }

        private readonly Func<T, bool> linkedAction;

        private T value = default;
        private float lastPressedTime = 999f;
        private bool canConsumme = false, consummed = true;

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

        /// <summary>Consume input if it's available and if the callback return true.</summary>
        public void ExecuteIfInputIsAvailable(Func<T, bool> _callback)
        {
            if (!available) return;

            if (_callback(value)) consummed = true;
        }

        public void TryExecute() => ExecuteIfInputIsAvailable(linkedAction);

        private bool available => !consummed && (canConsumme || (Time.time - lastPressedTime) < TIME_TO_SAVE);
    }
}