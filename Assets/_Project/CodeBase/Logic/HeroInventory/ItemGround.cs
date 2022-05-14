using _Project.CodeBase.Constants;
using _Project.CodeBase.StaticData.ItemsDataBase;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroInventory
{
    public class ItemGround : MonoBehaviour
    {
        public int Amount;
        [SerializeField] private ItemName _itemName;
        private int _dbID = -1;

        public void Construct(int dbID, int amount)
        {
            _dbID = dbID;
            Amount = amount;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagConstants.Player))
            {
                var inventory = other.GetComponent<HeroInventory>();
                TryConvertNameToId(inventory.ItemsDataBase);
                inventory.SetItemInFreeSlot(_dbID, Amount);
                Debug.Log($"Item {_dbID} added");
                Destroy(gameObject);
            }
        }

        private void TryConvertNameToId(ItemsDataBase db)
        {
            if (_itemName != ItemName.None) 
                _dbID = db.FindItem(_itemName).ItemPayloadData.DbId;
        }
    }
}