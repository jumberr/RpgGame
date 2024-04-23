using System;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public abstract class WeaponInfo<TSpecs> : EquippableInfo where TSpecs : WeaponSpecs
    {
        public abstract TSpecs GetSpecs();
    }
}