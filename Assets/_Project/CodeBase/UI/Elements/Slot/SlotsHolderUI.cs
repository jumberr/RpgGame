using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Windows.Inventory;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements.Slot
{
    public class SlotsHolderUI : MonoBehaviour, ISlotHolderUI
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private InventorySlotUI _prefab;
        [SerializeField] private RectTransform _parent;

        private readonly List<InventorySlotUI> _slotsUI = new List<InventorySlotUI>();
        private HeroInventory _heroInventory;
        private Transform _uiRoot;
        private int _from;
        private int _max;

        public RectTransform Parent => _parent;
        public List<InventorySlotUI> SlotsUI => _slotsUI;

        public void Construct(HeroInventory heroInventory, Transform uiRoot, int from, int max)
        {
            _heroInventory = heroInventory;
            _uiRoot = uiRoot;
            _from = from;
            _max = max;
        }

        public void HandleClick(InventorySlotUI slotUI) { }

        public void InitializeSlots(SlotTouchEvents slotTouchEvents)
        {
            for (var i = _from; i < _max; i++)
            {
                var slot = Instantiate(_prefab, _parent);
                slot.Construct(i, _canvas, _uiRoot, _heroInventory.GetSlot(i), slotTouchEvents);
                _slotsUI.Add(slot);
            }
        }

        public void UpdateData()
        {
            for (var i = _from; i < _max; i++) 
                UpdateSlot(i);
        }

        public void UpdateSlot(int slotIndex)
        {
            var slotUI = _slotsUI[slotIndex - _from];
            var slot = _heroInventory.GetSlot(slotIndex);
            UpdateSlot(slotUI, slot);
        }

        public void UpdateSlot(InventorySlotUI slotUI)
        {
            var slotIndex = slotUI.SlotID + _from;
            var slot = _heroInventory.GetSlot(slotIndex);
            UpdateSlot(slotUI, slot);
        }

        private void UpdateSlot(InventorySlotUI slotUI, InventorySlot slot)
        {
            slotUI.UpdateSlotData(slot);
            UpdateSlotUI(slot, slotUI);
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