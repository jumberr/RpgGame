using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.HeroWeapon.Animations;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
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
        private DefaultReload _defaultReload;
        private ReloadRevolver _reloadRevolver;
        private bool _isRevolver;
        private bool _isNeedReload;
        
        
        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
            _inputService.ReloadAction.Event += Reload;
        }
        
        private void Start()
        {
            _defaultReload = new DefaultReload(_heroAnimator);
            _reloadRevolver = new ReloadRevolver(_heroAnimator);
        }

        private void OnDestroy()
        {
            _inputService.ReloadAction.Event -= Reload;
            _reloadRevolver.UnsubscribeRevolverEvents();
        }
        
        public void Construct(GunInfo gunInfo, WeaponConfiguration config, RevolverAnimation revolverAnimation)
        {
            if (!(revolverAnimation is null))
            {
                _isRevolver = true;
                _reloadRevolver.Construct(gunInfo, config, revolverAnimation);
            }
            else
            {
                _isRevolver = false;
                _defaultReload.Construct(gunInfo);
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
            await PlayReloadAnimation(reload);
            _ammo.UpdateUI();
            ChangeReloadingState(false);
        }

        private async UniTask PlayReloadAnimation((ReloadState, int, int) result)
        {
            if (_isRevolver)
                await _reloadRevolver.Reload(result);
            else
                await _defaultReload.Reload(result);
        }

        private void ChangeReloadingState(bool value) => 
            _state.Reloading = value;
    }
}