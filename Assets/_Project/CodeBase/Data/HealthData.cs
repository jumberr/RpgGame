using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public struct HealthData
    {
        [field: SerializeField] public float CurrentHp {get; set;}
        [field: SerializeField] public float MaxHp {get; set;}
        [field: SerializeField] public bool Regeneration { get; set; }
        [ShowIf(nameof(Regeneration))] [field: SerializeField] public float RegenerationDelay { get; set; }
        [ShowIf(nameof(Regeneration))] [field: SerializeField] public float RegenerationSpeed { get; set; }
    }
}