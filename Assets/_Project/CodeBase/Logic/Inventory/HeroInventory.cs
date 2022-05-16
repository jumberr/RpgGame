using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData.ItemsDataBase;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using UnityEngine;

namespace _Project.CodeBase.Logic.Inventory
{
    public class HeroInventory : MonoBehaviour, ISavedProgress
    {
        public event Action OnUpdate;

        [SerializeField] private WeaponController _weaponController;
        
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

        public InventorySlot GetSlot(int index) => 
            Inventory.Slots[index];

        public void LoadProgress(PlayerProgress progress) => 
            _inventory = progress.Inventory;

        public void UpdateProgress(PlayerProgress progress) => 
            progress.Inventory = _inventory;

        public void EquipItem(int slotID)
        {
            var dbId = _inventory.Slots[slotID].DbId;
            var item = _itemsDataBase.FindItem(dbId);
            if (item is Weapon weapon) 
                EquipWeapon(weapon, slotID);
            if (item is Armor armor) 
                EquipArmor(armor);
        }

        public void SetItemInFreeSlot(int dbID, int amount)
        {
            var item = ItemsDataBase.FindItem(dbID).ItemPayloadData;
            _inventory.AddItemToInventory(dbID, item, amount);
            OnUpdate?.Invoke();
        }
        
        public void RemoveItem(ItemName itemName, int amount) => 
            RemoveItem(ItemsDataBase.FindIndex(itemName), amount);

        public void RemoveItem(int dbID, int amount)
        {
            _inventory.RemoveItemFromInventory(dbID, amount);
            OnUpdate?.Invoke();
        }

        public void RemoveItemFromSlot(ItemName itemName)
        {
            var index = FindFirstItemIndex(itemName);
            if (index == -1) return;
            RemoveItemFromSlot(index);
        }
        
        public void DropItemFromSlot(int id)
        {
            SpawnGroundItem(_inventory.Slots[id].DbId, 1);
            RemoveItemFromSlot(id);
        }

        public void DropAllItemsFromSlot(int id)
        {
            var slot = _inventory.Slots[id];
            SpawnGroundItem(slot.DbId, slot.Amount);
            RemoveAllItemsFromSlot(id);
        }

        public int FindItemAmount(ItemName itemName)
        {
            var count = 0;
            var dbIndex = _itemsDataBase.FindIndex(itemName);
            foreach (var slot in _inventory.Slots)
                if (slot.DbId == dbIndex) 
                    count += slot.Amount;
            return count;
        }

        public int FindFirstItemIndex(ItemName itemName)
        {
            var dbIndex = _itemsDataBase.FindIndex(itemName);
            foreach (var slot in _inventory.Slots)
                if (slot.DbId == dbIndex)
                    return slot.DbId;
            return -1;
        }

        private void RemoveItemFromSlot(int id)
        {
            _inventory.RemoveItemFromSlot(id);
            OnUpdate?.Invoke();
        }

        private void RemoveAllItemsFromSlot(int id)
        {
            _inventory.RemoveAllItemsFromSlot(id);
            OnUpdate?.Invoke();
        }

        private void SpawnGroundItem(int dbId, int amount)
        {
            var prefab = _itemsDataBase.FindItem(dbId).ItemPayloadData.GroundPrefab;
            var obj = Instantiate(prefab, transform.position + Vector3.forward, Quaternion.identity);
            obj.GetComponent<ItemGround>().Construct(dbId, amount);
        }
        
        private async void EquipWeapon(Weapon weapon, int slotID) => 
            await _weaponController.CreateNewWeapon(weapon.WeaponPrefab, weapon, slotID);

        private void EquipArmor(Armor armor)
        {
            
        }
    }
}