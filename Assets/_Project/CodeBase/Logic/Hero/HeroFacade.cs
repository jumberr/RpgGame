using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero.Cam;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.Utils.Factory;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class HeroFacade : MonoBehaviour
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
        
        private InputService _inputService;
        
        public HeroInventory Inventory => _inventory;
        public HeroCamera Camera => _camera;
        public IHealth Health => _health;
        public HeroAmmo Ammo => _ammo;
        public WeaponController WeaponController => _weaponController;
        public HeroState HeroState => _state;
        public HeroInteraction Interaction => _interaction;

        [Inject]
        private void Construct(InputService inputService, IStaticDataService staticDataService)
        {
            _inputService = inputService;
            SetupItemDatabase(staticDataService);
            SetupInputService(_inputService);
        }

        public void Setup(Action zeroHealthAction) => 
            _death.ZeroHealth += zeroHealthAction;

        private void SetupItemDatabase(IStaticDataService staticDataService)
        {
            var itemsDataBase = staticDataService.ForInventory();
            _inventory.Construct(itemsDataBase);
            _weaponController.Construct(itemsDataBase);
        }

        private void SetupInputService(InputService inputService)
        {
            _weaponController.Setup(inputService);
            _movement.SetInputService(inputService);
            _state.SetInputService(inputService);
            _camera.SetInputService(inputService);
            _scoping.SetInputService(inputService);
            _reload.SetInputService(inputService);
            _death.SetInputService(inputService);
        }

        public class Factory : ComponentPlaceholderFactory<HeroFacade>
        {
        }
    }
}