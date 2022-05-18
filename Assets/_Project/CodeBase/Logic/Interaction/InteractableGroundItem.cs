using _Project.CodeBase.Logic.Inventory;

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
            _inventory.SetItemInFreeSlot(_item.DbID, _item.Amount);
            Destroy(gameObject);
        }
    }
}