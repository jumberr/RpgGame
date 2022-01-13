using _Project.CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.States
{
    // State that should be used to initialize scene and game objects
    public class InitializeGameSceneState : IState
    {
        private const string NextScene = "Game";
        private const string InitialPoint = "InitialPoint";

        private SceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;
        private IGameFactory _gameFactory;

        public InitializeGameSceneState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(NextScene, OnLoaded);
        }

        private void OnLoaded()
        {
            InitializePlayer();
            _loadingCurtain.Hide();
        }

        private void InitializePlayer() => 
            _gameFactory.CreateHero(GameObject.FindWithTag(InitialPoint));

        public void Exit() { }
    }
}