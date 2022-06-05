using System;
using System.Collections.Generic;
using _Project.CodeBase.Constants;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using _Project.CodeBase.Utils.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Logic.Hero
{
    [RequireComponent(typeof(InputService), typeof(HeroAmmo))]
    public class HeroShooting : MonoBehaviour
    {
        private const float TimeDestroyFX = 0.1f;
        private const float TimeDestroyEnvFx = 2f;
        
        [SerializeField] private Camera _heroCamera;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private LineFade _lineFade;
        [SerializeField] private WeaponLight _weaponLight;
        [SerializeField] private GameObject _weaponFX;
        [SerializeField] private GameObject _rockParticlesFX;
        [SerializeField] private GameObject _sandParticlesFX;
        [SerializeField] private GameObject _bloodParticlesFX;

        private IPoolManager _poolManager;
        private HeroState _state;
        private InputService _inputService;
        private HeroAmmo _ammo;
        private HeroReload _reload;
        private HeroRecoil _recoil;
        private HeroAnimator _animator;

        private Transform _firePoint;
        
        private bool _isAutomatic;
        private float _damage;
        private float _range;
        
        private bool _isShooting;
        private float _fireSpeed;
        private float _automaticFireTimer;
        private float _singleFireTimer;
        private float _accuracy;
        private float _aimAccuracy;
        private float _accuracyDistance;

        private int _fractionCount;

        public void Construct(IPoolManager poolManager)
        {
            _poolManager = poolManager;
            _poolManager.Initialize();
        }
        
        public void Construct(
            HeroState state,
            InputService inputService,
            HeroAmmo ammo,
            HeroReload reload,
            HeroRecoil recoil,
            HeroAnimator animator)
        {
            _state = state;
            _inputService = inputService;
            _ammo = ammo;
            _reload = reload;
            _recoil = recoil;
            _animator = animator;
            _inputService.OnAttack += EnableDisableShoot;
        }

        private void Update()
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
                if (_singleFireTimer <= 0)
                {
                    _singleFireTimer = _fireSpeed;
                    Shoot();
                    ReleaseTrigger();
                }
            }
        }

        private void OnDisable() => 
            _inputService.OnAttack -= EnableDisableShoot;

        public void UpdateConfig(Weapon weapon, WeaponConfiguration config)
        {
            //var weapon = _weapon.WeaponData;
            _isAutomatic = weapon.WeaponData.IsAutomatic;
            _damage = weapon.WeaponData.Damage;
            _range = weapon.WeaponData.Range;
            _fireSpeed = 60 / weapon.WeaponData.FireRate;
            _accuracy = weapon.WeaponData.Accuracy;
            _aimAccuracy = weapon.WeaponData.AimAccuracy;
            _accuracyDistance = weapon.WeaponData.AccuracyDistance;

            var shotgun = weapon as Shotgun;
            _fractionCount = shotgun != null ? shotgun.FractionAmount : 1;

            SetupWeaponConfiguration(config);
        }

        private void SetupWeaponConfiguration(WeaponConfiguration config)
        {
            _firePoint = config.FirePoint;
            _weaponLight.Construct(config.LightPoint);
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
            if (_state.CurrentPlayerState == PlayerState.Reload) return;

            if (!_ammo.CanShoot())
            {
                _reload.Reload();
                return;
            }
            
            _ammo.UseOneAmmo();
            _animator.ShootAnimation();
            _weaponLight.TurnOn(TimeDestroyFX);
            _recoil.RecoilFire();
            MuzzleFlash();

            var dir = _fractionCount == 1 ? new List<Vector3> {RandShootDir()} : RandShotgunDir();

            for (var i = 0; i < _fractionCount; i++)
            {
                if (Physics.Raycast(_heroCamera.transform.position, dir[i], out var hit, _range, _layerMask))
                {
                    //if (hit.transform.TryGetComponent<IHealth>(out var enemy))
                    //    Debug.Log(enemy);

                    HitParticles(hit, TagConstants.SandTag, _sandParticlesFX);
                    HitParticles(hit, TagConstants.RockTag, _rockParticlesFX);
                    SpawnBulletTrail(_firePoint.position, hit.point);
                    Debug.Log(hit.collider.name);
                }
                else
                    SpawnBulletTrail(_firePoint.position, _heroCamera.transform.position + dir[i] * 0.15f);
            }   
            
        }

        private async void MuzzleFlash()
        {
            var fx = _poolManager.SpawnObject(_weaponFX, _firePoint.position, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyFX));
            _poolManager.ReleaseObject(fx);
        }

        private async void SpawnBulletTrail(Vector3 start, Vector3 end)
        {
            var lineFade = _poolManager.SpawnObject(_lineFade.gameObject, start, Quaternion.identity);
            lineFade.GetComponent<LineFade>().SetPositions(start, end);
            
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyFX));
            _poolManager.ReleaseObject(lineFade);
        }

        private async void HitParticles(RaycastHit hit, string tag, GameObject particles)
        {
            if (!hit.collider.CompareTag(tag)) return;
            var fx = _poolManager.SpawnObject(particles, hit.point, Quaternion.LookRotation(hit.normal));
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyEnvFx));
            _poolManager.ReleaseObject(fx);
        }

        private void PullTrigger() => 
            _isShooting = true;

        private void ReleaseTrigger()
        {
            _isShooting = false;
            if (_isAutomatic) 
                _automaticFireTimer = 0;
        }

        private List<Vector3> RandShotgunDir()
        {
            var randShotgunDir = new List<Vector3>();

            for (var i = 0; i < _fractionCount; i++) 
                randShotgunDir.Add(RandShootDir());

            return randShotgunDir;
        }

        private Vector3 RandShootDir()
        {
            var distanceDispersion = RandDistance(); // dispersion distance
            var angleDispersion = Random.Range(0, 2 * Mathf.PI); // dispersion angle
 
            var coordX = distanceDispersion * Mathf.Cos(angleDispersion);
            var coordY = distanceDispersion * Mathf.Sin(angleDispersion);
            return _heroCamera.transform.forward * _accuracyDistance + new Vector3(coordX, coordY, 0);
        }

        private float RandDistance() => 
            (_state.CurrentPlayerState == PlayerState.Scoping ? _aimAccuracy : _accuracy) * Mathf.Sqrt(-2 * Mathf.Log(1 - Random.Range(0, 1f)));
    }
}