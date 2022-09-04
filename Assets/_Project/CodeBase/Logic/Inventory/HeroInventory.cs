using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.StaticData.ItemsDataBase;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using UnityEngine;

namespace _Project.CodeBase.Logic.Inventory
{
    public class HeroInventory : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private WeaponController _weaponController;
        
        private IStaticDataService _staticDataService;
        private ItemsDataBase _itemsDataBase;
        private Inventory _inventory;
        private InteractableSpawner _interactableSpawner;
        
        public event Action OnUpdate;

        public ItemsDataBase ItemsDataBase => _itemsDataBase;
        public Inventory Inventory => _inventory;
        public int ErrorIndex => Inventory.ErrorIndex;

        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _itemsDataBase = _staticDataService.ForInventory();
        }
        
        public void Construct(InteractableSpawner interactableSpawner) => 
            _interactableSpawner = interactableSpawner;
        
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
            if (item is Knife knife) 
                EquipWeapon(knife, slotID);
            if (item is Armor armor) 
                EquipArmor(armor);
        }

        public int AddItemWithReturnAmount(int dbID, ItemPayloadData data, int amount)
        {
            var amountNotStored = _inventory.AddItemWithReturnAmount(dbID, data, amount);
            OnUpdate?.Invoke();
            return amountNotStored;
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
            if (index == ErrorIndex) return;
            RemoveItemFromSlot(index);
        }
        
        public void DropItemFromSlot(int slotId)
        {
            SpawnGroundItem(_inventory.Slots[slotId].DbId, 1);
            RemoveItemFromSlot(slotId);
        }

        public void DropAllItemsFromSlot(int slotId)
        {
            var slot = _inventory.Slots[slotId];
            SpawnGroundItem(slot.DbId, slot.Amount);
            RemoveAllItemsFromSlot(slotId);
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
            return ErrorIndex;
        }
        
        public int FindIndex(InventorySlot slot) => 
            slot != null 
                ? Array.IndexOf(Inventory.Slots, slot) 
                : ErrorIndex;

        public void SwapSlots(int one, int two) => 
            _inventory.SwapSlots(one, two);

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
            var obj = _interactableSpawner.SpawnInteractableItem(prefab, transform.position + Vector3.forward);
            _interactableSpawner.ConstructItem(obj, amount);
        }

        private async void EquipWeapon(Weapon weapon, int slotID) => 
            await _weaponController.CreateNewWeapon(weapon.WeaponPrefab, weapon, slotID);

        private async void EquipWeapon(Knife weapon, int slotID) => 
            await _weaponController.CreateNewMeleeWeapon(weapon.Prefab, weapon, slotID);

        private void EquipArmor(Armor armor)
        {
            
        }
    }
}