using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Nazio_LT.Tools.Core
{
    /// <summary>
    /// Envoie des inputs, doit etre enregistré dans l'input manager pour fonctionner.
    /// </summary>
    public interface IInputEventReceiver
    {
        public abstract GameObject Object { get; }
    }

    [AddComponentMenu("Nazio_LT/Core/InputManager"), RequireComponent(typeof(PlayerInput))]
    public class InputManager : Singleton<InputManager>
    {
        private PlayerInput playerInput;

        private List<IInputEventReceiver> receivers = new List<IInputEventReceiver>();

        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        public float GetFloat(string _actionName) => playerInput.actions.FindAction(_actionName).ReadValue<float>();
        public Vector2 GetVector2(string _actionName) => playerInput.actions.FindAction(_actionName).ReadValue<Vector2>();
        public bool GetInput(string _actionName) => playerInput.actions.FindAction(_actionName).IsPressed();

        public void Register(IInputEventReceiver _receiver) => receivers.Add(_receiver);
        public void UnRegister(IInputEventReceiver _receiver) => receivers.Remove(_receiver);

        #region Registered Inputs

        public void OnMove() => SendMessages("OnMove");
        public void OnInventoryInput() => SendMessages("OnInventoryInput");
        public void OnJump() => SendMessages("OnJump");

        #endregion

        protected void SendMessages(string _methodName)
        {
            foreach (var _receiver in receivers)
            {
                _receiver.Object.SendMessage(_methodName);
            }
        }
    }
}