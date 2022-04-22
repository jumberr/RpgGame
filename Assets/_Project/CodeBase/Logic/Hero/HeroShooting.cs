using System;
using _Project.CodeBase.Constants;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
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

        [SerializeField] private Weapon _weapon;
        [SerializeField] private HeroState _state;
        [SerializeField] private InputService _inputService;
        [SerializeField] private HeroAmmo _ammo;
        [SerializeField] private HeroReload _reload;
        [SerializeField] private HeroRecoil _recoil;
        
        [SerializeField] private Camera _heroCamera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _firePoint;

        [SerializeField] private LineFade _lineFade;
        [SerializeField] private WeaponLight _weaponLight;
        [SerializeField] private GameObject _weaponFX;
        [SerializeField] private GameObject _rockParticlesFX;
        [SerializeField] private GameObject _sandParticlesFX;
        [SerializeField] private GameObject _bloodParticlesFX;

        private IPoolManager _poolManager;
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

        public void Construct(IPoolManager poolManager)
        {
            _poolManager = poolManager;
            _poolManager.Initialize();
        }

        private void Start()
        {
            Initialize();
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

        private void Initialize()
        {
            _ammo.Construct(_weapon);
            _reload.Construct(_weapon);
            _recoil.Construct(_weapon);
            
            var weapon = _weapon.WeaponData;
            _isAutomatic = weapon.IsAutomatic;
            _damage = weapon.Damage;
            _range = weapon.Range;
            _fireSpeed = 60 / weapon.FireRate;
            _accuracy = weapon.Accuracy;
            _aimAccuracy = weapon.AimAccuracy;
            _accuracyDistance = weapon.AccuracyDistance;
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
            if (_state.CurrentState == EHeroState.Reload) return;

            if (!_ammo.CanShoot())
            {
                _reload.Reload();
                return;
            }

            var dir = RandShootDir();
            if (Physics.Raycast(_heroCamera.transform.position, dir, out var hit, _range, _layerMask))
            {
                _ammo.UseOneAmmo();

                //if (hit.transform.TryGetComponent<IHealth>(out var enemy))
                //    Debug.Log(enemy);

                MuzzleFlash();
                HitParticles(hit, TagConstants.SandTag, _sandParticlesFX);
                HitParticles(hit, TagConstants.RockTag, _rockParticlesFX);
                SpawnBulletTrail(_firePoint.position, hit.point);
                
                _weaponLight.TurnOn(TimeDestroyFX);
                _recoil.RecoilFire();
                Debug.Log(hit.collider.name);
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
            if (hit.collider.CompareTag(tag))
            {
                var fx = _poolManager.SpawnObject(particles, hit.point, Quaternion.LookRotation(hit.normal));
                await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyEnvFx));
                _poolManager.ReleaseObject(fx);
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
        
        private float RandDistance() => 
            (_state.CurrentState == EHeroState.Scoping ? _aimAccuracy : _accuracy) * Mathf.Sqrt(-2 * Mathf.Log(1 - Random.Range(0, 1f)));

        private Vector3 RandShootDir()
         {
             var distanceDispersion = RandDistance(); // dispersion distance
             var angleDispersion = Random.Range(0, 2 * Mathf.PI); // dispersion angle
 
             var coordX = distanceDispersion * Mathf.Cos(angleDispersion);
             var coordY = distanceDispersion * Mathf.Sin(angleDispersion);
             return _heroCamera.transform.forward * _accuracyDistance + new Vector3(coordX, coordY, 0);
         }
    }
}