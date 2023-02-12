using _Project.CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class GameScene : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;

        public GameScene(
            IGameStateMachine gameStateMachine,
            LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
        }

        public async void Initialize()
        {
            await _gameStateMachine.Enter<LoadProgressState>();
            await _gameStateMachine.Enter<InitializeGameSceneState>();
            _loadingCurtain.Hide().Forget();
        }
    }
}