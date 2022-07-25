using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class DoubleClickButtonUI : MonoBehaviour, IPointerDownHandler
    {
        private const int ClicksAmount = 2;
        [SerializeField] private float _doubleClickTime;

        private int _clicks;
        private float _time;

        public event Action OnDoubleClicked;

        private void Update()
        {
            if (_clicks <= 0) return;

            _time += Time.deltaTime;
            ClickedTwiceCompleted();
            ResetDoubleClick();
        }

        public void OnPointerDown(PointerEventData eventData) =>
            _clicks++;

        private void ClickedTwiceCompleted()
        {
            if (!(_time <= _doubleClickTime) || _clicks != ClicksAmount) return;
            OnDoubleClicked?.Invoke();
            ResetCounter();
        }

        private void ResetDoubleClick()
        {
            if (_time > _doubleClickTime)
                ResetCounter();
        }

        private void ResetCounter()
        {
            _clicks = 0;
            _time = 0;
        }
    }
}