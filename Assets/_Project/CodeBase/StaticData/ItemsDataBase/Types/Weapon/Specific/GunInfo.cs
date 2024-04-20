using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GunInfo : WeaponInfo<GunSpecs>
    {
        [SerializeField] private GunSpecs _gunSpecs;
        

        public override GunSpecs GetSpecs() => 
            _gunSpecs;
    }
}