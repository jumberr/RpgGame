using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.StaticData.ItemsDataBase;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroInventory
{
    public class HeroInventory : MonoBehaviour, ISavedProgress
    {
        public event Action OnUpdate;

        private IStaticDataService _staticDataService;
        private ItemsDataBase _itemsDataBase;
        private Inventory _inventory;
        
        public ItemsDataBase ItemsDataBase => _itemsDataBase;
        public Inventory Inventory => _inventory;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _itemsDataBase = _staticDataService.ForInventory();
        }

        public void LoadProgress(PlayerProgress progress) => 
            _inventory = progress.Inventory;

        public void UpdateProgress(PlayerProgress progress) => 
            progress.Inventory = _inventory;

        public void SetItemInFreeSlot(int dbID, int amount)
        {
            var item = ItemsDataBase.FindItemByIndex(dbID).ItemPayloadData;
            _inventory.AddItemToInventory(dbID, item, amount);
            OnUpdate?.Invoke();
        }

        public void RemoveItemFromSlot(int id)
        {
            _inventory.RemoveItemFromSlot(id);
            OnUpdate?.Invoke();
        }
    }
}