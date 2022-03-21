using System;

namespace _Project.CodeBase.Logic.Weapon
{
    [Serializable]
    public class Weapon
    {
        public bool IsAutomatic;
        public float Damage;
        public float ReloadTime;
        public float Range;
        public float FireRate;
        public float Spread;
    }
}