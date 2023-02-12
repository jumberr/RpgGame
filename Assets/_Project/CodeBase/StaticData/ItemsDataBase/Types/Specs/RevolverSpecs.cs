using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class RevolverSpecs
    {
        [SerializeField] private float _startReloadDuration;
        [SerializeField] private float _oneAmmoReloadDuration;
        [SerializeField] private float _endReloadAnimation;
        
        public float StartReloadDuration => _startReloadDuration;
        public float OneAmmoReloadDuration => _oneAmmoReloadDuration;
        public float EndReloadAnimation => _endReloadAnimation;
    }
}