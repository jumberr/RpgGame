using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon.Effects;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero.Shooting
{
    public class ShotgunShooting : DefaultShooting
    {
        public ShotgunShooting(HeroState state, LineFade lineFade, LayerMask layerMask, ShootingParticles particles)
            : base(state, lineFade, layerMask, particles)
        {
        }
        
        public void SetupConfig(int fractionCount) => 
            FractionCount = fractionCount;
    }
}