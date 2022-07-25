using System;
using _Project.CodeBase.Logic.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class InventoryUI : WindowBase, ISlotHolderUI
    {
        [SerializeField] private SlotHolderUI _slotHolder;
        [SerializeField] private InventoryContextUI _context;
        [SerializeField] private Transform _parent;
        [SerializeField] private Button _closeContext;

        private HeroInventory _heroInventory;
        private int HotBarSlotsAmount => _heroInventory.Inventory.HotBarSlots;

        public async void Construct(HeroInventory heroInventory)
        {
            _heroInventory = heroInventory;
            await UniTask.WaitUntil(IsInventoryExists);
            _slotHolder.Construct(heroInventory, HotBarSlotsAmount, _heroInventory.Inventory.Slots.Length);
            OnConstructInitialized();
        }

        public void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _heroInventory.GetSlot(slotUI.SlotID).DbId;
            if (dbId == -1) return;
            var actions = _heroInventory.ItemsDataBase.FindItem(dbId).ItemPayloadData.Actions;
            _context.InitializeContext(actions, slotUI);
        }

        public void InitializeSlots(Action<InventorySlotUI> handleClick) => 
            _slotHolder.InitializeSlots(handleClick);

        public void UpdateData() => 
            _slotHolder.UpdateData();

        public void UpdateSlot(InventorySlot inventorySlot, int slotIndex) => 
            _slotHolder.UpdateSlot(inventorySlot, slotIndex);

        protected override void OnConstructInitialized()
        {
            _context.Construct(_heroInventory, (RectTransform) _parent);
            Subscribe();
            _slotHolder.InitializeSlots(HandleClick);
            _slotHolder.UpdateData();
        }

        protected override void Cleanup()
        {
            _heroInventory.OnUpdate -= _slotHolder.UpdateData;
            CloseButton.onClick.RemoveAllListeners();
            _closeContext.onClick.RemoveAllListeners();
        }

        private void Subscribe()
        {
            _heroInventory.OnUpdate += _slotHolder.UpdateData;
            CloseButton.onClick.AddListener(_context.Clear);
            _closeContext.onClick.AddListener(_context.Clear);
        }

        private bool IsInventoryExists() => 
            _heroInventory.Inventory != null;
    }
}