using System;
using _Project.CodeBase.Logic.Enemy;
using UnityEngine;

namespace _Project.CodeBase.StaticData.Enemy
{
    [Serializable]
    public class EnemyHitboxInfo
    {
        [field: SerializeField] public HitboxName Name { get; private set; }
        [field: SerializeField] public float Multiplier { get; private set; }
    }
}