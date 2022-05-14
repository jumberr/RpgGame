using System;
using _Project.CodeBase.Logic.HeroWeapon;
using Sirenix.OdinInspector;

namespace _Project.CodeBase.StaticData.ItemsDataBase.Types
{
    [Serializable]
    public class Ammo : ItemData
    {
        [EnumToggleButtons, HideLabel]
        public AmmoType AmmoType;
    }
}