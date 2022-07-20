using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.Utils.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class InventoryUI : WindowBase
    {
        [SerializeField] private InventorySlotUI _prefab;
        [SerializeField] private InventoryContextUI _context;
        [SerializeField] private Transform _parent;
        [SerializeField] private Button _closeContext;

        private readonly List<InventorySlotUI> _inventory = new List<InventorySlotUI>();
        private HeroInventory _heroInventory;
        
        public async void Construct(HeroInventory heroInventory)
        {
            _heroInventory = heroInventory;
            await UniTask.WaitUntil(IsInventoryExists);
            OnConstructInitialized();
        }

        protected override void OnConstructInitialized()
        {
            _context.Construct(_heroInventory, _parent.GetComponent<RectTransform>());
            Subscribe();
            InitializeSlots();
            UpdateData();
        }

        protected override void Cleanup()
        {
            _heroInventory.OnUpdate -= UpdateData;
            CloseButton.onClick.RemoveAllListeners();
            _closeContext.onClick.RemoveAllListeners();
        }

        private void Subscribe()
        {
            _heroInventory.OnUpdate += UpdateData;
            CloseButton.onClick.AddListener(_context.Clear);
            _closeContext.onClick.AddListener(_context.Clear);
        }

        private void InitializeSlots()
        {
            for (var i = 0; i < _heroInventory.Inventory.Slots.Length; i++)
            {
                var slot = Instantiate(_prefab, _parent);
                slot.Construct(i, HandleClick);
                _inventory.Add(slot);
            }
        }

        private void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _heroInventory.GetSlot(slotUI.SlotID).DbId;
            if (dbId == -1) return;
            var actions = _heroInventory.ItemsDataBase.FindItem(dbId).ItemPayloadData.Actions;
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
                var itemData = _heroInventory.ItemsDataBase.FindItem(inventorySlot.DbId);
                UpdateSlotUI(_inventory[index], itemData.ItemUIData.Icon, inventorySlot.Amount.ToString());
            }
            else
                UpdateSlotUI(_inventory[index], null, "");
        }

        private void UpdateSlotUI(InventorySlotUI inventorySlotUI, Sprite icon, string text)
        {
            inventorySlotUI.Icon.ChangeAlpha(icon is null ? 0f : 1f);
            inventorySlotUI.Icon.sprite = icon;
            inventorySlotUI.Amount.text = text;
        }
        
        private bool IsInventoryExists() => 
            _heroInventory.Inventory != null;
    }
}