using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero.Reload
{
    public class HeroReload : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroAmmo _ammo;
        [SerializeField] private HeroScoping _heroScoping;
        [SerializeField] private HeroState _state;

        private InputService _inputService;
        private List<IReloadSystem> _reloadSystems;
        private IReloadSystem _selectedReloadSystem;
        private bool _isRevolver;
        private bool _isNeedReload;


        [Inject]
        public void Construct(InputService inputService, List<IReloadSystem> reloadSystems)
        {
            _inputService = inputService;
            _reloadSystems = reloadSystems;
            _inputService.ReloadAction.Event += Reload;

            foreach (var reloadSystem in reloadSystems) 
                reloadSystem.Init(_heroAnimator);
        }

        private void OnDestroy() => 
            _inputService.ReloadAction.Event -= Reload;

        public void Setup(WeaponInfo<GunSpecs> gunInfo, WeaponConfiguration config)
        {
            foreach (var reloadSystem in _reloadSystems.Where(reloadSystem => reloadSystem.Setup(gunInfo, config)))
            {
                _selectedReloadSystem = reloadSystem;
                break;
            }

            _isNeedReload = true;
        }
        
        public void Construct(KnifeInfo knifeInfo) => 
            _isNeedReload = false;

        public async void Reload()
        {
            if (!_isNeedReload) return;
            if (_state.Reloading) return;

            var reload = _ammo.Reload();
            if (reload.Item1 == ReloadState.None) return;

            if (_state.Aiming) 
                _heroScoping.UnScope();
            
            ChangeReloadingState(true);
            await _selectedReloadSystem.Reload(reload);
            _ammo.UpdateUI();
            ChangeReloadingState(false);
        }

        private void ChangeReloadingState(bool value) => 
            _state.Reloading = value;
    }
}