using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules;
using Better.StateMachine.Runtime.Modules.Transitions;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Logic.Hero
{
    public class ManeuverStateMachine : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private ManeuverState[] _states;
        private VelocityController _velocityController;
        private InputService _inputService;
        private IStaticDataService _staticDataService;
        private StateMachine<ManeuverState> _stateMachine;
        private StatesCacheModule<ManeuverState> _cacheModule;
        private AutoTransitionsModule<ManeuverState> _autoTransitionsModule;
        private MovementData _movementData;


        [Inject]
        public void Construct(
            IGameFactory gameFactory,
            ManeuverState[] states,
            VelocityController velocityController,
            InputService inputService,
            IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _states = states;
            _velocityController = velocityController;
            _inputService = inputService;
            _staticDataService = staticDataService;
        }

        private void Awake() => 
            _gameFactory.HeroCreated += OnHeroCreated;

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
        
        private void OnDestroy() => 
            Cleanup();
        
        private void OnHeroCreated(HeroFacade facade)
        {
            InitCacheModule();
            InitAutoTransitionModule();
            InitStateMachine();

            //stateMachine.ChangeState<IdleState>();
            //stateMachine.ChangeState<MoveState>();
            //stateMachine.ChangeState<SprintState>();
            
            _stateMachine.Run();
            _stateMachine.ChangeState(GetState<MoveState>());
            Subscribe();
        }

        private void Subscribe()
        {
            _gameFactory.HeroCreated -= OnHeroCreated;
            _inputService.JumpAction.Event += TryEnterJumpState;
            _inputService.CrouchAction.Event += ToggleCrouch;
        }

        private void Cleanup()
        {
            _inputService.JumpAction.Event -= TryEnterJumpState;
            _inputService.CrouchAction.Event -= ToggleCrouch;
            
            foreach (var state in _states) 
                state.Dispose();
        }

        private void InitCacheModule()
        {
            _cacheModule = new StatesCacheModule<ManeuverState>();
            _movementData = _staticDataService.ForPlayer().MovementData;

            foreach (var state in _states)
            {
                state.Init(_gameFactory, _velocityController, _movementData); // todo
                _cacheModule.Cache(state);
            }
        }

        private void InitAutoTransitionModule()
        {
            _autoTransitionsModule = new AutoTransitionsModule<ManeuverState>(); // todo
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine<ManeuverState>();
            _stateMachine.AddModule(_autoTransitionsModule);
            _stateMachine.AddModule(_cacheModule);
        }

        private void TryEnterJumpState()
        {
            var jumpState = GetState<JumpState>();
            if (jumpState.CanEnter()) 
                _stateMachine.ChangeState(jumpState);
        }

        private void ToggleCrouch()
        {
            var crouching = _stateMachine.CurrentState is CrouchState;
            var crouchState = GetState<CrouchState>();
            
            if (!crouching && crouchState.CanEnter())
                _stateMachine.ChangeState(crouchState);
            
            else if (crouching)
                _stateMachine.ChangeState(GetState<MoveState>());
        }

        private T GetState<T>() where T : ManeuverState => 
            _cacheModule.Get<T>();

        private bool IsRunning() => 
            _stateMachine != null && _stateMachine.IsRunning;
    }
}