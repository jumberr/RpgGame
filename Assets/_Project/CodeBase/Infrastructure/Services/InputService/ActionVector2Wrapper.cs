using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class ActionVector2Wrapper : BaseActionWrapper
    {
        public event Action<Vector2> Event;
        
        public ActionVector2Wrapper(InputAction inputAction) : base(inputAction)
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
            Event?.Invoke(obj.ReadValue<Vector2>());
        }

        protected override void ActionCancel(InputAction.CallbackContext obj)
        {
            if (InputBlocked)
                return;
            Event?.Invoke(obj.ReadValue<Vector2>());
        }
    }
}