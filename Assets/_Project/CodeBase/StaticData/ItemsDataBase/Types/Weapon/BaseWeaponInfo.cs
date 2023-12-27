using System;
using _Project.CodeBase.Logic.Hero.Shooting;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class BaseWeaponInfo : EquippableInfo
    {
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
    }
}