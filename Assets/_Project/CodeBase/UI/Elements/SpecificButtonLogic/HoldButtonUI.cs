using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class HoldButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _holdTime;
        
        private bool _pointerDown;
        private bool _holdApproved;
        private float _time;

        public event Action OnTouchStarted;
        public event Action OnHoldApproved;
        public event Action OnHoldEnded;
        public event Action OnClickPerformed;

        private void Update()
        {
            if (!_pointerDown) return;

            if (_time >= _holdTime && !_holdApproved)
            {
                //ResetHold();
                OnHoldApproved?.Invoke();
                _holdApproved = true;
            }

            _time += Time.deltaTime;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDown = true;
            OnTouchStarted?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_time >= _holdTime)
                OnHoldEnded?.Invoke();
            else
                OnClickPerformed?.Invoke();

            //OnTouchEnded?.Invoke(_time >= _holdTime);
            ResetHold();
        }

        private void ResetHold()
        {
            _pointerDown = false;
            _holdApproved = false;
            _time = 0;
        }
    }
}