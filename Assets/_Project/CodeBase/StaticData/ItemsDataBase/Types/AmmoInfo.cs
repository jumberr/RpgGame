using System;
using _Project.CodeBase.Logic.HeroWeapon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class AmmoInfo : ItemInfo
    {
        [EnumToggleButtons, HideLabel]
        [SerializeField] private AmmoType _ammoType;

        public AmmoType AmmoType => _ammoType;
    }
}