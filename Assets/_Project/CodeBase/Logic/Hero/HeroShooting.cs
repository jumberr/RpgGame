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
        [SerializeField] private float _firePower = 10;
        [SerializeField] private float _fireSpeed;
        [SerializeField] private int _range;

        [SerializeField] private GameObject _weaponFX;
        [SerializeField] private GameObject _rockParticlesFX;
        [SerializeField] private GameObject _sandParticlesFX;
        [SerializeField] private GameObject _bloodParticlesFX;

        private bool _isShooting;
        private float _fireTimer;

        private void Start() =>
            _inputService.OnAttack += EnableDisableShoot;

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
            if (!_isShooting) return;

            if (_isAutomatic)
            {
                if (_fireTimer > 0)
                    _fireTimer -= Time.deltaTime;
                else
                {
                    _fireTimer = _fireSpeed;
                    Shoot();
                }
            }
            else
            {
                Shoot();
                ReleaseTrigger();
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

                Debug.DrawRay(_heroCamera.transform.position, _heroCamera.transform.forward * hit.distance, Color.red);
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

        private void PullTrigger()
        {
            if (_fireSpeed > 0)
                _isShooting = true;
            //else
            //    Shoot();
        }

        private void ReleaseTrigger()
        {
            _isShooting = false;
            _fireTimer = 0;
        }
    }
}