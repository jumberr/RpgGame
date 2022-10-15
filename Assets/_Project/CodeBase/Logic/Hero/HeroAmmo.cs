using System;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroAmmo : MonoBehaviour
    {
        [SerializeField] private HeroInventory _inventory;
        private int _bulletMaxMagazine;
        private AmmoType _type;
        private ItemName _itemName;
        
        public event Action<int, int> OnUpdateAmmo;
        public event Action<int, int, Sprite> OnChangeWeapon;
        public event Action OnHideWeapon;

        public int BulletLeft { get; private set; }
        public int BulletAll { get; private set; }

        public void Construct(Weapon weapon)
        {
            SetupWeaponData(weapon);
            UpdateAmmoValue();
            var sprite = _inventory.ItemsDataBase.FindItem(_itemName).ItemUIData.Icon;
            OnChangeWeapon?.Invoke(BulletLeft, BulletAll, sprite);
        }
        
        public void Construct(Knife knife) => 
            SetupKnifeData();

        private void OnEnable() => 
            _inventory.OnUpdate += UpdateAmmoValue;

        private void OnDisable() => 
            _inventory.OnUpdate -= UpdateAmmoValue;

        public bool CanShoot() => 
            BulletLeft > 0;

        public void UseOneAmmo()
        {
            BulletLeft -= 1;
            _inventory.RemoveItemFromSlot(_itemName);
            UpdateUI();
        }

        public void UpdateUI() => 
            OnUpdateAmmo?.Invoke(BulletLeft, BulletAll);

        public void HideUI() =>
            OnHideWeapon?.Invoke();
        
        public (ReloadState, int, int) Reload()
        {
            var usedAmmo = _bulletMaxMagazine - BulletLeft;
            
            if (usedAmmo <= 0 || BulletAll <= 0)
                return (ReloadState.None, usedAmmo, 0);

            int grabbedFromInventory;
            if (BulletAll < usedAmmo)
            {
                grabbedFromInventory = BulletAll;
                BulletLeft += BulletAll;
                BulletAll = 0;
            }
            else
            {
                grabbedFromInventory = usedAmmo;
                BulletLeft += usedAmmo;
                BulletAll -= usedAmmo;
            }

            _inventory.RemoveItem(_itemName, grabbedFromInventory);
            
            return (usedAmmo == _bulletMaxMagazine ?
                ReloadState.FullReload : 
                ReloadState.Reload, usedAmmo, grabbedFromInventory);
        }

        private void SetupWeaponData(Weapon weapon)
        {
            BulletLeft = weapon.Magazine.BulletsLeft;
            _bulletMaxMagazine = weapon.Magazine.BulletsMax;
            _type = weapon.WeaponData.AmmoType;
            _itemName = _type.ToItemName();
        }
        
        private void SetupKnifeData()
        {
            BulletLeft = 0;
            _bulletMaxMagazine = 0;
        }

        private void UpdateAmmoValue()
        {
            var all = _inventory.FindItemAmount(_itemName);
            if (all != BulletAll) 
                UpdateInventoryAmmo(all);
        }

        private void UpdateInventoryAmmo(int bulletAll)
        {
            BulletAll = bulletAll;
            UpdateUI();
        }
    }
}