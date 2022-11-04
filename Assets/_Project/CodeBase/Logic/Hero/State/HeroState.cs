using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.State
{
    public class HeroState : MonoBehaviour
    {
        private PlayerState _prevPlayerState;
        private PlayerState _playerState;
        private InputService _inputService;

        public event Action<PlayerState> OnChangeState;

        public PlayerState PreviousPlayerState => _prevPlayerState;
        public PlayerState CurrentPlayerState => _playerState;
        
        public bool Aiming { get; set; }
        public bool Running { get; set; }
        public bool Crouching { get; set; }
        public bool Grounded { get; set; }
        public bool WasGrounded { get; set; }
        public bool Jumping { get; set; }

        private void Start() => 
            ChangeState(PlayerState.None);

        private void OnDestroy() => 
            CleanUp();

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
            Subscribe();
        }

        public void Enter(PlayerState newPlayerState)
        {
            ChangeState(newPlayerState);
            OnChangeState?.Invoke(_playerState);
        }

        private void ChangeState(PlayerState newPlayerState)
        {
            _prevPlayerState = _playerState;
            _playerState = newPlayerState;
        }

        private void Subscribe() => 
            _inputService.RunningAction.Event += Run;

        private void CleanUp() => 
            _inputService.RunningAction.Event -= Run;

        private void Run(bool run) => 
            Running = run;
    }
    

}