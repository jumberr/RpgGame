using System;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class ActionWrapper : BaseActionWrapper
    {
        public event Action Event;

        public ActionWrapper(InputAction inputAction) : base(inputAction)
        {
        }

        protected override void Subscribe() => 
            InputAction.performed += ActionPerform;

        protected override void Unsubscribe() => 
            InputAction.performed -= ActionPerform;

        protected override void ActionPerform(InputAction.CallbackContext obj)
        {
            if (InputBlocked)
                return;
            Event?.Invoke();
        }
    }
}