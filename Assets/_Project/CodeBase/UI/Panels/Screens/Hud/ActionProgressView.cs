using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class ActionProgressView : MonoBehaviour
    {
        private const float Zero = 0f;
        private const float One = 1f;

        [SerializeField] private Image _background;
        [SerializeField] private Image _progressBar;
        [SerializeField] private float _duration;
        

        public void SetProgress(float value) => 
            _progressBar.fillAmount = value;

        public void ToggleProgressbar(bool value)
        {
            _background.DOFade(value ? One : Zero, _duration);
            if (!value)
                SetProgress(Zero);
        }
    }
}