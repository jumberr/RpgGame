using System;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _cc;
        [SerializeField] private Transform cam;

        private bool isSprinting;

        [Header("Movement Speeds:")] [SerializeField]
        private float walkingSpeed = 1.5f;

        [SerializeField] private float runningSpeed = 5f;
        [SerializeField] private float sprintSpeed = 7f;
        [SerializeField] private float rotationSpeed = 15f;

        private Vector2 input;
        private float clampedInput;
        private Vector3 moveDirection;

        public Action<float, bool> OnMove;

        private void Start()
        {
            InputManager.Instance.OnMove += UpdateDirection;
            InputManager.Instance.OnSprint += UpdateSprintState;
        }

        private void UpdateDirection(Vector2 dir)
        {
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

        private void HandleMovement()
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

        private void HandleRotation()
        {
            var targetDirection = CalculateDirection();

            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            var targetRotation = Quaternion.LookRotation(targetDirection);
            var playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }

        private Vector3 CalculateDirection()
        {
            var direction = cam.forward * input.y;
            direction += cam.right * input.x;
            direction.Normalize();
            direction.y = 0;
            return direction;
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();

            OnMove?.Invoke(clampedInput, isSprinting);
        }
    }
}