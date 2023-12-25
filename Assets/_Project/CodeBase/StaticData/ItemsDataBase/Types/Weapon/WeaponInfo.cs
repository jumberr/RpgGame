using System;
using _Project.CodeBase.Logic.Hero.Shooting;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class WeaponInfo : EquippableInfo
    {
        [SerializeField] private WeaponSpecs _weaponSpecs;
        public WeaponSpecs WeaponSpecs => _weaponSpecs;
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
    }
}