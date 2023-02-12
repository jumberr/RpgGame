using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    public abstract class EquippableInfo : ItemInfo
    {
        [SerializeField] private float _durability;
        
        public float Durability => _durability;
    }
}