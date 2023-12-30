using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class RangeWeaponHandler : BaseWeaponBuilder
    {
        private readonly AttachmentController _attachmentController;
        private WeaponItem _weaponItem;
        private GunInfo _gunInfo;

        public override WeaponType Type => WeaponType.Default;


        protected RangeWeaponHandler(AttachmentController attachmentController) => 
            _attachmentController = attachmentController;
        
        public override async UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, GameObject prefab)
        {
            await base.CreateNewItem(slotID, part, info, prefab);
            
            if (_weaponItem is null) return;
            _attachmentController.Setup(CurrentItem, _weaponItem.WeaponData);
            SetupAttachments();
        }

        public override void FinishItemDestroy()
        {
            base.FinishItemDestroy();
            _attachmentController.DestroyAllAttachments();
        }

        protected override void PrepareData(CommonItemPart part, ItemInfo info)
        {
            _weaponItem = part.Item as WeaponItem;
            _gunInfo = info as GunInfo;
        }

        protected override void ConstructWeapon<T>(T info)
        {
            base.ConstructWeapon(info);
            
            if (info is not GunInfo gunInfo) return;
            
            HeroFacade.Ammo.Construct(_weaponItem.MagazineData, gunInfo);
            HeroFacade.Recoil.Construct(gunInfo);
            var config = CurrentItem.WeaponConfiguration;
            HeroFacade.Attack.UpdateStatsAndConfig(gunInfo, config);
            HeroFacade.Reload.Construct(gunInfo, config, config.RevolverAnimation);
            HeroFacade.Scoping.Construct(gunInfo, config);
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