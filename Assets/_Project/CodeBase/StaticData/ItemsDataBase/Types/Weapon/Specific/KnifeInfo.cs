using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class KnifeInfo : WeaponInfo<KnifeSpecs>
    {
        [SerializeField] private KnifeSpecs _knifeSpecs;
        private GunSpecs _defaultGunSpecs = new GunSpecs();
        
        
        public override KnifeSpecs GetSpecs() => 
            _knifeSpecs;

        public GunSpecs GetDefaultGunSpecs() => 
            _defaultGunSpecs;
    }
}