using System;
using _Project.CodeBase.Infrastructure.Services;
using _Project.CodeBase.Logic.Hero.State;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class FootstepsController : MonoBehaviour
    {
        [SerializeField] private HeroState _state;
        [Space]
        [SerializeField] private float _baseStepSpeed;
        [SerializeField] private float _crouchStepMultiplier;
        [SerializeField] private float _sprintStepMultiplier;
        [Space]
        [SerializeField] private float _raycastDistance;
        
        private SoundService _soundService;
        private float _footstepTimer;


        [Inject]
        private void Construct(SoundService soundService) => 
            _soundService = soundService;

        public void HandleMoveFootsteps(Vector2 input)
        {
            if (!_state.Grounded || input == Vector2.zero) return;
            
            _footstepTimer -= Time.deltaTime;
            
            if (!(_footstepTimer <= 0)) return;
            if (!TryPlayFootsteps(GetMoveState())) return;
            
            _footstepTimer = GetCurrentOffset();
        }

        public void HandleJumpFootsteps() => 
            TryPlayFootsteps(MoveType.Jump);

        private bool TryPlayFootsteps(MoveType moveType)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, out var hit, _raycastDistance)) return false;

            PlayFootstep(moveType, hit.collider.tag);
            return true;
        }

        private void PlayFootstep(MoveType moveType, string colliderTag)
        {
            if (!Enum.TryParse(colliderTag, out SurfaceType surfaceType)) return;

            _soundService.PlayMovementSound(surfaceType, moveType).Forget();
        }

        private MoveType GetMoveState() => 
            _state.Running ? MoveType.Run : MoveType.Walk;

        private float GetCurrentOffset() =>
            _state.Crouching 
                ? _baseStepSpeed * _crouchStepMultiplier 
                : _state.Running ? _baseStepSpeed * _sprintStepMultiplier : _baseStepSpeed;
    }
}