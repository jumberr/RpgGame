using System;
using Sirenix.OdinInspector;

namespace _Project.CodeBase.StaticData.ItemsDataBase.Types
{
    [Serializable]
    public class Armor : Equippable
    {
        [ProgressBar(0, 100, r: 0, g: 0.5f, b: 0.5f)]
        public int Strength;
        
        [ProgressBar(0, 100, r: 0, g: 0.5f, b: 0.5f)]
        public int Agility;
        
        [ProgressBar(0, 100, r: 0, g: 0.5f, b: 0.5f)]
        public int Speed;
    }
}