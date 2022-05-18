using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.UI.Elements.Crosshair;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements.Hud
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        [SerializeField] private AmmoUI _ammoUI;
        [SerializeField] private CrosshairAdapter _crosshairAdapter;
        [SerializeField] private InteractionUI _interactionUI;
        
        private IHealth _heroHealth;

        public void Construct(IHealth heroHealth,
            HeroAmmo heroAmmo,
            WeaponController weaponController,
            InputService inputService,
            HeroState heroState,
            Interaction interaction)
        {
            SetupHealth(heroHealth);
            SetupAmmoUI(heroAmmo);
            SetupCrosshairUI(weaponController, inputService, heroState);
            SetupInteractionUI(interaction);
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

        private void SetupInteractionUI(Interaction interaction) => 
            _interactionUI.Construct(interaction);

        private void UpdateHpBar() => 
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
    }
}