using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class InteractableGroundItem : InteractableBase
    {
        private HeroInventory _inventory;
        private ItemGround _item;
        private bool _alreadyPickedUp;

        public void Construct(HeroInventory inventory, ItemGround item)
        {
            Setup(inventory, item);
            _item.Construct(inventory.ItemsDataBase);
        }

        public void Construct(HeroInventory inventory, ItemGround item, int amount)
        {
            Setup(inventory, item);
            _item.Construct(inventory.ItemsDataBase, amount);
        }

        private void Setup(HeroInventory inventory, ItemGround item)
        {
            _inventory = inventory;
            _item = item;
            SetPickUpStatus(false);
        }

        public override void OnInteract()
        {
            if (_alreadyPickedUp) return;
            
            var payloadData = _inventory.ItemsDataBase.FindItem(_item.DbID).ItemPayloadData;
            var amountNotStored = _inventory.AddItemWithReturnAmount(_item.DbID, payloadData, _item.Amount);

            if (amountNotStored == 0)
            {
                Destroy(gameObject);
                SetPickUpStatus(true);
            }
            else
            {
                _item.UpdateAmount(amountNotStored);
                Debug.Log("Inventory is full!");
            }
        }

        private void SetPickUpStatus(bool value) => 
            _alreadyPickedUp = value;
    }
}