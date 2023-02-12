using System;
using System.Collections.Generic;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class ItemsData
    {
        public List<ItemData> Items = new List<ItemData>();

        public void AddData(ItemData itemData) => 
            Items.Add(itemData);
        
        public void RemoveData(ItemData itemData) => 
            Items.Remove(itemData);
    }
}