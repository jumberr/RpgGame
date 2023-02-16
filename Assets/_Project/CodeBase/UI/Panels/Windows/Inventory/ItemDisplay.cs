using _Project.CodeBase.UI.Elements.SpecificButtonLogic;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private DraggableComponent _draggablePrefab;
        [Space]
        [SerializeField] private ClickableComponent _clickable;
        [SerializeField] private TMP_Text _amount;

        private DraggableComponent _draggable;
        private Canvas _canvas;
        private Transform _uiRoot;
        private InventorySlotUI _slot;
        private SlotTouchEvents _events;

        public void Construct(Canvas canvas, Transform uiRoot, InventorySlotUI slot, SlotTouchEvents events)
        {
            _canvas = canvas;
            _uiRoot = uiRoot;
            _slot = slot;
            _events = events;
            _clickable.Construct(slot, _events.Click);
            CreateDraggableElement(_slot.SlotData.ID);
        }

        public void UpdateSlot(int dbId, Sprite icon, string text)
        {
            CreateDraggableElement(dbId);
            UpdateSlotUI(icon, text);
        }

        public void Clear()
        {
            _draggable = null;
            UpdateSlot(Logic.Inventory.Inventory.ErrorIndex, null, string.Empty);
        }

        private void CreateDraggableElement(int dbId)
        {
            if (_draggable != null) 
                CleanUpDraggable();

            if (dbId != Logic.Inventory.Inventory.ErrorIndex) 
                CreateDraggable();
        }

        private void UpdateSlotUI(Sprite icon, string text)
        {
            if (_draggable != null) 
                _draggable.ChangeIcon(icon);
            
            _amount.text = text;
        }

        private void CreateDraggable()
        {
            _draggable = Instantiate(_draggablePrefab, transform);
            _draggable.Construct(_canvas, _uiRoot, _slot);
            _draggable.OnDragStarted += ResetInventoryContext;
        }

        private void CleanUpDraggable()
        {
            _draggable.OnDragStarted -= ResetInventoryContext;
            Destroy(_draggable.gameObject);
        }

        private void ResetInventoryContext() => 
            _events.OnBeginDrag?.Invoke();
    }
}