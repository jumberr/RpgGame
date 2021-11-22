using UnityEngine;

namespace Code.Scripts
{
    public class Player : MonoBehaviour
    {
        private static Player _player;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAnimator _playerAnimator;

        public static Player Instance => _player;
        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerAnimator PlayerAnimator => _playerAnimator;

        private void Awake()
        {
            _player = this;
        }
    }
}