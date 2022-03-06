using _Project.CodeBase.Infrastructure.Factory;
using Zenject;

namespace _Project.CodeBase.Infrastructure.States
{
    public class ReloadSceneState : IState
    {
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
        private readonly IGameFactory _gameFactory;

        public ReloadSceneState(
            LazyInject<IGameStateMachine> gameStateMachine,
            IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _gameFactory.Cleanup();
            _gameStateMachine.Value.Enter<BootstrapState>();
        }

        public void Exit() { }
    }
}