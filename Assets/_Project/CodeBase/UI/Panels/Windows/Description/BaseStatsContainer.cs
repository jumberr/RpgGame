using TMPro;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class BaseStatsContainer : MonoBehaviour
    {
        protected const int Percent = 100;
        
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _valueText;
        [Space]
        [SerializeField] private string _statsName;

        protected TMP_Text ValueText => _valueText;
        
        private void Start() => 
            Initialize();

        protected virtual void Initialize() => 
            _nameText.text = _statsName;

        public void UpdateView(float currentValue) => 
            _valueText.text = $"{currentValue}";
    }
}