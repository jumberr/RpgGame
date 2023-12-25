using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class WeaponInfo : EquippableInfo
    {
        [SerializeField] private WeaponSpecs _weaponSpecs;
        public WeaponSpecs WeaponSpecs => _weaponSpecs;
    }
}