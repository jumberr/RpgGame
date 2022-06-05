using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.State
{
    public class HeroState : MonoBehaviour
    {
        public event Action<PlayerState> OnChangeState;
        
        private PlayerState _prevPlayerState;
        private PlayerState _playerState;
        
        public PlayerState PreviousPlayerState => _prevPlayerState;
        public PlayerState CurrentPlayerState => _playerState;

        private void Start() => 
            ChangeState(PlayerState.None);

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
    }
}