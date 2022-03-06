using _Project.CodeBase.Infrastructure.Factory;

namespace _Project.CodeBase.Infrastructure.States
{
    public class ExitGameState : IState
    {
        private readonly IGameFactory _gameFactory;

        public ExitGameState(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void Enter() => 
            _gameFactory.Cleanup();

        public void Exit() { }
    }
}