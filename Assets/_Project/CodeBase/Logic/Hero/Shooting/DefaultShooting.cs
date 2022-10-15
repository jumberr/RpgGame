using _Project.CodeBase.Constants;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    public class DefaultShooting : BaseShooting, IShootingSystem
    {
        public DefaultShooting(HeroState state, LineFade lineFade, LayerMask layerMask,
            ShootingParticles particles) : base(state, lineFade, layerMask, particles)
        {
        }

        public void Shoot()
        {
            var dir = RandShootDir();
            MuzzleFlash();

            if (Physics.Raycast(FirePoint.position, dir, out var hit, Range, LayerMask))
            {
                //if (hit.transform.TryGetComponent<IHealth>(out var enemy))
                //    Debug.Log(enemy);

                HitParticles(hit, TagConstants.SandTag, Particles.SandParticlesFX);
                HitParticles(hit, TagConstants.RockTag, Particles.RockParticlesFX);
                SpawnBulletTrail(FirePoint.position, hit.point);
            }
            else
                SpawnBulletTrail(FirePoint.position, FirePoint.position + dir * 0.15f);
        }
    }
}