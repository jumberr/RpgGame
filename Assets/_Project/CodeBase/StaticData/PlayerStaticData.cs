using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "Static Data/Player Stats", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObjectData DefaultData { get; private set; }
        [field: SerializeField] public HealthData HealthData { get; private set; }
        [field: SerializeField] public InventoryData InventoryData { get; private set; }
        [field: SerializeField] public MovementData MovementData { get; private set; }
    }
}