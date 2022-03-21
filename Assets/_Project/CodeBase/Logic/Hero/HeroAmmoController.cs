using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroAmmoController : MonoBehaviour
    {
        public event Action<int, int> OnUpdateAmmo;
        
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroState _state;
        [SerializeField] private InputService _inputService;
        private int _bulletLeft;
        private int _bulletMaxMagazine;
        private int _bulletAll;

        private float _reloadTime;

        public void Construct(WeaponData weaponData)
        {
            var magazine = weaponData.Magazine;
            _bulletLeft = magazine.BulletsLeft;
            _bulletMaxMagazine = magazine.BulletsMax;
            _bulletAll = 120;

            _reloadTime = weaponData.Weapon.ReloadTime;

            // Get _bulletAll from inventory
        }

        private void Start() => 
            _inputService.OnReload += Reload;

        public void UpdateAmmoUI() => 
            OnUpdateAmmo?.Invoke(_bulletLeft, _bulletAll);

        public bool CanShoot() => 
            _bulletLeft > 0;

        public void UseOneAmmo()
        {
            _bulletLeft -= 1;
            OnUpdateAmmo?.Invoke(_bulletLeft, _bulletAll);
        }

        public async void Reload()
        {
            var usedAmmo = _bulletMaxMagazine - _bulletLeft;
            if (usedAmmo <= 0 || _bulletAll <= 0 || _state.CurrentState == EHeroState.Reload) return;
            
            _state.Enter(EHeroState.Reload);
            await _heroAnimator.ReloadAnimation(_reloadTime);
            
            if (_bulletAll < usedAmmo)
            {
                _bulletLeft += _bulletAll;
                _bulletAll = 0;
            }
            else
            {
                _bulletLeft += usedAmmo;
                _bulletAll -= usedAmmo;
            }
            
            OnUpdateAmmo?.Invoke(_bulletLeft, _bulletAll);
            _state.Enter(EHeroState.None);
        }
    }
}