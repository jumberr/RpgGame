using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Windows.Inventory;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Elements.Hud.HotBar
{
    public class HotBarUI : MonoBehaviour
    {
        [SerializeField] private Transform _inventoryButton;
        [SerializeField] private float _buttonOffset;
        [Space]
        [SerializeField] private InventorySlotUI _prefab;
        [SerializeField] private HorizontalLayoutGroup _horizontal;
        [SerializeField] private RectTransform _container;

        private readonly List<InventorySlotUI> _slots = new List<InventorySlotUI>();
        private HeroInventory _inventory;

        private RectTransform _rectTransform;
        private float _width;
        private float _spacing;

        //private InventoryContextUI _context;

        public void Construct(HeroInventory inventory)
        {
            _inventory = inventory;
            //_context = context;
            InitializeBarSettings();
            InitializeHotBar();
            UpdateData();
        }

        private void InitializeHotBar()
        {
            for (var i = 0; i < _inventory.Inventory.HotBarSlots.Length; i++)
            {
                var slot = Instantiate(_prefab, _container);
                slot.Construct(i, HandleClick);
                _slots.Add(slot);
            }

            UpdateBarView();
        }

        private void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _inventory.GetHotBarSlot(slotUI.SlotID).DbId;
            if (dbId == -1) return;
            var actions = _inventory.ItemsDataBase.FindItem(dbId).ItemPayloadData.Actions;
            //_context.InitializeContext(actions, slotUI);
        }

        private void UpdateData()
        {
            for (var i = 0; i < _inventory.Inventory.HotBarSlots.Length; i++) 
                UpdateSlot(_inventory.GetHotBarSlot(i), i);
        }

        private void UpdateSlot(InventorySlot inventorySlot, int index)
        {
            if (inventorySlot.DbId != -1)
            {
                var itemData = _inventory.ItemsDataBase.FindItem(inventorySlot.DbId);
                UpdateSlotUI(_slots[index], itemData.ItemUIData.Icon, inventorySlot.Amount.ToString());
            }
            else
                UpdateSlotUI(_slots[index], null, "");
        }

        private void UpdateSlotUI(InventorySlotUI inventorySlotUI, Sprite icon, string text)
        {
            inventorySlotUI.Icon.ChangeAlpha(icon is null ? 0f : 1f);
            inventorySlotUI.Icon.sprite = icon;
            inventorySlotUI.Amount.text = text;
        }

        private void InitializeBarSettings()
        {
            _rectTransform = (RectTransform) transform;
            _width = ((RectTransform) _prefab.transform).sizeDelta.x;
            _spacing = _horizontal.spacing;
        }

        private void UpdateBarView()
        {
            var containerWidth = _width * _slots.Count + _spacing * (_slots.Count - 1);
            var buttonXPos = containerWidth / 2 + _spacing + _buttonOffset;
            
            ApplySizeDelta(_container, containerWidth);
            ApplySizeDelta(_rectTransform, containerWidth);
            ApplyXLocalPosition(_inventoryButton, buttonXPos);
        }

        private void ApplyXLocalPosition(Transform rect, float x)
        {
            var position = rect.localPosition;
            rect.localPosition = new Vector3(x, position.y, position.z);
        }
        
        private void ApplySizeDelta(RectTransform rect, float x) => 
            rect.sizeDelta = new Vector2(x, rect.sizeDelta.y);
    }
}