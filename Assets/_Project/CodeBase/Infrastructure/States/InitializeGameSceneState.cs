using _Project.CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.States
{
    // State that should be used to initialize scene and game objects
    public class InitializeGameSceneState : IState
    {
        private const string NextScene = "Game";
        private const string InitialPoint = "InitialPoint";

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public InitializeGameSceneState(
            SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IGameFactory gameFactory,
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(NextScene, OnLoaded);
        }

        private void OnLoaded()
        {
            InitializeGameWorld();
            _loadingCurtain.Hide();
        }

        private void InitializeGameWorld()
        {
            InitializePlayer();
            InitializeUIRoot();
            InitializeHud();
        }

        private void InitializePlayer() => 
            _gameFactory.CreateHero(GameObject.FindWithTag(InitialPoint));
        
        private void InitializeUIRoot() => 
            _uiFactory.CreateUIRoot();
        
        private void InitializeHud() => 
            _uiFactory.CreateHud();

        public void Exit() { }
    }
}