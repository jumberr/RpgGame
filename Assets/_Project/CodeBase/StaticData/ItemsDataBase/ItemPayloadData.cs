using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroInventory;

namespace _Project.CodeBase.StaticData.ItemsDataBase
{
    [Serializable]
    public class ItemPayloadData
    {
        public EItemType ItemType;
        public int MaxInContainer;
        public List<EActionType> Actions = new List<EActionType>
        {
            EActionType.Equip,
            EActionType.Use,
            EActionType.Drop,
            EActionType.DropAll
        };
    }
}