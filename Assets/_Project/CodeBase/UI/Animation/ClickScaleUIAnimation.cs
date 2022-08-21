using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Animation
{
    public class ClickScaleUIAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _scaleEndValue;
        [SerializeField] private float _duration;

        private Tween _tween;
        private Vector3 _defaultScaleValue;

        private void Awake() => 
            _defaultScaleValue = transform.localScale;

        public void OnPointerDown(PointerEventData eventData)
        {
            _tween?.Kill();
            _tween = transform.DOScale(_scaleEndValue, _duration);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _tween?.Kill();
            _tween = transform.DOScale(_defaultScaleValue, _duration);
        }
    }
}