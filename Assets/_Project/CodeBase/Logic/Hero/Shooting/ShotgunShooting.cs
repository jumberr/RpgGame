using System.Collections.Generic;
using _Project.CodeBase.Constants;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    public class ShotgunShooting : BaseShooting, IShootingSystem
    {
        protected int FractionCount;

        public ShotgunShooting(HeroState state, LineFade lineFade, LayerMask layerMask, ShootingParticles particles)
            : base(state, lineFade, layerMask, particles)
        {
        }
        
        public void SetupConfig(int fractionCount) => 
            FractionCount = fractionCount;

        public void Shoot()
        {
            var dir = RandShotgunDir();
            MuzzleFlash();

            for (var i = 0; i < FractionCount; i++)
            {
                if (Physics.Raycast(FirePoint.position, dir[i], out var hit, Range, LayerMask))
                {
                    //if (hit.transform.TryGetComponent<IHealth>(out var enemy))
                    //    Debug.Log(enemy);

                    HitParticles(hit, TagConstants.SandTag, Particles.SandParticlesFX);
                    HitParticles(hit, TagConstants.RockTag, Particles.RockParticlesFX);
                    SpawnBulletTrail(FirePoint.position, hit.point);
                }
                else
                    SpawnBulletTrail(FirePoint.position, FirePoint.position + dir[i] * 0.15f);
            }
        }

        private List<Vector3> RandShotgunDir()
        {
            var randShotgunDir = new List<Vector3>();

            for (var i = 0; i < FractionCount; i++) 
                randShotgunDir.Add(RandShootDir());
            return randShotgunDir;
        }
    }
}