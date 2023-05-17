using UnityEngine;
using System;

namespace Nazio_LT.Tools.Core
{
    public abstract class InputBuffer
    {
        protected float m_lastPressedTime = 999f;
        protected bool m_canConsumme = false, m_consummed = true;

        private const float TIME_TO_SAVE = 0.2f;

        public abstract void TryExecute();

        /// <summary>Reset the object.</summary>
        public void Reset()
        {
            m_canConsumme = false;
            m_consummed = true;
        }

        /// <summary>Consume input if it's available and if the callback return true.</summary>
        protected void ExecuteIfInputIsAvailable<T>(Func<T, bool> callback, T value)
        {
            if (!m_available) return;

            if (callback(value)) m_consummed = true;
        }

        private bool m_available => !m_consummed && (m_canConsumme || (Time.time - m_lastPressedTime) < TIME_TO_SAVE);
    }

    /// <summary>Save inputs.</summary>
    public class InputBuffer<T> : InputBuffer
    {
        public InputBuffer(Func<T, bool> linkedAction)
        {
            this.m_linkedAction = linkedAction;
        }

        private readonly Func<T, bool> m_linkedAction;
        private T m_value = default;

        public void Input(T value, bool performed)
        {
            m_value = value;

            if (!performed)
            {
                m_lastPressedTime = Time.time;
                m_canConsumme = false;
                return;
            }

            m_canConsumme = true;
            m_consummed = false;
        }

        #region Overrided Methods

        public override void TryExecute() => ExecuteIfInputIsAvailable<T>(m_linkedAction, m_value);

        #endregion
    }
}