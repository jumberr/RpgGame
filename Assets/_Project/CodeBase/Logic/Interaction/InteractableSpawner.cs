using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Data;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class InteractableSpawner : MonoBehaviour
    {
        private List<InteractableGroundItem> _items = new List<InteractableGroundItem>();

        private HeroInventory _heroInventory;
        private ItemsInfo _itemsInfo;

        private void OnValidate() => 
            _items = FindObjectsOfType<InteractableGroundItem>().ToList();

        public void OnDestroy()
        {
            _heroInventory.OnSpawnItemAtMap -= SpawnItemAtMap;
            _heroInventory.OnItemPickup -= ItemPickup;
        }

        public void Setup(HeroInventory heroInventory)
        {
            _heroInventory = heroInventory;
            _itemsInfo = _heroInventory.ItemsInfo;
            _heroInventory.OnSpawnItemAtMap += SpawnItemAtMap;
            _heroInventory.OnItemPickup += ItemPickup;
        }

        public void InitializeOnStart(List<ItemData> savedItems)
        {
            if (savedItems.Count == 0)
                SetupFirstTime(_items);
            else
            {
                DestroyPlacedGO();
                SpawnFromSave(savedItems);
            }
        }

        public ItemsData SaveItems()
        {
            var itemsData = new ItemsData();
            foreach (var interactable in _items)
            {
                var groundItem = interactable.ItemGround;
                var objectData = new GameObjectData(interactable.transform.position.AsVectorData(), interactable.transform.rotation.AsRotationData());
                itemsData.AddData(new ItemData(groundItem.DbID, groundItem.Amount, groundItem.Item, objectData));
            }

            return itemsData;
        }

        private void SetupFirstTime(List<InteractableGroundItem> items)
        {
            foreach (var item in items)
                ConstructItem(item, null, item.ItemGround.Amount);

            _items = items;
        }

        private void DestroyPlacedGO()
        {
            foreach (var item in _items) 
                DestroyGO(item);
            _items.Clear();
        }

        private void SpawnFromSave(List<ItemData> savedItems)
        {
            foreach (var savedItem in savedItems)
            {
                var part = savedItem.CommonItemPart;

                //if (_itemsInfo.FindItem(part.DbId).PayloadInfo.ItemType == ItemType.Weapon)
                //{
                //    part.Item = (WeaponItem) part.Item;
                //}

                CreateObject(part.DbId, part.Amount, part.Item, savedItem.GameObjectData.Position.AsUnityVector(),
                    savedItem.GameObjectData.Rotation.AsQuaternion());
            }
        }

        private void SpawnItemAtMap(CommonItemPart part) => 
            CreateObject(part.DbId, part.Amount, part.Item, _heroInventory.transform.position + Vector3.one, Quaternion.identity);

        private void CreateObject(int id, int amount, BaseItem data, Vector3 pos, Quaternion rotation)
        {
            var item = SpawnInteractableItem(_itemsInfo.FindItem(id).PayloadInfo.GroundPrefab, pos, rotation);
            ConstructItem(item, data, amount);
            _items.Add(item);
        }

        private InteractableGroundItem SpawnInteractableItem(InteractableGroundItem prefab, Vector3 position, Quaternion rotation) => 
            Instantiate(prefab, position, rotation, transform);

        private void ConstructItem(InteractableGroundItem item, BaseItem data, int amount) => 
            item.Construct(_heroInventory, data, amount);

        private void ItemPickup(InventorySlot slot, InteractableGroundItem interactable)
        {
            if (!_items.Contains(interactable)) return;
            var data = interactable.ItemGround.Item;
            _heroInventory.SetItemInSlot(slot, data);
            RemoveAndDestroy(interactable);
        }

        private void RemoveAndDestroy(InteractableGroundItem interactable)
        {
            _items.Remove(interactable);
            DestroyGO(interactable);
        }

        private void DestroyGO(InteractableGroundItem interactable) => 
            Destroy(interactable.gameObject);
    }
}