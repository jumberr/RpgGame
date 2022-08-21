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
        
        public bool Dragging { get; set; }

        public event Action OnTouchStarted;
        public event Action OnLongClickPerformed;
        public event Action OnTouchEnded;
        public event Action OnClickPerformed;

        private void Update()
        {
            if (!_pointerDown || Dragging) return;

            if (_time >= _holdTime && !_holdApproved)
            {
                //ResetHold();
                OnLongClickPerformed?.Invoke();
                _holdApproved = true;
            }

            _time += Time.deltaTime;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _pointerDown = true;
            OnTouchStarted?.Invoke();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (_time >= _holdTime)
                OnTouchEnded?.Invoke();
            else
                OnClickPerformed?.Invoke();

            //OnTouchEnded?.Invoke(_time >= _holdTime);
            ResetHold();
        }

        public void ResetHold()
        {
            _pointerDown = false;
            _holdApproved = false;
            _time = 0;
        }
    }
}