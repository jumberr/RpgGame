using UnityEngine;

namespace _Project.CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth Health;
        public HeroMovement Move;
        public HeroRotation Rotation;
        //public PlayerAnimator Animator;

        public GameObject DeathFx;
        private bool _isDead;

        private void Start() => 
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            DisableComponents();
            //Animator.PlayDeath();

            //Instantiate(DeathFx, transform.position, Quaternion.identity);
        }

        private void DisableComponents()
        {
            Move.enabled = false;
            Rotation.enabled = false;
        }
    }
}