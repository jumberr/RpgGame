using _Project.CodeBase.Infrastructure.Services.StaticData;
using Zenject;

namespace _Project.CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IStaticDataService staticDataService,
            LazyInject<IGameStateMachine> gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        { 
            _loadingCurtain.Show();

            await _staticDataService.LoadMenuStaticData();
            var projectSettings = _staticDataService.ForProjectSettings();
            _sceneLoader.Load(projectSettings.InitialScene, OnLoaded);
        }

        public void Exit() { }
        
        private void OnLoaded() => 
            _gameStateMachine.Value.Enter<LoadProgressState>();
    }
}