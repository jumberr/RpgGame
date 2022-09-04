using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class InteractableGroundItem : InteractableBase
    {
        private HeroInventory _inventory;
        private ItemGround _item;

        public void Construct(HeroInventory inventory, ItemGround item)
        {
            _inventory = inventory;
            _item = item;
            
            _item.Construct(inventory.ItemsDataBase);
        }

        public void Construct(HeroInventory inventory, ItemGround item, int amount)
        {
            _inventory = inventory;
            _item = item;
            
            _item.Construct(inventory.ItemsDataBase, amount);
        }

        public override void OnInteract()
        {
            var payloadData = _inventory.ItemsDataBase.FindItem(_item.DbID).ItemPayloadData;
            var amountNotStored = _inventory.AddItemWithReturnAmount(_item.DbID, payloadData, _item.Amount);
            
            if (amountNotStored == 0)
                Destroy(gameObject);
            else
            {
                _item.UpdateAmount(amountNotStored);
                Debug.Log("Inventory is full!");
            }
        }
    }
}