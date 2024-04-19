using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Utils
{
    public static class ItemInfoUtils
    {
        public static bool IsAnyWeapon(ItemInfo info)
        {
            if (IsKnife(info, out var knifeInfo)) return true;
            if (IsGun(info, out var gunInfo)) return true;
            if (IsRevolver(info, out var revolverSpecs)) return true;
            return false;
        }

        public static bool IsRangedWeapon(ItemInfo info) => 
            IsGun(info, out _) || IsRevolver(info, out _);

        public static bool IsRevolver(ItemInfo info, out WeaponInfo<RevolverSpecs> result) =>
            (result = info as WeaponInfo<RevolverSpecs>) is not null;

        public static bool IsGun(ItemInfo info, out WeaponInfo<GunSpecs> result) =>
            (result = info as WeaponInfo<GunSpecs>) is not null;

        public static bool IsKnife(ItemInfo info, out WeaponInfo<WeaponSpecs> result) => 
            (result = info as WeaponInfo<WeaponSpecs>) is not null;
    }
}