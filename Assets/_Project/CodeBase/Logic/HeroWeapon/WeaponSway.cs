using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponSway : MonoBehaviour
    {
        [Header("Weapon Sway")]
        [SerializeField] private float _swayAmount;
        [SerializeField] private bool _swayInvertX;
        [SerializeField] private bool _swayInvertY;
        [SerializeField] private float _swaySmoothing;
        [SerializeField] private float _swayResetSmoothing;
        [SerializeField] private float _swayClampX;
        [SerializeField] private float _swayClampY;

        [Header("Weapon Movement Sway")]
        [SerializeField] private float _movementSwayX;
        [SerializeField] private float _movementSwayY;
        [SerializeField] private bool _movementSwayXInverted;
        [SerializeField] private bool _movementSwayYInverted;
        [SerializeField] private float _movementSwaySmoothing;

        [Header("Weapon Breathing Sway")] 
        [SerializeField] private Transform _breathSway;
        [SerializeField] private float _swayAmountA;
        [SerializeField] private float _swayAmountB;
        [SerializeField] private float _swayScale;
        [SerializeField] private float _swayLerpSpeed;

        private InputService _inputService;

        private Vector3 _newRotation;
        private Vector3 _newRotationVelocity;
        private Vector3 _targetRotation;
        private Vector3 _targetRotationVelocity;

        private Vector3 _newMovementRotation;
        private Vector3 _newMovementRotationVelocity;
        private Vector3 _targetMovementRotation;
        private Vector3 _targetMovementRotationVelocity;
        
        private float _swayTime;
        private Vector3 _swayPosition;
        
        private Vector2 _viewInput;
        private Vector2 _moveInput;

        private void Start() => 
            _newRotation = transform.localRotation.eulerAngles;

        private void OnDisable()
        {
            _inputService.RotateAction.Event -= OnRotate;
            _inputService.MoveAction.Event -= UpdateDirection;
        }

        private void Update()
        {
            DefaultWeaponSway();
            MovementSway();
            SwayBreathing();

            transform.localRotation = Quaternion.Euler(_newRotation + _newMovementRotation);
        }

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
            _inputService.RotateAction.Event += OnRotate;
            _inputService.MoveAction.Event += UpdateDirection;
        }

        private void MovementSway()
        {
            _targetMovementRotation.z = _movementSwayX * (_movementSwayXInverted ? -_moveInput.x : _moveInput.x);
            _targetMovementRotation.x = _movementSwayY * (_movementSwayYInverted ? -_moveInput.y : _moveInput.y);

            _targetMovementRotation = Vector3.SmoothDamp(_targetMovementRotation, Vector3.zero, ref _targetMovementRotationVelocity, _movementSwaySmoothing);
            _newMovementRotation = Vector3.SmoothDamp(_newMovementRotation, _targetMovementRotation, ref _newMovementRotationVelocity, _movementSwaySmoothing);
        }

        private void DefaultWeaponSway()
        {
            _targetRotation.y += _swayAmount * (_swayInvertX ? -_viewInput.x : _viewInput.x) * Time.deltaTime;
            _targetRotation.x += _swayAmount * (_swayInvertY ? _viewInput.y : -_viewInput.y) * Time.deltaTime;

            _targetRotation.x = Mathf.Clamp(_targetRotation.x, -_swayClampX, _swayClampX);
            _targetRotation.y = Mathf.Clamp(_targetRotation.y, -_swayClampY, _swayClampY);
            _targetRotation.z = _targetRotation.y;

            _targetRotation = Vector3.SmoothDamp(_targetRotation, Vector3.zero, ref _targetRotationVelocity, _swayResetSmoothing);
            _newRotation = Vector3.SmoothDamp(_newRotation, _targetRotation, ref _newRotationVelocity, _swaySmoothing);
        }

        private void SwayBreathing()
        {
            var targetPosition = LissajousCurve(_swayTime, _swayAmountA, _swayAmountB) / _swayScale;
            _swayPosition = Vector3.Lerp(_swayPosition, targetPosition, Time.smoothDeltaTime * _swayLerpSpeed);
            _swayTime += Time.deltaTime;

            if (_swayTime > 6.3f) 
                _swayTime = 0f;

            _breathSway.localPosition = _swayPosition;
        }

        private Vector3 LissajousCurve(float time, float a, float b) => 
            new Vector3(Mathf.Sin(time), a * Mathf.Sin(b * time + Mathf.PI));

        private void OnRotate(Vector2 dir) => 
            _viewInput = dir;
        
        private void UpdateDirection(Vector2 dir) => 
            _moveInput = dir;
    }
}