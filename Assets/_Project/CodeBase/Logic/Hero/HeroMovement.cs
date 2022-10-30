using System.Threading;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero.State;
using Cysharp.Threading.Tasks;
using NTC.Global.Cache;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroMovement : NightCache, INightInit, INightFixedRun, ISavedProgress
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroState _state;
        [SerializeField] private CharacterController _characterController;

        [Title("Acceleration")]
        [SerializeField] private float _acceleration;
        [SerializeField] private float _accelerationInAir;
        [SerializeField] private float _deceleration;
        
        [Title("Speeds")]
        [SerializeField] private float _speedWalking;
        [SerializeField] private float _speedAiming;
        [SerializeField] private float _speedCrouching;
        [SerializeField] private float _speedRunning;
        
        [Title("Walking Multipliers")]
        [SerializeField, Range(0.0f, 1.0f)] private float _walkingMultiplierForward;
        [SerializeField, Range(0.0f, 1.0f)] private float _walkingMultiplierSideways;
        [SerializeField, Range(0.0f, 1.0f)] private float _walkingMultiplierBackwards;
        
        [Title("Air")]
        [SerializeField, Range(0.0f, 1.0f)] private float _airControl;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpGravity;

        [Tooltip("The force of the jump.")]
        [SerializeField] private float _jumpForce;

        [Tooltip("Force applied to keep the character from flying away while descending slopes.")]
        [SerializeField] private float _stickToGroundForce;

        [Title("Crouching")]
        [SerializeField] private bool _canCrouch;
        [SerializeField, ShowIf(nameof(_canCrouch))] private bool _canCrouchWhileFalling;
        [SerializeField, ShowIf(nameof(_canCrouch))] private bool _canJumpWhileCrouching;
        [SerializeField, ShowIf(nameof(_canCrouch))] private float _crouchHeight;
        [SerializeField, ShowIf(nameof(_canCrouch))] private LayerMask _crouchOverlapsMask;

        [Title("Rigidbody Push")]
        [SerializeField] private float _rigidbodyPushForce;

        private readonly Collider[] _buffer = new Collider[5];
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private InputService _inputService;

        private Vector3 _velocity;
        private Vector2 _input;
        private float _standingHeight;

        public CharacterController CharacterController => _characterController;

        public void Init() => 
            _standingHeight = _characterController.height;

        public void FixedRun()
        {
            _state.Grounded = _characterController.isGrounded;

            if (_state.Grounded && !_state.WasGrounded)
                _state.Jumping = false;
            
            MoveCharacter();
            _state.WasGrounded = _state.Grounded;
        }

        private void OnDisable()
        {
            _inputService.MoveAction.Event -= UpdateDirection;
            _inputService.JumpAction.Event -= Jump;
            _inputService.CrouchAction.Event -= ToggleCrouch;
        }

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
            _inputService.MoveAction.Event += UpdateDirection;
            _inputService.JumpAction.Event += Jump;
            _inputService.CrouchAction.Event += ToggleCrouch;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            var savedPosition = progress.PositionData;
            if (savedPosition != null)
                Warp(to: savedPosition);
        }

        public void UpdateProgress(PlayerProgress progress) => 
            progress.PositionData = CachedTransform.position.AsVectorData();

        private void UpdateDirection(Vector2 dir) => 
            _input = dir;

        private void ApplyMoveAnimation() => 
            _heroAnimator.EnterMoveState(_input.y);

        private void MoveCharacter()
        {
            Vector2 frameInput = Vector3.ClampMagnitude(_input, 1.0f);
            var desiredDirection = new Vector3(frameInput.x, 0.0f, frameInput.y);
            
            if (_state.Running)
                desiredDirection *= _speedRunning;
            else
            {
                if (_state.Crouching)
                    desiredDirection *= _speedCrouching;
                else
                {
                    if (_state.Aiming)
                        desiredDirection *= _speedAiming;
                    else
                    {
                        desiredDirection *= _speedWalking;
                        desiredDirection.x *= _walkingMultiplierSideways;
                        desiredDirection.z *= frameInput.y > 0 ? _walkingMultiplierForward : _walkingMultiplierBackwards;
                    }
                }
            } 

            desiredDirection = CachedTransform.TransformDirection(desiredDirection);

            if (!_state.Grounded)
            {
                if (_state.WasGrounded && !_state.Jumping)
                    _velocity.y = 0.0f;
                
                _velocity += desiredDirection * _accelerationInAir * _airControl * Time.deltaTime;
                _velocity.y -= (_velocity.y >= 0 ? _jumpGravity : _gravity) * Time.deltaTime;
            }
            else if (!_state.Jumping)
                _velocity = Vector3.Lerp(_velocity, new Vector3(desiredDirection.x, _velocity.y, desiredDirection.z), Time.deltaTime * (desiredDirection.sqrMagnitude > 0.0f ? _acceleration : _deceleration));

            var applied = _velocity * Time.deltaTime;
            if (_characterController.isGrounded && !_state.Jumping)
                applied.y = -_stickToGroundForce;

            _characterController.Move(applied);
        }

        private void Jump()
        {
            if (_state.Crouching && !_canJumpWhileCrouching)
                return;
            
            if (!_state.Grounded)
                return;

            _state.Jumping = true;
            _velocity = new Vector3(_velocity.x, Mathf.Sqrt(2.0f * _jumpForce * _jumpGravity), _velocity.z);
        }

        private async void ToggleCrouch()
        {
            if (!_state.Crouching && CanCrouch(true))
                Crouch(true);

            else if(_state.Crouching)
                await TryUncrouch();
        }

        private bool CanCrouch(bool newCrouching)
        {
            if (!_canCrouch)
                return false;

            if (!_state.Grounded && !_canCrouchWhileFalling)
                return false;
            
            if (newCrouching)
                return true;

            var sphereLocation = CachedTransform.position + Vector3.up * _standingHeight;
            return Physics.OverlapSphereNonAlloc(sphereLocation, _characterController.radius, _buffer, _crouchOverlapsMask) == 0;
        }

        private async UniTask TryUncrouch()
        {
            await UniTask.WaitUntil(UnCrouch, cancellationToken: _cts.Token);
            Crouch(false);
        }

        private bool UnCrouch() => 
            CanCrouch(false);

        private void Crouch(bool newCrouching)
        {
            _state.Crouching = newCrouching;
            _characterController.height = _state.Crouching ? _crouchHeight : _standingHeight;
            _characterController.center = _characterController.height / 2.0f * Vector3.up;
        }

        private void Warp(PositionData to)
        {
            _characterController.enabled = false;
            CachedTransform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }
    }
}