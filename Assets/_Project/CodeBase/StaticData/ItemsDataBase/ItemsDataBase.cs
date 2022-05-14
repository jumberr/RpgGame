using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroInventory;
using UnityEngine;

namespace _Project.CodeBase.StaticData.ItemsDataBase
{
    public class ItemsDataBase : ScriptableObject
    {
        public List<ItemData> ItemsDatabase;

        public ItemData FindItem(int index) => 
            ItemsDatabase.Find(x => x.ItemPayloadData.DbId == index);
        
        public ItemData FindItem(ItemName itemName) => 
            ItemsDatabase.Find(x => x.ItemUIData.Name == itemName);
        
        public int FindIndex(ItemName itemName) => 
            ItemsDatabase.FindIndex(x => x == FindItem(itemName));
    }
}