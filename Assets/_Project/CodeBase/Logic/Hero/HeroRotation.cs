using _Project.CodeBase.Infrastructure.Services.InputService;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroRotation : NightCache, INightRun
    {
        private const int MinRotationAngleY = -90;
        private const int MaxRotationAngleY = 90;
        private const int Multiplier = 100;

        [SerializeField] private Transform _player;
        [SerializeField] private Transform _hands;
        
        private InputService _inputService;
        private float _mouseSensitivity;
        private float _xRotation;

        public Vector2 Direction { get; private set; }

        public void Run() => 
            ProceedRotation();

        private void OnDisable() => 
            _inputService.RotateAction.Event -= OnRotate;

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
            _inputService.RotateAction.Event += OnRotate;
        }

        public void UpdateSensitivity(float sensitivity) => 
            _mouseSensitivity = sensitivity * Multiplier;

        private void ProceedRotation()
        {
            var mouseX = Direction.x * _mouseSensitivity * Time.deltaTime;
            var mouseY = Direction.y * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, MinRotationAngleY, MaxRotationAngleY);

            _hands.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _player.Rotate(Vector3.up * mouseX);

            Direction = Vector2.zero;
        }

        private void OnRotate(Vector2 dir) => 
            Direction = dir;
    }
}