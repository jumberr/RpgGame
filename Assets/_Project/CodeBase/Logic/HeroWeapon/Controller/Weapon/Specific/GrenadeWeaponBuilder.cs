using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class GrenadeWeaponBuilder : BaseWeaponBuilder
    {
        private readonly ItemThrower _itemThrower;
        private WeaponInfo<GrenadeSpecs> _grenadeInfo;
        private GrenadeSpecs _grenadeSpecs;
        public override Type[] SupportedTypes { get; } = {typeof(GrenadeInfo), typeof(ExplosiveGrenadeInfo)};


        public GrenadeWeaponBuilder(ItemThrower itemThrower) => 
            _itemThrower = itemThrower;

        protected override void PrepareData(CommonItemPart part, ItemInfo info)
        {
            if (!ItemInfoUtils.IsGrenade(info, out var grenadeInfo)) return;
            // todo
            
        }

        protected override void ConstructWeapon<T>(WeaponInfo<T> info)
        {
            if (!ItemInfoUtils.IsGrenade(info, out var grenadeInfo)) return;
            
            _itemThrower.SetupItem(CurrentItem, grenadeInfo);
        }
    }
}