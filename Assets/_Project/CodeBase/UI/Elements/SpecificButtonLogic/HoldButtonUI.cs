using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class HoldButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _holdTime;
        
        private bool _pointerDown;
        private float _time;
        
        public event Action OnHoldStarted;
        public event Action OnTimeFinished;
        public event Action OnHoldEnded;

        private void Update()
        {
            if (!_pointerDown) return;

            if (_time >= _holdTime)
            {
                ResetHold();
                OnTimeFinished?.Invoke();
            }

            _time += Time.deltaTime;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDown = true;
            OnHoldStarted?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetHold();
            OnHoldEnded?.Invoke();
        }

        private void ResetHold()
        {
            _pointerDown = false;
            _time = 0;
        }
    }
}