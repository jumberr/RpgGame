using System;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using _Project.CodeBase.Utils.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    public class BaseShooting
    {
        private const float TimeDestroyFX = 0.1f;
        private const float TimeDestroyEnvFx = 2f;

        protected readonly Camera HeroCamera;
        protected readonly LayerMask LayerMask;
        protected readonly GameObject RockParticlesFX;
        protected readonly GameObject SandParticlesFX;
        protected readonly GameObject BloodParticlesFX;
        protected Transform FirePoint;
        protected float Range;
        protected float Damage;

        private IPoolManager _poolManager;
        private readonly HeroState _state;
        private readonly LineFade _lineFade;
        private readonly GameObject _weaponFX;
        private float _accuracyDistance;
        private float _aimAccuracy;
        private float _accuracy;

        public BaseShooting(HeroState state, Camera camera, LineFade lineFade, GameObject weaponFx,
            LayerMask layerMask, GameObject rockParticles, GameObject sandParticles, GameObject bloodParticles)
        {
            _state = state;
            _lineFade = lineFade;
            _weaponFX = weaponFx;
            HeroCamera = camera;
            LayerMask = layerMask;
            RockParticlesFX = rockParticles;
            SandParticlesFX = sandParticles;
            BloodParticlesFX = bloodParticles;
        }

        public void SetupPoolManager(IPoolManager poolManager) => 
            _poolManager = poolManager;
        
        public void SetupFirePoint(Transform firePoint) => 
            FirePoint = firePoint;

        public void SetupConfig(float range, float accuracyDistance, float aimAccuracy, float accuracy, float damage)
        {
            Range = range;
            Damage = damage;
            _accuracyDistance = accuracyDistance;
            _aimAccuracy = aimAccuracy;
            _accuracy = accuracy;
        }

        protected async void HitParticles(RaycastHit hit, string tag, GameObject particles)
        {
            if (!hit.collider.CompareTag(tag)) return;
            var fx = _poolManager.SpawnObject(particles, hit.point, Quaternion.LookRotation(hit.normal));
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyEnvFx));
            _poolManager.ReleaseObject(fx);
        }

        protected async void SpawnBulletTrail(Vector3 start, Vector3 end)
        {
            var lineFade = _poolManager.SpawnObject(_lineFade.gameObject, start, Quaternion.identity);
            lineFade.GetComponent<LineFade>().SetPositions(start, end);
            
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyFX));
            _poolManager.ReleaseObject(lineFade);
        }

        protected async void MuzzleFlash()
        {
            var fx = _poolManager.SpawnObject(_weaponFX, FirePoint.position, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyFX));
            _poolManager.ReleaseObject(fx);
        }

        protected Vector3 RandShootDir()
        {
            var distanceDispersion = RandDistance(); // dispersion distance
            var angleDispersion = Random.Range(0, 2 * Mathf.PI); // dispersion angle
 
            var coordX = distanceDispersion * Mathf.Cos(angleDispersion);
            var coordY = distanceDispersion * Mathf.Sin(angleDispersion);
            return HeroCamera.transform.forward * _accuracyDistance + new Vector3(coordX, coordY, 0);
        }

        private float RandDistance() => 
            (_state.CurrentPlayerState == PlayerState.Scoping ? _aimAccuracy : _accuracy) * Mathf.Sqrt(-2 * Mathf.Log(1 - Random.Range(0, 1f)));
    }
}