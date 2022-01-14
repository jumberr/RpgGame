﻿using System;
using UnityEngine;

namespace _Project.CodeBase.Scripts
{
    public class InputManager : MonoBehaviour
    {
        private InputMaster _input;
        
        public Action<Vector2> OnMove;
        public Action<Vector2> OnRotate;
        public Action<bool> OnSprint;
        
        public Action OnJump;
        public Action OnRoll;

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

            // Sprint
            _input.PlayerMovement.Sprinting.performed += ctx => OnSprint?.Invoke(true);
            _input.PlayerMovement.Sprinting.canceled += ctx => OnSprint?.Invoke(false);
            
            // Jump
            _input.PlayerMovement.Jump.performed += ctx => OnJump?.Invoke();

            // Roll
            _input.PlayerMovement.Rolling.performed += ctx => OnRoll?.Invoke();
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}