﻿using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroRotation : MonoBehaviour
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

        private void Start() => 
            _inputService.RotateAction.Event += OnRotate;

        private void OnDisable() => 
            _inputService.RotateAction.Event -= OnRotate;

        public void SetInputService(InputService inputService) => 
            _inputService = inputService;

        private void Update()
        {
            var mouseX = Direction.x * _mouseSensitivity * Time.deltaTime;
            var mouseY = Direction.y * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, MinRotationAngleY, MaxRotationAngleY);
            
            _hands.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _player.Rotate(Vector3.up * mouseX);
            
            Direction = Vector2.zero;
        }

        public void UpdateSensitivity(float sensitivity) => 
            _mouseSensitivity = sensitivity * Multiplier;

        private void OnRotate(Vector2 dir) => 
            Direction = dir;
    }
}