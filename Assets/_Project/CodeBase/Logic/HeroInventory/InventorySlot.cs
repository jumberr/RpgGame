using System;

namespace _Project.CodeBase.Logic.HeroInventory
{
    [Serializable]
    public class InventorySlot
    {
        public int DbId = -1;
        public int Amount;
        public SlotState State;
    }
}