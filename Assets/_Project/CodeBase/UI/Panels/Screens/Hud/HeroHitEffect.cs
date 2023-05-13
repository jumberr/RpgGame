using _Project.CodeBase.Logic;
using _Project.CodeBase.Logic.Hero;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class HeroHitEffect : MonoBehaviour
    {
        [SerializeField] private Image bloodSplatterImage;
        
        [Header("Hurt")]
        [SerializeField] private float maxHurtAlpha;
        [SerializeField] private Image hurtImage;

        private IHealth _health;

        [Inject]
        public void Construct(HeroFacade.Factory factory)
        {
            _health = factory.Instance.Health;
            _health.HealthChanged += UpdateBloodOverlay;
        }

        private void OnDestroy() => 
            _health.HealthChanged -= UpdateBloodOverlay;

        private void UpdateBloodOverlay()
        {
            var current = _health.Current / _health.Max;
            UpdateImage(bloodSplatterImage, current, 1);
            UpdateImage(hurtImage, current, maxHurtAlpha);
        }

        private void UpdateImage(Graphic image, float current, float max)
        {
            var color = image.color;
            color.a = Mathf.Clamp(1 - current, 0, max);
            image.color = color;
        }
    }
}