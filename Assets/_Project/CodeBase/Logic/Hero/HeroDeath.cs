using System;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Hero.State;
using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        public event Action ZeroHealth;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMovement _move;
        [SerializeField] private HeroRotation _rotation;
        [SerializeField] private HeroState _state;
        [SerializeField] private HeroAnimator _animator;
        
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroAmmo _ammo;
        [SerializeField] private HeroReload _reload;
        [SerializeField] private HeroScoping _scoping;
        
        private bool _isDead;

        private void Start() => 
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() => 
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
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
            _health.enabled = false;
            _move.enabled = false;
            _rotation.enabled = false;
            _animator.enabled = false;
            _state.enabled = false;

            _attack.enabled = false;
            _ammo.enabled = false;
            _reload.enabled = false;
            _scoping.enabled = false;
        }
        
        private void ProduceHeroDeath()
        {
            var duration = 0.3f;
            var cc = _move.CharacterController;
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