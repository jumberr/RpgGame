using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    [Serializable]
    public class ShootingParticles
    {
        public GameObject WeaponFX;
        public GameObject RockParticlesFX;
        public GameObject SandParticlesFX;
        public GameObject BloodParticlesFX;

        public ShootingParticles(GameObject weaponFX, GameObject rockParticlesFX, GameObject sandParticlesFX, GameObject bloodParticlesFX)
        {
            WeaponFX = weaponFX;
            BloodParticlesFX = bloodParticlesFX;
            SandParticlesFX = sandParticlesFX;
            RockParticlesFX = rockParticlesFX;
        }
    }
}