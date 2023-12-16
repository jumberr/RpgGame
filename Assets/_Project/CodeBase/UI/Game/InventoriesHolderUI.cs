using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Services;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class InventoriesHolderUI
    {
        private readonly HeroFacade.Factory _heroFactory;
        private readonly IUIFactory _uiFactory;

        private HeroInventory _heroInventory;
        private Transform _uiRoot;
        private HotBarUI _hotBarUI;
        private InventoryWindow _inventoryWindow;

        private InventoriesHolderUI(HeroFacade.Factory heroFactory, IUIFactory uiFactory)
        {
            _heroFactory = heroFactory;
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            InitializeUI();
            InitializeInventory();
        }

        private void InitializeInventory()
        {
            _heroInventory = _heroFactory.Instance.Inventory;
            ConstructInventories();
        }

        private void InitializeUI()
        {
            _uiRoot = _uiFactory.UIRoot;
            _hotBarUI = _uiFactory.ActorUI.HotBar;
            _inventoryWindow = _uiFactory.InventoryWindow;
        }

        private void ConstructInventories()
        {
            _hotBarUI.Construct(_heroInventory, _inventoryWindow.ItemDescription, _uiRoot, HandleDrop);
            _inventoryWindow.Construct(_heroInventory, _uiRoot, HandleDrop);
            _inventoryWindow.ItemDescription.Construct(_heroInventory);
        }

        private void HandleDrop(InventorySlot one, InventorySlot two)
        {
            var first = _heroInventory.FindIndex(one);
            var second = _heroInventory.FindIndex(two);
            
            if (one != null && two != null) 
                _heroInventory.SwapSlots(first, second);
            
            UpdateSlots(first, second);
        }

        private void UpdateSlots(int first, int second)
        {
            UpdateSlot(first);
            UpdateSlot(second);
        }

        private void UpdateSlot(int index)
        {
            if (index == Inventory.ErrorIndex) return;
            
            var slotsHolder = index < _heroInventory.Inventory.HotBarSlots 
                ? _hotBarUI.SlotsHolder 
                : _inventoryWindow.SlotsHolder;

            slotsHolder.UpdateSlot(index);
        }
    }
}