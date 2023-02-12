using System;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroAmmo : MonoBehaviour
    {
        [SerializeField] private HeroInventory _inventory;

        private MagazineData _magazineData;
        private int _bulletMaxMagazine;
        private ItemName _itemName;

        public event Action<int, int> OnUpdateAmmo;
        public event Action<int, int, Sprite> OnChangeWeapon;
        public event Action OnHideWeapon;

        public MagazineData MagazineData => _magazineData;
        private int BulletLeft => _magazineData.BulletsLeft;
        private int BulletAll { get; set; }

        public void Construct(MagazineData magazineData, GunInfo gunInfo)
        {
            _magazineData = magazineData;
            
            SetupWeaponInfo(gunInfo);
            UpdateAmmoValue();
            var sprite = _inventory.ItemsInfo.FindItem(_itemName).UIInfo.Icon;
            OnChangeWeapon?.Invoke(BulletLeft, BulletAll, sprite);
        }
        
        public void Construct(KnifeInfo knifeInfo) => 
            SetupKnifeInfo();

        private void OnEnable() => 
            _inventory.OnUpdate += UpdateAmmoValue;

        private void OnDisable() => 
            _inventory.OnUpdate -= UpdateAmmoValue;

        public bool CanShoot() => 
            BulletLeft > MagazineData.Empty;

        public void UseOneAmmo()
        {
            _magazineData.BulletsLeft -= 1;
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
            
            if (usedAmmo <= MagazineData.Empty || BulletAll <= MagazineData.Empty)
                return (ReloadState.None, usedAmmo, MagazineData.Empty);

            int grabbedFromInventory;
            if (BulletAll < usedAmmo)
            {
                grabbedFromInventory = BulletAll;
                _magazineData.BulletsLeft += BulletAll;
                BulletAll = MagazineData.Empty;
            }
            else
            {
                grabbedFromInventory = usedAmmo;
                _magazineData.BulletsLeft += usedAmmo;
                BulletAll -= usedAmmo;
            }

            _inventory.RemoveItem(_itemName, grabbedFromInventory);
            
            return (usedAmmo == _bulletMaxMagazine ?
                ReloadState.FullReload : 
                ReloadState.Reload, usedAmmo, grabbedFromInventory);
        }

        private void SetupWeaponInfo(GunInfo gunInfo)
        {
            _bulletMaxMagazine = gunInfo.GunSpecs.MagazineInfo.BulletsMax;
            _itemName = gunInfo.GunSpecs.AmmoType.ToItemName();
        }
        
        private void SetupKnifeInfo()
        {
            _magazineData.BulletsLeft = MagazineData.Empty;
            _bulletMaxMagazine = MagazineData.Empty;
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