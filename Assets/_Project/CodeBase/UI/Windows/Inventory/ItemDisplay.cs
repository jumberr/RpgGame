using System;
using _Project.CodeBase.UI.Elements.SpecificButtonLogic;
using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI.Windows.Inventory
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

        public void Construct(Canvas canvas, Transform uiRoot, InventorySlotUI slot, Action<InventorySlotUI> click)
        {
            _canvas = canvas;
            _uiRoot = uiRoot;
            _slot = slot;
            _clickable.Construct(slot, click);
            CreateDraggableElement(_slot.SlotData.DbId);
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
                Destroy(_draggable.gameObject);
            
            if (dbId != Logic.Inventory.Inventory.ErrorIndex)
            {
                _draggable = Instantiate(_draggablePrefab, transform);
                _draggable.Construct(_canvas, _uiRoot, _slot);
            }
        }

        private void UpdateSlotUI(Sprite icon, string text)
        {
            if (_draggable != null) 
                _draggable.ChangeIcon(icon);
            
            _amount.text = text;
        }
    }
}