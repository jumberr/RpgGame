using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using NTC.Global.Cache;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    [RequireComponent(typeof(HeroAmmo))]
    public class HeroAttack : NightCache, INightRun
    {
        private const float TimeDestroyFX = 0.1f;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private LineFade _lineFade;
        [SerializeField] private WeaponLight _weaponLight;
        [SerializeField] private ShootingParticles _particles;

        private DefaultShooting _defaultShooting;
        private ShotgunShooting _shotgunShooting;
        private WeaponType _weaponType;
        private HeroState _state;
        private InputService _inputService;
        private HeroAmmo _ammo;
        private HeroReload _reload;
        private HeroRecoil _recoil;
        private HeroAnimator _animator;

        private bool _isAutomatic;
        private bool _isShooting;
        private float _fireSpeed;
        private float _automaticFireTimer;
        private float _singleFireTimer;

        public LayerMask LayerMask => _layerMask;

        public void Construct(HeroState state, InputService inputService, HeroAmmo ammo, HeroReload reload, HeroRecoil recoil, HeroAnimator animator)
        {
            _state = state;
            _inputService = inputService;
            _ammo = ammo;
            _reload = reload;
            _recoil = recoil;
            _animator = animator;
            _inputService.AttackAction.Event += EnableDisableShoot;

            _defaultShooting = new DefaultShooting(_state, _lineFade, _layerMask, _particles);
            _shotgunShooting = new ShotgunShooting(_state, _lineFade, _layerMask, _particles);
        }

        public void Run() => 
            HandleAttack();

        private void OnDisable() =>
            _inputService.AttackAction.Event -= EnableDisableShoot;

        public void UpdateStatsAndConfig(Weapon weapon, WeaponConfiguration config)
        {
            var data = weapon.WeaponData;
            
            _isAutomatic = weapon.WeaponData.IsAutomatic;
            _fireSpeed = 60 / weapon.WeaponData.FireRate;

            var shotgun = weapon as Shotgun;
            if (shotgun != null)
            {
                _shotgunShooting.SetupConfig(data.Range, data.AccuracyDistance, data.AimAccuracy, data.Accuracy, data.Damage);
                _shotgunShooting.SetupConfig(shotgun.FractionAmount);
                _weaponType = WeaponType.Shotgun;
            }
            else
            {
                _defaultShooting.SetupConfig(data.Range, data.AccuracyDistance, data.AimAccuracy, data.Accuracy, data.Damage);
                _weaponType = WeaponType.Default;
            }

            SetupWeaponConfiguration(config);
        }

        public void ApplyKnife(Knife knife)
        {
            _fireSpeed = 60 / knife.FireRate;
            _weaponType = WeaponType.Knife;
        }

        public void SetWeaponTypeNone() => 
            _weaponType = WeaponType.None;

        private void SetupWeaponConfiguration(WeaponConfiguration config)
        {
            _weaponLight.Construct(config.LightPoint);
            
            _shotgunShooting.SetupFirePoint(config.FirePoint);
            _defaultShooting.SetupFirePoint(config.FirePoint);
        }

        private void HandleAttack()
        {
            if (_singleFireTimer > 0)
                _singleFireTimer -= Time.deltaTime;

            if (!_isShooting) return;

            if (_isAutomatic)
            {
                if (_automaticFireTimer > 0)
                    _automaticFireTimer -= Time.deltaTime;
                else
                {
                    _automaticFireTimer = _fireSpeed;
                    Shoot();
                }
            }
            else
            {
                if (!(_singleFireTimer <= 0)) return;
                _singleFireTimer = _fireSpeed;
                Shoot();
                ReleaseTrigger();
            }
        }

        private void EnableDisableShoot(bool value)
        {
            if (value)
                PullTrigger();
            else
                ReleaseTrigger();
        }

        private void Shoot()
        {
            if (_weaponType == WeaponType.None || _state.Reloading) return;

            if (!_ammo.CanShoot() && _weaponType != WeaponType.Knife)
            {
                _reload.Reload();
                return;
            }

            _animator.ShootAnimation();

            if (_weaponType != WeaponType.Knife)
            {
                _ammo.UseOneAmmo();
                _weaponLight.TurnOn(TimeDestroyFX);
                _recoil.RecoilFire();

                switch (_weaponType)
                {
                    case WeaponType.Default:
                        _defaultShooting.Shoot();
                        break;
                    case WeaponType.Shotgun:
                        _shotgunShooting.Shoot();
                        break;
                }
            }
        }

        private void PullTrigger() => 
            _isShooting = true;

        private void ReleaseTrigger()
        {
            _isShooting = false;
            if (_isAutomatic) 
                _automaticFireTimer = 0;
        }
    }
}