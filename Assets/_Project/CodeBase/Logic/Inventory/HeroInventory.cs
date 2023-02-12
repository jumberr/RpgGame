using System;
using _Project.CodeBase.Data;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.Inventory
{
    public class HeroInventory : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private WeaponController _weaponController;
        
        private IStaticDataService _staticDataService;
        private ItemsInfo _itemsInfo;
        private Inventory _inventory;

        public event Action OnUpdate;
        public event Action<int> OnDrop;
        public event Action<InventorySlot, InteractableGroundItem> OnItemPickup;
        public event Action<CommonItemPart> OnSpawnItemAtMap;

        public ItemsInfo ItemsInfo => _itemsInfo;
        public Inventory Inventory => _inventory;
        private int ErrorIndex => Inventory.ErrorIndex;

        public void Construct(ItemsInfo itemsInfo) => 
            _itemsInfo = itemsInfo;

        public InventorySlot GetSlot(int index) => 
            Inventory.Slots[index];

        public void LoadProgress(PlayerProgress progress) => 
            _inventory = progress.Inventory;

        public void UpdateProgress(PlayerProgress progress) => 
            progress.Inventory = _inventory;

        public void EquipItem(int slotID)
        {
            var slot = _inventory.Slots[slotID];
            var info = _itemsInfo.FindItem(slot.ID);
            
            if (info is GunInfo weapon) 
                EquipWeapon(slotID, slot.CommonItemPart, weapon);
            if (info is KnifeInfo knife) 
                EquipWeapon(knife, slotID);
            //if (item is ArmorInfo armor) 
            //    EquipArmor(armor);
        }

        public void AddItem(InteractableGroundItem interactable)
        {
            var groundItem = interactable.ItemGround;
            
            var payloadData = _itemsInfo.FindItem(groundItem.DbID).PayloadInfo;
            var amountNotStored = _inventory.AddItemWithReturnAmount(groundItem.DbID, payloadData, groundItem.Amount);
            if (amountNotStored == 0)
            {
                interactable.SetPickUpStatus(true);
                var slot = _inventory.FindSlot(groundItem.DbID, groundItem.Amount);
                OnItemPickup?.Invoke(slot, interactable);
            }
            else
            {
                groundItem.UpdateAmount(amountNotStored);
                Debug.Log("Inventory is full!");
            }

            OnUpdate?.Invoke();
        }

        public void RemoveItem(ItemName itemName, int amount)
        {
            _inventory.RemoveItemFromInventory(ItemsInfo.FindIndex(itemName), amount);
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
            var slot = _inventory.Slots[slotId];
            SpawnGroundItem(new CommonItemPart(slot.ID, 1, slot.CommonItemPart.Item));
            RemoveItemFromSlot(slotId);
        }

        public void DropAllItemsFromSlot(int slotId)
        {
            SpawnGroundItem(_inventory.Slots[slotId].CommonItemPart);
            RemoveAllItemsFromSlot(slotId);
        }

        public void SetItemInSlot(InventorySlot slot, BaseItem data) => 
            slot.CommonItemPart.Item = data;

        public int FindItemAmount(ItemName itemName)
        {
            var count = 0;
            var dbIndex = _itemsInfo.FindIndex(itemName);
            foreach (var slot in _inventory.Slots)
                if (slot.ID == dbIndex) 
                    count += slot.CommonItemPart.Amount;
            return count;
        }

        public int FindFirstItemIndex(ItemName itemName)
        {
            var dbIndex = _itemsInfo.FindIndex(itemName);
            foreach (var slot in _inventory.Slots)
                if (slot.ID == dbIndex)
                    return slot.ID;
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
            InvokeRemoveEvents(id);
        }

        private void RemoveAllItemsFromSlot(int id)
        {
            _inventory.RemoveAllItemsFromSlot(id);
            InvokeRemoveEvents(id);
        }

        private void InvokeRemoveEvents(int id)
        {
            OnUpdate?.Invoke();
            OnDrop?.Invoke(id);
        }

        private void SpawnGroundItem(CommonItemPart part) => 
            OnSpawnItemAtMap?.Invoke(part);

        private async void EquipWeapon(int slotID, CommonItemPart item, GunInfo gunInfo) => 
            await _weaponController.CreateNewWeapon(slotID, item, gunInfo, gunInfo.Prefab);

        private async void EquipWeapon(KnifeInfo knifeInfo, int slotID) => 
            await _weaponController.CreateNewMeleeWeapon(knifeInfo.Prefab, knifeInfo, slotID);

        private void EquipArmor(ArmorInfo armorInfo)
        {
            
        }
    }
}