using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Utils.Extensions;
using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroDeath : MonoBehaviour
    {
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
        private InputService _inputService;
        public event Action ZeroHealth;

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
            //_health.Deactivate();
            //_move.Deactivate();
            //_rotation.Deactivate();
            //_animator.Deactivate();
            //_state.Deactivate();
//
            //_attack.Deactivate();
            //_ammo.Deactivate();
            //_reload.Deactivate();
            //_scoping.Deactivate();
        }

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
        }

        private void ProduceHeroDeath()
        {
            var duration = 0.3f;
            _inputService.BlockInput();
            //_move.CharacterController.Deactivate();

            ApplyDeathPositionToHero(_move.CharacterController, duration);

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

        private void RotateHeroCamera(Camera heroCam, float duration) => 
            heroCam.transform.DOLocalRotate(Vector3.left * 90, duration);

        private void SetEmptyCullingMaskToGunCamera(Camera weaponCamera) => 
            weaponCamera.cullingMask = 0;
    }
}