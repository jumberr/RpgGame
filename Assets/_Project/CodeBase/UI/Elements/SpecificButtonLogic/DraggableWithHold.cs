using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class DraggableWithHold : DraggableComponent
    {
        [SerializeField] private HoldButtonUI _holdButton;

        private bool _holdApproved;

        private void Awake() => 
            Subscribe();

        private void OnDestroy() => 
            Cleanup();

        public override void OnDrag(PointerEventData eventData)
        {
            if (!_holdApproved)
                return;
            
            base.OnDrag(eventData);
        }

        protected virtual void Click() { }

        private void Subscribe()
        {
            _holdButton.OnHoldApproved += HoldApproved;
            _holdButton.OnHoldEnded += HoldEnded;
            _holdButton.OnClickPerformed += Click;
        }

        private void Cleanup()
        {
            _holdButton.OnHoldApproved -= HoldApproved;
            _holdButton.OnHoldEnded -= HoldEnded;
            _holdButton.OnClickPerformed -= Click;
        }

        private void HoldApproved() => 
            _holdApproved = true;

        private void HoldEnded() => 
            _holdApproved = false;
    }
}