using System;

namespace _Project.CodeBase.Logic.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        public int DbId;
        public int Amount;
        public SlotState State;

        public InventorySlot()
        {
            DbId = -1;
            State = SlotState.Empty;
        }
    }
}