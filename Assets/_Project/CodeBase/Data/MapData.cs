using System;

namespace _Project.CodeBase.Data
{
    [Serializable]
    public class MapData
    {
        public ItemsData ItemsData = new ItemsData();
        
        public void Apply(ItemsData itemsData) => 
            ItemsData = itemsData;
    }
}