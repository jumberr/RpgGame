using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Logic.Hero.Animation;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroMovement : MonoBehaviour, ISavedProgress
    {
        public event Action OnMove;
        
        private const float Gravity = -9.81f;

        [Header("Jump:")] 
        [SerializeField] private float _jumpForce = 5f;
        [Header("Movement Speed:")] 
        [SerializeField] private float _runningSpeed = 5f;
        
        [SerializeField] private InputService _inputService;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private CharacterController _characterController;
        
        private Transform _cachedTransform;
        private Vector3 _velocity;
        private Vector2 _input;

        public CharacterController CharacterController => _characterController;
        
        private void Awake() => 
            _characterController = GetComponent<CharacterController>();

        private void Start()
        {
            _cachedTransform = transform;
            _inputService.OnMove += UpdateDirection;
            _inputService.OnJump += JumpAction;
        }

        private void FixedUpdate()
        {
            if (_characterController.isGrounded && _velocity.y <= 0)
                _velocity.y = 0f; //We're standing at the floor

            MovementAction();
            ApplyGravity();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            var savedPosition = progress.PositionData;
            if (savedPosition != null)
                Warp(to: savedPosition);
        }

        public void UpdateProgress(PlayerProgress progress) => 
            progress.PositionData = transform.position.AsVectorData();

        private void UpdateDirection(Vector2 dir)
        {
            _input = dir;
            OnMove?.Invoke();
        }

        private void MovementAction()
        {
            var moveDirection = CalculateDirection();
            ApplyMoveAnimation(moveDirection);
            _characterController.Move(moveDirection * _runningSpeed * Time.deltaTime);
        }

        private void ApplyMoveAnimation(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
                _heroAnimator.EnterIdleState();
            else
                _heroAnimator.EnterWalkState();
        }

        private Vector3 CalculateDirection() =>
            _cachedTransform.right * _input.x + _cachedTransform.forward * _input.y;

        private void JumpAction()
        {
            if (_characterController.isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpForce * -2f * Gravity);
        }

        private void ApplyGravity()
        {
            _velocity.y += Gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void Warp(PositionData to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }
    }
}