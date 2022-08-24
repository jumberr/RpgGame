using DG.Tweening;
using UnityEngine;

namespace _Project.CodeBase.UI.Animation
{
    public class SimpleScaleUIAnimation : MonoBehaviour
    {
        [SerializeField] private float _scaleEndValue;
        [SerializeField] private float _duration;

        private Tween _tween;
        private Vector3 _defaultScaleValue;

        private void Awake() => 
            _defaultScaleValue = transform.localScale;

        public void Scale()
        {
            _tween?.Kill();
            _tween = transform
                .DOScale(_defaultScaleValue, _duration)
                .From(_scaleEndValue);
        }
    }
}