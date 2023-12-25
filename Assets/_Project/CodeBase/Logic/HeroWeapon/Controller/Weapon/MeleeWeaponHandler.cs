using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class MeleeWeaponHandler : BaseWeaponBuilder
    {
        private KnifeInfo _knifeInfo;
        public override WeaponType Type => WeaponType.Knife;


        public override void FinishItemDestroy()
        {
        }

        protected override void PrepareData(CommonItemPart part, ItemInfo info) => 
            _knifeInfo = info as KnifeInfo;
        
        protected override void ConstructWeapon<T>(T info)
        {
            base.ConstructWeapon(info);
            
            if (info is not KnifeInfo knifeInfo) return;
            
            HeroFacade.Ammo.Construct(knifeInfo);
            HeroFacade.Recoil.Construct(knifeInfo);
            CurrentItem.WeaponConfiguration.MeleeWeapon.Construct(HeroFacade.Attack.LayerMask, knifeInfo.WeaponSpecs.Damage);
            HeroFacade.Attack.ApplyKnife(knifeInfo);
            HeroFacade.Reload.Construct(knifeInfo);
            HeroFacade.Scoping.Construct(knifeInfo);
        }
    }
}