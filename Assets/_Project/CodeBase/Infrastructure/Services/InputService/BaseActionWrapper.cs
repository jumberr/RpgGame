using UnityEngine.InputSystem;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class BaseActionWrapper
    {
        public InputAction InputAction;
        public bool InputBlocked;

        public BaseActionWrapper(InputAction inputAction)
        {
            InputAction = inputAction;
            Subscribe();
        }
        
        ~BaseActionWrapper() => 
            Unsubscribe();

        public void EnableInput() => 
            InputBlocked = false;
        
        public void DisableInput() => 
            InputBlocked = true;

        protected virtual void Subscribe()
        {
        }

        protected virtual void Unsubscribe()
        {
        }

        protected virtual void ActionPerform(InputAction.CallbackContext obj)
        {
        }
        
        protected virtual void ActionCancel(InputAction.CallbackContext obj)
        {
        }
    }
}