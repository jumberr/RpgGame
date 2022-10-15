using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using _Project.CodeBase.StaticData.ItemsDataBase.Types.Attachments;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    [Serializable]
    public class Weapon : Equippable
    {
        public WeaponData WeaponData;
        public Magazine Magazine;
        public List<ItemName> PossibleAttachments;
        public List<ItemName> DefaultAttachments;
        public ScopeSettings DefaultScopeData;
        public GameObject WeaponPrefab;
    }
}