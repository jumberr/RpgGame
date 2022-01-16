using Zenject;

namespace _Project.CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private LazyInject<GameStateMachine> _gameStateMachine;
        private LoadingCurtain _loadingCurtain;

        public MainMenuState(LazyInject<GameStateMachine> gameStateMachine, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter() => 
            _gameStateMachine.Value.Enter<InitializeGameSceneState>();

        public void Exit() { }
    }
}