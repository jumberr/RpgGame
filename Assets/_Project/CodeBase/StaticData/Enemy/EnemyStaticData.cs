using _Project.CodeBase.Logic.Enemy;
using UnityEngine;

namespace _Project.CodeBase.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "Static Data/Enemy Static Data", order = 0)]
    public class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField] public EnemyFacade EnemyPrefab { get; set; }
    }
}