using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData.ItemsDataBase;
using _Project.CodeBase.UI.Services.Windows.Inventory;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements.Inventory
{
    public class InventoryContextUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private InventoryActionUI _actionPrefab;
        private readonly List<InventoryActionUI> _list = new List<InventoryActionUI>();

        private HeroInventory _heroInventory;
        private RectTransform _inventoryRect;
        private Vector2 _actionSize;

        public void Construct(HeroInventory heroInventory, RectTransform rect)
        {
            _heroInventory = heroInventory;
            _inventoryRect = rect;
        }

        private void Start() => 
            _actionSize = _actionPrefab.GetComponent<RectTransform>().sizeDelta;

        public void InitializeContext(List<ActionType> actions, InventorySlotUI slotUI)
        {
            Clear();
            
            ChangeContextSize(actions.Count);
            ChangeContextPosition(slotUI.transform);
            
            foreach (var action in actions)
                CreateAction(action, slotUI);
        }

        public void Clear()
        {
            foreach (var action in _list) 
                Destroy(action.gameObject);
            _list.Clear();
        }
        
        private void CreateAction(ActionType action, InventorySlotUI slotUI)
        {
            var actionUI = Instantiate(_actionPrefab, transform);
            actionUI.Construct(FindAction(action, slotUI), action.ToString());
            _list.Add(actionUI);
        }

        private Action FindAction(ActionType action, InventorySlotUI slotUI)
        {
            Action onClick = action switch
            {
                ActionType.Equip => () => _heroInventory.EquipItem(slotUI.SlotID),
                ActionType.Drop => () => _heroInventory.DropItemFromSlot(slotUI.SlotID),
                ActionType.DropAll => () => _heroInventory.DropAllItemsFromSlot(slotUI.SlotID),
                _ => null
            };

            void OnClick()
            {
                onClick?.Invoke();
                Clear();
            }

            return OnClick;
        }

        private void ChangeContextSize(int size) => 
            _rectTransform.sizeDelta = new Vector2(_actionSize.x, _actionSize.y * size);

        private void ChangeContextPosition(Transform slotUI)
        {
            var position = slotUI.position;
            var localPosition = slotUI.localPosition;
            var contextPos = CalculatePosition(position, localPosition);
            _rectTransform.position = contextPos;
        }

        private Vector2 CalculatePosition(Vector2 position, Vector2 localPosition)
        {
            var delta = _rectTransform.sizeDelta;

            var one = new Vector2(delta.x, -delta.y);
            var two = new Vector2(-delta.x, -delta.y);
            var three = new Vector2(delta.x, delta.y);
            var four = new Vector2(-delta.x, delta.y);

            if (CheckContextInRect(localPosition, localPosition + one))
                return (position + (position + one)) / 2;
            if (CheckContextInRect(localPosition, localPosition + two))
                return (position + (position + two)) / 2;
            if (CheckContextInRect(localPosition, localPosition + three))
                return (position + (position + three)) / 2;
            if (CheckContextInRect(localPosition, localPosition + four))
                return ( position + (position + four)) / 2;

            throw new Exception();
        }

        private bool CheckContextInRect(Vector3 point1, Vector3 point2) => 
            _inventoryRect.rect.Contains(point1) && _inventoryRect.rect.Contains(point2);
    }
}