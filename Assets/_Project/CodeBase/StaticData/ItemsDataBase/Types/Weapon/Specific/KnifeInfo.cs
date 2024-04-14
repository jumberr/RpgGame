using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class KnifeInfo : WeaponInfo<WeaponSpecs>
    {
        [SerializeField] private WeaponSpecs _weaponSpecs;
        private GunSpecs _defaultGunSpecs = new GunSpecs();
        
        
        public override WeaponSpecs GetSpecs() => 
            _weaponSpecs;

        public GunSpecs GetDefaultGunSpecs() => 
            _defaultGunSpecs;
    }
}