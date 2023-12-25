using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponItemHandler : BaseItemHandler
    {
        private readonly AttachmentController _attachmentController;
        private WeaponItem _weaponItem;
        private GunInfo _gunInfo;

        public override ItemType ItemType => ItemType.Weapon;


        public WeaponItemHandler(AttachmentController attachmentController) => 
            _attachmentController = attachmentController;

        public override async UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, GameObject prefab)
        {
            _weaponItem = part.Item as WeaponItem;
            await CreateGun(slotID, info, prefab);
            _gunInfo = info as GunInfo; // todo remove this cast
            ConstructWeapon(_gunInfo);
            
            _attachmentController.Setup(CurrentItem, _weaponItem.WeaponData);
            SetupAttachments();
        }
        
        private async UniTask CreateGun(int slotID, ItemInfo info, GameObject prefab)
        {
            if (CurrentItem.SlotID == slotID) return;

            await Destroy();

            CurrentItem.Create(slotID, info, prefab, Parent);
            Animator.Construct(CurrentItem.WeaponConfiguration.HandsAnimator);
            await Animator.ShowWeaponOnInit();
        }
        
        private async UniTask AnimatorHideWeapon()
        {
            await Animator.HideWeapon();
            Animator.SetEmptyAnimator();
        }

        protected override async UniTask CheckIfDestroy(ItemInfo itemInfo)
        {
            if (itemInfo != null && Inventory.FindFirstItemIndex(itemInfo.UIInfo.Name) == Logic.Inventory.Inventory.ErrorIndex)
                await Destroy();
        }

        protected override async UniTask DestroyItem()
        {
            await base.DestroyItem();
            await AnimatorHideWeapon();
            Attack.SetWeaponTypeNone();
            DestroyWeaponAndAttachments();
        }
        
        private void ConstructWeapon(GunInfo gunInfo)
        {
            Ammo.Construct(_weaponItem.MagazineData, gunInfo);
            Recoil.Construct(gunInfo);
            SetupConfig(gunInfo);
        }
        
        private void DestroyWeaponAndAttachments()
        {
            _attachmentController.DestroyAllAttachments();
            CurrentItem.Destroy();
        }

        private void SetupConfig(GunInfo gunInfo)
        {
            var config = CurrentItem.WeaponConfiguration;
            Attack.UpdateStatsAndConfig(gunInfo, config);
            Reload.Construct(gunInfo, config, config.RevolverAnimation);
            Scoping.Construct(gunInfo, config);
        }

        private void SetupAttachments()
        {
            if (_gunInfo != null)
            {
                if (!_weaponItem.WeaponData.Modified)
                    CreateDefaultAttachments();
                else
                    CreateSavedAttachments();
            }
        }

        private void CreateDefaultAttachments()
        {
            foreach (var itemName in _gunInfo.DefaultAttachments) 
                CreateAttachment(itemName);
        }

        private void CreateSavedAttachments()
        {
            foreach (var attachment in _weaponItem.WeaponData.Attachments) 
                CreateAttachment(attachment.Value);
        }

        private void CreateAttachment(ItemName itemName) => 
            _attachmentController.CreateAttachment((AttachmentInfo) ItemsInfo.FindItem(itemName));
    }
}