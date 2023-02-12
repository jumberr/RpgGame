using System;

namespace _Project.CodeBase.Logic
{
    [Serializable]
    public class WeaponItem : BaseItem
    {
        public MagazineData MagazineData;
        public WeaponData WeaponData;

        public WeaponItem(MagazineData magazineData, WeaponData weaponData)
        {
            MagazineData = magazineData;
            WeaponData = weaponData;
        }
    }
}