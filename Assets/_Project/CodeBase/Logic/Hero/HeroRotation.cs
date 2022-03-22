using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [RequireComponent(typeof(InputService))]
    public class HeroRotation : MonoBehaviour
    {
        private const int MinRotationAngleY = -90;
        private const int MaxRotationAngleY = 90;
        
        [SerializeField] private InputService _inputService;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _hands;
        [SerializeField] private float _mouseSensitivity;
        
        private float _xRotation;
        private Vector2 _direction;
        
        private void Start() => 
            _inputService.OnRotate += OnRotate;

        private void OnDisable() => 
            _inputService.OnRotate -= OnRotate;

        private void Update()
        {
            var mouseX = _direction.x * _mouseSensitivity * Time.deltaTime;
            var mouseY = _direction.y * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, MinRotationAngleY, MaxRotationAngleY);
            
            _hands.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _player.Rotate(Vector3.up * mouseX);
            
            _direction = Vector2.zero;
        }

        private void OnRotate(Vector2 dir) => 
            _direction = dir;
    }
}