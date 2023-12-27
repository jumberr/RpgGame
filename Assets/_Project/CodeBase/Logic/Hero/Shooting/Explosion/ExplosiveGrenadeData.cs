using System;
using _Project.CodeBase.StaticData.Sound;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    [Serializable]
    public class ExplosiveGrenadeData
    {
        public GameObject ExplosionPrefab;
        public AudioData SoundData; 
        public float ExplosionDelay;
        public float ExplosionForce;
        public float ExplosionRadius;
    }
}