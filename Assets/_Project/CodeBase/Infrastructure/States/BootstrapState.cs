using Zenject;

namespace _Project.CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string NextScene = "Menu";

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly LazyInject<GameStateMachine> _gameStateMachine;

        public BootstrapState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, LazyInject<GameStateMachine> gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(NextScene, OnLoaded);
        }

        public void Exit() { }
        
        private void OnLoaded()
        {
            _loadingCurtain.Hide();
            _gameStateMachine.Value.Enter<MainMenuState>();
        }
    }
}