using System;
using _Project.CodeBase.Logic.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class HotBarUI : MonoBehaviour, ISlotHolderUI
    {
        private const int Zero = 0;
        
        [SerializeField] private SlotsHolderUI _slotsHolder;
        [SerializeField] private Transform _inventoryButton;
        [SerializeField] private float _buttonOffset;
        [Space] 
        [SerializeField] private InventorySlotUI _prefab;
        [SerializeField] private HorizontalLayoutGroup _horizontal;
        [SerializeField] private RectTransform _container;

        private HeroInventory _heroInventory;
        private ItemDescription _itemDescription;
        private RectTransform _rectTransform;
        private float _width;
        private float _spacing;

        public SlotsHolderUI SlotsHolder => _slotsHolder;
        
        public void Construct(HeroInventory inventory, ItemDescription itemDescription, Transform uiRoot, Action<InventorySlot,InventorySlot> handleDrop)
        {
            _heroInventory = inventory;
            _itemDescription = itemDescription;
            _slotsHolder.Construct(inventory, uiRoot, Zero, _heroInventory.Inventory.HotBarSlots);

            OnConstructInitialized(handleDrop);
        }

        private void OnDestroy() =>
            Cleanup();

        private void OnConstructInitialized(Action<InventorySlot,InventorySlot> handleDrop)
        {
            Subscribe();
            InitializeBarSettings();

            InitializeSlots(new SlotTouchEvents(HandleClick, handleDrop));
            UpdateData();
        }

        private void Subscribe() =>
            _heroInventory.OnUpdate += UpdateData;

        private void Cleanup() =>
            _heroInventory.OnUpdate -= UpdateData;

        public void InitializeSlots(SlotTouchEvents slotTouchEvents)
        {
            _slotsHolder.InitializeSlots(slotTouchEvents);
            UpdateBarView();
        }

        public void UpdateData() =>
            _slotsHolder.UpdateData();

        public void UpdateSlot(int slotIndex) =>
            _slotsHolder.UpdateSlot(slotIndex);

        public void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _heroInventory.GetSlot(slotUI.SlotID).ID;
            if (dbId == -1) return;
            var item = _heroInventory.ItemsInfo.FindItem(dbId);
            var actions = item.PayloadInfo.Actions;
            
            if (actions.Contains(ActionType.Equip))
                _heroInventory.EquipItem(slotUI.SlotID);
            
            _itemDescription.UpdateView(item, slotUI.SlotID);
        }

        private void InitializeBarSettings()
        {
            _rectTransform = (RectTransform) transform;
            _width = ((RectTransform) _prefab.transform).sizeDelta.x;
            _spacing = _horizontal.spacing;
        }

        private void UpdateBarView()
        {
            var containerWidth = _width * _slotsHolder.SlotsUI.Count + _spacing * (_slotsHolder.SlotsUI.Count - 1);
            var buttonXPos = containerWidth / 2 + _spacing + _buttonOffset;

            ApplySizeDelta(_container, containerWidth);
            ApplySizeDelta(_rectTransform, containerWidth);
            ApplyXLocalPosition(_inventoryButton, buttonXPos);
        }

        private void ApplyXLocalPosition(Transform rect, float x)
        {
            var position = rect.localPosition;
            rect.localPosition = new Vector3(x, position.y, position.z);
        }

        private void ApplySizeDelta(RectTransform rect, float x) =>
            rect.sizeDelta = new Vector2(x, rect.sizeDelta.y);
    }
}