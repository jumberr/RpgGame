﻿using System.Threading.Tasks;
using _Project.CodeBase.Hero;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Services;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.UI.Elements;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.States
{
    // State that should be used to initialize scene and game objects
    public class InitializeGameSceneState : IState
    {
        // TODO: Change const string to SO & static data
        private const string NextScene = "Game";
        private const string InitialPoint = "InitialPoint";

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private IStaticDataService _staticDataService;

        public InitializeGameSceneState(
            SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IGameFactory gameFactory,
            IUIFactory uiFactory,
            IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(NextScene, OnLoaded);
        }

        private async void OnLoaded()
        {
            await _staticDataService.Load();
            await InitializeGameWorld();
            InformProgressReaders();

            _loadingCurtain.Hide();
        }

        private async UniTask InitializeGameWorld()
        {
            //var data = LoadPlayerStaticData();
            var hero = await InitializePlayer();
            await InitializeUIRoot();
            await InitializeHud(hero);
        }

        //private PlayerStaticData LoadPlayerStaticData() =>
        //    _staticDataService.ForPlayer();

        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.Progress);
        }

        private async UniTask<GameObject> InitializePlayer()
        {
            return await _gameFactory.CreateHero(GameObject.FindWithTag(InitialPoint).transform.position);
        }

        private async UniTask InitializeUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private async UniTask InitializeHud(GameObject hero)
        {
            var hud = await _uiFactory.CreateHud();
            hud.GetComponentInChildren<ActorUI>()
                .Construct(hero.GetComponent<HeroHealth>());
        }

        public void Exit() { }
    }
}