using System;
using _Project.CodeBase.Data;

namespace _Project.CodeBase.Logic.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        public CommonItemPart CommonItemPart;
        public SlotState State;

        public int ID => CommonItemPart.DbId;
        
        public InventorySlot()
        {
            CommonItemPart = new CommonItemPart();
            State = SlotState.Empty;
        }
    }
}