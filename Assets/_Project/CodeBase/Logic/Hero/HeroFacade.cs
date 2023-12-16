using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero.Cam;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.Utils.Factory;
using NTC.Global.Cache;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroFacade : NightCache
    {
        [SerializeField] private WeaponController _weaponController;
        [SerializeField] private HeroInteraction _interaction;
        [SerializeField] private HeroInventory _inventory;
        [SerializeField] private HeroMovement _movement;
        [SerializeField] private HeroCamera _camera;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroScoping _scoping;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroReload _reload;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroRecoil _recoil;
        [SerializeField] private HeroDeath _death;
        [SerializeField] private HeroState _state;
        [SerializeField] private HeroAmmo _ammo;
        
        private IUIFactory _uiFactory;

        public HeroInventory Inventory => _inventory;
        public HeroCamera Camera => _camera;
        public IHealth Health => _health;
        public HeroAmmo Ammo => _ammo;
        public WeaponController WeaponController => _weaponController;
        public HeroState HeroState => _state;
        public HeroInteraction Interaction => _interaction;

        [Inject]
        private void Construct(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _death.SetHealthComponent(_health);
            
            _death.ZeroHealth += _uiFactory.ShowDeathScreen;
        }

        private void OnDestroy() => 
            _death.ZeroHealth -= _uiFactory.ShowDeathScreen;

        public class Factory : ComponentPlaceholderFactory<HeroFacade>
        {
        }
    }
}