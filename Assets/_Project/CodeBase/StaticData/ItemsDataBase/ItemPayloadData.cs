using System;
using _Project.CodeBase.Logic.HeroInventory;

namespace _Project.CodeBase.StaticData.ItemsDataBase
{
    [Serializable]
    public class ItemPayloadData
    {
        public EItemType ItemType;
        public int MaxInContainer;
    }
}