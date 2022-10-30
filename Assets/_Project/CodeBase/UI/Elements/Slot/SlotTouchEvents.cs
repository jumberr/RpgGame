using System;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Windows.Inventory;

namespace _Project.CodeBase.UI.Elements.Slot
{
    public class SlotTouchEvents
    {
        public readonly Action<InventorySlotUI> Click;
        public readonly Action<InventorySlot, InventorySlot> Drop;
        public Action OnBeginDrag;

        public SlotTouchEvents(Action<InventorySlotUI> click, Action<InventorySlot, InventorySlot> drop)
        {
            Click = click;
            Drop = drop;
            
        }

        public void SetBeginDragEvent(Action onBeginDrag) => 
            OnBeginDrag = onBeginDrag;
    }
}