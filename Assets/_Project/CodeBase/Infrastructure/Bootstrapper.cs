using _Project.CodeBase.Infrastructure.States;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly GameStateMachine _gameStateMachine;

        public Bootstrapper(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public void Initialize() => 
            _gameStateMachine.Enter<BootstrapState>();
    }
}