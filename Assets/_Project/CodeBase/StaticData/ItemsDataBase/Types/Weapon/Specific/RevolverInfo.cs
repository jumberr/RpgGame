using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class RevolverInfo : WeaponInfo<RevolverSpecs>
    {
        [SerializeField] private RevolverSpecs _revolverSpecs;
        
        
        public override RevolverSpecs GetSpecs() => 
            _revolverSpecs;
    }
}