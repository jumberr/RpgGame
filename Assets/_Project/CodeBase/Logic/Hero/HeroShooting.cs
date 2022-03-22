using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.Weapon;
using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [RequireComponent(typeof(InputService), typeof(HeroAmmo))]
    public class HeroShooting : MonoBehaviour
    {
        private const string SandTag = "Sand";
        private const string RockTag = "Rock";
        private const float TimeDestroyFX = 0.1f;
        private const float TimeDestroyEnvFx = 2f;

        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private HeroState _state;
        [SerializeField] private InputService _inputService;
        [SerializeField] private HeroAmmo _ammo;
        [SerializeField] private HeroReload _heroReload;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private Camera _heroCamera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform _firePoint;

        [SerializeField] private GameObject _weaponFX;
        [SerializeField] private GameObject _rockParticlesFX;
        [SerializeField] private GameObject _sandParticlesFX;
        [SerializeField] private GameObject _bloodParticlesFX;

        private bool _isAutomatic;
        private float _damage;
        private float _range;
        
        private bool _isShooting;
        private float _fireSpeed;
        private float _automaticFireTimer;
        private float _singleFireTimer;

        private void Start()
        {
            ApplyGunSettings();
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

        private void ApplyGunSettings()
        {
            _ammo.Construct(_weaponData);
            _heroReload.Construct(_weaponData);
            
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
                _heroReload.Reload();
                return;
            }
            
            if (Physics.Raycast(_heroCamera.transform.position, _heroCamera.transform.forward, out var hit, _range, _layerMask))
            {
                _ammo.UseOneAmmo();
                
                var fx = Instantiate(_weaponFX, _firePoint);
                Destroy(fx, TimeDestroyFX);
                
                //if (hit.transform.TryGetComponent<IHealth>(out var enemy))
                //{
                //    Debug.Log(enemy);
                //}
                
                HitParticles(hit, SandTag, _sandParticlesFX);
                HitParticles(hit, RockTag, _rockParticlesFX);
                Debug.Log(hit.collider.name);
            }
        }

        private void HitParticles(RaycastHit hit, string tag, GameObject particles)
        {
            if (hit.collider.CompareTag(tag))
            {
                var fx = Instantiate(particles, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(fx, TimeDestroyEnvFx);
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