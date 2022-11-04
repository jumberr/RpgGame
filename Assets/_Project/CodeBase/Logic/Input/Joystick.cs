using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace _Project.CodeBase.Logic.Input
{
    public class Joystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [InputControl(layout = "Vector2"), SerializeField] public string _controlPath;
        [field: SerializeField] public RectTransform Parent { get; private set; }
        [field: SerializeField] public float MovementRange { get; private set; }

        private Vector2 _pointerDownPos;

        protected Vector3 StartPos { get; private set; }

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        private void Start() => 
            StartPos = ((RectTransform)transform).anchoredPosition;

        public virtual void OnPointerDown(PointerEventData eventData) => 
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Parent, eventData.position, eventData.pressEventCamera, out _pointerDownPos);

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Parent, eventData.position, eventData.pressEventCamera, out var position);
            
            var delta = Vector2.ClampMagnitude(position - _pointerDownPos, MovementRange);

            var newPos = new Vector2(delta.x / MovementRange, delta.y / MovementRange);
            newPos = UpdateStick(newPos, delta);
            SendValueToControl(newPos);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            ((RectTransform) transform).anchoredPosition = StartPos;
            SendValueToControl(Vector2.zero);
        }

        protected virtual bool CanBeLocked(Vector2 input) =>
            false;
        
        protected virtual Vector2 UpdateStick(Vector2 input, Vector3 viewDelta)
        {
            UpdateStickView(viewDelta);
            return input;
        }

        private void UpdateStickView(Vector3 delta) => 
            ((RectTransform)transform).anchoredPosition = StartPos + delta;
    }
}