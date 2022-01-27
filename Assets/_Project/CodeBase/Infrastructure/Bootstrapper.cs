using _Project.CodeBase.Infrastructure.States;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public Bootstrapper(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public void Initialize() => 
            _gameStateMachine.Enter<BootstrapState>();
    }
}