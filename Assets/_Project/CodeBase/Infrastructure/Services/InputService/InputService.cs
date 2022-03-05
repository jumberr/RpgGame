using System;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class InputService : MonoBehaviour
    {
        private InputMaster _input;
        
        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnRotate;
        public event Action OnJump;
 
        public event Action<bool> OnAttack;

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
            _input.PlayerMovement.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
            _input.PlayerMovement.Move.canceled += ctx => OnMove?.Invoke(Vector2.zero);
            
            // Rotate
            _input.PlayerMovement.Rotation.performed += ctx => OnRotate?.Invoke(ctx.ReadValue<Vector2>());

            // Jump
            _input.PlayerMovement.Jump.performed += ctx => OnJump?.Invoke();
            
            // Shoot
            _input.PlayerFight.Attack.performed += ctx => OnAttack?.Invoke(true);
            _input.PlayerFight.Attack.canceled += ctx => OnAttack?.Invoke(false);
        }

        private void OnDisable() => 
            _input.Disable();
    }
}