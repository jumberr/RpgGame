using UnityEngine;

namespace Code.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAnimator _playerAnimator;
        
        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerAnimator PlayerAnimator => _playerAnimator;
    }
}