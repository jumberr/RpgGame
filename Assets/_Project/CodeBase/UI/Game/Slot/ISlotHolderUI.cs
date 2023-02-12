namespace _Project.CodeBase.UI
{
    public interface ISlotHolderUI
    {
        void HandleClick(InventorySlotUI slotUI);
        void InitializeSlots(SlotTouchEvents slotTouchEvents);
        void UpdateData();
        void UpdateSlot(int slotIndex);
    }
}