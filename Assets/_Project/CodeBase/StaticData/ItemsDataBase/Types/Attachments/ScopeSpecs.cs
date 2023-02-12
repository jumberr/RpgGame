using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ScopeSpecs
    {
        [SerializeField] private float _aimingInTime;
        [SerializeField] private float _aimingOutTime;
        [SerializeField] private float _scopeMultiplier;
        
        public float AimingInTime => _aimingInTime;
        public float AimingOutTime => _aimingOutTime;
        public float ScopeMultiplier => _scopeMultiplier;
    }
}