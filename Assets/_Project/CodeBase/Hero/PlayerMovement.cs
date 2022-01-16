using _Project.CodeBase.Scripts;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float Gravity = -9.81f;
        
        [Header("Jump:")] 
        [SerializeField] private float jumpForce = 5f;
        [Header("Movement Speed:")]
        [SerializeField] private float runningSpeed = 5f;
        
        [SerializeField] private InputManager inputManager;
        [SerializeField] private CharacterController characterController;

        private PlayerAnimator _playerAnimator;
        private Transform cachedTransform;
        private Vector3 velocity;
        private Vector2 input;

        public void Construct(PlayerAnimator playerAnimator) =>
            _playerAnimator = playerAnimator;

        private void Start()
        {
            cachedTransform = transform;
            inputManager.OnMove += UpdateDirection;
            inputManager.OnJump += JumpAction;
        }

        private void FixedUpdate()
        {
            if (characterController.isGrounded && velocity.y <= 0)
                velocity.y = 0f; //We're standing at the floor

            MovementAction();
            ApplyGravity();
        }

        private void UpdateDirection(Vector2 dir) => 
            input = dir;

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
    }
}