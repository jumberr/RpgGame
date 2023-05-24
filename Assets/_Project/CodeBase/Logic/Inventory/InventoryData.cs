using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Inventory
{
    [Serializable]
    public struct InventoryData
    {
        [field: SerializeField] public int HotBarSize { get; set; }
        [field: SerializeField] public int InventorySize { get; set; }
    }
}