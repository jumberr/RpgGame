using System;
using _Project.CodeBase.Logic.Inventory;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public interface ISlotHolderUI
    {
        void HandleClick(InventorySlotUI slotUI);
        void InitializeSlots(Action<InventorySlotUI> handleClick);
        void UpdateData();
        void UpdateSlot(InventorySlot inventorySlot, int slotIndex);
    }
}