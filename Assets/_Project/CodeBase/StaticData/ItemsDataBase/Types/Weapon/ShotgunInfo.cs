using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ShotgunInfo : GunInfo
    {
        [SerializeField] private ShotgunSpecs _shotgunSpecs;
        
        public ShotgunSpecs ShotgunSpecs => _shotgunSpecs;
    }
}