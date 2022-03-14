using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Weapon;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [RequireComponent(typeof(InputService))]
    public class HeroShooting : MonoBehaviour
    {
        private const string SandTag = "Sand";
        private const string RockTag = "Rock";
        private const float TimeDestroyFX = 0.1f;
        private const float TimeDestroyEnvFx = 2f;
        
        [SerializeField] private InputService _inputService;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private Camera _heroCamera;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private Transform _firePoint;

        //public bool IsAutomatic;
//
        //public float fireRatio = 100;
        //public float damage;
        //public float range;
//
        //private float nextFire;
        //private bool _isShoot;

        [SerializeField] private bool _isAutomatic;
        [SerializeField] private float _firePower = 10; // dmg
        [Tooltip("Rate of fire [0, 1000]")] [SerializeField] private float _fireRate;
        [SerializeField] private int _range;

        [SerializeField] private GameObject _weaponFX;
        [SerializeField] private GameObject _rockParticlesFX;
        [SerializeField] private GameObject _sandParticlesFX;
        [SerializeField] private GameObject _bloodParticlesFX;

        private bool _isShooting;
        private float _fireSpeed;
        private float _automaticFireTimer;
        private float _singleFireTimer;

        private void Start()
        {
            ApplyGunSettings();
            _inputService.OnAttack += EnableDisableShoot;
        }

        public void ApplyGunSettings()
        {
            _fireSpeed = 60 / _fireRate;
        }

        private void EnableDisableShoot(bool value)
        {
            if (value)
                PullTrigger();
            else
                ReleaseTrigger();
        }

        private void OnDisable() => 
            _inputService.OnAttack -= EnableDisableShoot;

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

        private void Shoot()
        {
            if (Physics.Raycast(_heroCamera.transform.position, _heroCamera.transform.forward, out var hit, _range, _layerMask))
            {
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