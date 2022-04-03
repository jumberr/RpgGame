using _Project.CodeBase.Constants;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroInventory
{
    public class ItemGround : MonoBehaviour
    {
        [SerializeField] private int _dbID;
        [SerializeField] private int _amount;
        
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