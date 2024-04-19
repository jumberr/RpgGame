using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public abstract class BaseWeaponBuilder : IWeaponBuilder
    {
        protected HeroFacade HeroFacade;
        protected ItemsInfo ItemsInfo;
        private Transform _parent;

        public CurrentItem CurrentItem { get; } = new CurrentItem();
        public abstract Type SupportedType { get; }

        
        public void Init(HeroFacade heroFacade, ItemsInfo itemsInfo, Transform parent)
        {
            ItemsInfo = itemsInfo;
            HeroFacade = heroFacade;
            _parent = parent;
        }

        public virtual async UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, GameObject prefab)
        {
            PrepareData(part, info);
            await CreateWeapon(slotID, info, prefab);

            if (ItemInfoUtils.IsKnife(info, out var knife))
            {
                ConstructWeapon(knife);
                return;
            }

            if (ItemInfoUtils.IsGun(info, out var gun))
                ConstructWeapon(gun);
        }
        
        public async UniTask AnimatorHideWeapon()
        {
            await HeroFacade.Animator.HideWeapon();
            HeroFacade.Animator.SetEmptyAnimator();
        }

        public virtual void FinishItemDestroy() => 
            CurrentItem?.Destroy();

        protected abstract void PrepareData(CommonItemPart part, ItemInfo info);
        protected abstract void ConstructWeapon<T>(WeaponInfo<T> info) where T : WeaponSpecs;

        private async UniTask CreateWeapon(int slotID, ItemInfo info, GameObject prefab)
        {
            if (CurrentItem.SlotID == slotID) return;

            CurrentItem.Create(slotID, info, prefab, _parent);
            if (CurrentItem.WeaponConfiguration == null) return;
            
            HeroFacade.Animator.Construct(CurrentItem.WeaponConfiguration.HandsAnimator);
            await HeroFacade.Animator.ShowWeaponOnInit();
        }
    }
}