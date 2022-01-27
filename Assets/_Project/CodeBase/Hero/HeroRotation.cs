using _Project.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    [RequireComponent(typeof(InputService))]
    public class HeroRotation : MonoBehaviour
    {
        private const int MinRotationAngleY = -90;
        private const int MaxRotationAngleY = 90;
        
        [SerializeField] private InputService inputService;
        [SerializeField] private Transform player;
        [SerializeField] private Transform hands;
        [SerializeField] private float mouseSensitivity;
        
        private float xRotation;
        private Vector2 direction;
        
        private void Start() => 
            inputService.OnRotate += OnRotate;

        private void Update()
        {
            var mouseX = direction.x * mouseSensitivity * Time.deltaTime;
            var mouseY = direction.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, MinRotationAngleY, MaxRotationAngleY);
            
            hands.localRotation = Quaternion.Euler(xRotation, 0, 0);
            player.Rotate(Vector3.up * mouseX);
            
            direction = Vector2.zero;
        }

        private void OnRotate(Vector2 dir) => 
            direction = dir;
    }
}