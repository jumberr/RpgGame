using System;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _cc;
        [SerializeField] private Transform cam;
        private Vector2 moveInput;
        private Vector3 moveDirection;
        private const float playerSpeed = 5f;
        private float rotationSpeed = 15f;

        public Action<Vector2> OnMove;

        private void Start()
        {
            InputManager.Instance.OnMove += UpdateDirection;
        }

        private void UpdateDirection(Vector2 dir)
        {
            moveInput = dir;
        }

        private void HandleMovement()
        {
            moveDirection = CalculateDirection();
            moveDirection *= playerSpeed;

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
            var direction = cam.forward * moveInput.y;
            direction += cam.right * moveInput.x;
            direction.Normalize();
            direction.y = 0;
            return direction;
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();

            OnMove?.Invoke(moveInput);
        }
    }
}