using _Project.CodeBase.Logic.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class InventoriesHolderUI : MonoBehaviour
    {
        private WeaponStatsUI _weaponStatsUI;
        private HotBarUI _hotBarUI;
        private InventoryWindow _inventoryWindow;
        private HeroInventory _heroInventory;

        public void Construct(HotBarUI hotBarUI, InventoryWindow inventoryWindow, HeroInventory heroInventory)
        {
            _heroInventory = heroInventory;
            _hotBarUI = hotBarUI;
            _inventoryWindow = inventoryWindow;
            _inventoryWindow.ItemDescription.Construct(heroInventory);
            
            ConstructInventories().Forget();
        }

        private async UniTaskVoid ConstructInventories()
        {
            await UniTask.WaitUntil(InventoryInitialized);
            _hotBarUI.Construct(_heroInventory, _inventoryWindow.ItemDescription, transform, HandleDrop);
            _inventoryWindow.Construct(_heroInventory, transform, HandleDrop);
        }

        private bool InventoryInitialized() => 
            _heroInventory.Inventory != null;

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