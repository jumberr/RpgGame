using System;
using _Project.CodeBase.Logic.HeroWeapon;

namespace _Project.CodeBase.StaticData.ItemsDataBase.Types
{
    [Serializable]
    public class Revolver : Weapon
    {
        public float StartReloadDuration;
        public float OneAmmoReloadDuration;
        public float EndReloadAnimation;
    }
}