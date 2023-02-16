using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class ItemDescription : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private DescriptionView _descriptionView;
        [SerializeField] private AttachmentControllerUI _attachmentController;
        [SerializeField] private WeaponStatsUI _weaponStatsUI;
        [Space]
        [SerializeField] private SimpleScaleUIAnimation _scaleAnimation;

        private HeroInventory _heroInventory;
        private List<Component> _components = new List<Component>();
        private bool _hidden;
        private int _slotID;

        private void Start()
        {
            InitializeComponents();
            Hide();
        }

        public void Construct(HeroInventory heroInventory)
        {
            _heroInventory = heroInventory;
            _heroInventory.OnDrop += HideDroppedItem;
        }

        public void UpdateView(ItemInfo info, int slotID)
        {
            _slotID = slotID;
            Show();
            Animate();
            UpdateGeneralPart(info);
            
            var value = info.PayloadInfo.ItemType == ItemType.Weapon;
            UpdateAttachmentPart(info);
            UpdateWeaponPart(info, value);
        }

        private void InitializeComponents() =>
            _components = new List<Component> {_background, _descriptionView.Icon, _descriptionView.Name, _descriptionView.Description };

        private void Animate() => 
            _scaleAnimation.Scale();

        private void UpdateGeneralPart(ItemInfo info) => 
            _descriptionView.Setup(info);

        private void UpdateAttachmentPart(ItemInfo info)
        {
            _attachmentController.Turn(false);

            if (!(info is GunInfo gun)) return;
            var possibleTypes = gun.AttachmentPossibility.GetPossibleTypes();
            if (possibleTypes.Count <= 0) return;
            // TODO: pass here IItem with attachments data or just possible types
            EnableAttachmentSlots(possibleTypes);
        }

        private void EnableAttachmentSlots(List<AttachmentType> possibleTypes)
        {
            _attachmentController.Turn(true);
            _attachmentController.Setup(possibleTypes);
        }

        private void UpdateWeaponPart(ItemInfo info, bool value)
        {
            _weaponStatsUI.Turn(value);
            if (value)
                _weaponStatsUI.UpdateView(info);
        }

        private void Show()
        {
            if (!_hidden) return;
            
            _components.ActivateComponents();
            _hidden = false;
        }

        private void Hide()
        {
            _hidden = true;
            _components.DeactivateComponents();
        }

        private void HideDroppedItem(int id)
        {
            if (_slotID == id) 
                Hide();
        }
    }
}