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
    public abstract class BaseItemHandler : IItemHandler
    {
        protected HeroAttack Attack;
        protected HeroAmmo Ammo;
        private HeroInventory _inventory;
        
        public abstract CurrentItem CurrentItem { get; }
        public abstract ItemType ItemType { get; }


        public virtual void Init(HeroFacade facade, ItemsInfo itemsInfo, Transform parent)
        {
            Attack = facade.Attack;
            Ammo = facade.Ammo;
            _inventory = facade.Inventory;
        }

        public abstract UniTask CreateNewItem(int slotID, CommonItemPart part, ItemInfo info, GameObject prefab); // todo prefab could be null
        
        public async void InventoryUpdate()
        {
            if (SlotIsEmpty()) return;
            
            await CheckIfDestroy(CurrentItem.Info);
        }

        public async UniTask Destroy()
        {
            if (SlotIsEmpty()) return;
            await DestroyItem();
        }

        protected abstract UniTask DestroyItem();

        private async UniTask CheckIfDestroy(ItemInfo itemInfo)
        {
            if (itemInfo != null && _inventory.FindFirstItemIndex(itemInfo.UIInfo.Name) == Logic.Inventory.Inventory.ErrorIndex)
                await Destroy();
        }

        private bool SlotIsEmpty() => 
            CurrentItem?.SlotID == Logic.Inventory.Inventory.ErrorIndex;
    }
}