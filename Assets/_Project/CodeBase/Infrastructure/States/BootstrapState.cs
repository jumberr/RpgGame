using Zenject;

namespace _Project.CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        // TODO: Change const string to SO & static data
        private const string InitialScene = "Initial";

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;

        public BootstrapState(
            SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            LazyInject<IGameStateMachine> gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        { 
            _loadingCurtain.Show();
            _sceneLoader.Load(InitialScene, OnLoaded);
        }

        public void Exit() { }
        
        private void OnLoaded()
        {
            //_loadingCurtain.Hide();
            _gameStateMachine.Value.Enter<LoadProgressState>();
        }
    }
}