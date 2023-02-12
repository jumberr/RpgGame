using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class WeaponInfo : EquippableInfo
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private WeaponSpecs _weaponSpecs;
        
        public GameObject Prefab => _prefab;
        public WeaponSpecs WeaponSpecs => _weaponSpecs;
    }
}