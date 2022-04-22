using System.Collections.Generic;
using UnityEngine;

namespace _Project.CodeBase.StaticData.ItemsDataBase
{
    public class ItemsDataBase : ScriptableObject
    {
        public List<ItemData> ItemsDatabase;

        public ItemData FindItemByIndex(int index) => 
            ItemsDatabase.Find(x => x.ItemPayloadData.DbId == index);
    }
}