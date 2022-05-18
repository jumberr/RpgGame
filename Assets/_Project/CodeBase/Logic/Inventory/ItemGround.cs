using _Project.CodeBase.StaticData.ItemsDataBase;
using UnityEngine;

namespace _Project.CodeBase.Logic.Inventory
{
    public class ItemGround : MonoBehaviour
    {
        public int Amount;
        [SerializeField] private ItemName _itemName;
        public int DbID { get; set; } = -1;

        public void Construct(ItemsDataBase db) => 
            DbID = TryConvertNameToId(db, _itemName);

        public void Construct(ItemsDataBase db, int amount)
        {
            Construct(db);
            Amount = amount;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag(TagConstants.Player))
        //    {
        //        var inventory = other.GetComponent<HeroInventory>();
        //        TryConvertNameToId(inventory.ItemsDataBase);
        //        inventory.SetItemInFreeSlot(_dbID, Amount);
        //        Debug.Log($"Item {_dbID} added");
        //        Destroy(gameObject);
        //    }
        //}
        
        private int TryConvertNameToId(ItemsDataBase db, ItemName itemName)
        {
            if (itemName != ItemName.None) 
                return db.FindItem(itemName).ItemPayloadData.DbId;
            return -1;
        }
    }
}