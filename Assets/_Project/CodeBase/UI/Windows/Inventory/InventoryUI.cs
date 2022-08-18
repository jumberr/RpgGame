using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Slot;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class InventoryUI : WindowBase, ISlotHolderUI
    {
        [SerializeField] private SlotHolderUI _slotHolder;
        [SerializeField] private InventoryContextUI _context;
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

        public void InitializeSlots(SlotTouchEvents slotTouchEvents) => 
            _slotHolder.InitializeSlots(slotTouchEvents);

        public void UpdateData() => 
            _slotHolder.UpdateData();

        public void UpdateSlot(InventorySlot inventorySlot, int slotIndex) => 
            _slotHolder.UpdateSlot(inventorySlot, slotIndex);

        protected override void OnConstructInitialized()
        {
            _context.Construct(_heroInventory, _slotHolder.Parent);
            Subscribe();
            InitializeSlots(new SlotTouchEvents(HandleClick, HandleDrop));
            UpdateData();
        }
        
        public void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _heroInventory.GetSlot(slotUI.SlotID).DbId;
            if (dbId == -1) return;
            var actions = _heroInventory.ItemsDataBase.FindItem(dbId).ItemPayloadData.Actions;
            _context.InitializeContext(actions, slotUI);
        }

        public void HandleDrop(InventorySlot one, InventorySlot two) => 
            _slotHolder.HandleDrop(one, two);

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

        private bool IsInventoryExists() => 
            _heroInventory.Inventory != null;
    }
}