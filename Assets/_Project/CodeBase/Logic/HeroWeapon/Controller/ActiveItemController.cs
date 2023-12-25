using System;
using System.Linq;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class ActiveItemController : MonoBehaviour
    {
        [SerializeField] private HeroFacade _facade;
        [SerializeField] private HeroInventory _inventory;
        [SerializeField] private Transform _parent;
        
        private IItemHandler[] _handlers;
        private WeaponItem _weaponItem;
        private IItemHandler _currentHandler;

        public event Action<bool> OnSwitch;
        
        public CurrentItem CurrentItem => _currentHandler?.CurrentItem;
        
        
        [Inject]
        private void Construct(IStaticDataService staticDataService, IItemHandler[] handlers)
        {
            _handlers = handlers;
            
            foreach (var itemHandler in _handlers)
                itemHandler.Init(_facade, staticDataService.ForInventory(), _parent);
        }
        
        private void OnEnable() => 
            _inventory.OnUpdate += InventoryUpdate;

        private void OnDisable() => 
            _inventory.OnUpdate -= InventoryUpdate;

        public async UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, ItemType type, GameObject prefab = null)
        {
            if (_currentHandler != null)
            {
                _currentHandler.Destroy();
                OnSwitch?.Invoke(false);
            }

            _currentHandler = GetItemHandler(type);
            await _currentHandler.CreateNewItem(slotID, part, info, prefab);
            OnSwitch?.Invoke(true);
        }

        private void InventoryUpdate() => 
            _currentHandler?.InventoryUpdate();

        //public async UniTask CreateNewMeleeWeapon(GameObject prefab, KnifeInfo knifeInfo, int slotID)
        //{
        //    await CreateGun(slotID, prefab);
        //    ConstructWeapon(knifeInfo);
        //    //OnSwitch?.Invoke(true); // todo temporary here, but better refactor structure
        //}
        //
        //private void ConstructWeapon(KnifeInfo knifeInfo)
        //{
        //    _currentWeapon.KnifeInfo = knifeInfo;
        //    
        //    _ammo.Construct(knifeInfo);
        //    _recoil.Construct(knifeInfo);
        //    SetupKnife(knifeInfo);
        //}
//
        //private void SetupKnife(KnifeInfo knifeInfo)
        //{
        //    _currentWeapon.WeaponConfiguration.MeleeWeapon.Construct(_attack.LayerMask, knifeInfo.WeaponSpecs.Damage);
        //    _attack.ApplyKnife(knifeInfo);
        //    _reload.Construct(knifeInfo);
        //    _scoping.Construct(knifeInfo);
        //}
        
        private IItemHandler GetItemHandler(ItemType type) => 
            _handlers.FirstOrDefault(itemHandler => itemHandler.ItemType == type);
    }
}