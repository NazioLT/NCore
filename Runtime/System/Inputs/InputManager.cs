using UnityEngine;
using UnityEngine.InputSystem;

namespace Nazio_LT.Tools.Core
{
    [AddComponentMenu("Nazio_LT/Core/InputManager"), RequireComponent(typeof(PlayerInput))]
    public class InputManager : Singleton<InputManager>
    {
        private PlayerInput playerInput;

        protected override void Awake()
        {
            base.Awake();

            playerInput.GetComponent<PlayerInput>();
        }

        public float GetFloat(string _actionName) => playerInput.actions.FindAction(_actionName).ReadValue<float>();
        public Vector2 GetVector2(string _actionName) => playerInput.actions.FindAction(_actionName).ReadValue<Vector2>();
        public bool GetInput(string _actionName) => playerInput.actions.FindAction(_actionName).IsPressed();

        // public void Register() => 
    }
}