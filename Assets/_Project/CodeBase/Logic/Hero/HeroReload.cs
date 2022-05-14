using System.Threading.Tasks;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using Cysharp.Threading.Tasks;
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
        private float _fullReloadTime;
        
        public void Construct(Weapon weapon)
        {
            _reloadTime = weapon.WeaponData.ReloadTime;
            _fullReloadTime = weapon.WeaponData.FullReloadTime;
        }

        private void OnEnable() => 
            _inputService.OnReload += Reload;

        private void OnDisable() => 
            _inputService.OnReload -= Reload;

        public async void Reload()
        {
            if (_state.CurrentState == State.State.Reload) return;

            var reload = _ammo.Reload();
            if (reload == ReloadState.None) return;

            if (_state.CurrentState == State.State.Scoping) 
                await _heroScoping.UnScope();
            
            _state.Enter(State.State.Reload);
            await PlayReloadAnimation(reload);
            _ammo.UpdateUI();
            _state.Enter(State.State.None);
        }

        private async Task PlayReloadAnimation(ReloadState result)
        {
            if (result == ReloadState.FullReload)
                await FullReloadAnimation();
            else if (result == ReloadState.Reload)
                await ReloadAnimation();
        }

        private async UniTask FullReloadAnimation() => 
            await _heroAnimator.FullReloadAnimation(_fullReloadTime);

        private async UniTask ReloadAnimation() => 
            await _heroAnimator.ReloadAnimation(_reloadTime);
    }
}