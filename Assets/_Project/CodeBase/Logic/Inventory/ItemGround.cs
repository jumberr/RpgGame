using _Project.CodeBase.StaticData.ItemsDataBase;
using UnityEngine;

namespace _Project.CodeBase.Logic.Inventory
{
    public class ItemGround : MonoBehaviour
    {
        public int Amount;
        [SerializeField] private ItemName _itemName;
        public int DbID { get; private set; } = -1;

        public void Construct(ItemsDataBase db) => 
            DbID = TryConvertNameToId(db, _itemName);

        public void Construct(ItemsDataBase db, int amount)
        {
            Construct(db);
            Amount = amount;
        }
        
        public void UpdateAmount(int amount) => 
            Amount = amount;

        private int TryConvertNameToId(ItemsDataBase db, ItemName itemName)
        {
            if (itemName != ItemName.None)
            {
                var findItem = db.FindItem(itemName);
                return findItem.ItemPayloadData.DbId;
            }

            return -1;
        }
    }
}