using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Hud.HotBar;
using _Project.CodeBase.UI.Windows.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements
{
    public class InventoriesHolderUI : MonoBehaviour
    {
        private HotBarUI _hotBarUI;
        private InventoryUI _inventoryUI;
        private HeroInventory _heroInventory;

        public void Construct(HotBarUI hotBarUI, InventoryUI inventoryUI, HeroInventory heroInventory)
        {
            _heroInventory = heroInventory;
            _hotBarUI = hotBarUI;
            _inventoryUI = inventoryUI;
            
            ConstructInventories().Forget();
        }

        private async UniTaskVoid ConstructInventories()
        {
            await UniTask.WaitUntil(InventoryInitialized);
            _hotBarUI.Construct(_heroInventory, transform, HandleDrop);
            _inventoryUI.Construct(_heroInventory, transform, HandleDrop);
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
            if (index == _heroInventory.ErrorIndex) return;
            
            var slotsHolder = index < _heroInventory.Inventory.HotBarSlots 
                ? _hotBarUI.SlotsHolder 
                : _inventoryUI.SlotsHolder;

            slotsHolder.UpdateSlot(index);
        }
    }
}