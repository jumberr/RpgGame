using System;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.StaticData.Sound;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GrenadeSpecs : WeaponSpecs
    {
        [field: SerializeField] public GrenadeView GrenadeProjectile { get; private set; }
        [field: SerializeField] public ThrowData ThrowData { get; private set; }
        [field: SerializeField] public AudioData ImpactSoundData { get; private set; }
        [field: SerializeField] public AudioData PullPinSoundData { get; private set; }
    }
}