using System;
using System.Linq;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class WeaponHandler : BaseItemHandler
    {
        private readonly IWeaponBuilder[] _builders;
        private IWeaponBuilder _builder;

        public override CurrentItem CurrentItem => _builder?.CurrentItem;
        public override Type[] SupportedTypes { get; protected set; }


        public WeaponHandler(IWeaponBuilder[] builders) => 
            _builders = builders;

        public override void Init(HeroFacade facade, ItemsInfo itemsInfo, Transform parent)
        {
            base.Init(facade, itemsInfo, parent);
            foreach (var builder in _builders)
                builder.Init(facade, itemsInfo, parent);
            SupportedTypes = _builders.Select(x => x.SupportedType).ToArray();
        }

        public override async UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, GameObject prefab)
        {
            _builder = GetBuilder(info.GetType()); // todo fix item type
            if (_builder == null) return;
            
            await _builder.CreateNewItem(slotID, part, info, prefab);
        }

        protected override async UniTask DestroyItem()
        {
            Ammo.HideUI();
            
            if (_builder == null) return;

            await _builder.AnimatorHideWeapon();
            Attack.SetWeaponTypeNone();
            _builder.FinishItemDestroy();
        }

        private IWeaponBuilder GetBuilder(Type type) => 
            _builders.FirstOrDefault(x => x.SupportedType == type);
    }
}