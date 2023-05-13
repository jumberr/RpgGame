using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private EnemyRagdoll ragdoll;

        public void EnableAnimator() => 
            SwitchComponents(true);

        public void DisableAnimator() => 
            SwitchComponents(false);

        private void SwitchComponents(bool animatorValue)
        {
            animator.ToggleAnimator(animatorValue);
            ragdoll.ToggleRagdoll(animatorValue);
        }
    }
}