using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon.Data
{
    public class CurrentWeapon
    {
        public int SlotID { get; private set; } = -1;
        public GameObject WeaponGO { get; private set; }
        public WeaponConfiguration WeaponConfiguration { get; private set; }
        public GunInfo GunInfo { get; set; }
        public KnifeInfo KnifeInfo { get; set; }

        public void Create(int slotID, GameObject prefab, Transform parent)
        {
            SlotID = slotID;
            WeaponGO = Object.Instantiate(prefab, parent);
            WeaponConfiguration = WeaponGO.GetComponent<WeaponConfiguration>();
        }

        public void Destroy()
        {
            GunInfo = null;
            KnifeInfo = null;
            SlotID = -1;
            Object.Destroy(WeaponGO);
        }
    }
}