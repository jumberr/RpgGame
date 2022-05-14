using System;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    [Serializable]
    public class Weapon : Equippable
    {
        public WeaponData WeaponData;
        public Magazine Magazine;
        public GameObject WeaponPrefab;
    }
}