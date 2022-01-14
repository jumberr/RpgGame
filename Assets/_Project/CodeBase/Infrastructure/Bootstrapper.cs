using System.Threading.Tasks;
using _Project.CodeBase.Infrastructure.States;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly GameStateMachine _gameStateMachine;

        public Bootstrapper(
            GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public async void Initialize()
        {
            await Task.Delay(10000);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}