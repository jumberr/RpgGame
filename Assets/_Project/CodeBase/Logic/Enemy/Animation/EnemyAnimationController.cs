using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private EnemyRagdoll ragdoll;
        [field: SerializeField] public EnemyAnimator Animator { get; private set; }

        public void EnableAnimator() => 
            SwitchComponents(true);

        public void DisableAnimator() => 
            SwitchComponents(false);

        private void SwitchComponents(bool animatorValue)
        {
            Animator.ToggleAnimator(animatorValue);
            ragdoll.ToggleRagdoll(animatorValue);
        }
    }
}