using _Project.CodeBase.Scripts;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private Transform player;
        [SerializeField] private float mouseSensitivity;
        
        private float xRotation;
        private Vector2 direction;
        
        private void Start() => 
            inputManager.OnRotate += OnRotate;

        private void Update()
        {
            var mouseX = direction.x * mouseSensitivity * Time.deltaTime;
            var mouseY = direction.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            player.Rotate(Vector3.up * mouseX);
            
            direction = Vector2.zero;
        }

        private void OnRotate(Vector2 dir) => 
            direction = dir;
    }
}