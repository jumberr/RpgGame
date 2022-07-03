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

        public ShotgunShooting(HeroState state, Camera camera, LineFade lineFade, GameObject weaponFx, LayerMask layerMask,
            GameObject rockParticles, GameObject sandParticles, GameObject bloodParticles) : base(state, camera, lineFade,
            weaponFx, layerMask, rockParticles, sandParticles, bloodParticles)
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
                if (Physics.Raycast(HeroCamera.transform.position, dir[i], out var hit, Range, LayerMask))
                {
                    //if (hit.transform.TryGetComponent<IHealth>(out var enemy))
                    //    Debug.Log(enemy);

                    HitParticles(hit, TagConstants.SandTag, SandParticlesFX);
                    HitParticles(hit, TagConstants.RockTag, RockParticlesFX);
                    SpawnBulletTrail(FirePoint.position, hit.point);
                }
                else
                    SpawnBulletTrail(FirePoint.position, HeroCamera.transform.position + dir[i] * 0.15f);
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