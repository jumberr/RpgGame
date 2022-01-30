using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    public class HeroMovement : MonoBehaviour, ISavedProgress
    {
        public event Action OnMove;
        
        private const float Gravity = -9.81f;

        [Header("Jump:")] 
        [SerializeField] private float jumpForce = 5f;
        [Header("Movement Speed:")] 
        [SerializeField] private float runningSpeed = 5f;
        
        [SerializeField] private InputService inputService;
        [SerializeField] private CharacterController characterController;
        
        private Transform cachedTransform;
        private Vector3 velocity;
        private Vector2 input;

        public CharacterController CharacterController => characterController;
        
        private void Awake() => 
            characterController = GetComponent<CharacterController>();

        private void Start()
        {
            cachedTransform = transform;
            inputService.OnMove += UpdateDirection;
            inputService.OnJump += JumpAction;
        }

        private void FixedUpdate()
        {
            if (characterController.isGrounded && velocity.y <= 0)
                velocity.y = 0f; //We're standing at the floor

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
            input = dir;
            OnMove?.Invoke();
        }

        private void MovementAction()
        {
            var moveDirection = CalculateDirection();
            characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
        }

        private Vector3 CalculateDirection() =>
            cachedTransform.right * input.x + cachedTransform.forward * input.y;

        private void JumpAction()
        {
            if (characterController.isGrounded)
                velocity.y = Mathf.Sqrt(jumpForce * -2f * Gravity);
        }

        private void ApplyGravity()
        {
            velocity.y += Gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

        private void Warp(PositionData to)
        {
            characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(characterController.height);
            characterController.enabled = true;
        }
    }
}