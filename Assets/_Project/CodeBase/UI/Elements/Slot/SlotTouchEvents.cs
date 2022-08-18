using System;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Windows.Inventory;

namespace _Project.CodeBase.UI.Elements.Slot
{
    public class SlotTouchEvents
    {
        public Action<InventorySlotUI> Click;
        public Action<InventorySlot, InventorySlot> Drop;

        public SlotTouchEvents(Action<InventorySlotUI> click, Action<InventorySlot, InventorySlot> drop)
        {
            Click = click;
            Drop = drop;
        }
    }
}