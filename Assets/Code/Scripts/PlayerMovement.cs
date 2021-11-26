using System;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _cc;
        [SerializeField] private Transform cam;

        private bool isSprinting;

        [Header("Jump:")] [SerializeField] private float jumpForce = 5f;

        [Header("Falling:")] [SerializeField] private float gravity = 9.81f;
        private Vector3 velocity;

        [Header("Movement Speeds:")] [SerializeField]
        private float walkingSpeed = 1.5f;

        [SerializeField] private float runningSpeed = 5f;
        [SerializeField] private float sprintSpeed = 7f;
        [SerializeField] private float rotationSpeed = 15f;

        private Vector2 input;
        private float clampedInput;
        private Vector3 moveDirection;

        public Action<float, bool> OnMove;

        // Rolling
        public bool InAction { get; set; }
        public bool OneMoveDirection { get; set; }

        private void Start()
        {
            InputManager.Instance.OnMove += UpdateDirection;
            InputManager.Instance.OnSprint += UpdateSprintState;

            InputManager.Instance.OnJump += JumpAction;
            InputManager.Instance.OnRoll += RollingAction;
        }

        private void UpdateDirection(Vector2 dir)
        {
            if (!OneMoveDirection)
                input = dir;
        }

        private void UpdateSprintState(bool sprint)
        {
            if (clampedInput >= 0.5f && sprint)
            {
                isSprinting = true;
            }
            else if (!sprint)
            {
                isSprinting = false;
            }
        }

        private void FixedUpdate()
        {
            if (_cc.isGrounded && velocity.y <= 0)
                velocity.y = 0f; //We're standing at the floor

            MovementAction();
            RotationAction();
            ApplyGravity();

            OnMove?.Invoke(clampedInput, isSprinting);
        }

        private void MovementAction()
        {
            moveDirection = CalculateDirection();

            clampedInput = Mathf.Clamp01(Mathf.Abs(input.x) + Mathf.Abs(input.y));

            if (isSprinting)
            {
                moveDirection *= sprintSpeed;
            }
            else
            {
                if (clampedInput >= 0.5f)
                    moveDirection *= runningSpeed;
                else
                    moveDirection *= walkingSpeed;
            }

            _cc.Move(moveDirection * Time.deltaTime);
        }

        private void RotationAction()
        {
            var targetDirection = CalculateDirection();

            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            var targetRotation = Quaternion.LookRotation(targetDirection);
            var playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }

        private void RollingAction()
        {
            if (!InAction)
            {
                OneMoveDirection = true;
                InAction = true;
                Player.Instance.PlayerAnimator.PlayTargetAnimation(PlayerAnimator.Roll, false,
                    () => OneMoveDirection = false);
            }
        }

        private void JumpAction()
        {
            if (!InAction && _cc.isGrounded)
            {
                OneMoveDirection = true;
                InAction = true;
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
                Player.Instance.PlayerAnimator.PlayTargetAnimation(PlayerAnimator.Jump, false,
                    () => OneMoveDirection = false);
            }
        }

        private void ApplyGravity()
        {
            velocity.y += gravity * Time.deltaTime;
            _cc.Move(velocity * Time.deltaTime);
        }

        private Vector3 CalculateDirection()
        {
            var direction = cam.forward * input.y;
            direction += cam.right * input.x;
            direction.Normalize();
            direction.y = 0;
            return direction;
        }
    }
}