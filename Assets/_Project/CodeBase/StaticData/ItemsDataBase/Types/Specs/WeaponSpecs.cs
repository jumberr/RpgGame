using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class WeaponSpecs
    {
        [ProgressBar(0, 100, r: 1, g: 0f, b: 0f)]
        [SerializeField] private float _damage;
        
        [ProgressBar(0, 100, r: 0, g: 0.2f, b: 0.75f)]
        [SerializeField] private float _range;
        
        [ProgressBar(0, 1000, r: 0, g: 0.2f, b: 0.75f)]
        [SerializeField] private float _fireRate;
        
        public float Damage => _damage;
        public float Range => _range;
        public float FireRate => _fireRate;
    }
}