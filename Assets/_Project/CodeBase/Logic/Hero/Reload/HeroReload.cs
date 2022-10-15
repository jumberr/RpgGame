using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.HeroWeapon.Animations;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
        
        public void Construct(Weapon weapon, WeaponConfiguration config, RevolverAnimation revolverAnimation)
        {
            if (!(revolverAnimation is null))
            {
                _isRevolver = true;
                _reloadRevolver.Construct(weapon, config, revolverAnimation);
            }
            else
            {
                _isRevolver = false;
                _defaultReload.Construct(weapon);
            }
            _isNeedReload = true;
        }
        
        public void Construct(Knife knife) => 
            _isNeedReload = false;

        private void Start()
        {
            _defaultReload = new DefaultReload(_heroAnimator);
            _reloadRevolver = new ReloadRevolver(_heroAnimator);
        }

        private void OnDisable()
        {
            _inputService.ReloadAction.Event -= Reload;
            _reloadRevolver.UnsubscribeRevolverEvents();
        }

        public void SetInputService(InputService inputService)
        {
            _inputService = inputService;
            _inputService.ReloadAction.Event += Reload;
        }

        public async void Reload()
        {
            if (!_isNeedReload) return;
            if (_state.CurrentPlayerState == PlayerState.Reload) return;

            var reload = _ammo.Reload();
            if (reload.Item1 == ReloadState.None) return;

            if (_state.CurrentPlayerState == PlayerState.Scoping) 
                _heroScoping.UnScope();
            
            _state.Enter(PlayerState.Reload);
            await PlayReloadAnimation(reload);
            _ammo.UpdateUI();
            _state.Enter(PlayerState.None);
        }

        private async UniTask PlayReloadAnimation((ReloadState, int, int) result)
        {
            if (_isRevolver)
                await _reloadRevolver.Reload(result);
            else
                await _defaultReload.Reload(result);
        }
    }
}