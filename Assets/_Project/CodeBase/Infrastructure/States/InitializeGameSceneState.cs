using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.UI.Elements.Hud;
using _Project.CodeBase.UI.Services;
using _Project.CodeBase.UI.Services.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Infrastructure.States
{
    public class InitializeGameSceneState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IWindowService _windowService;

        public InitializeGameSceneState(SceneLoader sceneLoader,
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
            var projectSettings = _staticDataService.ForProjectSettings();
            _sceneLoader.Load(projectSettings.GameScene, OnLoaded);
        }
        
        public void Exit() { }

        private async void OnLoaded()
        {
            await InitializeGameWorld();
            InformProgressReaders();

            _loadingCurtain.Hide();
        }

        private async UniTask InitializeGameWorld()
        {
            var hero = await InitializePlayer();
            await InitializeInteractableSpawner(hero);
            await InitializeUI(hero);
        }

        private async UniTask InitializeInteractableSpawner(GameObject hero) => 
            await _gameFactory.CreateInteractableSpawner(hero);

        private async UniTask InitializeUI(GameObject hero)
        {
            await InitializeUIRoot();
            await InitializeHud();

            _uiFactory.ConstructActorUI(hero);
            
            InitializeInventory(hero);
            InitializeSettings(hero);
        }

        private async UniTask<GameObject> InitializePlayer() => 
            await _gameFactory.CreateHero(Vector3.zero);

        private async UniTask InitializeUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private async UniTask InitializeHud() => 
            await _uiFactory.CreateHud();

        private void InitializeInventory(GameObject hero) => 
             _uiFactory.CreateInventory(hero);

        private void InitializeSettings(GameObject hero)
        {
            var settings = _uiFactory.CreateSettings(hero.GetComponent<HeroRotation>());
            _gameFactory.AddProgressWatchers(settings);
        }

        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.Progress);
        }
    }
}