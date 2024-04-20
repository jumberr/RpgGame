using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class RangeWeaponBuilder : BaseWeaponBuilder
    {
        private readonly AttachmentController _attachmentController;
        private WeaponItem _weaponItem;
        private GunInfo _gunInfo;
        private GunSpecs _gunSpecs;
        public override Type[] SupportedTypes { get; } = {typeof(GunInfo)};


        protected RangeWeaponBuilder(AttachmentController attachmentController) => 
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
            _gunSpecs = _gunInfo.GetSpecs();
        }

        protected override void ConstructWeapon<T>(WeaponInfo<T> info)
        {
            if (!ItemInfoUtils.IsGun(info, out var gunInfo)) return;
            
            HeroFacade.Ammo.Setup(_weaponItem.MagazineData, gunInfo);
            HeroFacade.Recoil.Setup(gunInfo);
            var config = CurrentItem.WeaponConfiguration;
            HeroFacade.Attack.UpdateStatsAndConfig(gunInfo, config);
            HeroFacade.Reload.Setup(gunInfo, config);
            HeroFacade.Scoping.Setup(gunInfo, config);
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
            foreach (var itemName in _gunSpecs.DefaultAttachments) 
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