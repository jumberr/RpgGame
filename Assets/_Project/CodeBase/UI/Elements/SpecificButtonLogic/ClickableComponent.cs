using System;
using _Project.CodeBase.UI.Windows.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class ClickableComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler
    {
        private InventorySlotUI _slot;
        private Action<InventorySlotUI> _click;

        public void Construct(InventorySlotUI slot, Action<InventorySlotUI> click)
        {
            _slot = slot;
            _click = click;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerPress == gameObject && !eventData.dragging) 
                _click?.Invoke(_slot);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
    }
}