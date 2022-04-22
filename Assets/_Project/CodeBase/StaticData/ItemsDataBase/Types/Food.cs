using System;
using Sirenix.OdinInspector;

namespace _Project.CodeBase.StaticData.ItemsDataBase.Types
{
    [Serializable]
    public class Food : ItemData
    {
        [ProgressBar(0, 100, r: 0, g: 0.2f, b: 0.75f)]
        public int HealthRestore;
        
        [ProgressBar(0, 100, r: 0, g: 0.2f, b: 0.75f)]
        public int Seconds;
    }
}