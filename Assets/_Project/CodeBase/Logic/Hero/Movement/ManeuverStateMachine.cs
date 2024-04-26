using System;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules;
using Better.StateMachine.Runtime.Modules.Transitions;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class ManeuverStateMachine : MonoBehaviour, IInitializable, IDisposable
    {
        private readonly InputService _inputService;
        private readonly ManeuverState[] _states;
        private readonly MovementData _movementData;
        private StateMachine<ManeuverState> _stateMachine;
        private StatesCacheModule<ManeuverState> _cacheModule;
        private AutoTransitionsModule<ManeuverState> _autoTransitionsModule;


        public ManeuverStateMachine(InputService inputService, ManeuverState[] states, IStaticDataService staticDataService)
        {
            _inputService = inputService;
            _states = states;
            _movementData = staticDataService.ForPlayer().MovementData;
        }
        
        public void Initialize()
        {
            InitCacheModule();
            InitAutoTransitionModule();
            InitStateMachine();

            //stateMachine.ChangeState<IdleState>();
            //stateMachine.ChangeState<MoveState>();
            //stateMachine.ChangeState<SprintState>();
            
            _stateMachine.Run();
            Subscribe();
        }

        public void Dispose() => 
            Cleanup();

        private void Update()
        {
            if (!IsRunning()) return;
            _stateMachine.CurrentState.Update();
        }
        
        private void FixedUpdate()
        {
            if (!IsRunning()) return;
            _stateMachine.CurrentState.FixedUpdate();
        }

        private void Subscribe()
        {
            _inputService.JumpAction.Event += TryEnterJumpState;
            _inputService.CrouchAction.Event += ToggleCrouch;
        }

        private void Cleanup()
        {
            _inputService.JumpAction.Event -= TryEnterJumpState;
            _inputService.CrouchAction.Event -= ToggleCrouch;
        }

        private void InitCacheModule()
        {
            _cacheModule = new StatesCacheModule<ManeuverState>();
            foreach (var state in _states)
            {
                state.Init(null, null, default); // todo
                _cacheModule.Cache(state);
            }
        }

        private void InitAutoTransitionModule()
        {
            _autoTransitionsModule = new AutoTransitionsModule<ManeuverState>();
            
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine<ManeuverState>();
            _stateMachine.AddModule(_autoTransitionsModule);
            _stateMachine.AddModule(_cacheModule);
        }

        private void TryEnterJumpState()
        {
            if (can)
            {
                _stateMachine.ChangeState(GetState<JumpState>());
            }
        }

        private void ToggleCrouch()
        {
            var crouching = _stateMachine.CurrentState is CrouchState;
            var crouchState = GetState<CrouchState>();
            
            if (!crouching && crouchState.CanEnter())
                _stateMachine.ChangeState(crouchState);
            
            else if (crouching)
                _stateMachine.ChangeState(GetState<IdleState>());
        }

        private T GetState<T>() where T : ManeuverState => 
            _cacheModule.Get<T>();

        private bool IsRunning() => 
            _stateMachine != null && _stateMachine.IsRunning;
    }
}