using System;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class ActionBoolWrapper : BaseActionWrapper
    {
        public event Action<bool> Event;
        
        public ActionBoolWrapper(InputAction inputAction) : base(inputAction)
        {
        }

        protected override void Subscribe()
        {
            InputAction.performed += ActionPerform;
            InputAction.canceled += ActionCancel;
        }

        protected override void Unsubscribe()
        {
            InputAction.performed -= ActionPerform;
            InputAction.canceled -= ActionCancel;
        }
        
        protected override void ActionPerform(InputAction.CallbackContext obj)
        {
            if (InputBlocked)
                return;
            Event?.Invoke(true);
        }

        protected override void ActionCancel(InputAction.CallbackContext obj)
        {
            if (InputBlocked)
                return;
            Event?.Invoke(false);
        }
    }
}