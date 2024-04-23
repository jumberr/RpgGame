using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    public abstract class EquippableInfo : ItemInfo
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _durability;
        
        public GameObject Prefab => _prefab;
        public float Durability => _durability;
    }
}