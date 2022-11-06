using System;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using Cysharp.Threading.Tasks;
using NTC.Global.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    public class BaseShooting
    {
        private const float TimeDestroyFX = 0.1f;
        private const float TimeDestroyEnvFx = 2f;

        protected readonly LayerMask LayerMask;
        protected readonly ShootingParticles Particles;
        protected Transform FirePoint;
        protected float Range;
        protected float Damage;

        private readonly HeroState _state;
        private readonly LineFade _lineFade;
        private float _accuracyDistance;
        private float _aimAccuracy;
        private float _accuracy;
        
        protected BaseShooting(HeroState state, LineFade lineFade, LayerMask layerMask, ShootingParticles particles)
        {
            _state = state;
            _lineFade = lineFade;
            LayerMask = layerMask;
            Particles = particles;
        }

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
            
            var fx = NightPool.Spawn(particles, hit.point, Quaternion.LookRotation(hit.normal));
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyEnvFx));
            NightPool.Despawn(fx);
        }

        protected async void SpawnBulletTrail(Vector3 start, Vector3 end)
        {
            var lineFade = NightPool.Spawn(_lineFade, start, Quaternion.identity);
            lineFade.SetPositions(start, end);

            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyFX));
            NightPool.Despawn(lineFade);
        }

        protected async void MuzzleFlash()
        {
            var fx = NightPool.Spawn(Particles.WeaponFX, FirePoint.position, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(TimeDestroyFX));
            NightPool.Despawn(fx);
        }

        protected Vector3 RandShootDir()
        {
            var distanceDispersion = RandDistance(); // dispersion distance
            var angleDispersion = Random.Range(0, 2 * Mathf.PI); // dispersion angle
 
            var coordX = distanceDispersion * Mathf.Cos(angleDispersion);
            var coordY = distanceDispersion * Mathf.Sin(angleDispersion);
            return FirePoint.forward * _accuracyDistance + new Vector3(coordX, coordY, 0);
        }

        private float RandDistance() => 
            (_state.Aiming ? _aimAccuracy : _accuracy) * Mathf.Sqrt(-2 * Mathf.Log(1 - Random.Range(0, 1f)));
    }
}