using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.Logic.Interaction
{
    public class InteractableSpawner : MonoBehaviour
    {
        private List<InteractableGroundItem> _items = new List<InteractableGroundItem>();
        private HeroInventory _heroInventory;

        public void Construct(HeroInventory heroInventory)
        {
            _heroInventory = heroInventory;
            _heroInventory.OnSpawn += SpawnAndConstruct;
            InitializeOnStart();
        }

        private void InitializeOnStart()
        {
            var items = FindObjectsOfType<InteractableGroundItem>().ToList();
            foreach (var item in items)
                ConstructItem(item);

            _items = items;
        }

        private void SpawnAndConstruct(GameObject prefab, int amount)
        {
            var obj = SpawnInteractableItem(prefab, _heroInventory.transform.position + Vector3.forward);
            ConstructItem(obj, amount);
        }

        private InteractableGroundItem SpawnInteractableItem(GameObject prefab, Vector3 position)
        {
            var item = Instantiate(prefab, position, Quaternion.identity);
            return item.GetComponent<InteractableGroundItem>();
        }

        private void ConstructItem(InteractableGroundItem item, int amount) => 
            item.Construct(_heroInventory, item.GetComponent<ItemGround>(), amount);

        private void ConstructItem(InteractableGroundItem item) => 
            item.Construct(_heroInventory, item.GetComponent<ItemGround>());
    }
}