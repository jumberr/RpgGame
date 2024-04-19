using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Utils
{
    public static class ItemInfoUtils
    {
        public static bool IsRangedWeapon<T>(WeaponInfo<T> info) where T : WeaponSpecs => 
            IsGun(info, out _) || IsRevolver(info, out _);

        public static bool IsRevolver<T>(WeaponInfo<T> info, out WeaponInfo<RevolverSpecs> result) where T : WeaponSpecs =>
            (result = info as WeaponInfo<RevolverSpecs>) is not null;

        public static bool IsGun<T>(WeaponInfo<T> info, out WeaponInfo<GunSpecs> result) where T : WeaponSpecs =>
            (result = info as WeaponInfo<GunSpecs>) is not null;

        public static bool IsKnife<T>(WeaponInfo<T> info) where T : WeaponSpecs => 
            info is WeaponInfo<WeaponSpecs>;
    }
}