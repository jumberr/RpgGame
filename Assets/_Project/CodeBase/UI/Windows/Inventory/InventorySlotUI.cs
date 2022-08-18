using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Slot;
using _Project.CodeBase.UI.Elements.Slot.Drop;
using _Project.CodeBase.UI.Elements.SpecificButtonLogic;
using UnityEngine;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private DropArea _dropArea;
        [SerializeField] private ItemDisplay _prefab;

        [HideInInspector] public ItemDisplay Display;
        [HideInInspector] public int SlotID;

        private InventorySlot _slotData;
        private Canvas _canvas;
        private SlotTouchEvents _events;

        public InventorySlot SlotData => _slotData;
        
        public void Construct(int id, Canvas canvas, InventorySlot inventorySlot, SlotTouchEvents events)
        {
            SlotID = id;
            _canvas = canvas;
            _slotData = inventorySlot;
            _events = events;

            CreateDisplay();
            Subscribe();
        }

        private void OnDestroy() => 
            Cleanup();

        public void UpdateSlotUI(Sprite icon, string text)
        {
            CreateDisplay();
            Display.UpdateSlotUI(icon, text);
            Display.transform.localPosition = Vector3.zero;
        }

        private void CreateDisplay()
        {
            if (Display != null) return;
            Display = Instantiate(_prefab, transform);
            Display.Construct(_canvas, this, _events.Click);
        }

        private void Subscribe() => 
            _dropArea.OnDropHandler += Drop;

        private void Cleanup() => 
            _dropArea.OnDropHandler -= Drop;

        private void Drop(DraggableComponent draggable)
        {
            var slot = draggable.GetComponentInParent<InventorySlotUI>();
            _events.Drop?.Invoke(SlotData, slot.SlotData);
        }

        public void UpdateSlotData(InventorySlot inventorySlot) => 
            _slotData = inventorySlot;
    }
}