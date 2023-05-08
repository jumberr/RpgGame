using System.Collections.Generic;
using _Project.CodeBase.Constants;
using _Project.CodeBase.Logic.Enemy;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    public class DefaultShooting : BaseShooting, IShootingSystem
    {
        protected int FractionCount = 1;

        public DefaultShooting(HeroState state, LineFade lineFade, LayerMask layerMask,
            ShootingParticles particles) : base(state, lineFade, layerMask, particles)
        {
        }

        public void Shoot()
        {
            var dir = GetRandomDirections();
            MuzzleFlash();

            for (var i = 0; i < FractionCount; i++)
            {
                var firePointPos = FirePoint.position;
                if (Physics.Raycast(firePointPos, dir[i], out var hit, Range, LayerMask))
                {
                    if (hit.transform.TryGetComponent<IHitBox>(out var hitbox)) 
                        hitbox.Hit(Damage);

                    HitParticles(hit, TagConstants.SandTag, Particles.SandParticlesFX);
                    HitParticles(hit, TagConstants.RockTag, Particles.RockParticlesFX);
                    SpawnBulletTrail(firePointPos, hit.point);
                }
                else
                    SpawnBulletTrail(firePointPos, firePointPos + dir[i] * 0.15f);
            }
        }

        private List<Vector3> GetRandomDirections()
        {
            var dir = new List<Vector3>();

            for (var i = 0; i < FractionCount; i++) 
                dir.Add(RandShootDir());
            return dir;
        }
    }
}