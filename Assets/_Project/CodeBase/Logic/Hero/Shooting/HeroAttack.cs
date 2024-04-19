using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using _Project.CodeBase.StaticData;
using NTC.Global.Cache;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    [RequireComponent(typeof(HeroAmmo))]
    public class HeroAttack : NightCache, INightRun
    {
        private const float TimeDestroyFX = 0.1f;
        private const int SecondsInMinute = 60;

        [SerializeField] private HeroFacade _facade;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private LineFade _lineFade;
        [SerializeField] private WeaponLight _weaponLight;
        [SerializeField] private ShootingParticles _particles;

        private IShootingSystem _shooting;
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

        
        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
            _state = _facade.State;
            _ammo = _facade.Ammo;
            _reload = _facade.Reload;
            _recoil = _facade.Recoil;
            _animator = _facade.Animator;
            _inputService.AttackAction.Event += EnableDisableShoot;

            _shooting = new DefaultShooting(_state, _lineFade, _layerMask, _particles);
        }

        public void Run() => 
            HandleAttack();

        private void OnDestroy() => 
            _inputService.AttackAction.Event -= EnableDisableShoot;

        public void UpdateStatsAndConfig<T>(WeaponInfo<T> info, WeaponConfiguration config) where T : GunSpecs
        {
            var specs = info.GetSpecs();
            
            _isAutomatic = specs.IsAutomatic;
            UpdateFireRate(specs);
            
            _shooting.SetupConfig(specs);
            _weaponType = WeaponType.Default;
            
            SetupWeaponConfiguration(config);
        }

        public void ApplyKnife(KnifeInfo knifeInfo)
        {
            UpdateFireRate(knifeInfo.GetSpecs());
            _weaponType = WeaponType.Knife;
        }

        public void SetWeaponTypeNone() => 
            _weaponType = WeaponType.None;

        private void SetupWeaponConfiguration(WeaponConfiguration config)
        {
            _weaponLight.Construct(config.LightPoint);
            _shooting.SetupFirePoint(config.FirePoint);
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
                
                _shooting.Shoot();
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

        private void UpdateFireRate(WeaponSpecs weaponSpecs) => 
            _fireSpeed = SecondsInMinute / weaponSpecs.FireRate;
    }
}