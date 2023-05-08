using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private EnemyRagdoll ragdoll;

        public void TurnAnimator() => 
            SwitchComponents(true);

        public void TurnRagdoll() => 
            SwitchComponents(false);

        private void SwitchComponents(bool animatorValue)
        {
            animator.enabled = animatorValue;
            ragdoll.ToggleRagdoll(!animatorValue);
        }
    }
}