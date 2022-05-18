using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class InteractableSpawner : MonoBehaviour
    {
        private HeroInventory _heroInventory;
        
        public void Construct(HeroInventory heroInventory)
        {
            _heroInventory = heroInventory;
            InitializeOnStart();
        }

        public InteractableGroundItem SpawnInteractableItem(GameObject prefab, Vector3 position)
        {
            var item = Instantiate(prefab, position, Quaternion.identity);
            return item.GetComponent<InteractableGroundItem>();
        }

        public void ConstructItem(InteractableGroundItem item, int amount) => 
            item.Construct(_heroInventory, item.GetComponent<ItemGround>(), amount);
        
        public void ConstructItem(InteractableGroundItem item) => 
            item.Construct(_heroInventory, item.GetComponent<ItemGround>());

        private void InitializeOnStart()
        {
            var items = FindObjectsOfType<InteractableGroundItem>();
            foreach (var item in items) 
                ConstructItem(item);
        }
    }
}