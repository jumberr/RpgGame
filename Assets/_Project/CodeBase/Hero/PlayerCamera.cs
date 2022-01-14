using _Project.CodeBase.Scripts;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private Transform target;
        [SerializeField] private Transform pivot;
        [SerializeField] private Transform cam;
        [SerializeField] private LayerMask collisionLayer;

        [SerializeField] private float FOLLOW_SPEED = 0.2f;
        [SerializeField] private float CAMERA_LOOK_SPEED = 1;
        [SerializeField] private float CAMERA_PIVOT_SPEED = 1;
        [SerializeField] private float MIN_PIVOT_ANGLE = -10;
        [SerializeField] private float MAX_PIVOT_ANGLE = 35;
        [SerializeField] private float COLLISION_RADIUS = 2;
        [SerializeField] private float COLLISION_OFFSET = 0.2f;
        [SerializeField] private float MIN_COLLISION_OFFSET = 0.2f;

        private Vector2 input;
        private Vector3 cameraFollowVelocity;
        private float lookAngle;
        private float pivotAngle;
        private float defaultPosition;
        private Vector3 cameraVectorPosition;

        private void Start()
        {
            inputManager.OnRotate += UpdateInput;
            defaultPosition = cam.localPosition.z;
        }

        private void UpdateInput(Vector2 dir) => 
            input = dir;

        private void FollowTarget()
        {
            var targetPos = Vector3.SmoothDamp
                (transform.position, target.position, ref cameraFollowVelocity, FOLLOW_SPEED);
            transform.position = targetPos;
        }

        private void RotateCamera()
        {
            lookAngle += input.x * CAMERA_LOOK_SPEED;
            pivotAngle -= input.y * CAMERA_PIVOT_SPEED;
            pivotAngle = Mathf.Clamp(pivotAngle, MIN_PIVOT_ANGLE, MAX_PIVOT_ANGLE);

            var rotation = Vector3.zero;
            rotation.y = lookAngle;
            var targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            pivot.localRotation = targetRotation;
        }

        private void HandleCollisions()
        {
            var targetPosition = defaultPosition;
            var direction = cam.position - pivot.position;
            direction.Normalize();
            if (Physics.SphereCast(pivot.position, COLLISION_RADIUS, direction, out var hit,
                Mathf.Abs(targetPosition), collisionLayer))
            {
                var distance = Vector3.Distance(pivot.position, hit.point);
                targetPosition -= distance - COLLISION_OFFSET;
            }

            if (Mathf.Abs(targetPosition) < MIN_COLLISION_OFFSET)
            {
                targetPosition -= MIN_COLLISION_OFFSET;
            }

            cameraVectorPosition.z = Mathf.Lerp(cam.localPosition.z, targetPosition, 0.5f);
            cam.localPosition = cameraVectorPosition;
        }

        private void LateUpdate()
        {
            FollowTarget();
            RotateCamera();
            HandleCollisions();
        }
    }
}