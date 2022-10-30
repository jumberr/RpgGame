using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Slot;
using _Project.CodeBase.UI.Elements.Slot.Drop;
using UnityEngine;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class InventorySlotUI : MonoBehaviour, IDropArea
    {
        [SerializeField] private ItemDisplay _prefab;

        [HideInInspector] public ItemDisplay Display;
        [HideInInspector] public int SlotID;

        private InventorySlot _slotData;
        private Canvas _canvas;
        private Transform _uiRoot;
        private SlotTouchEvents _events;

        public InventorySlot SlotData => _slotData;
        public List<DropCondition> DropConditions { get; set; }

        public void Construct(int id, Canvas canvas, Transform uiRoot, InventorySlot inventorySlot, SlotTouchEvents events)
        {
            SlotID = id;
            _canvas = canvas;
            _uiRoot = uiRoot;
            _slotData = inventorySlot;
            _events = events;

            CreateDisplay();
        }

        private void OnDestroy() => 
            Cleanup();
        
        public void Initialize()
        {
        }

        public bool Accepts(ICanBeDragged draggable) =>
            true;

        public void Drop(InventorySlot data) => 
            _events.Drop?.Invoke(SlotData, data);

        public void UpdateSlotData(InventorySlot inventorySlot) =>
            _slotData = inventorySlot;

        public void UpdateSlotUI(Sprite icon, string text)
        {
            CreateDisplay();
            Display.UpdateSlot(_slotData.DbId, icon, text);
            Display.transform.localPosition = Vector3.zero;
        }

        public void Release() =>
            ClearSlot();

        private void CreateDisplay()
        {
            if (Display != null) return;
            Display = Instantiate(_prefab, transform);
            Display.Construct(_canvas, _uiRoot, this, _events);
        }

        private void ClearSlot()
        {
            _slotData = null;
            Display.Clear();
        }

        private void Cleanup() => 
            ClearSlot();
    }
}