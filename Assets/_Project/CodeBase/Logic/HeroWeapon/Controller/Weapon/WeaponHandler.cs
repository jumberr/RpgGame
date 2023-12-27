using System.Linq;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponHandler : BaseItemHandler
    {
        private readonly IWeaponBuilder[] _builders;
        private WeaponType _currentType;

        public override CurrentItem CurrentItem => GetBuilder(_currentType)?.CurrentItem;
        public override ItemType ItemType => ItemType.Weapon;


        public WeaponHandler(IWeaponBuilder[] builders)
        {
            _builders = builders;
        }

        public override void Init(HeroFacade facade, ItemsInfo itemsInfo, Transform parent)
        {
            base.Init(facade, itemsInfo, parent);
            foreach (var builder in _builders)
                builder.Init(facade, itemsInfo, parent);
        }

        public override async UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, GameObject prefab)
        {
            _currentType = (info as BaseWeaponInfo).WeaponType;
            var builder = GetBuilder(_currentType);
            if (builder == null) return;
            
            await builder.CreateNewItem(slotID, part, info, prefab);
        }

        protected override async UniTask DestroyItem()
        {
            Ammo.HideUI();
            
            var builder = GetBuilder(_currentType);
            if (builder != null)
            {
                await builder.AnimatorHideWeapon();
                Attack.SetWeaponTypeNone();
                builder.FinishItemDestroy();
            }
        }

        private IWeaponBuilder GetBuilder(WeaponType type) => 
            _builders.FirstOrDefault(x => x.Type == type);
    }
}