using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Crosshair;
using _Project.CodeBase.UI.Elements.Hud.HotBar;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements.Hud
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        [SerializeField] private AmmoUI _ammoUI;
        [SerializeField] private CrosshairAdapter _crosshairAdapter;
        [SerializeField] private InteractionUI _interactionUI;
        [SerializeField] private HotBarUI _hotBarUI;

        private IHealth _heroHealth;
        private HeroInventory _heroInventory;

        public HotBarUI HotBar => _hotBarUI;

        public void Construct(InputService inputService,
            IHealth heroHealth,
            HeroAmmo heroAmmo,
            WeaponController weaponController,
            HeroState heroState,
            HeroInteraction heroInteraction)
        {
            SetupHealth(heroHealth);
            SetupAmmoUI(heroAmmo);
            SetupCrosshairUI(weaponController, inputService, heroState);
            SetupInteractionUI(inputService, heroInteraction);
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