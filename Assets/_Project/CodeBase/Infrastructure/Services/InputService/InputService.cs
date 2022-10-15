using System.Collections.Generic;

namespace _Project.CodeBase.Infrastructure.Services.InputService
{
    public class InputService
    {
        public readonly ActionVector2Wrapper MoveAction;
        public readonly ActionVector2Wrapper RotateAction;
        public readonly ActionBoolWrapper AttackAction;
        public readonly ActionWrapper JumpAction;
        public readonly ActionWrapper ScopeAction;
        public readonly ActionWrapper ReloadAction;
        public readonly ActionWrapper InteractAction;
            
        private readonly List<BaseActionWrapper> _actions;
        private readonly InputMaster _input;
        private bool _inputBlocked;
        
        private InputService()
        {
            _input = new InputMaster();
            _input.Enable();

            MoveAction = new ActionVector2Wrapper(_input.PlayerMovement.Move);
            RotateAction = new ActionVector2Wrapper(_input.PlayerMovement.Rotation);
            JumpAction = new ActionWrapper(_input.PlayerMovement.Jump);
            
            
            AttackAction = new ActionBoolWrapper(_input.PlayerFight.Attack);
            ScopeAction = new ActionWrapper(_input.PlayerFight.Scope);
            ReloadAction = new ActionWrapper(_input.PlayerFight.Reload);
            
            InteractAction = new ActionWrapper(_input.PlayerActions.Interact);

            _actions = new List<BaseActionWrapper>
            {
                    MoveAction, RotateAction, JumpAction, AttackAction, ScopeAction, ReloadAction, InteractAction
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