using System;
using UnityEngine;

namespace Code.Scripts
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager _inputManager;
        private InputMaster _input;
        public Action<Vector2> OnMove;
        public Action<Vector2> OnRotate;
        public Action<bool> OnSprint;

        public static InputManager Instance => _inputManager;

        private void Awake()
        {
            _inputManager = this;
        }

        private void OnEnable()
        {
            if (_input == null)
            {
                _input = new InputMaster();
                Subscribe();
            }

            _input.Enable();
        }
        
        private void Subscribe()
        {
            // Move
            _input.Player.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
            _input.Player.Move.canceled += ctx => OnMove?.Invoke(Vector2.zero);

            // Rotate
            _input.Player.Rotation.performed += ctx => OnRotate?.Invoke(ctx.ReadValue<Vector2>());

            // Sprint
            _input.Player.Sprinting.performed += ctx => OnSprint?.Invoke(true);
            _input.Player.Sprinting.canceled += ctx => OnSprint?.Invoke(false);
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}