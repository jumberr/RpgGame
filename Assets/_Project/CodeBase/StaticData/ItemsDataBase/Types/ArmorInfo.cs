using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class ArmorInfo : EquippableInfo
    {
        [ProgressBar(0, 100, r: 0, g: 0.5f, b: 0.5f)]
        [SerializeField] private int _strength;
        
        [ProgressBar(0, 100, r: 0, g: 0.5f, b: 0.5f)]
        [SerializeField] private int _agility;
        
        [ProgressBar(0, 100, r: 0, g: 0.5f, b: 0.5f)]
        [SerializeField] private int _speed;
        
        public int Strength => _strength;
        public int Agility => _agility;
        public int Speed => _speed;
    }
}