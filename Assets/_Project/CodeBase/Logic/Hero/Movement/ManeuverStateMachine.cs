using Better.StateMachine.Runtime;
using Better.StateMachine.Runtime.Modules;
using Better.StateMachine.Runtime.Modules.Transitions;

namespace _Project.CodeBase.Logic.Hero
{
    public class ManeuverStateMachine
    {
        private StateMachine<ManeuverState> _stateMachine;
        private StatesCacheModule<ManeuverState> _cacheModule;
        private AutoTransitionsModule<ManeuverState> _autoTransitionsModule;
        
        
        void S()
        {
            InitCacheModule();
            InitAutoTransitionModule();
            InitStateMachine();

            stateMachine.ChangeState<IdleState>();
            stateMachine.ChangeState<MoveState>();
            stateMachine.ChangeState<SprintState>();
            
            _stateMachine.Run();
        }

        private void InitCacheModule()
        {
            _cacheModule = new StatesCacheModule<ManeuverState>();
            _cacheModule.Cache<IdleState>();
            _cacheModule.Cache<MoveState>();
            _cacheModule.Cache<SprintState>();
        }

        private void InitAutoTransitionModule() => 
            _autoTransitionsModule = new AutoTransitionsModule<ManeuverState>();

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine<ManeuverState>();
            _stateMachine.AddModule(_autoTransitionsModule);
            _stateMachine.AddModule(_cacheModule);
        }
    }
}