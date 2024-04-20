using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GrenadeInfo : WeaponInfo<GrenadeSpecs>
    {
        [SerializeField] private GrenadeSpecs _grenadeSpecs;

        
        public override GrenadeSpecs GetSpecs() => 
            _grenadeSpecs;
    }
}