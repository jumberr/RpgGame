using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        public event Action ZeroHealth;
        [SerializeField] public HeroHealth health;
        [SerializeField] public HeroMovement move;
        [SerializeField] public HeroRotation rotation;

        private bool _isDead;

        private void Start() => 
            health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && health.Current <= 0)
                Die();
        }

        private void Die()
        {
            ZeroHealth?.Invoke();
            _isDead = true;
            DisableComponents();
            ProduceHeroDeath();
        }

        private void DisableComponents()
        {
            move.enabled = false;
            rotation.enabled = false;
        }
        
        private void ProduceHeroDeath()
        {
            var duration = 0.3f;
            var cc = move.CharacterController;
            cc.enabled = false;

            ApplyDeathPositionToHero(cc, duration);

            var cams = gameObject.GetComponentsInChildren<Camera>();
            RotateHeroCamera(cams[0], duration);
            SetEmptyCullingMaskToGunCamera(cams[1]);
        }

        private void ApplyDeathPositionToHero(CharacterController cc, float duration)
        {
            var pos = gameObject.transform.position;
            pos.y -= cc.height;
            gameObject.transform.DOLocalMove(pos, duration);
        }

        private static void RotateHeroCamera(Camera heroCam, float duration) => 
            heroCam.transform.DOLocalRotate(Vector3.left * 90, duration);

        private static void SetEmptyCullingMaskToGunCamera(Camera weaponCamera) => 
            weaponCamera.cullingMask = 0;
    }
}