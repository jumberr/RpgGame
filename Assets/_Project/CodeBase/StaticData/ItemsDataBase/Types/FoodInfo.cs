using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class FoodInfo : ItemInfo
    {
        [ProgressBar(0, 100, r: 0, g: 0.2f, b: 0.75f)]
        [SerializeField] private int _healthRestore;
        
        [ProgressBar(0, 100, r: 0, g: 0.2f, b: 0.75f)]
        [SerializeField] private int _seconds;
        
        public int HealthRestore => _healthRestore;
        public int Seconds => _seconds;
    }
}