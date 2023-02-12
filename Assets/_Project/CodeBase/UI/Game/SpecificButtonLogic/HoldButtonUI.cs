using System;
using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class HoldButtonUI : NightCache, INightRun, IPointerDownHandler, IPointerUpHandler
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

        public void Run()
        {
            if (!_pointerDown || Dragging) return;

            if (_time >= _holdTime && !_holdApproved)
            {
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