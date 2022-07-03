using _Project.CodeBase.Constants;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    public class DefaultShooting : BaseShooting, IShootingSystem
    {
        public DefaultShooting(HeroState state, Camera camera, LineFade lineFade, GameObject weaponFx, LayerMask layerMask,
            GameObject rockParticles, GameObject sandParticles, GameObject bloodParticles) : base(state, camera, lineFade,
            weaponFx, layerMask, rockParticles, sandParticles, bloodParticles)
        {
        }

        public void Shoot()
        {
            var dir = RandShootDir();
            MuzzleFlash();

            if (Physics.Raycast(HeroCamera.transform.position, dir, out var hit, Range, LayerMask))
            {
                //if (hit.transform.TryGetComponent<IHealth>(out var enemy))
                //    Debug.Log(enemy);

                HitParticles(hit, TagConstants.SandTag, SandParticlesFX);
                HitParticles(hit, TagConstants.RockTag, RockParticlesFX);
                SpawnBulletTrail(FirePoint.position, hit.point);
            }
            else
                SpawnBulletTrail(FirePoint.position, HeroCamera.transform.position + dir * 0.15f);
        }
    }
}