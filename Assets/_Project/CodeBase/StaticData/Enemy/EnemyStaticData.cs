using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Enemy;
using _Project.CodeBase.Logic.Enemy.FSM;
using UnityEngine;

namespace _Project.CodeBase.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Static Data/Enemy Static Data", order = 0)]
    public class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField] public EnemyFacade EnemyPrefab { get; set; }
        [field: SerializeField] public HealthData HealthData { get; set; }
        [field: SerializeField] public List<EnemyHitboxInfo> HitboxData { get; set; }
        [field: SerializeField] public AIStateName InitialState { get; private set; }
        [field: SerializeField] public List<AIStateName> States { get; private set; }
        [field: SerializeField] public float SightDistance { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float DestinationCooldown { get; private set; }
        [field: SerializeField] public float StoppingDistance { get; private set; }
        [field: SerializeField] public float RotationModifier { get; private set; }

        public EnemyHitboxInfo FindHitboxData(HitboxName name) =>
            HitboxData.FirstOrDefault(x => x.Name == name);
    }
}