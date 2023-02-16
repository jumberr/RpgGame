using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class SliderStatsContainer : BaseStatsContainer
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _foregroundSlider;
        [SerializeField] private Color32 _sliderColor;

        protected override void Initialize()
        {
            base.Initialize(); 
            _foregroundSlider.color = _sliderColor;
        }

        public void UpdateView(float currentValue, float maxValue)
        {
            var divided = currentValue / maxValue;
            var percent = Mathf.RoundToInt(divided * Percent);
            _slider.value = divided;
            ValueText.text = $"{percent}%";
        }
    }
}