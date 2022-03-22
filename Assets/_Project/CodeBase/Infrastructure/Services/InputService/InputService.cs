using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class InputService : MonoBehaviour
    {
        private InputMaster _input;
        
        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnRotate;
        public event Action OnJump;

        public event Action<bool> OnAttack;
        public event Action OnScope;
        public event Action OnReload;

        private void OnEnable()
        {
            if (_input == null)
            {
                _input = new InputMaster();
                Subscribe();
            }

            _input.Enable();
        }

        private void OnDisable()
        {
            UnSubscribe();
            _input.Disable();
        }

        private void Subscribe()
        {
            _input.PlayerMovement.Move.performed += MovePerformed();
            _input.PlayerMovement.Move.canceled += MoveCanceled();
            _input.PlayerMovement.Rotation.performed += Rotation();
            _input.PlayerMovement.Jump.performed += Jump();
            
            _input.PlayerFight.Attack.performed += Attack(true);
            _input.PlayerFight.Attack.canceled += Attack(false);
            _input.PlayerFight.Scope.performed += Scope();
            _input.PlayerFight.Reload.performed += Reload();
        }

        private void UnSubscribe()
        {
            _input.PlayerMovement.Move.performed -= MovePerformed();
            _input.PlayerMovement.Move.canceled -= MoveCanceled();
            _input.PlayerMovement.Rotation.performed -= Rotation();
            _input.PlayerMovement.Jump.performed -= Jump();
            
            _input.PlayerFight.Attack.performed -= Attack(true);
            _input.PlayerFight.Attack.canceled -= Attack(false);
            _input.PlayerFight.Scope.performed -= Scope();
            _input.PlayerFight.Reload.performed -= Reload();
        }

        private Action<InputAction.CallbackContext> MovePerformed() => 
            ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());

        private Action<InputAction.CallbackContext> MoveCanceled() => 
            ctx => OnMove?.Invoke(Vector2.zero);

        private Action<InputAction.CallbackContext> Rotation() => 
            ctx => OnRotate?.Invoke(ctx.ReadValue<Vector2>());

        private Action<InputAction.CallbackContext> Jump() => 
            ctx => OnJump?.Invoke();
        
        private Action<InputAction.CallbackContext> Attack(bool value) => 
            ctx => OnAttack?.Invoke(value);
        
        private Action<InputAction.CallbackContext> Scope() => 
            ctx => OnScope?.Invoke();

        private Action<InputAction.CallbackContext> Reload() => 
            ctx => OnReload?.Invoke();
    }
}