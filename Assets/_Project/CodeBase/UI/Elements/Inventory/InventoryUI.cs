using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroInventory;
using _Project.CodeBase.UI.Services.Windows.Inventory;
using _Project.CodeBase.UI.Windows;
using _Project.CodeBase.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Elements.Inventory
{
    public class InventoryUI : WindowBase
    {
        [SerializeField] private InventorySlotUI _prefab;
        [SerializeField] private InventoryContextUI _context;
        [SerializeField] private Transform _parent;
        [SerializeField] private Button _closeContext;

        private readonly List<InventorySlotUI> _list = new List<InventorySlotUI>();
        private HeroInventory _heroInventory;
        
        public void Construct(HeroInventory heroInventory) => 
            _heroInventory = heroInventory;

        protected override void OnAwake()
        {
            base.OnAwake();
            _context.Construct(_heroInventory, _parent.GetComponent<RectTransform>());
            CloseButton.onClick.AddListener(_context.Clear);
            _closeContext.onClick.AddListener(_context.Clear);
            InitializeSlots();
        }
        
        private void OnDestroy()
        {
            CloseButton.onClick.RemoveAllListeners();
            _closeContext.onClick.RemoveAllListeners();
        }

        protected override void SubscribeUpdates()
        {
            _heroInventory.OnUpdate += UpdateData;
            UpdateData();
        }

        private void InitializeSlots()
        {
            for (var i = 0; i < _heroInventory.Inventory.Slots.Length; i++)
            {
                var slot = Instantiate(_prefab, _parent);
                slot.Construct(i, HandleClick);
                _list.Add(slot);
            }
        }

        private void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _heroInventory.GetSlot(slotUI.SlotID).DbId;
            if (dbId == -1) return;
            var actions = _heroInventory.ItemsDataBase.FindItemByIndex(dbId).ItemPayloadData.Actions;
            _context.InitializeContext(actions, slotUI);
        }

        private void UpdateData()
        {
            for (var i = 0; i < _heroInventory.Inventory.Slots.Length; i++) 
                UpdateSlot(_heroInventory.GetSlot(i), i);
        }
        
        private void UpdateSlot(InventorySlot inventorySlot, int index)
        {
            if (inventorySlot.DbId != -1)
            {
                var itemData = _heroInventory.ItemsDataBase.FindItemByIndex(inventorySlot.DbId);
                UpdateSlotUI(_list[index], itemData.ItemUIData.Icon, inventorySlot.Amount.ToString());
            }
            else
                UpdateSlotUI(_list[index], null, "");
        }

        private void UpdateSlotUI(InventorySlotUI inventorySlotUI, Sprite icon, string text)
        {
            inventorySlotUI.Icon.ChangeAlpha(icon is null ? 0f : 1f);
            inventorySlotUI.Icon.sprite = icon;
            inventorySlotUI.Amount.text = text;
        }

        
    }
}