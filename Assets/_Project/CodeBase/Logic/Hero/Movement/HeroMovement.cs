using System.Threading;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Utils.Extensions;
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
        [SerializeField] private FallingDamage _fallingDamage;

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
        
        [Title("Animation")]
        [SerializeField] private float animationThreshold;
        [SerializeField] private int sprintAnimationValue = 1;
        [SerializeField] private float walkAnimationValue = 0.85f;
        [SerializeField] private float idleAnimationValue;
        
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

            ApplyFallDamage();
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

        public void LoadProgress(PlayerProgress progress) => 
            Warp(to: progress.GameObjectData);

        public void UpdateProgress(PlayerProgress progress)
        {
            var position = CachedTransform.position.AsVectorData();
            var rotation = CachedTransform.rotation.AsRotationData();
            progress.GameObjectData.Update(position, rotation);
        }

        private void UpdateDirection(Vector2 dir) => 
            _input = dir;

        private void ApplyFallDamage()
        {
            if (_state.Grounded && !_state.WasGrounded)
            {
                if (_state.WasGrounded) return;
                
                _fallingDamage.ApplyFallDamage(CachedTransform.position.y);
                _fallingDamage.ResetHighestPoint();
                _state.Jumping = false;
            }
            else
                _fallingDamage.StorePosition(CachedTransform.position.y);
        }

        private void MoveCharacter()
        {
            Vector2 frameInput = Vector3.ClampMagnitude(_input, 1.0f);
            var desiredDirection = CalculateDirection(frameInput);

            CalculateVelocity(desiredDirection);

            var applied = ApplyVelocity();
            _characterController.Move(applied);
            ApplyMoveAnimation(applied);
        }

        private Vector3 CalculateDirection(Vector2 frameInput)
        {
            var desiredDirection = new Vector3(frameInput.x, 0f, frameInput.y);
            desiredDirection = ApplySpeed(frameInput, desiredDirection);
            return CachedTransform.TransformDirection(desiredDirection);
        }

        private void CalculateVelocity(Vector3 desiredDirection)
        {
            if (!_state.Grounded)
            {
                ResetVelocity();
                _velocity += desiredDirection * _accelerationInAir * _airControl * Time.deltaTime;
                _velocity.y -= (_velocity.y >= 0 ? _jumpGravity : _gravity) * Time.deltaTime;
            }
            else if (!_state.Jumping)
                _velocity = Vector3.Lerp(_velocity, new Vector3(desiredDirection.x, _velocity.y, desiredDirection.z), Time.deltaTime * (desiredDirection.sqrMagnitude > 0.0f ? _acceleration : _deceleration));
        }

        private Vector3 ApplyVelocity()
        {
            var applied = _velocity * Time.deltaTime;
            if (_characterController.isGrounded && !_state.Jumping)
                applied.y = -_stickToGroundForce;
            return applied;
        }

        private void ApplyMoveAnimation(Vector3 dir)
        {
            dir.y = 0f;
            
            if (_state.Running)
                _heroAnimator.EnterMoveState(sprintAnimationValue);
            else if (dir.magnitude >= animationThreshold)
                _heroAnimator.EnterMoveState(walkAnimationValue);
            else
                _heroAnimator.EnterMoveState(idleAnimationValue);
        }

        private void ResetVelocity()
        {
            if (_state.WasGrounded && !_state.Jumping)
                _velocity.y = 0f;
        }

        private Vector3 ApplySpeed(Vector2 frameInput, Vector3 desiredDirection)
        {
            if (_state.Running)
                desiredDirection *= _speedRunning;
            else if (_state.Crouching)
                desiredDirection *= _speedCrouching;
            else if (_state.Aiming)
                desiredDirection *= _speedAiming;
            else
            {
                desiredDirection *= _speedWalking;
                desiredDirection.x *= _walkingMultiplierSideways;
                desiredDirection.z *= frameInput.y > 0 ? _walkingMultiplierForward : _walkingMultiplierBackwards;
            }
            return desiredDirection;
        }

        private void Jump()
        {
            if (!_state.Grounded || _state.Crouching && !_canJumpWhileCrouching)
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
            if (!_canCrouch || !_state.Grounded && !_canCrouchWhileFalling)
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
            _characterController.height = newCrouching ? _crouchHeight : _standingHeight;
            _characterController.center = _characterController.height / 2.0f * Vector3.up;
            _state.Crouching = newCrouching;
        }

        private void Warp(GameObjectData to)
        {
            _characterController.enabled = false;
            
            var position = to.Position.AsUnityVector().AddY(_characterController.height) + Vector3.forward * 5;
            var quaternion = to.Rotation.AsQuaternion();
            CachedTransform.SetPositionAndRotation(position, quaternion);
            
            _characterController.enabled = true;
        }
    }
}