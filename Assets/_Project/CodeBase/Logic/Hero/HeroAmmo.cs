using System;
using _Project.CodeBase.Logic.HeroWeapon;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroAmmo : MonoBehaviour
    {
        public event Action<int, int> OnUpdateAmmo;
        
        public int BulletLeft { get; private set; }
        public int BulletMaxMagazine { get; private set; }
        public int BulletAll { get; private set; }
        
        public void Construct(Weapon weapon)
        {
            var magazine = weapon.Magazine;
            BulletLeft = magazine.BulletsLeft;
            BulletMaxMagazine = magazine.BulletsMax;
            BulletAll = 120;

            // Get _bulletAll from inventory
        }

        public bool CanShoot() => 
            BulletLeft > 0;

        public void UseOneAmmo()
        {
            BulletLeft -= 1;
            OnUpdateAmmo?.Invoke(BulletLeft, BulletAll);
        }

        public void SetAmmoValue(int bulletLeft, int bulletAll)
        {
            BulletLeft = bulletLeft;
            BulletAll = bulletAll;
            OnUpdateAmmo?.Invoke(BulletLeft, BulletAll);
        }
    }
}