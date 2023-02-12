using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class InteractableGroundItem : InteractableBase
    {
        [SerializeField] private ItemGround _itemGround;
        
        private HeroInventory _inventory;
        private bool _alreadyPickedUp;

        public ItemGround ItemGround => _itemGround;
        
        public void Construct(HeroInventory inventory, BaseItem data, int amount)
        {
            _inventory = inventory;
            _itemGround.Construct(inventory.ItemsInfo, data, amount);
            SetPickUpStatus(false);
        }

        public override void OnInteract()
        {
            if (_alreadyPickedUp) return;
            _inventory.AddItem(this);
        }

        public void SetPickUpStatus(bool value) => 
            _alreadyPickedUp = value;
    }
}