using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Slot;
using _Project.CodeBase.UI.Windows.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Elements.Hud.HotBar
{
    public class HotBarUI : MonoBehaviour, ISlotHolderUI
    {
        [SerializeField] private SlotHolderUI _slotHolder;
        [SerializeField] private Transform _inventoryButton;
        [SerializeField] private float _buttonOffset;
        [Space] [SerializeField] private InventorySlotUI _prefab;
        [SerializeField] private HorizontalLayoutGroup _horizontal;
        [SerializeField] private RectTransform _container;

        private HeroInventory _heroInventory;
        private RectTransform _rectTransform;
        private float _width;
        private float _spacing;

        public void Construct(HeroInventory inventory)
        {
            _heroInventory = inventory;
            _slotHolder.Construct(inventory, 0, _heroInventory.Inventory.HotBarSlots);
            Subscribe();
            InitializeBarSettings();

            InitializeSlots(new SlotTouchEvents(HandleClick, HandleDrop));
            UpdateData();
        }

        private void OnDestroy() =>
            Cleanup();

        private void Subscribe() =>
            _heroInventory.OnUpdate += UpdateData;

        private void Cleanup() =>
            _heroInventory.OnUpdate -= UpdateData;

        public void InitializeSlots(SlotTouchEvents slotTouchEvents)
        {
            _slotHolder.InitializeSlots(slotTouchEvents);
            UpdateBarView();
        }

        public void UpdateData() =>
            _slotHolder.UpdateData();

        public void UpdateSlot(InventorySlot inventorySlot, int slotIndex) =>
            _slotHolder.UpdateSlot(inventorySlot, slotIndex);

        public void HandleClick(InventorySlotUI slotUI)
        {
            var dbId = _heroInventory.GetSlot(slotUI.SlotID).DbId;
            if (dbId == -1) return;
            var actions = _heroInventory.ItemsDataBase.FindItem(dbId).ItemPayloadData.Actions;
            if (actions.Contains(ActionType.Equip))
                _heroInventory.EquipItem(slotUI.SlotID);
        }

        public void HandleDrop(InventorySlot one, InventorySlot two) =>
            _slotHolder.HandleDrop(one, two);

        private void InitializeBarSettings()
        {
            _rectTransform = (RectTransform) transform;
            _width = ((RectTransform) _prefab.transform).sizeDelta.x;
            _spacing = _horizontal.spacing;
        }

        private void UpdateBarView()
        {
            var containerWidth = _width * _slotHolder.SlotsUI.Count + _spacing * (_slotHolder.SlotsUI.Count - 1);
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