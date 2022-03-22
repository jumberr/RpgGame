using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroReload : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroAmmo _ammo;
        [SerializeField] private HeroScoping _heroScoping;
        [SerializeField] private HeroState _state;
        [SerializeField] private InputService _inputService;

        private float _reloadTime;
        
        public void Construct(WeaponData weaponData) => 
            _reloadTime = weaponData.Weapon.ReloadTime;

        private void Start() => 
            _inputService.OnReload += Reload;

        private void OnDisable() => 
            _inputService.OnReload -= Reload;
        
        public async void Reload()
        {
            var maxMagazine = _ammo.BulletMaxMagazine;
            var bulletLeft = _ammo.BulletLeft;
            var bulletAll = _ammo.BulletAll;

            var usedAmmo = maxMagazine - bulletLeft;
            if (usedAmmo <= 0 || bulletAll <= 0 || _state.CurrentState == EHeroState.Reload) return;

            if (_state.CurrentState == EHeroState.Scoping) 
                _heroScoping.UnScope();

            _state.Enter(EHeroState.Reload);
            await _heroAnimator.ReloadAnimation(_reloadTime);
            
            if (bulletAll < usedAmmo)
            {
                bulletLeft += bulletAll;
                bulletAll = 0;
            }
            else
            {
                bulletLeft += usedAmmo;
                bulletAll -= usedAmmo;
            }

            _ammo.SetAmmoValue(bulletLeft, bulletAll);
            _state.Enter(EHeroState.None);
        }
    }
}