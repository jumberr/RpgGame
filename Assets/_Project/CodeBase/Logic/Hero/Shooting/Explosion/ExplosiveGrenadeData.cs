using System;
using _Project.CodeBase.StaticData.Sound;
using _Project.CodeBase.Utils;

namespace _Project.CodeBase.Logic.Hero
{
    [Serializable]
    public class ExplosiveGrenadeData
    {
        public VSFXComponent ExplosionPrefab;
        public AudioData SoundData; 
        public float ExplosionDelay;
        public float ExplosionForce;
        public float ExplosionRadius;
        public float MaxDamage;
    }
}