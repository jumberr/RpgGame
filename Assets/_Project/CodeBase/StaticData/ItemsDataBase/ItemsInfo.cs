using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    public class ItemsInfo : ScriptableObject
    {
        [SerializeField] private List<ItemInfo> _itemsDatabase;
        public List<ItemInfo> ItemsDatabase => _itemsDatabase;

        public ItemInfo FindItem(int index) => 
            _itemsDatabase.Find(x => x.PayloadInfo.DbId == index);
        
        public ItemInfo FindItem(ItemName itemName) => 
            _itemsDatabase.Find(x => x.UIInfo.Name == itemName);
        
        public int FindIndex(ItemName itemName) => 
            _itemsDatabase.FindIndex(x => x == FindItem(itemName));
    }
}