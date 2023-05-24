using UnityEngine;

namespace _Project.CodeBase.Logic.Enemy
{
    public class EnemyRagdoll : MonoBehaviour
    {
        [SerializeField] private EnemyHitbox[] hitboxes;

        public void Setup(EnemyHealth health)
        {
            foreach (var hitbox in hitboxes) 
                hitbox.SetupHealth(health);
        }

        public void ToggleRagdoll(bool value)
        {
            foreach (var hitbox in hitboxes)
                hitbox.ToggleRagdoll(value);
        }
    }
}