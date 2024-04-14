using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ShotgunInfo : WeaponInfo<ShotgunSpecs>
    {
        [SerializeField] private ShotgunSpecs _shotgunSpecs;
        
        
        public override ShotgunSpecs GetSpecs() => 
            _shotgunSpecs;
    }
}