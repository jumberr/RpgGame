using System;
using _Project.CodeBase.Constants;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.Weapon;
using _Project.CodeBase.Logic.Weapon.Effects;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [RequireComponent(typeof(InputService), typeof(HeroAmmo))]
    public class HeroShooting : MonoBehaviour
    {
        private const float TimeDestroyFX = 0.1f;
        private const float TimeDestroyEnvFx = 2f;

        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private HeroState _state;
        [SerializeField] private InputService _inputService;
        [SerializeField] private HeroAmmo _ammo;
        [SerializeField] private HeroReload _reload;
        [SerializeField] private HeroRecoil _recoil;
        [SerializeField] private BulletPool _bulletPool;
        
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
            _ammo.Construct(_weaponData);
            _reload.Construct(_weaponData);
            _recoil.Construct(_weaponData);
            
            var weapon = _weaponData.Weapon;
            _isAutomatic = weapon.IsAutomatic;
            _damage = weapon.Damage;
            _range = weapon.Range;
            _fireSpeed = 60 / weapon.FireRate;
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
            
            if (Physics.Raycast(_heroCamera.transform.position, _heroCamera.transform.forward, out var hit, _range, _layerMask))
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
    }
}