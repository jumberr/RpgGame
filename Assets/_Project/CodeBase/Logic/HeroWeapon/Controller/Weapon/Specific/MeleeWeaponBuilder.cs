using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class MeleeWeaponBuilder : BaseWeaponBuilder
    {
        private KnifeInfo _knifeInfo;
        public override Type[] SupportedTypes { get; } = {typeof(KnifeInfo)};



        protected override void PrepareData(CommonItemPart part, ItemInfo info) => 
            _knifeInfo = info as KnifeInfo;
        
        protected override void ConstructWeapon(ItemInfo info)
        {
            if (info is not KnifeInfo knifeInfo) return;
            
            HeroFacade.Ammo.Construct(knifeInfo);
            HeroFacade.Recoil.Setup(knifeInfo);
            CurrentItem.WeaponConfiguration.MeleeWeapon.Construct(HeroFacade.Attack.LayerMask, knifeInfo.GetSpecs().Damage);
            HeroFacade.Attack.ApplyKnife(knifeInfo);
            HeroFacade.Reload.Construct(knifeInfo);
            HeroFacade.Scoping.Construct(knifeInfo);
        }
    }
}