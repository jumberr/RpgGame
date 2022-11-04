using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.Logic.Input
{
    public class CustomJoystick : Joystick
    {
        [SerializeField] private SprintButton _sprintButton;
        private bool _locked;

        public override void OnPointerDown(PointerEventData eventData)
        {
            _locked = false;
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (_locked) return;
            base.OnPointerUp(eventData);
        }

        protected override bool CanBeLocked(Vector2 input) =>
            Mathf.Abs(input.x) <= 0.15f && input.y >= 0.95f;
        
        protected override Vector2 UpdateStick(Vector2 input, Vector3 viewDelta)
        {
            var value = CanBeLocked(input);
            ShowHideSprint(input);
            LockStick(value);
            UpdateStickView(value, viewDelta);
            return value ? Vector2.up : input;
        }

        private void UpdateStickView(bool locked, Vector3 delta) => 
            ((RectTransform)transform).anchoredPosition = locked 
                ? new Vector3(0, MovementRange)
                : StartPos + delta;
        
        private void ShowHideSprint(Vector2 dir) => 
            _sprintButton.gameObject.SetActive(dir.y > 0.5f);

        private void LockStick(bool value)
        {
            _locked = value;
            _sprintButton.SendValue(value ? 1f : 0f);
        }
    }
}