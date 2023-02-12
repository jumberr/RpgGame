using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;

namespace _Project.CodeBase.UI.Elements.Slot.Drop
{
    public interface IDropArea
    {
        public List<DropCondition> DropConditions { get; set; }
        public InventorySlot SlotData { get; }
        bool Accepts(ICanBeDragged draggable);
        void Drop(InventorySlot data);
        void Initialize();
        void Release();
    }
}