using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        private IHealth _health;

        [Inject]
        public void Construct(HeroFacade.Factory factory)
        {
            _health = factory.Instance.Health;
            _health.HealthChanged += UpdateBar;
        }

        private void OnDestroy() => 
            _health.HealthChanged -= UpdateBar;

        private void UpdateBar() => 
            SetValue(_health.Current, _health.Max);

        private void SetValue(float current, float max) =>
            barImage.fillAmount = current / max;
    }
}