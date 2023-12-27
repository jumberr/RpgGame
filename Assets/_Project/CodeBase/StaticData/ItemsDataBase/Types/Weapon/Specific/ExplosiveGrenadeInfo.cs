using System;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ExplosiveGrenadeInfo : GrenadeInfo
    {
        [field: SerializeField] public ExplosionGrenadeData ExplosionGrenadeData { get; private set; }
    }
}