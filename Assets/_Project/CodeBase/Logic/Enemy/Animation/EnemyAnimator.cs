using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void EnableAnimator() => 
            ToggleAnimator(true);

        public void DisableAnimator() =>
            ToggleAnimator(false);

        public void ToggleAnimator(bool value) => 
            animator.enabled = value;
    }
}