using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        [SerializeField] private AmmoUI _ammoUI;
        
        private IHealth _heroHealth;

        public void Construct(
            IHealth heroHealth,
            HeroAmmoController heroAmmoController)
        {
            SetupHealth(heroHealth);
            SetupAmmoUI(heroAmmoController);
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                SetupHealth(health);
        }

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= UpdateHpBar;

        private void SetupHealth(IHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void SetupAmmoUI(HeroAmmoController heroAmmoController) => 
            _ammoUI.Construct(heroAmmoController);

        private void UpdateHpBar() => 
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
    }
}