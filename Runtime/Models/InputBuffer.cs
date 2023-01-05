using UnityEngine;
using System;

namespace Nazio_LT.Tools.Core
{
    public abstract class InputBuffer
    {
        protected float lastPressedTime = 999f;
        protected bool canConsumme = false, consummed = true;

        private const float TIME_TO_SAVE = 0.2f;

        public abstract void TryExecute();

        /// <summary>Reset the object.</summary>
        public void Reset()
        {
            canConsumme = false;
            consummed = true;
        }

        /// <summary>Consume input if it's available and if the callback return true.</summary>
        protected void ExecuteIfInputIsAvailable<T>(Func<T, bool> _callback, T _value)
        {
            if (!available) return;

            if (_callback(_value)) consummed = true;
        }

        private bool available => !consummed && (canConsumme || (Time.time - lastPressedTime) < TIME_TO_SAVE);
    }

    /// <summary>Save inputs.</summary>
    public class InputBuffer<T> : InputBuffer
    {
        public InputBuffer(Func<T, bool> _linkedAction)
        {
            linkedAction = _linkedAction;
        }

        private readonly Func<T, bool> linkedAction;
        private T value = default;

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

        #region Overrided Methods

        public override void TryExecute() => ExecuteIfInputIsAvailable<T>(linkedAction, value);

        #endregion
    }
}