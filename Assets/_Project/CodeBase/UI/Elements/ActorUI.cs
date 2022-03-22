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
            HeroAmmo heroAmmo)
        {
            SetupHealth(heroHealth);
            SetupAmmoUI(heroAmmo);
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

        private void SetupAmmoUI(HeroAmmo heroAmmo) => 
            _ammoUI.Construct(heroAmmo);

        private void UpdateHpBar() => 
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
    }
}