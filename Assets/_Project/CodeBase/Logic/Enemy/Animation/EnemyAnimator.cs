using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private readonly int _attack = Animator.StringToHash("Attack");
        private readonly int _fall = Animator.StringToHash("Fall");
        private readonly int _hit = Animator.StringToHash("Hit");
        private readonly int _horizontal = Animator.StringToHash("Horizontal");
        private readonly int _vertical = Animator.StringToHash("Vertical");

        public void EnableAnimator() => 
            ToggleAnimator(true);

        public void DisableAnimator() =>
            ToggleAnimator(false);

        public void ToggleAnimator(bool value) => 
            animator.enabled = value;

        public void IdleAnimation() => 
            PlayMoveAnimation(0);

        public void PlayMoveAnimation(float speed)
        {
            animator.SetFloat(_horizontal, speed);
            animator.SetFloat(_vertical, speed);
        }

        public void PlayAttackAnimation() => 
            animator.SetTrigger(_attack);
        
        public void PlayHitAnimation() => 
            animator.SetTrigger(_hit);
        
        public void PlayDeathAnimation() => 
            animator.SetTrigger(_fall);
    }
}