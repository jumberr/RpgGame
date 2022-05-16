using _Project.CodeBase.UI.MVA;
using _Project.CodeBase.Utils;
using UnityEngine;

namespace _Project.CodeBase.UI.Elements.Crosshair
{
    public class CrosshairView : MonoBehaviour, IView<CrosshairState>
    {
        [SerializeField] private RectTransform _dot;
        [SerializeField] private RectTransform _crosshair;

        public void ApplySize(float currentSize) => 
            _crosshair.sizeDelta = new Vector2(currentSize, currentSize);

        public void Show() { }

        public void Show(CrosshairState state) => 
            (state == CrosshairState.Dot ? _dot : _crosshair).gameObject.SetActive(true);

        public void Hide() => 
            ComponentUtils.SetActive(false, _dot, _crosshair);
    }
}