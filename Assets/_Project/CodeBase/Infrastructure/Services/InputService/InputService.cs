using System.Collections.Generic;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class InputService
    {
        public readonly ActionVector2Wrapper MoveAction;
        public readonly ActionBoolWrapper RunningAction;
        public readonly ActionVector2Wrapper RotateAction;
        public readonly ActionBoolWrapper AttackAction;
        public readonly ActionWrapper JumpAction;
        public readonly ActionWrapper CrouchAction;
        public readonly ActionWrapper ScopeAction;
        public readonly ActionWrapper ReloadAction;
        public readonly ActionWrapper InteractAction;
        public readonly ActionWrapper BackAction;
        public readonly ActionWrapper InventoryAction;

        private readonly List<BaseActionWrapper> _actions;
        private readonly InputMaster _input;
        private bool _inputBlocked;

        private InputService()
        {
            _input = new InputMaster();
            _input.Enable();

            MoveAction = new ActionVector2Wrapper(_input.PlayerMovement.Move);
            RunningAction = new ActionBoolWrapper(_input.PlayerMovement.Run);
            RotateAction = new ActionVector2Wrapper(_input.PlayerMovement.Rotation);
            JumpAction = new ActionWrapper(_input.PlayerMovement.Jump);
            CrouchAction = new ActionWrapper(_input.PlayerMovement.Crouch);
            
            AttackAction = new ActionBoolWrapper(_input.PlayerFight.Attack);
            ScopeAction = new ActionWrapper(_input.PlayerFight.Scope);
            ReloadAction = new ActionWrapper(_input.PlayerFight.Reload);
            
            InteractAction = new ActionWrapper(_input.PlayerActions.Interact);

            BackAction = new ActionWrapper(_input.UI.Back);
            InventoryAction = new ActionWrapper(_input.UI.Inventory);

            _actions = new List<BaseActionWrapper>
            {
                MoveAction, RunningAction, RotateAction, JumpAction, AttackAction, ScopeAction, ReloadAction, InteractAction, BackAction, InventoryAction
            };
            
            EnableInput();
        }

        ~InputService() => 
            _input.Disable();
        
        public void EnableInput()
        { 
            _inputBlocked = false;
            _actions.ForEach(x => x.InputBlocked = _inputBlocked);
        }

        public void BlockInput()
        {
            _inputBlocked = true;
            _actions.ForEach(x => x.InputBlocked = _inputBlocked);
        }
    }
}