using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.UI.Animation
{
    public class UiAnimation : MonoBehaviour
    {
        public const float StartValue = 0f;
        public const float EndValue = 1f;

        [SerializeField] private bool _needAnimation;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private UiAnimationType _animationType;
        [SerializeField] private Ease _type;
        [SerializeField] private float _animationTime;

        public float AnimationTime => _animationTime;

        public void PrepareAnimation(Transform parent)
        {
            if (!_needAnimation) return;
            switch (_animationType)
            {
                case UiAnimationType.Fade:
                    _canvasGroup.alpha = 0;
                    break;
                case UiAnimationType.Scale:
                    parent.localScale = Vector3.zero;
                    break;
            }
        }
        
        public void DoAnimation(float endValue)
        {
            if (!_needAnimation) return;
            switch (_animationType)
            {
                case UiAnimationType.Fade:
                    Fade(endValue);
                    break;

                case UiAnimationType.Scale:
                    Scale(endValue);
                    break;
            }
        }

        private void Scale(float endValue) => 
            _canvasGroup.transform
                .DOScale(endValue, _animationTime)
                .SetEase(_type);
        
        private void Fade(float endValue) => 
            _canvasGroup
                .DOFade(endValue, _animationTime)
                .SetEase(_type);
    }
}