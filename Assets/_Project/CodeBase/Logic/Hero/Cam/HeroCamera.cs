using _Project.CodeBase.Infrastructure.Services.InputService;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Cam
{
    public class HeroCamera : NightCache, INightInit, INightLateRun
    {
        [SerializeField] private Transform _player;
        [SerializeField] private bool _smooth;
        [SerializeField] private float _interpolationSpeed;
        [SerializeField] private Vector2 _yClamp;
        
        private InputService _inputService;
        private float _mouseSensitivity;
        private float _xRotation;
        private Quaternion _rotationCharacter;
        private Quaternion _rotationCamera;

        private Vector2 Direction { get; set; }

        public void Init()
        {
            _rotationCharacter = _player.localRotation;
            _rotationCamera = transform.localRotation;
        }

        public void LateRun()
        {
            var input = Direction * _mouseSensitivity;

            var rotationYaw = Quaternion.Euler(0.0f, input.x, 0.0f);
            var rotationPitch = Quaternion.Euler(-input.y, 0.0f, 0.0f);
            
            _rotationCamera *= rotationPitch;
            _rotationCamera = Clamp(_rotationCamera);
            _rotationCharacter *= rotationYaw;
            
            var localRotation = transform.localRotation;

            if (_smooth)
            {
                localRotation = Quaternion.Slerp(localRotation, _rotationCamera, Time.deltaTime * _interpolationSpeed);
                localRotation = Clamp(localRotation);
                _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation, _rotationCharacter, Time.deltaTime * _interpolationSpeed);
            }
            else
            {
                localRotation *= rotationPitch;
                localRotation = Clamp(localRotation);
                _player.transform.rotation *= rotationYaw;
            }
            
            CachedTransform.localRotation = localRotation;
        }

        private void OnDisable() => 
            _inputService.RotateAction.Event -= OnRotate;

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
            _inputService.RotateAction.Event += OnRotate;
        }

        public void UpdateSensitivity(float sensitivity) => 
            _mouseSensitivity = sensitivity;

        private void OnRotate(Vector2 dir) => 
            Direction = dir;

        private Quaternion Clamp(Quaternion rotation)
        {
            rotation.x /= rotation.w;
            rotation.y /= rotation.w;
            rotation.z /= rotation.w;
            rotation.w = 1.0f;

            var pitch = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rotation.x);

            pitch = Mathf.Clamp(pitch, _yClamp.x, _yClamp.y);
            rotation.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * pitch);

            return rotation;
        }
    }
}