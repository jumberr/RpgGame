using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.StaticData.ItemsDataBase
{
    [CreateAssetMenu(fileName = "ItemsDataBase", menuName = "Static Data/ItemsDataBase", order = 0)]
    public class ItemsDataBase : ScriptableObject
    {
        public List<ItemData> ItemsDatabase;

        public ItemData FindItemByIndex(int index)
        {
            if (ItemsDatabase.Count <= index || index < 0)
                throw new ArgumentException($"No such index: {index}");

            return ItemsDatabase[index];
        }
    }
}