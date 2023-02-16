using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using UnityEngine;
using Zenject;
using Screen = _Project.CodeBase.UI.Screens.Screen;

namespace _Project.CodeBase.UI
{
    public class ActorUI : Screen
    {
        [SerializeField] private PlatformSpecificHud _hud;
        [Space]
        [SerializeField] private HpBar _hpBar;
        [SerializeField] private AmmoUI _ammoUI;
        [SerializeField] private CrosshairAdapter _crosshairAdapter;
        [SerializeField] private InteractionUI _interactionUI;
        [SerializeField] private HotBarUI _hotBarUI;

        private InputService _inputService;
        private IHealth _heroHealth;
        private HeroInventory _heroInventory;

        public HotBarUI HotBar => _hotBarUI;

        [Inject]
        public void Construct(InputService inputService, HeroFacade.Factory factory)
        {
            _hud.Initialize();
            _inputService = inputService;
            var facade = factory.Facade;

            SetupHealth(facade.Health);
            SetupAmmoUI(facade.Ammo);
            SetupCrosshairUI(facade.WeaponController, _inputService, facade.HeroState);
            SetupInteractionUI(_inputService, facade.Interaction);
        }

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= UpdateHpBar;

        private void SetupHealth(IHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void SetupAmmoUI(HeroAmmo heroAmmo) => 
            _ammoUI.Construct(heroAmmo);

        private void SetupCrosshairUI(WeaponController weaponController, InputService inputService, HeroState heroState) => 
            _crosshairAdapter.Construct(weaponController, inputService, heroState);

        private void SetupInteractionUI(InputService inputService, HeroInteraction heroInteraction) => 
            _interactionUI.Construct(heroInteraction, inputService);

        private void UpdateHpBar() => 
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
    }
}