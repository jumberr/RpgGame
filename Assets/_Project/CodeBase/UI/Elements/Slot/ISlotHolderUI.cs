using System;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Windows.Inventory;

namespace _Project.CodeBase.UI.Elements.Slot
{
    public interface ISlotHolderUI
    {
        void HandleClick(InventorySlotUI slotUI);
        void InitializeSlots(Action<InventorySlotUI> handleClick);
        void UpdateData();
        void UpdateSlot(InventorySlot inventorySlot, int slotIndex);
    }
}