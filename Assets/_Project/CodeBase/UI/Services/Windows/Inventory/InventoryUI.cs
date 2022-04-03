using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroInventory;
using _Project.CodeBase.UI.Windows;
using UnityEngine;

namespace _Project.CodeBase.UI.Services.Windows.Inventory
{
    public class InventoryUI : WindowBase
    {
        [SerializeField] private InventorySlotUI _prefab;
        [SerializeField] private Transform _parent;
        private readonly List<InventorySlotUI> _list = new List<InventorySlotUI>();
        private HeroInventory _heroInventory;

        public void Construct(HeroInventory heroInventory) => 
            _heroInventory = heroInventory;

        protected override void OnAwake()
        {
            base.OnAwake();
            InitializeSlots();
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
                _list.Add(slot);
            }
        }

        private void UpdateData()
        {
            for (var i = 0; i < _heroInventory.Inventory.Slots.Length; i++) 
                UpdateSlot(_heroInventory.Inventory.Slots[i], i);
        }
        
        private void UpdateSlot(InventorySlot inventorySlot, int index)
        {
            if (inventorySlot.DbId != -1)
            {
                var itemData = _heroInventory.ItemsDataBase.FindItemByIndex(inventorySlot.DbId);
                UpdateSlotUI(itemData.ItemUIData.Icon, inventorySlot.Amount.ToString(), index);
                _list[index].Icon.color = Color.white;
            }
            else
            {
                UpdateSlotUI(null, "", index);
                _list[index].Icon.color = new Color(1, 1, 1, 0);
            }
        }

        private void UpdateSlotUI(Sprite icon, string text, int index)
        {
            _list[index].Icon.sprite = icon;
            _list[index].Amount.text = text;
        }
    }
}