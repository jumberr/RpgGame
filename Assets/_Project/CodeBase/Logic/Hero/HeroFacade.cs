using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero.Reload;
using _Project.CodeBase.Logic.Hero.Shooting;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
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
        [SerializeField] private HeroRotation _rotation;
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

        public void Construct(InputService inputService, IStaticDataService staticDataService, Action zeroHealthAction)
        {
            _inputService = inputService;
            _inventory.Construct(staticDataService);
            _death.ZeroHealth += zeroHealthAction;
            SetupInputService(_inputService);
        }

        private void SetupInputService(InputService inputService)
        {
            _weaponController.Setup(inputService);
            _movement.SetInputService(inputService);
            _rotation.SetInputService(inputService);
            _scoping.SetInputService(inputService);
            _reload.SetInputService(inputService);
            _death.SetInputService(inputService);
        }

        public class Factory : PlaceholderFactory<HeroFacade>
        {
        }
    }
}