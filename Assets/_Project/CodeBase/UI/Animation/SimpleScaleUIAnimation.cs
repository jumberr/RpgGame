using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class SimpleScaleUIAnimation : MonoBehaviour
    {
        private const float DefaultScale = 1f;
        
        [SerializeField] private float _scaleFromValue;
        [SerializeField] private float _duration;

        private Tween _tween;

        public void Scale()
        {
            _tween?.Kill();
            _tween = transform
                .DOScale(DefaultScale, _duration)
                .From(_scaleFromValue);
        }
    }
}