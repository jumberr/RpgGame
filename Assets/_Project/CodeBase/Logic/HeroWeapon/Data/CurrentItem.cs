using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Data
{
    public class CurrentItem
    {
        public int SlotID { get; private set; } = -1;
        public GameObject GameObject { get; private set; }
        public WeaponConfiguration WeaponConfiguration { get; private set; }
        public ItemInfo Info { get; set; }

        public void Create(int slotID, ItemInfo itemInfo, GameObject prefab, Transform parent)
        {
            SlotID = slotID;
            Info = itemInfo;
            GameObject = Object.Instantiate(prefab, parent);
            
            if (GameObject.TryGetComponent<WeaponConfiguration>(out var weaponConfiguration))
                WeaponConfiguration = weaponConfiguration;
        }

        public void Destroy()
        {
            Info = null;
            SlotID = -1;
            Object.Destroy(GameObject);
        }
    }
}