using System;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GrenadeInfo : BaseWeaponInfo
    {
        [field: SerializeField] public ThrowData ThrowData { get; private set; }
    }
}