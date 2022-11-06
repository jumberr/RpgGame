using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.State
{
    public class HeroState : MonoBehaviour
    {
        private InputService _inputService;
        private bool _crouching;
        private bool _aiming;

        public event Action OnCrouchingChanged;
        public event Action OnAimingChanged;

        public bool Reloading { get; set; }
        public bool Grounded { get; set; }
        public bool WasGrounded { get; set; }
        public bool Jumping { get; set; }
        public bool Running { get; private set; }

        public bool Aiming
        {
            get => _aiming;
            set
            {
                _aiming = value;
                OnAimingChanged?.Invoke();
            }
        }
        
        public bool Crouching 
        {
            get => _crouching;
            set
            {
                _crouching = value;
                OnCrouchingChanged?.Invoke();
            }
        }

        private void OnDestroy() => 
            Cleanup();

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
            Subscribe();
        }
        
        private void Subscribe() => 
            _inputService.RunningAction.Event += Run;

        private void Cleanup() => 
            _inputService.RunningAction.Event -= Run;

        private void Run(bool run) => 
            Running = run;
    }
}