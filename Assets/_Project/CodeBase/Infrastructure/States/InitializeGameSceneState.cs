using System.Threading.Tasks;
using _Project.CodeBase.Infrastructure.Factory;
using _Project.CodeBase.Infrastructure.Services.InputService;
using _Project.CodeBase.Infrastructure.Services.PersistentProgress;
using _Project.CodeBase.Infrastructure.Services.StaticData;
using _Project.CodeBase.Logic.Hero;
using _Project.CodeBase.Logic.Hero.State;
using _Project.CodeBase.Logic.HeroWeapon;
using _Project.CodeBase.Logic.Interaction;
using _Project.CodeBase.UI.Elements;
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
            IStaticDataService staticDataService,
            IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _windowService = windowService;
        }

        public void Enter()
        {
            var projectSettings = _staticDataService.ForProjectSettings();
            _sceneLoader.Load(projectSettings.GameScene, OnLoaded);
        }

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
            await InitializeHud(hero);
            InitializeInventory(hero);
        }

        private async UniTask<GameObject> InitializePlayer() => 
            await _gameFactory.CreateHero(Vector3.zero);

        private async UniTask InitializeUIRoot() =>
            await _uiFactory.CreateUIRoot();

        private async UniTask InitializeHud(GameObject hero)
        {
            var hud = await _uiFactory.CreateHud();
            _uiFactory.SetupWindowButtons(_windowService);
            var actorUI = hud.GetComponentInChildren<ActorUI>();
            actorUI.Construct(hero.GetComponent<HeroHealth>(), hero.GetComponent<HeroAmmo>(),
                hero.GetComponent<WeaponController>(), hero.GetComponent<InputService>(),
                hero.GetComponent<HeroState>(), hero.GetComponent<Interaction>());
        }

        private void InitializeInventory(GameObject hero) => 
             _uiFactory.CreateInventory(hero);

        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_persistentProgressService.Progress);
        }

        public void Exit() { }
    }
}