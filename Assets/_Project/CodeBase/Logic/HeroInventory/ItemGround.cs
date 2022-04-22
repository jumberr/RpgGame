using _Project.CodeBase.Constants;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroInventory
{
    public class ItemGround : MonoBehaviour
    {
        private int _dbID;
        private int _amount = 1;

        public void Construct(int dbID, int amount)
        {
            _dbID = dbID;
            _amount = amount;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagConstants.Player))
            {
                var inventory = other.GetComponent<HeroInventory>();
                inventory.SetItemInFreeSlot(_dbID, _amount);
                
                Debug.Log($"Item {_dbID} added");
                Destroy(gameObject);
            }
        }
    }
}