using System;
using _Project.CodeBase.Logic.HeroInventory;
using _Project.CodeBase.Logic.HeroWeapon;

namespace _Project.CodeBase.Utils.Extensions
{
    public static class AmmoTypeExtensions
    {
        public static ItemName ToItemName(this AmmoType ammoType)
        {
            return ammoType switch
            {
                AmmoType.Lmg => ItemName.LmgAmmo,
                AmmoType.Rifle => ItemName.RifleAmmo,
                AmmoType.Sniper => ItemName.SniperAmmo,
                AmmoType.Pistol => ItemName.PistolAmmo,
                AmmoType.Shotgun => ItemName.ShotgunAmmo,
                AmmoType.Flame => ItemName.FlameAmmo,
                AmmoType.Vog25 => ItemName.Vog25Ammo,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}