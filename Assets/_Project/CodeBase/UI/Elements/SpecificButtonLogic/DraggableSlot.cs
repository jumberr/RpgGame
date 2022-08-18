using System;
using _Project.CodeBase.UI.Windows.Inventory;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class DraggableSlot : DraggableWithHold
    {
        private InventorySlotUI _slot;
        private Action<InventorySlotUI> _click;

        public void Construct(InventorySlotUI slot, Action<InventorySlotUI> click)
        {
            _slot = slot;
            _click = click;
        }

        protected override void Click() => 
            _click?.Invoke(_slot);
    }
}