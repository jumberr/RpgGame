using System;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Slot;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class InventoryUI : WindowBase, ISlotHolderUI
    {
        [SerializeField] private SlotsHolderUI _slotsHolder;
        [SerializeField] private InventoryContextUI _context;
        [SerializeField] private Button _closeContext;
        [Space]
        [SerializeField] private ItemDescription _itemDescription;

        private HeroInventory _heroInventory;
        private Action<InventorySlot, InventorySlot> _handleDrop;

        public SlotsHolderUI SlotsHolder => _slotsHolder;
        public ItemDescription ItemDescription => _itemDescription;
        private int HotBarSlotsAmount => _heroInventory.Inventory.HotBarSlots;

        public void Construct(HeroInventory heroInventory, Transform uiRoot, Action<InventorySlot,InventorySlot> handleDrop)
        {
            _heroInventory = heroInventory;
            _handleDrop = handleDrop;
            _slotsHolder.Construct(heroInventory, uiRoot, HotBarSlotsAmount, _heroInventory.Inventory.Slots.Length);
            OnConstructInitialized();
        }

        protected override UniTask OnHiding()
        {
            _context.Clear();
            return base.OnHiding();
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
            
            InitializeSlots(InitializeSlotEvents());
            UpdateData();
        }

        public void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _heroInventory.GetSlot(slotUI.SlotID).ID;
            if (dbId == -1) return;
            var item = _heroInventory.ItemsInfo.FindItem(dbId);
            _context.InitializeContext(item.PayloadInfo.Actions, slotUI);
            
            _itemDescription.UpdateView(item, slotUI.SlotID);
        }

        protected override void Cleanup()
        {
            _heroInventory.OnUpdate -= UpdateData;
            _closeContext.onClick.RemoveAllListeners();
        }

        private void Subscribe()
        {
            _heroInventory.OnUpdate += UpdateData;
            _closeContext.onClick.AddListener(_context.Clear);
        }

        private SlotTouchEvents InitializeSlotEvents()
        {
            var slotTouch = new SlotTouchEvents(HandleClick, _handleDrop);
            slotTouch.SetBeginDragEvent(_context.Clear);
            return slotTouch;
        }
    }
}