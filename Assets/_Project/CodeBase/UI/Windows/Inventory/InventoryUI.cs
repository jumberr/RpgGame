using System;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Slot;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class InventoryUI : WindowBase, ISlotHolderUI
    {
        [SerializeField] private SlotsHolderUI _slotsHolder;
        [SerializeField] private InventoryContextUI _context;
        [SerializeField] private Button _closeContext;

        private HeroInventory _heroInventory;
        private Action<InventorySlot, InventorySlot> _handleDrop;

        public SlotsHolderUI SlotsHolder => _slotsHolder;
        private int HotBarSlotsAmount => _heroInventory.Inventory.HotBarSlots;

        public void Construct(HeroInventory heroInventory, Transform uiRoot, Action<InventorySlot,InventorySlot> handleDrop)
        {
            _heroInventory = heroInventory;
            _handleDrop = handleDrop;
            _slotsHolder.Construct(heroInventory, uiRoot, HotBarSlotsAmount, _heroInventory.Inventory.Slots.Length);
            OnConstructInitialized();
        }

        public void InitializeSlots(SlotTouchEvents slotTouchEvents) => 
            _slotsHolder.InitializeSlots(slotTouchEvents);

        public void UpdateData() => 
            _slotsHolder.UpdateData();

        public void UpdateSlot(int slotIndex) => 
            _slotsHolder.UpdateSlot(slotIndex);

        protected override void OnConstructInitialized()
        {
            _context.Construct(_heroInventory, _slotsHolder.Parent);
            Subscribe();
            InitializeSlots(new SlotTouchEvents(HandleClick, _handleDrop));
            UpdateData();
        }
        
        public void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _heroInventory.GetSlot(slotUI.SlotID).DbId;
            if (dbId == -1) return;
            var actions = _heroInventory.ItemsDataBase.FindItem(dbId).ItemPayloadData.Actions;
            _context.InitializeContext(actions, slotUI);
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
    }
}