using _Project.CodeBase.Data;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Static Data/Player Stats", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        public HealthData HealthData;
    }
}