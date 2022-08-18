using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Windows.Inventory;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements.Slot
{
    public class SlotHolderUI : MonoBehaviour, ISlotHolderUI
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private InventorySlotUI _prefab;
        [SerializeField] private RectTransform _parent;

        private readonly List<InventorySlotUI> _slotsUI = new List<InventorySlotUI>();
        private HeroInventory _heroInventory;
        private int _from;
        private int _max;

        public RectTransform Parent => _parent;
        public List<InventorySlotUI> SlotsUI => _slotsUI;

        public void Construct(HeroInventory heroInventory, int from, int max)
        {
            _heroInventory = heroInventory;
            _from = from;
            _max = max;
        }

        public virtual void HandleClick(InventorySlotUI slotUI) { }

        public virtual void HandleDrop(InventorySlot one, InventorySlot two)
        {
            var first = _heroInventory.FindIndex(one);
            var second = _heroInventory.FindIndex(two);
            
            _heroInventory.SwapSlots(first, second);
            UpdateData();
        }

        public void InitializeSlots(SlotTouchEvents slotTouchEvents)
        {
            for (var i = _from; i < _max; i++)
            {
                var slot = Instantiate(_prefab, _parent);
                slot.Construct(i, _canvas, _heroInventory.GetSlot(i), slotTouchEvents);
                _slotsUI.Add(slot);
            }
        }

        public void UpdateData()
        {
            for (var i = _from; i < _max; i++) 
                UpdateSlot(_heroInventory.GetSlot(i), i);
        }

        public void UpdateSlot(InventorySlot inventorySlot, int slotIndex)
        {
            var slotUI = _slotsUI[slotIndex - _from];
            
            slotUI.UpdateSlotData(inventorySlot);
            UpdateSlotUI(inventorySlot, slotUI);
        }

        private void UpdateSlotUI(InventorySlot inventorySlot, InventorySlotUI slotUI)
        {
            if (inventorySlot.DbId != -1)
            {
                var itemData = _heroInventory.ItemsDataBase.FindItem(inventorySlot.DbId);
                slotUI.UpdateSlotUI(itemData.ItemUIData.Icon, inventorySlot.Amount.ToString());
            }
            else
                slotUI.UpdateSlotUI(null, string.Empty);
        }
    }
}