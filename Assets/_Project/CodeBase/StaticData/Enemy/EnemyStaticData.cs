using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Enemy;
using UnityEngine;

namespace _Project.CodeBase.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Static Data/Enemy Static Data", order = 0)]
    public class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField] public EnemyFacade EnemyPrefab { get; set; }
        [field: SerializeField] public HealthData HealthData { get; set; }
        [field: SerializeField] public List<EnemyHitboxInfo> HitboxData { get; set; }

        public EnemyHitboxInfo FindHitboxData(HitboxName name) =>
            HitboxData.FirstOrDefault(x => x.Name == name);
    }
}