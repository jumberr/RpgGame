﻿using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Crosshair;
using _Project.CodeBase.UI.Elements.Hud.HotBar;
using Cysharp.Threading.Tasks;
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

        public async UniTask Construct(
            IHealth heroHealth,
            HeroAmmo heroAmmo,
            WeaponController weaponController,
            InputService inputService,
            HeroState heroState,
            Interaction interaction,
            HeroInventory inventory)
        {
            SetupHealth(heroHealth);
            SetupAmmoUI(heroAmmo);
            SetupCrosshairUI(weaponController, inputService, heroState);
            SetupInteractionUI(interaction);
            await SetupHotBar(inventory);
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

        private async UniTask SetupHotBar(HeroInventory inventory)
        {
            _heroInventory = inventory;
            await UniTask.WaitUntil(IsInventoryExists);
            _hotBarUI.Construct(inventory);
        }
        
        private bool IsInventoryExists() => 
            _heroInventory.Inventory != null;
    }
}